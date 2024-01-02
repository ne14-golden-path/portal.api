// <copyright file="PdfConversionSucceededConsumer.cs" company="ne1410s">
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
public class PdfConversionSucceededConsumer(
    IRabbitMqSession session,
    ITelemeter telemeter,
    ILogger<PdfConversionSucceededConsumer> logger)
        : TracedMqConsumer<PdfConversionSucceededMessage>(session, telemeter, logger)
{
    /// <inheritdoc/>
    public override string ExchangeName => "pdf-conversion-failed";

    /// <inheritdoc/>
    public override Task Consume(PdfConversionSucceededMessage message, ConsumerContext context)
    {
        message = message ?? throw new PermanentFailureException();
        this.Logger.LogInformation(
            "API CONSUMER REPORTED PDF CONVERSION SUCCESS: {OldRef} -> {NewRef}",
            message.InboundBlobReference,
            message.OutboundBlobReference);
        return Task.CompletedTask;
    }
}
