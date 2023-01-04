using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using GMap.NET.MapProviders;
using GMap.NET.WindowsForms;
using GMap.NET.WindowsForms.Markers;
using GMap.NET;
//using System.Device.Location;
using System.Data.SQLite;
using System.Data.SqlClient;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ToolBar;
using System.Runtime.Remoting;
using System.Device.Location;
using System.Drawing.Drawing2D;

namespace vrt_proje
{
    public partial class Form2 : Form
    {
        Customer_adding cust_add = new Customer_adding();

        public GMapOverlay markerOverlay;
        public GMapOverlay markeroverlay2map;
        public GMapOverlay markeroverlay3map;
        public GMapOverlay markeroverlay4map;

        public GMapOverlay Routeoverlay;
        public GMapOverlay Routeoverlay4map;


        SQLiteConnection conn;
        Random random = new Random();

        Form3 postal_form3 = new Form3();
        Form4 _form4 = new Form4();
        solution Sol;

        public string file_dir;
        public string file_name;
        public string file_Way;
        public string city_kod;

        public string city_code00;
        public int lnght;
        public int node_count;
        public List<int> custIDs;
        public List<double> Lats;
        public List<double> Lngs;
        public List<double> Lngs00;
        public List<double> Lats00;
        public List<int> custIDs00;
        public List<PointLatLng> select_route_point;
        public List<string> LatLng_list;
        public List<double> Lat_list;
        public List<double> Lng_list;
        //char[] coor_split = { ':' };
        public List<string> slt_cust;
        public int c_box_lnght;
        public int NodeNum; 
        //public int NodeNum;    //Düğüm sayısı
        public double[,] DistMat4;  //Uzaklık matrisi

        List<PointLatLng> RoutePoints4;
        public List<int> Route4;
        public List<int> BestRoute4;
        public double Cost4 = 0;
        public double BestCost4 = double.MaxValue;
        public Form2()
        {
            InitializeComponent();
        }
        public void Form2_Load(object sender, EventArgs e)
        {


            gMapControl1.MapProvider = GoogleHybridMapProvider.Instance;
            gMapControl1.Position = new PointLatLng(38.6, 34);
            gMapControl1.Zoom = 6;
            markerOverlay = new GMapOverlay("markers");

            gMapControl1.Overlays.Add(markerOverlay);
            Routeoverlay = new GMapOverlay("route");
            gMapControl1.Overlays.Add(Routeoverlay);



            gMapControl2.MapProvider = GoogleHybridMapProvider.Instance;
            gMapControl2.Position = new PointLatLng(38.6, 34);
            gMapControl2.Zoom = 6;
            markeroverlay2map = new GMapOverlay("markers2");
            gMapControl2.Overlays.Add(markeroverlay2map);

            gMapControl3.MapProvider = GoogleHybridMapProvider.Instance;
            gMapControl3.Position = new PointLatLng(38.6, 34);
            gMapControl3.Zoom = 6;
            markeroverlay3map = new GMapOverlay("markers3");
            gMapControl3.Overlays.Add(markeroverlay3map);

            gMapControl4.MapProvider = GoogleHybridMapProvider.Instance;
            gMapControl4.Position = new PointLatLng(38.6, 34);
            gMapControl4.Zoom = 6;
            markeroverlay4map = new GMapOverlay("markers4");
            gMapControl4.Overlays.Add(markeroverlay4map);
            Routeoverlay4map = new GMapOverlay("Route4map");
            gMapControl4.Overlays.Add(markeroverlay4map);

            guna2ToggleSwitch1.Checked = true;


        }


        public void add_marker(double lat, double lon, int CustID)
        {
            PointLatLng marker_point = new PointLatLng(lat, lon);
            GMarkerGoogle marker = new GMarkerGoogle(marker_point, GMarkerGoogleType.lightblue_dot);
            marker.ToolTip = new GMapToolTip(marker);
            marker.ToolTipText = Convert.ToString(CustID);
            marker.ToolTipMode = MarkerTooltipMode.OnMouseOver;
            markerOverlay.Markers.Add(marker);
        }
        public void add_marker_2th(double lat, double lon, int CustID)
        {
            PointLatLng marker_point = new PointLatLng(lat, lon);
            GMarkerGoogle marker = new GMarkerGoogle(marker_point, GMarkerGoogleType.lightblue_dot);
            marker.ToolTip = new GMapToolTip(marker);
            marker.ToolTipText = Convert.ToString(CustID);
            marker.ToolTipMode = MarkerTooltipMode.OnMouseOver;
            markeroverlay2map.Markers.Add(marker);
        }
        public void add_marker_3th(double lat, double lon, int CustID)
        {
            PointLatLng marker_point = new PointLatLng(lat, lon);
            GMarkerGoogle marker = new GMarkerGoogle(marker_point, GMarkerGoogleType.lightblue_dot);
            marker.ToolTip = new GMapToolTip(marker);
            marker.ToolTipText = Convert.ToString(CustID);
            marker.ToolTipMode = MarkerTooltipMode.OnMouseOver;
            markeroverlay3map.Markers.Add(marker);
        }
        public void add_marker_4th(double lat, double lon, int CustID)
        {
            PointLatLng marker_point = new PointLatLng(lat, lon);
            GMarkerGoogle marker = new GMarkerGoogle(marker_point, GMarkerGoogleType.lightblue_dot);
            marker.ToolTip = new GMapToolTip(marker);
            marker.ToolTipText = Convert.ToString(CustID);
            marker.ToolTipMode = MarkerTooltipMode.OnMouseOver;
            markeroverlay4map.Markers.Add(marker);
        }

        public void guna2CircleButton3_Click(object sender, EventArgs e)
        {
            //conn = new SQLiteConnection(@"Data Source=C:\Users\Rıfat DEMİROK\Downloads\Boundaries.s3db;");
            //string Cust_sql_comm = "";
            //SQLiteCommand get_customer = new SQLiteCommand(Cust_sql_comm, conn);
            //***********************************************************************************************************************
            city_code00 = guna2TextBox4.Text;
            string command_sql = "SELECT custID, city_kod,Lat,Lng FROM customer WHERE city_kod = " + "\"" + city_code00 + "\" ";
            string conn_text = @"Data Source= C:\Users\Rıfat DEMİROK\Downloads\Boundaries.s3db;";
            SQLiteConnection Show_marker_conn = new SQLiteConnection(conn_text);

            SQLiteCommand get_marker = new SQLiteCommand(command_sql, Show_marker_conn);

            Show_marker_conn.Open();
            SQLiteDataReader Take_Marker = get_marker.ExecuteReader();

            Lats00 = new List<double>(0);
            custIDs00 = new List<int>(0);
            Lngs00 = new List<double>(0);

            while (Take_Marker.Read())
            {
                //Lats = new List<double>(0);
                int CustIDs = Convert.ToInt32(Take_Marker["CustID"]);
                double Lat = Convert.ToDouble(Take_Marker["Lat"]);
                double Lng = Convert.ToDouble(Take_Marker["Lng"]);

                Lats00.Add(Lat);
                Lngs00.Add(Lng);
                custIDs00.Add(CustIDs);

            }
            lnght = Lats00.Count;

            Show_marker_conn.Close();
            for (int i = 0; i < lnght; i++)
            {
                add_marker(Lats00[i], Lngs00[i], custIDs00[i]);
            }

        }


        private void gMapControl2_MouseDoubleClick(object sender, MouseEventArgs e)
        {

            if (e.Button != MouseButtons.Left)
            {
                return;
            }

            double lat1 = gMapControl2.FromLocalToLatLng(e.X, e.Y).Lat;
            double lon1 = gMapControl2.FromLocalToLatLng(e.X, e.Y).Lng;

            random = new Random();
            int cust_ıd = random.Next(10000, 100000);
            string city_kod = guna2TextBox3.Text;

            if (guna2TextBox3.Text == "")
            {
                guna2TextBox3.Show();
                MessageBox.Show("Ekleyeceğiniz Müşterinin Posta Kodunu Giriniz");
                return;
            }
            else if (guna2TextBox3.Text != "")
            {
                add_marker_2th(lat1, lon1, cust_ıd);
                cust_add.add_customer(lat1, lon1, cust_ıd, city_kod);
            }
        }
        private void guna2Button1_Click(object sender, EventArgs e)
        {
            if (guna2TextBox3.Text == "")
            {
                guna2TextBox3.Show();
                MessageBox.Show("Ekleyeceğiniz Müşterinin Posta Kodunu Giriniz");
                return;
            }
            else if (guna2TextBox3.Text != "")
            {
                random = new Random();
                int cust_id = random.Next(10000, 100000);
                double lat = Convert.ToDouble(guna2TextBox1.Text);
                double lon = Convert.ToDouble(guna2TextBox2.Text);
                string city_kod = guna2TextBox3.Text;
                cust_add.add_customer(lat, lon, cust_id, city_kod);
                add_marker_2th(lat, lon, cust_id);
            }

        }

        private void guna2Button2_Click(object sender, EventArgs e)
        {
            guna2TextBox1.Text = string.Empty;
            guna2TextBox3.Text = string.Empty;
            guna2TextBox2.Text = string.Empty;
        }

        private void guna2CircleButton2_Click(object sender, EventArgs e)
        {
            if (tabControl1.SelectedTab != tabControl1.TabPages["Add New Customer"])
            {
                markeroverlay2map.Markers.Clear();
            }
            if (tabControl1.SelectedTab != tabControl1.TabPages["View Customer"])
            {
                markerOverlay.Markers.Clear();
                Routeoverlay.Routes.Clear();
            }
            if (tabControl1.SelectedTab != tabControl1.TabPages["Delete Customer"])
            {
                markeroverlay3map.Markers.Clear();
            }
            if (tabControl1.SelectedTab != tabControl1.TabPages["Selecting Route"])
            {
                markeroverlay4map.Markers.Clear();
            }

        }
        private void guna2ToggleSwitch1_CheckedChanged(object sender, EventArgs e)
        {
            if (guna2ToggleSwitch1.Checked == false)
            {
                if (tabControl1.SelectedTab != tabControl1.TabPages["Add New Customer"])
                {
                    gMapControl2.MarkersEnabled = false;
                }
                if (tabControl1.SelectedTab != tabControl1.TabPages["View Customer"])
                {
                    gMapControl1.MarkersEnabled = false;
                }
            }

            if (guna2ToggleSwitch1.Checked == true)
            {
                if (tabControl1.SelectedTab != tabControl1.TabPages["Add New Customer"])
                {
                    gMapControl2.MarkersEnabled = true;
                }
                if (tabControl1.SelectedTab != tabControl1.TabPages["View Customer"])
                {
                    gMapControl1.MarkersEnabled = true;
                }
            }
        }

        private void guna2Button4_Click(object sender, EventArgs e)
        {
            //**************************************************************************************************************
            string city_code = cıtykod_txb.Text;
            string command_sql = "SELECT custID, city_kod,Lat,Lng FROM customer WHERE city_kod = " + "\"" + city_code + "\" ";
            string conn_text = @"Data Source= C:\Users\Rıfat DEMİROK\Downloads\Boundaries.s3db;";
            SQLiteConnection Show_marker_conn = new SQLiteConnection(conn_text);

            SQLiteCommand get_marker = new SQLiteCommand(command_sql, Show_marker_conn);

            Show_marker_conn.Open();
            SQLiteDataReader Take_Marker = get_marker.ExecuteReader();

            List<double> lats1 = new List<double>(0);
            List<double> Lngs1 = new List<double>(0);
            List<int> custIDs1 = new List<int>(0);

            while (Take_Marker.Read())
            {
                int CustIDs = Convert.ToInt32(Take_Marker["CustID"]);
                double Lat = Convert.ToDouble(Take_Marker["Lat"]);
                double Lng = Convert.ToDouble(Take_Marker["Lng"]);

                lats1.Add(Lat);
                Lngs1.Add(Lng);
                custIDs1.Add(CustIDs);

            }
            int node_count0 = lats1.Count;

            Show_marker_conn.Close();
            for (int i = 0; i < node_count0; i++)
            {
                add_marker_3th(lats1[i], Lngs1[i], custIDs1[i]);
            }

        }

        private void guna2Button3_Click(object sender, EventArgs e)
        {
            int cust_id = Convert.ToInt32(custID_txb.Text);
            string sql_command = "DELETE FROM customer WHERE custID = " + cust_id;
            string conn_text = @"Data Source= C:\Users\Rıfat DEMİROK\Downloads\Boundaries.s3db;";
            SQLiteConnection delete_marker_conn = new SQLiteConnection(conn_text);
            SQLiteCommand del_mar = new SQLiteCommand(sql_command, delete_marker_conn);
            delete_marker_conn.Open();
            SqlDataReader delete_get_cust = (SqlDataReader)del_mar.ExecuteScalar();
            delete_marker_conn.Close();
        }

        public List<int> Route;
        public List<int> BestRoute;
        public double maliyet = 0;
        public double EnİyiMaliyet = double.MaxValue;
        problem Prob;

        public void obsolution()
        {

            int GüncelMüş, SıradakiMüş = 0;

            Route = new List<int>(0);
            BestRoute = new List<int>(0);

            for (int i = 0; i < lnght; i++)
            {
                Route.Clear();
                maliyet = 0;

                GüncelMüş = i;
                Route.Add(GüncelMüş);

                do
                {
                    SıradakiMüş = YakındakiMüşBul(GüncelMüş);
                    Route.Add(SıradakiMüş);
                    GüncelMüş = SıradakiMüş;
                } while (Route.Count != lnght);

                int FirstCust = Route.First();
                int LastCust = Route.Last();
                maliyet += distMat[LastCust, FirstCust];

                if (maliyet < EnİyiMaliyet)
                {
                    EnİyiMaliyet = maliyet;
                    BestRoute = Route.ToList();
                }
            }

        }

        public int YakındakiMüşBul(int GüncelMüş)
        {
            Prob = new problem();

            int SıradakiMüş = 0;
            double MinVal = double.MaxValue;
            for (int i = 0; i < lnght; i++)
            {
                int Position = Route.IndexOf(i);
                if (Position == -1 && GüncelMüş != i && distMat[GüncelMüş, i] < MinVal)
                {
                    MinVal = distMat[GüncelMüş, i];
                    SıradakiMüş = i;
                }
            }
            maliyet += MinVal;
            return SıradakiMüş;

        }
        public double[,] distMat;

        public void EvaluateDistMat()
        {
            //form2 = new Form2();

            distMat = new double[lnght, lnght];

            for (int i = 0; i < lnght; i++)
            {
                for (int j = (i + 1); j < lnght; j++)
                {
                    GeoCoordinate P1 = new GeoCoordinate(Lats00[i], Lngs00[i]);
                    GeoCoordinate P2 = new GeoCoordinate(Lats00[j], Lngs00[j]);


                    distMat[i, j] = P1.GetDistanceTo(P2) / 1000;
                    distMat[j, i] = distMat[i, j];
                }
            }
        }
        private void route_bt_Click(object sender, EventArgs e)
        {
            Sol = new solution();

            List<PointLatLng> RoutePoints = new List<PointLatLng>(0);
            EvaluateDistMat();
            obsolution();
            for (int i = 0; i < BestRoute.Count; i++)
            {
                int CurNode = BestRoute[i];
                double Lat = Lats00[CurNode];
                double Lng = Lngs00[CurNode];
                PointLatLng CurPoint = new PointLatLng(Lat, Lng);
                RoutePoints.Add(CurPoint);

            }
            PointLatLng StartPoint = RoutePoints.First();//**********
            RoutePoints.Add(StartPoint);

            GMapRoute TSProute = new GMapRoute(RoutePoints, "TSP Solution");
            TSProute.Stroke.Width = 2;
            TSProute.Stroke.Color = Color.Black;
            TSProute.Stroke.DashStyle = System.Drawing.Drawing2D.DashStyle.Solid;
            Routeoverlay.Routes.Add(TSProute);



        }

        private void guna2CircleButton2_Click_1(object sender, EventArgs e)
        {
            double no = random.Next(10000, 100000);

            MessageBox.Show("Rota Maliyeti : " + maliyet, "Maliyet", MessageBoxButtons.YesNoCancel);
            string city_code2 = routecitycode.Text;

            string addcustomer = "INSERT INTO route_cost(\n";
            addcustomer += "route_no,\n";
            addcustomer += "city_code,\n";
            addcustomer += "route_cost)\n";
            addcustomer += "VALUES(\n";
            addcustomer += "\t" + no.ToString() + ",\n";
            addcustomer += "\t" + city_code2.ToString() + ",\n";
            addcustomer += "\t" + maliyet.ToString() + "\n";
            addcustomer += ")";



            conn = new SQLiteConnection(@"Data Source=C:\Users\Rıfat DEMİROK\Downloads\Boundaries.s3db;");
            SQLiteCommand add_rota = new SQLiteCommand(addcustomer, conn);
            conn.Open();
            add_rota.ExecuteNonQuery();
            conn.Close();
        }
        public void show_data(string data)
        {
            string conn = @"Data Source= C:\Users\Rıfat DEMİROK\Downloads\Boundaries.s3db;";
            SQLiteDataAdapter dataAdapter = new SQLiteDataAdapter(data, conn);
            DataSet dataset = new DataSet();
            dataAdapter.Fill(dataset);

            dataGridView1.DataSource = dataset.Tables[0];
        }

        private void guna2TextBox4_TextChanged(object sender, EventArgs e)
        {

        }

        private void guna2CircleButton4_Click(object sender, EventArgs e)
        {
            string conn = @"Data Source= C:\Users\Rıfat DEMİROK\Downloads\Boundaries.s3db;";
            SQLiteConnection connection = new SQLiteConnection(conn);
            show_data("select * from customer");
        }

        private void label8_Click(object sender, EventArgs e)
        {

        }

        private void guna2Button5_Click(object sender, EventArgs e)
        {

            string conn = @"Data Source= C:\Users\Rıfat DEMİROK\Downloads\Boundaries.s3db;";
            SQLiteConnection connection = new SQLiteConnection(conn);
            //  show_data("select * from customer where ");
            int cust_id = Convert.ToInt32(guna2TextBox5.Text);
            string sql_command = "select * FROM customer WHERE custID = " + cust_id;
            SQLiteDataAdapter dataAdapter = new SQLiteDataAdapter(sql_command, conn);
            DataSet dataset = new DataSet();
            dataAdapter.Fill(dataset);

            dataGridView1.DataSource = dataset.Tables[0];
            if (guna2TextBox5.Text == "000")
            {
                dataset.Clear();
            }

        }



        private void FLTR_BT_Click_1(object sender, EventArgs e)
        {
            string conn = @"Data Source= C:\Users\Rıfat DEMİROK\Downloads\Boundaries.s3db;";
            SQLiteConnection connection = new SQLiteConnection(conn);
            //  show_data("select * from customer where ");
            int cust_id = Convert.ToInt32(guna2TextBox6.Text);
            string sql_command = "select * FROM customer WHERE city_kod = " + cust_id;
            SQLiteDataAdapter dataAdapter = new SQLiteDataAdapter(sql_command, conn);
            DataSet dataset = new DataSet();
            dataAdapter.Fill(dataset);

            dataGridView1.DataSource = dataset.Tables[0];
        }

        private void gMapControl4_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Left)
            {
                return;
            }

            double lat1 = gMapControl4.FromLocalToLatLng(e.X, e.Y).Lat;
            double lon1 = gMapControl4.FromLocalToLatLng(e.X, e.Y).Lng;

            random = new Random();
            int cust_ıd = random.Next(10000, 100000);
            string city_kod = slR_txbox_citykod.Text;

            if (slR_txbox_citykod.Text == "")
            {
                guna2TextBox3.Show();
                MessageBox.Show("Ekleyeceğiniz Müşterinin Posta Kodunu Giriniz");
                return;
            }
            else if (slR_txbox_citykod.Text != "")
            {
                add_marker_4th(lat1, lon1, cust_ıd);
                cust_add.add_customer(lat1, lon1, cust_ıd, city_kod);
            }
        }

        private void slR_add_bt_Click(object sender, EventArgs e)
        {
            if (slR_txbox_citykod.Text == "")
            {
                guna2TextBox3.Show();
                MessageBox.Show("Ekleyeceğiniz Müşterinin Posta Kodunu Giriniz");
                return;
            }
            else if (slR_txbox_citykod.Text != "")
            {
                random = new Random();
                int cust_id = random.Next(10000, 100000);
                double lat = Convert.ToDouble(slR_txbox_lat.Text);
                double lon = Convert.ToDouble(slR_txbox_lon.Text);
                string city_kod = slR_txbox_citykod.Text;
                cust_add.add_customer(lat, lon, cust_id, city_kod);
                add_marker_4th(lat, lon, cust_id);
            }
        }
        //*****************************************************************************************************************************



        public void slR_clear_bt_Click(object sender, EventArgs e)
        {
            List<PointLatLng> select_route_point = new List<PointLatLng>();
            LatLng_list = new List<string>(0);
            Lat_list = new List<double>(0);
            Lng_list = new List<double>(0);
            char[] coor_split = { ':' };
            slt_cust = new List<string>(0);

            checkedListBox1.Items[0] = "38.0413654412813:32.5121927261353";
            checkedListBox1.Items[1] = "38.0367686780658:32.5145101547241";
            checkedListBox1.Items[2] = "38.0331181017353:32.5106906890869";
            checkedListBox1.Items[3] = "38.03791789592:32.505841255188";

            c_box_lnght = checkedListBox1.CheckedItems.Count;
            for (int i = 0; i < c_box_lnght; i++)
            {
                slt_cust.Add((string)checkedListBox1.CheckedItems[i]);
            }
            for (int i = 0; i < c_box_lnght; i++)
            {
                string[] LatLng_split = slt_cust[i].Split(coor_split);
                for (int j = 0; j < LatLng_split.Length; j++)
                {
                    LatLng_list.Add(LatLng_split[j]);
                }
            }
            int latlng_count = LatLng_list.Count;

            for (int i = 0; i < latlng_count; i += 2)
            {
                Lat_list.Add(Convert.ToDouble(LatLng_list[i]));
            }
            for (int i = 1; i < latlng_count; i += 2)
            {
                Lng_list.Add(Convert.ToDouble(LatLng_list[i]));
            }
            for (int i = 0; i < c_box_lnght; i++)
            {
                select_route_point.Add(new PointLatLng(Lat_list[i], Lng_list[i]));
            }


            //PointLatLng marker_point = new PointLatLng(lat, lon);
            for (int i = 0; i < c_box_lnght; i++)
            {
                GMarkerGoogle marker = new GMarkerGoogle(select_route_point[i], GMarkerGoogleType.lightblue_dot);
                marker.ToolTip = new GMapToolTip(marker);
                //  marker.ToolTipText = Convert.ToString([i]);
                marker.ToolTipMode = MarkerTooltipMode.OnMouseOver;
                markeroverlay4map.Markers.Add(marker);
            }


        }

        private void button1_Click(object sender, EventArgs e)
        {
            RoutePoints4 = new List<PointLatLng>(0);
            EvaluateDistMat4();
            ObtainSolution4();
            for (int i = 0; i < BestRoute4.Count; i++)
            {
                int CurNode = BestRoute4[i];
                double Lat = Lat_list[CurNode];
                double Lng = Lng_list[CurNode];
                PointLatLng CurPoint = new PointLatLng(Lat, Lng);
                RoutePoints4.Add(CurPoint);
            }
            //****************************************************************HATA***************|||||||||||||||||2. KEZ DÖNDÜĞÜNDE NULL DÖNÜYOR
            PointLatLng StartPoint = RoutePoints4.First();
            RoutePoints4.Add(StartPoint);
            //*****************************************************************HATA***************

            GMapRoute TSPRoute = new GMapRoute(RoutePoints4, "TSP Solution");
            TSPRoute.Stroke.Color = Color.Red;
            TSPRoute.Stroke.DashStyle = DashStyle.Solid;
            TSPRoute.Stroke.Width = 2;
            Routeoverlay4map.Routes.Add(TSPRoute);
        }
        public void EvaluateDistMat4()
        {

            NodeNum = c_box_lnght;
            DistMat4 = new double[NodeNum, NodeNum];
            for (int i = 0; i < NodeNum; i++)
            {
                for (int j = i + 1; j < NodeNum; j++)
                {
                    GeoCoordinate P1 = new GeoCoordinate(Lat_list[i], Lng_list[i]);
                    GeoCoordinate P2 = new GeoCoordinate(Lat_list[j], Lng_list[j]);
                    DistMat4[i, j] = P1.GetDistanceTo(P2) / 1000;
                    DistMat4[j, i] = DistMat4[i, j];
                }
            }
        }
        public void ObtainSolution4()
        {
            int CurNode, NextNode = 0;

            Route4 = new List<int>(0);
            BestRoute4 = new List<int>(0);


            for (int i = 0; i < NodeNum; i++)
            {

                Route4.Clear();
                Cost4 = 0;

                CurNode = i;
                Route4.Add(CurNode);


                do
                {
                    NextNode = NearestNode4(CurNode);
                    Route4.Add(NextNode);
                    CurNode = NextNode;
                } while (Route4.Count != NodeNum);


                int FirstCust = Route4.First();
                int LastCust = Route4.Last();
                Cost4 += DistMat4[LastCust, FirstCust];


                if (Cost4 < BestCost4)
                {
                    BestCost4 = Cost4;
                    BestRoute4 = Route4.ToList();
                }
            }
        }
        public int NearestNode4(int CurNode)
        {
            int NextNode = 0;

            double MinVal = double.MaxValue;
            for (int i = 0; i < NodeNum; i++)
            {
                int Position = Route4.IndexOf(i);
                if (Position == -1 && CurNode != i && DistMat4[CurNode, i] < MinVal)
                {
                    MinVal = DistMat4[CurNode, i];
                    NextNode = i;
                }
            }
            Cost4 += MinVal;
            return NextNode;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Routeoverlay4map.Routes.Clear();
            List<PointLatLng> select_route_point = new List<PointLatLng>(0);
            LatLng_list = new List<string>(0);
            Lat_list = new List<double>(0);
            Lng_list = new List<double>(0);
            List<string> slt_cust = new List<string>(0);
            // RoutePoints = new List<PointLatLng>(0);
            RoutePoints4.Clear();
            Route4 = new List<int>(0);
            BestRoute4 = new List<int>(0);
        }
    }
}
