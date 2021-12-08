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

        const string sqlGetLastUpdateRequestId = "SELECT TOP 1 request_id FROM updates ORDER BY request_id DESC";
        const string sqlGetFirstCamperCodeByFamilyId = "SELECT TOP 1 camper_code FROM campers WHERE family_id = @familyId";
        const string sqlAddNewUpdateRequest = "SET IDENTITY_INSERT updates ON; INSERT INTO updates " +
                    "(request_id, field_to_be_changed, camper_code, action, new_data, old_data, requestor, status, request_date) " +
                    "VALUES (@requestId, @fieldToBeChanged, @camperCode, @action, @newData, @oldData, @requestor, @status, @requestDate); SET IDENTITY_INSERT updates OFF;";
        const string sqlRequestById = "SELECT * FROM updates WHERE request_id = @requestId";
        const string sqlSetFinalizeDate = "UPDATE updates SET finalize_date = @Now WHERE request_id = @requestId";
        const string sqlUnenrollCamper = "UPDATE campers SET active = false WHERE camper_code = @camperCode";
        

        // int userId = int.Parse(this.User.FindFirst("sub").Value);

        public int GetNextUpdateRequestId()
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(sqlGetLastUpdateRequestId, conn);
                return (Convert.ToInt32(cmd.ExecuteScalar()) + 1);
            }
        }

        public int GetFirstCamperCodeByFamilyId(int familyId)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(sqlGetFirstCamperCodeByFamilyId, conn);
                return (Convert.ToInt32(cmd.ExecuteScalar()));
            }

        }

        public int AddNewCamperUpdateRequest(int userId, Camper newCamperData, Camper oldCamperData)
        {
            //Camper oldCamperData = camp.FetchCamper(newCamperData.CamperCode);
            int requestId = GetNextUpdateRequestId();
            string user = userId.ToString();

            if (requestId == 0)
            {
                requestId = 1;
            }

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();

                    using (SqlCommand cmd = new SqlCommand(sqlAddNewUpdateRequest, conn))
                    {
                        cmd.Parameters.AddWithValue("@requestId", requestId);
                        cmd.Parameters.AddWithValue("@camperCode", newCamperData.CamperCode);
                        cmd.Parameters.AddWithValue("@action", "Update");
                        cmd.Parameters.AddWithValue("@requestor", user);
                        cmd.Parameters.AddWithValue("@status", "Pending");
                        cmd.Parameters.AddWithValue("@requestDate", DateTime.Now);

                        cmd.Parameters.Add("@fieldToBeChanged", SqlDbType.NVarChar);
                        cmd.Parameters.Add("@newData", SqlDbType.NVarChar);
                        cmd.Parameters.Add("@oldData", SqlDbType.NVarChar);
                        if (oldCamperData.FirstName != newCamperData.FirstName)
                        {
                            cmd.Parameters["@fieldToBeChanged"].Value = "FirstName";
                            cmd.Parameters["@newData"].Value = newCamperData.FirstName;
                            cmd.Parameters["@oldData"].Value = oldCamperData.FirstName;
                            cmd.ExecuteNonQuery();
                        }
                        if (oldCamperData.LastName != newCamperData.LastName)
                        {
                            cmd.Parameters["@fieldToBeChanged"].Value = "LastName";
                            cmd.Parameters["@newData"].Value = newCamperData.LastName;
                            cmd.Parameters["@oldData"].Value = oldCamperData.LastName;
                            cmd.ExecuteNonQuery();
                        }
                        if (oldCamperData.DOB != newCamperData.DOB)
                        {
                            cmd.Parameters["@fieldToBeChanged"].Value = "DOB";
                            cmd.Parameters["@newData"].Value = newCamperData.DOB;
                            cmd.Parameters["@oldData"].Value = oldCamperData.DOB;
                            cmd.ExecuteNonQuery();
                        }
                        if (oldCamperData.Medications != newCamperData.Medications)
                        {
                            cmd.Parameters["@fieldToBeChanged"].Value = "Medications";
                            cmd.Parameters["@newData"].Value = newCamperData.Medications;
                            cmd.Parameters["@oldData"].Value = oldCamperData.Medications;
                            cmd.ExecuteNonQuery();
                        }
                        if (oldCamperData.SpecialNeeds != newCamperData.SpecialNeeds)
                        {
                            cmd.Parameters["@fieldToBeChanged"].Value = "SpecialNeeds";
                            cmd.Parameters["@newData"].Value = newCamperData.SpecialNeeds;
                            cmd.Parameters["@oldData"].Value = oldCamperData.SpecialNeeds;
                            cmd.ExecuteNonQuery();
                        }
                        if (oldCamperData.Allergies != newCamperData.Allergies)
                        {
                            cmd.Parameters["@fieldToBeChanged"].Value = "Allergies";
                            cmd.Parameters["@newData"].Value = newCamperData.Allergies;
                            cmd.Parameters["@oldData"].Value = oldCamperData.Allergies;
                            cmd.ExecuteNonQuery();
                        }
                    }
                }
                catch (SqlException ex)
                {
                    return -1;
                }
                return requestId;
            }
        }

        //public int AddNewFamilyUpdateRequest(int userId, Family newFamilyData)
        //{
        //    int requestId = GetNextUpdateRequestId();
        //    Family oldFamilyData = camp.FetchFamily(newFamilyData.FamilyId);
        //    int camperCode = GetFirstCamperCodeByFamilyId(newFamilyData.FamilyId);
        //    string user = userId.ToString();

        //    using (SqlConnection conn = new SqlConnection(connectionString))
        //    {
        //        try
        //        {
        //            conn.Open();

        //            using (SqlCommand cmd = new SqlCommand(sqlAddNewUpdateRequest, conn))
        //            {
        //                cmd.Parameters.AddWithValue("@requestId", requestId);
        //                cmd.Parameters.AddWithValue("@camperCode", camperCode);
        //                cmd.Parameters.AddWithValue("@action", "Update");
        //                cmd.Parameters.AddWithValue("@requestor", user);
        //                cmd.Parameters.AddWithValue("@status", "Pending");
        //                cmd.Parameters.AddWithValue("@requestDate", DateTime.Now);

        //                cmd.Parameters.Add("@fieldToBeChanged", SqlDbType.NVarChar);
        //                cmd.Parameters.Add("@newData", SqlDbType.NVarChar);
        //                cmd.Parameters.Add("@oldData", SqlDbType.NVarChar);
        //                if (oldFamilyData.FullName != newFamilyData.FullName)
        //                {
        //                    cmd.Parameters["@fieldToBeChanged"].Value = "FullName";
        //                    cmd.Parameters["@newData"].Value = newFamilyData.FullName;
        //                    cmd.Parameters["@oldData"].Value = oldFamilyData.FullName;
        //                    cmd.ExecuteNonQuery();
        //                }
        //                if (oldFamilyData.Address != newFamilyData.Address)
        //                {
        //                    cmd.Parameters["@fieldToBeChanged"].Value = "Address";
        //                    cmd.Parameters["@newData"].Value = newFamilyData.Address;
        //                    cmd.Parameters["@oldData"].Value = oldFamilyData.Address;
        //                    cmd.ExecuteNonQuery();
        //                }
        //                if (oldFamilyData.City != newFamilyData.City)
        //                {
        //                    cmd.Parameters["@fieldToBeChanged"].Value = "City";
        //                    cmd.Parameters["@newData"].Value = newFamilyData.City;
        //                    cmd.Parameters["@oldData"].Value = oldFamilyData.City;
        //                    cmd.ExecuteNonQuery();
        //                }
        //                if (oldFamilyData.State != newFamilyData.State)
        //                {
        //                    cmd.Parameters["@fieldToBeChanged"].Value = "State";
        //                    cmd.Parameters["@newData"].Value = newFamilyData.State;
        //                    cmd.Parameters["@oldData"].Value = oldFamilyData.State;
        //                    cmd.ExecuteNonQuery();
        //                }
        //                if (oldFamilyData.Zip != newFamilyData.Zip)
        //                {
        //                    cmd.Parameters["@fieldToBeChanged"].Value = "Zip";
        //                    cmd.Parameters["@newData"].Value = newFamilyData.Zip;
        //                    cmd.Parameters["@oldData"].Value = oldFamilyData.Zip;
        //                    cmd.ExecuteNonQuery();
        //                }
        //                if (oldFamilyData.PhoneNumber != newFamilyData.PhoneNumber)
        //                {
        //                    cmd.Parameters["@fieldToBeChanged"].Value = "PhoneNumber";
        //                    cmd.Parameters["@newData"].Value = newFamilyData.PhoneNumber;
        //                    cmd.Parameters["@oldData"].Value = oldFamilyData.PhoneNumber;
        //                    cmd.ExecuteNonQuery();
        //                }
        //                if (oldFamilyData.EmailAddress != newFamilyData.EmailAddress)
        //                {
        //                    cmd.Parameters["@fieldToBeChanged"].Value = "EmailAddress";
        //                    cmd.Parameters["@newData"].Value = newFamilyData.EmailAddress;
        //                    cmd.Parameters["@oldData"].Value = oldFamilyData.EmailAddress;
        //                    cmd.ExecuteNonQuery();
        //                }
        //            }
        //        }
        //        catch (SqlException ex)
        //        {
        //            return -1;
        //        }
        //        return requestId;
        //    }
        //}

        public bool ProcessApprovedRequests(int requestId)
        {
            // Begin building update SQL Statements
            string sqlUpdateCamper = "";
            string sqlUpdateFamily = "";
            //  These need to be populated with camper/family we are working with.
            //  Need to decide if we're fetching them here or passing them in.

            Camper camper = new Camper();
            Family family = new Family();

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();

                using (SqlCommand cmd = new SqlCommand(sqlRequestById, conn))
                {
                    cmd.Parameters.AddWithValue("@requestId", requestId);
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                                   
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
                sqlUpdateCamper = "UPDATE campers SET " + sqlUpdateCamper + " WHERE camper_code = " + camper.CamperCode;
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
            if (sqlUpdateCamper != "")
            {
                sqlUpdateFamily = "UPDATE family SET " + sqlUpdateFamily + " WHERE family_id = " + family.FamilyId;
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
                        return false;
                    }
                }
            }
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
                    cmd.Parameters.AddWithValue("@now", DateTime.Now);
                    cmd.Parameters.AddWithValue("@request_id", requestId);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void BuildUpdateStrings(RequestLineItem rqi, ref string sqlUpdateCamper, ref string sqlUpdateFamily)
        {
            if (rqi.FieldToBeChanged == "FirstName") sqlUpdateCamper += " first_name = " + rqi.NewData;
            if (rqi.FieldToBeChanged == "LastName") sqlUpdateCamper += " last_name = " + rqi.NewData;
            if (rqi.FieldToBeChanged == "DOB") sqlUpdateCamper += " dob = " + rqi.NewData;
            if (rqi.FieldToBeChanged == "Medications") sqlUpdateCamper += "  medications = " + rqi.NewData;
            if (rqi.FieldToBeChanged == "Allergies") sqlUpdateCamper += " allergies = " + rqi.NewData;
            if (rqi.FieldToBeChanged == "SpecialNeeds") sqlUpdateCamper += " special_needs = " + rqi.NewData;

            if (rqi.FieldToBeChanged == "FullName") sqlUpdateFamily += " parent_guardian_name = " + rqi.NewData;
            if (rqi.FieldToBeChanged == "Address") sqlUpdateFamily += " address = " + rqi.NewData;
            if (rqi.FieldToBeChanged == "City") sqlUpdateFamily += " city = " + rqi.NewData;
            if (rqi.FieldToBeChanged == "State") sqlUpdateFamily += " state = " + rqi.NewData;
            if (rqi.FieldToBeChanged == "Zip") sqlUpdateFamily += " zip = " + rqi.NewData;
            if (rqi.FieldToBeChanged == "Phone") sqlUpdateFamily += " phone = " + rqi.NewData;
            if (rqi.FieldToBeChanged == "EmailAddress") sqlUpdateFamily += " email_address = " + rqi.NewData;
        }
    }
}
