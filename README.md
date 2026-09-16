
Cookbook for developers

<!-- ============================================================
REVIEW: repo-level notes (instructor review pass, 2026-09-16)

Inline comments are marked REVIEW(azure) / REVIEW(sec) / REVIEW(api) /
REVIEW(bug) / REVIEW(noob) / REVIEW(config) / REVIEW(good) in the source.
This branch exists to be read as a diff. Do not merge it: read it, act on
what you agree with, then delete it.

The findings below have no single line to attach to.

1. RecipeController HAS NO [Authorize] AND NO OWNERSHIP CHECKS. Anyone, without
   logging in, can create a recipe in any user's cookbook (the client picks
   RecipebookId), edit any recipe, or delete any recipe by id. RecipeBookController
   right next to it does all of this correctly: [Authorize], the user id taken
   from the token claims, and every repository query filtered by owner. Copy that
   pattern across. This is the single most important change in the repo.

2. THE DEPLOYED APP CANNOT START. The connection string exists only in
   appsettings.Development.json, so outside Development the app gets null and
   dies on the first request. Nothing applies EF migrations either, so even with
   a connection string the database would be empty.

3. NO SQL INJECTION RISK. Everything goes through EF Core LINQ, there is no
   string-built SQL anywhere. Worth knowing why you are safe: it is the
   parameterization EF does for you, not anything you did deliberately. If you
   ever reach for FromSqlRaw, switch to FromSqlInterpolated.

4. THE FRONTEND MAKES NO API CALLS YET. Before it does, read the note in
   vite.config.js: use import.meta.env.VITE_API_BASE_URL and a dev-server proxy,
   not a hardcoded localhost URL and not a third-party dotenv package.

5. ONE TEST FILE (AuthControllerTest) and none for the recipe or recipe-book
   logic, which is where the actual rules live.

6. THE COMPILER ALREADY KNOWS ABOUT TWO OF THE BUGS HERE. The `if (false)` dead
   branch in RecipeController.DeleteRecipe and RecipeBookService returning null
   from a non-nullable Task<RecipeBook> are both warnings nobody is reading. Turn
   on TreatWarningsAsErrors and this class of bug stops reaching review.
============================================================ -->
