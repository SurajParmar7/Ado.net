using demo.Models;
using System.Data.SqlClient;
using static demo.Services.UserServices;

namespace demo.Services
{
    public class UserServices : IUserService
    {
        public string Constr { get; set; }
        public IConfiguration _configuration;
        public SqlConnection con;
        public UserServices(IConfiguration configuration)
        {
            _configuration = configuration;
            Constr = _configuration.GetConnectionString("DBConnection");
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public List<User> GetUserList()
        {
            List<User> users = new List<User>();
            try
            {
                using (con = new SqlConnection(Constr))
                {
                    con.Open();
                    var cmd = new SqlCommand("SP_GetUserList", con);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    SqlDataReader sqlDataReader = cmd.ExecuteReader();
                    while (sqlDataReader.Read())
                    {
                        User user = new User();
                        user.UserId = Convert.ToInt64(sqlDataReader["UserId"]);
                        user.FirstName = sqlDataReader["FirstName"].ToString();
                        user.LastName = sqlDataReader["LastName"].ToString();
                        user.Email = sqlDataReader["Email"].ToString();
                        user.Phone = sqlDataReader["Phone"].ToString();
                        user.StreetAddress = sqlDataReader["StreetAddress"].ToString();

                        if (sqlDataReader["CityId"] != DBNull.Value)
                            user.CityId = Convert.ToInt64(sqlDataReader["CityId"]);

                        if (sqlDataReader["StateId"] != DBNull.Value)
                            user.StateId = Convert.ToInt64(sqlDataReader["StateId"]);

                        user.UserName = sqlDataReader["UserName"].ToString();
                        user.Password = sqlDataReader["Password"].ToString();

                        users.Add(user);
                    }
                }
                return users.ToList();
            }
            catch (Exception)
            {
                throw;
            }
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public int SaveUser(User user)

        {
            using (con = new SqlConnection(Constr))
            {
                SqlCommand cmd = new SqlCommand("SP_insertuser", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                con.Open();
                cmd.Parameters.AddWithValue("@FirstName", user.FirstName);
                cmd.Parameters.AddWithValue("@LastName", user.LastName);
                cmd.Parameters.AddWithValue("@Email", user.Email);
                cmd.Parameters.AddWithValue("@Phone", user.Phone);
                cmd.Parameters.AddWithValue("@StreetAddress", user.StreetAddress);
                cmd.Parameters.AddWithValue("@CityId", user.CityId);
                cmd.Parameters.AddWithValue("@StateId", user.StateId);
                cmd.Parameters.AddWithValue("@UserName", user.UserName);
                cmd.Parameters.AddWithValue("@Password", user.Password);
                int i = cmd.ExecuteNonQuery();
                con.Close();
                return i;
            }
        }
        /// <summary>
        /// list of state
        /// </summary>
        /// <returns></returns>
        public List<State> GetStateList()
        {

            List<State> states = new List<State>();
            try
            {
                using (con = new SqlConnection(Constr))
                {
                    con.Open();
                    var cmd = new SqlCommand("sp_listofstate", con);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    SqlDataReader sqlDataReader = cmd.ExecuteReader();
                    while (sqlDataReader.Read())
                    {
                        State state = new State();
                        state.StateId = Convert.ToInt64(sqlDataReader["StateId"]);
                        state.StateName = sqlDataReader["Statename"].ToString();



                        states.Add(state);

                    }
                }
                return states.ToList();
            }
            catch (Exception)
            {
                throw;
            }
        }
        /// <summary>
        /// list of cities
        /// </summary>
        /// <param name="StateId"></param>
        /// <returns></returns>
        public List<City> GetCityList(int StateId)
        {
            List<City> cities = new List<City>();
            try
            {
                using (con = new SqlConnection(Constr))
                {
                    con.Open();
                    var cmd = new SqlCommand("sp_listofcity", con);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@StateId",StateId);
                    SqlDataReader sqlDataReader = cmd.ExecuteReader();
                    while (sqlDataReader.Read())
                    {
                        City city1 = new City();
                        city1.CityId = Convert.ToInt64(sqlDataReader["CityId"]);
                        city1.CityName = sqlDataReader["CityName"].ToString();



                        cities.Add(city1);
                    }
                }
                return cities.ToList();
            }
            catch (Exception)
            {
                throw;
            }

        }
        public bool CheckEmailAvailability(string email)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(Constr))
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("SP_CheckEmailAvailability", con);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Email", email);
                    SqlParameter isAvailableParam = new SqlParameter("@IsAvailable", System.Data.SqlDbType.Bit);
                    isAvailableParam.Direction = System.Data.ParameterDirection.Output;
                    cmd.Parameters.Add(isAvailableParam);
                    cmd.ExecuteNonQuery();
                    return Convert.ToBoolean(isAvailableParam.Value);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }




        public interface IUserService
        {
            public List<User> GetUserList();
            public int SaveUser(User user);
            public List<State> GetStateList();
            public List<City> GetCityList(int StateId);
            public bool CheckEmailAvailability(string email);
        }
    }
}
