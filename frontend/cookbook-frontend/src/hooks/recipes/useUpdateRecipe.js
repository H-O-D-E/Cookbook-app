import { updateRecipe } from "@/api/recipeApi/recipeApi";
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
