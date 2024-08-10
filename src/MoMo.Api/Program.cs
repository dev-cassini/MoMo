using MoMo.Api.Endpoints;
using MoMo.Modules.LeadImporter.Infrastructure;
using MoMo.Modules.Leads.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddEndpointsApiExplorer()
    .AddSwaggerGen()
    .AddMediatR(configuration =>
    {
        configuration
            .RegisterServicesFromAssemblyContaining<MoMo.Modules.Firms.Application.Marker>()
            .RegisterServicesFromAssemblyContaining<MoMo.Modules.LeadDistribution.Application.Marker>()
            .RegisterServicesFromAssemblyContaining<MoMo.Modules.LeadImporter.Application.Marker>()
            .RegisterServicesFromAssemblyContaining<MoMo.Modules.Leads.Application.Marker>()
            .RegisterServicesFromAssemblyContaining<MoMo.Modules.Leads.Integrations.Inbound.Marker>();
    })
    .AddLeadImporterInfrastructureServices()
    .AddLeadInfrastructure();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app
    .RegisterEndpoints()
    .UseHttpsRedirection();

app.Run();

public partial class Program;