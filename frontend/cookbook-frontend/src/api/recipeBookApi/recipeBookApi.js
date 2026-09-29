import { apiFetch } from "../apiFetch";

export async function getRecipeBook(id) {
  const response = await apiFetch(`/api/recipebooks/${id}`);

  if (!response.ok) {
    throw new Error("Unable to find recipe book");
  }

  return response.json();
}

export async function getAllRecipeBooks({page = 1,
                                          pageSize = 12,
                                          sort = "asc",
                                          tag = "",
                                        } = {}) {
  const params = new URLSearchParams({
    page: String(page),
    pageSize: String(pageSize),
    sort,
  });

  if (tag) {
    params.set("tag", tag);
  }

  const response = await apiFetch(`/api/recipebooks?${params}`);

  if (!response.ok) {
    throw new Error("Unable to fetch recipe books");
  }

  return response.json();
}

export async function createRecipeBook({
  recipeBookName,
  description,
  imageUrl,
    tag,
}) {
  const response = await apiFetch("/api/recipebooks", {
    method: "POST",
    body: JSON.stringify({ recipeBookName, description, imageUrl,tag }),
  });

  if (!response.ok) {
    throw new Error("Unable to create recipe book");
  }
  return response.json();
}

export async function updateRecipeBook(id, { name, description, imageUrl,tag }) {
  const response = await apiFetch(`/api/recipebooks/${id}`, {
    method: "PUT",
    body: JSON.stringify({ name, description, imageUrl,tag }),
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
