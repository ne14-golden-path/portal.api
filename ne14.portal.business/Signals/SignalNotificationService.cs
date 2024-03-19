// <copyright file="SignalNotificationService.cs" company="ne1410s">
// Copyright (c) ne1410s. All rights reserved.
// </copyright>

namespace ne14.portal.business.Signals;

using Microsoft.AspNetCore.SignalR;

/// <inheritdoc cref="INotificationService"/>
public class SignalNotificationService : Hub<INotificationService>, INotificationService
{
    /// <inheritdoc/>
    public async Task NotifyAsync(Guid userId, string message)
    {
        Console.WriteLine($"INCOMING NOTIFICATION!! (message: {message})");

        var allClients = this.Clients?.All;
        if (allClients != null)
        {
            await allClients.NotifyAsync(userId, message);
        }
    }
}
