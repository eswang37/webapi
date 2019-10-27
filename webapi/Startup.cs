using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using webapi.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using webapi.Middlewares;
using System.Text;
using Swashbuckle.AspNetCore.Swagger;

namespace webapi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }
        public static string connString = string.Empty;
        public static SymmetricSecurityKey signingKey;
        public static string emailUserName = string.Empty;
        public static string emailPassword = string.Empty;
        public static string apiURL = string.Empty;
        public static TokenProviderOptions userTokenOptions;
        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors();
            connString = Configuration.GetConnectionString("SQLSERVERDB");
            signingKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(Configuration["Keys:UserAuthSecretKey"]));
            apiURL = Configuration["Keys:APIURL"];
            emailUserName = Configuration["EmailSettings:UserName"];
            emailPassword = Configuration["EmailSettings:Passwrod"];
            userTokenOptions = new TokenProviderOptions
            {
                Audience = "ConsumerUser",
                Issuer = "Backend",
                SigningCredentials = new SigningCredentials(signingKey, SecurityAlgorithms.HmacSha256),
            };

            services.AddDbContext<ContactContext>(options =>
                    options.UseSqlServer(connString));
            services.AddDbContext<PostContext>(options =>
                    options.UseSqlServer(connString));

            services.Configure<EmailSettings>(Configuration.GetSection("EmailSettings"));
            services.AddMvc();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info { Title = "Andmap WebAPI", Version = "v1" });
            });
            services.AddAntiforgery(options => options.HeaderName = "X-XSRF-TOKEN");
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseStaticFiles();
            app.UseDefaultFiles();
            app.UseCors(builder => builder
                .AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader()
                .AllowCredentials());
            app.UseMvc();
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Andmap WebAPI");
            });
        }
    }
}
