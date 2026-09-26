import { useState } from "react";
import { useParams } from "react-router";
import { HashLoader } from "react-spinners";
import { useGetRecipes } from "@/hooks/recipes/useGetRecipes";
import { useGetCookbook } from "@/hooks/cookbook/useGetCookBook";
import CreateNewRecipe from "@/components/CreateNewRecipe";
import RecipeList from "@/components/RecipeList";
import Modal from "@/components/Modal";

function RecipesPage() {
  const { recipeBookId } = useParams();
  const {
    data: recipes,
    isLoading,
    isError,
    error,
  } = useGetRecipes(recipeBookId);
  const [isCreateModalOpen, setIsCreateModalOpen] = useState(false);

  const { data: cookbook } = useGetCookbook(recipeBookId);

  if (isLoading) {
    return (
      <div className="min-h-dvh flex items-center justify-center">
        <HashLoader color="#a3e635" size={68} />
      </div>
    );
  }

  if (isError) {
    return <p>{error.message}</p>;
  }

  return (
    <div className="w-4/5 mx-auto p-10 min-h-dvh ">
      <div className="mb-20 flex justify-between">
        <h1 className="text-5xl text-call-to-action font-extrabold">
          Cookbook: {cookbook?.name}
        </h1>
        <button
          className="btn text-white bg-call-to-action border-0 font-extrabold text-md"
          onClick={() => setIsCreateModalOpen(true)}
        >
          Create a new recipe
        </button>
      </div>
      <RecipeList recipes={recipes} />

      <Modal
        isOpen={isCreateModalOpen}
        onClose={() => setIsCreateModalOpen(false)}
      >
        <CreateNewRecipe
          recipeBookId={recipeBookId}
          onSuccess={() => setIsCreateModalOpen(false)}
        />
      </Modal>
    </div>
  );
}

export default RecipesPage;
