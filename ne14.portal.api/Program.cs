// <copyright file="Program.cs" company="ne1410s">
// Copyright (c) ne1410s. All rights reserved.
// </copyright>

using Microsoft.Extensions.Azure;
using ne14.library.startup_extensions.Extensions;
using ne14.library.startup_extensions.Telemetry;
using ne14.portal.business;
using ne14.portal.business.Signals;

[assembly: TraceThis]

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddSignalR();
builder.Services.AddEnterpriseCors();
builder.Services.AddEnterpriseDiscovery();
builder.Services.AddEnterpriseErrorHandling();
builder.Services.AddEnterpriseHealthChecks();
builder.Services.AddEnterpriseTelemetry(builder.Configuration);
builder.Services.AddEnterpriseMq(builder.Configuration);
builder.Services.AddMqProducer<PdfConversionRequiredProducer>();
builder.Services.AddMqConsumer<PdfConversionSucceededConsumer>();
builder.Services.AddMqConsumer<PdfConversionFailedConsumer>();
builder.Services.AddControllers();

var storageConnection = builder.Configuration["AzureClients:LocalBlob"];
builder.Services.AddAzureClients(opts => opts.AddBlobServiceClient(storageConnection));
builder.Services.AddScoped<IBlobRepository, AzureBlobRepository>();
builder.Services.AddScoped<PdfDomainService>();
builder.Services.AddScoped<INotificationService, SignalNotificationService>();

var app = builder.Build();
app.UseEnterpriseCors();
app.UseEnterpriseDiscovery(builder.Environment);
app.UseEnterpriseErrorHandling();
app.UseEnterpriseHealthChecks();
app.MapControllers();
app.MapHub<SignalNotificationService>("/hub");
app.Run();