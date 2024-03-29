﻿using Capstone.DAO;
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
        [DataRow("Nigel Thornberry", "Pheonix", "AZ", 55555, "1234 FakeAddress Lane", "419-867-5309", null)]
        [DataRow("Bruce Dwayne", "Detriot", "MI", 12345, "4321 MyStreet Lane", null, "dwaynetrain@gmail.com")]
        public void AddFamily_ReturnsFamilyID(string familyName, string city, string state, 
            int zip, string address, string phoneNumber, string emailAddress)
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
            Assert.IsNotNull(result);
        }

        [TestMethod]
        [DataRow(100004, "Dan", "Thompson", "1950-01-01", "user", false, true, "", "bees", "")]
        [DataRow(100001, "Eric", "Rausch", "2001-01-01", "admin", true, true, "Apple Butter, Tylenol", "Bees, Tomfoolery", "Likes his PB&Js cut diagonally.")]
        public void AddCamper_ReturnsCamperCode(int familyId, string firstName, string lastName,
            string dob, string registrar, bool paymentStatus, bool activeStatus, 
            string medications, string allergies, string specialNeeds)
        {
            //Arrange
            CampSqlDao ops = new CampSqlDao(connectionString);

            Camper camper = new Camper()
            {
                FamilyId = familyId,
                FirstName = firstName,
                LastName = lastName,
                DOB = Convert.ToDateTime(dob),
                Registrar = registrar,
                PaymentStatus = paymentStatus,
                ActiveStatus = activeStatus,
                Medications = medications,
                Allergies = allergies,
                SpecialNeeds = specialNeeds

            };

            //Act
            int result = ops.AddCamper(camper);

            //Assert
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void FetchAllFamilies_ReturnsListOfFamilies()
        {
            //Arrange
            CampSqlDao ops = new CampSqlDao(connectionString);

            //Act
            List<Family> result = ops.FetchAllFamilies();

            //Assert
            CollectionAssert.AllItemsAreNotNull(result);
        }

        [TestMethod]
        public void FetchAllCampers_ReturnsListOfCampers()
        {
            //Arrange
            CampSqlDao ops = new CampSqlDao(connectionString);

            //Act
            List<Camper> result = ops.FetchAllCampers();

            //Assert
            CollectionAssert.AllItemsAreNotNull(result);
        }

        [TestMethod]
        [DataRow(100001, "Mary Andrews")]
        [DataRow(100002, "Mike Bowers")]
        [DataRow(100003, "Molly Carter")]
        [DataRow(100004, "Antony Gonzales")]
        public void FetchFamily_ReturnsCorrectFamily(int familyId, string expected)
        {
            //Arrange
            CampSqlDao ops = new CampSqlDao(connectionString);

            //Act
            Family family = ops.FetchFamily(familyId);

            //Assert
            Assert.AreEqual(expected, family.FullName);

        }

        [TestMethod]
        [DataRow(200001, "Andrews")]
        [DataRow(200002, "Andrews")]
        [DataRow(200003, "Bowers")]
        [DataRow(200004, "Carter")]
        [DataRow(200005, "Carter")]
        [DataRow(200006, "Gonzales")]
        public void FetchCamper_ReturnsCorrectCamper(int camperCode, string expected)
        {
            //Arrange
            CampSqlDao ops = new CampSqlDao(connectionString);

            //Act
            Camper camper = ops.FetchCamper(camperCode);

            //Assert
            Assert.AreEqual(expected, camper.LastName);
        }
    }
}
