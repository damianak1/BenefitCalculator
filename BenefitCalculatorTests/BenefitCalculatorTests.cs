using Moq;
using NUnit.Framework;
using System.Linq;
using BenefitCalculator.Models;
using BenefitCalculator.Services;
using BenefitCalculator.Services.Interfaces;

namespace BenefitCalculatorTests
{
    public class Tests
    {
        [Test]
        public void BenefitCalculatorSmokeTest()
        {
            Mock<IBenefitDiscountService> mockDiscountService = new Mock<IBenefitDiscountService>();
            BenefitCalculatorService benefitCalculatorService = new BenefitCalculatorService(mockDiscountService.Object);

            BenefitCalculatorModel benefitCalculatorModel = new BenefitCalculatorModel { ParticipantBenefitRecord = new BenefitRecord { Name = "Sam" } };
            benefitCalculatorModel.DependencyBenefitRecords.Add(new BenefitRecord { Name = "Joe" });
            benefitCalculatorModel.DependencyBenefitRecords.Add(new BenefitRecord { Name = "Jen" });

            BenefitCalculationResponse results = benefitCalculatorService.CalculateBenefitCost(benefitCalculatorModel);
            Assert.AreEqual(76.92M, results.BenefitCostPerPayPeriod);
        }

        [Test]
        public void NameStartsWithABenefitDiscountServiceTest_OneDependentDiscount()
        {
            BenefitCalculatorModel benefitCalculatorModel = new BenefitCalculatorModel { ParticipantBenefitRecord = new BenefitRecord { Name = "Sam" } };
            benefitCalculatorModel.DependencyBenefitRecords.Add(new BenefitRecord { Name = "Joe" });
            benefitCalculatorModel.DependencyBenefitRecords.Add(new BenefitRecord { Name = "Anne" });// Discount should be applied 

            NameStartsWithABenefitDiscountService nameStartsWithABenefitDiscountService = new NameStartsWithABenefitDiscountService();
            nameStartsWithABenefitDiscountService.ApplyBenefitDiscounts(benefitCalculatorModel);

            Assert.AreEqual(0, benefitCalculatorModel.ParticipantBenefitRecord.DiscountPercent);
            Assert.True(benefitCalculatorModel.DependencyBenefitRecords.Where(x => x.DiscountPercent == 10).Count() == 1);
        }

        [Test]
        public void NameStartsWithABenefitDiscountServiceTest_ParticipantDisount()
        {
            BenefitCalculatorModel benefitCalculatorModel = new BenefitCalculatorModel
            {
                ParticipantBenefitRecord = new BenefitRecord { Name = "Amy" } // Discount should be applied 
            };
            benefitCalculatorModel.DependencyBenefitRecords.Add(new BenefitRecord { Name = "Joe" });
            benefitCalculatorModel.DependencyBenefitRecords.Add(new BenefitRecord { Name = "Jim" });

            NameStartsWithABenefitDiscountService nameStartsWithABenefitDiscountService = new NameStartsWithABenefitDiscountService();
            nameStartsWithABenefitDiscountService.ApplyBenefitDiscounts(benefitCalculatorModel);

            Assert.AreEqual(10, benefitCalculatorModel.ParticipantBenefitRecord.DiscountPercent);
            Assert.True(benefitCalculatorModel.DependencyBenefitRecords.Where(x => x.DiscountPercent == 10).Count() == 0);
        }


        [Test]
        public void BenefitCalculatorService_OneDependentDiscount()
        {
            BenefitCalculatorModel benefitCalculatorModel = new BenefitCalculatorModel { ParticipantBenefitRecord = new BenefitRecord { Name = "Sam" } };
            benefitCalculatorModel.DependencyBenefitRecords.Add(new BenefitRecord { Name = "Joe" });
            benefitCalculatorModel.DependencyBenefitRecords.Add(new BenefitRecord { Name = "Anne" });// Discount should be applied 

            NameStartsWithABenefitDiscountService nameStartsWithABenefitDiscountService = new NameStartsWithABenefitDiscountService();
            BenefitCalculatorService benefitCalculatorService = new BenefitCalculatorService(nameStartsWithABenefitDiscountService);

            BenefitCalculationResponse results = benefitCalculatorService.CalculateBenefitCost(benefitCalculatorModel);
            Assert.AreEqual(75.0M, results.BenefitCostPerPayPeriod);
        }

        [Test]
        public void BenefitCalculatorService_ParticipantDiscount()
        {
            BenefitCalculatorModel benefitCalculatorModel = new BenefitCalculatorModel { ParticipantBenefitRecord = new BenefitRecord { Name = "Amy" } };// Discount should be applied 
            benefitCalculatorModel.DependencyBenefitRecords.Add(new BenefitRecord { Name = "Joe" });
            benefitCalculatorModel.DependencyBenefitRecords.Add(new BenefitRecord { Name = "Jim" });

            NameStartsWithABenefitDiscountService nameStartsWithABenefitDiscountService = new NameStartsWithABenefitDiscountService();
            BenefitCalculatorService benefitCalculatorService = new BenefitCalculatorService(nameStartsWithABenefitDiscountService);

            BenefitCalculationResponse results = benefitCalculatorService.CalculateBenefitCost(benefitCalculatorModel);
            Assert.AreEqual(73.08M, results.BenefitCostPerPayPeriod);
        }

    }
}