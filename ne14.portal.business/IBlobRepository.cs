﻿// <copyright file="IBlobRepository.cs" company="ne1410s">
// Copyright (c) ne1410s. All rights reserved.
// </copyright>

namespace ne14.portal.business;

/// <summary>
/// Blob repository.
/// </summary>
public interface IBlobRepository
{
    /// <summary>
    /// Uploads a new blob.
    /// </summary>
    /// <param name="containerName">The container name.</param>
    /// <param name="fileName">The file name.</param>
    /// <param name="content">The content.</param>
    /// <returns>The newly generated blob reference.</returns>
    public Task<Guid> UploadAsync(string containerName, string fileName, Stream content);
}
