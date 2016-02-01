using System;

namespace Uni.Core.Database.Sql
{
    public interface ISqlInstruction
    {
        string ToString(ISqlDialect dialect);

        object[] GetParameters();
    }
}
