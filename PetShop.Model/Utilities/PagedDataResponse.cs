namespace PetShop.Model.Utilities
{
    public class PagedDataResponse<T> where T : class
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public int TotalCount { get; set; }
        public int TotalPages
        {
            get
            {
                return TotalCount / PageSize + (TotalCount % PageSize == 0 ? 0 : 1);
            }
        }
        public int From
        {
            get
            {
                return ((PageNumber - 1) * PageSize) + 1;
            }
        }
        public int To
        {
            get
            {
                return Math.Min(PageNumber * PageSize, TotalCount);
            }
        }
        public IEnumerable<T>? Data { get; set; }
    }
}
