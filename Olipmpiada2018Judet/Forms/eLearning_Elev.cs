using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Olipmpiada2018Judet.Models;
using Olipmpiada2018Judet.DataAcces;
using Olipmpiada2018Judet.ItemsControl;
using System.Windows.Forms.DataVisualization.Charting;
using System.Drawing.Printing;

namespace Olipmpiada2018Judet.Forms
{
    public partial class eLearning_Elev : Form
    {
        private Bitmap bitmap;
        private List<ResponModel> raspunsuri = new List<ResponModel>();
        private List<ItemModel> items;
        public UserModel UserLoged { get; set; }
        public eLearning_Elev()
        {
            InitializeComponent();

            button2.Enabled = false;
        }

        public void Raspunde(string raspunsCorect, string raspuns)
        {
            if (raspuns == raspunsCorect)
            {
                label2.Text = ((int.Parse(label2.Text)) + 1).ToString();
            }
            ResponModel newRespons = new ResponModel { RaspunsCorect = raspunsCorect, RaspunsUtilizator = raspuns };
            raspunsuri.Add(newRespons);
        }

        protected override void OnClosed(EventArgs e)
        {
            (Tag as eLearning1918_Start).Visible = true;
        }

        private void InitializareItemi()
        {
            items = new List<ItemModel>();
            List<ItemModel> tempItems = SqlDataAcces.GetAllItems(SqlDataAcces.ConnectionString);

            List<ItemModel> itemTip1 = tempItems.Where(x => x.TipItem == 1).ToList();
            List<ItemModel> itemTip2 = tempItems.Where(x => x.TipItem == 2).ToList();
            List<ItemModel> itemTip3 = tempItems.Where(x => x.TipItem == 3).ToList();
            List<ItemModel> itemTip4 = tempItems.Where(x => x.TipItem == 4).ToList();

            Random r = new Random();
            items.Add(itemTip1[r.Next(0, itemTip1.Count() - 1)]);
            items.Add(itemTip2[r.Next(0, itemTip2.Count() - 1)]);
            items.Add(itemTip3[r.Next(0, itemTip3.Count() - 1)]);
            items.Add(itemTip4[r.Next(0, itemTip4.Count() - 1)]);


            items.Add(itemTip1.Find(x => !items.Contains(x)));
            items.Add(itemTip2.Find(x => !items.Contains(x)));
            items.Add(itemTip3.Find(x => !items.Contains(x)));
            items.Add(itemTip4.Find(x => !items.Contains(x)));
            items.Add(itemTip1.Find(x => !items.Contains(x)));

            foreach (ItemModel item in items)
            {
                CreateNewItemPage(item);
            }

        }

        private void CreateNewItemPage(ItemModel item)
        {
            string index = (tabControl2.TabPages.Count + 1).ToString();
            switch (item.TipItem)
            {
                case 1:
                    ItemTip1 item1 = new ItemTip1
                {
                    RaspunsCorect = item.RaspunscorectItem,
                    NrItem = index,
                    EnuntItem = item.EnuntItem,
                    Tag = this,
                    Dock = DockStyle.Fill
                };
                    TabPage tp1 = new TabPage("Item "+index);
                    tabControl2.TabPages.Add(tp1);
                    tp1.Controls.Add(item1);
                    break;
                case 2:
                    ItemTip2 item2 = new ItemTip2
                    {
                        RaspunsCorect = item.RaspunscorectItem,
                        Var1 = item.Raspuns1Item,
                        Var2 = item.Raspuns2Item,
                        Var3 = item.Raspuns3Item,
                        Var4 = item.Raspuns4Item,
                        NrItem = index,
                        EnuntItem = item.EnuntItem,
                        Tag = this,
                        Dock = DockStyle.Fill
                    };
                    TabPage tp2 = new TabPage("Item " + index);
                    tabControl2.TabPages.Add(tp2);
                    tp2.Controls.Add(item2);
                    break;
                case 3:
                    ItemTip3 item3 = new ItemTip3
                    {
                        RaspunsCorect = item.RaspunscorectItem,
                        Var1 = item.Raspuns1Item,
                        Var2 = item.Raspuns2Item,
                        Var3 = item.Raspuns3Item,
                        Var4 = item.Raspuns4Item,
                        NrItem = index,
                        EnuntItem = item.EnuntItem,
                        Tag = this,
                        Dock = DockStyle.Fill
                    };
                    TabPage tp3 = new TabPage("Item " + index);
                    tabControl2.TabPages.Add(tp3);
                    tp3.Controls.Add(item3);
                    break;
                case 4:
                    ItemTip4 item4 = new ItemTip4
                    {
                        RaspunsCorect = int.Parse(item.RaspunscorectItem),
                        NrItem = index,
                        EnuntItem = item.EnuntItem,
                        Tag = this,
                        Dock = DockStyle.Fill
                    };
                    TabPage tp4 = new TabPage("Item " + index);
                    tabControl2.TabPages.Add(tp4);
                    tp4.Controls.Add(item4);
                    break;


                default:
                    break;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (button1.Text == "Start test")
            {
                button1.Text = "Finializare";
                InitializareItemi();
                button1.Enabled = false;
                button2.Enabled = true;
            }
            else
            {
                button1.Enabled = false;

                SqlDataAcces.SalvareNota(SqlDataAcces.ConnectionString, UserLoged.IDUtilizator, int.Parse(label2.Text));

                RaportForm raport = new RaportForm();

                foreach (ResponModel item in raspunsuri)
                {
                    raport.AddRow(item.RaspunsUtilizator, item.RaspunsCorect);
                }

                raport.ShowDialog();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            tabControl2.SelectedIndex++;
            if (tabControl2.SelectedIndex == 8)
            {
                button2.Enabled = false;
                button1.Enabled = true;
            }
        }

        private void InitializeChart()
        {
            List<MarkModel> note = SqlDataAcces.GetAllMarks(SqlDataAcces.ConnectionString, this.UserLoged);
            note = note.OrderBy(x => x.Data).ToList();

            Series serie = new Series();
            serie.ChartType = SeriesChartType.Line;

            foreach (MarkModel nota in note)
            {
                serie.Points.AddXY(nota.Data, nota.Nota);
            }


            chart1.Series.Clear();
            chart1.Series.Add(serie);

            chart1.Series[0].XValueType = ChartValueType.DateTime;
            chart1.Series[0].Name = "Nota";
            chart1.ChartAreas[0].AxisX.LabelStyle.Format = "dd.MM.yyyy";
            //chart1.ChartAreas[0].AxisX.Interval = 1;
            chart1.ChartAreas[0].AxisX.IntervalType = DateTimeIntervalType.Days;

            DateTime maxDate = note.Max(x => x.Data);
            DateTime minDate = note.Min(x => x.Data);

            chart1.ChartAreas[0].AxisX.Maximum = maxDate.ToOADate();
            chart1.ChartAreas[0].AxisX.Minimum = minDate.ToOADate();


            Series notaMedie = new Series();
            notaMedie.ChartType = SeriesChartType.Line;

            notaMedie.Points.AddXY(minDate, MarkModel.NotaMedie);
            notaMedie.Points.AddXY(maxDate, MarkModel.NotaMedie);
            chart1.Series.Add(notaMedie);
            chart1.Series[1].Name = "Media clasei";
        }

        private void InitializeCarnet()
        {
            List<MarkModel> note = SqlDataAcces.GetAllMarks(SqlDataAcces.ConnectionString, UserLoged);
            dataGridView1.Columns.Clear();

            DataTable newTable = new DataTable();
            newTable.Columns.Add("Nota", typeof(int));
            newTable.Columns.Add("Data", typeof(DateTime));
            foreach (MarkModel item in note)
            {
                DataRow newRow = newTable.NewRow();
                newRow["Nota"] = item.Nota;
                newRow["Data"] = item.Data;
                newTable.Rows.Add(newRow);
            }
            dataGridView1.DataSource = newTable;
        }

        private void tabControl1_Click(object sender, EventArgs e)
        {
            InitializeChart();
            InitializeCarnet();
        }

        protected override void OnLoad(EventArgs e)
        {
            label3.Text = "Carnetul elevului : " + UserLoged.Nume;
        }

        private void button3_Click(object sender, EventArgs e)
        {
        //    PrintPreviewDialog printPreview = new PrintPreviewDialog();
        //    printPreview.ClientSize = new Size(400, 400);
        //    printPreview.DesktopLocation = new Point(30, 30);
        //    printPreview.Name = "Print preview dialog";
        //    printPreview.UseAntiAlias = true;
        //    printPreview.Document = new PrintDocument();

            int height = dataGridView1.Height;

            dataGridView1.Height = dataGridView1.RowCount * dataGridView1.RowTemplate.Height;

            bitmap = new Bitmap(dataGridView1.Width, dataGridView1.Height);
            dataGridView1.DrawToBitmap(bitmap, new Rectangle(0, 0, bitmap.Width, bitmap.Height));

            dataGridView1.Height = height;

            printPreviewDialog1.Document = printDocument1;
            printPreviewDialog1.PrintPreviewControl.Zoom = 1;
            printPreviewDialog1.ShowDialog();
        }

        private void printDocument1_PrintPage(object sender, PrintPageEventArgs e)
        {
            e.Graphics.DrawImage(bitmap, new Point(0, 0));
        }


    }
}
