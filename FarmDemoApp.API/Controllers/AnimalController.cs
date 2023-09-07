using FarmDemoApp.API.ApiModels;
using FarmDemoApp.BL;
using FarmDemoApp.BL.Handlers.Animal;
using FarmDemoApp.BL.Models.Common;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FarmDemoApp.API.Controllers;

[ApiController]
[Route("api/v1/animals")]
public class AnimalController : ControllerBase
{
    private readonly ISender _sender;

    public AnimalController(ISender sender)
    {
        _sender = sender;
    }

    [HttpGet]
    [ProducesResponseType(typeof(PageModel<AnimalModel>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> GetAnimals([FromQuery] GetVisitsApiModel model)
    {
        var query = new GetAnimalPageQuery { Take = model.Take, Name = model.Name, Skip = model.Skip };
        var result = await _sender.Send(query);
        return Ok(result);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> CreateAnimal([FromBody] CreateUpdateAnimalApiModel model)
    {
        var command = new CreateAnimalCommand { Name = model.Name };
        await _sender.Send(command);
        return Ok();
    }

    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> UpdateAnimal(int id, [FromBody]CreateUpdateAnimalApiModel model)
    {
        var commad = new UpdateAnimalCommand { Id = id, Name = model.Name };
        await _sender.Send(commad);
        return Ok();
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> DeleteAnimal(int id)
    {
        var commad = new DeleteAnimalCommand { Id = id };
        await _sender.Send(commad);
        return Ok();
    }
}
