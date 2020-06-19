using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using ProductService.DataTransfer.Channel.Factories;
using ProductServices.Notifier.Data;
using ProductServices.Notifier.Hubs;
using ProductServices.Notifier.Interfaces;
using ProductServices.Notifier.System;

namespace ProductServices.Notifier
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSignalR(hubOptions =>
            {
                hubOptions.EnableDetailedErrors = true;
            }).AddJsonProtocol(options =>
            {
                options.PayloadSerializerOptions.PropertyNamingPolicy = null;
            });
            services.AddCors(options =>
            {
                options.AddPolicy( Constants.AllowAnyCrosPolicy, builder => builder
                    .AllowCredentials()
                    .AllowAnyHeader()
                    .AllowAnyMethod()
                    .SetIsOriginAllowed(origin => true));
            });
            services.AddTransient<IChannelFactory, ChannelFactory>();
            services.AddSingleton<IListenerManager, ListenerManager>();
            services.AddScoped<DataMapper>();
            services.AddScoped<INotificationHelper, NotificationHelper>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseCors(Constants.AllowAnyCrosPolicy);

            app.UseDefaultFiles();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapGet("/", async context =>
                {
                    await context.Response.WriteAsync("Product notification service running!");
                });

                endpoints.MapHub<ProductChangesHub>("/product_changes");
            });
        }
    }
}
