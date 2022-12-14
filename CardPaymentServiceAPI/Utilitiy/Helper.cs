using System;

namespace CardPaymentServiceAPI.Utilitiy
{
    public static class Helper
    {
        public static string AccountNumber()
        {
            Random rnd = new Random();

            var accountNumber = rnd.Next(100000, 999999);
     
            return accountNumber.ToString();
        }
    }
}
