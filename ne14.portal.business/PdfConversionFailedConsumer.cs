// <copyright file="PdfConversionFailedConsumer.cs" company="ne1410s">
// Copyright (c) ne1410s. All rights reserved.
// </copyright>

namespace ne14.portal.business;

using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using ne14.library.message_contracts.Docs;
using ne14.library.messaging.Abstractions.Consumer;
using ne14.library.startup_extensions.Mq;
using ne14.library.startup_extensions.Telemetry;
using RabbitMQ.Client;

/// <inheritdoc cref="MqTracingConsumer{T}"/>
public class PdfConversionFailedConsumer(
    IConnectionFactory connectionFactory,
    ITelemeter telemeter,
    ILogger<PdfConversionFailedConsumer> logger,
    IConfiguration config)
        : MqTracingConsumer<PdfConversionFailedMessage>(connectionFactory, telemeter, logger, config)
{
    /// <inheritdoc/>
    public override string ExchangeName => "pdf-conversion-failed";

    /// <inheritdoc/>
    public override Task ConsumeAsync(PdfConversionFailedMessage message, MqConsumerEventArgs args)
    {
        message = message ?? throw new PermanentFailureException();

        throw new ArithmeticException("lulz");

        logger.LogWarning("API CONSUMER REPORTED PDF CONVERSION FAILURE: {Id}", message.InboundBlobReference);
        return Task.CompletedTask;
    }
}
