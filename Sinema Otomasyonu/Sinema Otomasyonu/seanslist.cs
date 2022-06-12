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
    public partial class seanslist : Form
    {
        public seanslist()
        {
            InitializeComponent();
        }
        OleDbConnection baglantim = new OleDbConnection("Provider=Microsoft.Ace.OleDb.12.0;Data Source=data/database.accdb");
        private void seanslist_Load(object sender, EventArgs e)
        {
            try
            {
                baglantim.Open();
                OleDbDataAdapter order_list = new OleDbDataAdapter
                    ("SELECT filmAdi AS[FİLM], salon AS[SALON],tarih AS[TARİH],seans AS[SEANS] FROM seans", baglantim);
                DataSet memory = new DataSet();
                order_list.Fill(memory);
                dataGridView1.DataSource = memory.Tables[0];
                baglantim.Close();
            }
            catch (Exception hatamsj)
            {
                MessageBox.Show(hatamsj.Message, "Sinema Otomasyonu", MessageBoxButtons.OK, MessageBoxIcon.Error);
                baglantim.Close();
            }
        }
    }
}
