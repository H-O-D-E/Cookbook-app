using System.Text;
using Cookbook_app.Data;
using Cookbook_app.Repositories;
using Cookbook_app.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

// REVIEW(noob): AddControllers() without AddProblemDetails(). You are hand-building ProblemDetails objects in two controllers already, so let the framework produce them consistently for unhandled exceptions too.
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<IJwtService, JwtService>();


// Add DI Scopes here
builder.Services.AddScoped<IRecipeBookRepository, RecipeBookRepository>();
builder.Services.AddScoped<IRecipeBookService, RecipeBookService>();

builder.Services.AddScoped<IRecipeRepository, RecipeRepository>();
builder.Services.AddScoped<IRecipeService, RecipeService>();

//For passord hashing




//DB connection

builder.Services.AddDbContext<CookbookDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));


// connections to the frontend

// REVIEW(config): the frontend origin is hardcoded, and the comment next to it admits it. This is the single most common reason a working app breaks the moment it is deployed: in Azure the frontend is on a completely different hostname. Read it from configuration: builder.Configuration.GetSection("Cors:AllowedOrigins").Get<string[]>(), with the value supplied per environment as Cors__AllowedOrigins__0. Same principle as the connection string, and IConfiguration is already injected everywhere you need it.
builder.Services.AddCors(options =>
{
    options.AddPolicy("ReactFrontend", policy =>
    {
        policy.WithOrigins("http://localhost:5173") // change this to whatever ur running ur frontend with
            .AllowAnyHeader()
            .AllowAnyMethod()
            // REVIEW(sec): AllowCredentials is only needed when the browser sends cookies. You authenticate with a bearer token in a header, so this can go. It also means that if anyone ever relaxes WithOrigins to AllowAnyOrigin, the combination is rejected by the CORS spec and you will lose an afternoon to it.
            .AllowCredentials(); 
    });
});


//Authentication with JWT signing key. claim
builder.Services
    // REVIEW(noob): AddRoles<IdentityRole> is registered but no role is ever created, assigned, or checked. Either use it or remove it, so the next reader does not assume there is a role model.
    .AddIdentityCore<IdentityUser>()
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<CookbookDbContext>();

var issuer = builder.Configuration["Jwt:ValidIssuer"];
var audience = builder.Configuration["Jwt:ValidAudience"];
var signingKey = builder.Configuration["Jwt:IssuerSigningKey"];

// REVIEW(good): failing fast at startup when the signing key is missing, rather than discovering it on the first login. This is exactly the right shape for required configuration.
if (string.IsNullOrWhiteSpace(signingKey))
{
    throw new InvalidOperationException(
        "JWT signing key is not configured.");
}

builder.Services
    .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,

            ValidIssuer = issuer,
            ValidAudience = audience,

            IssuerSigningKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(signingKey)),

            ClockSkew = TimeSpan.Zero
        };
    });

// REVIEW(azure): no health endpoint. builder.Services.AddHealthChecks() and app.MapHealthChecks("/health") is two lines and it is what the Azure probe needs.
builder.Services.AddAuthorization();

var app = builder.Build();



// Configure the HTTP request pipeline.
// REVIEW(azure): Swagger is dev-only. That is a defensible default, but it also means the deployed API has no discoverable docs at all, which makes the demo harder. Consider exposing it behind an environment flag rather than IsDevelopment.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// REVIEW(azure): UseHttpsRedirection inside a container is a trap. Azure Container Apps and App Service terminate TLS at the ingress and forward plain HTTP, so this either sends the client a redirect to a port nothing is listening on, or loops. Either drop it when running in a container, or add UseForwardedHeaders so the app can see the original scheme from X-Forwarded-Proto.
app.UseHttpsRedirection();
app.UseCors("ReactFrontend");


app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
