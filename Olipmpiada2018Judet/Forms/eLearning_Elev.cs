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

namespace Olipmpiada2018Judet.Forms
{
    public partial class eLearning_Elev : Form
    {
        private List<ItemModel> items;
        public eLearning_Elev()
        {
            InitializeComponent();

            InitializareItemi();

        }

        public void RaspunsCorect()
        {
            label2.Text = ((int.Parse(label2.Text)) + 1).ToString();
        }

        protected override void OnClosed(EventArgs e)
        {
            (Tag as eLearning1918_Start).Visible = true;
        }

        private void InitializareItemi()
        {
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
    }
}
