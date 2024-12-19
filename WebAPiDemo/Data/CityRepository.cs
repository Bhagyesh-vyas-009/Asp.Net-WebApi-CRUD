using WebAPiDemo.Models;
using System.Data.SqlClient;
using System.Data;

namespace WebAPiDemo.Data
{
    public class CityRepository
    {
        private readonly string _connectionString;

        public CityRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("ConnectionString");
        }

        public List<CityModel> GetAllCities(int FilterID)
        {
            var cities = new List<CityModel>();

            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                SqlCommand cmd = conn.CreateCommand();
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = "PR_LOC_City_SelectAll";
                cmd.Parameters.AddWithValue("@StateID", FilterID);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    cities.Add(new CityModel
                    {
                        CityID = Convert.ToInt32(reader["CityID"]),
                        CityName = reader["CityName"].ToString(),
                        CityCode = reader["CityCode"].ToString(),
                        StateID = Convert.ToInt32(reader["StateID"]),
                        StateName= reader["StateName"].ToString(),
                        CountryID = Convert.ToInt32(reader["CountryID"]),
                        CountryName = reader["CountryName"].ToString(),
                    });
                }
                return cities;
            }
        }

        public CityModel SelectCityByPK(int CityID)
        {
            CityModel city = null;
            SqlConnection conn = new SqlConnection(_connectionString);
            conn.Open();
            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.CommandText = "PR_LOC_City_SelectByPK";
            cmd.Parameters.AddWithValue("@CityID", CityID);

            SqlDataReader reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                city = new CityModel
                {
                    CityID = Convert.ToInt32(reader["CityID"]),
                    CityName = reader["CityName"].ToString(),
                    CityCode = reader["CityCode"].ToString(),
                    StateID = Convert.ToInt32(reader["StateID"]),
                    CountryID = Convert.ToInt32(reader["CountryID"]),
                };
            }
            return city;
        }

        public bool DeleteCity(int CityID)
        {
            SqlConnection conn = new SqlConnection(_connectionString);
            conn.Open();
            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.CommandText = "PR_LOC_City_Delete";
            cmd.Parameters.AddWithValue("@CityID", CityID);

            int rowsAffected = cmd.ExecuteNonQuery();
            return rowsAffected > 0;
        }

        public bool InsertCity(CityModel city)
        {
            SqlConnection conn=new SqlConnection(_connectionString);
            conn.Open();
            SqlCommand cmd=conn.CreateCommand();
            cmd.CommandType=System.Data.CommandType.StoredProcedure;
            cmd.CommandText = "PR_LOC_City_Insert";

            cmd.Parameters.AddWithValue("@CityName", city.CityName);
            cmd.Parameters.AddWithValue("@CityCode", city.CityCode);
            cmd.Parameters.AddWithValue("@StateID", city.CountryID);
            cmd.Parameters.AddWithValue("@CountryID", city.CountryID);
            cmd.Parameters.AddWithValue("@CreatedDate", DateTime.Now);
            cmd.Parameters.AddWithValue("@ModifiedDate", DateTime.Now);

            int rowsAffected = cmd.ExecuteNonQuery();
            return rowsAffected > 0;
        }

        public bool UpdateCity(CityModel city)
        {
            SqlConnection conn = new SqlConnection(_connectionString);
            conn.Open();
            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.CommandText = "PR_LOC_City_Update";

            cmd.Parameters.AddWithValue("@CityID",city.CityID);
            cmd.Parameters.AddWithValue("@CityName", city.CityName);
            cmd.Parameters.AddWithValue("@CityCode", city.CityCode);
            cmd.Parameters.AddWithValue("@StateID", city.StateID);
            cmd.Parameters.AddWithValue("@CountryID", city.CountryID);
            cmd.Parameters.Add("@ModifiedDate", SqlDbType.DateTime).Value = DBNull.Value;

            int rowsAffected = cmd.ExecuteNonQuery();
            return rowsAffected > 0;
        }
    }
}
