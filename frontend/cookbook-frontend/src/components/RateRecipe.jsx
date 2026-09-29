import { useState } from "react";
import { useRateRecipe } from "@/hooks/recipes/useRateRecipe";

function RateRecipe({ recipe }) {
  const [selectedScore, setSelectedScore] = useState(0);
  const rateMutation = useRateRecipe(recipe.recipeId);

  function handleRating(score) {
    const previousScore = selectedScore;
    setSelectedScore(score);

    rateMutation.mutate(score, {
      onError: () => setSelectedScore(previousScore),
    });
  }

  const name = `rating-${recipe.recipeId}`;

  return (
    <div className="flex items-center justify-center p-6 scale-120">
      <div className="rating rating-xl">
        <input
          type="radio"
          name={name}
          className="mask mask-star-2 bg-orange-400"
          aria-label="1 star"
          checked={selectedScore === 1}
          disabled={rateMutation.isPending}
          onChange={() => handleRating(1)}
        />
        <input
          type="radio"
          name={name}
          className="mask mask-star-2 bg-orange-400"
          aria-label="2 stars"
          checked={selectedScore === 2}
          disabled={rateMutation.isPending}
          onChange={() => handleRating(2)}
        />
        <input
          type="radio"
          name={name}
          className="mask mask-star-2 bg-orange-400"
          aria-label="3 stars"
          checked={selectedScore === 3}
          disabled={rateMutation.isPending}
          onChange={() => handleRating(3)}
        />
        <input
          type="radio"
          name={name}
          className="mask mask-star-2 bg-orange-400"
          aria-label="4 stars"
          checked={selectedScore === 4}
          disabled={rateMutation.isPending}
          onChange={() => handleRating(4)}
        />
        <input
          type="radio"
          name={name}
          className="mask mask-star-2 bg-orange-400"
          aria-label="5 stars"
          checked={selectedScore === 5}
          disabled={rateMutation.isPending}
          onChange={() => handleRating(5)}
        />
      </div>

      {rateMutation.isError && <p>Could not save rating.</p>}
    </div>
  );
}

export default RateRecipe;
