using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Capstone.Models;

namespace Capstone.DAO
{
    public class RequestSqlDao : IRequestDao
    {
        private readonly string connectionString;

        public RequestSqlDao(string connectionString)
        {
            if (string.IsNullOrWhiteSpace(connectionString))
                throw new ArgumentNullException(nameof(connectionString));
            this.connectionString = connectionString;
        }

        const string sqlGetLastUpdateRequestId = "SELECT TOP 1 request_id FROM updates ORDER BY request_id DESC";
        const string sqlGetFirstCamperCodeByFamilyId = "SELECT TOP 1 camper_code FROM campers WHERE family_id = @familyId";
        const string sqlAddNewUpdateRequest = "INSERT INTO updates " +
                    "(request_id, field_to_be_changed, camper_code, action, new_data, old_data, requestor, status, request_date) " +
                    "VALUES (@requestId, @fieldToBeChanged, @camperCode, @action, @newData, @oldData, @requestor, @status, @requestDate);";
        const string sqlGetCamperUpdateList = "SELECT request_id, field_to_be_changed, camper_code, action, " +
            "new_data, old_data, requestor, status, request_date, finalize_date FROM camper_updates";
        const string sqlGetFamilyUpdateList = "SELECT request_id, field_to_be_changed, family_id, action," +
            " new_data, old_data, requestor, status, request_date, finalize_date FROM family_updates";

        public List<Update> GetCamperUpdateList(bool isCamperUpdate)
        {
            List<Update> updateList = new List<Update>();

            string sqlStatement = sqlGetCamperUpdateList;

            if(!isCamperUpdate)
            {
                sqlStatement = sqlGetFamilyUpdateList;
            }

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();

                using (SqlCommand cmd = new SqlCommand(sqlStatement, conn))
                {
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {

                            Update update = BuildUpdate(reader);

                            var finalizeDate = reader["finalize_date"];

                            if (isCamperUpdate)
                            {
                                update.CamperCode = Convert.ToInt32(reader["camper_code"]);
                            }

                            if (!finalizeDate.Equals(null))
                            {
                                update.FinalizeDate = Convert.ToDateTime(finalizeDate);
                            }

                            updateList.Add(update);
                        }
                    }
                }
            }

            return updateList;
        }

        private Update BuildUpdate(SqlDataReader reader)
        {
            return new Update()
            {
                RequestId = Convert.ToInt32(reader["request_id"]),
                FieldToBeChanged = Convert.ToString(reader["field_to_be_changed"]),
                FamilyId = Convert.ToInt32(reader["family_id"]),
                Action = Convert.ToString(reader["action"]),
                NewData = Convert.ToString(reader["new_data"]),
                OldData = Convert.ToString(reader["old_data"]),
                Requestor = Convert.ToString(reader["requestor"]),
                Status = Convert.ToString(reader["status"]),
                RequestDate = Convert.ToDateTime(reader["request_date"]),
            };
        }

        public int GetNextUpdateRequestId()
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(sqlGetLastUpdateRequestId, conn);
                int nextId = (Convert.ToInt32(cmd.ExecuteScalar()) + 1);
                if (nextId == 1) nextId = 300001; // This will be the first update record.
                return nextId;
            }
        }

        public int GetFirstCamperCodeByFamilyId(int familyId)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(sqlGetFirstCamperCodeByFamilyId, conn);
                cmd.Parameters.AddWithValue("@familyId", familyId);
                return (Convert.ToInt32(cmd.ExecuteScalar()));
            }
        }

        public int AddNewCamperUpdateRequest(int userId, Camper newCamperData, Camper currentCamperData)
        {
            int requestId = GetNextUpdateRequestId();
            string user = userId.ToString();

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
                        cmd.Parameters.AddWithValue("@requestDate", DateTime.Now.Date);

                        cmd.Parameters.Add("@fieldToBeChanged", SqlDbType.NVarChar);
                        cmd.Parameters.Add("@newData", SqlDbType.NVarChar);
                        cmd.Parameters.Add("@oldData", SqlDbType.NVarChar);
                        if (currentCamperData.FirstName != newCamperData.FirstName)
                        {
                            cmd.Parameters["@fieldToBeChanged"].Value = "FirstName";
                            cmd.Parameters["@newData"].Value = newCamperData.FirstName;
                            cmd.Parameters["@oldData"].Value = currentCamperData.FirstName;
                            cmd.ExecuteNonQuery();
                        }
                        if (currentCamperData.LastName != newCamperData.LastName)
                        {
                            cmd.Parameters["@fieldToBeChanged"].Value = "LastName";
                            cmd.Parameters["@newData"].Value = newCamperData.LastName;
                            cmd.Parameters["@oldData"].Value = currentCamperData.LastName;
                            cmd.ExecuteNonQuery();
                        }
                        if (currentCamperData.DOB != newCamperData.DOB)
                        {
                            cmd.Parameters["@fieldToBeChanged"].Value = "DOB";
                            cmd.Parameters["@newData"].Value = newCamperData.DOB;
                            cmd.Parameters["@oldData"].Value = currentCamperData.DOB;
                            cmd.ExecuteNonQuery();
                        }
                        if (currentCamperData.Medications != newCamperData.Medications)
                        {
                            cmd.Parameters["@fieldToBeChanged"].Value = "Medications";
                            cmd.Parameters["@newData"].Value = newCamperData.Medications;
                            cmd.Parameters["@oldData"].Value = currentCamperData.Medications;
                            cmd.ExecuteNonQuery();
                        }
                        if (currentCamperData.SpecialNeeds != newCamperData.SpecialNeeds)
                        {
                            cmd.Parameters["@fieldToBeChanged"].Value = "SpecialNeeds";
                            cmd.Parameters["@newData"].Value = newCamperData.SpecialNeeds;
                            cmd.Parameters["@oldData"].Value = currentCamperData.SpecialNeeds;
                            cmd.ExecuteNonQuery();
                        }
                        if (currentCamperData.Allergies != newCamperData.Allergies)
                        {
                            cmd.Parameters["@fieldToBeChanged"].Value = "Allergies";
                            cmd.Parameters["@newData"].Value = newCamperData.Allergies;
                            cmd.Parameters["@oldData"].Value = currentCamperData.Allergies;
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

        public int AddNewFamilyUpdateRequest(int userId, Family newFamilyData, Family currentFamilyData)
        {
            int requestId = GetNextUpdateRequestId();
            string user = userId.ToString();
            int camperCode = GetFirstCamperCodeByFamilyId(newFamilyData.FamilyId);


            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();

                    using (SqlCommand cmd = new SqlCommand(sqlAddNewUpdateRequest, conn))
                    {
                        cmd.Parameters.AddWithValue("@requestId", requestId);
                        cmd.Parameters.AddWithValue("@camperCode", camperCode);
                        cmd.Parameters.AddWithValue("@action", "Update");
                        cmd.Parameters.AddWithValue("@requestor", user);
                        cmd.Parameters.AddWithValue("@status", "Pending");
                        cmd.Parameters.AddWithValue("@requestDate", DateTime.Now.Date);

                        cmd.Parameters.Add("@fieldToBeChanged", SqlDbType.NVarChar);
                        cmd.Parameters.Add("@newData", SqlDbType.NVarChar);
                        cmd.Parameters.Add("@oldData", SqlDbType.NVarChar);
                        if (currentFamilyData.FullName != newFamilyData.FullName)
                        {
                            cmd.Parameters["@fieldToBeChanged"].Value = "FullName";
                            cmd.Parameters["@newData"].Value = newFamilyData.FullName;
                            cmd.Parameters["@oldData"].Value = currentFamilyData.FullName;
                            cmd.ExecuteNonQuery();
                        }
                        if (currentFamilyData.Address != newFamilyData.Address)
                        {
                            cmd.Parameters["@fieldToBeChanged"].Value = "Address";
                            cmd.Parameters["@newData"].Value = newFamilyData.Address;
                            cmd.Parameters["@oldData"].Value = currentFamilyData.Address;
                            cmd.ExecuteNonQuery();
                        }
                        if (currentFamilyData.City != newFamilyData.City)
                        {
                            cmd.Parameters["@fieldToBeChanged"].Value = "City";
                            cmd.Parameters["@newData"].Value = newFamilyData.City;
                            cmd.Parameters["@oldData"].Value = currentFamilyData.City;
                            cmd.ExecuteNonQuery();
                        }
                        if (currentFamilyData.State != newFamilyData.State)
                        {
                            cmd.Parameters["@fieldToBeChanged"].Value = "State";
                            cmd.Parameters["@newData"].Value = newFamilyData.State;
                            cmd.Parameters["@oldData"].Value = currentFamilyData.State;
                            cmd.ExecuteNonQuery();
                        }
                        if (currentFamilyData.Zip != newFamilyData.Zip)
                        {
                            cmd.Parameters["@fieldToBeChanged"].Value = "Zip";
                            cmd.Parameters["@newData"].Value = newFamilyData.Zip;
                            cmd.Parameters["@oldData"].Value = currentFamilyData.Zip;
                            cmd.ExecuteNonQuery();
                        }
                        if (currentFamilyData.PhoneNumber != newFamilyData.PhoneNumber)
                        {
                            cmd.Parameters["@fieldToBeChanged"].Value = "Phone";
                            cmd.Parameters["@newData"].Value = newFamilyData.PhoneNumber;
                            cmd.Parameters["@oldData"].Value = currentFamilyData.PhoneNumber;
                            cmd.ExecuteNonQuery();
                        }
                        if (currentFamilyData.EmailAddress != newFamilyData.EmailAddress)
                        {
                            cmd.Parameters["@fieldToBeChanged"].Value = "EmailAddress";
                            cmd.Parameters["@newData"].Value = newFamilyData.EmailAddress;
                            cmd.Parameters["@oldData"].Value = currentFamilyData.EmailAddress;
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


    }
}
