using Microsoft.EntityFrameworkCore;

namespace LearningBuddy.Application.Common
{
    public class PaginatedList<T> where T : class
    {
        public IEnumerable<T> PaginatedProperty { get; }
        public int Page { get; }
        public int TotalPages { get; }
        public int PropertyCount { get; }
        public bool HasPrevious => Page > 1;
        public bool HasNext => Page < TotalPages;

        private PaginatedList(IEnumerable<T> properties, int page, int pageSize, int count)
        {
            PaginatedProperty = properties;
            Page = page;
            TotalPages = (int)Math.Ceiling(count / (double)pageSize);
            PropertyCount = count;
        }

        public async static Task<PaginatedList<T>> CreateAsync(IQueryable<T> items, int pageNumber, int pageSize)
        {
            int count = await items.CountAsync();
            var properties = Enumerable.AsEnumerable
                (items.Skip((pageNumber - 1) * pageSize).Take(pageSize));

            return new PaginatedList<T>(properties, pageNumber, pageSize, count);
        }
    }
}
