using DotnetMongo.Models;
using DotnetMongo.Services;
using Microsoft.AspNetCore.Mvc;

namespace DotnetMongo.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CompanyController : ControllerBase 
{
    private readonly CompanyService _companyService;

    public CompanyController(CompanyService companyService) => 
       _companyService = companyService;

    [HttpGet] 
    public async Task<List<Company>> Get () => 
       await _companyService.GetAsync();

    [HttpGet("{id:length(24)}")]
    public async Task<ActionResult<Company>> Get(string id) 
    {
        var company = await _companyService.GetAsync(id);

        if ( company is null ) 
        {
            return NotFound();
        }

        return company;
    }

    [HttpPost]
    public async Task<IActionResult> Post(Company newCompany) 
    {
      await _companyService.CreateAsync(newCompany);

      return CreatedAtAction(nameof(Get) , new { id = newCompany.Id}, newCompany);
    }

    [HttpPut("{id:length(24)}")]
    public async Task<IActionResult> Update(string id, Company updatedCompnay)
    {
        var company = await _companyService.GetAsync(id);

        if (company is null)
        {
            return NotFound();
        }

        updatedCompnay.Id = company.Id;

        await _companyService.UpdateAsync(id, updatedCompnay);

        return NoContent();
    }

    [HttpDelete("{id:length(24)}")]
    public async Task<IActionResult> Delete(string id)
    {
        var company = await _companyService.GetAsync(id);

        if (company is null)
        {
            return NotFound();
        }

        await _companyService.RemoveAsync(id);

        return NoContent();

    }
}


