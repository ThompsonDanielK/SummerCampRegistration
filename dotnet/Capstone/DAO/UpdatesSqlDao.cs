using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Capstone.Models;

namespace Capstone.DAO
{
    public class UpdatesSqlDao
    {
        private readonly string connectionString;

        public UpdatesSqlDao(string connectionString)
        {
            if (string.IsNullOrWhiteSpace(connectionString))
                throw new ArgumentNullException(nameof(connectionString));
            this.connectionString = connectionString;
        }

        const string sqlGetLastUpdateRequestId = "SELECT TOP 1 request_id from updates ORDER BY request_id DESC";
        const string sqlAddNewUpdateRequest = "INSERT INTO updates " +
                    "(request_id, field_to_be_changed, camper_code, action, new_data, old_data, requestor, status, request_date) " +
                    "VALUES (@requestId, @fieldToBeChanged, @camperCode, @action, @newData, @oldData, @requestor, @status, @requestDate)";

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

        public bool AddNewUpdateRequest(Request request)
        {
            int requestId = GetNextUpdateRequestId();
            string user = "User";

            //  Are we adding request date/time here?

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();


                    using (SqlCommand cmd = new SqlCommand(sqlAddNewUpdateRequest, conn))
                    {
                        cmd.Parameters.AddWithValue("@requestId", requestId);
                        cmd.Parameters.AddWithValue("@camperCode", request.CamperCode);
                        cmd.Parameters.AddWithValue("@action", "Update");
                        cmd.Parameters.AddWithValue("@requestor", user);
                        cmd.Parameters.AddWithValue("@status", "Pending");
                        cmd.Parameters.AddWithValue("@requestDate", DateTime.Now);

                        cmd.Parameters.Add("@fieldToBeChanged", SqlDbType.NVarChar);
                        cmd.Parameters.Add("@newData", SqlDbType.NVarChar);
                        cmd.Parameters.Add("@oldData", SqlDbType.NVarChar);
                        foreach (RequestLineItem item in request.Items)
                        {
                            cmd.Parameters["@fieldToBeChanged"].Value = item.FieldToBeChanged;
                            cmd.Parameters["@newData"].Value = item.NewData;
                            cmd.Parameters["@oldData"].Value = item.OldData;
                            cmd.ExecuteNonQuery();
                        }
                    }
                } catch(SqlException ex)
                {
                    return false;
                }

                return true;
            }
        }

        public bool ProcessApprovedRequests()
        {

            return true;
        }

        public List<Update> FetchARequest(int requestId)
        {
            List<Update> updates = new List<Update>();


            return updates;
        }



        public bool AddNewDeleteRequest(Request request)
        {

            return true;
        }

        public bool AddNewAddCamperRequest(Request request)
        {

            return true;
        }




        public bool ApproveAndUpdateRequest(int requestId)
        {




            return true;
        }


    }
}
