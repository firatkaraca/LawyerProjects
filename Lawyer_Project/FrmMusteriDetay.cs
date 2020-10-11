using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Lawyer_Project
{
    public partial class FrmMusteriDetay : Form
    {
        public FrmMusteriDetay()
        {
            InitializeComponent();
        }
        sqlbaglantisi bgl = new sqlbaglantisi();
        public string TC;

        public string avukatdava;
        private void FrmMusteriDetay_Load(object sender, EventArgs e)
        {
            button2.Visible = false;

            //Labela adsoyad bilgisi çekme
            LblTC.Text = TC;
            SqlCommand komut = new SqlCommand("SELECT DAVACIAD,DAVACISOYAD FROM TBLDAVACI WHERE DAVACITC='"+TC+"'", bgl.baglanti());
            SqlDataReader dr = komut.ExecuteReader();
            while (dr.Read())
            {
                LblAdSoyad.Text = dr[0] +" "+ dr[1].ToString();
            }
            bgl.baglanti().Close();
            
            //Davaları çekme

            SqlDataAdapter da1 = new SqlDataAdapter("SELECT DAVATUR,DAVAFIYAT FROM TBLDAVA", bgl.baglanti());
            DataTable dt1 = new DataTable();
            da1.Fill(dt1);
            dataGridView1.DataSource = dt1;
            dataGridView1.Columns[1].Width = 107;

            //Dava comboboxa çekme

            SqlDataAdapter da2 = new SqlDataAdapter("SELECT * FROM TBLDAVA", bgl.baglanti());
            DataTable dt2 = new DataTable();
            da2.Fill(dt2);
            comboBox1.ValueMember = "DAVAID";
            comboBox1.DisplayMember = "DAVATUR";
            comboBox1.DataSource = dt2;
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            FrmMusteriBilgiGuncelle fr = new FrmMusteriBilgiGuncelle();
            fr.tc = LblTC.Text;
            fr.Show();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            SqlCommand cmd = new SqlCommand("SELECT * FROM TBLDAVA WHERE DAVATUR=@P1", bgl.baglanti());
            cmd.Parameters.AddWithValue("@p1", comboBox1.Text);
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                label2.Text = dr[2].ToString();
            }
            bgl.baglanti().Close();
        }

        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            FrmMesajMusteri fr = new FrmMesajMusteri();
            fr.TC = LblTC.Text;
            fr.ADSOYAD = LblAdSoyad.Text.ToUpper();
            fr.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {     
            richTextBox1.Text = LblTC.Text + "  TC Kimlik numaralı " + LblAdSoyad.Text + " tarafından " + comboBox1.Text + " açılmak üzere tarafınıza bildirilir...";

            button2.Visible = true;
        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            FrmMusteriGiris fr = new FrmMusteriGiris();
            fr.Show();
            this.Hide();
        }

        private void pictureBox4_MouseHover(object sender, EventArgs e)
        {
            pictureBox4.BackColor = Color.Red;
        }

        private void pictureBox4_MouseLeave(object sender, EventArgs e)
        {
            pictureBox4.BackColor = Color.Transparent;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            
            //Dava gönder
            SqlCommand komut = new SqlCommand("INSERT INTO TBLDOSYALAR (DAVATUR,DAVACIADSOYAD,DURUM,DAVAFIYAT) VALUES (@P1,@P2,0,@p4)", bgl.baglanti());
            komut.Parameters.AddWithValue("@p1", comboBox1.Text);
            komut.Parameters.AddWithValue("@p2", LblAdSoyad.Text);
            komut.Parameters.AddWithValue("@p4", label2.Text);
            komut.ExecuteNonQuery();
            bgl.baglanti().Close();
            MessageBox.Show("Davanız kabul edildiğinde davalar tablosundan görüntüleyebileksiniz...");
        }
        private void linkLabel4_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            FrmZiraatMusteri fr = new FrmZiraatMusteri();
            fr.Adsoyad = LblAdSoyad.Text;
            fr.tc = LblTC.Text;
            fr.Show();
        }
    }
}
