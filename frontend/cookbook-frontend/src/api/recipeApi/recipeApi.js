import {apiFetch} from '../apiFetch';

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


export async function createRecipe({ name, description, imageUrl, ingredients, instructions, recipeBookId}) {
    const response = await apiFetch("/api/recipes", {
        method: "POST",
        body: JSON.stringify({ recipeName: name, description, imageUrl, ingredients, instructions, recipebookId: recipeBookId }),
    });

    if (!response.ok) {
        throw new Error("Error when creating recipe");
    }

    return response.json();
}

export async function updateRecipe(recipeId, {name, description, ingredients, instructions}) {
    const response = await apiFetch(`/api/recipes/${recipeId}`, {
        method: "PUT",
        body: JSON.stringify({ name, description, ingredients, instructions }),
    });

    if (!response.ok) {
        throw new Error("Failed to update recipe");
    }
    return response.json();
}

export async function deleteRecipe(recipeId) {
    const response = await apiFetch(`/api/recipes/${recipeId}`, {
        method: "DELETE"
    });

    if (!response.ok) {
        throw new Error("Failed to delete recipe");
    }
}

