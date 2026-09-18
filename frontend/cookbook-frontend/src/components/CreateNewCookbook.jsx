import { useCreateCookbook } from "@/hooks/cookbook/useCreateCookbook";
import { useState } from "react";

function CreateNewCookbook({ onSuccess }) {
  const [recipeBookName, setRecipeBookName] = useState("");
  const [description, setDescription] = useState("");
  const [imageUrl, setImageUrl] = useState("");

  const createCookbook = useCreateCookbook();

  function handleSubmit(e) {
    e.preventDefault();

    createCookbook.mutate(
      {
        recipeBookName,
        description,
        imageUrl,
      },
      {
        onSuccess: () => {
          onSuccess?.();
        },
      },
    );
  }

  return (
    <form className="w-full" onSubmit={handleSubmit}>
      <fieldset className="fieldset bg-surface text-foreground rounded-2xl w-full p-10 text-lg">
        <div className="mb-5 text-center">
          <h1 className="text-4xl font-bold">Create a new cookbook</h1>

          <p className="mt-1 text-md font-normal text-muted">
            Please fill out the form to create a new cookbook
          </p>
        </div>

        <label className="label font-semibold text-foreground">
          Cookbook name
        </label>

        <input
          type="text"
          className="input input-lg w-full bg-surface-secondary border-border text-foreground"
          value={recipeBookName}
          onChange={(e) => setRecipeBookName(e.target.value)}
          required
        />

        <label className="label mt-3 font-semibold text-foreground">
          Description
        </label>

        <input
          type="text"
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

        <button
          className="btn mt-7 w-full border-0 bg-call-to-action text-white font-bold"
          type="submit"
          disabled={createCookbook.isPending}
        >
          Save
        </button>
      </fieldset>
    </form>
  );
}

export default CreateNewCookbook;
