// <copyright file="NotificationService.cs" company="ne1410s">
// Copyright (c) ne1410s. All rights reserved.
// </copyright>

namespace ne14.portal.business;

/// <inheritdoc cref="INotificationService"/>
public class NotificationService : INotificationService
{
    /// <inheritdoc/>
    public Task NotifyAsync(Guid userId, string message)
    {
        Console.WriteLine($"INCOMING NOTIFICATION!! (message: {message})");
        return Task.CompletedTask;
    }
}
