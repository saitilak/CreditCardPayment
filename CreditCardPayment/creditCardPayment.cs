using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CreditCardPayment
{
    public class creditCardPayment
    {
              
        public bool validate(long creditCardNumber)
        {
            cardValidation cardValidate = new cardValidation();
            bool result = cardValidate.isCardValid(creditCardNumber);
            return result;
        }
        public bool validatePin(long creditCardNumber, int ccardPin)
        {
            cardValidation cardValidate = new cardValidation();
            bool result1 = cardValidate.isValidPin(creditCardNumber,ccardPin);
            return result1;
        }
    }
}
