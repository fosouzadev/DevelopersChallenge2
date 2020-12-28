using Microsoft.Extensions.DependencyInjection;
using Repository;
using Domain.Interfaces;
using Domain.Services;

namespace Api.Configuration
{
    public static class DependencyInjection
    {
        public static void AddDependencyInjection(this IServiceCollection services)
        {
            services.AddScoped<IBankTransactionService, BankTransactionService>();
            services.AddScoped<IBankTransactionRepository, BankTransactionRepository>();
        }
    }
}