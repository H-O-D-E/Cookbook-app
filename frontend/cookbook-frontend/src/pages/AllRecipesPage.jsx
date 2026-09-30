import { HashLoader } from "react-spinners";
import { useGetOtherUsersRecipes } from "@/hooks/recipes/useGetRecipes";
import ExploreRecipeList from "@/components/ExploreRecipeList";
import {useState} from "react";


function AllRecipesPage() {
  const [options, setOptions] = useState({
    page: 1,
    pageSize: 12,
    sort: "asc",
    tag: "",
  });
  const {
    data: recipes,
    isLoading,
    isFetching,
    isError,
    error,
  } = useGetOtherUsersRecipes(options);
  
  
 
  const totalPages = Math.max(
      1,
      Math.ceil((recipes?.totalCount ?? 0) / options.pageSize)
  );


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
      <h1 className="text-3xl lg:text-5xl items-center flex justify-center font-extrabold underline text-call-to-action pb-20 ">
        Explore and rate other recipes
      </h1>

      <div className="mb-8 flex flex-wrap gap-4 text-foreground">
        <label>
          Sort
          <select
              className="select ml-2 bg-surface text-foreground border-border"
              value={options.sort}
              onChange={(event) =>
                  setOptions((previous) => ({
                    ...previous,
                    sort: event.target.value,
                    page: 1,
                  }))
              }
          >
            <option value="asc">A–Z</option>
            <option value="desc">Z–A</option>
            <option value="rating">Highest rated</option>
          </select>
        </label>

        <label>
          Tag
          <select
              className="select ml-2 bg-surface text-foreground border-border"
              value={options.tag}
              onChange={(event) =>
                  setOptions((previous) => ({
                    ...previous,
                    tag: event.target.value,
                    page: 1,
                  }))
              }
          >
            <option value="">All tags</option>
            <option value="Breakfast">Breakfast</option>
            <option value="Dinner">Dinner</option>
            <option value="Dessert">Dessert</option>
          </select>
        </label>
      </div>

      <ExploreRecipeList recipes={recipes?.items?? []} />
      <nav
        aria-label="Recipe pages"
        className="flex items-center justify-center gap-4 mt-8"
      >
        <button
            className="btn"
            disabled={options.page <= 1 || isFetching}
            onClick={() =>
                setOptions((previous) => ({ ...previous, page: previous.page - 1 }))
            }
        >
          Previous
        </button>
        <span>Page {options.page} of {totalPages}</span>
        <button
            className="btn"
            disabled={options.page >= totalPages || isFetching}
            onClick={() =>
                setOptions((previous) => ({ ...previous, page: previous.page + 1 }))
            }
        >
          Next
        </button>
      </nav>
    

    
  
    </div>
        
      
  );
}

export default AllRecipesPage;
