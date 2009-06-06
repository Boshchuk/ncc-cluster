using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using nccgle_program;

namespace RExpertone
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Cooficient answer = new Cooficient(textBoxAns.Text);

            int n = int.Parse(textBoxNumber.Text);
            Matrix test = new Matrix(2,n);

            //RExpertoneService.Rfrequency
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        
    }
}
