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

       
        const string sqlGetFamilyIdFromCamperCode = "Select TOP 1 family_id FROM campers WHERE camper_code = @camperCode";
        const string sqlRequestById = "SELECT * FROM updates WHERE request_id = @requestId";
        const string sqlSetFinalizeDate = "UPDATE updates SET finalize_date = @Now WHERE request_id = @requestId";
        const string sqlUnenrollCamper = "UPDATE campers SET active = false WHERE camper_code = @camperCode";
        
 

        public bool ProcessApprovedRequests(int requestId)
        {
            // Begin building update SQL Statements
            string sqlUpdateCamper = "";
            string sqlUpdateFamily = "";
            //  These need to be populated with camper/family we are working with.
            //  Need to decide if we're fetching them here or passing them in.

            int camperCode = 0;
            int familyId = 0;

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();

                using (SqlCommand cmd = new SqlCommand(sqlRequestById, conn))
                {
                    cmd.Parameters.AddWithValue("@requestId", requestId);
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        int counter = 0;
                        while (reader.Read())
                        {
                            if (counter == 0)
                            {
                                camperCode = Convert.ToInt32(reader["camper_code"]);
                                counter++;
                            } else
                            {
                                if (sqlUpdateCamper != "") sqlUpdateCamper += ", ";
                                if (sqlUpdateFamily != "") sqlUpdateFamily += ", ";
                            }
                                RequestLineItem rqi = new RequestLineItem();
                                rqi.FieldToBeChanged = Convert.ToString(reader["field_to_be_changed"]);
                                rqi.NewData = Convert.ToString(reader["new_data"]);
                                rqi.OldData = Convert.ToString(reader["old_data"]);
                                BuildUpdateStrings(rqi, ref sqlUpdateCamper, ref sqlUpdateFamily);
                            
                        }
                    }
                }
            }
            if (sqlUpdateCamper != "")
            {
                sqlUpdateCamper = "UPDATE campers SET " + sqlUpdateCamper + " WHERE camper_code = " + camperCode;
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    try
                    {
                        conn.Open();
                        using (SqlCommand cmd = new SqlCommand(sqlUpdateCamper, conn))
                        {
                            cmd.ExecuteNonQuery();
                        }
                    }
                    catch (SqlException ex)
                    {
                        // deal with exception
                    }
                }
            }
            if (sqlUpdateFamily != "")
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    try
                    {
                        conn.Open();
                        using (SqlCommand cmd = new SqlCommand(sqlGetFamilyIdFromCamperCode, conn))
                        {
                            cmd.Parameters.AddWithValue("@camperCode", camperCode);
                            familyId = Convert.ToInt32(cmd.ExecuteScalar());
                        }
                    }
                    catch
                    {

                    }
                }
                
                sqlUpdateFamily = "UPDATE family SET " + sqlUpdateFamily + " WHERE family_id = " + familyId;
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    try
                    {
                        conn.Open();
                        using (SqlCommand cmd = new SqlCommand(sqlUpdateFamily, conn))
                        {
                            cmd.ExecuteNonQuery();
                        }
                    }
                    catch (SqlException ex)
                    {
                        // deal with exception
                        return false;
                    }
                }
            }
            SetFinalizeDate(requestId);
            return true;
        }

        public bool UnenrollCamper(int camperCode)
        {
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


        public void SetFinalizeDate(int requestId)
        {
            // Updates are done, close the request by setting the finalize date
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();

                using (SqlCommand cmd = new SqlCommand(sqlSetFinalizeDate, conn))
                {
                    try
                    {
                        cmd.Parameters.AddWithValue("@now", DateTime.Now.Date);
                        cmd.Parameters.AddWithValue("@requestId", requestId);
                        cmd.ExecuteNonQuery();
                    }
                    catch (SqlException ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                }
            }
        }

        public void BuildUpdateStrings(RequestLineItem rqi, ref string sqlUpdateCamper, ref string sqlUpdateFamily)
        {

            if (rqi.FieldToBeChanged == "FirstName") sqlUpdateCamper += " first_name = '" + rqi.NewData + "'";
            if (rqi.FieldToBeChanged == "LastName") sqlUpdateCamper += " last_name = '" + rqi.NewData + "'";
            if (rqi.FieldToBeChanged == "DOB") sqlUpdateCamper += " dob = '" + rqi.NewData + "'";
            if (rqi.FieldToBeChanged == "Medications") sqlUpdateCamper += "  medications = '" + rqi.NewData + "'";
            if (rqi.FieldToBeChanged == "Allergies") sqlUpdateCamper += " allergies = '" + rqi.NewData + "'";
            if (rqi.FieldToBeChanged == "SpecialNeeds") sqlUpdateCamper += " special_needs = '" + rqi.NewData + "'";

            if (rqi.FieldToBeChanged == "FullName") sqlUpdateFamily += " parent_guardian_name = '" + rqi.NewData + "'";
            if (rqi.FieldToBeChanged == "Address") sqlUpdateFamily += " address = '" + rqi.NewData + "'";
            if (rqi.FieldToBeChanged == "City") sqlUpdateFamily += " city = '" + rqi.NewData + "'";
            if (rqi.FieldToBeChanged == "State") sqlUpdateFamily += " state = '" + rqi.NewData + "'";
            if (rqi.FieldToBeChanged == "Zip") sqlUpdateFamily += " zip = '" + rqi.NewData + "'";
            if (rqi.FieldToBeChanged == "Phone") sqlUpdateFamily += " phone = '" + rqi.NewData + "'";
            if (rqi.FieldToBeChanged == "EmailAddress") sqlUpdateFamily += " email_address = '" + rqi.NewData + "'";           
        }
    }
}
