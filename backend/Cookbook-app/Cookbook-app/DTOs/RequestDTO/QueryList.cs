using System.ComponentModel.DataAnnotations;

namespace Cookbook_app.DTOs.RequestDTO;

public class ListQuery
{
    [Range(1, 100000)]
    public int Page { get; set; } = 1;

    [Range(1, 100)]
    public int PageSize { get; set; } = 12;

    [RegularExpression("^(asc|desc)$")]
    public string Sort { get; set; } = "asc";

    public string? Tag { get; set; }
}