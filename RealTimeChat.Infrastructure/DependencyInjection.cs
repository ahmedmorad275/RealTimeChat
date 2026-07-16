using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RealTimeChat.Application.Interfaces;
using RealTimeChat.Application.Interfaces.Repositories;
using RealTimeChat.Domain.Entities;
using RealTimeChat.Infrastructure.Data;
using RealTimeChat.Infrastructure.Repositories;

namespace RealTimeChat.Infrastructure
{
  public static class DependencyInjection
  {
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {

      services.AddDbContext<ApplicationDbContext>(options =>
          options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

      services.AddScoped<IApplicationDbContext>(provider =>
          provider.GetRequiredService<ApplicationDbContext>());

      // services.AddIde<ApplicationUser, IdentityRole<Guid>(options =>
      // {
        
      // });
      
      services.AddScoped<IMessageRepository, MessageRepository>();
      
      services.AddScoped<IConversationRepository, ConversationRepository>();
      
      services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));

      return services;
    }
  }
}