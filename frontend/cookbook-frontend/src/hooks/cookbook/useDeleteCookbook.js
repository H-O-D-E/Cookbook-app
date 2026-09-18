import { deleteRecipeBook } from "@/api/recipeBookApi/recipeBookApi";
import { useMutation, useQueryClient } from "@tanstack/react-query";
import { toast } from "react-toastify";

export function useDeleteCookbook() {
  const queryClient = useQueryClient();

  return useMutation({
    mutationFn: deleteRecipeBook,

    onSuccess: () => {
      queryClient.invalidateQueries({
        queryKey: ["recipebooks"],
      });
      toast.success("Successfully deleted recipebook.");
    },
  });
}
