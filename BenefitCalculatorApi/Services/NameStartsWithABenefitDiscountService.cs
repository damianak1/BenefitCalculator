using BenefitCalculator.Models;
using BenefitCalculator.Services.Interfaces;

namespace BenefitCalculator.Services
{
    public class NameStartsWithABenefitDiscountService : IBenefitDiscountService
    {
        public void ApplyBenefitDiscounts(BenefitCalculatorModel benefitCalculatorModel)
        {
            if (benefitCalculatorModel.ParticipantBenefitRecord.Name.ToLower().StartsWith('a')){
                benefitCalculatorModel.ParticipantBenefitRecord.DiscountPercent = 10;
            }

            foreach(var dependentRecord in benefitCalculatorModel.DependencyBenefitRecords)
            {
                if (dependentRecord.Name.ToLower().StartsWith('a'))
                {
                    dependentRecord.DiscountPercent = 10;
                }
            }
        }
    }
}
