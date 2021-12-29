
using BenefitCalculator.Models;

namespace BenefitCalculator.Services.Interfaces
{
    public interface IBenefitCalculatorService
    {
        BenefitCalculationResponse CalculateBenefitCost(BenefitCalculatorModel benefitCalculatorModel);
    }
}
