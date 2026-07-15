using Microsoft.Extensions.DependencyInjection;
using RealTimeChat.Application.Interfaces;
using RealTimeChat.Application.Interfaces.Repositories;
using RealTimeChat.Infrastructure.Data;
using RealTimeChat.Infrastructure.Repositories;

namespace RealTimeChat.Infrastructure
{
  public static class DependencyInjection
  {
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
      services.AddScoped<IApplicationDbContext>(provider =>
          provider.GetRequiredService<ApplicationDbContext>());
      services.AddScoped<IMessageRepository, MessageRepository>();
      services.AddScoped<IConversationRepository, ConversationRepository>();
      services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));

      return services;
    }
  }
}