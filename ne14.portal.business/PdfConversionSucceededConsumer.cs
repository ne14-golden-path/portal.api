// <copyright file="PdfConversionSucceededConsumer.cs" company="ne1410s">
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
public class PdfConversionSucceededConsumer(
    IConnectionFactory connectionFactory,
    ITelemeter telemeter,
    ILogger<PdfConversionSucceededConsumer> logger,
    IConfiguration config)
        : MqTracingConsumer<PdfConversionSucceededMessage>(connectionFactory, telemeter, logger, config)
{
    /// <inheritdoc/>
    public override string ExchangeName => "pdf-conversion-succeeded";

    /// <inheritdoc/>
    public override Task ConsumeAsync(PdfConversionSucceededMessage message, MqConsumerEventArgs args)
    {
        message = message ?? throw new PermanentFailureException();
        logger.LogInformation(
            "API CONSUMER REPORTED PDF CONVERSION SUCCESS: {OldRef} -> {NewRef}",
            message.InboundBlobReference,
            message.OutboundBlobReference);
        return Task.CompletedTask;
    }
}
