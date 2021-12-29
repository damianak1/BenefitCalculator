
using Tutorial.Models;

namespace Tutorial.Services.Interfaces
{
    public interface IBenefitDiscountService
    {
        void ApplyBenefitDiscounts(BenefitCalculatorModel benefitCalculatorModel);
    }
}
