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

        int PinNumber { get; set; }

        /// <summary>
        /// Status: True for active
        /// </summary>
        bool Status { get; set; }
        double Balance { get; set; }

        double Credit(double amount);
    }

    public class Account : IAccount
    {
        public Account(string name, long accountNumber, double balance, int pin, bool status = true)
        {
            Name = name;
            AccountNumber = accountNumber;
            Balance = balance;
            PinNumber = pin;
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
            var account = new Account("Anjan", 1111111111111111, 1000,1234);
            var account1 = new Account("Sai", 2222222222222222, 1000, 1243);
            //account.Balance = 1000;
            Accounts.Add(account);
            Accounts.Add(account1);


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
            if (!IsAccountActive(debitAccount))
            {
                result.Success = false;
                result.Message = "Card Inactive";
            }
            if (!IsCardValid(debitAccount))
            {
                result.Success = false;
                result.Message = "Card invalid";
            }
            else if(!IsValidPin(debitAccount,pin))
            {
                result.Success = false;
                result.Message = "Pin incorrect";
            }
            else if(!FundsAvailable(debitAccount, amount))
            {
                result.Success = false;
                result.Message = "Insufficient funds";
            }
            //Ready to transact
            else
            {
                result = TransferAmount(debitAccount, creditAccount, amount);
            }


            return result;
        }

        private static Result TransferAmount(long debitAccountNum, long creditAccountNum, double amount)
        {
            var ca = Accounts.FirstOrDefault(a => a.AccountNumber == creditAccountNum);
            //return failure result if ca is null
            var da = Accounts.FirstOrDefault(a => a.AccountNumber == creditAccountNum);
            //return failure result if da is null
            var result1 = new Result();
            if (ca != null)
            {
                DebitFromAccount(debitAccountNum, amount);
                result1.Success = true;
                result1.Message = "Amount successfully debited";
                CreditToAccount(creditAccountNum, amount);
            }
            return result1;
        }

        private static void CreditToAccount(long creditAccountNum, double amount)
        {
            var account = Accounts.FirstOrDefault(a => a.AccountNumber == creditAccountNum);
            var NewBalance = account.Balance + amount;
            account.Balance = NewBalance;
        }

        private static void DebitFromAccount(long debitAccountNum, double amount)
        {
            var account = Accounts.FirstOrDefault(a => a.AccountNumber == debitAccountNum);
            var NewBalance = account.Balance - amount;
            account.Balance = NewBalance;
        }

        private static bool FundsAvailable(long debitAccount, double amount)
        {
            var account = Accounts.FirstOrDefault(a => a.AccountNumber == debitAccount);
            if (account != null)
            {
                return account.Balance >= amount;
            }
            return false;
        }

        private static bool IsCardValid(long debitAccount)
        {
            if(debitAccount.ToString().Length==16)
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
            else {
                return false;
            }
        }
        private static bool IsValidPin(long debitAccount,int pin)
        {
            if (pin.ToString().Length == 4)
            {
                for (int i = 0; i < Accounts.Count; i++)
                {
                    if ((Accounts[i].AccountNumber == debitAccount) && (Accounts[i].PinNumber == pin))
                    {
                        return true;
                    }
                }
                return false;
            }
            else {
                return false;
            }
            }


        private static bool IsAccountActive(long debitAccount)
        {
            //Search throuth all <see ref="Accounts">
            var account = Accounts.FirstOrDefault(a => a.AccountNumber == debitAccount);
            if (account != null)
            {
                return account.Status;
            }
            return false;
        }

        

    }

}
