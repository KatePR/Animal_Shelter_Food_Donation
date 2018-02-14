using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Configuration;

namespace KateRaderProject2.App_Code
{
    public class CustomerUtil
    {//instance variables
        public string userName { get; set; }
        public string fName { get; set; }
        public string lName { get; set; }
        public string email { get; set; }
        public string country { get; set; }
        public string password { get; set; }
        public int age { get; set; }
        public char gender { get; set; }
            //Methods
            public void insertData() {
                SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["ShelterDonations2017"].ConnectionString);
                conn.Open();
                string insertString= "insert into Customer(UserName, FName, LName, Email, Country, Password, Age, Gender) values(@UserName, @FName, @LName, @Email, @Country, @Password, @Age, @Gender)";
            SqlCommand comd = new SqlCommand(insertString, conn);
            comd.Parameters.AddWithValue("@UserName", this.userName);
            comd.Parameters.AddWithValue("@FName", this.fName);
            comd.Parameters.AddWithValue("@LName", this.lName);
            comd.Parameters.AddWithValue("@Email", this.email);
            comd.Parameters.AddWithValue("@Country", this.country);
            comd.Parameters.AddWithValue("@Password", EncryptPassword.encryptString(this.password));
            comd.Parameters.AddWithValue("@Age", this.age);
            comd.Parameters.AddWithValue("@Gender", this.gender);
            comd.ExecuteNonQuery();
            conn.Close();

        }//close insertData method

        public bool checkUserExist() {
            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["ShelterDonations2017"].ConnectionString);
            conn.Open();
            string checkUser = "select * from Customer where UserName = @userName";
            SqlCommand comd = new SqlCommand(checkUser, conn);
            comd.Parameters.AddWithValue("@userName", this.userName);
            SqlDataReader dr = comd.ExecuteReader();
            if (dr.HasRows)
            {
                dr.Close();
                conn.Close();
                return true;
            }

            dr.Close();
            conn.Close();
            return false;
        }//close checkUserExist() method

        public bool checkPassword() {
            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["ShelterDonations2017"].ConnectionString);
            conn.Open();
            string checkUser = "select * from Customer where UserName = @userName";
            SqlCommand comd = new SqlCommand(checkUser, conn);
            comd.Parameters.AddWithValue("@userName", this.userName);
            SqlDataReader dr = comd.ExecuteReader();
            dr.Read();
            if (dr.HasRows){
                if (dr["Password"].ToString().Equals(EncryptPassword.encryptString(this.password))) {
                    dr.Close();
                    conn.Close();
                    return true;

                }


            }

            return false;
        }

        public CustomerUtil getUserInfo(string userNameInput) {
            CustomerUtil c = new CustomerUtil();
            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["ShelterDonations2017"].ConnectionString);
            conn.Open();
            string searchStr = "select UserName, FName, LName, Email, Country, Password, Age, Gender from Customer where UserName = @userName";
            SqlCommand comd = new SqlCommand(searchStr, conn);
            comd.Parameters.AddWithValue("@userName", userNameInput);
            SqlDataReader dr = comd.ExecuteReader();
            dr.Read();
            if (dr.HasRows) {
                c.userName = dr[0].ToString();
                c.fName = dr[1].ToString();
                c.lName = dr[2].ToString();
                c.email = dr[3].ToString();
                c.country = dr[4].ToString();
                c.password = dr[5].ToString();
                c.age = Convert.ToInt32(dr[6].ToString());
                c.gender = dr[7].ToString().ToCharArray()[0];

            }
            dr.Close();
            conn.Close();
            return c;
        }//close getUserInfo method

        public void resetPassword(string newPassword) {
            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["ShelterDonations2017"].ConnectionString);
            conn.Open();
            string updateStr = "update Customer Set Customer.Password = @password where Customer.UserName = @userName";
            SqlCommand comd = new SqlCommand(updateStr, conn);
            comd.Parameters.AddWithValue("@userName", this.userName);
            comd.Parameters.AddWithValue("@password", EncryptPassword.encryptString(newPassword));
            comd.ExecuteNonQuery();
            conn.Close();

        }//close reset password method

    }//close CustommerUtil class
}