using CqrsMovie.Seats.API.Sagas.Consumers;
using CqrsMovie.Seats.API.Sagas.Persistence;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
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
            services.AddScoped<ISerializer, Serializer>();

			services.Configure<ServiceBusOptions>(Configuration.GetSection("MassTransit:RabbitMQ"));
			var serviceBusOptions = new ServiceBusOptions();
			Configuration.GetSection("MassTransit:RabbitMQ").Bind(serviceBusOptions);

			services.AddMufloneMassTransitWithRabbitMQ(serviceBusOptions, x =>
			{
				x.AddConsumer<StartBookSeatsSagaConsumer>();
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
		}
	}
}
