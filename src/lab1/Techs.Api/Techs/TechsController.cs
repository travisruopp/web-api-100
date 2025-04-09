using FluentValidation;
using Marten;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Techs.Api.Techs;

public class TechsController(IDocumentSession documentSession) : ControllerBase
{
    [HttpPost("/techs")]
    public async Task<ActionResult> AddATech([FromBody] TechCreateModel request, [FromServices] IValidator<TechCreateModel> validator)
    {
        
        if (validator.Validate(request).IsValid == false)
        {
            return BadRequest();
        }
        var entityToSave = new TechEntity 
        {
            Id = Guid.NewGuid(),
            FirstName = request.FirstName,
            LastName = request.LastName,
            Sub = request.Sub,
            Email = request.Email,
            Phone = request.Phone
        };
        documentSession.Store(entityToSave);
        await documentSession.SaveChangesAsync();

        return Created($"/techs/{entityToSave.Id}",entityToSave);
    }

    [HttpGet("/techs/{id:guid}")]
    public async Task<ActionResult> GetTechById(Guid id)
    {
        var response = await documentSession.Query<TechEntity>().SingleOrDefaultAsync(v=>v.Id == id);
        if (response is null)
        {
            return NotFound();
        }
        else
        {
            return Ok(response);
        }
    }
}