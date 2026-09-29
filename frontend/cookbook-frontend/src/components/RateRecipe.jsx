function RateRecipe() {
  return (
    <div className="flex items-center justify-center p-4">
      <div className="rating rating-xl scale-110">
        <input
          type="radio"
          name="rating-9"
          className="mask mask-star-2 bg-orange-400"
          aria-label="1 star"
        />
        <input
          type="radio"
          name="rating-9"
          className="mask mask-star-2 bg-orange-400"
          aria-label="2 star"
          defaultChecked
        />
        <input
          type="radio"
          name="rating-9"
          className="mask mask-star-2 bg-orange-400"
          aria-label="3 star"
        />
        <input
          type="radio"
          name="rating-9"
          className="mask mask-star-2 bg-orange-400"
          aria-label="4 star"
        />
        <input
          type="radio"
          name="rating-9"
          className="mask mask-star-2 bg-orange-400"
          aria-label="5 star"
        />
      </div>
    </div>
  );
}

export default RateRecipe;
