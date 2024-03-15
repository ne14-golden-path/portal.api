// <copyright file="AzureBlobRepository.cs" company="ne1410s">
// Copyright (c) ne1410s. All rights reserved.
// </copyright>

namespace ne14.portal.business;

using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using ne14.library.fluent_errors.Extensions;

/// <inheritdoc cref="IBlobRepository"/>
public class AzureBlobRepository(BlobServiceClient blobService) : IBlobRepository
{
    /// <inheritdoc/>
    public async Task<Guid> UploadAsync(string containerName, string fileName, Stream content)
    {
        var container = blobService.GetBlobContainerClient(containerName);
        var createResult = await container.CreateIfNotExistsAsync();
        createResult.GetRawResponse().IsError.MustBe(false);

        var blobReference = Guid.NewGuid();
        var metadata = new Dictionary<string, string> { ["filename"] = fileName };
        var blob = container.GetBlobClient(blobReference.ToString());
        var uploadResult = await blob.UploadAsync(content, new BlobUploadOptions { Metadata = metadata });
        uploadResult.GetRawResponse().IsError.MustBe(false);

        return blobReference;
    }
}
