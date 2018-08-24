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
        long cardNumber { get; set; }
        bool Status { get; set; }
        double Balance { get; set; }

        
    }
}
