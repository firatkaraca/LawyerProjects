﻿using System;
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
    public partial class FrmMesajMusteri : Form
    {
        public FrmMesajMusteri()
        {
            InitializeComponent();
        }
        void listele()
        {
            dataGridView1.RowHeadersVisible = false;
            SqlCommand komut = new SqlCommand("SELECT MESAJID,MESAJKONU FROM TBLMESAJ ORDER BY MESAJID DESC ", bgl.baglanti());
            SqlDataAdapter da = new SqlDataAdapter(komut);
            DataTable dt = new DataTable();
            da.Fill(dt);
            dataGridView1.DataSource = dt;
            dataGridView1.Columns[0].Visible = false;
        }
        private void pictureBox4_MouseHover(object sender, EventArgs e)
        {
            pictureBox4.BackColor = Color.Red;
        }

        private void pictureBox4_MouseLeave(object sender, EventArgs e)
        {
            pictureBox4.BackColor = Color.Transparent;
        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            SqlCommand komut = new SqlCommand("DELETE FROM TBLMESAJ WHERE MESAJID=@P1", bgl.baglanti());
            komut.Parameters.AddWithValue("@P1", textBox1.Text);
            komut.ExecuteNonQuery();
            bgl.baglanti().Close();
            listele();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            SqlCommand komut = new SqlCommand("INSERT INTO TBLMESAJ (MESAJKONU) VALUES (@P1)", bgl.baglanti());
            komut.Parameters.AddWithValue("@P1", richTextBox1.Text);
            komut.ExecuteNonQuery();
            listele();
        }

        public string TC,ADSOYAD;
        sqlbaglantisi bgl = new sqlbaglantisi();


        private void button3_Click(object sender, EventArgs e)
        {
            SqlCommand komut = new SqlCommand("TRUNCATE TABLE TBLMESAJ", bgl.baglanti());
            komut.ExecuteNonQuery();
            bgl.baglanti().Close();
            listele();
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            SqlCommand komut = new SqlCommand("SELECT MESAJID,MESAJKONU FROM TBLMESAJ ", bgl.baglanti());
            SqlDataAdapter da = new SqlDataAdapter(komut);
            DataTable dt = new DataTable();
            da.Fill(dt);
            dataGridView1.DataSource = dt;
            dataGridView1.Columns[0].Width = 70;
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            listele();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                textBox1.Text = dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString();
            }
            catch { }
        }

        private void FrmMesajMusteri_Load(object sender, EventArgs e)
        {
            LblTC.Text = TC;
            LblAdSoyad.Text = ADSOYAD;
            listele();
        }
    }
}
