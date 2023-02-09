using Actio.Api.Handlers;
using Actio.Common.Events;
using Actio.Common.Events.Shared;
using Actio.Common.RabbitMq;
using IHostingEnvironment = Microsoft.AspNetCore.Hosting.IHostingEnvironment;

namespace Actio.Api;

public class Startup
{
    public Startup(IConfiguration configuration)
    {
        Configuration = configuration;

    }

    public IConfiguration Configuration { get; }

    public void ConfigureServices(IServiceCollection services)
    {

        services.AddControllers();
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();
        services.AddRabbitMq(Configuration);
        services.AddSingleton<IEventHandler<ActivityCreated>, ActivityCreatedHandler>();

    }
    public void Configure(IApplicationBuilder app, IHostingEnvironment env)
    {
        var scope = app.ApplicationServices.CreateScope();
        var service = scope.ServiceProvider.GetService<IEventHandler<ActivityCreated>>();
        // Configure the HTTP request pipeline.
        if (env.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();
        app.UseRouting();
        app.UseAuthorization();

        app.UseEndpoints(endpoints => endpoints.MapControllers());
    }

}