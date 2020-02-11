using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using OlimpiadaCSharp.Helpers;
using OlimpiadaCSharp.Models;

namespace OlimpiadaCSharp.Forms
{
    public partial class VizualizareExcursie : Form
    {
        private string connectionString;
        private Timer timer = new Timer();
        private List<LocationModel> locations;

        public VizualizareExcursie(string connectionString)
        {
            this.connectionString = connectionString;
            InitializeComponent();

            MaximizeBox = false;
            MinimizeBox = false;

            dateTimePicker1.Value = DateTime.ParseExact("05.19.2017", "MM.dd.yyyy", null);
            dateTimePicker2.Value = DateTime.ParseExact("07.27.2017", "MM.dd.yyyy", null);

            timer.Interval = 2000;
            timer.Tick += new EventHandler(timer_Tick);

            locations = DataAcces.GetLocations();
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            (Tag as Form).Visible = true;
        }

        private void VizualizareExcursie_Load(object sender, EventArgs e)
        {
            string selectStatement = "Select Nume,DataStart,DataStop,Frecventa,Ziua from Planificari p ,Localitati l where p.IDLocalitate = l.IDLocalitate";
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(selectStatement, con))
                {
                    command.CommandType = CommandType.Text;
                    using (SqlDataAdapter dataAdapter = new SqlDataAdapter(command))
                    {
                        using (DataTable table = new DataTable())
                        {
                            dataAdapter.Fill(table);
                            dataGridView1.DataSource = table;
                        }
                    }
                }
            }

        }

        private void button2_Click(object sender, EventArgs e)
        {
            string selectStatement = "Select Nume,DataStart,DataStop,Frecventa,Ziua " +
                                     "from Planificari p ,Localitati l " +
                                     "where p.IDLocalitate = l.IDLocalitate";


            using (SqlConnection con = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(selectStatement, con))
                {
                    DateTime datastart = dateTimePicker1.Value;
                    DateTime datastop = dateTimePicker2.Value;
                    int ziuastart = datastart.Day;
                    int ziuastop = datastop.Day;
                    command.Parameters.AddWithValue("datastart", datastart.ToString());
                    command.Parameters.AddWithValue("datastop", datastop.ToString());
                    command.CommandType = CommandType.Text;
                    using (SqlDataAdapter dataAdapter = new SqlDataAdapter(command))
                    {
                        using (DataTable table = new DataTable())
                        {
                            dataAdapter.Fill(table);

                            DataTable newTable = TableMethods.Fill(table, datastart, datastop);

                            newTable.DefaultView.Sort = "DataStart ASC";
                            newTable.Columns["Nume"].ColumnName = "Localitate";
                            dataGridView2.DataSource = newTable;
                            //dataGridView2.Sort(dataGridView2.Columns["DataStart"], ListSortDirection.Ascending);

                            DataTable itinerariu = new DataTable();
                            itinerariu.Columns.Add("Localitate", typeof(string));
                            itinerariu.Columns.Add("Data", typeof(DateTime));


                            dataGridView3.DataSource = FillVizualizareItinerariu.Fill(newTable, itinerariu);
                            dataGridView3.Columns["Image"].Visible = false;

                            tabControl1.SelectedTab = tabControl1.TabPages[1];
                        }
                    }
                }
            }
        }

        private int i;

        private void button1_Click(object sender, EventArgs e)
        {
            Button button = sender as Button;
            if (button.Text == "Start")
            {
                try
                {
                    List<string> lactionName = ((DataTable)(dataGridView3.DataSource)).AsEnumerable().Select(r => r.Field<string>("Localitate")).ToList();
                    progressBar1.Value = 0;
                    i = 0;
                    progressBar1.Maximum = dataGridView3.Rows.Count;
                    button.Text = "Stop";
                    timer.Start();
                }
                catch (Exception)
                {
                    MessageBox.Show("Excursia inca nu a fost generata!");
                }
            }
            else
            {
                button.Text = "Start";
                timer.Stop();
            }
        }


        void timer_Tick(object sender, EventArgs e)
        {
            if (progressBar1.Value < progressBar1.Maximum - 1)
            {
                label3.Text = (String)dataGridView3.Rows[i].Cells[0].Value;
                label4.Text = ((DateTime)dataGridView3.Rows[i].Cells[1].Value).ToShortDateString();
                string filePath = @"Imagini\" + dataGridView3.Rows[i].Cells[2].Value;
                pictureBox1.Image = new Bitmap(filePath);
                i++;
                progressBar1.Value++;
            }
            else
            {
                progressBar1.Value++;
                button1_Click(button1, null);
            }
        }

        protected override void OnClosed(EventArgs e)
        {
            base.OnClosed(e);
            timer.Stop();
        }

    }
}
