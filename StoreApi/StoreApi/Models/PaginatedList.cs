namespace StoreApi.Models
{
    public class PaginatedList<T>: List<T>
    {
        public int PageIndex { get; set; } = 1;
        public int TotalPage { get; set; }
        
        public PaginatedList(List<T> items, int count, int pageIndex, int pageSize) 
        { 
            PageIndex= pageIndex;
            TotalPage= (int)Math.Ceiling(count/(double)pageSize);
            AddRange(items);
        }
        public static PaginatedList<T> Create(IQueryable<T> source, int pageIndex, int pagesize) 
        { 
            var count = source.Count();
            var items = source.Skip((pageIndex-1)*pagesize).Take(pagesize).ToList();
            return new PaginatedList<T>(items, count,pageIndex, pagesize);
        }
    }
}
