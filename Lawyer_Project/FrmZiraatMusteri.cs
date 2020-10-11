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
    public partial class FrmZiraatMusteri : Form
    {
        public FrmZiraatMusteri()
        {
            InitializeComponent();
        }
        public string Adsoyad;
        public string tc;

        sqlbaglantisi bgl = new sqlbaglantisi();

        void lbllistele()
        {
            SqlCommand komut = new SqlCommand("SELECT DAVACIKASA FROM TBLDAVACI WHERE DAVACITC=@p1", bgl.baglanti());
            komut.Parameters.AddWithValue("@p1", tc);
            SqlDataReader dr = komut.ExecuteReader();
            while (dr.Read())
            {
                label1.Text = dr[0].ToString();
            }
            bgl.baglanti().Close();
        }

        private void FrmZiraatMusteri_Load(object sender, EventArgs e)
        {
            this.Size = new Size(725, 508);
            LblAdSoyad.Text = Adsoyad;

            lbllistele();

        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Size = new Size(725, 730);
            button2.Visible = false;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Size = new Size(725, 508);
            button2.Visible = true;
        }

        private void button1_Click(object sender, EventArgs e)
        {

            if (textBox1.Text == "")
            {
                MessageBox.Show("Eklemek istediğiniz tutarı girin!", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {

                try
                {
                    int kasatutar = Convert.ToInt32(textBox1.Text);
                    int butce = Convert.ToInt32(label1.Text);
                    int ekle = kasatutar + butce;
                    SqlCommand komut = new SqlCommand("UPDATE TBLDAVACI SET DAVACIKASA=@P1 WHERE DAVACITC=@P2", bgl.baglanti());
                    komut.Parameters.AddWithValue("@p1", ekle);
                    komut.Parameters.AddWithValue("@p2", tc);
                    komut.ExecuteNonQuery();
                    bgl.baglanti().Close();
                    MessageBox.Show("Yükleme işlemi başarılı");

                    lbllistele();

                }
                catch
                {
                    MessageBox.Show("Hatalı veri girişi", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }

        private void pictureBox4_MouseHover(object sender, EventArgs e)
        {
            pictureBox4.BackColor = Color.Yellow;
        }

        private void pictureBox4_MouseLeave(object sender, EventArgs e)
        {
            pictureBox4.BackColor = Color.Transparent;
        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            this.Hide();
        }
    }
}
