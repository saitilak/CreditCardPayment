using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CreditCardPayment;
using CreditCardPayment.Contracts;

namespace initiatePaymentApplication
{
    class Program
    {

        static void Main(string[] args)
        {
            var retailer = new Retailer
            {
                Name = "CAMP Industries",
                Account = (IAccount)new RetailerAccount("CAMP Industries", 99999, 0)
            };
            //Retailer account loaded.

            bool isContinue = true;

            while (isContinue)
            {


                Console.WriteLine("Enter Customer account/card no");
                var custAccountNo = Console.ReadLine();

                var isCardValid = Bank.ValidateDebitAccount(Convert.ToInt64(custAccountNo));

                if (!isCardValid.Success)
                {
                    Console.WriteLine(isCardValid.Message);
                }
                else {
                    Console.WriteLine("Enter Purchase Amount");
                    var amount = Console.ReadLine();

                    Console.WriteLine("Enter PIN");
                    var pin = Console.ReadLine();

                    var result = retailer.InitiatePosTransaction(Convert.ToInt64(custAccountNo), Convert.ToDouble(amount),
                        Convert.ToInt32(pin));

                    Console.WriteLine(result.Message);
                    if (result.Success)
                    {
                        Console.WriteLine("Deliver items");
                    }
                }

                Console.WriteLine("Proceed with another transaction Y/N?");

                var readLine = Console.ReadLine();

                if (readLine != null)
                {
                    isContinue = readLine.ToUpper() == "Y";
                }
                else
                {
                    isContinue = false;
                }
            }
            Console.ReadLine();

        }
    }
}
