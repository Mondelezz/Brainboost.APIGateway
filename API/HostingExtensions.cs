using Microsoft.Extensions.Configuration;
using Microsoft.OpenApi.Models;
using Ocelot.DependencyInjection;
using Ocelot.Middleware;

namespace API
{
    internal static class HostingExtensions
    {
        private static string CONFIGURATION_FOLDER = "Configuration";
        public static WebApplicationBuilder ConfigureServices(this WebApplicationBuilder builder)
        {
            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(o =>
            #region SwaggerDoc
                o.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "Brainboost.APIGateway",
                    Version = "v1",
                    Description = "Microservice module API with Ocelot. This template based on .NET 8.0\n" +
                    @"
                            ░▒▓██████████►╬◄██████████▓▒░               
                            ░▒▓██►╔╦╦╦═╦╗╔═╦═╦══╦═╗◄██▓▒░
                            ░▒▓██►║║║║╩╣╚╣═╣║║║║║╩╣◄██▓▒░
                            ░▒▓██►╚══╩═╩═╩═╩═╩╩╩╩═╝◄██▓▒░
                            ░▒▓██████████►╬◄██████████▓▒░
                    " +
                    @" 
                        ───────────────██████████───────────
                        ──────────────████████████──────────
                        ──────────────██────────██──────────
                        ──────────────██▄▄▄▄▄▄▄▄▄█──────────
                        ──────────────██▀███─███▀█────────── 
                        █─────────────▀█────────█▀──────────
                        ██──────────────██──█─██────────────
                        ─█──────────────████████────────────
                        █▄────────────████─██──████─────────
                        ─▄███████████████──██──██████ ──────
                        ────█████████████──██──█████████────
                        ─────────────████──██─█████──███────
                        ──────────────███──██─█████──███────
                        ──────────────███─██───█████████────
                        ──────────────████───████████▀──────
                        ────────────────██████████──────────
                        ────────────────██████████──────────
                        ─────────────────██████████─────────
                        ──────────────────██████████▄▄──────
                        ────────────────────█████████▀──────
                        ───────────────────██████──█████────
                        ────────────────────▄████▄──█████▄──
                        ────────────────────██████───▀▀▀██──
                        ────────────────────▀▄▄▄▄▀────▀▄▄▄▀",
                    Contact = new OpenApiContact
                    {
                        Url = new Uri("https://github.com/Mondelezz"),
                        Email = @"pankov.egor26032005@yandex.ru",
                        Name = "Mondelezz"
                    },
                }));
            #endregion

            builder.Configuration
                .SetBasePath(AppContext.BaseDirectory)
                .AddJsonFile(Path.Combine(CONFIGURATION_FOLDER, "appsettings.json"), optional: false, reloadOnChange: true)
                .AddJsonFile(Path.Combine(CONFIGURATION_FOLDER, "ocelot.json"), optional: false, reloadOnChange: true);


            builder.Services.AddOcelot();

            return builder;
        }

        public static WebApplication ConfigurePipeline(this WebApplication app)
        {
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseRouting();

            app.UseAuthentication();

            app.UseAuthorization();

            app.MapControllers();

            app.UseOcelot();

            return app;
        }
    }
}
