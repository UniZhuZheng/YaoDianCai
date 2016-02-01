using System;
using System.Text;
using System.Data;

namespace Uni.Core.Database.DebugProxy
{
    class DbCommandDebugProxy : IDbCommand
    {
        private bool disposed;

        private readonly IDbCommand command;

        public string CommandText      { get { return command.CommandText; }    set { command.CommandText = value;    } }
        public int CommandTimeout      { get { return command.CommandTimeout; } set { command.CommandTimeout = value; } }
        public CommandType CommandType { get { return command.CommandType; }    set { command.CommandType = value;    } }
        public UpdateRowSource UpdatedRowSource    { get { return command.UpdatedRowSource; } set { command.UpdatedRowSource = value; } }
        public IDataParameterCollection Parameters { get { return command.Parameters; } }
        public IDbConnection Connection {
            get { return new DbConnectionDebugProxy(command.Connection); }
            set { command.Connection = (value is DbConnectionDebugProxy ? value : new DbConnectionDebugProxy(value)); }
        }
        public IDbTransaction Transaction {
            get { return command.Transaction; }
            set { command.Transaction = value is DbTransactionDebugProxy ? ((DbTransactionDebugProxy)value).transaction : value; }
        }

        
        public DbCommandDebugProxy(IDbCommand command)
        {
            if (command == null) throw new ArgumentNullException("command");

            this.command = command;
        }

        public void Cancel()
        {
            command.Cancel();
        }

        public IDbDataParameter CreateParameter()
        {
            return command.CreateParameter();
        }

        public int ExecuteNonQuery()
        {
            using (ActionTimer.Begin(dur => AdoDebugProfile.DebugOut(this, "ExecuteNonQuery", dur)))
            {
                return command.ExecuteNonQuery();
            }
        }

        public IDataReader ExecuteReader(CommandBehavior behavior)
        {
            using (ActionTimer.Begin(dur => AdoDebugProfile.DebugOut(this, string.Format("ExecuteReader({0})", behavior), dur)))
            {
                return command.ExecuteReader(behavior);
            }
        }

        public IDataReader ExecuteReader()
        {
            using (ActionTimer.Begin(dur => AdoDebugProfile.DebugOut(this, "ExecuteReader", dur)))
            {
                return command.ExecuteReader();
            }
        }

        public object ExecuteScalar()
        {
            using (ActionTimer.Begin(dur => AdoDebugProfile.DebugOut(this, "ExecuteScalar", dur)))
            {
                return command.ExecuteScalar();
            }
        }

        public void Prepare()
        {
            command.Prepare();
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
                    command.Dispose();
                }
                disposed = true;
            }
        }

        ~DbCommandDebugProxy()
        {
            Dispose(false);
        }
    }
}
