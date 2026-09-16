namespace Cookbook_app.DTOs.RequestDTO;

// REVIEW(api): no validation attributes at all, so an empty name or a 50,000 character instruction body is accepted. [Required], [StringLength] and friends on the record components are honoured automatically by [ApiController] and cost nothing.
// REVIEW(sec): RecipebookId in the request body is the id the client should not be choosing unchecked. See the note on RecipeController.
public record CreateRecipeRequest(string RecipeName, string Description, string Ingredients, string Instructions, int RecipebookId);