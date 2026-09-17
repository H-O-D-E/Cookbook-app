import { Plus, PlusCircle } from "lucide-react";

function CreateNewRecipebook() {
  return (
    <div className="card border bg-gray-50 opacity-50 w-auto shadow-sm hover:scale-105">
      <div className="card-body flex items-center justify-center">
        <Plus className="w-32 h-32 flex-1" strokeWidth={1.5} />
        <h2 className="card-title text-foreground text-3xl">
          Create new recipebook
        </h2>
      </div>
    </div>
  );
}

export default CreateNewRecipebook;
