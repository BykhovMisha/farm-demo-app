namespace FarmDemoApp.BL.Models.Common;

public class PageModel<T>
{
    public int TotalCount { get; init; }

    public required List<T> Items { get; init; }
}
