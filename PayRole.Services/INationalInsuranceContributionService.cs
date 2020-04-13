using System;
using System.Collections.Generic;
using System.Text;

namespace PayRole.Services
{
    public interface INationalInsuranceContributionService
    {
        decimal NIContribution(decimal totalAmount);
    }
}
