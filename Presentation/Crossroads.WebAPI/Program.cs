
using Crossroads.Application.Extensions;
using Crossroads.Application.Features.AppUser.Commands.AddAppUser;
using Crossroads.Persistence.Extensions;
using Crossroads.WebAPI.Extensions;

namespace Crossroads.WebAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddCrossroadsDbContextConfigurations(builder.Configuration);
            builder.Services.AddRepositories();
            builder.Services.AddJwtConfigurations(builder.Configuration);
            builder.Services.AddEmailExtensions(builder.Configuration);
            builder.Services.AddFeatureExtensions(builder.Configuration);
            builder.Services.AddFluentValidationWithAssemblies();

            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseStaticFiles();
            app.UseHttpsRedirection();
            app.UseAuthorization();
            app.MapControllers();
            app.Run();
        }
    }
}