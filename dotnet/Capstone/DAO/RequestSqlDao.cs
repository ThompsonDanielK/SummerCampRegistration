﻿using System;
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

        const string sqlAddNewCamperUpdateRequest = "INSERT INTO camper_updates " +
                    "(field_to_be_changed, camper_code, action, new_data, old_data, requestor, status, request_date) " +
                    "VALUES (@fieldToBeChanged, @camperCode, @action, @newData, @oldData, @requestor, @status, @requestDate); SELECT @@IDENTITY";
        const string sqlAddNewFamilyUpdateRequest = "INSERT INTO family_updates " +
                    "(field_to_be_changed, family_id, action, new_data, old_data, requestor, status, request_date) " +
                    "VALUES (@fieldToBeChanged, @familyId, @action, @newData, @oldData, @requestor, @status, @requestDate); SELECT @@IDENTITY";
        const string sqlGetCamperUpdateList = "SELECT request_id, field_to_be_changed, camper_code, action, " +
            "new_data, old_data, requestor, status, request_date, finalize_date FROM camper_updates";
        const string sqlGetFamilyUpdateList = "SELECT request_id, field_to_be_changed, family_id, action," +
            " new_data, old_data, requestor, status, request_date, finalize_date FROM family_updates";
        const string sqlGetUpdatesByCamperCode = "SELECT request_id, field_to_be_changed, camper_code, action, " +
            "new_data, old_data, requestor, status, request_date, finalize_date FROM camper_updates WHERE camper_code = @camper_code";
        const string sqlGetUpdatesByFamilyId = "SELECT request_id, field_to_be_changed, family_id, action, " +
            "new_data, old_data, requestor, status, request_date, finalize_date FROM family_updates WHERE family_id = @family_id";

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
                updateList.Add(update);
            }
            return updateList;
        }

        public List<Update> GetFamilyUpdatesByFamilyId(int familyId)
        {
            List<Update> updateList = new List<Update>();
            using SqlConnection conn = new SqlConnection(connectionString);
            conn.Open();
            using SqlCommand cmd = new SqlCommand(sqlGetUpdatesByFamilyId, conn);
            cmd.Parameters.AddWithValue("@family_id", familyId);
            using SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                Update update = BuildUpdate(reader, false);
                updateList.Add(update);
            }
            return updateList;
        }

        private Update BuildUpdate(SqlDataReader reader, bool isCamper)
        {
            Update update = new Update() { };

            update.RequestId = Convert.ToInt32(reader["request_id"]);
            update.FieldToBeChanged = Convert.ToString(reader["field_to_be_changed"]);
            update.Action = Convert.ToString(reader["action"]);
            update.Requestor = Convert.ToString(reader["requestor"]);
            update.Status = Convert.ToString(reader["status"]);
            update.RequestDate = Convert.ToDateTime(reader["request_date"]);
            if (reader["old_data"] != DBNull.Value)
            {
                update.OldData = Convert.ToString(reader["old_data"]);
            }
            else
            {
                update.OldData = "";
            }
            if (reader["new_data"] != DBNull.Value)
            {
                update.NewData = Convert.ToString(reader["new_data"]);
            }
            else
            {
                update.NewData = "";
            }
            if (reader["finalize_date"] != DBNull.Value)
            {
                update.FinalizeDate = Convert.ToDateTime(reader["finalize_date"]);
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

        public int AddNewCamperUpdateRequest(int userId, Camper newCamperData, Camper currentCamperData)
        {
            string user = userId.ToString();

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();

                    using (SqlCommand cmd = new SqlCommand(sqlAddNewCamperUpdateRequest, conn))
                    {
                        cmd.Parameters.AddWithValue("@camperCode", newCamperData.CamperCode);
                        cmd.Parameters.AddWithValue("@action", "Update");
                        cmd.Parameters.AddWithValue("@requestor", user);
                        cmd.Parameters.AddWithValue("@status", "Pending");
                        cmd.Parameters.AddWithValue("@requestDate", DateTime.Now.Date);

                        cmd.Parameters.Add("@fieldToBeChanged", SqlDbType.NVarChar);
                        cmd.Parameters.Add("@newData", SqlDbType.NVarChar);
                        cmd.Parameters.Add("@oldData", SqlDbType.NVarChar);
                        int requestId = 0;
                        if (currentCamperData.FirstName != newCamperData.FirstName)
                        {
                            cmd.Parameters["@fieldToBeChanged"].Value = "first_name";
                            cmd.Parameters["@newData"].Value = newCamperData.FirstName;
                            cmd.Parameters["@oldData"].Value = currentCamperData.FirstName;
                            requestId = Convert.ToInt32(cmd.ExecuteScalar());
                        }
                        if (currentCamperData.LastName != newCamperData.LastName)
                        {
                            cmd.Parameters["@fieldToBeChanged"].Value = "last_name";
                            cmd.Parameters["@newData"].Value = newCamperData.LastName;
                            cmd.Parameters["@oldData"].Value = currentCamperData.LastName;
                            requestId = Convert.ToInt32(cmd.ExecuteScalar());
                        }
                        if (currentCamperData.DOB != newCamperData.DOB)
                        {
                            cmd.Parameters["@fieldToBeChanged"].Value = "dob";
                            cmd.Parameters["@newData"].Value = newCamperData.DOB;
                            cmd.Parameters["@oldData"].Value = currentCamperData.DOB;
                            requestId = Convert.ToInt32(cmd.ExecuteScalar());
                        }
                        if (currentCamperData.Medications != newCamperData.Medications)
                        {
                            cmd.Parameters["@fieldToBeChanged"].Value = "medications";
                            cmd.Parameters["@newData"].Value = newCamperData.Medications;
                            cmd.Parameters["@oldData"].Value = currentCamperData.Medications;
                            requestId = Convert.ToInt32(cmd.ExecuteScalar());
                        }
                        if (currentCamperData.SpecialNeeds != newCamperData.SpecialNeeds)
                        {
                            cmd.Parameters["@fieldToBeChanged"].Value = "special_needs";
                            cmd.Parameters["@newData"].Value = newCamperData.SpecialNeeds;
                            cmd.Parameters["@oldData"].Value = currentCamperData.SpecialNeeds;
                            requestId = Convert.ToInt32(cmd.ExecuteScalar());
                        }
                        if (currentCamperData.Allergies != newCamperData.Allergies)
                        {
                            cmd.Parameters["@fieldToBeChanged"].Value = "allergies";
                            cmd.Parameters["@newData"].Value = newCamperData.Allergies;
                            cmd.Parameters["@oldData"].Value = currentCamperData.Allergies;
                            requestId = Convert.ToInt32(cmd.ExecuteScalar());
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
                            requestId = Convert.ToInt32(cmd.ExecuteScalar());
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
                            requestId = Convert.ToInt32(cmd.ExecuteScalar());
                        }
                        if (currentCamperData.ActiveStatus != newCamperData.ActiveStatus)
                        {
                            cmd.Parameters["@fieldToBeChanged"].Value = "active_status";
                            cmd.Parameters["@newData"].Value = newCamperData.ActiveStatus;
                            cmd.Parameters["@oldData"].Value = currentCamperData.ActiveStatus;
                            requestId = Convert.ToInt32(cmd.ExecuteScalar());
                        }
                        if (currentCamperData.DateAdded != newCamperData.DateAdded)
                        {
                            cmd.Parameters["@fieldToBeChanged"].Value = "date_added";
                            cmd.Parameters["@newData"].Value = newCamperData.ActiveStatus;
                            cmd.Parameters["@oldData"].Value = currentCamperData.ActiveStatus;
                            requestId = Convert.ToInt32(cmd.ExecuteScalar());
                        }
                        return requestId;
                    }
                }
                catch (SqlException ex)
                {
                    return -1;
                }
            }
        }

        public int AddNewFamilyUpdateRequest(int userId, Family newFamilyData, Family currentFamilyData, string username)
        {
            string user = userId.ToString();

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();

                    int requestId = 0;

                    using (SqlCommand cmd = new SqlCommand(sqlAddNewFamilyUpdateRequest, conn))
                    {
                        cmd.Parameters.AddWithValue("@familyId", newFamilyData.FamilyId);
                        cmd.Parameters.AddWithValue("@action", "Update");
                        cmd.Parameters.AddWithValue("@requestor", username);
                        cmd.Parameters.AddWithValue("@status", "Pending");
                        cmd.Parameters.AddWithValue("@requestDate", DateTime.Now.Date);

                        cmd.Parameters.Add("@fieldToBeChanged", SqlDbType.NVarChar);
                        cmd.Parameters.Add("@newData", SqlDbType.NVarChar);
                        cmd.Parameters.Add("@oldData", SqlDbType.NVarChar);
                        if (currentFamilyData.FullName != newFamilyData.FullName)
                        {
                            cmd.Parameters["@fieldToBeChanged"].Value = "full_name";
                            cmd.Parameters["@newData"].Value = newFamilyData.FullName;
                            cmd.Parameters["@oldData"].Value = currentFamilyData.FullName;
                            requestId = Convert.ToInt32(cmd.ExecuteScalar());
                        }
                        if (currentFamilyData.Address != newFamilyData.Address)
                        {
                            cmd.Parameters["@fieldToBeChanged"].Value = "address";
                            cmd.Parameters["@newData"].Value = newFamilyData.Address;
                            cmd.Parameters["@oldData"].Value = currentFamilyData.Address;
                            requestId = Convert.ToInt32(cmd.ExecuteScalar());
                        }
                        if (currentFamilyData.City != newFamilyData.City)
                        {
                            cmd.Parameters["@fieldToBeChanged"].Value = "city";
                            cmd.Parameters["@newData"].Value = newFamilyData.City;
                            cmd.Parameters["@oldData"].Value = currentFamilyData.City;
                            requestId = Convert.ToInt32(cmd.ExecuteScalar());
                        }
                        if (currentFamilyData.State != newFamilyData.State)
                        {
                            cmd.Parameters["@fieldToBeChanged"].Value = "state";
                            cmd.Parameters["@newData"].Value = newFamilyData.State;
                            cmd.Parameters["@oldData"].Value = currentFamilyData.State;
                            requestId = Convert.ToInt32(cmd.ExecuteScalar());
                        }
                        if (currentFamilyData.Zip != newFamilyData.Zip)
                        {
                            cmd.Parameters["@fieldToBeChanged"].Value = "zip";
                            cmd.Parameters["@newData"].Value = newFamilyData.Zip;
                            cmd.Parameters["@oldData"].Value = currentFamilyData.Zip;
                            requestId = Convert.ToInt32(cmd.ExecuteScalar());
                        }
                        if (currentFamilyData.PhoneNumber != newFamilyData.PhoneNumber)
                        {
                            cmd.Parameters["@fieldToBeChanged"].Value = "phone_number";
                            cmd.Parameters["@newData"].Value = newFamilyData.PhoneNumber;
                            cmd.Parameters["@oldData"].Value = currentFamilyData.PhoneNumber;
                            requestId = Convert.ToInt32(cmd.ExecuteScalar());
                        }
                        if (currentFamilyData.EmailAddress != newFamilyData.EmailAddress)
                        {
                            cmd.Parameters["@fieldToBeChanged"].Value = "email_address";
                            cmd.Parameters["@newData"].Value = newFamilyData.EmailAddress;
                            cmd.Parameters["@oldData"].Value = currentFamilyData.EmailAddress;
                            requestId = Convert.ToInt32(cmd.ExecuteScalar());
                        }
                        return requestId;
                    }
                }
                catch (SqlException ex)
                {
                    return -1;
                }
            }
        }


    }
}
