using Application.IServices;
using Application.Services;
using Infrastructure.Mongo;
using Hangfire;
using Microsoft.Extensions.Configuration;
using Infrastructure.ScopedServices;

public class Startup
{
    public Startup(IConfiguration configuration)
    {
        Configuration = configuration;
    }

    public IConfiguration Configuration { get; }

    // Método para configurar os serviços
    public void ConfigureServices(IServiceCollection services)
    {
        // Configuração do MongoDB
        MongoDbConfig.ConfigureMongoDb(services, Configuration);
        ScopedServices.AddScopedServices(services);
        
        // Adição dos controladores
        services.AddControllers();

        // Configuração do Swagger
        services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo { Title = "CyberPay", Version = "v1" });
        });
    }

    // Método para configurar a aplicação no pipeline de requisições HTTP
    public void Configure(IApplicationBuilder app)
    {
        // Página de exceções para ambiente de desenvolvimento
        app.UseDeveloperExceptionPage();

        // Configuração do Swagger
        app.UseSwagger();
        app.UseSwaggerUI(c =>
        {
            c.SwaggerEndpoint("/swagger/v1/swagger.json", "CyberPay");
        });

        // Redirecionamento HTTP para HTTPS
        app.UseHttpsRedirection();

        // Roteamento
        app.UseRouting();

        // Autorização
        app.UseAuthorization();

        // Mapeamento dos endpoints dos controladores
        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
        });
            
        // Configuração do painel do Hangfire
        app.UseHangfireDashboard();
        app.UseHangfireServer();
    }
}
