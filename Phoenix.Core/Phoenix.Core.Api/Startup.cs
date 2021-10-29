// ------------------------------------------------
// Copyright (c) Coalitions of Inspired Developers
// FREE TO USE FOR THE WORLD
// ------------------------------------------------

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;

namespace Phoenix.Core.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)=> Configuration = configuration;
        

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc(
                    name: "v1", 
                    info: new OpenApiInfo { 
                        Title = "Phoenix.Core.Api", 
                        Version = "v1" }
                    );
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(
            IApplicationBuilder applicationBuilder,
            IWebHostEnvironment webHostEnvironment)
        {
            if (webHostEnvironment.IsDevelopment())
            {
                applicationBuilder.UseDeveloperExceptionPage();
                applicationBuilder.UseSwagger();
                applicationBuilder.UseSwaggerUI(options => 
                    options.SwaggerEndpoint(
                        url:"/swagger/v1/swagger.json",
                        name:"Phoenix.Core.Api v1"));
            }

            applicationBuilder.UseHttpsRedirection();
            applicationBuilder.UseRouting();
            applicationBuilder.UseAuthorization();
            applicationBuilder.UseEndpoints(endpoints => endpoints.MapControllers());
        }
    }
}
