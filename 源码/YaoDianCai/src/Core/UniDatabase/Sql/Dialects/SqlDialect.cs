using System;
using System.Text;
using System.Data;


namespace Uni.Core.Database.Sql
{
    public class SqlDialect : ISqlDialect
    {
        public static readonly ISqlDialect Default = new SqlDialect();
        public virtual string TypeOfDataBase{ get { return "sqlserver"; } }
        public virtual string IdentityQuery { get { return "@@identity"; } }
        public virtual string Autoincrement { get { return "AUTOINCREMENT"; } }
        public virtual string InsertIgnore  { get { return "insert ignore"; } }

        public virtual bool SupportMultiTableUpdate { get { return true; } }
        public virtual bool SeparateCreateIndex { get { return true; } }

        public virtual string DbTypeToString(DbType type, int size, int precision)
        {
            StringBuilder s = new StringBuilder(type.ToString().ToLower());
            if (0 < size)
            {
                s.AppendFormat(0 < precision ? "({0}, {1})" : "({0})", size, precision);
            }
            return s.ToString();
        }

        public virtual IsolationLevel GetSupportedIsolationLevel(IsolationLevel il)
        {
            return il;
        }
    }
}
