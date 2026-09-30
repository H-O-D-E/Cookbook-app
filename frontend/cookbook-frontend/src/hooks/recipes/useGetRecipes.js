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

export function useGetOtherUsersRecipes(options) {
  return useQuery({
    queryKey: ["recipes", "explore",options],
    queryFn: ()=>getOtherUsersRecipes(options),
  });
}
