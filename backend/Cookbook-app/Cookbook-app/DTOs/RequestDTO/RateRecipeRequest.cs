using System.ComponentModel.DataAnnotations;

namespace Cookbook_app.DTOs.RequestDTO;

public record RateRecipeRequest
{
    [Range(1, 5)]
    public int Score { get; set; }
}