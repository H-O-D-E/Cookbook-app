import { useDeleteCookbook } from "@/hooks/cookbook/useDeleteCookbook";
import EditCookbookMenu from "./EditCookbookMenu";

function Cookbook({ cookbook }) {
  return (
    <div className="card  bg-surface border-2 w-auto shadow-sm hover:scale-101">
      <figure className="h-64 w-full">
        <img
          src={cookbook.imageUrl}
          alt={cookbook.name}
          className="w-full h-full object-cover"
        />
      </figure>
      <div className="card-body">
        <EditCookbookMenu cookbookInfo={cookbook} />

        <h2 className="card-title text-foreground text-2xl">{cookbook.name}</h2>
        <p className="font-semibold text-lg">{cookbook.description}</p>
        <div className="card-actions justify-end">
          <button className="btn btn-primary">View</button>
        </div>
      </div>
    </div>
  );
}

export default Cookbook;
