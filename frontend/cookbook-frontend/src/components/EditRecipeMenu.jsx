import { Ellipsis } from "lucide-react";
import { useState } from "react";
import Modal from "./Modal";
import { useDeleteRecipe } from "@/hooks/recipes/useDeleteRecipe";
import { useParams } from "react-router";
import EditRecipe from "./EditRecipe";

function EditRecipeMenu({ recipe }) {
  const { recipeBookId } = useParams();
  const deleteRecipe = useDeleteRecipe(recipeBookId);
  const [isModalOpen, setIsModalOpen] = useState(false);

  return (
    <>
      <Modal isOpen={isModalOpen} onClose={() => setIsModalOpen(false)}>
        <EditRecipe
          recipe={recipe}
          recipeBookId={recipeBookId}
          onSuccess={() => setIsModalOpen(false)}
        />
      </Modal>
      <div className="dropdown dropdown-end absolute right-4">
        <button tabIndex={0} role="button" className="text-foreground">
          <Ellipsis />
        </button>

        <ul
          tabIndex={0}
          className="dropdown-content menu bg-surface rounded-box z-10 w-52 p-2 shadow-sm font-extrabold"
        >
          <li>
            <button
              className="hover:bg-background"
              onClick={() => setIsModalOpen(true)}
            >
              Edit recipe
            </button>
          </li>

          <li>
            <button
              className="hover:bg-background"
              onClick={() => deleteRecipe.mutate(recipe.recipeId)}
            >
              Delete recipe
            </button>
          </li>
        </ul>
      </div>
    </>
  );
}

export default EditRecipeMenu;
