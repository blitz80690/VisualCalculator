using System;
using System.Windows;
using System.Windows.Controls;

namespace $safeprojectname$
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        double currentValue = 0;
        double newValue = 0;
        double tempValue = 0;
        double tempValue2 = 0;
        double oldValue = 0;
        // to hold input prior to parsing
        string bufferInput;
        // placeholder for the type of operation we will be doing.
        string operationValue = "";
        // need to know if this is the first two numbers input or not.
        bool firstTime = true;
        enum Operation { Add, Subtract, Multiply, Divide, Equals, Start };
        
        public MainWindow()
        {
            InitializeComponent();
            txtOut.Text = currentValue.ToString();
        }

        private void BtnEntry_Click(object sender, RoutedEventArgs e)
        {
            // tell compiler I KNOW I am expecting a button.
            Button numberButton = (Button)sender;
          
            // Get the value from the button and assign it to a string variable.
            string input = numberButton.Content.ToString();
            
            // Add it to a buffer.  This is so we can have numbers over 9 or with decimals.
            bufferInput = bufferInput + input;

            // Show the user what number they pressed.
            txtOut.Text = bufferInput;
            
        }

        private void Calculate(Operation op)
        {

            switch (op.ToString())
            {
                case "Add":

                    if (firstTime == true)
                    {
                        // this is our first time, so add our 2 numbers together
                        newValue = tempValue + tempValue2;
                        
                        // set since the next operation won't be the first time anymore.
                        firstTime = false;
                    }
                    else
                    {
                        // this is our new value to add
                        newValue = tempValue;
                    }
                                     
                    // add our most recent clicked number.
                    currentValue = oldValue + newValue;

                    // Preserve for next click now that we have a new current value.
                    oldValue = currentValue;

                    // reset our temp values for the next operation
                    tempValue = 0;
                    tempValue2 = 0;

                    txtOut.Text = currentValue.ToString();

                    break;

                case "Subtract":

                    if (firstTime == true)
                    {
                        // this is our first time, so add our 2 numbers together
                        currentValue = tempValue2 - tempValue;

                        // set since the next operation won't be the first time anymore.
                        firstTime = false;
                    }
                    else
                    {
                        // this is our new value to subtract
                        newValue = tempValue;
                        
                        // subtract our most recent clicked number.
                        currentValue = oldValue - newValue;
                    }
                         
                    // Preserve for next click now that we have a new current value.
                    oldValue = currentValue;

                    // reset our temp values for the next operation
                    tempValue = 0;
                    tempValue2 = 0;

                    txtOut.Text = currentValue.ToString();

                    break;

                case "Multiply":

                    if (firstTime == true)
                    {
                        // this is our first time, so add our 2 numbers together
                        newValue = tempValue * tempValue2;
                        currentValue = newValue;

                        // set since the next operation won't be the first time anymore.
                        firstTime = false;
                    }
                    else
                    {
                        // this is our new value to subtract
                        newValue = tempValue;

                        // subtract our most recent clicked number.
                        currentValue = oldValue * newValue;
                    }
                         
                    // set this for next potential operation
                    oldValue = currentValue;

                    // reset our temp values for the next operation
                    tempValue = 0;
                    tempValue2 = 0;

                    txtOut.Text = currentValue.ToString();

                    break;

                case "Divide":

                    if (firstTime == true)
                    {

                        // Make sure there isn't a divide by 0
                        if (tempValue != 0 || tempValue2 != 0)
                        {
                            newValue = tempValue2 / tempValue;
                            currentValue = newValue;
                            oldValue = currentValue;
                        }
                        else
                        {
                            currentValue = 0;
                        }
                        
                        // set since the next operation won't be the first time anymore.
                        firstTime = false;
                    }
                    else
                    {
                        // this is our new value to divide
                        newValue = tempValue;

                        txtOut.Text = "oldValue: " + oldValue + "  newValue: " + newValue;
                    

                        if (oldValue != 0 || newValue != 0)
                        {
                            // subtract our most recent clicked number.
                            currentValue = oldValue / newValue;

                            // save currentValue to oldValue for any new operations after this
                            oldValue = currentValue;
                        }
                        else
                        {
                            currentValue = 0;
                        }

                    }

                    txtOut.Text = currentValue.ToString(); 
                    break;

                default:
                    break;
            }                

        }

        // 4 event handlers for operations:
        private void BtnAdd_Click(object sender, RoutedEventArgs e)
        {

            tempValue = Double.Parse(bufferInput);
           
            // preserve 1st clicked number
            tempValue2 = tempValue;

            // clear the number the user sees
            bufferInput = "";
            txtOut.Text = "+";

            // set the type of operation from our enum statement
            operationValue = "Add";

        }

        private void BtnSubtract_Click(object sender, RoutedEventArgs e)
        {

            tempValue = Double.Parse(bufferInput);

            // preserve 1st clicked number
            tempValue2 = tempValue;

            // clear the number the user sees
            bufferInput = "";
            txtOut.Text = "-";

            // set the type of operation from our enum statement
            operationValue = "Subtract";
        }

        private void BtnMultiply_Click(object sender, RoutedEventArgs e)
        {

            tempValue = Double.Parse(bufferInput);

            // preserve 1st clicked number
            tempValue2 = tempValue;

            // clear the number the user sees
            bufferInput = "";
            txtOut.Text = "*";
        
            // set the type of operation from our enum statement
            operationValue = "Multiply";
        }

        private void BtnDivide_Click(object sender, RoutedEventArgs e)
        {

            tempValue = Double.Parse(bufferInput);

            // preserve 1st clicked number
            tempValue2 = tempValue;

            // clear the number the user sees
            bufferInput = "";
            txtOut.Text = "/";
   
            // set the type of operation from our enum statement
            operationValue = "Divide";
        }

        //Clear the current results
        private void BtnClear_Click(object sender, RoutedEventArgs e)
        {
            // re-intiliaze all variables.
            oldValue = 0;
            newValue = 0;
            currentValue = 0;
            tempValue = 0;
            tempValue2 = 0;
            bufferInput = "";
            // reset flag
            firstTime = true;
            txtOut.Text = currentValue.ToString(); 
        }

        //Handle the Equals button
        private void BtnEquals_Click(object sender, RoutedEventArgs e)
        {

            // get 2nd clicked number
            tempValue = Double.Parse(bufferInput);

            string operationType = operationValue.ToString();

            switch (operationType)
            {
                case "Add" :
                    Calculate(Operation.Add);
                    break;

                case "Subtract" :
                    Calculate(Operation.Subtract);
                    break;
        
                case "Multiply" :
                    Calculate(Operation.Multiply);
                    break;

                case "Divide" :
                    Calculate(Operation.Divide);
                    break;

                default:
                    break;
            }      

        }

    }
}
