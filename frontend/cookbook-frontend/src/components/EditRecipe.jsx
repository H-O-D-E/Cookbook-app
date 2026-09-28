import { useUpdateRecipe } from "@/hooks/recipes/useUpdateRecipe";
import { useState } from "react";

function EditRecipe({ recipe, recipeBookId, onSuccess }) {
  const [name, setName] = useState(recipe.name ?? "");
  const [description, setDescription] = useState(recipe.description ?? "");
  const [imageUrl, setImageUrl] = useState(recipe.imageUrl ?? "");
  const [ingredients, setIngredients] = useState(recipe.ingredients ?? "");
  const [instructions, setInstructions] = useState(recipe.instructions ?? "");

  const updateRecipe = useUpdateRecipe(recipeBookId);

  function handleSubmit(e) {
    e.preventDefault();

    updateRecipe.mutate(
      {
        recipeId: recipe.recipeId,
        name,
        description,
        imageUrl,
        ingredients,
        instructions,
      },
      {
        onSuccess: () => {
          onSuccess?.();
        },
      },
    );
  }

  return (
    <div>
      <form className="w-full" onSubmit={handleSubmit}>
        <fieldset className="fieldset bg-surface text-foreground rounded-2xl w-full p-10 text-lg">
          <div className="mb-5 text-center">
            <h1 className="text-4xl font-bold">Edit recipe</h1>

            <p className="mt-1 text-md font-normal text-muted">
              Please fill out the form to edit your recipe
            </p>
          </div>

          <label className="label font-semibold text-foreground">
            Recipe name
          </label>

          <input
            type="text"
            className="input input-lg w-full bg-surface-secondary border-border text-foreground"
            value={name}
            onChange={(e) => setName(e.target.value)}
            required
          />

          <label className="label mt-3 font-semibold text-foreground">
            Description
          </label>

          <input
            type="text"
            value={description}
            className="input input-lg w-full bg-surface-secondary border-border text-foreground"
            onChange={(e) => setDescription(e.target.value)}
            required
          />

          <label className="label mt-3 font-semibold text-foreground">
            Image URL
          </label>

          <input
            type="url"
            value={imageUrl}
            onChange={(e) => setImageUrl(e.target.value)}
            className="input input-lg w-full bg-surface-secondary border-border text-foreground"
            required
          />

          <label className="label mt-3 font-semibold text-foreground">
            Ingredients
          </label>

          <textarea
            value={ingredients}
            onChange={(e) => setIngredients(e.target.value)}
            className="textarea textarea-lg w-full bg-surface-secondary border-border text-foreground"
            required
          />

          <label className="label mt-3 font-semibold text-foreground">
            Instructions
          </label>

          <textarea
            value={instructions}
            onChange={(e) => setInstructions(e.target.value)}
            className="textarea textarea-lg w-full bg-surface-secondary border-border text-foreground"
            required
          />

          <button
            className="btn mt-7 w-full border-0 bg-call-to-action text-white font-bold"
            type="submit"
            disabled={updateRecipe.isPending}
          >
            Update recipe
          </button>
        </fieldset>
      </form>
    </div>
  );
}

export default EditRecipe;
