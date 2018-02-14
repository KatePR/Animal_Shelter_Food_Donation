using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace KateRaderProject2.App_Code
{
    public class ContactUti
        {
            public string id { get; set; }
            public string Name { get; set; }
            public string Content { get; set; }
            public string Time { get; set; }
            public ContactUti()
            {
            }
            public void insertLog()
            {
                SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["ShelterDonations2017"].ConnectionString);
                conn.Open();
                string insertQuery = "insert into ContactLog(Name, Content) values (@Name, @Content)";
                SqlCommand comd = new SqlCommand(insertQuery, conn);
                comd.Parameters.AddWithValue("@Name", Name);
                comd.Parameters.AddWithValue("@Content", Content);
                comd.ExecuteNonQuery();
                conn.Close();
            }
        }
    }