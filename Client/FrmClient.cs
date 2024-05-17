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

namespace Client
{
    public partial class FrmClient : Form
    {
        Communication comm;
        BindingList<Pair> pairs;
        public FrmClient()
        {
            InitializeComponent();
        }

        private void FrmClient_Load(object sender, EventArgs e)
        {
            comm = new Communication();
            if (comm.ConnectToServer())
            {
                this.Text = "Match schedule - Client side Connection to server successful!";
                FillGrid();

            }
            else
            {
                this.Text = "Error connecting to server!";
            }


        }
        void FillGrid()
        {
            DataGridViewComboBoxColumn homeTeam = new DataGridViewComboBoxColumn();
            DataGridViewComboBoxColumn awayTeam = new DataGridViewComboBoxColumn();

            homeTeam.Name = "homeTeam";
            homeTeam.HeaderText = "Home Team";
            homeTeam.DataPropertyName = "HomeTeam";
            homeTeam.DataSource = comm.GetAllCountries();
            homeTeam.ValueMember = "Object";
            homeTeam.DisplayMember = "Name";

            awayTeam.Name = "awayTeam";
            awayTeam.HeaderText = "Away Team";
            awayTeam.DataPropertyName = "AwayTeam";
            awayTeam.DataSource = comm.GetAllCountries();
            awayTeam.ValueMember = "Object";
            awayTeam.DisplayMember = "Name";

            dataGridView1.Columns.Add(homeTeam);
            dataGridView1.Columns.Add(awayTeam);

            dataGridView1.AutoGenerateColumns = false;
            pairs = new BindingList<Pair>();
            dataGridView1.DataSource = pairs;

        }
        private void btnAdd_Click(object sender, EventArgs e)
        {
            Pair p = new Pair();
            try
            {

                p.Date = Convert.ToDateTime(txtDate.Text.Trim());
            }
            catch (Exception)
            {
                MessageBox.Show("Please enter a valid date!");
                return;
            }
            pairs.Add(p);
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                Pair p = dataGridView1.CurrentRow.DataBoundItem as Pair;
                pairs.Remove(p);
            }
            catch (Exception)
            {
                MessageBox.Show("Please select a pair!");
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (comm.ExistingSchedule(new List<Pair>(pairs)))
            {
                MessageBox.Show("Date already scheduled!\nSchedule another date!");
                txtDate.Clear();
                pairs.Clear();
                return;
            }
            else
            {
                HashSet<int> ids = new HashSet<int>();

                foreach (Pair pair in pairs)
                {
                    if (pair.HomeTeam.Id == pair.AwayTeam.Id)
                    {
                        MessageBox.Show("A country can't play against itself!");

                    }

                    if (!ids.Add(pair.HomeTeam.Id) || !ids.Add(pair.AwayTeam.Id))
                    {
                        MessageBox.Show("A country can only play once a day");
                        txtDate.Clear();
                        pairs.Clear();
                        return;
                    }
                }

                if (comm.SavePairs(new List<Pair>(pairs)))
                {
                    MessageBox.Show("Schedule saved successfully!");
                }
                else
                {
                    MessageBox.Show("Error!");
                }
            }
        }
    }
}
