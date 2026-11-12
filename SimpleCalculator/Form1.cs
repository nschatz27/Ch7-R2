namespace SimpleCalculator
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btnCalculate_Click(object sender, EventArgs e)
        {
            if (IsValidData())
            {
                try
                {
                    decimal operand1 = Convert.ToDecimal(txtOperand1.Text);
                    string operator1 = txtOperator.Text;
                    decimal operand2 = Convert.ToDecimal(txtOperand2.Text);
                    decimal result = Calculate(operand1, operator1, operand2);

                    result = Math.Round(result, 4);
                    txtResult.Text = result.ToString();
                    txtOperand1.Focus();
                }

                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message + "\n\n" +
                    ex.GetType().ToString() + "\n" +
                    ex.StackTrace, "Exception");
                }
            }
        }

            /*catch (FormatException)
            {
                MessageBox.Show(
                    "Invalid numeric format. Please check all entries.",
                    "Entry Error");
            }
            catch (OverflowException)
            {
                MessageBox.Show(
                    "Overflow error. Please enter smaller values.",
                    "Entry Error");
            }
            catch (DivideByZeroException)
            {
                MessageBox.Show(
                    "Divide-by-zero error. Please enter a non-zero value for operand 2.",
                    "Entry Error");
            }*/
        
        private string IsPresent(string value, string name)
        {
            string message = "";
            if (value == "")
            {
                message = name + " is a required field. \n";
            }

            return message;
        }

        private string IsDecimal(string value, string name)
        {
            string message = "";
            if (!Decimal.TryParse(value, out _))
            {
                message = name + " must be a valid decimal value. \n";
            }

            return message;
        }

        private string IsWithinRange(string value, string name, decimal min, decimal max)
        {
            string message = "";
            if (Decimal.TryParse(value, out decimal number))
            {
                if (number < min || number > max)
                {
                    message = name + " must be between " + min + " and " + max + ". \n";
                }
            }
            return message;
        }

        private string IsOperator(string value, string name)
        {
            string message = "";
            if (value != "+" && value != "-" && value != "*" && value != "/")
            {
                message = name + " must be one of the following: +, -, *, /. \n";
            }
            return message;
        }

        private string IsValidOperation(string operand2, string operator1)
        {
            string message = "";
            try
            {
                if (operator1 == "/" && Convert.ToDecimal(operand2) == 0)
                {
                    message = " Cannot divide by zero. \n";
                }
            }
            catch (FormatException)
            {
                message = " Please enter a non-zero value for operand2 \n";
            }
            return message;
        }

        private bool IsValidData()
        {
            bool success = true;
            string errorMessage = "";
            errorMessage += IsDecimal(txtOperand1.Text, "Operand1");
            errorMessage += IsPresent(txtOperand1.Text, "Operand1");
            errorMessage += IsWithinRange(txtOperand1.Text, "Operand1", 0, 1000000);

            errorMessage += IsDecimal(txtOperand2.Text, "Operand2");
            errorMessage += IsPresent(txtOperand2.Text, "Operand2");
            errorMessage += IsWithinRange(txtOperand2.Text, "Operand2", 0, 1000000);

            errorMessage += IsOperator(txtOperator.Text, "Operator");
            errorMessage += IsValidOperation(txtOperand2.Text, txtOperator.Text);

            if (errorMessage != "")
            {
                success = false;
                MessageBox.Show(errorMessage, "Entry Error");
            }
            return success;
        }

        private decimal Calculate(decimal operand1, string operator1, decimal operand2)
        {
            decimal result = 0;
            if (operator1 == "+")
                result = operand1 + operand2;
            else if (operator1 == "-")
                result = operand1 - operand2;
            else if (operator1 == "*")
                result = operand1 * operand2;
            else if (operator1 == "/")
                result = operand1 / operand2;
            return result;
        }

        private void ClearResult(object sender, EventArgs e)
        {
            txtResult.Text = "";
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}