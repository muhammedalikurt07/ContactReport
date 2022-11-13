using Bll.Services.Abstractions;
using Bll.Services.Concretes;
using Dal.Abstractions;
using Dal.Concretes;
using Dal.Contexts;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ContactReport
{
    public class Startup
    {
        public Startup(IConfiguration _configuration)
        {
            _Configuration = _configuration;
        }

        public IConfiguration _Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection _services)
        {

            _services.AddControllers().AddJsonOptions(_options =>
            {
                _options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.Preserve;
            });
            _services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "ContactReport", Version = "v1" });
            });

            _services.AddDbContext<ContactDbContext>(_options =>
            {
                _options.UseNpgsql(_Configuration.GetConnectionString("Postgre"));
            });

            _services.AddScoped<IUnitOfWork, UnitOfWork>();

            _services.AddScoped<IContactService, ContactService>();

            _services.AddScoped<IContactInformationService, ContactInformationService>();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder _app, IWebHostEnvironment _env)
        {
            if (_env.IsDevelopment())
            {
                _app.UseDeveloperExceptionPage();
                _app.UseSwagger();
                _app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "ContactReport v1"));
            }

            _app.UseHttpsRedirection();

            _app.UseRouting();

            _app.UseAuthorization();

            _app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
