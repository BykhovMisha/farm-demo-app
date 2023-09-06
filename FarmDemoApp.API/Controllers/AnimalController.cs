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
    [ProducesResponseType(typeof(HttpValidationProblemDetails), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> GetVisits([FromQuery] GetVisitsApiModel request)
    {
        var query = new GetAnimalPageQuery { Take = request.Take, Name = request.Name, Skip = request.Skip };
        var result = await _sender.Send(query);
        return Ok(result);
    }
}
