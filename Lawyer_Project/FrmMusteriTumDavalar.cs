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
    public partial class FrmMusteriTumDavalar : Form
    {
        public FrmMusteriTumDavalar()
        {
            InitializeComponent();
        }
        public string TC;
        sqlbaglantisi bgl = new sqlbaglantisi();

        void kabuledilen()
        {
            this.Text = "Kabul Edilmiş Davalar";
            String durum = "TRUE";

            SqlCommand komut = new SqlCommand("SELECT DAVATUR,AVUKATADSOYAD,DAVAFIYAT FROM TBLDOSYALAR WHERE DAVACITC=@P1 AND DURUM=@p2", bgl.baglanti());
            komut.Parameters.AddWithValue("P1", TC);
            komut.Parameters.AddWithValue("@P2", durum);
            SqlDataAdapter da = new SqlDataAdapter(komut);
            DataTable dt = new DataTable();
            da.Fill(dt);
            dataGridView1.DataSource = dt;
        }


        public void DatagridviewSetting (DataGridView datagridview)
        {
            datagridview.RowHeadersVisible = false;//İlk sütunu gizleme
            datagridview.BorderStyle = BorderStyle.None;//Borderı sıfırlama
            datagridview.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(0,0,0); //Varsayılan arkaplan rengi verme
            datagridview.DefaultCellStyle.SelectionBackColor = Color.FromArgb(42, 54, 80);//Seçilen hücrenin arkaplan rengi
            datagridview.DefaultCellStyle.SelectionForeColor = Color.White;//Seçilen hücrenin yazı rengi
            datagridview.EnableHeadersVisualStyles = false;//Başlık özelliğini değiştirmeyi etkinleştir
            datagridview.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;//Başlık çizgilerini kaldırma
            datagridview.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(42,54,99);//Başlık arkaplan rengini belirleme
            datagridview.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;//Başlık yazı rengini belirleme
            datagridview.SelectionMode = DataGridViewSelectionMode.FullRowSelect;//Satırı tamamen seçme kodu
            datagridview.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;//En uzun genişliğe göre ayarla---AllCells
            datagridview.ReadOnly = true;
            datagridview.BackgroundColor = Color.FromArgb(42,54,99);
        }


        private void FrmMusteriTumDavalar_Load(object sender, EventArgs e)
        {
            kabuledilen();
            button2.Visible = false;

            DatagridviewSetting(dataGridView1);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            kabuledilen();

            button2.Visible = false;
            button1.Visible = true;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Text = "Beklemede olan davalar";
            String durum = "FALSE";

            SqlCommand komut = new SqlCommand("SELECT DAVATUR,DAVAFIYAT FROM TBLDOSYALAR WHERE DAVACITC=@P1 AND DURUM=@p2", bgl.baglanti());
            komut.Parameters.AddWithValue("P1", TC);
            komut.Parameters.AddWithValue("@P2", durum);
            SqlDataAdapter da = new SqlDataAdapter(komut);
            DataTable dt = new DataTable();
            da.Fill(dt);
            dataGridView1.DataSource = dt;

            button1.Visible = false;
            button2.Visible = true;
        }

    }
}
