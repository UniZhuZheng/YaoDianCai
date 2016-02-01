using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using System.Data.Common;
using Uni.Core.Database;

namespace Uni.YDC.Dao.Manager
{
    static class DBManager
    {
        public static string DatabaseId { get { return "YDCManagerSqlServer"; } }

        static DBManager()
        {
            try
            {
                string connString = ConfigurationManager.ConnectionStrings[DatabaseId].ConnectionString.ToString();
                string connProvider = ConfigurationManager.ConnectionStrings[DatabaseId].ProviderName.ToString();

                DbProviderFactory providerFactory = DbProviderFactories.GetFactory(connProvider);
                DbRegistry.RegisterDatabase(DatabaseId, providerFactory, connString);
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}
