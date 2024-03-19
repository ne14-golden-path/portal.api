// <copyright file="INotificationService.cs" company="ne1410s">
// Copyright (c) ne1410s. All rights reserved.
// </copyright>

namespace ne14.portal.business.Signals;

/// <summary>
/// Notification service.
/// </summary>
public interface INotificationService
{
    /// <summary>
    /// Emits a notification.
    /// </summary>
    /// <param name="userId">The user id.</param>
    /// <param name="message">The message.</param>
    /// <returns>Async task.</returns>
    public Task NotifyAsync(Guid userId, string message);
}
