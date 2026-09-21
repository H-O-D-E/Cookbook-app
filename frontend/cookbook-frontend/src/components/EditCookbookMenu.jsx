import { deleteRecipeBook } from "@/api/recipeBookApi/recipeBookApi";
import { useDeleteCookbook } from "@/hooks/cookbook/useDeleteCookbook";
import { Ellipsis } from "lucide-react";

function EditCookbookMenu({ cookbookInfo }) {
  const deleteCookbook = useDeleteCookbook();

  return (
    <div className="dropdown dropdown-end absolute right-4">
      <button tabIndex={0} role="button" className=" text-foreground">
        <Ellipsis />
      </button>

      <ul
        tabIndex={0}
        className="dropdown-content menu bg-surface rounded-box z-10 w-52 p-2 shadow-sm font-extrabold"
      >
        <li>
          <button className="hover:bg-background">Edit cookbook</button>
        </li>
        <li>
          <button
            className="hover:bg-background"
            onClick={() => deleteCookbook.mutate(cookbookInfo.recipeBookId)}
          >
            Delete cookbook
          </button>
        </li>
      </ul>
    </div>
  );
}

export default EditCookbookMenu;
