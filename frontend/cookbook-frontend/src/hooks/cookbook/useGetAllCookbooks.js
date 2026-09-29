import { getAllRecipeBooks } from "@/api/recipeBookApi/recipeBookApi";
import { useMutation, useQuery, useQueryClient } from "@tanstack/react-query";

export function useGetAllRecipebooks(options = {}) {
  return useQuery({
    queryKey: ["recipebooks", options],
    queryFn: () => getAllRecipeBooks(options),
  });
}
