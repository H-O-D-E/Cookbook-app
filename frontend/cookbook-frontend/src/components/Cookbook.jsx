import EditCookbookMenu from "./EditCookbookMenu";
import { useNavigate } from "react-router";
import { Star } from "lucide-react";

function Cookbook({ cookbook }) {
  const navigate = useNavigate();

  function handleViewRecipes() {
    navigate(`/cookbooks/${cookbook.recipeBookId}/recipes`);
  }

  return (
    <div className="card  bg-surface border-2 w-auto shadow-sm ">
      <figure className="h-64 w-full">
        <img
          src={cookbook.imageUrl}
          alt={cookbook.name}
          className="w-full h-full object-cover"
        />
      </figure>
      <div className="card-body">
        <EditCookbookMenu cookbookInfo={cookbook} />

        <h2 className="card-title text-foreground text-3xl font-extrabold">
          {cookbook?.name}
        </h2>
        <p className="font-extrabold text-xl text-muted">
          {cookbook.description}
        </p>
        <div className="card-actions justify-between items-end">
          <div className="flex items-center gap-2  text-lg ">
            {cookbook.recipeBookScore > 0 ? (
              <>
                <Star className="" />
                {cookbook.recipeBookScore.toFixed(1)}
              </>
            ) : (
              "No ratings yet"
            )}
          </div>

          <button
            className="btn btn-primary bg-call-to-action border-0 p-4 hover:bg-accent-color"
            onClick={handleViewRecipes}
            title="View recipes in cookbook"
          >
            View
          </button>
        </div>
      </div>
    </div>
  );
}

export default Cookbook;
