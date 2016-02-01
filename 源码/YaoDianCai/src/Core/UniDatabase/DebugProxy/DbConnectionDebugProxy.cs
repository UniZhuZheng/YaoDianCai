using System;
using System.Text;
using System.Data;

namespace Uni.Core.Database.DebugProxy
{
    class DbConnectionDebugProxy : IDbConnection
    {
        private bool disposed;

        private readonly IDbConnection connection;

        public string ConnectionString { get { return connection.ConnectionString; } set { connection.ConnectionString = value; } }
        public int ConnectionTimeout   { get { return connection.ConnectionTimeout; } }
        public ConnectionState State   { get { return connection.State; } }
        public string Database         { get { return connection.Database; } }


        public DbConnectionDebugProxy(IDbConnection connection)
        {
            if (connection == null) throw new ArgumentNullException("connection");

            this.connection = connection;
        }

        public IDbTransaction BeginTransaction(IsolationLevel il)
        {
            using (ActionTimer.Begin(dur => AdoDebugProfile.DebugOut(this, String.Format("BeginTransaction({0})", il), dur)))
            {
                return new DbTransactionDebugProxy(connection.BeginTransaction(il));
            }
        }

        public IDbTransaction BeginTransaction()
        {
            using (ActionTimer.Begin(dur => AdoDebugProfile.DebugOut(this, "BeginTransaction", dur)))
            {
                return new DbTransactionDebugProxy(connection.BeginTransaction());
            }
        }

        public void ChangeDatabase(string databaseName)
        {
            connection.ChangeDatabase(databaseName);
        }

        public void Close()
        {
            connection.Close();
        }

        public IDbCommand CreateCommand()
        {
            return new DbCommandDebugProxy(connection.CreateCommand());
        }

        public void Open()
        {
            using (ActionTimer.Begin(dur => AdoDebugProfile.DebugOut(this, "Open", dur)))
            {
                connection.Open();
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    using (ActionTimer.Begin(dur => AdoDebugProfile.DebugOut(this, "Dispose", dur)))
                    {
                        connection.Dispose();
                    }
                }
                disposed = true;
            }
        }

        ~DbConnectionDebugProxy()
        {
            Dispose(false);
        }
    }
}
