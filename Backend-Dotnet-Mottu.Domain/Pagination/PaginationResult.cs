namespace Backend_Dotnet_Mottu.Domain.Pagination
{
    public sealed class PaginatedResult<T>
    {
   

        public IReadOnlyList<T> Items { get; set; }
        public int TotalItems { get; set; }
        public int Page { get; set; }
        public int PageSize { get; set; }
        public int TotalPages => (int)Math.Ceiling((double)TotalItems / PageSize);

        public bool HasPrevious => Page > 1;

        public bool HasNext => Page < TotalPages;
    }
}
