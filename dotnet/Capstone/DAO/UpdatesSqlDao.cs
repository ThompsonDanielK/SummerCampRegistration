using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Capstone.Models;

namespace Capstone.DAO
{
    public class UpdatesSqlDao : IUpdatesDao
    {
        private readonly string connectionString;

        public UpdatesSqlDao(string connectionString)
        {
            if (string.IsNullOrWhiteSpace(connectionString))
                throw new ArgumentNullException(nameof(connectionString));
            this.connectionString = connectionString;
        }

        public bool ProcessApprovedRequests(string table, int requestId)
        {
            string sqlRequestById = ($"SELECT * FROM {table} WHERE request_id = @requestId");
            string sqlUpdateString = "";
            string indexField = "";
            int indexValue = 0;
            string tableToUpdate = "";

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();

                using (SqlCommand cmd = new SqlCommand(sqlRequestById, conn))
                {
                    cmd.Parameters.AddWithValue("@requestId", requestId);
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        bool firstIteration = true;
                        while (reader.Read())
                        {
                            // First time through, get our indexs & table to update
                            // Second and following iterations add a comma to seperate fields to be updated.
                            if (firstIteration)
                            {
                                if (table == "family_updates")
                                {
                                    indexField = "family_id";
                                    indexValue = Convert.ToInt32(reader["family_id"]);
                                    tableToUpdate = "family";
                                }
                                if (table == "camper_updates")
                                {
                                    indexField = "camper_code";
                                    indexValue = Convert.ToInt32(reader["camper_code"]);
                                    tableToUpdate = "campers";
                                }
                                firstIteration = false;
                            }
                            else
                            {
                                sqlUpdateString += ", ";
                            }

                            // Add column name & new value to sql string
                            sqlUpdateString += (Convert.ToString(reader["field_to_be_changed"]) + " = '" + Convert.ToString(reader["new_data"]) + "'");
                        }
                    }
                }
            }

            sqlUpdateString = $"Update {tableToUpdate} SET {sqlUpdateString} WHERE {indexField} = {indexValue}";

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand(sqlUpdateString, conn))
                    {
                        cmd.ExecuteNonQuery();
                    }
                }
                catch (SqlException ex)
                {
                    // deal with exception
                }
            }

            FinalizeRequest(table, requestId, "Approved");
            return true;
        }

        public bool UnenrollCamper(int camperCode)
        {
            string sqlUnenrollCamper = "UPDATE campers SET active = false WHERE camper_code = @camperCode";
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand(sqlUnenrollCamper, conn))
                    {
                        cmd.Parameters.AddWithValue("@camperCode", camperCode);
                        cmd.ExecuteNonQuery();
                    }
                }
                catch (SqlException ex)
                {
                    // deal with exception
                    return false;
                }
            }
            return true;
        }

        public bool FinalizeRequest(string table, int requestId, string status)
        {
            string sqlFinalizeRequest = $"UPDATE {table} SET finalize_date = @Now, status = @status WHERE request_id = @requestId";
            // Updates are done, close the request by setting the finalize date
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();

                using (SqlCommand cmd = new SqlCommand(sqlFinalizeRequest, conn))
                {
                    try
                    {
                        cmd.Parameters.AddWithValue("@now", DateTime.Now.Date);
                        cmd.Parameters.AddWithValue("@status", status);
                        cmd.Parameters.AddWithValue("@requestId", requestId);
                        cmd.ExecuteNonQuery();
                    }
                    catch (SqlException ex)
                    {
                        Console.WriteLine(ex.Message);
                        return false;
                    }
                }
            }
            return true;
        }
    }
}
