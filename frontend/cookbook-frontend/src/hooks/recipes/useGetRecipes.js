import { useQuery } from "@tanstack/react-query";
import { getRecipesByRecipeBookId } from "@/api/recipeApi/recipeApi";

export function useGetRecipes(recipeBookId) {
    return useQuery({
        queryKey: ["recipebooks", recipeBookId, "recipes"],
        queryFn: () => getRecipesByRecipeBookId(recipeBookId),
        enabled: !!recipeBookId,
    });
}