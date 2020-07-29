using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.DependencyResolvers;
using Core.Extensions;
using Core.Utilities.IoC;
using Core.Utilities.Security.Encryption;
using Core.Utilities.Security.Jwt;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;

namespace WebAPI
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            // Farkl� clientlar �zerinden istek at�ld���nda g�venlik engelleri ile kar��la��l�rz, bunu engellemek i�in cors ayarlar� yap�l�r.
            services.AddCors(options =>
            {
                options.AddPolicy("AllowOrign", builder =>
                {
                    builder.WithOrigins("http://localhost:3000");
                });
            });

            var tokenOptions = Configuration.GetSection("TokenOptions").Get<TokenOptions>();

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = tokenOptions.Issuer,
                    ValidAudience = tokenOptions.Audience,
                    IssuerSigningKey = SecurityKeyHelper.CreateSecurityKey(tokenOptions.SecurityKey)
                };
            });

            // ServiceCollectionExtension'da yaz�ld�.
            services.AddDependenyResolvers(new ICoreModule[]{
                    new CoreModule()
                });

        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.ConfigureCustomExceptionMiddleware();

            // AllowAnyHeader ile gelen t�m isteklere (get, post, delete, put) izin ver diyoruz.
            app.UseCors(builder => builder.WithOrigins("http://localhost:3000").AllowAnyHeader());

            app.UseHttpsRedirection();

            app.UseRouting();

            // Sisteme giri� yapmak i�in gerekli kimlik kontrol�n�n yap�lmas�n� sa�lar. Kullan�c� Login i�lemi ba�ar�l�ysa Authenticate olmu� olur.
            app.UseAuthentication();

            // Sisteme giri� yapt�ktan sonra sayfalarda gezinmek i�in gerekli yetkilendirmeleri kontrol eder. Kullan�c� Authenticate olduktan sonra Jwt'deki rollerine g�re authorize i�lemini ger�ekle�tirir.
            app.UseAuthorization();



            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
