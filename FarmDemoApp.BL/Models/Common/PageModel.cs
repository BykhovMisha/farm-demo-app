namespace FarmDemoApp.BL.Models.Common;

public class PageModel<T>
{
    public int TotalCount { get; init; }

    public List<T> Items { get; init; } = null!;
}
