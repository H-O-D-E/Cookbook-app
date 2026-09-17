import ExampleCard from "@/components/ExampleCard";

function CookbookPage() {
  return (
    <div className="w-4/5 mx-auto p-10 min-h-dvh ">
      <h1 className="text-5xl text-foreground font-semibold mb-20">
        My cookbooks
      </h1>
      <div className="grid grid-cols-1 md:grid-cols-2 2xl:grid-cols-3 min-[2500px]:grid-cols-4 gap-5">
        <ExampleCard></ExampleCard>
        <ExampleCard></ExampleCard>
        <ExampleCard></ExampleCard>
        <ExampleCard></ExampleCard>
        <ExampleCard></ExampleCard>
        <ExampleCard></ExampleCard>
        <ExampleCard></ExampleCard>
      </div>
    </div>
  );
}

export default CookbookPage;
