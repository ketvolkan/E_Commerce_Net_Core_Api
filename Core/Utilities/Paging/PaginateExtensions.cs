namespace Core.Utilities.Paging;

using System.Collections.Generic;
using System.Linq;

public static class PaginateExtensions
{
    public static PagedResult<T> ToPagedResult<T>(this IEnumerable<T> source, int pageNumber, int pageSize)
    {
        var list = source as IList<T> ?? source.ToList();
        var totalCount = list.Count;
        var safePageNumber = pageNumber < 1 ? 1 : pageNumber;
        var safePageSize = pageSize < 1 ? 10 : pageSize;
        var items = list.Skip((safePageNumber - 1) * safePageSize).Take(safePageSize).ToList();

        return new PagedResult<T>(items, totalCount, safePageNumber, safePageSize);
    }

    public static PagedResult<T> ToPagedResult<T>(this IQueryable<T> source, int pageNumber, int pageSize)
    {
        var safePageNumber = pageNumber < 1 ? 1 : pageNumber;
        var safePageSize = pageSize < 1 ? 10 : pageSize;
        var totalCount = source.Count();
        var items = source.Skip((safePageNumber - 1) * safePageSize).Take(safePageSize).ToList();

        return new PagedResult<T>(items, totalCount, safePageNumber, safePageSize);
    }
}
