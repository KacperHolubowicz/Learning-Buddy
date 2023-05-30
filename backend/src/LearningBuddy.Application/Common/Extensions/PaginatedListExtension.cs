using Microsoft.EntityFrameworkCore;

namespace LearningBuddy.Application.Common.Extensions
{
    internal static class PaginatedListExtension
    {
        internal static Task<PaginatedList<T>> PaginatedListAsync<T>(this IQueryable<T> items, int page, int pageSize)
            where T : class
        {
            return PaginatedList<T>.CreateAsync(items.AsNoTracking(), page, pageSize);
        }
    }
}
