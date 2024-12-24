using Lumix.Core.Interfaces.Repositories;
using Lumix.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Lumix.Persistence
{
    public static class PersistenceExtensions
    {
        public static IServiceCollection AddPersistence(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<LumixDbContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
            });
            services.AddScoped<IUsersRepository, UsersRepository>();
            services.AddScoped<IAuthRepository, AuthRepository>();
			services.AddScoped<IPhotosRepository, PhotosRepository>();
            services.AddScoped<ILikesRepository, LikesRepository>();
            services.AddScoped<ICommentsRepository, CommentsRepository>();
			return services;
        }
    }
}