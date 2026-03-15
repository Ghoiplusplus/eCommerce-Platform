using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace UserService.JwtConfig
{
    public static class JwtExtension
    {
        private static string _secret = Environment.GetEnvironmentVariable("SECRET");
        private static string _issuer = "userservice";
        public static void AddJwtAuthentication(this IServiceCollection services)
        {
            services
                .AddAuthentication()
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        // указывает, будет ли валидироваться издатель при валидации токена
                        ValidateIssuer = true,
                        // строка, представляющая издателя
                        ValidIssuer = _issuer,
                        // будет ли валидироваться потребитель токена
                        ValidateAudience = false,
                        // будет ли валидироваться время существования
                        ValidateLifetime = true,
                        // установка ключа безопасности
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_secret))
                    };
                });
        }

    }
}
