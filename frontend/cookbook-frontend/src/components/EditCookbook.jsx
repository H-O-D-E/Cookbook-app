import { useUpdateCookbook } from "@/hooks/cookbook/useUpdateCookbook";
import { useState } from "react";

function EditCookBook({ cookbookInfo, onSuccess }) {
  const [recipeBookName, setRecipeBookName] = useState(cookbookInfo.name ?? "");
  const [description, setDescription] = useState(
    cookbookInfo.description ?? "",
  );
  const [imageUrl, setImageUrl] = useState(cookbookInfo.imageUrl ?? "");

  const updateCookbook = useUpdateCookbook();

  function handleSubmit(e) {
    e.preventDefault();

    updateCookbook.mutate(
      {
        id: cookbookInfo.recipeBookId,
        name: recipeBookName,
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
    <div>
      <form className="w-full" onSubmit={handleSubmit}>
        <fieldset className="fieldset bg-surface text-foreground rounded-2xl w-full p-10 text-lg">
          <div className="mb-5 text-center">
            <h1 className="text-4xl font-bold">Edit cookbook</h1>

            <p className="mt-1 text-md font-normal text-muted">
              Please fill out the form to edit your cookbook
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

          <button
            className="btn mt-7 w-full border-0 bg-call-to-action text-white font-bold"
            type="submit"
            disabled={updateCookbook.isPending}
          >
            Update cookbook
          </button>
        </fieldset>
      </form>
    </div>
  );
}

export default EditCookBook;
