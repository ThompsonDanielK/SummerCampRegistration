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

        const string sqlAllCampers = "Sql Statement";
        const string sqlFilterCampers = "Sql Statement";




        // return type needs set to a list of campers
        public void FetchAllCampers()
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();

                using (SqlCommand command = new SqlCommand(sqlAllCampers, conn))
                {
                    using (SqlDataReader Reader = command.ExecuteReader())
                    {
                        while (Reader.Read())
                        {
                            BuildCamperFromReader(Reader);
                            //Add Camper to list
                        }
                    }
                }
            }
            // return list of campers
        }

        // return type needs set to a camper
        private void BuildCamperFromReader(SqlDataReader reader)
        {
            //return new Camper
            //{
            //    Field = Convert.ToXxx(reader["tablefield]),
            //}
        }

    }
}
