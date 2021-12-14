using Capstone.DAO;
using Capstone.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace CapstoneTest
{
    [TestClass]
    public class UpdatesSqlDaoTest
    {
        private const string connectionString = "Server=.\\SQLEXPRESS;Database=final_capstone;Trusted_Connection=True;";

        [TestMethod]
        [DataRow("camper_updates", 400001, 200003, "Bowersmith")]
        public void ProcessApprovedRequests_MakesChanges(string table, int requestId, int camperOrFamilyId, string expected)
        {
            //Arrange
            UpdatesSqlDao ops = new UpdatesSqlDao(connectionString);
            CampSqlDao secretOps = new CampSqlDao(connectionString);

            //Act
            bool result = ops.ProcessApprovedRequests(table, requestId);
            Camper verfication = secretOps.FetchCamper(camperOrFamilyId);

            //Assert
            Assert.AreEqual(expected, verfication.LastName);
        }
    }
}
