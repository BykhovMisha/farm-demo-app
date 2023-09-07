namespace FarmDemoApp.DataAccess.Dto.Common
{
    public class PageDto<T>
    {
        public int TotalCount { get; set; }

        public required List<T> Items { get; init; }
    }
}
