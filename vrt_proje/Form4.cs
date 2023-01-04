using GMap.NET.WindowsForms.Markers;
using GMap.NET.WindowsForms;
using GMap.NET;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SQLite;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using GMap.NET.WindowsPresentation;

namespace vrt_proje
{
    public partial class Form4 : Form
    {
        //public Form2 form2=new Form2();
        public Form4()
        {
            InitializeComponent();
        }
        public int düğümsayısı;
        public List<int> custIDs;
        public List<double> Lats;
        public List<double> Lngs;
        public GMapOverlay markerOverlay;
        public void showbt_Click(object sender, EventArgs e)
        {
            Form2 form2 = new Form2();

            string city_code = textBox1.Text;
            string command_sql = "SELECT custID, city_kod,Lat,Lng FROM customer WHERE city_kod = " + "\"" + city_code + "\" ";
            string conn_text = @"Data Source= C:\Users\Rıfat DEMİROK\Downloads\Boundaries.s3db;";
            SQLiteConnection Show_marker_conn = new SQLiteConnection(conn_text);
            SQLiteCommand get_marker = new SQLiteCommand(command_sql, Show_marker_conn);
            //SqlCommand a=new SqlCommand()
            Show_marker_conn.Open();
            SQLiteDataReader Take_Marker = get_marker.ExecuteReader();
            Lats = new List<double>(0);
            Lngs = new List<double>(0);
            custIDs = new List<int>(0);



            while (Take_Marker.Read())
            {
                int CustIDs = Convert.ToInt32(Take_Marker["CustID"]);
                double Lat = Convert.ToDouble(Take_Marker["Lat"]);
                double Lng = Convert.ToDouble(Take_Marker["Lng"]);

                Lats.Add(Lat);
                Lngs.Add(Lng);
                custIDs.Add(CustIDs);

                // form2.add_marker(Lat, Lng, CustIDs);
            }
            düğümsayısı = Lats.Count;

            Show_marker_conn.Close();
            for (int i = 0; i < düğümsayısı; i++)
            {
                add_marker(Lats[i], Lngs[i], custIDs[i]);
            }

        }

        public void add_marker(double lat, double lon, int CustID)
        {
            Form2 form2 = new Form2();
            GMapOverlay markerOverlay = new GMapOverlay();
            PointLatLng marker_point = new PointLatLng(lat, lon);
            GMarkerGoogle marker = new GMarkerGoogle(marker_point, GMarkerGoogleType.lightblue_dot);
            marker.ToolTip = new GMapToolTip(marker);//markerin içine tool tanımladık
            marker.ToolTipText = Convert.ToString(CustID);//markerin içine yazı yazdırdık
            marker.ToolTipMode = MarkerTooltipMode.OnMouseOver;//markerin içine yazının ne zaman görüneceğini yazdık
            markerOverlay.Markers.Add(marker);
            
            markerOverlay = new GMapOverlay("markers");
            form2.gMapControl1.Overlays.Add(markerOverlay);

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
