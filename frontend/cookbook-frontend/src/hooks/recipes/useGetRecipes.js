import { useQuery } from "@tanstack/react-query";
import {
  getOtherUsersRecipes,
  getRecipesByRecipeBookId,
} from "@/api/recipeApi/recipeApi";

export function useGetRecipes(recipeBookId) {
  return useQuery({
    queryKey: ["recipebooks", recipeBookId, "recipes"],
    queryFn: () => getRecipesByRecipeBookId(recipeBookId),
    enabled: !!recipeBookId,
  });
}

export function useGetOtherUsersRecipes() {
  return useQuery({
    queryKey: ["recipes", "explore"],
    queryFn: getOtherUsersRecipes,
  });
}
