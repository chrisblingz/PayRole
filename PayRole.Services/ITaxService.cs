using System;
using System.Collections.Generic;
using System.Text;

namespace PayRole.Services
{
    public interface ITaxService
    {
        decimal TaxAmount(decimal totalAmount);
    }
}
