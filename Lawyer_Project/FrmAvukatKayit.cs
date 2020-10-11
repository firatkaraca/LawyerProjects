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
    public partial class FrmAvukatKayit : Form
    {
        public FrmAvukatKayit()
        {
            InitializeComponent();
        }
        sqlbaglantisi bgl = new sqlbaglantisi();

        private void FrmAvukatKayit_Load(object sender, EventArgs e)
        {
            SqlDataAdapter da = new SqlDataAdapter("SELECT * FROM TBLBRANS", bgl.baglanti());
            DataTable dt = new DataTable();
            da.Fill(dt);
            comboBox1.ValueMember = "BRANSID";
            comboBox1.DisplayMember = "BRANSAD";
            comboBox1.DataSource = dt;

        }
        public void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            label8.Text = comboBox1.SelectedValue.ToString();

        }
        private void button1_Click(object sender, EventArgs e)
        {
            if (TxtAd.Text == "" | TxtSoyad.Text == "" | TxtSifre.Text == "" | MskTC.Text=="" | MskTelefon.Text==""  | CmbCinsiyet.Text == "")
            {
                MessageBox.Show("Tüm alanları doldurmak zorunludur!", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                SqlCommand komut = new SqlCommand("INSERT INTO TBLAVUKAT (AVUKATAD,AVUKATSOYAD,AVUKATSIFRE,AVUKATTC,AVUKATTELEFON,AVUKATBRANS,AVUKATCINSIYET) VALUES(@p1,@p2,@p3,@p4,@p5,@p6,@p7)", bgl.baglanti());
                komut.Parameters.AddWithValue("@p1", TxtAd.Text);
                komut.Parameters.AddWithValue("@p2", TxtSoyad.Text);
                komut.Parameters.AddWithValue("@p3", TxtSifre.Text);
                komut.Parameters.AddWithValue("@p4", MskTC.Text);
                komut.Parameters.AddWithValue("@p5", MskTelefon.Text);
                komut.Parameters.AddWithValue("@p6", comboBox1.SelectedValue);
                komut.Parameters.AddWithValue("@p7", CmbCinsiyet.Text);
                komut.ExecuteNonQuery();
                bgl.baglanti().Close();
                MessageBox.Show("Avukat Sisteme Kaydedildi");
            }            
        }    
    }
}
