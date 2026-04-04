using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using TallerMecanico.Models;
using Microsoft.AspNetCore.Authorization;

public class AuthController : Controller
{
    private readonly AppDbContext _context;
    private readonly IConfiguration _configuration;

    public AuthController(AppDbContext context, IConfiguration configuration)
    {
        _context = context;
        _configuration = configuration;
    }

    [HttpGet]
    [AllowAnonymous]
    public IActionResult Login()
    {
        if (User.Identity?.IsAuthenticated == true)
        {
            return RedirectToAction("Index", "Home");
        }

        return View();
    }

    [HttpPost("/api/auth/login")]
    [AllowAnonymous]
    public async Task<IActionResult> ApiLogin([FromBody] LoginRequest request)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(new { message = "Debe completar usuario y contraseña." });
        }

        var usuario = await _context.Usuarios
            .FirstOrDefaultAsync(u => u.Username == request.Username);

        if (usuario == null || !BCrypt.Net.BCrypt.Verify(request.Password, usuario.Password))
        {
            return Unauthorized(new { message = "Credenciales inválidas." });
        }

        var token = GenerarToken(usuario);
        var expiration = DateTimeOffset.UtcNow.AddHours(8);

        Response.Cookies.Append("auth_token", token, new CookieOptions
        {
            HttpOnly = true,
            Secure = Request.IsHttps,
            SameSite = SameSiteMode.Strict,
            Expires = expiration
        });

        return Ok(new LoginResponse
        {
            Token = token,
            Username = usuario.Username,
            Rol = usuario.Rol,
            RedirectUrl = Url.Action("Index", "Home") ?? "/"
        });
    }

    [HttpPost("/api/auth/logout")]
    [Authorize]
    public IActionResult Logout()
    {
        Response.Cookies.Delete("auth_token");
        return Ok(new { message = "Sesión cerrada." });
    }

    private string GenerarToken(Usuario usuario)
    {
        var jwtKey = GetJwtSetting("JWT_KEY", "taller_mecanico_jwt_key_2026_segura");
        var jwtIssuer = GetJwtSetting("JWT_ISSUER", "TallerMecanico");
        var jwtAudience = GetJwtSetting("JWT_AUDIENCE", "TallerMecanicoUsuarios");

        var claims = new List<Claim>
        {
            new(JwtRegisteredClaimNames.Sub, usuario.Id.ToString()),
            new(ClaimTypes.NameIdentifier, usuario.Id.ToString()),
            new(ClaimTypes.Name, usuario.Username),
            new(ClaimTypes.Role, usuario.Rol),
            new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
        };

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey));
        var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            issuer: jwtIssuer,
            audience: jwtAudience,
            claims: claims,
            expires: DateTime.UtcNow.AddHours(8),
            signingCredentials: credentials);

        return new JwtSecurityTokenHandler().WriteToken(token);
    }

    private string GetJwtSetting(string key, string fallback)
    {
        return _configuration[key] ?? Environment.GetEnvironmentVariable(key) ?? fallback;
    }
}
