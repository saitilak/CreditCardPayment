using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CreditCardPayment.Contracts;

namespace CreditCardPayment
{
    public class Retailer
    {
        public IAccount Account { get; set; }
        public string Name { get; set; }

        public Result InitiatePosTransaction(long customerAccountNo, double amount, int pin)
        {
            return Bank.DoTransaction(customerAccountNo, this.Account.AccountNumber, pin, amount);

        }

    }
}   