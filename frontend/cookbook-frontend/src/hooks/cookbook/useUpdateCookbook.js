import { updateRecipeBook } from "@/api/recipeBookApi/recipeBookApi";
import { useMutation, useQueryClient } from "@tanstack/react-query";
import { toast } from "react-toastify";

export function useUpdateCookbook() {
  const queryClient = useQueryClient();

  return useMutation({
    mutationFn: ({ id, name, description, imageUrl, tag }) =>
      updateRecipeBook(id, { name, description, imageUrl,tag }),

    onSuccess: () => {
      queryClient.invalidateQueries({
        queryKey: ["recipebooks"],
      });
      toast.success("Updated recipebook");
    },
  });
}
