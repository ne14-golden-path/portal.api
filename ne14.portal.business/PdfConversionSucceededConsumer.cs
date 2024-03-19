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
using ne14.portal.business.Signals;
using RabbitMQ.Client;

/// <inheritdoc cref="MqTracingConsumer{T}"/>
public class PdfConversionSucceededConsumer(
    INotificationService notifier,
    IConnectionFactory connectionFactory,
    ITelemeter telemeter,
    ILogger<PdfConversionSucceededConsumer> logger,
    IConfiguration config)
        : MqTracingConsumer<PdfConversionSucceededMessage>(connectionFactory, telemeter, logger, config)
{
    /// <inheritdoc/>
    public override string ExchangeName => "pdf-conversion-succeeded";

    /// <inheritdoc/>
    public override async Task ConsumeAsync(PdfConversionSucceededMessage message, MqConsumerEventArgs args)
    {
        logger.LogInformation(
            "API CONSUMER REPORTED PDF CONVERSION SUCCESS: {OldRef} -> {NewRef}",
            message.InboundBlobReference,
            message.OutboundBlobReference);

        await notifier.NotifyAsync(Guid.NewGuid(), $"Upload success!: {message.InboundBlobReference}");
    }
}
