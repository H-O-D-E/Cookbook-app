import { useState } from "react";
import { useCreateRecipe } from "@/hooks/recipes/UseCreateRecipe";

function CreateNewRecipe({ recipeBookId, onSuccess }) {
    const [name, setName] = useState("");
    const [description, setDescription] = useState("");
    const [imageUrl, setImageUrl] = useState("");
    const [ingredients, setIngredients] = useState("");
    const [instructions, setInstructions] = useState("");
    const createRecipe = useCreateRecipe(recipeBookId);

    const handleSubmit = (e) => {
        e.preventDefault();

        createRecipe.mutate({
            name,
            description,
            imageUrl,
            ingredients,
            instructions,
            recipeBookId: Number(recipeBookId),
        }, {
            onSuccess: () => {
                onSuccess?.();
            },
        });
    }

  return (
    <form className="w-full" onSubmit={handleSubmit}>
      <fieldset className="fieldset bg-surface text-foreground rounded-2xl w-full p-10 text-lg">
        <div className="mb-5 text-center">
          <h1 className="text-4xl font-bold">Create a new recipe</h1>

          <p className="mt-1 text-md font-normal text-muted">
            Please fill out the form to create a new recipe
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
          className="input input-lg w-full bg-surface-secondary border-border text-foreground"
          value={description}
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

        <input
          type="text"
          value={ingredients}
          onChange={(e) => setIngredients(e.target.value)}
          className="input input-lg w-full bg-surface-secondary border-border text-foreground"
          required
        />

        <label className="label mt-3 font-semibold text-foreground">
          Instructions
        </label>

        <input
          type="text"
          value={instructions}
          onChange={(e) => setInstructions(e.target.value)}
          className="input input-lg w-full bg-surface-secondary border-border text-foreground"
          required
        />

        <button
          className="btn mt-7 w-full border-0 bg-call-to-action text-white font-bold"
          type="submit"
          disabled={createRecipe.isPending}
        >
          Save
        </button>
      </fieldset>
    </form>
  );
}

export default CreateNewRecipe;