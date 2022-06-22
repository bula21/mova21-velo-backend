using System;
using System.Linq;
using System.Security.Claims;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.SpaServices.AngularCli;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Mova21AppBackend.Data.Interfaces;
using Mova21AppBackend.Data.Models;
using Mova21AppBackend.Data.Storage;

namespace Mova21AppBackend;

public class Startup
{
    private const string OpenIdConnectPolicyName = "OpenIDConnectPolicy";

    public Startup(IWebHostEnvironment env, IConfiguration configuration)
    {
        Environment = env;
        Configuration = configuration;
    }

    public IConfiguration Configuration { get; }
    public IWebHostEnvironment Environment { get; }

    // This method gets called by the runtime. Use this method to add services to the container.
    public void ConfigureServices(IServiceCollection services)
    {
        services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        }).AddJwtBearer(o =>
        {
            o.Authority = Configuration["Jwt:Authority"];
            o.TokenValidationParameters.ValidateAudience = false;
            o.MetadataAddress = Configuration["Jwt:MetadataAddress"];
            o.Events = new JwtBearerEvents
            {
                OnAuthenticationFailed = c =>
                {
                    c.NoResult();

                    c.Response.StatusCode = 500;
                    c.Response.ContentType = "text/plain";
                    if (Environment.IsDevelopment())
                    {
                        return c.Response.WriteAsync(c.Exception.ToString());
                    }
                    return c.Response.WriteAsync("An error occurred processing your authentication.");
                },
                OnTokenValidated = async tokenValidationContext =>
                {
                    if (tokenValidationContext.Principal is null)
                    {
                        return;
                    }

                    var resourceAccessJson = tokenValidationContext.Principal.Claims
                        .FirstOrDefault(c => c.Type == "resource_access")?.Value;
                    if (resourceAccessJson == null)
                    {
                        tokenValidationContext.Principal.AddIdentity(new ClaimsIdentity(new[]
                        {
                            new Claim(Claims.ActivityEdit, "false"),
                            new Claim(Claims.BikeEdit, "false"),
                            new Claim(Claims.WeatherEdit, "false")
                        }));
                    }
                    else
                    {
                        JsonObject obj = JsonNode.Parse(resourceAccessJson) as JsonObject;
                        
                        var roles = obj["velo-backend"]["roles"].AsArray().Select(x => x.AsValue().GetValue<string>());
                        tokenValidationContext.Principal.AddIdentity(new ClaimsIdentity(new[]
                        {
                            new Claim(Claims.ActivityEdit, roles.Contains("activity", StringComparer.InvariantCultureIgnoreCase) ? "true" : "false"),
                            new Claim(Claims.BikeEdit, roles.Contains("velo", StringComparer.InvariantCultureIgnoreCase) ? "true" : "false"),
                            new Claim(Claims.WeatherEdit, roles.Contains("wetter", StringComparer.InvariantCultureIgnoreCase) ? "true" : "false")
                        }));
                    }
                }
            };
        });

        services.AddAuthorization(options =>
        {
            options.AddPolicy(PolicyNames.Activity,policy => policy.RequireClaim(Claims.ActivityEdit, "true"));
            options.AddPolicy(PolicyNames.Bike,policy => policy.RequireClaim(Claims.BikeEdit, "true"));
            options.AddPolicy(PolicyNames.Weather,policy => policy.RequireClaim(Claims.WeatherEdit, "true"));
        });        

        services.AddScoped<IBikeRepository, DirectusBikeRepository>();
        services.AddScoped<IWeatherRepository, DirectusWeatherRepository>();
        services.AddScoped<IActivityRepository, DirectusActivityRepository>();

        services.AddControllersWithViews();
        // In production, the Angular files will be served from this directory
        services.AddSpaStaticFiles(configuration =>
        {
            configuration.RootPath = "ClientApp/dist";
        });
    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
        }
        else
        {
            app.UseExceptionHandler("/Error");
        }
        
        app.UseStaticFiles();
        if (!env.IsDevelopment())
        {
            app.UseSpaStaticFiles();
        }

        app.UseAuthentication();

        app.UseRouting();

        app.UseAuthorization();

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllerRoute(
                name: "default",
                pattern: "{controller}/{action=Index}/{id?}").RequireCors(OpenIdConnectPolicyName);
        });

        app.UseSpa(spa =>
        {
            spa.Options.SourcePath = "ClientApp";

            if (env.IsDevelopment())
            {
                spa.UseAngularCliServer(npmScript: "start");
            }
        });
    }
}