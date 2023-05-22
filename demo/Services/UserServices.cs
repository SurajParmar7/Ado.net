using demo.Models;
using System.Data.SqlClient;

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
                    while(sqlDataReader.Read())
                    {
                        User user = new User();
                        user.UserId =Convert.ToInt64(sqlDataReader["UserId"]);
                        user.FirstName = sqlDataReader["FirstName"].ToString();
                        user.LastName = sqlDataReader["LastName"].ToString();
                        user.Email = sqlDataReader["Email"].ToString();
                        user.Phone = sqlDataReader["Phone"].ToString();
                        user.StreetAddress = sqlDataReader["StreetAddress"].ToString();
                        user.City = sqlDataReader["City"].ToString();
                        user.State = sqlDataReader["State"].ToString();
                        user.UserName = sqlDataReader["UserName"].ToString();
                        user.Password = sqlDataReader["Password"].ToString();
                        

                        users.Add(user);
                       
                    }
                }
                return users.ToList();
            }
            catch (Exception )
            {
                throw;
            }
        }
        
        
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
                cmd.Parameters.AddWithValue("@City", user.City);
                cmd.Parameters.AddWithValue("@State", user.State);
                cmd.Parameters.AddWithValue("@UserName", user.UserName);
                cmd.Parameters.AddWithValue("@Password", user.Password);
                int i = cmd.ExecuteNonQuery();
                con.Close();
                return i;
            }
        }

    }

    public interface IUserService
    {
        public List<User> GetUserList();
        public int SaveUser(User user);
    }
}
