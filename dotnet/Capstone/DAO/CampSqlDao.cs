using Capstone.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace Capstone.DAO
{
    public class CampSqlDao : ICampDao
    {

        private readonly string connectionString;

        public CampSqlDao(string connectionString)
        {
            if (string.IsNullOrWhiteSpace(connectionString))
                throw new ArgumentNullException(nameof(connectionString));
            this.connectionString = connectionString;
        }

        const string sqlAllCampers = "SELECT camper_code, family_id, " +
            "first_name, last_name, dob, " +
            "medications, allergies, special_needs FROM campers";
        const string sqlAllFamilies = "SELECT family_id, " +
            "parent_guardian_name, address, city, " +
            "state, zip, phone FROM family";
        const string sqlFamily = "SELECT family_id, " +
            "parent_guardian_name, address, city, " +
            "state, zip, phone FROM family WHERE family_id = @family_id";
        const string sqlCamper = "SELECT camper_code, family_id, " +
            "first_name, last_name, dob, " +
            "medications, allergies, special_needs FROM campers WHERE camper_code = @camper_code";
        const string sqlAddCamper = "INSERT INTO campers " +
            "(family_id, first_name, last_name, dob, medications, allergies, special_needs) " +
            "VALUES (@familyId, @firstName, @lastName, @dob, @medications, " +
            "@allergies, @specialNeeds)";
        const string sqlAddFamily = "INSERT INTO family " +
            "(parent_guardian_name, address, email_address, city, state, zip, phone) " +
            "VALUES " +
            "(@parentGuardianName, @address, @emailAddress, @city, @state, @zip, @phoneNumber); SELECT @@IDENTITY";
        const string sqlFilterCampers = "Sql Statement";

        public int AddFamily(Family family)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();

                using (SqlCommand command = new SqlCommand(sqlAddFamily, conn))
                {
                    command.Parameters.AddWithValue("@parentGuardianName", family.FullName);
                    command.Parameters.AddWithValue("@address", family.Address);
                    command.Parameters.AddWithValue("@emailAddress", family.EmailAddress);
                    command.Parameters.AddWithValue("@city", family.City);
                    command.Parameters.AddWithValue("@state", family.State);
                    command.Parameters.AddWithValue("@zip", family.Zip);
                    command.Parameters.AddWithValue("@phoneNumber", family.PhoneNumber);

                    int familyId = Convert.ToInt32(command.ExecuteScalar());

                    return familyId;
                }
            }
        }

        public int AddCamper(Camper camper)
        {
            int camperCode = 0;

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();

                using (SqlCommand command = new SqlCommand(sqlAddCamper, conn))
                {
                    command.Parameters.AddWithValue("@familyId", camper.FamilyId);
                    command.Parameters.AddWithValue("@firstName", camper.FirstName);
                    command.Parameters.AddWithValue("@lastName", camper.LastName);
                    command.Parameters.AddWithValue("@dob", camper.DOB);
                    command.Parameters.AddWithValue("@medications", camper.Medications);
                    command.Parameters.AddWithValue("@allergies", camper.Allergies);
                    command.Parameters.AddWithValue("@specialNeeds", camper.SpecialNeeds);

                    camperCode = Convert.ToInt32(command.ExecuteNonQuery());             
                }
            }

            return camperCode;
        }

        public List<Camper> FetchAllCampers()
        {
            List<Camper> camperList = new List<Camper>();

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();

                using (SqlCommand command = new SqlCommand(sqlAllCampers, conn))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            camperList.Add(BuildCamperFromReader(reader));
                            //Add Camper to list
                        }
                    }
                }
            }
            // return list of campers
            return camperList;
        }

        public List<Family> FetchAllFamilies()
        {
            List<Family> familyList = new List<Family>();

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();

                using (SqlCommand command = new SqlCommand(sqlAllFamilies, conn))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            //Add Family to list
                            familyList.Add(BuildFamilyFromReader(reader));
                        }
                    }
                }
            }
            // return list of families
            return familyList;
        }

        public Family FetchFamily(int familyId)
        {
            Family family = new Family();

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();

                using (SqlCommand command = new SqlCommand(sqlFamily, conn))
                {
                    command.Parameters.AddWithValue("@family_id", familyId);
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            family = (BuildFamilyFromReader(reader));
                        }
                    }
                }
            }
            return family;
        }

        private Family BuildFamilyFromReader(SqlDataReader reader)
        {
            Family family = new Family()
            {
                FamilyId = Convert.ToInt32(reader["family_id"]),
                FullName = Convert.ToString(reader["parent_guardian_name"]),
                Address = Convert.ToString(reader["address"]),
                City = Convert.ToString(reader["city"]),
                State = Convert.ToString(reader["state"]),
                Zip = Convert.ToInt32(reader["zip"]),
                PhoneNumber = Convert.ToString(reader["phone"]),
            };
            return family;
        }

        public Camper FetchCamper(int camperCode)
        {
            Camper camper = new Camper();
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();

                using (SqlCommand command = new SqlCommand(sqlCamper, conn))
                {
                    command.Parameters.AddWithValue("@camper_code", camperCode);
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            camper = BuildCamperFromReader(reader);
                        }
                    }
                    return camper;
                }
            }
        }

        // return type needs set to a camper
        private Camper BuildCamperFromReader(SqlDataReader reader)
        {
            //return new Camper
            Camper camper = new Camper
            {
                CamperCode = Convert.ToInt32(reader["camper_code"]),
                FamilyId = Convert.ToInt32(reader["family_id"]),
                FirstName = Convert.ToString(reader["first_name"]),
                LastName = Convert.ToString(reader["last_name"]),
                DOB = Convert.ToDateTime(reader["dob"]),
                Medications = Convert.ToString(reader["medications"]),
                Allergies = Convert.ToString(reader["allergies"]),
                SpecialNeeds = Convert.ToString(reader["special_needs"])

            };

            return camper;
        }

    }
}
