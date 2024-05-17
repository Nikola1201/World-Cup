using DBAccess;
using Domain;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Server
{
    public partial class FrmServer : Form
    {
        Server server;
        Timer t;
        public FrmServer()
        {
            InitializeComponent();
        }

        private void FrmServer_Load(object sender, EventArgs e)
        {
            server = new Server();
            t=new Timer();
            if (server.StartServer())

            {
                
                this.Text = "Server is running!";
                t.Interval = 10000;
                t.Tick += RefreshServer;
                t.Start();
            }
            string parameter = "";
            dataGridView1.DataSource = Broker.Instance().GetAllPairs(parameter);
        }

        private void RefreshServer(object sender, EventArgs e)
        {
            string parameter = "";
            if (cbDateFilter.Checked)
            {
                try
                {
                    DateTime date = Convert.ToDateTime(txtDate.Text.Trim());
                    string dateString = date.ToString("yyyy-MM-dd");
                    parameter = $"WHERE date = '{dateString}'";
                    dataGridView1.DataSource = Broker.Instance().GetAllPairs(parameter);

                }
                catch (Exception)
                {
                    parameter = "";
                }
            }


            dataGridView1.DataSource = Broker.Instance().GetAllPairs(parameter);
        
        }

        private void FillGrid()
        {
            DataGridViewTextBoxColumn homeTeam = new DataGridViewTextBoxColumn();
            DataGridViewTextBoxColumn awayTeam = new DataGridViewTextBoxColumn();
            DataGridViewTextBoxColumn Date = new DataGridViewTextBoxColumn();

            homeTeam.HeaderText = "Home team";
            homeTeam.DataPropertyName = "HomeTeam";

            awayTeam.HeaderText = "Away team";
            awayTeam.DataPropertyName = "AwayTeam";

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void cbDateFilter_CheckedChanged(object sender, EventArgs e)
        {
        }
    }
}
