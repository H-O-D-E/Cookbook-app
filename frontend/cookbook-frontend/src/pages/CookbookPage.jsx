import Cookbook from "@/components/Cookbook";
import CreateNewRecipebook from "@/components/CreateNewRecipebook";
import { useGetAllRecipebooks } from "@/hooks/cookbook/useGetAllCookbooks";
import { HashLoader } from "react-spinners";
function CookbookPage() {
  const { data: cookbooks, isLoading, isError, error } = useGetAllRecipebooks();

  // TODO
  if (isLoading) {
    return (
      <HashLoader
        className="flex min-h-dvw items-center justify-center"
        color="#a3e635"
        size={68}
      />
    );
  }

  // TODO
  if (isError) {
    return <p>{error.message}</p>;
  }

  return (
    <div className="w-4/5 mx-auto p-10 min-h-dvh ">
      <h1 className="text-5xl text-foreground font-semibold mb-20">
        My cookbooks
      </h1>
      <div className="grid grid-cols-1 md:grid-cols-2 2xl:grid-cols-3 min-[2500px]:grid-cols-4 gap-10">
        <CreateNewRecipebook />

        {cookbooks?.map((cb) => (
          <Cookbook key={cb.recipeBookId} cookbook={cb} />
        ))}
      </div>
    </div>
  );
}

export default CookbookPage;
