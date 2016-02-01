using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using Uni.Core.Database.Sql;

namespace Uni.Core.Database
{
    public static class DbExtensions
    {
        public static List<object[]> ExecuteList(this IDbConnection connection, string sql, params object[] parameters)
        {
            using (IDbCommand command = connection.CreateCommand())
            {
                return command.ExecuteList(sql, parameters);
            }
        }

        public static T ExecuteScalar<T>(this IDbConnection connection, string sql, params object[] parameters)
        {
            using (IDbCommand command = connection.CreateCommand())
            {
                return command.ExecuteScalar<T>(sql, parameters);
            }
        }

        public static int ExecuteNonQuery(this IDbConnection connection, string sql, params object[] parameters)
        {
            using (IDbCommand command = connection.CreateCommand())
            {
                return command.ExecuteNonQuery(sql, parameters);
            }
        }

        public static IDbCommand CreateCommand(this IDbConnection connection, string sql, params object[] parameters)
        {
            IDbCommand command = connection.CreateCommand();
            command.CommandText = sql;
            command.AddParameters(parameters);
            return command;
        }


        public static IDbCommand AddParameter(this IDbCommand command, string name, object value)
        {
            IDbDataParameter p = command.CreateParameter();

            if (!string.IsNullOrEmpty(name))
            {
                p.ParameterName = name.StartsWith("@") ? name : "@" + name;
            }

            if (value == null)
            {
                p.Value = DBNull.Value;
            }
            else if (value is Enum)
            {
                p.Value = ((Enum)value).ToString("d");
            }
            else if (value is DateTime)
            {
                var d = (DateTime)value;
                p.Value = new DateTime(d.Year, d.Month, d.Day, d.Hour, d.Minute, d.Second, DateTimeKind.Unspecified);
            }
            else
            {
                p.Value = value;
            }

            command.Parameters.Add(p);
            return command;
        }

        public static IDbCommand AddParameters(this IDbCommand command, params object[] parameters)
        {
            if (parameters == null)
            {
                return command;
            }

            foreach (var value in parameters)
            {
                if (value != null && value.GetType().Name.StartsWith("<>f__AnonymousType"))
                {
                    foreach (var p in value.GetType().GetProperties())
                    {
                        command.AddParameter(p.Name, p.GetValue(value, null));
                    }
                }
                else
                {
                    command.AddParameter(null, value);
                }
            }
            return command;
        }

        public static List<object[]> ExecuteList(this IDbCommand command)
        {
            return ExecuteList(command, command.CommandText, null);
        }

        public static List<object[]> ExecuteList(this IDbCommand command, string sql, params object[] parameters)
        {
            command.CommandText = sql;
            if (parameters != null)
            {
                command.Parameters.Clear();
                command.AddParameters(parameters);
            }
            return ExecuteListReader(command);
        }

        private static List<object[]> ExecuteListReader(IDbCommand command)
        {
            List<object[]> result = new List<object[]>();
            using (IDataReader reader = command.ExecuteReader())
            {
                int fieldCount = reader.FieldCount;
                while (reader.Read())
                {
                    object[] row = new object[fieldCount];
                    for (var i = 0; i < fieldCount; i++)
                    {
                        row[i] = reader[i];
                        if (DBNull.Value.Equals(row[i]))
                        {
                            row[i] = null;
                        }
                    }
                    result.Add(row);
                }
            }
            return result;
        }

        public static T ExecuteScalar<T>(this IDbCommand command)
        {
            return ExecuteScalar<T>(command, command.CommandText, null);
        }

        public static T ExecuteScalar<T>(this IDbCommand command, string sql, params object[] parameters)
        {
            command.CommandText = sql;
            if (parameters != null)
            {
                command.Parameters.Clear();
                command.AddParameters(parameters);
            }

            var scalar = command.ExecuteScalar();

            if (scalar == null || scalar == DBNull.Value)
            {
                return default(T);
            }
            Type scalarType = typeof(T);
            if (scalarType == typeof(object))
            {
                return (T)scalar;
            }

            if (scalarType.Name == "Nullable`1")
            {
                scalarType = scalarType.GetGenericArguments()[0];
            }
            return (T)Convert.ChangeType(scalar, scalarType);
        }

        public static int ExecuteNonQuery(this IDbCommand command, string sql, params object[] parameters)
        {
            command.CommandText = sql;
            command.Parameters.Clear();
            command.AddParameters(parameters);
            return command.ExecuteNonQuery();
        }

        public static List<object[]> ExecuteList(this IDbCommand command, ISqlInstruction sql, ISqlDialect dialect)
        {
            ApplySqlInstruction(command, sql, dialect);
            return command.ExecuteList();
        }

        public static List<T> ExecuteList<T>(this IDbCommand command, ISqlInstruction sql, ISqlDialect dialect, Converter<IDataRecord, T> mapper)
        {
            ApplySqlInstruction(command, sql, dialect);
            var result = new List<T>();
            using (var reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    result.Add(mapper(reader));
                }
            }
            return result;
        }

        public static T ExecuteScalar<T>(this IDbCommand command, ISqlInstruction sql, ISqlDialect dialect)
        {
            ApplySqlInstruction(command, sql, dialect);
            return command.ExecuteScalar<T>();
        }

        public static int ExecuteNonQuery(this IDbCommand command, ISqlInstruction sql, ISqlDialect dialect)
        {
            ApplySqlInstruction(command, sql, dialect);
            return command.ExecuteNonQuery();
        }

        private static void ApplySqlInstruction(IDbCommand command, ISqlInstruction sql, ISqlDialect dialect)
        {
            string sqlStr = sql.ToString(dialect);
            object[] parameters = sql.GetParameters();
            command.Parameters.Clear();

            string[] sqlParts = sqlStr.Split('?');
            StringBuilder sqlBuilder = new StringBuilder();
            for (int i = 0; i < sqlParts.Length - 1; i++)
            {
                string name = "p" + i;
                command.AddParameter(name, parameters[i]);
                sqlBuilder.AppendFormat("{0}@{1}", sqlParts[i], name);
            }
            sqlBuilder.Append(sqlParts[sqlParts.Length - 1]);
            command.CommandText = sqlBuilder.ToString();
        }


        public static T Get<T>(this IDataRecord r, int i)
        {
            if (r.IsDBNull(i))
            {
                return default(T);
            }

            var value = r.GetValue(i);
            if (typeof(T) == typeof(Guid))
            {
                value = r.GetGuid(i);
            }
            return (T)Convert.ChangeType(value, typeof(T));
        }

        public static T Get<T>(this IDataRecord r, string name)
        {
            return Get<T>(r, r.GetOrdinal(name));
        }
    }
}
