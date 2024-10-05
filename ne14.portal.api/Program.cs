// <copyright file="Program.cs" company="ne1410s">
// Copyright (c) ne1410s. All rights reserved.
// </copyright>

using EnterpriseStartup.Extensions;
using EnterpriseStartup.Telemetry;
using ne14.portal.business;

[assembly: TraceThis]

var builder = WebApplication.CreateBuilder(args);
var config = builder.Configuration;
builder.Services.AddEnterpriseCors(config["Cors:Origins"]!.Split(','));
builder.Services.AddEnterpriseDiscovery();
builder.Services.AddEnterpriseErrorHandling();
builder.Services.AddEnterpriseHealthChecks();
builder.Services.AddEnterpriseSignalR(config);
builder.Services.AddEnterpriseTelemetry(config);
builder.Services.AddEnterpriseMq(config)
    .AddMqProducer<PdfConversionRequiredProducer>()
    .AddMqConsumer<PdfConversionSucceededConsumer>()
    .AddMqConsumer<PdfConversionFailedConsumer>();
builder.Services.AddEnterpriseB2C(config);
builder.Services.AddEnterpriseUserBlobs(config);
builder.Services.AddControllers();
builder.Services.AddScoped<PdfDomainService>();

var app = builder.Build();
app.UseEnterpriseCors();
app.UseEnterpriseDiscovery(builder.Environment);
app.UseEnterpriseErrorHandling();
app.UseEnterpriseHealthChecks();
app.UseEnterpriseB2C();
app.UseEnterpriseSignalR();
await app.RunAsync();