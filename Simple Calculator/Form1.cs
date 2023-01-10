using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Simple_Calculator
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void click_btn(object sender, EventArgs e)
        {
            if(resultBox.Text == "0")
            {
                resultBox.Clear();
            }
            //puts sender object into a button so we use the button text to add values to result textbox
            Button button = (Button)sender;
            resultBox.Text = resultBox.Text + button.Text;
        }
    }
}
