import { createRecipe } from "@/api/recipeApi/recipeApi";
import { useMutation, useQueryClient } from "@tanstack/react-query";
import { toast } from "react-toastify";

export function useCreateRecipe(recipeBookId) {
    const queryClient = useQueryClient();

    return useMutation({
        mutationFn: createRecipe,
        onSuccess: () => {
            queryClient.invalidateQueries({
                queryKey: ["recipebooks", recipeBookId, "recipes"],
            });
            toast.success("Created new recipe");
        },
    });
}