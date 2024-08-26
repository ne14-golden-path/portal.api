// <copyright file="PdfConversionSucceededConsumer.cs" company="ne1410s">
// Copyright (c) ne1410s. All rights reserved.
// </copyright>

namespace ne14.portal.business;

using System.Threading.Tasks;
using EnterpriseStartup.Messaging.Abstractions.Consumer;
using EnterpriseStartup.Mq;
using EnterpriseStartup.SignalR;
using EnterpriseStartup.Telemetry;
using FluentErrors.Extensions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using ne14.library.message_contracts.Docs;
using RabbitMQ.Client;

/// <inheritdoc cref="MqTracingConsumer{T}"/>
public class PdfConversionSucceededConsumer(
    INotifier notifier,
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
        message.MustExist();

        logger.LogInformation(
            "API CONSUMER REPORTED PDF CONVERSION SUCCESS: {OldRef} -> {NewRef}",
            message.InboundBlobReference,
            message.OutboundBlobReference);

        await notifier.Notify(
            message.UserId,
            NoticeLevel.Success,
            $"Upload success!: {message.InboundBlobReference}");
    }
}
