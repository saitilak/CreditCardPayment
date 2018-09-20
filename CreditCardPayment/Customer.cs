using CreditCardPayment.Contracts;

namespace CreditCardPayment
{
    public class Customer
    {
        public IAccount Account { get; set; }
        public string Name { get; set; }

    }
}