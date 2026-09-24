import Cookbook from "@/components/Cookbook";
import CreateNewCookbook from "@/components/CreateNewCookbook";

import Modal from "@/components/Modal";
import { useGetAllRecipebooks } from "@/hooks/cookbook/useGetAllCookbooks";
import { useState } from "react";
import { HashLoader } from "react-spinners";
function CookbookPage() {
  const { data: cookbooks, isLoading, isError, error } = useGetAllRecipebooks();
  const [isCreateModalOpen, setIsCreateModalOpen] = useState(false);

  // TODO
  if (isLoading) {
    return (
      <div className="min-h-dvh flex items-center justify-center">
        <HashLoader color="#000000" size={72} />
      </div>
    );
  }

  // TODO
  if (isError) {
    return <p>{error.message}</p>;
  }

  return (
    <div className="w-4/5 mx-auto p-10 min-h-dvh ">
      <div className="mb-20 flex justify-between">
        <h1 className="text-5xl text-call-to-action font-extrabold">
          My cookbooks
        </h1>
        <button
          className="btn text-white bg-call-to-action border-0 font-extrabold text-md shadow-lg hover:scale-102"
          onClick={() => setIsCreateModalOpen(true)}
        >
          Create new cookbook
        </button>
      </div>
      <div className="grid grid-cols-1 md:grid-cols-2 2xl:grid-cols-3 min-[2500px]:grid-cols-4 gap-10">
        {cookbooks?.map((cb) => (
          <Cookbook key={cb.recipeBookId} cookbook={cb} />
        ))}
      </div>
      <Modal
        isOpen={isCreateModalOpen}
        onClose={() => setIsCreateModalOpen(false)}
      >
        <CreateNewCookbook onSuccess={() => setIsCreateModalOpen(false)} />
      </Modal>
    </div>
  );
}

export default CookbookPage;
