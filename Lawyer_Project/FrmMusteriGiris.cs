using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Lawyer_Project
{
    public partial class FrmMusteriGiris : Form
    {
        public FrmMusteriGiris()
        {
            InitializeComponent();
        }

        private void FrmMusteriGiris_Load(object sender, EventArgs e)
        {
            GraphicsPath gpath = new GraphicsPath();
            Point[] pnt = new Point[8];
            pnt[0] = new Point(50, 0);
            pnt[1] = new Point(0, 60);
            pnt[2] = new Point(20, 200);
            pnt[3] = new Point(0, 300);
            pnt[4] = new Point(40, 800);
            pnt[5] = new Point(600, 800);
            pnt[6] = new Point(600, 250);
            pnt[7] = new Point(1300, 0);


            gpath.AddPolygon(pnt);
            Region bolge = new Region(gpath);
            this.Region = bolge;
        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            FrmAnaForm fr = new FrmAnaForm();
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

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            FrmMusteriKayit fr = new FrmMusteriKayit();
            fr.Show();
        }
        sqlbaglantisi bgl = new sqlbaglantisi();
        private void button1_Click(object sender, EventArgs e)
        {
            SqlCommand komut = new SqlCommand("SELECT * FROM TBLDAVACI WHERE DAVACITC=@P1 AND DAVACISIFRE=@P2",bgl.baglanti());
            komut.Parameters.AddWithValue("@p1", MskTC.Text);
            komut.Parameters.AddWithValue("@p2", TxtMusteriSifre.Text);
            SqlDataReader dr = komut.ExecuteReader();
            if (dr.Read())
            {
                FrmMusteriDetay fr = new FrmMusteriDetay();
                fr.TC = MskTC.Text;
                fr.Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("Hatalı TC & Şifre");
            }
            bgl.baglanti().Close();
        }
    }
}
