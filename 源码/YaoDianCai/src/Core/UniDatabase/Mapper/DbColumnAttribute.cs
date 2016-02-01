using System;


namespace UNI.Core.Database.Mapper
{
    [AttributeUsage(AttributeTargets.Property)]
    public class DbColumnAttribute : Attribute
    {
        public bool IsKey        { get; private set; }
        public bool IsIdentity   { get; private set; }
        public Type DataBaseType { get; private set; }
        public string Column     { get; private set; }

        public DbColumnAttribute(string column, bool isKey, bool isIdentity)
            : this(column, isKey, isIdentity, null)
        {
        }

        public DbColumnAttribute(string column)
            : this(column, null)
        {
        }

        public DbColumnAttribute(string column, Type dataBaseType)
            : this(column, false, dataBaseType)
        {
        }

        public DbColumnAttribute(string column, bool isKey, Type dataBaseType)
            : this(column, isKey, false, dataBaseType)
        {
        }

        public DbColumnAttribute(string column, bool isKey, bool isIdentity, Type dataBaseType)
        {
            IsKey = isKey;
            IsIdentity = isIdentity;
            DataBaseType = dataBaseType;
            Column = column;
        }  
    }
}
