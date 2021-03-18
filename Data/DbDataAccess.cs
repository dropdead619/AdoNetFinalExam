using Services;
using System;
using System.Collections.Generic;
using System.Data.Common;

namespace Data
{
    public abstract class DbDataAccess<T> : IDisposable
    {
        protected readonly DbProviderFactory factory;
        protected readonly DbConnection sqlConnection;
        public DbDataAccess()
        {
            factory = DbProviderFactories.GetFactory("System.Data.SqlClient");
            sqlConnection = factory.CreateConnection();
            sqlConnection.ConnectionString = ConfigurationService.Configuration["dataAccessConnectionString"];
            sqlConnection.Open();
        }

        public void Dispose()
        {
            sqlConnection.Close();
        }
        public abstract void Insert(T entity);
        public abstract void Update(T entity);
        public abstract void Delete(T entity);
        public abstract ICollection<T> Select();

        public void ExecuteTransaction(params DbCommand[] commands)
        {
            using (var transaction = sqlConnection.BeginTransaction())
            {
                try
                {
                    foreach (var command in commands)
                    {
                        command.Transaction = transaction;
                        command.ExecuteNonQuery();
                    }
                    transaction.Commit();
                }
                catch (DbException)
                {
                    transaction.Rollback();
                }
            }
        }
    }
}
