using MVC_Demo.Common;
using MVC_Demo.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace MVC_Demo.DAL
{
    public class RegisterDataLayer
    {
        public string SignUpUser(UserModel model)
        {
            Password encryptPassword = new Password();
            SqlConnection con = new SqlConnection("Data Source=(LocalDb)\\MSSQLLocalDB;Initial Catalog=MVC-Demo;Integrated Security=True");

            try
            {
                SqlCommand cmd = new SqlCommand("proc_RegisterUser", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Name", model.Name);
                cmd.Parameters.AddWithValue("@Email", model.Email);
                cmd.Parameters.AddWithValue("@Mobile", model.Mobile);
                cmd.Parameters.AddWithValue("@Password", encryptPassword.EncryptPassword(model.Password));
                cmd.Parameters.AddWithValue("@Gender", model.Gender);

                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
                return ("Data saved successfully");
            }
            catch(Exception ex)
            {
                if(con.State == ConnectionState.Open)
                {
                    con.Close();
                }
                return (ex.Message.ToString());
            }
        }
    }
}