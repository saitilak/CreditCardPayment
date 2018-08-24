using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CreditCardPayment
{
    public interface IAccount
    {
        string Name { get; set; }
        long AccountNumber { get; set; }

        /// <summary>
        /// Status: True for active
        /// </summary>
        bool Status { get; set; }
        double Balance { get; set; }

        double Credit(double amount);
    }

    public class Account : IAccount
    {
        public Account(string name, long accountNumber, double balance)
        {
            Name = name;
            AccountNumber = accountNumber;
            Balance = balance;
        }

        public string Name { get; set; }
        public long AccountNumber { get; set; }
        public bool Status { get; set; }
        public double Balance { get; set; }
        public double Credit(double amount)
        {
            throw new NotImplementedException();
        }
    }

    public class Retailer
    {
        public IAccount Account { get; set; }
        public string Name { get; set; }

        public Result DoPosTransaction(long customerAccountNo, double amount, int pin)
        {
            return Bank.DoTransaction(customerAccountNo, this.Account.AccountNumber, pin, amount);

        }

    }

    public class Result
    {

        public bool Success { get; set; }

        public string Message { get; set; }
    }

    public class Customer
    {
        public IAccount Account { get; set; }
        public string Name { get; set; }

    }

    /// <summary>
    /// Universal single Bank and branch
    /// </summary>
    public static class Bank
    {
        static IList<IAccount> Accounts;
        //no need if we fecth accounts from database
        static Bank()
        {
            Accounts = new List<IAccount>();
            var account = new Account("Anjan", 1111, 1000);
            account.Balance = 1000;
            Accounts.Add(account);


        }
        public static Result DoTransaction(long debitAccount, long creditAccount, int pin, double amount)
        {
            //Case 1: payment successful
            //Case 2: payment failed
            //1) Incorrect pin,
            //2) Card invalid
            //3) Insufficient funds
            //4) Network failure
             var result = new Result();
            if (!IsCardValid(debitAccount))
            {
                result.Success = false;
                result.Message = "Card invalid";
            }
            else if(true)
            {
                result.Success = false;
                result.Message = "Card invalid";
            }
            else if (true)
            {
                result.Success = false;
                result.Message = "Card invalid";
            }


            return result;
        }

        private static bool IsCardValid(long debitAccount)
        {
            for (int i = 0; i < Accounts.Count; i++)
            {
                if (Accounts[i] != null && Accounts[i].AccountNumber == debitAccount)
                {
                    return true;
                }
            }
            return false;
        }

        private static bool IsAccountActive()
        {
            //Search throuth all <see ref="Accounts">
            return false;
        }


    }

}
