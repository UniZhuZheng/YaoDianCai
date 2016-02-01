using System;
using System.Text;
using System.Collections.Generic;
using Uni.Core.Database.Sql.Expressions;


namespace Uni.Core.Database.Sql
{
    public class SqlUpdate : ISqlInstruction
    {
        private readonly List<string> expressions = new List<string>();
        private readonly Dictionary<string, object> sets = new Dictionary<string, object>();
        private readonly SqlIdentifier table;
        private Exp where = Exp.Empty;

        public SqlUpdate(string table)
        {
            this.table = (SqlIdentifier)table;
        }

        public string ToString(ISqlDialect dialect)
        {
            StringBuilder sql = new StringBuilder();
            sql.AppendFormat("update {0} set ", table.ToString(dialect));
            foreach (var set in sets)
            {
                sql.AppendFormat("{0} = {1}, ", set.Key, set.Value is ISqlInstruction ? ((ISqlInstruction)set.Value).ToString(dialect) : "?");
            }
            expressions.ForEach(expression => sql.AppendFormat("{0}, ", expression));
            sql.Remove(sql.Length - 2, 2);
            if (where != Exp.Empty)
            {
                sql.AppendFormat(" where {0}", where.ToString(dialect));
            }
            return sql.ToString();
        }

        public object[] GetParameters()
        {
            var parameters = new List<object>();
            foreach (var parameter in sets.Values)
            {
                if (parameter is ISqlInstruction)
                {
                    parameters.AddRange(((ISqlInstruction)parameter).GetParameters());
                }
                else
                {
                    parameters.Add(parameter);
                }
            }

            if (where != Exp.Empty)
            {
                parameters.AddRange(where.GetParameters());
            }

            return parameters.ToArray();
        }

        public SqlUpdate Set(string expression)
        {
            expressions.Add(expression);
            return this;
        }

        public SqlUpdate Set(string column, object value)
        {
            sets[column] = value is SqlQuery ? new SubExp((SqlQuery)value) : value;
            return this;
        }

        public SqlUpdate Where(Exp where)
        {
            this.where = this.where & where;
            return this;
        }

        public SqlUpdate Where(string column, object value)
        {
            return Where(Exp.Eq(column, value));
        }

        public override string ToString()
        {
            return ToString(SqlDialect.Default);
        }
    }
}
