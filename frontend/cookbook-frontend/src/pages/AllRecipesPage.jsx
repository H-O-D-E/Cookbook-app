import { HashLoader } from "react-spinners";
import { useGetOtherUsersRecipes } from "@/hooks/recipes/useGetRecipes";
import ExploreRecipeList from "@/components/ExploreRecipeList";

function AllRecipesPage() {
  const {
    data: recipes,
    isLoading,
    isError,
    error,
  } = useGetOtherUsersRecipes();

  if (isLoading) {
    return (
      <div className="min-h-dvh flex items-center justify-center">
        <HashLoader color="#000000" size={68} />
      </div>
    );
  }

  if (isError) {
    return <p role="alert">{error.message}</p>;
  }

  return (
    <div className="w-4/5 mx-auto p-10 ">
      <h1 className="text-3xl lg:text-5xl items-center flex justify-center font-extrabold text-call-to-action pb-20 ">
        Find new recipes and share your ratings
      </h1>

      <ExploreRecipeList recipes={recipes} />
    </div>
  );
}

export default AllRecipesPage;
