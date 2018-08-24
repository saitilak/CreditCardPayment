using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CreditCardPayment
{
    class cardValidation:IAccount
    {
        public double Balance { get; set; }
      
        public long cardNumber { get; set; }
      
        public string Name { get; set; }
        

        public bool Status { get; set; }

        long dummycardNumber = 48549999999999;

        int cardPin = 1234;
        public bool isCardValid(long cardNumber1)
        {
            if (dummycardNumber == cardNumber1)
                return true;
            else
                return false;
        }

        public bool isValidPin(long cardNumber1,int pinNumber)
        {
            if (isCardValid(cardNumber1))
            {
                if (cardPin == pinNumber)
                {
                    return true;
                }
                else
                    return false;
            }
            else
            {
                return false;
            }
        }
    }
}
