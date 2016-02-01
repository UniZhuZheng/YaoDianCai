using System;
using System.Data;
using System.Text;
using System.Web;
using System.Collections.Generic;

using Uni.Core.Database.DebugProxy;
using Uni.Core.Database.Sql;
using Uni.Core.Common.Web;


namespace Uni.Core.Database
{
    public class DbManager : IDisposable
    {
        private readonly bool isDebug;

        private readonly bool shared;

        private ISqlDialect dialect;
        private volatile bool disposed;

        private IDbCommand command;
        private IDbCommand Command {
            get
            {
                CheckDispose();
                if (command == null) command = OpenConnection().CreateCommand();

                if (command.Connection.State == ConnectionState.Closed || command.Connection.State == ConnectionState.Broken)
                {
                    command = OpenConnection().CreateCommand();
                }
                return command;
            }
        }

        public string DatabaseId { get; private set; }

        public bool InTransaction { get { return Command.Transaction != null; } }

        public IDbConnection Connection { get { return Command.Connection; } }


        public DbManager(string databaseId)
            : this(databaseId, true, false)
        {
        }

        public DbManager(string databaseId, bool shared)
            : this(databaseId, shared, false)
        {
        }

        public DbManager(string databaseId, bool shared, bool debug)
        {
            if (databaseId == null) throw new ArgumentNullException("databaseId");

            DatabaseId = databaseId;
            this.shared = shared;
            this.isDebug = debug;
        }

        public void Dispose()
        {
            lock (this)
            {
                if (disposed) return;

                disposed = true;

                if (command != null)
                {
                    if (command.Connection != null)
                    {
                        command.Connection.Dispose();
                    }

                    command.Dispose();
                    command = null;
                }
            }
        }

        public static DbManager FromHttpContext(string databaseId)
        {
            if (HttpContext.Current != null)
            {
                DbManager dbManager = DisposableHttpContext.Current[databaseId] as DbManager;
                if (dbManager == null || dbManager.disposed)
                {
                    dbManager = new DbManager(databaseId);
                    DisposableHttpContext.Current[databaseId] = dbManager;
                }
                return dbManager;
            }
            return new DbManager(databaseId);
        }

        public IDbConnection OpenConnection()
        {
            CheckDispose();
            IDbConnection connection = null;
            string key = null;
            if (shared && HttpContext.Current != null)
            {
                key = string.Format("Connection {0}|{1}", GetDialect(), DbRegistry.GetConnectionString(DatabaseId));
                connection = DisposableHttpContext.Current[key] as IDbConnection;
                if (connection != null)
                {
                    ConnectionState state = ConnectionState.Closed;
                    bool disposed = false;
                    try
                    {
                        state = connection.State;
                    }
                    catch (ObjectDisposedException)
                    {
                        disposed = true;
                    }

                    if (!disposed && (state == ConnectionState.Closed || state == ConnectionState.Broken))
                    {
                        if (string.IsNullOrEmpty(connection.ConnectionString))
                        {
                            connection.ConnectionString = DbRegistry.GetConnectionString(DatabaseId);
                        }
                        connection.Open();
                        return connection;
                    }
                }
            }

            connection = DbRegistry.CreateDbConnection(DatabaseId);

            if (isDebug)
            {
                connection = new DbConnectionDebugProxy(connection);
            }

            connection.Open();

            if (shared && HttpContext.Current != null)
            {
                DisposableHttpContext.Current[key] = connection;
            }

            return connection;
        }

        public IDbTransaction BeginTransaction()
        {
            if (InTransaction) throw new InvalidOperationException("Transaction already open.");

            Command.Transaction = Command.Connection.BeginTransaction();

            DbTransaction tx = new DbTransaction(Command.Transaction);
            tx.Unavailable += TransactionUnavailable;
            return tx;
        }

        public IDbTransaction BeginTransaction(IsolationLevel il)
        {
            if (InTransaction) throw new InvalidOperationException("Transaction already open.");

            il = GetDialect().GetSupportedIsolationLevel(il);
            Command.Transaction = Command.Connection.BeginTransaction(il);

            DbTransaction tx = new DbTransaction(Command.Transaction);
            tx.Unavailable += TransactionUnavailable;
            return tx;
        }

        public List<object[]> ExecuteList(string sql, params object[] parameters)
        {
            return Command.ExecuteList(sql, parameters);
        }

        public List<object[]> ExecuteList(ISqlInstruction sql)
        {
            return Command.ExecuteList(sql, GetDialect());
        }

        public List<T> ExecuteList<T>(ISqlInstruction sql, Converter<IDataRecord, T> converter)
        {
            return Command.ExecuteList(sql, GetDialect(), converter);
        }

        public T ExecuteScalar<T>(string sql)
        {
            return ExecuteScalar<T>(sql, null);
        }

        public T ExecuteScalar<T>(string sql, params object[] parameters)
        {
            return Command.ExecuteScalar<T>(sql, parameters);
        }

        public T ExecuteScalar<T>(ISqlInstruction sql)
        {
            return Command.ExecuteScalar<T>(sql, GetDialect());
        }

        public int ExecuteNonQuery(string sql, params object[] parameters)
        {
            return Command.ExecuteNonQuery(sql, parameters);
        }

        public int ExecuteNonQuery(ISqlInstruction sql)
        {
            return Command.ExecuteNonQuery(sql, GetDialect());
        }

        public int ExecuteBatch(IEnumerable<ISqlInstruction> batch)
        {
            if (batch == null) throw new ArgumentNullException("batch");

            var affected = 0;
            using (var tx = BeginTransaction())
            {
                foreach (var sql in batch)
                {
                    affected += ExecuteNonQuery(sql);
                }
                tx.Commit();
            }
            return affected;
        }

        private void TransactionUnavailable(object sender, EventArgs e)
        {
            if (Command.Transaction != null)
            {
                Command.Transaction = null;
            }
        }

        private void CheckDispose()
        {
            if (disposed) throw new ObjectDisposedException(GetType().FullName);
        }

        private ISqlDialect GetDialect()
        {
            return dialect ?? (dialect = DbRegistry.GetSqlDialect(DatabaseId));
        }

        //private void AdoProxyExecutedEventHandler(ExecutedEventArgs args)
        //{
        //    ThreadContext.Properties["duration"] = args.Duration.TotalMilliseconds;
        //    ThreadContext.Properties["sql"] = RemoveWhiteSpaces(args.Sql);
        //    ThreadContext.Properties["sqlParams"] = RemoveWhiteSpaces(args.SqlParams);
        //    logger.Debug(args.SqlMethod);
        //}

        //private string RemoveWhiteSpaces(string str)
        //{
        //    return !string.IsNullOrEmpty(str)
        //        ? str.Replace(Environment.NewLine, " ").Replace("\n", "").Replace("\r", "").Replace("\t", " ")
        //        : string.Empty;
        //}
    }
}
