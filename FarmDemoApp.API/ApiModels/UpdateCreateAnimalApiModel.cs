using Microsoft.AspNetCore.Mvc;

namespace FarmDemoApp.API.ApiModels;

public class UpdateCreateAnimalApiModel
{
    [FromRoute]
    public int Id { get; init; }

    [FromBody]
    public string Name { get; init; } = string.Empty;
}
