// <copyright file="Program.cs" company="ne1410s">
// Copyright (c) ne1410s. All rights reserved.
// </copyright>

using ne14.library.startup_extensions;
using ne14.library.startup_extensions.Telemetry;

[assembly: TraceThis]

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddEnterpriseCors();
builder.Services.AddEnterpriseDiscovery();
builder.Services.AddEnterpriseErrorHandling();
builder.Services.AddEnterpriseHealthChecks();
builder.Services.AddEnterpriseTelemetry(builder.Configuration);
builder.Services.AddControllers();

var app = builder.Build();
app.UseEnterpriseCors();
app.UseEnterpriseDiscovery(builder.Environment);
app.UseEnterpriseErrorHandling();
app.UseEnterpriseHealthChecks();
app.MapControllers();
app.Run();
