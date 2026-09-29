import { useState } from "react";
import Modal from "./Modal";
import RecipeDetails from "./RecipeDetails";
import RecipeDetailsWithRating from "./RecipeDetailsWithRating";
import { Star, StarCheck, ThumbsUp } from "lucide-react";

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
            className="card w-full overflow-hidden border-2 bg-surface shadow-sm"
          >
            <figure className="h-48 w-full">
              <img
                src={recipe.imageUrl}
                alt={recipe.name}
                className="h-full w-full object-cover"
              />
            </figure>

            <div className="card-body flex flex-1 flex-col p-5">
              <h2 className="card-title line-clamp-2 text-xl text-foreground">
                {recipe.name}
              </h2>

              <p className="line-clamp-2 font-extrabold text-muted">
                {recipe.description}
              </p>

              <div className="card-actions mt-auto flex w-full items-center justify-between pt-4">
                <div className="flex items-center gap-2">
                  <Star className="" />
                  <span className="text-lg">
                    {recipe.recipeScore === 0
                      ? "Unrated"
                      : recipe.recipeScore.toFixed(1)}
                  </span>
                </div>

                <button
                  className="btn border-0 bg-call-to-action"
                  onClick={() => setSelectedRecipe(recipe)}
                >
                  Discover
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
        {selectedRecipe && <RecipeDetailsWithRating recipe={selectedRecipe} />}
      </Modal>
    </>
  );
}

export default ExploreRecipeList;
