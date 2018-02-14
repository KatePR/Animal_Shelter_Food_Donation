using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace KateRaderProject2.App_Code
{
    public class OrderUti
    {
        public string OrderId { get; set; }
        public string UserName { get; set; }
        public string BagSize { get; set; }
        public string BagStyle { get; set; }
        public string BagSizeCat { get; set; }
        public double Price { get; set; }
        public string OrderTime { get; set; }
        public string Delivery_ID { get; set; }

        public void insertOrder()
        {
            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["ShelterDonations2017"].ConnectionString);
            conn.Open();
            string qry = "insert into [Order_Food] (BagSize,BagStyle,BagSizeCat,Price,UserName,Delivery_ID) values (@BagSize,@BagStyle,@BagSizeCat,@Price,@UserName,@Delivery_ID)";
            SqlCommand cmd = new SqlCommand(qry, conn);
            cmd.Parameters.AddWithValue("@UserName", UserName);
            cmd.Parameters.AddWithValue("@BagSize", BagSize);
            cmd.Parameters.AddWithValue("@BagStyle", BagStyle);
            cmd.Parameters.AddWithValue("@BagSizeCat", BagSizeCat);
            cmd.Parameters.AddWithValue("@Price", Price);
            cmd.Parameters.AddWithValue("@Delivery_ID", Delivery_ID);
            cmd.ExecuteNonQuery();
            conn.Close();
        }
    }
}