// <copyright file="PdfConversionFailedConsumer.cs" company="ne1410s">
// Copyright (c) ne1410s. All rights reserved.
// </copyright>

namespace ne14.portal.business;

using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using ne14.library.message_contracts.Docs;
using ne14.library.rabbitmq.Consumer;
using ne14.library.rabbitmq.Exceptions;
using ne14.library.rabbitmq.Vendor;
using ne14.library.startup_extensions.Mq;
using ne14.library.startup_extensions.Telemetry;

/// <inheritdoc cref="TracedMqConsumer{T}"/>
public class PdfConversionFailedConsumer(
    IRabbitMqSession session,
    ITelemeter telemeter,
    ILogger<PdfConversionFailedConsumer> logger)
        : TracedMqConsumer<PdfConversionFailedMessage>(session, telemeter, logger)
{
    /// <inheritdoc/>
    public override string ExchangeName => "pdf-conversion-failed";

    /// <inheritdoc/>
    public override Task Consume(PdfConversionFailedMessage message, ConsumerContext context)
    {
        message = message ?? throw new PermanentFailureException();
        this.Logger.LogWarning("API CONSUMER REPORTED PDF CONVERSION FAILURE: {Id}", message.InboundBlobReference);
        return Task.CompletedTask;
    }
}
