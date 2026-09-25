import { getRecipeBook } from "@/api/recipeBookApi/recipeBookApi";
import { useQuery } from "@tanstack/react-query";

export function useGetCookbook(id) {
    return useQuery({
        queryKey: ["recipebooks", id],
        queryFn: () => getRecipeBook(id),
        enabled: !!id,
    })
}