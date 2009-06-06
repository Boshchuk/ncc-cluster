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
        Control ControlObject;
        List<System.Windows.Forms.TextBox> DynTextBoxesL = new List<TextBox>();
        List<System.Windows.Forms.TextBox> DynTextBoxesR = new List<TextBox>();
        List<Matrix> PublicInterval = new List<Matrix>();


        int peremencount;
        public Form1()
        {
            InitializeComponent();           
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Cooficient answer = new Cooficient(textBoxAns.Text);

            int n = int.Parse(textBoxNumber.Text);


            Matrix test = new Matrix(2,n);


            peremencount = int.Parse( textBoxPeremen.Text);
            Matrix ObobIntervals = new Matrix(2,peremencount);

            makeTextBox(peremencount);
          
            
            for (int i = 0; i < peremencount; i++)
            {
                Matrix M = new Matrix(1, 4);
                PublicInterval.Add(M);
            }
        }

        /// <summary>
        /// добавляет пару текстбоксов , что бы ввести обобщенные интервалы
        /// </summary>
        private void makeTextBox(int parnumber)
        {
            for (int i = 0; i < parnumber; i++)
            {
                TextBox DynTextBox;

                System.Reflection.Assembly asm;
                asm = typeof(Form).Assembly;
                ControlObject = (System.Windows.Forms.Control)asm.CreateInstance(" System.Windows.Forms.TextBox");
                ControlObject.Name = "texboxL"+i.ToString();
                ControlObject.Location = new System.Drawing.Point(30, 130+(i*30));
                this.Controls.Add(ControlObject);
                

                DynTextBox = (System.Windows.Forms.TextBox)ControlObject;
                DynTextBox.Width = 50;
                DynTextBox.Text = "Левая граница";
                DynTextBox.Click += new System.EventHandler(DynCtrl_Event);
                DynTextBoxesL.Add(DynTextBox);
                
            }
            for (int i = 0; i < parnumber; i++)
            {
                TextBox DynTextBox;

                System.Reflection.Assembly asm;
                asm = typeof(Form).Assembly;
                ControlObject = (System.Windows.Forms.Control)asm.CreateInstance(" System.Windows.Forms.TextBox");
                ControlObject.Name = "texboxR" + i.ToString();
                ControlObject.Location = new System.Drawing.Point(100, 130 + (i * 30));
                this.Controls.Add(ControlObject);


                DynTextBox = (System.Windows.Forms.TextBox)ControlObject;
                DynTextBox.Width = 50;
                DynTextBox.Text = "Правая граница";

                DynTextBox.Click += new System.EventHandler(DynCtrl_Event);
                DynTextBoxesL.Add(DynTextBox);                
            }           
        }
        private void DynCtrl_Event(object sender, System.EventArgs e)
        {
            TextBox t = (TextBox)sender;
            t.Text = "";
        }        
    }
}