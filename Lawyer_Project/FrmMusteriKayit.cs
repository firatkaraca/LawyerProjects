using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace Lawyer_Project
{
    public partial class FrmMusteriKayit : Form
    {
        public FrmMusteriKayit()
        {
            InitializeComponent();
        }
        sqlbaglantisi bgl = new sqlbaglantisi();
        private void button1_Click(object sender, EventArgs e)
        {
            if(TxtAd.Text=="" | TxtSoyad.Text==""| TxtSifre.Text == "" | MskTC.Text == "" | MskTelefon.Text == "" | CmbCinsiyet.Text == "")
            {
                MessageBox.Show("Tüm alanları doldurmak zorunludur!", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                SqlCommand komut = new SqlCommand("INSERT INTO TBLDAVACI (DAVACIAD,DAVACISOYAD,DAVACISIFRE,DAVACITC,DAVACITELEFON,DAVACICINSIYET) values (@p1,@p2,@p3,@p4,@p5,@p6)", bgl.baglanti());
                komut.Parameters.AddWithValue("@p1", TxtAd.Text);
                komut.Parameters.AddWithValue("@p2", TxtSoyad.Text);
                komut.Parameters.AddWithValue("@p3", TxtSifre.Text);
                komut.Parameters.AddWithValue("@p4", MskTC.Text);
                komut.Parameters.AddWithValue("@p5", MskTelefon.Text);
                komut.Parameters.AddWithValue("@p6", CmbCinsiyet.Text);
                komut.ExecuteNonQuery();
                bgl.baglanti().Close();
                MessageBox.Show("Bilgileriniz Başarıyla Kaydedildi");
            }     
        }
    }
}
