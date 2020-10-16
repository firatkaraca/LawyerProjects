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
            SqlCommand komut2 = new SqlCommand("SELECT DOSYAID,DAVACIADSOYAD,DAVATUR,DAVAFIYAT,DAVACITC,DAVACIID FROM TBLDOSYALAR WHERE DURUM=0", bgl.baglanti());
            SqlDataAdapter da = new SqlDataAdapter(komut2);
            DataTable dt = new DataTable();
            da.Fill(dt);
            dataGridView1.DataSource = dt;
            dataGridView1.Columns[4].Visible = false;
            dataGridView1.Columns[5].Visible = false;
            dataGridView1.Columns[0].Visible = false;
        }
        private void FrmAvukatDetay_Load(object sender, EventArgs e)
        {
            dataGridView1.RowHeadersVisible = false;
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

        ///KABUL ET BUTONUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUU
        string kabul;
        string avukatpara;//Avukatın kasasındaki toplam tutar
        string davapara;//Seçilen davanın tutarı

        string KASADAKIPARA;//Avukatın kasasındaki toplam tutar
        string DAVATCNO;//Datagridin 4.hücresindeki değer

        private void BtnKabulEt_Click(object sender, EventArgs e)
        {
            try
            {
            kabul = "True";

            //Avukat Toplam Kasa------------------
            SqlCommand komut2 = new SqlCommand("SELECT AVUKATKASA FROM TBLAVUKAT WHERE AVUKATTC=@K1", bgl.baglanti());
            komut2.Parameters.AddWithValue("@K1", LblTC.Text);
            SqlDataReader dr = komut2.ExecuteReader();
            while (dr.Read())
            {
                avukatpara = dr[0].ToString();
            }
            bgl.baglanti().Close();


            //Dava Fiyat------------------
            SqlCommand komut4 = new SqlCommand("SELECT DAVAFIYAT FROM TBLDOSYALAR WHERE DOSYAID=@L1", bgl.baglanti());
            komut4.Parameters.AddWithValue("@L1", textBox1.Text);
            SqlDataReader dr4 = komut4.ExecuteReader();
            while (dr4.Read())
            {
                davapara = dr4[0].ToString();
            }
            bgl.baglanti().Close();


            //Davacı Toplam Kasa------------------
            SqlCommand komut7 = new SqlCommand("SELECT TOP 1 DAVACIKASA FROM TBLDAVACI  INNER JOIN TBLDOSYALAR ON TBLDOSYALAR.DAVACIID=TBLDAVACI.DAVACIID where TBLDAVACI.DAVACITC=@A1", bgl.baglanti());
            komut7.Parameters.AddWithValue("@A1", DAVATCNO);
            SqlDataReader dr7 = komut7.ExecuteReader();
            while (dr7.Read())
            {
                KASADAKIPARA = dr7[0].ToString();
            }
            bgl.baglanti().Close();


            //Davayı kabul etme------------------
            SqlCommand komut = new SqlCommand("UPDATE TBLDOSYALAR SET DURUM=@P1,AVUKATADSOYAD=@P2 WHERE DOSYAID=@p3", bgl.baglanti());
            komut.Parameters.AddWithValue("@P1", kabul);

            if (kabul == "True")
            {
                komut.Parameters.AddWithValue("@p2", LblAdSoyad.Text);
            }

            komut.Parameters.AddWithValue("@P3", textBox1.Text);
            komut.ExecuteNonQuery();
            bgl.baglanti().Close();


            //Avukata Para Yatır------------------
            int ap = Convert.ToInt32(avukatpara);
            int dp = Convert.ToInt32(davapara);
            int toplamm = ap + dp;

            SqlCommand komut3 = new SqlCommand("UPDATE TBLAVUKAT SET AVUKATKASA=@Z1 WHERE AVUKATTC=@Z2", bgl.baglanti());
            komut3.Parameters.AddWithValue("@Z1", toplamm);
            komut3.Parameters.AddWithValue("@Z2", LblTC.Text);
            komut3.ExecuteNonQuery();
            bgl.baglanti().Close();


            //Davacıdan Parayı Al------------------
            int kp = Convert.ToInt32(KASADAKIPARA);
            int cikar = kp - dp;

            SqlCommand komut8 = new SqlCommand("UPDATE TBLDAVACI SET DAVACIKASA=@B1 WHERE DAVACITC=@B2", bgl.baglanti());
            komut8.Parameters.AddWithValue("B1", cikar);
            komut8.Parameters.AddWithValue("B2", DAVATCNO);
            komut8.ExecuteNonQuery();
            bgl.baglanti().Close();
            MessageBox.Show("Davayı kabul ettiniz ücret kasaya yatırıldı");
            davalistele();
            }
            catch
            {
                MessageBox.Show("Almak istediğiniz davaya tıklayıp ardından -KABUL ET- butonuna tıklayarak davayı kabul edebilirsiniz :-)", "Bilgilendirme", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
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
            dataGridView1.Enabled = false;
            SqlCommand komut = new SqlCommand("SELECT DAVACIADSOYAD,DAVATUR,DAVAFIYAT FROM TBLDOSYALAR WHERE AVUKATADSOYAD=@P1", bgl.baglanti());
            komut.Parameters.AddWithValue("@P1", LblAdSoyad.Text);
            SqlDataAdapter da = new SqlDataAdapter(komut);
            DataTable dt = new DataTable();
            da.Fill(dt);
            dataGridView1.DataSource = dt;

        }

        private void button2_Click(object sender, EventArgs e)
        {
            dataGridView1.Enabled = true;
            davalistele();
        }

        private void pictureBox4_MouseHover(object sender, EventArgs e)
        {
            pictureBox4.BackColor = Color.Red;
        }

        private void pictureBox4_MouseLeave(object sender, EventArgs e)
        {
            pictureBox4.BackColor = Color.Transparent;
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                textBox1.Text = dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString();
                DAVATCNO = dataGridView1.Rows[e.RowIndex].Cells[4].Value.ToString();//---TC yi aktar
            }
            catch { }
        }
     
        private void button3_Click(object sender, EventArgs e)
        {
            FrmAvukatMesaj fr = new FrmAvukatMesaj();
            fr.adsoyad = LblAdSoyad.Text;
            fr.tc = LblTC.Text;
            fr.Show();

            //-----------------------------------------------------------------------------------------------------------------

            //SqlCommand komut5 = new SqlCommand("SELECT top 1 substring (davacıadsoyad,1,charindex (' ',davacıadsoyad)) as KASA from TBLDOSYALAR", bgl.baglanti());

            //string isim1 = komut5.ExecuteScalar().ToString();

            //SqlCommand komut6 = new SqlCommand("select top 1 DAVACIAD from TBLDAVACI ", bgl.baglanti());
            //string isim2 = komut6.ExecuteScalar().ToString();


            //if (isim1 == isim2 + " ")
            //{
            //    SqlCommand komut7 = new SqlCommand("SELECT DAVACIKASA FROM TBLDAVACI WHERE DAVACIAD=@p1", bgl.baglanti());
            //    komut7.Parameters.AddWithValue("@p1", isim2);
            //    SqlDataReader dr = komut7.ExecuteReader();
            //    while (dr.Read())
            //    {
            //        MessageBox.Show(dr[0].ToString());
            //        label2.Text = dr[0].ToString();
            //    }
            //    bgl.baglanti().Close();
            //}

            //else
            //{
            //    MessageBox.Show("hata");
            //}
        }
    }
}
