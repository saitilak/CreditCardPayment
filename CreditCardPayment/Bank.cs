using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CreditCardPayment.Contracts;

namespace CreditCardPayment
{
    /// <summary>
    /// Universal Bank
    /// </summary>
    public static class Bank
    {
        static IList<IAccount> Accounts;
        //no need if we fecth accounts from database
        static Bank()
        {
            Accounts = new List<IAccount>();
            for (int i = 101; i <= 105; i++)
            {
                Accounts.Add((IAccount) new CustomerAccount("Customer" + i, i, 1000, Convert.ToInt32(string.Format("{0}1", i))));
            }

            Accounts.Add((IAccount) new RetailerAccount("CAMP Industries", 99999, 0));
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

            if (!IsValidPin(debitAccount, pin))
            {
                result.Success = false;
                result.Message = "Pin incorrect";
            }
            else if (!FundsAvailable(debitAccount, amount))
            {
                result.Success = false;
                result.Message = "Insufficient funds";
            }
            //Ready to transact
            else
            {
                result = TransferAmount(debitAccount, creditAccount, amount);
            }

            if (result != null )
            {                   
                //Send sms to debitor and creditor about the transaction and remaining balance

                if (result.Success)
                {
                    //SendSms(debitAccount, amount);
                    //SendSms(creditAccount, amount);
                }
                else
                {
                    //SendSms(debitAccount, amount); that transaction tried is failed.
                }

            }

            return result;
        }

        public static Result ValidateDebitAccount(long debitAccount)
        {
            var result = new Result();
            result.Success = true;
            result.Message = "Card is valid.";

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
            return result;
        }

        private static Result TransferAmount(long debitAccountNum, long creditAccountNum, double amount)
        {
            var creditAccount = Accounts.FirstOrDefault(a => a.AccountNumber == creditAccountNum);
            var debitAccount = Accounts.FirstOrDefault(a => a.AccountNumber == debitAccountNum);

            var result = new Result()
            {Success = false, Message = string.Empty};

            if (debitAccount != null)
            {
                var isDebitSuccess = DebitAccount(debitAccountNum, amount);
                try
                {
                    if (isDebitSuccess.HasValue)
                    {
                        if (creditAccount != null)
                        {
                            CreditAccount(creditAccountNum, amount);
                            result.Success = true;
                            result.Message = "Transaction Successful";
                            return result;
                        }
                    }
                }
                catch (Exception ex)
                {
                    //Undo transaction
                    CreditAccount(debitAccountNum, amount);
                    result.Message = "Transaction failed";
                }
            }
            
            return result;
        }

        private static void CreditAccount (long creditAccountNum, double amount)
        {
            var acc = Accounts.FirstOrDefault(a => a.AccountNumber == creditAccountNum);
            if (acc != null)
            {
                acc.Balance += amount;
            }
        }

        private static double? DebitAccount (long debitAccountNum, double amount)
        {
            var account = Accounts.FirstOrDefault(a => a.AccountNumber == debitAccountNum);

            if (account != null)
            {
                account.Balance -= amount;
                return account.Balance;
            }
            return null;
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
            if (debitAccount.ToString().Length == 16 || debitAccount.ToString().Length == 3)
            {
                return Accounts.Any(t => t != null && t.AccountNumber == debitAccount);
            }
            return false;
        }

        private static bool IsValidPin(long debitAccount, int pin)
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

        public static void DoSomething()
        {
        }


    }

}
