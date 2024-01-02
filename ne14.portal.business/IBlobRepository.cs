// <copyright file="IBlobRepository.cs" company="ne1410s">
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
    /// <param name="directory">The upload directory.</param>
    /// <param name="input">The input.</param>
    /// <returns>The newly generated id.</returns>
    public Task<Guid> UploadAsync(string directory, Stream input);
}
