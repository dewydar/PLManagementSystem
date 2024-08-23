using Microsoft.EntityFrameworkCore;

namespace PLManagementSystem.Helpers.Helpers
{
    public class PagedList<T> : List<T>
    {

        public int CurrentPage { get; set; }
        public int TotalPages { get; set; }
        public int PageSize { get; set; }
        public int TotalCount { get; set; }
        public PagedList(List<T> items, int count, int PageNumber, int Pagesize)
        {
            TotalCount = count;
            PageSize = Pagesize;
            CurrentPage = PageNumber;
            if (PageNumber <= 0 || Pagesize <= 0)
                TotalPages = 0;
            else
                TotalPages = (int)Math.Ceiling(count / (double)Pagesize);
            this.AddRange(items);
        }
        public static async Task<PagedList<T>> CreateAsync(IQueryable<T> source, int pageNumber, int pageSize)
        {
            int count = await source.CountAsync();
            if (pageNumber <= 0 || pageSize <= 0)
                return new PagedList<T>(await source.ToListAsync(), count, 0, 0);
            List<T> item = await source.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync();
            return new PagedList<T>(item, count, pageNumber, pageSize);
        }
        public static PagedList<T> CreateAsync(List<T> Source, int PageNumber, int PageSize)
        {
            int count = Source.Count();
            if (PageNumber <= 0 || PageSize <= 0)
                return new PagedList<T>(Source, count, 0, 0);
            List<T> item = Source.Skip((PageNumber - 1) * PageSize).Take(PageSize).ToList();
            return new PagedList<T>(item, count, PageNumber, PageSize);
        }

    }
}
