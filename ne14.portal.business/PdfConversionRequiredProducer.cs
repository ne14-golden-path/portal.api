// <copyright file="PdfConversionRequiredProducer.cs" company="ne1410s">
// Copyright (c) ne1410s. All rights reserved.
// </copyright>

namespace ne14.portal.business;

using EnterpriseStartup.Mq;
using EnterpriseStartup.Telemetry;
using Microsoft.Extensions.Logging;
using ne14.library.message_contracts.Docs;
using RabbitMQ.Client;

/// <inheritdoc cref="MqTracingProducer{T}"/>
public class PdfConversionRequiredProducer(
    IConnectionFactory session,
    ITelemeter telemeter,
    ILogger<PdfConversionRequiredProducer> logger)
        : MqTracingProducer<PdfConversionRequiredMessage>(session, telemeter, logger)
{
    /// <inheritdoc/>
    public override string ExchangeName => "pdf-conversion-required";
}
