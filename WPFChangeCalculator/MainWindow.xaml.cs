using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WPFChangeCalculator
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        //Initialize Cash Register
        public static decimal RegisterTotalDec = 100;
        public static string RegisterTotalStr = RegisterTotalDec.ToString("C");


        //Grab Users Input
        public static string var_itemPrice;
        public static string var_customersMoney;
        public static decimal ItemPrice;
        public static decimal InputMoney;

        //Math Outputs
        public static decimal change;

        //Assign Varibles to Calculate functions output
        public static string var_numOf50s;
        public static string var_numOf20s;
        public static string var_numOf10s;
        public static string var_numOf5s;
        public static string var_numOf1s;
        public static string var_numOfQuarters;
        public static string var_numOfDimes;
        public static string var_numOfNickesl;
        public static string var_numOfPennies;
        public static string var_changeTotal;

        public static decimal dec_numOf50s;
        public static decimal dec_numOf20s;
        public static decimal dec_numOf10s;
        public static decimal dec_numOf5s;
        public static decimal dec_numOf1s;
        public static decimal dec_numOfQuarters;
        public static decimal dec_numOfDimes;
        public static decimal dec_numOfNickesl;
        public static decimal dec_numOfPennies;
        public static decimal dec_changeTotal;

        //Array of output TextBoxes
        public TextBox[] outputTextBoxes = new TextBox[9];

        public TextBox[] inputTextBoxes = new TextBox[2];

        public MainWindow()
        {
            
            InitializeComponent();
            //populate register total
            RegisterTotal.Content = RegisterTotalStr;

            //Add Output Textboxes to Array
            outputTextBoxes[0] = numOf50;
            outputTextBoxes[1] = numOf20;
            outputTextBoxes[2] = numOf10;
            outputTextBoxes[3] = numOf5;
            outputTextBoxes[4] = numOf1;
            outputTextBoxes[5] = numOfQuarters1;
            outputTextBoxes[6] = numOfDimes;
            outputTextBoxes[7] = numOfNickels1;
            outputTextBoxes[8] = numOfPennies;


            //Add input Textboxes to Array
            inputTextBoxes[0] = itemPrice;
            inputTextBoxes[1] = CustomersMoney;
        }

        private void textBox1_Copy6_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void textBox_Copy_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        public void calcButton_Click(object sender, RoutedEventArgs e)
        {
            //Get Customer Inputs
            var_itemPrice = itemPrice.Text;
            var_customersMoney = CustomersMoney.Text;


            //Ensure some input is present
            if ((var_itemPrice == "") || (var_customersMoney == ""))
            {

                MessageBox.Show("Your need to fill in both item cost and customers money amounts!");
                clearTextboxes();
                return;


            }

            if ((!JeffToolBox.hasLetters(var_itemPrice)) || (!JeffToolBox.hasLetters(var_customersMoney)))
            {

                MessageBox.Show("Please enter a number!");
                clearTextboxes();
                return;

            }

            //Remove Specail Characters
            var_itemPrice = JeffToolBox.RemoveSpecialCharacters(var_itemPrice);
            var_customersMoney = JeffToolBox.RemoveSpecialCharacters(var_customersMoney);


            //parse Customer Inputs to Decimals
            ItemPrice = Decimal.Parse(var_itemPrice);
            InputMoney = Decimal.Parse(var_customersMoney);

           //Valifationt to ensure item price is lower thna what customer paid
            if (ItemPrice > InputMoney)
            {

                MessageBox.Show("Your customer needs to give you more Money!");
                clearTextboxes();
                return;
            }

            //valifation to ensure enough money is in vash register
            decimal yourChange = InputMoney - ItemPrice;


            if( yourChange > RegisterTotalDec)
            {
                MessageBox.Show("You dont have enough change to give the customer. Do not sell.");
                clearTextboxes();
                return;


            }


           
           



            //Ensure that inputs are not larger than max value
            if(((ItemPrice > Decimal.MaxValue) || (ItemPrice < Decimal.MinValue  ))||((InputMoney > Decimal.MaxValue) || (InputMoney < Decimal.MinValue)))
            {
                MessageBox.Show("Dont be a hacker! Enter a Realistic number!");
                clearTextboxes();
                return;


            }



            //Perform Math

            DetermineChange(ItemPrice, InputMoney);


            displayOutput();
            showChange(change);



        }
         void DetermineChange(decimal itemCost, decimal givenAmount)
        {
            change = givenAmount - itemCost;
            string customersChange = change.ToString("C");

            string ItemPrice = itemCost.ToString("C");

            //Register Total Programming
            RegisterTotalDec = RegisterTotalDec + itemCost;

            RegisterTotalDec = RegisterTotalDec - change;

            RegisterTotalStr = RegisterTotalDec.ToString("C");

            RegisterTotal.Content = RegisterTotalStr;





            //insert validation



            dec_numOf50s = Math.Floor(change / 50.00M);
            dec_numOf20s = Math.Floor((change % 50.00M) / 20.00M);
            dec_numOf10s = Math.Floor(((change % 50.00M) % 20.00M) / 10.00M);
            dec_numOf5s = Math.Floor((((change % 50.00M) % 20.00M) % 10.00M) / 5.00M);
            dec_numOf1s = Math.Floor(((((change % 50.00M) % 20.00M) % 10.00M) % 5.00M) / 1.00M);


            dec_numOfQuarters = Math.Floor((((((change % 50.00M) % 20.00M) % 10.00M) % 5.00M) % 1.00M) / .25M);
            dec_numOfDimes = Math.Floor(((((((change % 50.00M) % 20.00M) % 10.00M) % 5.00M) % 1.00M) % .25M) / .10M);
            dec_numOfNickesl = Math.Floor((((((((change % 50.00M) % 20.00M) % 10.00M) % 5.00M) % 1.00M) % .25M) % .10M) / .05M);
            dec_numOfPennies = Math.Floor(((((((((change % 50.00M) % 20.00M) % 10.00M) % 5.00M) % 1.00M) % .25M) % .10M) % .05M) / .01M);

            var_numOf50s = dec_numOf50s.ToString();
            var_numOf20s = dec_numOf20s.ToString();
            var_numOf10s = dec_numOf10s.ToString();
            var_numOf5s = dec_numOf5s.ToString();
            var_numOf1s = dec_numOf1s.ToString();
            var_numOfQuarters = dec_numOfQuarters.ToString();
            var_numOfDimes = dec_numOfDimes.ToString();
            var_numOfNickesl = dec_numOfNickesl.ToString();
            var_numOfPennies = dec_numOfPennies.ToString();

            

        }
        public void showChange(decimal change)
        {

            string myTotal = change.ToString("C");
            totalChange.Content = myTotal;
             



        }
        public void displayOutput()
        {
            numOf50.Text = var_numOf50s;
            numOf20.Text = var_numOf20s;
            numOf10.Text = var_numOf10s;
            numOf5.Text = var_numOf5s;
            numOf1.Text = var_numOf1s;
            numOfQuarters1.Text = var_numOfQuarters;
            numOfDimes.Text = var_numOfDimes;
            numOfNickels1.Text = var_numOfNickesl;
            numOfPennies.Text = var_numOfPennies;
        }

        //Clear ItemBox Text on focus
        private void itemPrice_GotFocus(object sender, RoutedEventArgs e)
        {
            itemPrice.Text = String.Empty;
        }

        private void CustomersMoney_GotFocus(object sender, RoutedEventArgs e)
        {
            CustomersMoney.Text = String.Empty;
        }
        public void clearTextboxes()
        {
           foreach (TextBox i in outputTextBoxes) 
                {
                      i.Text = String.Empty;
                }
            
          foreach (TextBox j in inputTextBoxes)
            {

                j.Text = String.Empty;

            }

            totalChange.Content = String.Empty;
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {

        }

        private void ClearButton_Click(object sender, RoutedEventArgs e)
        {
            clearTextboxes();
        }

        private void ExitButton_Click(object sender, RoutedEventArgs e)
        {
            Environment.Exit(0);

            //Doesnt Work
            //* MessageBox.Show("Sure", "Some Title", MessageBoxButtons.YesNo);
            //      if (dialogResult == DialogResult.Yes)
            //        {
            //              Environment.Exit(0);
            //
            // }
            // else if (dialogResult == DialogResult.No)
            // {
            //    return;
            // }

        }
    }



}

