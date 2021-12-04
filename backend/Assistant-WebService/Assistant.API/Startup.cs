using Assistant.Application.Interfaces;
using Assistant.Application.Interfaces.Authentication;
using Assistant.Application.Services;
using Assistant.Application.Services.Authentication;
using Assistant.Domain.Configuration;
using Assistant.Domain.DatabaseModel;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using NSwag;
using NSwag.Generation.Processors.Security;

namespace Assistant.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            services.AddControllers();

            services.AddOpenApiDocument(c =>
            {
                c.DocumentName = "v1";
                c.Title = "Context Aware Personal Assistant BackEnd";
                c.Version = "0.1";
                c.AddSecurity("Bearer token", 
                    new OpenApiSecurityScheme
                    {
                        Type = OpenApiSecuritySchemeType.Http,
                        Scheme = "Bearer",
                        In = OpenApiSecurityApiKeyLocation.Header,
                        Name = "Authorization",
                        Description = "Add your token here"
                    });

                c.OperationProcessors.Add(new AspNetCoreOperationSecurityScopeProcessor("Bearer token"));
            });

            // OLD ways
            // services.AddOpenApiDocument(c =>
            // {
            //     c.SwaggerDoc("v1", new OpenApiInfo { Title = "Assistant.API", Version = "v1" });
            // });

            services.AddSingleton<FirestoreDbAccessor>();
            services.AddScoped(typeof(IDatabaseService<User>), typeof(DatabaseService<User>));
            services.AddScoped(typeof(IDatabaseService<UserSlot>), typeof(DatabaseService<UserSlot>));

            services.AddOptions();
            services.Configure<ApplicationConfiguration>(Configuration.GetSection(nameof(ApplicationConfiguration)));
            services.Configure<AuthenticationConfiguration>(Configuration.GetSection(nameof(AuthenticationConfiguration)));

            services.AddTransient<IUserService, UserService>();
            services.AddTransient<ITokenBuilder, TokenBuilder>();
            services.AddSingleton<IEncryption, AesEncryption>();
            services.AddSingleton<Sha512Helper>();

            services.AddHttpClient<IRasaHttpService, RasaHttpService>(); // Adds a DI registration where a HttpClient is injected

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddScoped<ICurrentUserAccessor, CurrentUserAccessor>();

            services.AddSingleton<ISlotDifferenceCalculator, SlotDifferenceCalculator>();

            services.AddAuthentication(o =>
                {
                    o.DefaultScheme = nameof(CustomAuthenticationHandler);
                    o.DefaultChallengeScheme = nameof(CustomAuthenticationHandler);
                })
                .AddScheme<AuthenticationSchemeOptions, CustomAuthenticationHandler>(nameof(CustomAuthenticationHandler), o => { });

            services.AddAuthorization();

            services.AddCors(options =>
            {
                options.AddDefaultPolicy(
                    builder =>
                    {
                        builder.SetIsOriginAllowed(o => true)
                            .AllowAnyMethod()
                            .AllowAnyHeader()
                            .AllowCredentials();
                    });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                // app.UseSwagger();
                // app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Assistant.API v1"));

                app.UseOpenApi(); // serve OpenAPI/Swagger documents
                app.UseSwaggerUi3(); // serve Swagger UI
                app.UseReDoc(c => c.Path = "/swaggerredoc"); // serve ReDoc UI (Looks fancy :) )
            }

            // app.UseHttpsRedirection();

            app.UseRouting();

            app.UseCors();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
