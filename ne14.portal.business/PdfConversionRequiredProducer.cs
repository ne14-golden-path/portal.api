// <copyright file="PdfConversionRequiredProducer.cs" company="ne1410s">
// Copyright (c) ne1410s. All rights reserved.
// </copyright>

namespace ne14.portal.business;

using Microsoft.Extensions.Logging;
using ne14.library.message_contracts.Docs;
using ne14.library.rabbitmq.Vendor;
using ne14.library.startup_extensions.Mq;
using ne14.library.startup_extensions.Telemetry;

/// <inheritdoc cref="TracedMqProducer{T}"/>
public class PdfConversionRequiredProducer(
    IRabbitMqSession session,
    ITelemeter telemeter,
    ILogger<PdfConversionRequiredProducer> logger)
        : TracedMqProducer<PdfConversionRequiredMessage>(session, telemeter, logger)
{
    /// <inheritdoc/>
    public override string ExchangeName => "pdf-conversion-required";
}
