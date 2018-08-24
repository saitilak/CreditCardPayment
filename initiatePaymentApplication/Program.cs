using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CreditCardPayment;

namespace initiatePaymentApplication
{
    class Program
    {
        static void Main1(string[] args)
        {
            long ccreditCardNumber1;
            int ccpinNumber;
            double billAmount;
            Console.WriteLine("Enter the credit card number");
            ccreditCardNumber1 = Convert.ToInt64(Console.ReadLine());
            creditCardPayment creditCardPayment1 = new creditCardPayment();
            bool result1= creditCardPayment1.validate(ccreditCardNumber1);
            if(result1)
            {
                Console.WriteLine("Enter the bill amount:");
                billAmount = Convert.ToDouble(Console.ReadLine());
                Console.WriteLine("Enter the credit card pin number");
                ccpinNumber= Convert.ToInt32(Console.ReadLine());
                bool validPin=creditCardPayment1.validatePin(ccreditCardNumber1,ccpinNumber);
                if(validPin)
                {

                }
                else
                {

                }
            }
            else
            {
                Console.WriteLine("Invalid Card");
            }
            //Console.WriteLine(result1);
            //Console.ReadKey();
        }
        static void Main(string[] args)
        {
            var retailer = new Retailer();
            retailer.Name = "RIL";
            retailer.Account = new Account(retailer.Name, 004001, 10000);
            //Retailer account loaded.

            Console.WriteLine("Enter Customer account/card no");
            var custAccountNo = Console.ReadLine();

            Console.WriteLine("Enter Amount");
            var amount = Console.ReadLine();

            Console.WriteLine("Enter PIN");
            var pin = Console.ReadLine();

            var result = retailer.DoPosTransaction(Convert.ToInt64(custAccountNo), 100, Convert.ToInt32(pin));
            Console.WriteLine(result.Message);
            if (result.Success)
            {
                Console.WriteLine("Deliver items");
            }

        }
    }
}
