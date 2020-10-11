using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Lawyer_Project
{
    public partial class FrmAvukatDetay : Form
    {
        public FrmAvukatDetay()
        {
            InitializeComponent();
        }
        public string tc;
        sqlbaglantisi bgl = new sqlbaglantisi();

        void davalistele()
        {
            //Dava isteklerini çekme

            SqlCommand komut2 = new SqlCommand("SELECT DOSYAID,DAVACIADSOYAD,DAVATUR,DAVAFIYAT FROM TBLDOSYALAR WHERE DURUM=0", bgl.baglanti());
            SqlDataAdapter da = new SqlDataAdapter(komut2);
            DataTable dt = new DataTable();
            da.Fill(dt);
            dataGridView1.DataSource = dt;
        }
        private void FrmAvukatDetay_Load(object sender, EventArgs e)
        {
            LblTC.Text = tc;

            //Adsoyad çekme

            SqlCommand komut = new SqlCommand("SELECT AVUKATAD,AVUKATSOYAD FROM TBLAVUKAT WHERE AVUKATTC=@P1", bgl.baglanti());
            komut.Parameters.AddWithValue("@P1", tc);
            SqlDataReader dr = komut.ExecuteReader();
            while (dr.Read())
            {
                LblAdSoyad.Text = dr[0] + " " + dr[1];
            }
            bgl.baglanti().Close();

            davalistele();



        }

        ///KABUL ET BUTONUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUU
        string kabul;
        string avukatpara;
        string davapara;

        private void button3_Click(object sender, EventArgs e)
        {
            kabul = "True";

            SqlCommand komut2 = new SqlCommand("SELECT AVUKATKASA FROM TBLAVUKAT WHERE AVUKATTC=@K1", bgl.baglanti());
            komut2.Parameters.AddWithValue("@K1", LblTC.Text);
            SqlDataReader dr = komut2.ExecuteReader();
            while (dr.Read())
            {
                avukatpara = dr[0].ToString();
            }
            bgl.baglanti().Close();

            //davapara -------

            SqlCommand komut4 = new SqlCommand("SELECT DAVAFIYAT FROM TBLDOSYALAR WHERE DOSYAID=@L1", bgl.baglanti());
            komut4.Parameters.AddWithValue("@L1", textBox1.Text);
            SqlDataReader dr4 = komut4.ExecuteReader();
            while (dr4.Read())
            {
                davapara = dr4[0].ToString();
            }
            bgl.baglanti().Close();


            SqlCommand komut = new SqlCommand("UPDATE TBLDOSYALAR SET DURUM=@P1,AVUKATADSOYAD=@P2 WHERE DOSYAID=@p3", bgl.baglanti());
            komut.Parameters.AddWithValue("@P1", kabul);

            if (kabul == "True")
            {
                komut.Parameters.AddWithValue("@p2", LblAdSoyad.Text);
            }

            komut.Parameters.AddWithValue("@P3", textBox1.Text);
            komut.ExecuteNonQuery();
            bgl.baglanti().Close();

            //avukata para yatır
            int ap = Convert.ToInt32(avukatpara);
            int dp = Convert.ToInt32(davapara);
            int toplamm = ap + dp;


            SqlCommand komut3 = new SqlCommand("UPDATE TBLAVUKAT SET AVUKATKASA=@Z1 WHERE AVUKATTC=@Z2", bgl.baglanti());
            komut3.Parameters.AddWithValue("@Z1", toplamm);
            komut3.Parameters.AddWithValue("@Z2", LblTC.Text);
            komut3.ExecuteNonQuery();
            bgl.baglanti().Close();
            MessageBox.Show("Davayı kabul ettiniz ücret kasaya yatırıldı");
            davalistele();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            textBox1.Text = dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            FrmAvukatZiraat fr = new FrmAvukatZiraat();
            fr.adsoyad = LblAdSoyad.Text;
            fr.tc = LblTC.Text;
            fr.Show();
        }
        private void pictureBox4_Click(object sender, EventArgs e)
        {
            FrmAvukatGiris fr = new FrmAvukatGiris();
            fr.Show();
            this.Hide();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            SqlCommand komut = new SqlCommand("SELECT DOSYAID,DAVACIADSOYAD,DAVAFIYAT FROM TBLDOSYALAR WHERE AVUKATADSOYAD=@P1", bgl.baglanti());
            komut.Parameters.AddWithValue("@P1", LblAdSoyad.Text);
            SqlDataAdapter da = new SqlDataAdapter(komut);
            DataTable dt = new DataTable();
            da.Fill(dt);
            dataGridView1.DataSource = dt;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            davalistele();
        }
    }
}
