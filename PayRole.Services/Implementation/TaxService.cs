using System;
using System.Collections.Generic;
using System.Text;

namespace PayRole.Services.Implementation
{
    public class TaxService : ITaxService
    {
        private decimal taxRate;
        private decimal tax;

        public decimal TaxAmount(decimal totalAmount)
        {
            if(totalAmount <= 1042)
            {
                //Tax Free Rate
                taxRate = 0.0m;
                tax = totalAmount * taxRate;
            }
            else if (totalAmount > 1042 && totalAmount <= 3125)
            {
                //Basic tax Rate
                taxRate = 0.20m;
                //Income Tax
                tax = (1042 * 0.0m) + ((totalAmount - 1042) * taxRate);
            }
            else if (totalAmount > 3125 && totalAmount <= 12500)
            {
                //Higher Tax rate
                taxRate = 0.40m;
                //income Tax
                tax = (1042 * 0.0m) + ((3125 - 1042) * 0.20m) + ((totalAmount - 3125) * taxRate);
            }
            else if (totalAmount > 12500)
            {
                //Additional Tax rate
                taxRate = 0.45m;
                //income Tax
                tax = (1042 * 0.0m) + ((3125 - 1042) * 0.20m) + ((12500 - 3125) * 0.40m) + ((totalAmount - 12500) * taxRate);
            }

            return tax;
        }
    }
}
