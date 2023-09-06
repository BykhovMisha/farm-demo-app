namespace FarmDemoApp.DataAccess.Dto.Common
{
    public class PageEntity<T>
    {
        public int TotalCount { get; set; }

        public List<T> Items { get; init; } = null!;
    }
}
