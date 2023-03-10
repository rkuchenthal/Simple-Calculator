using System;
using System.Collections;
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
        Stack<string> variables = new Stack<string>();
        ArrayList inFixArList = new ArrayList();
        ArrayList postFixArList = new ArrayList();
        bool decimalExists = false;
        string opChecklist = "+-*/";
        int inVar = 0, postVar = 0;
        

        public Form1()
        {
            InitializeComponent();
        }

        private void click_btnNumber(object sender, EventArgs e)
        {
            //puts sender object into a button so we can use the button text 
            Button button = (Button)sender;

            //preventing the 0 from being deleted when making decimals < 1 as the first number
            // and making sure there already is not a decimal point in that number
            if (resultBox.Text.Equals("0") && button.Text.Equals(".") && !resultBox.Text.Contains("."))
            {                
                resultBox.Text += button.Text;
                //postfix = resultBox + button.Text;
                AddToArray("0.", 0);
                decimalExists = true;                                            
            }
            //remove leading for non decimal numbers
            else if (resultBox.Text == "0")
            {
                //turn existing zero into new number
                resultBox.Text = button.Text;
                //postfix = button.Text;
                AddToArray(button.Text, 0);
            }
            //all btn numbers that arent a decimal
            else if (!button.Text.Equals("."))
            {
                resultBox.Text += button.Text;
                //postfix += button.Text;
                AddToArray(button.Text, 0);
            }
            else
            {
                //making sure your not adding 2nd decimal to currrent number
                if (button.Text.Equals(".") && !resultBox.Text.Contains("."))
                {
                    resultBox.Text += button.Text;
                    //postfix += button.Text;
                    AddToArray(button.Text,0);
                    decimalExists = true;
                }
                else if (resultBox.Text.Contains("."))
                {
                    string infix = resultBox.Text;                    
                    int s = infix.LastIndexOf(" ");

                    //checks the decimalExists flag, if false the
                    //current number doesnt have one yet.
                    if (!decimalExists)
                    {
                        if(s == infix.Length - 1)
                        {
                            resultBox.Text += "0" + button.Text;
                            //postfix += "0" + button.Text;
                            AddToArray("0" + button.Text,0);
                        }
                        else
                        {
                            resultBox.Text += button.Text;
                            AddToArray(button.Text,0);
                        }

                        decimalExists = true;

                    }                    
                }
            }                                                                               
        }

        private void operator_click(object sender, EventArgs e)
        { 
            Button button = (Button)sender;
            if(button.Text.Equals("(") || button.Text.Equals(")"))
            {
                //temporary blocker for parenthesis since i havent added code for them yet
            }
            else if (opChecklist.Contains(Convert.ToString(inFixArList[inVar])))
            {
                //this if just makes sure the last element isnt a operand
                //if it is it does nothing, to prevent back to back operands being entered
            }
            else
            {
                //format result box to properly space new operand
                resultBox.Text += " " + button.Text + " ";

                AddToArray(button.Text, 1);

                // flip the decimal flag off, being that the new last element
                // will be  empty and not contain any decimals
                decimalExists = false;
            }          
        }

        private void PostfixConv()
        {
            //translate to postfix            
            for (int i = 0; i <= inVar; i++)
            {
                string element = (string)inFixArList[i];
                int prio = PriorityOperators(element);

                if (prio == 0)
                {//for all numbers
                    postFixArList.Add(element);
                    postVar++;
                }              
                else if (element.Equals("*"))
                {
                    PriorityStack(element, prio);
                }
                else if (element.Equals("/"))
                {
                    PriorityStack(element, prio);
                }
                else if (element.Equals("+"))
                {
                    PriorityStack(element, prio);
                }
                else if (element.Equals("-"))
                {
                    PriorityStack(element, prio);
                }               
            }
            //now combine the stack into array
            for(int i = 0; i<= variables.Count; i++)
            {
                postFixArList.Add(variables.Pop());
                postVar++;
            }
        }
        
        private void btnEquals_Click(object sender, EventArgs e)
        {
            //the user has hit equals without entering a full expression
            if(inFixArList.Count < 3 || inFixArList[inVar].Equals(""))
            {
                
            }
            //user has entered a complete expression
            else
            {
                //convert equation to postfix
                PostfixConv();

                //solve postfix equation
                string sa, sb;
                for (int i = 0; i < postVar; i++)
                {
                    string operand = (string)postFixArList[i];
                    switch (operand)
                    {
                        case "+":
                            sa = variables.Pop();
                            sb = variables.Pop();
                            variables.Push(Convert.ToString(Convert.ToDouble(sa) + Convert.ToDouble(sb)));
                            break;
                        case "-":
                            sa = variables.Pop();
                            sb = variables.Pop();
                            variables.Push(Convert.ToString(Convert.ToDouble(sb) - Convert.ToDouble(sa)));
                            break;
                        case "*":
                            sa = variables.Pop();
                            sb = variables.Pop();
                            variables.Push(Convert.ToString(Convert.ToDouble(sa) * Convert.ToDouble(sb)));
                            break;
                        case "/":
                            sa = variables.Pop();
                            sb = variables.Pop();
                            variables.Push(Convert.ToString(Convert.ToDouble(sb) / Convert.ToDouble(sa)));
                            break;
                        default:
                            variables.Push(operand);
                            break;
                    }

                }
                //the FINAL ANSWER = final pop()
                // format it to only show 4 decimal places since we are working with small display area
                string ans = string.Format("{0:0.####}", Convert.ToDouble(variables.Pop().ToString()));
                //print to result box
                resultBox.Text = ans;

                //clear all lists so we can continue on with answer if we choose
                inFixArList.Clear();
                inVar = 0;
                postFixArList.Clear();
                postVar = 0;

                //add answer to infix array
                int decFlag =0 ;
                if (ans.Contains("."))
                {
                    decFlag = 1;
                }

                AddToArray(resultBox.Text, decFlag);
            }                                  
        }

        private int PriorityOperators(string n)
        {
            if(n.Equals("+") || n.Equals("-")) { return 1; }
            else if (n.Equals("*") || n.Equals("/")) { return 2; }
            else { return 0; }
        }

        private void PriorityStack(string op, int prio)
        {
            //if stack empty push operator onto it
            if(variables.Count == 0)
            {
                variables.Push(op);
            }
            // if stack.peek() has lower precendence that op, we put op on stack
            else if (prio > PriorityOperators(variables.Peek()))
            {
                variables.Push(op);
            }
            // if stack.peek() has higher prec, we pop it and add to postFixArList,
            // then we throw orig values back into PriorityStack(og, prio)
            else
            {
                postFixArList.Add(variables.Pop());
                postVar++;
                PriorityStack(op, prio);
            }
        }

        private void AddToArray(string newVal, int opFlag)
        {
            if(inFixArList.Count > 0 && opFlag != 1)
            {
                string oldVal = (string)inFixArList[inVar]; //grab current value in the last element
                inFixArList[inVar] = oldVal + newVal; //replace old value with old + new value
                
            }
            else if(inFixArList.Count > 0 && opFlag == 1)
            {
                //add new element to list for the operator
                inFixArList.Add(newVal);
                inVar++; //inc +1 for the prior number element being completed
                inVar++; //inc +1 for the new operator being completed
                inFixArList.Add(""); //add new blank element for the next number entry to append to
            }
            else
            {
                inFixArList.Add(newVal);
            }           
        }

        private void btnUndo_Click(object sender, EventArgs e)
        {
            //1.) need to delete last number/operand off of inFixArList
            //2.) then need to update the resultBox.Text to show new equation
            //3.) need to account for it they click undo and there is only 1 var or none aka its still at 0
            //4.) create go back to last equation function
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            resultBox.Text = "0";
            //clear all lists and counters
            inFixArList.Clear();
            postFixArList.Clear();
            postVar= 0;
            inVar= 0;
        }
    }
}




//TODO
//fix continuing on once you have answer/make it so you cant add a number, only symbols direcctly after
// -if entering number need to reset resultBox to new number
// -if entering operand need to add like normal
// add parenthesis
//add more operands
// add backspace button