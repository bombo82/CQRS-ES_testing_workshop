using CqrsMovie.Seats.API.Sagas.Consumers;
using CqrsMovie.Seats.API.Sagas.Persistence;
using CqrsMovie.Seats.Infrastructure.MassTransit.Commands;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Muflone.Eventstore;
using Muflone.MassTransit.RabbitMQ;
using Muflone.Saga.Persistence;

namespace CqrsMovie.Seats.API.Sagas
{
	public class Startup
	{
		public Startup(IConfiguration configuration)
		{
			Configuration = configuration;
		}

		public IConfiguration Configuration { get; }

		public void ConfigureServices(IServiceCollection services)
		{
			services.AddMvc(option => option.EnableEndpointRouting = false);
            services.AddScoped<ISagaRepository, InMemorySagaRepository>();
            services.AddMufloneEventStore(Configuration.GetConnectionString("EventStore"));
			services.AddScoped<ISerializer, Serializer>();

			services.Configure<ServiceBusOptions>(Configuration.GetSection("MassTransit:RabbitMQ"));
			var serviceBusOptions = new ServiceBusOptions();
			Configuration.GetSection("MassTransit:RabbitMQ").Bind(serviceBusOptions);

			services.AddMufloneMassTransitWithRabbitMQ(serviceBusOptions, x =>
			{
				//x.AddConsumer<StartBookSeatsSagaConsumer>();

                x.AddConsumer<BookSeatsConsumer>();
			});

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "CQRS-ES Workshop", Version = "v1", Description = "Web Api Services for CQRS-ES workshop" });
            });
		}

		public void Configure(IApplicationBuilder app, IHostEnvironment env)
		{
			if (env.IsDevelopment())
				app.UseDeveloperExceptionPage();
			else
				app.UseHsts();

			app.UseAuthentication();
			app.UseFileServer();
			app.UseRouting();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    "default",
                    "{controller=Home}/{action=Index}/{id?}");
                endpoints.MapRazorPages();
            });

            app.UseSwagger(c =>
            {
                c.RouteTemplate = "documentation/{documentName}/documentation.json";
                c.SerializeAsV2 = true;
            });
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/documentation/v1/documentation.json", "CQRS Movie Sagas API v1");
            });
		}
	}
}
