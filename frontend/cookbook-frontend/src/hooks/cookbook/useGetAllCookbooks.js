import { getAllRecipeBooks } from "@/api/recipeBookApi/recipeBookApi";
import { useMutation, useQuery, useQueryClient } from "@tanstack/react-query";

export function useGetAllRecipebooks() {
  return useQuery({
    queryKey: ["recipebooks"],
    queryFn: getAllRecipeBooks,
  });
}
