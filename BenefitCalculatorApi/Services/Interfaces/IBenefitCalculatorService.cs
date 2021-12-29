
using Tutorial.Models;

namespace Tutorial.Services.Interfaces
{
    public interface IBenefitCalculatorService
    {
        BenefitCalculationResponse CalculateBenefitCost(BenefitCalculatorModel benefitCalculatorModel);
    }
}
