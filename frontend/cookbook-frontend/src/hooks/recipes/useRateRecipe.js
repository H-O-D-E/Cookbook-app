import { rateRecipe } from "@/api/recipeApi/recipeApi";
import { useMutation, useQueryClient } from "@tanstack/react-query";
import { toast } from "react-toastify";

export function useRateRecipe(recipeId) {
  const queryClient = useQueryClient();

  return useMutation({
    mutationFn: (score) => rateRecipe(recipeId, score),

    onSuccess: () => {
      queryClient.invalidateQueries({
        queryKey: ["recipes", "explore"],
      });
      toast.success("Thanks for your rating!");
    },
  });
}
