import { apiFetch } from "../apiFetch";

export async function getRecipe(recipeId) {
  const response = await apiFetch(`/api/recipes/${recipeId}`);

  if (!response.ok) {
    throw new Error("Unable to find recipe :(");
  }

  return response.json();
}

export async function getRecipesByRecipeBookId(recipeBookId) {
  const response = await apiFetch(`/api/recipebooks/${recipeBookId}/recipes`);

  if (!response.ok) {
    throw new Error("Unable to find recipes for this recipebook ");
  }

  return response.json();
}

export async function getOtherUsersRecipes({page = 1 ,
                                           pageSize=12,
                                           sort = "asc",
                                           tag ="",
                                           }={}) {
  
  const params= new URLSearchParams({
    page : String(page),
    pageSize:String(pageSize),
    sort,
  })

  
  if (tag){
    params.set("tag",tag);
  }
  const response = await apiFetch(`/api/recipes/explore?${params}`);
  

  if (!response.ok) {
    throw new Error("Unable to load others recipes");
  }

  return response.json();
}

export async function createRecipe({
  name,
  description,
  imageUrl,
  ingredients,
  instructions,
  recipeBookId,
    tag,
}) {
  const response = await apiFetch(`/api/recipebooks/${recipeBookId}/recipes`, {
    method: "POST",
    body: JSON.stringify({
      recipeName: name,
      description,
      imageUrl,
      ingredients,
      instructions,
      tag,
    }),
  });

  if (!response.ok) {
    throw new Error("Error when creating recipe");
  }

  return response.json();
}

export async function updateRecipe({
  recipeId,
  name,
  description,
  imageUrl,
  ingredients,
  instructions,
    tag,
}) {
  const response = await apiFetch(`/api/recipes/${recipeId}`, {
    method: "PUT",
    body: JSON.stringify({
      name,
      description,
      imageUrl,
      ingredients,
      instructions,
      tag,
    }),
  });

  if (!response.ok) {
    throw new Error("Failed to update recipe");
  }

  return response.json();
}

export async function deleteRecipe(recipeId) {
  const response = await apiFetch(`/api/recipes/${recipeId}`, {
    method: "DELETE",
  });

  if (!response.ok) {
    throw new Error("Failed to delete recipe");
  }
}

export async function rateRecipe(recipeId, score) {
  const response = await apiFetch(`/api/recipes/${recipeId}/rating`, {
    method: "PUT",
    body: JSON.stringify({ score }),
  });

  if (!response.ok) {
    throw new Error("Could not rate this recipe");
  }

  return response.json();
}
