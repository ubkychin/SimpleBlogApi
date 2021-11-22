using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using SimpleBlogApi.Services;

namespace SimpleBlogApi {
    public class Startup {
        public Startup (IConfiguration configuration) {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices (IServiceCollection services) {

            services.AddSingleton<ICosmosDbService> (InitializeCosmosClientInstanceAsync (Configuration.GetSection ("CosmosDb")).GetAwaiter ().GetResult ());
            services.AddControllers ();
            services.AddSwaggerGen (c => {
                c.SwaggerDoc ("v1", new OpenApiInfo { Title = "SimpleBlogApi", Version = "v1" });
            });

            services.AddCors(options => {
                options.AddDefaultPolicy(
                    builder => {
                    builder.AllowAnyOrigin().
                    AllowAnyMethod().
                    AllowAnyHeader();
                });
        });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure (IApplicationBuilder app, IWebHostEnvironment env) {
            if (env.IsDevelopment ()) {
                app.UseDeveloperExceptionPage ();
                app.UseSwagger ();
                app.UseSwaggerUI (c => c.SwaggerEndpoint ("/swagger/v1/swagger.json", "SimpleBlogApi v1"));
            }

            app.UseHttpsRedirection ();

            app.UseRouting ();

            app.UseCors();

            app.UseAuthorization ();

            app.UseEndpoints (endpoints => {
                endpoints.MapControllers ();
            });
        }

        private static async Task<CosmosDbService> InitializeCosmosClientInstanceAsync (IConfigurationSection configurationSection) {
            var databaseName = configurationSection["DatabaseName"];
            var containerName = configurationSection["ContainerName"];
            var account = configurationSection["Account"];
            var key = configurationSection["Key"];
            var client = new Microsoft.Azure.Cosmos.CosmosClient (account, key);
            var database = await client.CreateDatabaseIfNotExistsAsync (databaseName);
            await database.Database.CreateContainerIfNotExistsAsync (containerName, "/id");
            var cosmosDbService = new CosmosDbService (client, databaseName, containerName);
            return cosmosDbService;
        }
    }
}