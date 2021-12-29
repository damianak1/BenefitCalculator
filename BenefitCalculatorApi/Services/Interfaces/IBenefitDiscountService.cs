
using BenefitCalculator.Models;

namespace BenefitCalculator.Services.Interfaces
{
    public interface IBenefitDiscountService
    {
        void ApplyBenefitDiscounts(BenefitCalculatorModel benefitCalculatorModel);
    }
}
