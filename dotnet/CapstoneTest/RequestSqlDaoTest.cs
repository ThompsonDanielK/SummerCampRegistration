using Capstone.DAO;
using Capstone.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace CapstoneTest
{
    [TestClass]
    public class RequestSqlDaoTest : SqlDaoTestBase
    {
        private const string connectionString = "Server=.\\SQLEXPRESS;Database=final_capstone;Trusted_Connection=True;";

        [TestMethod]
        public void GetCamperUpdateList_ReturnsCamperUpdateList()
        {
            //Arrange
            RequestSqlDao ops = new RequestSqlDao(connectionString);

            //Act
            List<Update> result = ops.GetCamperUpdateList(true);

            //Assert
            foreach (Update update in result)
            {
                Assert.IsNotNull(update.CamperCode);
            }
        }

        [TestMethod]
        public void GetCamperUpdateList_ReturnsFamilyUpdateList()
        {
            //Arrange
            RequestSqlDao ops = new RequestSqlDao(connectionString);

            //Act
            List<Update> result = ops.GetCamperUpdateList(false);

            //Assert
            foreach (Update update in result)
            {
                Assert.AreEqual(0, update.CamperCode);
            }
        }

        [TestMethod]
        [DataRow(200003)]
        [DataRow(200005)]
        [DataRow(200006)]
        public void GetCamperUpdatesByCamperCode_ReturnsListOfUpdatesFromSingleCamper(int camperCode)
        {
            //Arrange
            RequestSqlDao ops = new RequestSqlDao(connectionString);

            //Act
            List<Update> result = ops.GetCamperUpdatesByCamperCode(camperCode);

            //Assert
            foreach (Update update in result)
            {
                Assert.AreEqual(camperCode, update.CamperCode);
            }

        }

        [TestMethod]
        [DataRow(100002)]
        [DataRow(100004)]
        public void GetFamilyUpdatesByFamilyId_ReturnsListOfUpdatesFromSingleFamily(int familyId)
        {
            //Arrange
            RequestSqlDao ops = new RequestSqlDao(connectionString);

            //Act
            List<Update> result = ops.GetFamilyUpdatesByFamilyId(familyId);

            //Assert
            foreach (Update update in result)
            {
                Assert.AreEqual(familyId, update.FamilyId);
            }

        }

        [DataTestMethod]
        [DynamicData(nameof(AddNewCamperUpdateRequestTestData), DynamicDataSourceType.Method)]
        public void AddNewCamperUpdateRequest_ReturnsRequestID(Update data)
        {
            //Arrange
            RequestSqlDao ops = new RequestSqlDao(connectionString);

            //Act
            int result = ops.AddNewCamperUpdateRequest(data);

            //Assert
            Assert.IsNotNull(result);
        }

        public static IEnumerable<Object[]> AddNewCamperUpdateRequestTestData()
        {
            return new object[][]
            {
                new object []
                {
                    new Update()
                    {
                        OldData = "Brandi",
                        NewData = "Brandy",
                        CamperCode = 200002,
                        Action = "Update",
                        Requestor = "admin",
                        FieldToBeChanged = "first_name"

                    }
                }

            };
        }
    }
}
