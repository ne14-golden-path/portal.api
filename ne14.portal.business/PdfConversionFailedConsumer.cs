// <copyright file="PdfConversionFailedConsumer.cs" company="ne1410s">
// Copyright (c) ne1410s. All rights reserved.
// </copyright>

namespace ne14.portal.business;

using System.Threading.Tasks;
using FluentErrors.Extensions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using ne14.library.message_contracts.Docs;
using ne14.library.messaging.Abstractions.Consumer;
using ne14.library.startup_extensions.Mq;
using ne14.library.startup_extensions.Telemetry;
using ne14.portal.business.Signals;
using RabbitMQ.Client;

/// <inheritdoc cref="MqTracingConsumer{T}"/>
public class PdfConversionFailedConsumer(
    INotificationService notifier,
    IConnectionFactory connectionFactory,
    ITelemeter telemeter,
    ILogger<PdfConversionFailedConsumer> logger,
    IConfiguration config)
        : MqTracingConsumer<PdfConversionFailedMessage>(connectionFactory, telemeter, logger, config)
{
    /// <inheritdoc/>
    public override string ExchangeName => "pdf-conversion-failed";

    /// <inheritdoc/>
    public override async Task ConsumeAsync(PdfConversionFailedMessage message, MqConsumerEventArgs args)
    {
        message.MustExist();
        logger.LogWarning("API CONSUMER REPORTED PDF CONVERSION FAILURE: {Id}", message.InboundBlobReference);
        await notifier.NotifyAsync(Guid.NewGuid(), $"Failed to upload: {message.FailureReason}");
    }
}
