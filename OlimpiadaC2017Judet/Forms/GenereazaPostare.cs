using OlimpiadaCSharp.Helpers;
using OlimpiadaCSharp.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Windows.Forms;

namespace OlimpiadaCSharp.Forms
{
    public partial class GenereazaPostare : Form
    {
        private string connectionString;
        private List<string> images;
        private List<LocationModel> locations;
        private Bitmap pictureDisplayed;

        public GenereazaPostare(string connectionString)
        {
            InitializeComponent();
            MaximizeBox = false;
            MinimizeBox = false;

            this.connectionString = connectionString;

            locations = DataAcces.GetLocations();
            comboBox1.Items.AddRange(locations.Select(x => x.Localitate).ToArray());
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            (Tag as Form).Visible = true;
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            var selectedLocation = (sender as ComboBox).SelectedItem as string;

            images = DataAcces.GetImages(locations.Find(x => x.Localitate == selectedLocation).Id);

            comboBox2.Items.Clear();
            comboBox2.Items.AddRange(images.ToArray());
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (listView1.Items.Count < 9)
            {
                if (!String.IsNullOrWhiteSpace((comboBox2.SelectedItem as string)))
                {
                    string selectedItemText = (string)comboBox2.SelectedItem;

                    listView1.Items.Add(selectedItemText);

                    string filePath = @"Imagini\" + selectedItemText;

                    Bitmap bmp = new Bitmap(filePath);
                    int newWidth = (600 * bmp.Width) / bmp.Height;
                    Bitmap bmpResized = new Bitmap(bmp, newWidth, 600);

                    tableLayoutPanel1.Controls.Add(new Panel { BackgroundImage = bmpResized, BackgroundImageLayout = ImageLayout.Zoom });

                    int width = newWidth;
                    if (pictureDisplayed != null)
                    {
                        width += pictureDisplayed.Width;
                    }

                    int height = 600;

                    Bitmap newPicture = new Bitmap(width, height);
                    using (Graphics gr = Graphics.FromImage(newPicture))
                    {
                        if (pictureDisplayed != null)
                        {
                            gr.DrawImage(pictureDisplayed, 0, 0);
                            gr.DrawImage(bmpResized, pictureDisplayed.Width, 0);
                        }
                        else
                        {
                            gr.DrawImage(bmpResized, 0, 0);
                        }
                    }

                    pictureDisplayed = newPicture;
                    pictureBox1.Image = pictureDisplayed;
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            SaveFileDialog sfd1 = new SaveFileDialog();
            sfd1.FileName = "fromtablelayout.bmp";
            int width = tableLayoutPanel1.Width;
            int height = tableLayoutPanel1.Height;

            Bitmap newBit = new Bitmap(width, height);

            tableLayoutPanel1.DrawToBitmap(newBit, new Rectangle(0, 0, width, height));

            if (sfd1.ShowDialog() == DialogResult.OK)
            {
                newBit.Save(sfd1.FileName, ImageFormat.Bmp);
            }

            SaveFileDialog sfd = new SaveFileDialog();
            sfd.FileName = textBox1.Text;
            sfd.Filter = "Images|*.png;*.bmp;*.jpg";
            ImageFormat format = ImageFormat.Png;
            if (sfd.ShowDialog() == DialogResult.OK)
            {
                string ext = System.IO.Path.GetExtension(sfd.FileName);
                switch (ext)
                {
                    case ".jpg":
                        format = ImageFormat.Jpeg;
                        break;

                    case ".bmp":
                        format = ImageFormat.Bmp;
                        break;
                }
                pictureBox1.Image.Save(sfd.FileName, format);
            }
        }
    }
}