import { useDeleteCookbook } from "@/hooks/cookbook/useDeleteCookbook";
import { Ellipsis } from "lucide-react";
import { useState } from "react";
import Modal from "./Modal";
import EditCookBook from "./EditCookbook";

function EditCookbookMenu({ cookbookInfo }) {
  const deleteCookbook = useDeleteCookbook();
  const [isModalOpen, setIsModalOpen] = useState(false);

  return (
    <>
      <Modal isOpen={isModalOpen} onClose={() => setIsModalOpen(false)}>
        <EditCookBook
          cookbookInfo={cookbookInfo}
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
              Edit cookbook
            </button>
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
    </>
  );
}

export default EditCookbookMenu;
