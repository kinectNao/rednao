using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NaoTestDev
{
    public partial class Form1 : Form
    {
        ConsoleApplication1.Program main;

        public Form1(ConsoleApplication1.Program p)
        {
          
            InitializeComponent();
            
           
            
            main = p;
           
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
           
            
            main.controlLArm(float.Parse(t1.Text),float.Parse(t2.Text),float.Parse(t3.Text),float.Parse(t4.Text),float.Parse(t5.Text));

        }

      
    }
}
