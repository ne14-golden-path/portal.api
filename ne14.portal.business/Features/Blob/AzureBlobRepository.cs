// <copyright file="AzureBlobRepository.cs" company="ne1410s">
// Copyright (c) ne1410s. All rights reserved.
// </copyright>

namespace ne14.portal.business.Features.Blob;

using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using FluentErrors.Extensions;

/// <inheritdoc cref="IBlobRepository"/>
public class AzureBlobRepository(BlobServiceClient blobService) : IBlobRepository
{
    /// <inheritdoc/>
    public async Task<Guid> UploadAsync(string containerName, string userId, string fileName, Stream content)
    {
        var container = blobService.GetBlobContainerClient(containerName);
        if (!await container.ExistsAsync())
        {
            await container.CreateAsync();
        }

        var blobReference = Guid.NewGuid();
        var metadata = new Dictionary<string, string> { ["filename"] = fileName };
        var blob = container.GetBlobClient($"{userId}/{blobReference}");
        var uploadResult = await blob.UploadAsync(content, new BlobUploadOptions { Metadata = metadata });
        uploadResult.GetRawResponse().IsError.MustBe(false);

        return blobReference;
    }

    /// <inheritdoc/>
    public async Task<List<BlobListing>> ListAsync(string containerName, string userId)
    {
        var container = blobService.GetBlobContainerClient(containerName);
        var retVal = new List<BlobListing>();
        await foreach (var blob in container.GetBlobsAsync(prefix: $"{userId}/"))
        {
            var size = blob.Properties.ContentLength ?? 0;
            var blobRef = Guid.Parse(blob.Name.Split('/')[^1]);
            retVal.Add(new(blobRef, blob.Metadata["filename"], size));
        }

        return retVal;
    }
}
