using System;
using System.Text;
using System.Data;


namespace Uni.Core.Database.DebugProxy
{
    class DbTransactionDebugProxy : IDbTransaction
    {
        private bool disposed;

        public readonly IDbTransaction transaction;
        
        public IDbConnection Connection      { get { return transaction.Connection; } }
        public IsolationLevel IsolationLevel { get { return transaction.IsolationLevel; } }


        public DbTransactionDebugProxy(IDbTransaction transaction)
        {
            if (transaction == null) throw new ArgumentNullException("transaction");

            this.transaction = transaction;
        }

        public void Commit()
        {
            using (ActionTimer.Begin(dur => AdoDebugProfile.DebugOut(this, "Commit", dur)))
            {
                transaction.Commit();
            }
        }

        public void Rollback()
        {
            using (ActionTimer.Begin(dur => AdoDebugProfile.DebugOut(this, "Rollback", dur)))
            {
                transaction.Rollback();
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
                    transaction.Dispose();
                }
                disposed = true;
            }
        }

        ~DbTransactionDebugProxy()
        {
            Dispose(false);
        }
    }
}
