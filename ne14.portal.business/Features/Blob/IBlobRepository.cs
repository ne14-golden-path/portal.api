// <copyright file="IBlobRepository.cs" company="ne1410s">
// Copyright (c) ne1410s. All rights reserved.
// </copyright>

namespace ne14.portal.business.Features.Blob;

/// <summary>
/// Blob repository.
/// </summary>
public interface IBlobRepository
{
    /// <summary>
    /// Uploads a new blob.
    /// </summary>
    /// <param name="containerName">The container name.</param>
    /// <param name="userId">The user id.</param>
    /// <param name="fileName">The file name.</param>
    /// <param name="content">The content.</param>
    /// <returns>The newly generated blob reference.</returns>
    public Task<Guid> UploadAsync(string containerName, string userId, string fileName, Stream content);

    /// <summary>
    /// Lists blobs in the given container.
    /// </summary>
    /// <param name="containerName">The container name.</param>
    /// <param name="userId">The user id.</param>
    /// <returns>The listing result.</returns>
    public Task<List<BlobListing>> ListAsync(string containerName, string userId);

    /// <summary>
    /// Downloads a blob.
    /// </summary>
    /// <param name="containerName">The container name.</param>
    /// <param name="userId">The user id.</param>
    /// <param name="blobReference">The blob reference.</param>
    /// <returns>The blob.</returns>
    public Task<BlobMeta> DownloadAsync(string containerName, string userId, Guid blobReference);

    /// <summary>
    /// Deletes a blob.
    /// </summary>
    /// <param name="containerName">The container name.</param>
    /// <param name="userId">The user id.</param>
    /// <param name="blobReference">The blob reference.</param>
    /// <returns>Async task.</returns>
    public Task DeleteAsync(string containerName, string userId, Guid blobReference);
}
