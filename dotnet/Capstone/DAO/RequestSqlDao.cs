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

        const string sqlGetLastFamilyUpdateRequestId = "SELECT TOP 1 request_id FROM family_updates ORDER BY request_id DESC";
        const string sqlGetLastCamperUpdateRequestId = "SELECT TOP 1 request_id FROM camper_updates ORDER BY request_id DESC";
        const string sqlAddNewCamperUpdateRequest = "INSERT INTO camper_updates " +
                    "(request_id, field_to_be_changed, camper_code, action, new_data, old_data, requestor, status, request_date) " +
                    "VALUES (@requestId, @fieldToBeChanged, @camperCode, @action, @newData, @oldData, @requestor, @status, @requestDate);";
        const string sqlAddNewFamilyUpdateRequest = "INSERT INTO family_updates " +
                    "(request_id, field_to_be_changed, family_id, action, new_data, old_data, requestor, status, request_date) " +
                    "VALUES (@requestId, @fieldToBeChanged, @familyId, @action, @newData, @oldData, @requestor, @status, @requestDate);";
        const string sqlGetCamperUpdateList = "SELECT request_id, field_to_be_changed, camper_code, action, " +
            "new_data, old_data, requestor, status, request_date, finalize_date FROM camper_updates";
        const string sqlGetFamilyUpdateList = "SELECT request_id, field_to_be_changed, family_id, action," +
            " new_data, old_data, requestor, status, request_date, finalize_date FROM family_updates";
        const string sqlGetUpdatesByCamperCode = "SELECT request_id, field_to_be_changed, camper_code, action, " +
            "new_data, old_data, requestor, status, request_date, finalize_date FROM camper_updates WHERE camper_code = @camper_code AND status = 'Pending'";

        public List<Update> GetCamperUpdateList(bool isCamperUpdate)
        {
            List<Update> updateList = new List<Update>();

            string sqlStatement = sqlGetCamperUpdateList;

            if (!isCamperUpdate)
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

                            Update update = BuildUpdate(reader, isCamperUpdate);

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

        public List<Update> GetCamperUpdatesByCamperCode(int camperCode)
        {
            List<Update> updateList = new List<Update>();
            using SqlConnection conn = new SqlConnection(connectionString);
            conn.Open();
            using SqlCommand cmd = new SqlCommand(sqlGetUpdatesByCamperCode, conn);
            cmd.Parameters.AddWithValue("@camper_code", camperCode);
            using SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                Update update = BuildUpdate(reader, true);
                if (reader["finalize_date"] != DBNull.Value)
                {
                    update.FinalizeDate = Convert.ToDateTime(reader["finalize_date"]);
                }
                updateList.Add(update);
            }
            return updateList;

        }

        private Update BuildUpdate(SqlDataReader reader, bool isCamper)
        {
            Update update = new Update()
            {
                RequestId = Convert.ToInt32(reader["request_id"]),
                FieldToBeChanged = Convert.ToString(reader["field_to_be_changed"]),
                Action = Convert.ToString(reader["action"]),
                NewData = Convert.ToString(reader["new_data"]),
                Requestor = Convert.ToString(reader["requestor"]),
                Status = Convert.ToString(reader["status"]),
                RequestDate = Convert.ToDateTime(reader["request_date"]),
            };
            if (reader["old_data"] != DBNull.Value)
            {
                update.OldData = Convert.ToString(reader["old_data"]);
            }
            else 
            {
                update.OldData = null;
            }
            if (isCamper)
            {
                update.CamperCode = Convert.ToInt32(reader["camper_code"]);
            }
            else 
            {
                update.FamilyId = Convert.ToInt32(reader["family_id"]);
            }
            return update;
        }

        public int GetNextFamilyUpdateRequestId()
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(sqlGetLastFamilyUpdateRequestId, conn);
                int nextId = (Convert.ToInt32(cmd.ExecuteScalar()) + 1);
                if (nextId == 1) nextId = 300001; // This will be the first update record.
                return nextId;
            }
        }
        public int GetNextCamperUpdateRequestId()
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(sqlGetLastCamperUpdateRequestId, conn);
                int nextId = (Convert.ToInt32(cmd.ExecuteScalar()) + 1);
                if (nextId == 1) nextId = 400001; // This will be the first update record.
                return nextId;
            }
        }

        public int AddNewCamperUpdateRequest(int userId, Camper newCamperData, Camper currentCamperData)
        {
            int requestId = GetNextCamperUpdateRequestId();
            string user = userId.ToString();

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();

                    using (SqlCommand cmd = new SqlCommand(sqlAddNewCamperUpdateRequest, conn))
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
                            cmd.Parameters["@fieldToBeChanged"].Value = "first_name";
                            cmd.Parameters["@newData"].Value = newCamperData.FirstName;
                            cmd.Parameters["@oldData"].Value = currentCamperData.FirstName;
                            cmd.ExecuteNonQuery();
                        }
                        if (currentCamperData.LastName != newCamperData.LastName)
                        {
                            cmd.Parameters["@fieldToBeChanged"].Value = "last_name";
                            cmd.Parameters["@newData"].Value = newCamperData.LastName;
                            cmd.Parameters["@oldData"].Value = currentCamperData.LastName;
                            cmd.ExecuteNonQuery();
                        }
                        if (currentCamperData.DOB != newCamperData.DOB)
                        {
                            cmd.Parameters["@fieldToBeChanged"].Value = "dob";
                            cmd.Parameters["@newData"].Value = newCamperData.DOB;
                            cmd.Parameters["@oldData"].Value = currentCamperData.DOB;
                            cmd.ExecuteNonQuery();
                        }
                        if (currentCamperData.Medications != newCamperData.Medications)
                        {
                            cmd.Parameters["@fieldToBeChanged"].Value = "medications";
                            cmd.Parameters["@newData"].Value = newCamperData.Medications;
                            cmd.Parameters["@oldData"].Value = currentCamperData.Medications;
                            cmd.ExecuteNonQuery();
                        }
                        if (currentCamperData.SpecialNeeds != newCamperData.SpecialNeeds)
                        {
                            cmd.Parameters["@fieldToBeChanged"].Value = "special_needs";
                            cmd.Parameters["@newData"].Value = newCamperData.SpecialNeeds;
                            cmd.Parameters["@oldData"].Value = currentCamperData.SpecialNeeds;
                            cmd.ExecuteNonQuery();
                        }
                        if (currentCamperData.Allergies != newCamperData.Allergies)
                        {
                            cmd.Parameters["@fieldToBeChanged"].Value = "allergies";
                            cmd.Parameters["@newData"].Value = newCamperData.Allergies;
                            cmd.Parameters["@oldData"].Value = currentCamperData.Allergies;
                            cmd.ExecuteNonQuery();
                        }
                        if (currentCamperData.Registrar != newCamperData.Registrar)
                        {
                            cmd.Parameters["@fieldToBeChanged"].Value = "registrar";
                            cmd.Parameters["@newData"].Value = newCamperData.Registrar;
                            if (currentCamperData.Registrar == null)
                            {
                                cmd.Parameters["@oldData"].Value = DBNull.Value;
                            }
                            else
                            {
                                cmd.Parameters["@oldData"].Value = currentCamperData.Registrar;
                            }
                            cmd.ExecuteNonQuery();
                        }
                        if (currentCamperData.PaymentStatus != newCamperData.PaymentStatus)
                        {
                            cmd.Parameters["@fieldToBeChanged"].Value = "payment_status";
                            cmd.Parameters["@newData"].Value = newCamperData.PaymentStatus;
                            if (currentCamperData.Registrar == null)
                            {
                                cmd.Parameters["@oldData"].Value = DBNull.Value;
                            }
                            else
                            {
                                cmd.Parameters["@oldData"].Value = currentCamperData.PaymentStatus;
                            }
                            cmd.ExecuteNonQuery();
                        }
                        if (currentCamperData.ActiveStatus != newCamperData.ActiveStatus)
                        {
                            cmd.Parameters["@fieldToBeChanged"].Value = "active_status";
                            cmd.Parameters["@newData"].Value = newCamperData.ActiveStatus;
                            cmd.Parameters["@oldData"].Value = currentCamperData.ActiveStatus;
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
            int requestId = GetNextFamilyUpdateRequestId();
            string user = userId.ToString();

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();

                    using (SqlCommand cmd = new SqlCommand(sqlAddNewFamilyUpdateRequest, conn))
                    {
                        cmd.Parameters.AddWithValue("@requestId", requestId);
                        cmd.Parameters.AddWithValue("@familyId", newFamilyData.FamilyId);
                        cmd.Parameters.AddWithValue("@action", "Update");
                        cmd.Parameters.AddWithValue("@requestor", user);
                        cmd.Parameters.AddWithValue("@status", "Pending");
                        cmd.Parameters.AddWithValue("@requestDate", DateTime.Now.Date);

                        cmd.Parameters.Add("@fieldToBeChanged", SqlDbType.NVarChar);
                        cmd.Parameters.Add("@newData", SqlDbType.NVarChar);
                        cmd.Parameters.Add("@oldData", SqlDbType.NVarChar);
                        if (currentFamilyData.FullName != newFamilyData.FullName)
                        {
                            cmd.Parameters["@fieldToBeChanged"].Value = "parent_guardian_name";
                            cmd.Parameters["@newData"].Value = newFamilyData.FullName;
                            cmd.Parameters["@oldData"].Value = currentFamilyData.FullName;
                            cmd.ExecuteNonQuery();
                        }
                        if (currentFamilyData.Address != newFamilyData.Address)
                        {
                            cmd.Parameters["@fieldToBeChanged"].Value = "address";
                            cmd.Parameters["@newData"].Value = newFamilyData.Address;
                            cmd.Parameters["@oldData"].Value = currentFamilyData.Address;
                            cmd.ExecuteNonQuery();
                        }
                        if (currentFamilyData.City != newFamilyData.City)
                        {
                            cmd.Parameters["@fieldToBeChanged"].Value = "city";
                            cmd.Parameters["@newData"].Value = newFamilyData.City;
                            cmd.Parameters["@oldData"].Value = currentFamilyData.City;
                            cmd.ExecuteNonQuery();
                        }
                        if (currentFamilyData.State != newFamilyData.State)
                        {
                            cmd.Parameters["@fieldToBeChanged"].Value = "state";
                            cmd.Parameters["@newData"].Value = newFamilyData.State;
                            cmd.Parameters["@oldData"].Value = currentFamilyData.State;
                            cmd.ExecuteNonQuery();
                        }
                        if (currentFamilyData.Zip != newFamilyData.Zip)
                        {
                            cmd.Parameters["@fieldToBeChanged"].Value = "zip";
                            cmd.Parameters["@newData"].Value = newFamilyData.Zip;
                            cmd.Parameters["@oldData"].Value = currentFamilyData.Zip;
                            cmd.ExecuteNonQuery();
                        }
                        if (currentFamilyData.PhoneNumber != newFamilyData.PhoneNumber)
                        {
                            cmd.Parameters["@fieldToBeChanged"].Value = "phone";
                            cmd.Parameters["@newData"].Value = newFamilyData.PhoneNumber;
                            cmd.Parameters["@oldData"].Value = currentFamilyData.PhoneNumber;
                            cmd.ExecuteNonQuery();
                        }
                        if (currentFamilyData.EmailAddress != newFamilyData.EmailAddress)
                        {
                            cmd.Parameters["@fieldToBeChanged"].Value = "email_address";
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
