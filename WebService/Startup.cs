using System;
using System.IO;
using System.Reflection;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using WebService.Services;

namespace WebService
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
            services.AddDbContext<EsmeraldaContext>(
                options => options.AddMysqlDb(Configuration)
            );

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                    .AddJwtBearer(
                         options =>
                             options.TokenValidationParameters = new TokenValidationParameters
                             {
                                 ValidateIssuer = true,
                                 ValidateAudience = true,
                                 ValidateLifetime = true,
                                 ValidateIssuerSigningKey = true,
                                 ValidIssuer = "apolosalud.net",
                                 ValidAudience = "apolosalud.net",
                                 IssuerSigningKey = new SymmetricSecurityKey(
                                     Encoding.UTF8.GetBytes(Configuration["llavePrincipal"])
                                 ),
                                 ClockSkew = TimeSpan.Zero
                             }
                     );

            services.AddHealthChecks()
                    .AddCheck("self", () => HealthCheckResult.Healthy())
                    .AddDbContextCheck<EsmeraldaContext>(
                         tags: new[] {"service"},
                         customTestQuery: async (context, token) =>
                         {
                             var database = context.Database;
                             return await database.CanConnectAsync();
                         }
                     );

            services.AddControllers();

            services.AddRouting(o => o.LowercaseUrls = true);

            services.AddCors(
                options =>
                {
                    options.AddPolicy(
                        "CorsPolicy",
                        b => b
                            .AllowAnyHeader()
                            .AllowAnyMethod()
                            .AllowAnyOrigin()
                    );
                }
            );

            services.AddSwaggerGen(
                c =>
                {
                    c.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
                    {
                        Description = "Encabezado de autorización estándar usando el esquema bearer. Ejemplo: \"bearer {token}\"",
                        In = ParameterLocation.Header,
                        Name = "Authorization",
                        Type = SecuritySchemeType.ApiKey
                    });
                    c.SwaggerDoc("v1", new OpenApiInfo {Title = "Monitor Esmeralda Api", Version = "v1"});
                    var docXml = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                    var pathHelp = Path.Combine(AppContext.BaseDirectory, docXml);
                    c.IncludeXmlComments(pathHelp);
                }
            );
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment()) app.UseDeveloperExceptionPage();

            app.UseSwagger(c => c.RouteTemplate = "apolohra/apidocs/{documentname}/docs.json");
            app.UseSwaggerUI(c=>
                {
                    c.SwaggerEndpoint("/apolohra/apidocs/v1/docs.json", "Monitor Esmeralda Api");
                    c.RoutePrefix = "apolohra/apidocs";
                }
            );
            app.UseHttpsRedirection();

            app.UseAuthentication();
            app.UseRouting();
            app.UseCors("CorsPolicy");
            app.UseAuthorization();

            app.UseHealthChecks("/self", new HealthCheckOptions {Predicate = r => r.Name.Contains("self")});
            app.UseHealthChecks("/ready", new HealthCheckOptions {Predicate = r => r.Tags.Contains("service")});
            
            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }
    }
}
