// <copyright file="Program.cs" company="ne1410s">
// Copyright (c) ne1410s. All rights reserved.
// </copyright>

using EnterpriseStartup.Extensions;
using EnterpriseStartup.Telemetry;
using Microsoft.Extensions.Azure;
using ne14.portal.business;
using ne14.portal.business.Features.Blob;

[assembly: TraceThis]

var builder = WebApplication.CreateBuilder(args);
var config = builder.Configuration;
builder.Services.AddEnterpriseCors(config["Cors:Origins"]!.Split(','));
builder.Services.AddEnterpriseDiscovery();
builder.Services.AddEnterpriseErrorHandling();
builder.Services.AddEnterpriseHealthChecks().AddAzureBlobStorage(timeout: TimeSpan.FromSeconds(5));
builder.Services.AddEnterpriseSignalR(config);
builder.Services.AddEnterpriseTelemetry(config);
builder.Services.AddEnterpriseMq(config)
    .AddMqProducer<PdfConversionRequiredProducer>()
    .AddMqConsumer<PdfConversionSucceededConsumer>()
    .AddMqConsumer<PdfConversionFailedConsumer>();
builder.Services.AddEnterpriseB2C(config);
builder.Services.AddControllers();

var storageConnection = config["AzureClients:LocalBlob"];
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