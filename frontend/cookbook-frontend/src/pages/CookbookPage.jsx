import Cookbook from "@/components/Cookbook";
import CreateNewCookbook from "@/components/CreateNewCookbook";

import Modal from "@/components/Modal";
import { useGetAllRecipebooks } from "@/hooks/cookbook/useGetAllCookbooks";
import { useState } from "react";
import { HashLoader } from "react-spinners";
function CookbookPage() {
  const [options, setOptions] = useState({
    page: 1,
    pageSize: 12,
    sort: "asc",
    tag: "",
  });
  const [isCreateModalOpen, setIsCreateModalOpen] = useState(false);
  const { data, isLoading, isFetching, isError, error } =
    useGetAllRecipebooks(options);
  const totalPages = Math.max(
    1,
    Math.ceil((data?.totalCount ?? 0) / options.pageSize),
  );

  // Adjust this component's state only when a refreshed list loses a page.
  if (data && !isFetching && options.page > totalPages) {
    setOptions({ ...options, page: totalPages });
  }

  if (isLoading) {
    return (
      <div className="min-h-dvh flex items-center justify-center">
        <HashLoader color="#000000" size={72} />
      </div>
    );
  }

  if (isError) {
    return <p>{error.message}</p>;
  }

  return (
    <>
      <div className="relative isolate min-h-dvh overflow-hidden">
        <div className="relative z-10 mx-auto min-h-dvh w-4/5 p-10">
          <div className="mb-20 flex justify-between">
            <h1 className=" text-3xl lg:text-5xl text-foreground font-extrabold">
              My cookbooks
            </h1>
            <button
              className="btn text-white bg-call-to-action border-0 font-extrabold text-md shadow-lg hover:scale-102 hover:bg-accent-color"
              onClick={() => setIsCreateModalOpen(true)}
              title="Create a new cookbook"
            >
              Create new cookbook
            </button>
          </div>
          <div className="mb-8 flex flex-wrap gap-4 text-foreground ">
            <label>
              Sort
              <select
                className="select ml-2 bg-surface text-foreground border"
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
              </select>
            </label>

            <label>
              Tag
              <select
                className="select ml-2 bg-surface text-foreground border"
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
          <div className="grid grid-cols-1 md:grid-cols-2 2xl:grid-cols-3 min-[2500px]:grid-cols-4 gap-10">
            {data?.items.map((cb) => (
              <Cookbook key={cb.recipeBookId} cookbook={cb} />
            ))}
          </div>
          {data?.totalCount === 0 && <p>No cookbooks match your selection.</p>}
          <nav
            aria-label="Cookbook pages"
            className="flex items-center justify-center gap-4 mt-8"
          >
            <button
              className="btn"
              disabled={options.page <= 1 || isFetching}
              onClick={() =>
                setOptions((previous) => ({
                  ...previous,
                  page: previous.page - 1,
                }))
              }
            >
              Previous
            </button>
            <span>
              Page {options.page} of {totalPages}
            </span>
            <button
              className="btn"
              disabled={options.page >= totalPages || isFetching}
              onClick={() =>
                setOptions((previous) => ({
                  ...previous,
                  page: previous.page + 1,
                }))
              }
            >
              Next
            </button>
          </nav>
          <Modal
            isOpen={isCreateModalOpen}
            onClose={() => setIsCreateModalOpen(false)}
          >
            <CreateNewCookbook onSuccess={() => setIsCreateModalOpen(false)} />
          </Modal>
        </div>
      </div>
    </>
  );
}

export default CookbookPage;
