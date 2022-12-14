using CardPaymentServiceAPI.DatabaseConnection.DBContext;
using CardPaymentServiceAPI.Services;
using CardPaymentServiceAPI.Services.Interface;
using CardPaymentServiceAPI.Utilitiy;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;

namespace CardPaymentServiceAPI
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
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "CardPaymentServiceAPI", Version = "v1" });
            });
            services.AddScoped<IEncryptionService, EncryptionService>();
            services.AddScoped<IFintechService, FintechService>();
            services.AddScoped<IPaymentService, PaymentService>();
            services.AddScoped<ICardsDetails, CardService>();
            services.AddAutoMapper(typeof(AutoMapperProfile));
            // For Entity Framework  
            services.AddDbContext<CardPaymentServiceDbContext>(options => options.UseSqlServer(Configuration.GetConnectionString("ConnStr")));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();

            }
            app.UseSwagger();
            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "CardPaymentServiceAPI v1"));
            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
