using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace KateRaderProject2.App_Code
{
    public class AddressUti
    {
        public string Delivery_ID { get; set; }
        public string UserName { get; set; }
        public string AddressType { get; set; }
        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }
        public string ZipCode { get; set; }
        public string Phone { get; set; }

        public void insertAddress()
        {
            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["ShelterDonations2017"].ConnectionString);
            conn.Open();
            string qry = "insert into [Delivery_Address] (Address_Type,AddressLine1,AddressLine2,ZipCode,Phone,UserName)values ( @AddressType,@AddressLine1,@AddressLine2, @ZipCode,@Phone,@UserName) select @@identity";
            SqlCommand cmd = new SqlCommand(qry, conn);
            cmd.Parameters.AddWithValue("@UserName", UserName);
            cmd.Parameters.AddWithValue("@AddressType", AddressType);
            cmd.Parameters.AddWithValue("@AddressLine1", AddressLine1);
            cmd.Parameters.AddWithValue("@AddressLine2", AddressLine2);
            cmd.Parameters.AddWithValue("@ZipCode", ZipCode);
            cmd.Parameters.AddWithValue("@Phone", Phone);
            SqlDataReader dr = cmd.ExecuteReader();
            dr.Read();
            Delivery_ID = dr[0].ToString();
            dr.Close();
            conn.Close();
        }
        public void selectAddress()
        {
            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["ShelterDonations2017"].ConnectionString);
            conn.Open();
            string qry = "select * from [Delivery_Address] where (Delivery_ID=@Delivery_ID)";
            SqlCommand cmd = new SqlCommand(qry, conn);
            cmd.Parameters.AddWithValue("@Delivery_ID", Delivery_ID);
            SqlDataReader dr = cmd.ExecuteReader();
            dr.Read();

            AddressType = dr[1].ToString();
            AddressLine1 = dr[2].ToString();
            AddressLine2 = dr[3].ToString();
            ZipCode = dr[4].ToString();
            Phone = dr[5].ToString();
            UserName = dr[6].ToString();
            dr.Close();
            conn.Close();
        }
    }
}