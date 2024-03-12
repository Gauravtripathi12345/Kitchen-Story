using System.Configuration;
using System.Data.SqlClient;

namespace KitchenDALLib
{
    public class AdminManager
    {
        public string kitchenStr;
        public SqlConnection con;
        public AdminManager()
        {
            kitchenStr = ConfigurationManager.ConnectionStrings["KitchenStoryDB"].ConnectionString;
            con = new SqlConnection(kitchenStr);
        }

        public bool AdminLogin(AdminMaster adminMaster)
        {
            SqlCommand cmd = new SqlCommand("Select Password from Admin where EmailId = @EmailId", con);
            cmd.Parameters.AddWithValue("@EmailId", adminMaster.EmailId);
            con.Open();
            SqlDataReader reader = cmd.ExecuteReader();

            if (reader.Read())
            {
                string strPassword = reader["Password"].ToString();
                if (strPassword == adminMaster.Password)
                {
                    return true;
                }
            }
            return false;
        }


        public bool ChangePassword(AdminMaster adminMaster) 
        {
            SqlCommand cmd = new SqlCommand("dbo.sp_updatePassword", con);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@p_EmailId", adminMaster.EmailId);
            cmd.Parameters.AddWithValue("@p_Password", adminMaster.Password);
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
            con.Dispose();
            return true;
        }

        public bool validEmail(string EmailId)
        {
            SqlCommand cmd = new SqlCommand("SELECT COUNT(*) FROM Admin WHERE EmailId = @EmailId", con);
            cmd.Parameters.AddWithValue("@EmailId", EmailId);

            con.Open();
            int count = (int)cmd.ExecuteScalar();
            con.Close();

            return count > 0;
        }
    }
}
