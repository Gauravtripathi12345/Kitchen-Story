using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;

namespace KitchenDALLib
{
    public class FoodItem
    {
        public string kitchenStr;
        public SqlConnection con;

        public FoodItem()
        {
            kitchenStr = ConfigurationManager.ConnectionStrings["KitchenStoryDB"].ConnectionString;
            con = new SqlConnection(kitchenStr);
        }

        public List<FoodMaster> GetAllFoodItem()
        {
            List<FoodMaster> foodItemList = new List<FoodMaster>();
            SqlCommand cmd = new SqlCommand("Select * from FoodItem",con);
            con.Open(); 
            SqlDataReader reader = cmd.ExecuteReader();
            while(reader.Read()) 
            {
                FoodMaster item = new FoodMaster();
                item.Id = Convert.ToInt32(reader["Id"]);
                item.FoodName = reader["FoodName"].ToString();
                item.Price = Convert.ToSingle(reader["price"]);
                foodItemList.Add(item);
            }
            con.Close();    
            con.Dispose();
            return foodItemList;
        }

        public FoodMaster GetFoodItemById(int id)
        {
            SqlCommand cmd = new SqlCommand("Select * from FoodItem where Id = @Id", con);
            cmd.Parameters.AddWithValue("@Id", id);

            con.Open();
            SqlDataReader reader = cmd.ExecuteReader();
            FoodMaster item = null; 

            if (reader.HasRows)
            {
                reader.Read(); 

                item = new FoodMaster
                {
                    Id = Convert.ToInt32(reader["Id"]),
                    FoodName = reader["FoodName"].ToString(),
                    Price = Convert.ToSingle(reader["price"])
                };
            }

            con.Close();
            con.Dispose();
            return item;
        }


        public bool AddFoodItem(FoodMaster foodMaster)
        {
            SqlCommand cmd = new SqlCommand("dbo.sp_insertFoodItem", con);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@p_FoodName", foodMaster.FoodName);
            cmd.Parameters.AddWithValue("@p_Price", foodMaster.Price);
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();    
            con.Dispose();  
            return true;
        }

        public bool UpdateFoodItem(FoodMaster foodMaster) {
            SqlCommand cmd = new SqlCommand("dbo.sp_updateFoodItem", con);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@p_Id", foodMaster.Id);
            cmd.Parameters.AddWithValue("@p_FoodName", foodMaster.FoodName);
            cmd.Parameters.AddWithValue("@p_Price", foodMaster.Price);
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
            con.Dispose();
            return true;

        }
        public bool DeleteFoodItem(int id) {
            SqlCommand cmd = new SqlCommand("Delete * from FoodItem where Id="+id, con);
            con.Open();
            cmd.ExecuteNonQuery();  
            con.Close();    
            con.Dispose();
            return true;
        }
    }
}
