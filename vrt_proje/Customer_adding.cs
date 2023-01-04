using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace vrt_proje
{
    internal class Customer_adding
    {
        public SQLiteConnection conn;

        public void add_customer(double lat, double lon, int cust_ıd, string city_kod)
        {
            string addcustomer = "INSERT INTO customer(\n";
            addcustomer += "custID,\n";
            addcustomer += "Lat,\n";
            addcustomer += "Lng,\n";
            addcustomer += "city_kod)\n";
            addcustomer += "VALUES(\n";
            addcustomer += "\t" + cust_ıd.ToString() + ",\n";
            addcustomer += "\t" + lat.ToString() + ",\n";
            addcustomer += "\t" + lon.ToString() + ",\n";
            addcustomer += "\t" + city_kod.ToString() + "\n";
            addcustomer += ")";

            conn = new SQLiteConnection(@"Data Source=C:\Users\Rıfat DEMİROK\Downloads\Boundaries.s3db;");
            SQLiteCommand add_c = new SQLiteCommand(addcustomer, conn);
            conn.Open();
            add_c.ExecuteNonQuery();
            conn.Close();
        }
    }
}
