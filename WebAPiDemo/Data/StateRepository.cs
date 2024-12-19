using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Data.SqlClient;
using WebAPiDemo.Models;

namespace WebAPiDemo.Data
{
    public class StateRepository
    {
        private readonly string _connectionString;

        public StateRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("ConnectionString");
        }

        public List<StateModel> GetAllStates()
        {
            var states = new List<StateModel>();

            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                SqlCommand cmd = conn.CreateCommand();
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = "PR_LOC_State_SelectAll";
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    states.Add(new StateModel
                    {
                        StateID = Convert.ToInt32(reader["StateID"]),
                        StateName = reader["StateName"].ToString(),
                        StateCode = reader["StateCode"].ToString(),
                        CountryID = Convert.ToInt32(reader["CountryID"]),
                        CityCount = Convert.ToInt32(reader["CityCount"]),
                        CountryName = reader["CountryName"].ToString(),
                    });
                }
                return states;
            }
        }

        public StateModel SelectStateByID(int StateID)
        {
            StateModel state = null;
            SqlConnection conn = new SqlConnection(_connectionString);
            conn.Open();
            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.CommandText = "PR_LOC_State_SelectByPK";
            cmd.Parameters.AddWithValue("@StateID", StateID);

            SqlDataReader reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                state = new StateModel
                {
                    StateID = Convert.ToInt32(reader["StateID"]),
                    StateName = reader["StateName"].ToString(),
                    StateCode = reader["StateCode"].ToString(),
                    CountryID = Convert.ToInt32(reader["CountryID"]),
                };
            }
            return state;
        }

        public bool DeleteState(int StateID)
        {

            SqlConnection conn = new SqlConnection(_connectionString);
            conn.Open();
            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.CommandText = "PR_LOC_State_Delete";
            cmd.Parameters.AddWithValue("@StateID", StateID);

            int rowsAffected = cmd.ExecuteNonQuery();
            return rowsAffected > 0;
        }

        public bool InsertState(StateModel state)
        {
            SqlConnection conn = new SqlConnection(_connectionString);
            conn.Open();
            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.CommandText = "PR_LOC_State_Insert";

            cmd.Parameters.AddWithValue("@StateName", state.StateName);
            cmd.Parameters.AddWithValue("@StateCode", state.StateCode);
            cmd.Parameters.AddWithValue("@CountryID", state.CountryID);
            cmd.Parameters.AddWithValue("@CreatedDate", DateTime.Now);
            cmd.Parameters.AddWithValue("@ModifiedDate", DateTime.Now);

            int rowsAffected = cmd.ExecuteNonQuery();
            return rowsAffected > 0;
        }

        public bool UpdateState([Bind("StateName","StateCode","CountryID")]StateModel state)
        {
            SqlConnection conn = new SqlConnection(_connectionString);
            conn.Open();
            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.CommandText = "PR_LOC_State_Update";

            cmd.Parameters.AddWithValue("@StateID", state.StateID);
            cmd.Parameters.AddWithValue("@StateName", state.StateName);
            cmd.Parameters.AddWithValue("@StateCode", state.StateCode);
            cmd.Parameters.AddWithValue("@CountryID", state.CountryID);
            cmd.Parameters.Add("@ModifiedDate",SqlDbType.DateTime).Value=DBNull.Value;

            int rowsAffected = cmd.ExecuteNonQuery();
            return rowsAffected > 0;
        }
    }
}
