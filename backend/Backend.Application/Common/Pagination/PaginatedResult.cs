namespace Backend.Application.Common.Pagination;

using System.Collections.Generic;

public record PaginatedResult<TPaginatedItem>(
    IReadOnlyList<TPaginatedItem> Items,
    int TotalCount);