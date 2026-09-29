import { useCreateCookbook } from "@/hooks/cookbook/useCreateCookbook";
import { useState } from "react";

function CreateNewCookbook({ onSuccess }) {
  const [recipeBookName, setRecipeBookName] = useState("");
  const [description, setDescription] = useState("");
  const [imageUrl, setImageUrl] = useState(
    "" ??
      "https://media.istockphoto.com/id/1980276924/vector/no-photo-thumbnail-graphic-element-no-found-or-available-image-in-the-gallery-or-album-flat.jpg?s=612x612&w=0&k=20&c=ZBE3NqfzIeHGDPkyvulUw14SaWfDj2rZtyiKv3toItk=",
  );
  const [tag, setTag] = useState("")

  const createCookbook = useCreateCookbook();

  function handleSubmit(e) {
    e.preventDefault();

    createCookbook.mutate(
      {
        recipeBookName,
        description,
        imageUrl,
        tag,
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
        />
        <label className="text-foreground">
          Tag
          <select
              className="select bg-surface text-foreground border-border"
              value={tag}
              onChange={(event) => setTag(event.target.value)}
          >
            <option value="">No tag</option>
            <option value="Breakfast">Breakfast</option>
            <option value="Dinner">Dinner</option>
            <option value="Dessert">Dessert</option>
          </select>
        </label>

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
