// <copyright file="Program.cs" company="ne1410s">
// Copyright (c) ne1410s. All rights reserved.
// </copyright>

using ne14.library.startup_extensions.Extensions;
using ne14.library.startup_extensions.Telemetry;
using ne14.portal.business;

[assembly: TraceThis]

var builder = WebApplication.CreateBuilder(args);
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

builder.Services.AddScoped<IBlobRepository, StubBlobRepository>();
builder.Services.AddScoped<INotificationService, NotificationService>();
builder.Services.AddScoped<PdfDomainService>();

var app = builder.Build();
app.UseEnterpriseCors();
app.UseEnterpriseDiscovery(builder.Environment);
app.UseEnterpriseErrorHandling();
app.UseEnterpriseHealthChecks();
app.MapControllers();
app.Run();
