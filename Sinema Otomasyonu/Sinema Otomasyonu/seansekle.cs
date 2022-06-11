using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.IO.Ports;
using System.Data.OleDb;

namespace Sinema_Otomasyonu
{
    public partial class seansekle : Form
    {
        public seansekle()
        {
            InitializeComponent();
        }
        OleDbConnection baglantim = new OleDbConnection("Provider=Microsoft.Ace.OleDb.12.0;Data Source=data/database.accdb");
        private void seanslist()
        {
            comboBox3.Items.Add("Seçiniz...");
            comboBox3.Items.Add("9:00-11:00");
            comboBox3.Items.Add("11:30-13:30");
            comboBox3.Items.Add("14:00-16:00");
            comboBox3.Items.Add("16:30-18:30");
            comboBox3.Items.Add("19:00-21:30");
            comboBox3.Items.Add("22:00-00:00");
            comboBox3.SelectedIndex = 0;
        }
       private void filmlist()
        {
            comboBox1.Items.Add("Seçiniz...");
            try
            {
                baglantim.Open();
                OleDbCommand sorgu = new OleDbCommand("SELECT * FROM film ", baglantim);
                OleDbDataReader kayitokuma = sorgu.ExecuteReader();
                while (kayitokuma.Read())
                {
                    comboBox1.Items.Add(kayitokuma.GetValue(1).ToString());
                }
                baglantim.Close();
                comboBox1.SelectedIndex = 0;
            }
            catch (Exception hatamsj)
            {
                MessageBox.Show(hatamsj.Message, "Sinema Otomasyonu", MessageBoxButtons.OK, MessageBoxIcon.Error);
                baglantim.Close();
            }
        }

        private void salonlist()
        {
            comboBox2.Items.Add("Seçiniz...");
            try
            {
                baglantim.Open();
                OleDbCommand sorgu = new OleDbCommand("SELECT * FROM salon ", baglantim);
                OleDbDataReader kayitokuma = sorgu.ExecuteReader();
                while (kayitokuma.Read())
                {
                    comboBox2.Items.Add(kayitokuma.GetValue(1).ToString());
                }
                baglantim.Close();
                comboBox2.SelectedIndex = 0;
            }
            catch (Exception hatamsj)
            {
                MessageBox.Show(hatamsj.Message, "Sinema Otomasyonu", MessageBoxButtons.OK, MessageBoxIcon.Error);
                baglantim.Close();
            }
        }
        private void seansekle_Load(object sender, EventArgs e)
        {
            filmlist();
            salonlist();
            seanslist();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (comboBox1.SelectedItem.ToString() != "Seçiniz..." && comboBox2.SelectedItem.ToString() != "Seçiniz..." && comboBox3.SelectedItem.ToString() != "Seçiniz..." && dateTimePicker1.Text!="" )
            {
                try
                {
                    baglantim.Open();
                    OleDbCommand salonSave = new OleDbCommand("INSERT INTO seans (filmAdi,salon,tarih,seans) VALUES('" + comboBox1.SelectedItem.ToString() + "','" + comboBox2.SelectedItem.ToString() + "','" + dateTimePicker1.Text + "','" + comboBox3.SelectedItem.ToString() + "')", baglantim);
                    salonSave.ExecuteNonQuery();
                    baglantim.Close();
                    MessageBox.Show("Seans başarı ile kaydedildi !", "Sinema Otomasyonu", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Close();
                }
                catch (Exception msg)
                {

                    MessageBox.Show(msg.Message, "Sinema Otomasyonu", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    baglantim.Close();
                }
            }
            else
            {
                MessageBox.Show("Lütfen Boş Alan Bırakmayınız !", "Sinema Otomasyonu", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
        }
    }
}
