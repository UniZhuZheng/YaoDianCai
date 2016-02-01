using System;
using System.Text;

using Uni.Core.Database.Sql.Expressions;


namespace Uni.Core.Database.Sql
{
    public class SqlDelete : ISqlInstruction
    {
        private readonly string table;
        private Exp where = Exp.Empty;

        public SqlDelete(string table)
        {
            this.table = table;
        }

        public string ToString(ISqlDialect dialect)
        {
            StringBuilder sql = new StringBuilder();
            sql.AppendFormat("delete from {0}", table);

            if (where != Exp.Empty)
            {
                sql.AppendFormat(" where {0}", where.ToString(dialect));
            }

            return sql.ToString();
        }

        public object[] GetParameters()
        {
            return where != Exp.Empty ? where.GetParameters() : new object[0];
        }

        public SqlDelete Where(Exp where)
        {
            this.where = this.where & where;
            return this;
        }

        public SqlDelete Where(string column, object value)
        {
            return Where(Exp.Eq(column, value));
        }

        public override string ToString()
        {
            return ToString(SqlDialect.Default);
        }
    }
}
