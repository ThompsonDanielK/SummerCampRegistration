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

        public int AddNewCamperUpdateRequest(Update update)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();

                    using (SqlCommand cmd = new SqlCommand(sqlAddNewCamperUpdateRequest, conn))
                    {
                        if (update.OldData == "None")
                        {
                            cmd.Parameters.AddWithValue("@oldData", "");
                        }
                        else
                        {
                            cmd.Parameters.AddWithValue("@oldData", update.OldData);
                        }
                        if (update.NewData == "None")
                        {
                            cmd.Parameters.AddWithValue("@newData", "");
                        }
                        else
                        {
                            cmd.Parameters.AddWithValue("@newData", update.NewData);
                        }
                        cmd.Parameters.AddWithValue("@camperCode", update.CamperCode);
                        cmd.Parameters.AddWithValue("@action", update.Action);
                        cmd.Parameters.AddWithValue("@requestor", update.Requestor);
                        cmd.Parameters.AddWithValue("@status", "Pending");
                        cmd.Parameters.AddWithValue("@requestDate", DateTime.Now.Date);
                        cmd.Parameters.AddWithValue("@fieldToBeChanged", update.FieldToBeChanged);
                        int requestId = Convert.ToInt32(cmd.ExecuteScalar());
                        return requestId;
                    }
                }
                catch (SqlException ex)
                {
                    return -1;
                }
            }
        }

        public int AddNewFamilyUpdateRequest(Update update)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();

                    using (SqlCommand cmd = new SqlCommand(sqlAddNewFamilyUpdateRequest, conn))
                    {
                        cmd.Parameters.AddWithValue("@familyId", update.FamilyId);
                        cmd.Parameters.AddWithValue("@action", "Update");
                        cmd.Parameters.AddWithValue("@requestor", update.Requestor);
                        cmd.Parameters.AddWithValue("@status", "Pending");
                        cmd.Parameters.AddWithValue("@requestDate", DateTime.Now.Date);
                        cmd.Parameters.AddWithValue("@fieldToBeChanged", update.FieldToBeChanged);
                        cmd.Parameters.AddWithValue("@newData", update.NewData);
                        cmd.Parameters.AddWithValue("@oldData", update.OldData);
                        int requestId = Convert.ToInt32(cmd.ExecuteScalar());
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
