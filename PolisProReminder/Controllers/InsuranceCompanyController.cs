﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PolisProReminder.Models;
using PolisProReminder.Services;

namespace PolisProReminder.Controllers
{
    [Route("api/company")]
    public class InsuranceCompanyController : ControllerBase
    {
        private readonly IInsuranceCompanyService _insuranceCompanyService;

        public InsuranceCompanyController(IInsuranceCompanyService insuranceCompanyService)
        {
            _insuranceCompanyService = insuranceCompanyService;
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Admin, Agent")]
        public ActionResult<InsuranceCompanyDto> Update([FromBody] CreateInsuranceCompanyDto dto, [FromRoute] int id)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var company = _insuranceCompanyService.Update(id, dto);

            return Ok(company);
        }

        [HttpPost]
        [Authorize(Roles = "Admin, Agent")]
        public ActionResult<InsuranceCompanyDto> CreateInsuranceCompany([FromBody] CreateInsuranceCompanyDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var company = _insuranceCompanyService.Create(dto);

            return Ok(company);
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin, Agent")]
        public ActionResult Delete([FromRoute] int id)
        {
            _insuranceCompanyService.Delete(id);

            return Ok();
        }

        [HttpGet]
        public ActionResult<IEnumerable<InsuranceCompanyDto>> GetAll()
        {
            var companies = _insuranceCompanyService.GetAll();

            return Ok(companies);
        }

        [HttpGet("{id}")]
        public ActionResult<InsuranceCompanyDto> Get([FromRoute] int id)
        {
            var company = _insuranceCompanyService.Get(id);
            return Ok(company);
        }
    }
}
