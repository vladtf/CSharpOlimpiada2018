using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace OlimpiadaCsharp2018.Forms
{
    public partial class Diploma : Form
    {
        public Diploma(int grade)
        {
            InitializeComponent();

            label2.Text += GetAward(grade);
        }

        private string GetAward(int grade)
        {
            if (grade < 5)
            {
                return "diploma de participare.";
            }
            if (grade >= 5 && grade <= 7)
            {
                return "mentiune";
            }
            if(grade == 8)
            {
                return "III";
            }
            if (grade == 9)
            {
                return "II";
            }

            return "I";
        }

    }
}
