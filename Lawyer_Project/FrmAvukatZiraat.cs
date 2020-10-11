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
    public partial class FrmAvukatZiraat : Form
    {
        public FrmAvukatZiraat()
        {
            InitializeComponent();
        }

        sqlbaglantisi bgl = new sqlbaglantisi();
        public string adsoyad;
        public string tc;
        private void FrmAvukatZiraat_Load(object sender, EventArgs e)
        {
            LblAdSoyad.Text = adsoyad;

            SqlCommand komut = new SqlCommand("SELECT AVUKATKASA FROM TBLAVUKAT WHERE AVUKATTC=@P1", bgl.baglanti());
            komut.Parameters.AddWithValue("@p1", tc);
            SqlDataReader dr = komut.ExecuteReader();
            while (dr.Read())
            {
                label1.Text = dr[0].ToString();
            }
        }
    }
}
