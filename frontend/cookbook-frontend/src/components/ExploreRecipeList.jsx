import { useState } from "react";
import Modal from "./Modal";
import RecipeDetails from "./RecipeDetails";

function ExploreRecipeList({ recipes }) {
  const [selectedRecipe, setSelectedRecipe] = useState(null);

  if (!recipes?.length) {
    return <p className="text-lg font-semibold">No recipes are made</p>;
  }

  return (
    <>
      <div className="grid grid-cols-1 sm:grid-cols-2 lg:grid-cols-3 2xl:grid-cols-4 gap-6 text-foreground">
        {recipes.map((recipe) => (
          <div
            key={recipe.recipeId}
            className="card w-full bg-surface border-2 shadow-sm overflow-hidden"
          >
            <figure className="w-full h-48">
              <img
                src={recipe.imageUrl}
                alt={recipe.name}
                className="w-full h-full object-cover"
              />
            </figure>

            <div className="card-body p-5">
              <h2 className="card-title text-foreground text-xl line-clamp-2">
                {recipe.name}
              </h2>

              <p className="font-extrabold text-muted line-clamp-2">
                {recipe.description}
              </p>

              <div className="card-actions justify-end mt-auto">
                <button
                  className="btn bg-call-to-action border-0"
                  onClick={() => setSelectedRecipe(recipe)}
                >
                  Details
                </button>
              </div>
            </div>
          </div>
        ))}
      </div>

      <Modal
        isOpen={selectedRecipe !== null}
        onClose={() => setSelectedRecipe(null)}
      >
        {selectedRecipe && <RecipeDetails recipe={selectedRecipe} />}
      </Modal>
    </>
  );
}

export default ExploreRecipeList;
