using EshopMonolithic.API.Application.Commands;
using EshopMonolithic.Domain.Interfaces;
using EshopMonolithic.Infrastructure.Idempotency;
using EshopMonolithic.Infrastructure.Repositories;
using EshopMonolithic.Infrastructure;
using FluentValidation;
using EshopMonolithic.API.Application.Behaviors;
using EshopMonolithic.API.Application.Validations;
using Microsoft.EntityFrameworkCore;
using EshopMonolithic.API.Application.Queries;
using EshopMonolithic.API.Infrastructure;

namespace EshopMonolithic.API.Extensions
{
    public static class Extensions
    {
        public static void AddApplicationServices(this WebApplicationBuilder builder)
        {
            var services = builder.Services;

            // Add the authentication services to DI
            //services.AddDefaultAuthentication();

            // Pooling is disabled because of the following error:
            // Unhandled exception. System.InvalidOperationException:
            // The DbContext of type 'OrderingContext' cannot be pooled because it does not have a public constructor accepting a single parameter of type DbContextOptions or has more than one constructor.
            services.AddDbContext<OrderingContext>(options =>
            {
                options.UseNpgsql(builder.Configuration.GetConnectionString("orderingdb"));
            });
            //services.EnrichNpgsqlDbContext<OrderingContext>();

            services.AddMigration<OrderingContext, OrderingContextSeed>();

            // Add the integration services that consume the DbContext
            //services.AddTransient<IIntegrationEventLogService, IntegrationEventLogService<OrderingContext>>();

            //services.AddTransient<IOrderingIntegrationEventService, OrderingIntegrationEventService>();

            //builder.AddRabbitMqEventBus("eventbus")
            //       .AddEventBusSubscriptions();

            services.AddHttpContextAccessor();
            //services.AddTransient<IIdentityService, IdentityService>();

            // Configure mediatR
            services.AddMediatR(cfg =>
            {
                cfg.RegisterServicesFromAssemblyContaining(typeof(Program));

                cfg.AddOpenBehavior(typeof(LoggingBehavior<,>));
                cfg.AddOpenBehavior(typeof(ValidatorBehavior<,>));
                cfg.AddOpenBehavior(typeof(TransactionBehavior<,>));
            });

            // Register the command validators for the validator behavior (validators based on FluentValidation library)
            services.AddSingleton<IValidator<CancelOrderCommand>, CancelOrderCommandValidator>();
            services.AddSingleton<IValidator<CreateOrderCommand>, CreateOrderCommandValidator>();
            services.AddSingleton<IValidator<IdentifiedCommand<CreateOrderCommand, bool>>, IdentifiedCommandValidator>();
            services.AddSingleton<IValidator<ShipOrderCommand>, ShipOrderCommandValidator>();

            services.AddScoped<IOrderQueries, OrderQueries>();
            services.AddScoped<IBuyerRepository, BuyerRepository>();
            services.AddScoped<IOrderRepository, OrderRepository>();
            services.AddScoped<IRequestManager, RequestManager>();
        }

        //private static void AddEventBusSubscriptions(this IEventBusBuilder eventBus)
        //{
        //    eventBus.AddSubscription<GracePeriodConfirmedIntegrationEvent, GracePeriodConfirmedIntegrationEventHandler>();
        //    eventBus.AddSubscription<OrderStockConfirmedIntegrationEvent, OrderStockConfirmedIntegrationEventHandler>();
        //    eventBus.AddSubscription<OrderStockRejectedIntegrationEvent, OrderStockRejectedIntegrationEventHandler>();
        //    eventBus.AddSubscription<OrderPaymentFailedIntegrationEvent, OrderPaymentFailedIntegrationEventHandler>();
        //    eventBus.AddSubscription<OrderPaymentSucceededIntegrationEvent, OrderPaymentSucceededIntegrationEventHandler>();
        //}
    }
}
