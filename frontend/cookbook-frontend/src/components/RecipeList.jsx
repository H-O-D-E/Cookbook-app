import { useState } from "react";
import Modal from "./Modal";
import RecipeDetails from "./RecipeDetails";
import EditRecipeMenu from "./EditRecipeMenu";
import { Eye, Lock, LockOpen, Star } from "lucide-react";
import { useUpdateRecipeVisibility } from "@/hooks/recipes/useUpdateRecipe";

function RecipeList({ recipes, recipeBookId }) {
  const [selectedRecipe, setSelectedRecipe] = useState(null);
  const changeRecipeVisibility = useUpdateRecipeVisibility(recipeBookId);

  function handleChangeVisibility(recipeId, isPublic) {
    changeRecipeVisibility.mutate({
      recipeId,
      isPublic: !isPublic,
    });
  }

  console.log(recipes);

  if (!recipes?.length) {
    return (
      <p className="text-lg font-semibold">
        No recipes in this cookbook yet :( Make one!
      </p>
    );
  }

  return (
    <>
      <div className="w-full lg:w-2/3 mx-auto flex flex-col gap-6 text-foreground">
        {recipes.map((recipe) => (
          <div
            key={recipe.recipeId}
            className="
              card
              md:card-side
              w-full
              md:h-64
              bg-surface
              border-2
              shadow-sm
              overflow-hidden
            "
          >
            <figure
              className="
                w-full
                h-52
                md:w-64
                lg:w-80
                md:h-full
                shrink-0
              "
            >
              <img
                src={recipe.imageUrl}
                alt={recipe.name}
                className="w-full h-full object-cover"
              />
            </figure>

            <div className="card-body p-5 md:p-8 ">
              <div className="flex items-center justify-end gap-2">
                <button
                  disabled={changeRecipeVisibility.isPending}
                  onClick={() =>
                    handleChangeVisibility(recipe.recipeId, recipe.isPublic)
                  }
                  title="Set recipe public or private"
                  className="cursor-pointer"
                >
                  {!recipe.isPublic ? (
                    <Lock
                      className="h-5 w-5 shrink-0"
                      title="ed"
                      strokeWidth={3}
                    />
                  ) : (
                    <LockOpen
                      className="h-5 w-5 shrink-0"
                      title="ed"
                      strokeWidth={3}
                    />
                  )}
                </button>
                <EditRecipeMenu recipe={recipe} />
              </div>
              <h2 className="card-title text-foreground text-2xl md:text-4xl line-clamp-1">
                {recipe.name}
              </h2>
              <h3 className="card-title text-muted text-xl">
                {recipe.description}
              </h3>

              <div className="card-actions mt-auto flex w-full items-center justify-between pt-4">
                <div className="flex items-center gap-2">
                  <Star />
                  <span className="text-lg">
                    {recipe.recipeScore === 0
                      ? "Unrated"
                      : recipe.recipeScore.toFixed(1)}
                  </span>
                </div>
                <div className="flex gap-3 items-center">
                  <button
                    className="btn border-0 bg-call-to-action hover:bg-accent-color"
                    title="See more details about this recipe"
                    onClick={() => setSelectedRecipe(recipe)}
                  >
                    See more
                  </button>
                </div>
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

export default RecipeList;
