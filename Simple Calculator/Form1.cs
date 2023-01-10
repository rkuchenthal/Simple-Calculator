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
        Double resultValue = 0;
        string operatorClicked;
        bool isOperatorCLicked = false;

        public Form1()
        {
            InitializeComponent();
        }

        private void click_btnNumber(object sender, EventArgs e)
        {
            //puts sender object into a button so we can use the button text 
            Button button = (Button)sender;

            
            //preventing the 0 from being deleted when making decimals <1
            if (resultBox.Text.Equals("0") && button.Text.Equals("."))
            {
                //making sure there already is not a decimal point
                if (!resultBox.Text.Contains("."))
                {
                    resultBox.Text += button.Text;
                }                
                
            }
            //remove initial zero in resultBox
            else if (resultBox.Text == "0")
            {
                //turn existing zero into new number
                resultBox.Text = button.Text;
            }
            else
            {
                //making sure your not adding 2nd decimal to same number
                if (button.Text.Equals(".") && !resultBox.Text.Contains("."))
                {
                    resultBox.Text += button.Text;
                }
                //all btn numbers that arent a decimal
                else if (!button.Text.Equals("."))
                {
                    resultBox.Text += button.Text;
                }
                
            }

            

        }

        private void operator_click(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            operatorClicked = button.Text;
            resultValue = Convert.ToDouble(resultBox.Text);
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            resultBox.Text = "0";
            resultValue = 0;
        }

        private void btnEquals_Click(object sender, EventArgs e)
        {
            switch (operatorClicked)
            {
                case "+":
                    resultBox.Text = (resultValue + Double.Parse(resultBox.Text)).ToString(); break;
                case "-":
                    resultBox.Text = (resultValue + Double.Parse(resultBox.Text)).ToString(); break;
                case "*":
                    resultBox.Text = (resultValue + Double.Parse(resultBox.Text)).ToString(); break;
                case "/":
                    resultBox.Text = (resultValue + Double.Parse(resultBox.Text)).ToString(); break;
                default:
                    break;
            }
        }

        
    }
}
