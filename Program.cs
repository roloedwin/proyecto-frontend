using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);
var jwtKey = builder.Configuration["JWT_KEY"] ?? Environment.GetEnvironmentVariable("JWT_KEY") ?? "taller_mecanico_jwt_key_2026_segura";
var jwtIssuer = builder.Configuration["JWT_ISSUER"] ?? Environment.GetEnvironmentVariable("JWT_ISSUER") ?? "TallerMecanico";
var jwtAudience = builder.Configuration["JWT_AUDIENCE"] ?? Environment.GetEnvironmentVariable("JWT_AUDIENCE") ?? "TallerMecanicoUsuarios";

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = jwtIssuer,
            ValidAudience = jwtAudience,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey))
        };

        options.Events = new JwtBearerEvents
        {
            OnMessageReceived = context =>
            {
                var token = context.Request.Cookies["auth_token"];
                if (!string.IsNullOrWhiteSpace(token))
                {
                    context.Token = token;
                }

                return Task.CompletedTask;
            },
            OnChallenge = context =>
            {
                if (!context.Request.Path.StartsWithSegments("/api"))
                {
                    context.HandleResponse();
                    context.Response.Redirect("/Auth/Login");
                }

                return Task.CompletedTask;
            }
        };
    });

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

var app = builder.Build();

// Pipeline
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
