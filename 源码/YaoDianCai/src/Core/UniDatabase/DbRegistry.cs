using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;
using System.Configuration;

using Uni.Core.Database.Sql;
using Uni.Core.Database.Sql.Dialects;



namespace Uni.Core.Database
{
    public class DbRegistry
    {
        private static readonly object syncRoot = new object();

        private static readonly IDictionary<string, DbProviderFactory> providers = new Dictionary<string, DbProviderFactory>();
        private static readonly IDictionary<string, string> connnectionStrings = new Dictionary<string, string>();
        private static readonly IDictionary<string, ISqlDialect> dialects = new Dictionary<string, ISqlDialect>();

        static DbRegistry()
        {
            dialects["MySql.Data.MySqlClient.MySqlClientFactory"] = new MySQLDialect();
            dialects["Devart.Data.MySql.MySqlProviderFactory"]    = new MySQLDialect();
            dialects["System.Data.SQLite.SQLiteFactory"]          = new SQLiteDialect();
            dialects["System.Data.SqlClient.SqlClientFactory"]    = new SqlDialect();
        }

        public static void RegisterDatabase(string databaseId, DbProviderFactory providerFactory)
        {
            RegisterDatabase(databaseId, providerFactory, null);
        }

        public static void RegisterDatabase(string databaseId, DbProviderFactory providerFactory, string connectionString)
        {
            if (string.IsNullOrEmpty(databaseId)) throw new ArgumentNullException("databaseId");
            if (providerFactory == null) throw new ArgumentNullException("providerFactory");

            lock (syncRoot)
            {
                if (!providers.ContainsKey(databaseId))
                {
                    providers.Add(databaseId, providerFactory);
                    if (!string.IsNullOrEmpty(connectionString))
                    {
                        connnectionStrings.Add(databaseId, connectionString);
                    }
                }
            }
        }

        public static bool IsDatabaseRegistered(string databaseId)
        {
            lock (syncRoot)
            {
                return providers.ContainsKey(databaseId);
            }
        }

        public static IDbConnection CreateDbConnection(string databaseId)
        {
            DbConnection connection = providers[databaseId].CreateConnection();
            if (connnectionStrings.ContainsKey(databaseId))
            {
                connection.ConnectionString = connnectionStrings[databaseId];
            }
            return connection;
        }

        public static DbProviderFactory GetDbProviderFactory(string databaseId)
        {
            return providers.ContainsKey(databaseId) ? providers[databaseId] : null;
        }

        public static string GetConnectionString(string databaseId)
        {
            return connnectionStrings.ContainsKey(databaseId) ? connnectionStrings[databaseId] : null;
        }

        public static ISqlDialect GetSqlDialect(string databaseId)
        {
            DbProviderFactory provider = GetDbProviderFactory(databaseId);
            if (provider != null && dialects.ContainsKey(provider.GetType().FullName))
            {
                return dialects[provider.GetType().FullName];
            }
            return SqlDialect.Default;
        }
    }
}
