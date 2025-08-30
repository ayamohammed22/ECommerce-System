namespace Catalog.Core.Specification
{
    public class CatalogSpecParms
    {
        private const int MaxPageSize = 80;
        private int _pageSize = 10;
        public int PageIndex { get; set; } = 1;
        public int PageSize
        {
            get => _pageSize;
            set => _pageSize = (value > MaxPageSize || value <= 0) ? MaxPageSize : value;
        }
        public string? BrandId { get; set; }
        public string? TypeId { get; set; }
        public string? SortBy { get; set; }
        public string? Search { get; set; }
    }
}
