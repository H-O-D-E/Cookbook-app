import {
  updateRecipe,
  updateRecipeVisibility,
} from "@/api/recipeApi/recipeApi";
import { useMutation, useQueryClient } from "@tanstack/react-query";
import { toast } from "react-toastify";

export function useUpdateRecipe(recipeBookId) {
  const queryClient = useQueryClient();

  return useMutation({
    mutationFn: updateRecipe,

    onSuccess: () => {
      queryClient.invalidateQueries({
        queryKey: ["recipebooks", recipeBookId, "recipes"],
      });

      toast.success("Updated recipe!");
    },
  });
}

export function useUpdateRecipeVisibility(recipeBookId) {
  const queryClient = useQueryClient();

  return useMutation({
    mutationFn: ({ recipeId, isPublic }) =>
      updateRecipeVisibility(recipeId, isPublic),

    onSuccess: () => {
      queryClient.invalidateQueries({
        queryKey: ["recipebooks", recipeBookId, "recipes"],
      });

      toast.success("Successfully changed this recipe's visibility");
    },

    onError: () => {
      toast.error("Failed to update recipe visibility");
    },
  });
}
