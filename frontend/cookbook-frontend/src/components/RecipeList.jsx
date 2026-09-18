

function RecipeList({ recipes }) {
    
    if (!recipes?.length) {
        return <p>No recipes in this cookbook yet :( Make one! </p>;
    }


    return (
    <ul className="list bg-base-100 rounded-box shadow-md">
  
    <li className="p-4 pb-2 text-xs opacity-60 tracking-wide">Recipes</li>

    {recipes.map((recipe, index) => (
        <li key={recipe.recipeId} className="list-row">
        <div className ="text-4xl font-thin opacity-30 tabular-nums">
            {String(index+1).padStart(2, '0')}
        </div>
        <div><img className="size-10 rounded-box" alt={recipe.name} src={recipe.imageUrl}/></div>
        <div className="list-col-grow">
            <div>{recipe.name}</div>
            <div className="text-xs uppercase font-semibold opacity-60">{recipe.description}</div>
        </div>
        </li>

    ))}
    </ul>
    );
}

export default RecipeList;