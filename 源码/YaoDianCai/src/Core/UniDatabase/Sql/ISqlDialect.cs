using System;
using System.Data;
using System.Text;


namespace Uni.Core.Database.Sql
{
    public interface ISqlDialect
    {
        string TypeOfDataBase{ get; }
        string IdentityQuery { get; }
        string Autoincrement { get; }
        string InsertIgnore  { get; }

        bool SupportMultiTableUpdate { get; }
        bool SeparateCreateIndex     { get; }

        string DbTypeToString(DbType type, int size, int precision);

        IsolationLevel GetSupportedIsolationLevel(IsolationLevel il);
    }
}
