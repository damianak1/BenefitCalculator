using Moq;
using NUnit.Framework;
using System.Linq;
using Tutorial.Models;
using Tutorial.Services;
using Tutorial.Services.Interfaces;

namespace BenefitCalculatorTests
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void BenefitCalculatorSmokeTest()
        {
            var foo = new Mock<IBenefitDiscountService>();

            var bar = new BenefitCalculatorService(foo.Object);

            BenefitCalculatorModel benefitCalculatorModel = new BenefitCalculatorModel { ParticipantBenefitRecord = new BenefitRecord { Name = "Sam" } };

            benefitCalculatorModel.DependencyBenefitRecords.Add(new BenefitRecord { Name = "Joe" });
            benefitCalculatorModel.DependencyBenefitRecords.Add(new BenefitRecord { Name = "Jen" });

            var results = bar.CalculateBenefitCost(benefitCalculatorModel);

            //76.92M
        }


        [Test]
        public void NameStartsWithABenefitDiscountServiceTest_OneDependentDiscount()
        {
            BenefitCalculatorModel benefitCalculatorModel = new BenefitCalculatorModel { ParticipantBenefitRecord = new BenefitRecord { Name = "Sam" } };
            benefitCalculatorModel.DependencyBenefitRecords.Add(new BenefitRecord { Name = "Joe" });
            benefitCalculatorModel.DependencyBenefitRecords.Add(new BenefitRecord { Name = "Anne" });

            NameStartsWithABenefitDiscountService nameStartsWithABenefitDiscountService = new NameStartsWithABenefitDiscountService();
            nameStartsWithABenefitDiscountService.ApplyBenefitDiscounts(benefitCalculatorModel);

            Assert.AreEqual(0, benefitCalculatorModel.ParticipantBenefitRecord.DiscountPercent);
            Assert.True(benefitCalculatorModel.DependencyBenefitRecords.Where(x => x.DiscountPercent == 10).Count() == 1);

        }

        [Test]
        public void NameStartsWithABenefitDiscountServiceTest_ParticipantDisount()
        {
            BenefitCalculatorModel benefitCalculatorModel = new BenefitCalculatorModel { ParticipantBenefitRecord = new BenefitRecord { Name = "Amy" } };
            benefitCalculatorModel.DependencyBenefitRecords.Add(new BenefitRecord { Name = "Joe" });
            benefitCalculatorModel.DependencyBenefitRecords.Add(new BenefitRecord { Name = "Jim" });

            NameStartsWithABenefitDiscountService nameStartsWithABenefitDiscountService = new NameStartsWithABenefitDiscountService();
            nameStartsWithABenefitDiscountService.ApplyBenefitDiscounts(benefitCalculatorModel);

            Assert.AreEqual(10, benefitCalculatorModel.ParticipantBenefitRecord.DiscountPercent);
            Assert.True(benefitCalculatorModel.DependencyBenefitRecords.Where(x => x.DiscountPercent == 10).Count() == 0);

        }


        [Test]
        public void BenefitCalculator_OneDependentDiscount()
        {

            BenefitCalculatorModel benefitCalculatorModel = new BenefitCalculatorModel { ParticipantBenefitRecord = new BenefitRecord { Name = "Sam" } };
            benefitCalculatorModel.DependencyBenefitRecords.Add(new BenefitRecord { Name = "Joe" });
            benefitCalculatorModel.DependencyBenefitRecords.Add(new BenefitRecord { Name = "Anne" });

            NameStartsWithABenefitDiscountService nameStartsWithABenefitDiscountService = new NameStartsWithABenefitDiscountService();

            var bar = new BenefitCalculatorService(nameStartsWithABenefitDiscountService);

            var results = bar.CalculateBenefitCost(benefitCalculatorModel);

            //76.92M
        }

        [Test]
        public void BenefitCalculator_ParticipantDisount()
        {

            BenefitCalculatorModel benefitCalculatorModel = new BenefitCalculatorModel { ParticipantBenefitRecord = new BenefitRecord { Name = "Amy" } };
            benefitCalculatorModel.DependencyBenefitRecords.Add(new BenefitRecord { Name = "Joe" });
            benefitCalculatorModel.DependencyBenefitRecords.Add(new BenefitRecord { Name = "Jim" });

            NameStartsWithABenefitDiscountService nameStartsWithABenefitDiscountService = new NameStartsWithABenefitDiscountService();

            var bar = new BenefitCalculatorService(nameStartsWithABenefitDiscountService);

            var results = bar.CalculateBenefitCost(benefitCalculatorModel);

            //73.08M
        }

    }
}