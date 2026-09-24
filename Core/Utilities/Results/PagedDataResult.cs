namespace Core.Utilities.Results;

using System.Collections.Generic;
using Core.Utilities.Paging;

public class PagedDataResult<T> : SuccessDataResult<PagedResult<T>>
{
    public PagedDataResult(List<T> items, int totalCount, int pageNumber, int pageSize, string message = "")
        : base(new PagedResult<T>(items, totalCount, pageNumber, pageSize), message)
    {
    }

    public PagedDataResult(PagedResult<T> pagedResult, string message = "")
        : base(pagedResult, message)
    {
    }
}
