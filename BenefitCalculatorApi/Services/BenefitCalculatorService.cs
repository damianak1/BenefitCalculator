using System;
using Tutorial.Models;
using Tutorial.Services.Interfaces;

namespace Tutorial.Services
{
    public class BenefitCalculatorService : IBenefitCalculatorService
    {
        private readonly IBenefitDiscountService _benefitDiscountService;

        public BenefitCalculatorService(IBenefitDiscountService benefitDiscountService)
        {
            this._benefitDiscountService = benefitDiscountService;
        }
        public BenefitCalculationResponse CalculateBenefitCost(BenefitCalculatorModel benefitCalculatorModel)
        {
            //Apply any discounts 
            this._benefitDiscountService.ApplyBenefitDiscounts(benefitCalculatorModel);

            decimal participantBaseCost = (benefitCalculatorModel.BaseParticipantBenefitAnnualCost); 
            decimal dependentBaseCost = (benefitCalculatorModel.BaseDependentBenefitAnnualCost);

            decimal participantBenefitCost = participantBaseCost - (participantBaseCost * benefitCalculatorModel.ParticipantBenefitRecord.DiscountPercentAsDecimal);

            decimal dependentBenefitCost = 0;
            foreach(var dependent in benefitCalculatorModel.DependencyBenefitRecords)
            {
                dependentBenefitCost += dependentBaseCost - (dependentBaseCost * dependent.DiscountPercentAsDecimal);
            }

            decimal totalAnnualCost = participantBenefitCost + dependentBenefitCost;
            decimal totalCostPerPayPeriod = Math.Round((totalAnnualCost) /26,2, MidpointRounding.AwayFromZero);

            // If we wanted to present a final payment to account for rounding 
            decimal diffAfterRounding = totalAnnualCost - (totalCostPerPayPeriod * 26);
            decimal lastPayemnt = totalCostPerPayPeriod + diffAfterRounding;

            return new BenefitCalculationResponse { BenefitCostPerPayPeriod = totalCostPerPayPeriod };
        }



    }
}
