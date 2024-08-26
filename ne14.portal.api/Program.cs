// <copyright file="Program.cs" company="ne1410s">
// Copyright (c) ne1410s. All rights reserved.
// </copyright>

using EnterpriseStartup.Extensions;
using EnterpriseStartup.Telemetry;
using Microsoft.Extensions.Azure;
using ne14.portal.business;

[assembly: TraceThis]

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddEnterpriseCors();
builder.Services.AddEnterpriseDiscovery();
builder.Services.AddEnterpriseErrorHandling();
builder.Services.AddEnterpriseHealthChecks();
builder.Services.AddEnterpriseSignalR();
builder.Services.AddEnterpriseTelemetry(builder.Configuration);
builder.Services.AddEnterpriseMq(builder.Configuration)
    .AddMqProducer<PdfConversionRequiredProducer>()
    .AddMqConsumer<PdfConversionSucceededConsumer>()
    .AddMqConsumer<PdfConversionFailedConsumer>();
builder.Services.AddEnterpriseB2C(builder.Configuration);
builder.Services.AddControllers();

var storageConnection = builder.Configuration["AzureClients:LocalBlob"];
builder.Services.AddAzureClients(opts => opts.AddBlobServiceClient(storageConnection));
builder.Services.AddScoped<IBlobRepository, AzureBlobRepository>();
builder.Services.AddScoped<PdfDomainService>();

var app = builder.Build();
app.UseEnterpriseCors();
app.UseEnterpriseDiscovery(builder.Environment);
app.UseEnterpriseErrorHandling();
app.UseEnterpriseHealthChecks();
app.UseEnterpriseB2C();
app.UseEnterpriseSignalR();
await app.RunAsync();