namespace Cookbook_app.DTOs.RequestDTO;


public record PagedResult<T>(
    List<T> Items,
    int TotalCount,
    int Page,
    int PageSize
);