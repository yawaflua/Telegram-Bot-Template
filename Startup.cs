using Microsoft.AspNetCore.Hosting.Server;
using System;

namespace $safeprojectname$
{
    /// <summary>
    /// Startup class for ASP.NET with setting and configuration
    /// </summary>
    public class Startup
    {
       
        private readonly IConfiguration configuration;
            
        public Startup()
        {
            configuration = new ConfigurationBuilder()
                .AddEnvironmentVariables()
                .AddJsonFile("appsettings.json", optional: true)
                .Build();
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services
                    .AddSwaggerGen();

            services
                .AddSingleton(configuration)
                .AddSingleton(x => x.CreateScope())
                .AddHostedService<TelegramBotService>();

        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwagger(c =>
                {
                    c.RouteTemplate = "/swagger/v1/swagger.json";
                });
            }
            app.UseStaticFiles();
            app.UseRouting();
            app.UseCors(k => { k.AllowAnyHeader(); k.AllowAnyMethod(); k.AllowAnyOrigin(); k.WithMethods("POST", "GET"); });
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
        
    }
}
