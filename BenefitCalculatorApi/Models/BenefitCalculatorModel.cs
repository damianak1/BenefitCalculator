using System;
using System.Collections.Generic;

// One file, 3 classes for simplicity
namespace BenefitCalculator.Models
{
    public class BenefitCalculatorModel
    {
        public decimal ParticipantPayPerPeriod { get; set; } = 2000;
        public decimal BaseParticipantBenefitAnnualCost { get; set; } = 1000;
        public decimal BaseDependentBenefitAnnualCost { get; set; } = 500;
        public BenefitRecord ParticipantBenefitRecord { get; set; } = new BenefitRecord();
        public List<BenefitRecord> DependencyBenefitRecords { get; set; } = new List<BenefitRecord>();
    }

    public class BenefitRecord
    {
        public string Name { get; set; }
        /// <summary>
        /// Integer to be translated to decimal, for example 5 would represent 5% or .05
        /// </summary>
        public int DiscountPercent { get; set; }
        public decimal DiscountPercentAsDecimal => this.DiscountPercent == 0 ? 0 : Decimal.Divide(DiscountPercent,100);
    }

    public class BenefitCalculationResponse
    {
        public decimal BenefitCostPerPayPeriod { get; set; }
    }
}
