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
        const string sqlFilterCampers = "Sql Statement";




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
                MedicationsCSV = Convert.ToString(reader["medications"]),
                AllergiesCSV = Convert.ToString(reader["allergies"]),
                SpecialNeedsCSV = Convert.ToString(reader["special_needs"])
                 
            };

            return camper;
        }

    }
}
