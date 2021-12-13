using Capstone.DAO;
using Capstone.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace CapstoneTest
{
    [TestClass]
    public class CampSqlDaoTest : SqlDaoTestBase
    {

        private const string connectionString = "Server=.\\SQLEXPRESS;Database=final_capstone;Trusted_Connection=True;";

        [TestMethod]
        [DataRow("Nigel Thornberry", "Pheonix", "AZ", 55555, "1234 FakeAddress Lane", "419-867-5309", null, 100005)]
        public void AddFamily_ReturnsCorrectFamilyID(string familyName, string city, string state, int zip, string address, string phoneNumber, string emailAddress, int expected)
        {
            //Arrange
            CampSqlDao ops = new CampSqlDao(connectionString);

            Family family = new Family()
            {
                FullName = familyName,
                City = city,
                State = state,
                Zip = zip,
                Address = address,
                PhoneNumber = phoneNumber,
                EmailAddress = emailAddress
            };

            //Act
            int result = ops.AddFamily(family);

            //Assert
            Assert.AreEqual(expected, result);
        }
    }
}
