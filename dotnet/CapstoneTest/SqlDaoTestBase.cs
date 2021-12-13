using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Text;
using System.Transactions;

namespace CapstoneTest
{
    public abstract class SqlDaoTestBase
    {
        protected string ConnectionString = @"Server=.\SQLEXPRESS;Database=final_capstone;Trusted_Connection=True;";

        private TransactionScope transaction;

        [TestInitialize]
        public void Initalize()
        {
            transaction = new TransactionScope();

            string sql = File.ReadAllText("setup.sql");

            using (SqlConnection conn = new SqlConnection(ConnectionString))
            {
                conn.Open();

                SqlCommand cmd = new SqlCommand(sql, conn);

                cmd.ExecuteNonQuery();
            }
        }

        [TestCleanup]
        public void Cleanup()
        {
            transaction?.Dispose();
        }
    }
}
