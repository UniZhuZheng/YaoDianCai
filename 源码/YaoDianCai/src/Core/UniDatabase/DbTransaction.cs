using System;
using System.Text;
using System.Data;


namespace Uni.Core.Database
{
    public class DbTransaction : IDbTransaction
    {
        internal IDbTransaction Transaction { get; private set; }

        public IDbConnection  Connection     { get { return Transaction.Connection; } }
        public IsolationLevel IsolationLevel { get { return Transaction.IsolationLevel; } }

        public event EventHandler Unavailable;


        public DbTransaction(IDbTransaction transaction)
        {
            if (transaction == null) throw new ArgumentNullException("transaction");

            Transaction = transaction;
        }   

        public void Commit()
        {
            try
            {
                Transaction.Commit();
            }
            finally
            {
                OnUnavailable();
            }
        }

        public void Rollback()
        {
            try
            {
                Transaction.Rollback();
            }
            finally
            {
                OnUnavailable();
            }
        }

        public void Dispose()
        {
            try
            {
                Transaction.Dispose();
            }
            finally
            {
                OnUnavailable();
            }
        }

        private void OnUnavailable()
        {
            try
            {
                if (Unavailable != null)
                {
                    Unavailable(this, EventArgs.Empty);
                }
            }
            catch
            {
            }
        }
    }
}
