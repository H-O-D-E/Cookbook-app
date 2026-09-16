import { Link } from "react-router"

function Homepage() {
  return (
    <div className="card bg-base-100 w-96 shadow-sm">
      <Link to="/recipes/id">
        <figure>
          <img 
            src="https://www.jonathan-petitcolas.com/img/posts/ascii-art-converter/homer.png" 
            alt=""
          />
        </figure>
        <div className="card-body">
          <h2 className="card-title">Homer Recipe</h2>
          <p>The Homer Simpson Special</p>
        </div>
      </Link>
    </div>
  );
}

export default Homepage;
