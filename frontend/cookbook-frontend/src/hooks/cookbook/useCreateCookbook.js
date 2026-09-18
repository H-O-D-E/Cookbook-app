import { createRecipeBook } from "@/api/recipeBookApi/recipeBookApi";
import { useMutation, useQueryClient } from "@tanstack/react-query";
import { toast } from "react-toastify";

export function useCreateCookbook() {
  const queryClient = useQueryClient();

  return useMutation({
    mutationFn: createRecipeBook,

    onSuccess: () => {
      queryClient.invalidateQueries({
        queryKey: ["recipebooks"],
      });
      toast.success("Created new recipebook");
    },
  });
}
