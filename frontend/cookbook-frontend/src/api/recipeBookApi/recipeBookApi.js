import { apiFetch } from "../apiFetch";

export async function getRecipeBook(id) {
  const response = await apiFetch(`/api/recipebooks/${id}`);

  if (!response.ok) {
    throw new Error("Unable to find recipe book");
  }

  return response.json();
}

export async function getAllRecipeBooks() {
  const response = await apiFetch("/api/recipebooks");

  if (!response.ok) {
    throw new Error("Unable to fetch recipe books");
  }

  return response.json();
}

export async function createRecipeBook({
  recipeBookName,
  description,
  imageUrl,
}) {
  const response = await apiFetch("/api/recipebooks", {
    method: "POST",
    body: JSON.stringify({ recipeBookName, description, imageUrl }),
  });

  if (!response.ok) {
    throw new Error("Unable to create recipe book");
  }
  return response.json();
}

export async function updateRecipeBook(id, name) {
  const response = await apiFetch(`/api/recipebooks/${id}`, {
    method: "PUT",
    body: JSON.stringify({ name }),
  });

  if (!response.ok) {
    throw new Error("Unable to update recipe book");
  }
}

export async function deleteRecipeBook(id) {
  const response = await apiFetch(`/api/recipebooks/${id}`, {
    method: "DELETE",
  });

  if (!response.ok) {
    throw new Error("Unable to delete recipe book");
  }
}
