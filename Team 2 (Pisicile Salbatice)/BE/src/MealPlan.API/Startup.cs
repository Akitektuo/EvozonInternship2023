using System.IO;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Rewrite;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MealPlan.API.DependencyRegistration;
using MealPlan.Data;
using MealPlan.Business.Versions.Handlers;
using FluentValidation;
using MealPlan.API.Requests.Users;
using FluentValidation.AspNetCore;
using MealPlan.API.Middlewares.ExceptionHandler;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.AspNetCore.Authorization;
using OpenAI.Extensions;
using OpenAI.ObjectModels;

namespace MealPlan.API
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
            services.AddCors(options => options.AddDefaultPolicy(
                builder => builder.AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader()
            ));

            services.AddSwaggerGen();

            services.AddDbContext<MealPlanContext>(options =>
            {
                options.UseSqlServer(Configuration.GetConnectionString("SqlConnection"));
            });

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.SaveToken = true;
                options.RequireHttpsMetadata = false;
                options.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidAudience = Configuration["JWT:ValidAudience"],
                    ValidIssuer = Configuration["JWT:ValidIssuer"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["JWT:Secret"]))
                };
            });

            services.AddControllers();
            services.AddMediatR(typeof(GetVersionQueryHandler));

            services.AddOpenAIService(settings => 
            { 
                settings.ApiKey = Configuration["OPEN_AI_API_KEY"];
                settings.DefaultModelId = Models.Davinci;
            });
            
            services.AddValidatorsFromAssemblyContaining<LoginRequestValidator>();
            services.AddFluentValidationAutoValidation();

            services.AddAuthorization(option => 
                new AuthorizationPolicyBuilder().RequireAuthenticatedUser().Build()
            );

            services.RegisterServices();
            services.AddRouting(options => options.LowercaseUrls = true);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseMiddleware<CustomExceptionHandlerMiddleware>();

            app.UseSwagger();
            app.UseSwaggerUI();

            using (var iisUrlRewriteStreamReader = File.OpenText("IISUrlRewrite.xml"))
            {
                var options = new RewriteOptions().AddIISUrlRewrite(iisUrlRewriteStreamReader);

                app.UseRewriter(options);
            }

            app.UseDefaultFiles();
            app.UseStaticFiles();
            app.UseHttpsRedirection();

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
