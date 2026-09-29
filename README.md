# Cookbooklet
## Are you cooked?

Cookbook app is a web app where you can create, browse and organise all of your favorite recipes in one place. You can group them into cookbooks such as "Greatest Mexican Food" and "Grandma's secrets", and share them with friends, family and the rest of the world. 



## Live demo:
[Cookbooklet](https://wonderful-bush-014271b03.2.azurestaticapps.net/)

## Team:
Oliver Kristiansen | [okristiansen](**https://github.com/okristiansen**)
Håkon Lervåg | [haaler](**https://github.com/haaler**)
David Hoang Nguyen | [DAVNGU99](**https://github.com/DAVNGU99**)
Evan Hanif Belal | [visord3](**https://github.com/visord3**)

## Domain mapping
The template's generic placeholders map onto Cookbook's domain like this:

| Placeholder     | Our domain     | Description                                             |
|-----------------|----------------|---------------------------------------------------------|
| `[USER]`        | **User**       | A person who signs up and creates recipes and cookbooks |
| `[PRIMARY]`     | **Cookbook**   | A collection of recipes                                 |
| `[CHILD]`       | **Recipe**     | A dish, belonging to a cookbook                         |
| `[TAG]`         | **Ingredient** | An item in a recipe, such as "Chicken" or "Paprika"     |
| `[INTERACTION]` | **Thumbs up (Like)**     | A thumbs up (Like) on a recipe                                 |

**Relationships:** A User has many Cookbooks. A Cookbook has many Recipes, which again have many Ingredients. Users can leave a like hos other users recipes.
- In the code, a cookbook is called `recipebook`

## Architecture

- **Frontend:** React + Vite (`frontend/cookbook-frontend`)
- **Backend:** ASP.NET Core 8 Web API with JWT authentication (`backend/Cookbook-app`)
- **Database:** PostgreSQL on Azure, managed with Entity Framework Core migrations
- **Deployment:** Frontend and backend run on Azure. GitHub Actions runs the tests on every push and pull request to `main`.

## Running locally

Requires Docker. Create a `.env` file in the repo root (see below), then run:

```bash
docker compose up --build
```

- Frontend: http://localhost:5173
- Backend: http://localhost:7046

## Running tests

Requires the .NET 8 SDK, and Docker must be running for the integration tests.

```bash
cd backend/Cookbook-app
dotnet test
```

## Environment variables

| Variable | Description |
|----------|-------------|
| `ConnectionStrings__DefaultConnection` | PostgreSQL connection string |
| `JWT_SIGNING_KEY` | Secret for signing JWTs (at least 32 characters) |