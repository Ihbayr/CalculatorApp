using System;
using System.Windows.Forms;

namespace CalculatorApp
{
    public partial class Form1 : Form
    {
        private double resultValue = 0;
        private string operation = "";
        private bool isOperationPerformed = false;
        private double memoryValue = 0;

        public Form1()
        {
            InitializeComponent();
            AssignEventHandlers();
        }

        private void AssignEventHandlers()
        {
            foreach (Control ctrl in this.Controls)
            {
                if (ctrl is Button btn)
                {
                    string txt = btn.Text;
                    string name = btn.Name;

                    // button1 (7), button7 (6), button8 (*), button18 (+) зэрэг 
                    // Designer дээр аль хэдийн Click event-тэй болсон товчнуудыг 
                    // энд давхар code-оор нэмэхгүй байхаар шалгаж алгасна.
                    if (name == "button1" || name == "button7" || name == "button8" || name == "button18" || name == "button21")
                    {
                        continue;
                    }

                    // Бусад товчнуудыг автоматаар холбох
                    if ((int.TryParse(txt, out int num) && num >= 0 && num <= 9) || txt == ".")
                    {
                        btn.Click += Number_Click;
                    }
                    else if (txt == "+" || txt == "-" || txt == "*" || txt == "/")
                    {
                        btn.Click += Operator_Click;
                    }
                    else if (txt == "=")
                    {
                        btn.Click += Equals_Click;
                    }
                    else if (txt == "C")
                    {
                        btn.Click += C_Click;
                    }
                    else if (txt == "CE")
                    {
                        btn.Click += CE_Click;
                    }
                    else if (txt.ToLower() == "backspace")
                    {
                        btn.Click += Backspace_Click;
                    }
                    else if (txt == "+/-")
                    {
                        btn.Click += PlusMinus_Click;
                    }
                    else if (txt == "sqrt")
                    {
                        btn.Click += Sqrt_Click;
                    }
                    else if (txt == "%")
                    {
                        btn.Click += Percent_Click;
                    }
                    else if (txt == "1/x")
                    {
                        btn.Click += Reciprocal_Click;
                    }
                    else if (txt == "MC")
                    {
                        btn.Click += MC_Click;
                    }
                    else if (txt == "MR")
                    {
                        btn.Click += MR_Click;
                    }
                    else if (txt == "MS")
                    {
                        btn.Click += MS_Click;
                    }
                }
            }
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            // Дэлгэцийн өөрчлөлт
        }

        // --- Event Methods (Designer дээр холбогдсон болон бусад товчны методууд) ---

        private void button1_Click(object sender, EventArgs e) // 7 товч
        {
            Number_Click_Logic("7");
        }

        private void button7_Click(object sender, EventArgs e) // 6 товч
        {
            Number_Click_Logic("6");
        }

        private void button8_Click(object sender, EventArgs e) // * үйлдэл
        {
            Operator_Click_Logic("*");
        }

        private void button18_Click(object sender, EventArgs e) // + үйлдэл
        {
            Operator_Click_Logic("+");
        }

        private void button21_Click(object sender, EventArgs e) // M+
        {
            if (double.TryParse(textBox2.Text, out double val))
            {
                memoryValue += val;
                textBox1.Text = "M+ : " + memoryValue;
            }
        }

        // --- Ерөнхий логик функцууд ---

        private void Number_Click(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            Number_Click_Logic(button.Text);
        }

        private void Number_Click_Logic(string valText)
        {
            if (textBox2.Text == "0" || isOperationPerformed)
                textBox2.Clear();

            isOperationPerformed = false;

            if (valText == ".")
            {
                if (!textBox2.Text.Contains("."))
                    textBox2.Text += valText;
            }
            else
            {
                textBox2.Text += valText;
            }
        }

        private void Operator_Click(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            Operator_Click_Logic(button.Text);
        }

        private void Operator_Click_Logic(string opText)
        {
            if (resultValue != 0 && !isOperationPerformed)
            {
                // Өмнөх үйлдлийг тооцоолохдоо шууд method руу хандах
                CalculateResult();
            }
            else
            {
                double.TryParse(textBox2.Text, out resultValue);
            }

            operation = opText;
            isOperationPerformed = true;
            textBox1.Text = resultValue + " " + operation;
        }

        // Тэнцүү болон дараалсан үйлдэл хийхэд зориулсан туслах метод
        private void CalculateResult()
        {
            double secondValue;
            double.TryParse(textBox2.Text, out secondValue);

            switch (operation)
            {
                case "+":
                    textBox2.Text = (resultValue + secondValue).ToString();
                    break;
                case "-":
                    textBox2.Text = (resultValue - secondValue).ToString();
                    break;
                case "*":
                    textBox2.Text = (resultValue * secondValue).ToString();
                    break;
                case "/":
                    if (secondValue != 0)
                        textBox2.Text = (resultValue / secondValue).ToString();
                    else
                        textBox2.Text = "Zero division error";
                    break;
            }
            double.TryParse(textBox2.Text, out resultValue);
        }

        private void Equals_Click(object sender, EventArgs e)
        {
            double secondValue;
            double.TryParse(textBox2.Text, out secondValue);

            switch (operation)
            {
                case "+":
                    textBox2.Text = (resultValue + secondValue).ToString();
                    break;
                case "-":
                    textBox2.Text = (resultValue - secondValue).ToString();
                    break;
                case "*":
                    textBox2.Text = (resultValue * secondValue).ToString();
                    break;
                case "/":
                    if (secondValue != 0)
                        textBox2.Text = (resultValue / secondValue).ToString();
                    else
                        textBox2.Text = "Zero division error";
                    break;
                default:
                    break;
            }
            double.TryParse(textBox2.Text, out resultValue);
            operation = "";
            textBox1.Text = "";
            isOperationPerformed = true;
        }

        private void C_Click(object sender, EventArgs e)
        {
            textBox2.Text = "0";
            textBox1.Text = "";
            resultValue = 0;
            operation = "";
        }

        private void CE_Click(object sender, EventArgs e)
        {
            textBox2.Text = "0";
        }

        private void Backspace_Click(object sender, EventArgs e)
        {
            if (textBox2.Text.Length > 0)
            {
                textBox2.Text = textBox2.Text.Remove(textBox2.Text.Length - 1, 1);
                if (textBox2.Text == "" || textBox2.Text == "-")
                {
                    textBox2.Text = "0";
                }
            }
        }

        private void PlusMinus_Click(object sender, EventArgs e)
        {
            if (double.TryParse(textBox2.Text, out double val))
            {
                val = -val;
                textBox2.Text = val.ToString();
            }
        }

        private void Sqrt_Click(object sender, EventArgs e)
        {
            if (double.TryParse(textBox2.Text, out double val))
            {
                if (val >= 0)
                {
                    textBox2.Text = Math.Sqrt(val).ToString();
                }
                else
                {
                    textBox2.Text = "Invalid input";
                }
            }
        }

        private void Percent_Click(object sender, EventArgs e)
        {
            if (double.TryParse(textBox2.Text, out double val))
            {
                textBox2.Text = (resultValue * (val / 100)).ToString();
            }
        }

        private void Reciprocal_Click(object sender, EventArgs e)
        {
            if (double.TryParse(textBox2.Text, out double val))
            {
                if (val != 0)
                {
                    textBox2.Text = (1 / val).ToString();
                }
                else
                {
                    textBox2.Text = "Cannot divide by zero";
                }
            }
        }

        private void MC_Click(object sender, EventArgs e)
        {
            memoryValue = 0;
            textBox1.Text = "Memory Cleared";
        }

        private void MR_Click(object sender, EventArgs e)
        {
            textBox2.Text = memoryValue.ToString();
        }

        private void MS_Click(object sender, EventArgs e)
        {
            double.TryParse(textBox2.Text, out memoryValue);
            textBox1.Text = "Stored: " + memoryValue;
        }
    }
}