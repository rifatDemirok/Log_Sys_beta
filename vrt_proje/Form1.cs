using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SQLite;
using System.Data.Sql;
using GMap.NET;

namespace vrt_proje
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            SQLiteConnection baglanti = new SQLiteConnection(@"Data Source= C:\Users\Rıfat DEMİROK\Downloads\Boundaries.s3db;");
            Form2 gmap_form = new Form2();
            gmap_form = new Form2();

            string r = textBox2.Text;
            baglanti.Open();
            string sql = "select Ad, Sifre  from parola where Ad = @adi AND Sifre = @sifresi";
            SQLiteCommand komut = new SQLiteCommand(sql, baglanti);
            komut.Parameters.AddWithValue("@adi", textBox1.Text);
            komut.Parameters.AddWithValue("@sifresi", textBox2.Text);
            SQLiteDataReader dr;
            dr = komut.ExecuteReader();

            if (dr.Read())
            {
                Form1 giriş_ekran = new Form1();
                this.Hide();
                gmap_form.Show();
            }
            else
            {
                MessageBox.Show("kullanıcı adı veya şifre hatalı!", "kontrol ediniz", MessageBoxButtons.RetryCancel);
            }

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
