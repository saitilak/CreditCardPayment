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
        
        static void Main(string[] args)
        {
            var retailer = new Retailer();
            retailer.Name = "RIL";
            retailer.Account = new Account(retailer.Name, 004001, 10000,1111);
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

            Console.Read();

        }
    }
}
