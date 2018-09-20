using System;

namespace CreditCardPayment.Contracts
{
    public interface IAccount
    {
        string Name { get; set; }
        long AccountNumber { get; set; }

        int PinNumber { get; set; }

        /// <summary>
        /// Status: True for active
        /// </summary>
        bool Status { get; set; }
        double Balance { get; set; }

        double Credit(double amount);
    }


    public class RetailerAccount : Account
    {
        public RetailerAccount(string name, long accountNumber, double balance, bool status = true)
            : base(name, accountNumber, balance, status)
        {
        }
    }

    public class CustomerAccount : Account
    {
        public CustomerAccount(string name, long accountNumber, double balance, int pin, bool status = true)
            : base(name, accountNumber, balance, status)
        {
            PinNumber = pin;
        }
    }

    public class Account : IAccount
    {
        public Account(string name, long accountNumber, double balance, bool status = true)
        {
            Name = name;
            AccountNumber = accountNumber;
            Balance = balance;
            
            Status = true;
        }

        public string Name { get; set; }
        public long AccountNumber { get; set; }
        public bool Status { get; set; }
        public double Balance { get; set; }
        public int PinNumber { get; set; }
        public double Credit(double amount)
        {
            throw new NotImplementedException();
        }
    }
}