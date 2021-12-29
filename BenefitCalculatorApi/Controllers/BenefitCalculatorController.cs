using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tutorial.Models;
using Tutorial.Services.Interfaces;

namespace Tutorial.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BenefitCalculatorController : ControllerBase
    {
        private readonly IBenefitCalculatorService _benefitCalculatorService;

        public BenefitCalculatorController(IBenefitCalculatorService benefitCalculatorService)
        {
            this._benefitCalculatorService = benefitCalculatorService;
        }


        [HttpGet]
        public JsonResult Get()
        {
            //We could use configuation to override defaults here such as the base benefit costs
            BenefitCalculatorModel benefitCalculatorModel = new BenefitCalculatorModel();
            return new JsonResult(benefitCalculatorModel);
        }

        [HttpPost]
        public JsonResult Post(BenefitCalculatorModel benefitCalculatorModel)
        {
            BenefitCalculationResponse benefitCalculationResponse = this._benefitCalculatorService.CalculateBenefitCost(benefitCalculatorModel);
            return new JsonResult(benefitCalculationResponse);
        }

    }
}
