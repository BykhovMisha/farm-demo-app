namespace FarmDemoApp.API.ApiModels;

public class GetVisitsApiModel
{
    public int Skip { get; init; }

    public int Take { get; init; }

    public string? Name { get; init; }
}
