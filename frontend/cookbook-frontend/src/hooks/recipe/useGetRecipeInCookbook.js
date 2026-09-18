import { getRecipesByRecipeBookId } from "@/api/recipeApi/recipeApi";
import { useQuery } from "@tanstack/react-query";

export function useGetAllRecipebooks(id) {
  return useQuery({
    queryKey: ["recipes", id],
    queryFn: getRecipesByRecipeBookId(id),
  });
}
