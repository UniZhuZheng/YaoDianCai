using System;
using System.Text;

namespace UNI.Core.Database.Mapper
{
    [AttributeUsage(AttributeTargets.Class)]
    public class DbTableAttribute : Attribute
    {
        public string TableName { get; private set; }

        public DbTableAttribute(string tableName)
        {
            TableName = tableName;
        }
    }
}
