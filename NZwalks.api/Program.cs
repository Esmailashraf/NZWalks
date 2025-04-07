
using Microsoft.EntityFrameworkCore;
using NZwalks.api.Data;
using NZwalks.api.Mapping;
using NZwalks.api.Repositories;

namespace NZwalks.api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddDbContext<NZWalksDbContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("con"));
            });
            builder.Services.AddScoped<IRegionRepository, SqlRegionRepository>();
            builder.Services.AddScoped<IWalkRepository, SqlWalkRepository>();

            builder.Services.AddAutoMapper(typeof(AutoMapperProfiles));

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
