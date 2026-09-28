function RecipeDetails({ recipe }) {
  return (
    <div className="text-foreground">
      <img
        src={recipe.imageUrl}
        alt={recipe.name}
        className="w-full h-72 object-cover rounded-xl mb-6"
      />

      <h2 className="text-4xl font-extrabold mb-2">{recipe.name}</h2>

      <p className="text-lg text-muted mb-8">{recipe.description}</p>

      <div className="mb-8">
        <h3 className="text-2xl font-bold mb-3">Ingredients</h3>

        <p className="text-lg whitespace-pre-line">{recipe.ingredients}</p>
      </div>

      <div>
        <h3 className="text-2xl font-bold mb-3">Instructions</h3>

        <p className="text-lg whitespace-pre-line">{recipe.instructions}</p>
      </div>
    </div>
  );
}

export default RecipeDetails;
