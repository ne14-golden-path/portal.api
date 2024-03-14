// <copyright file="AzureBlobRepository.cs" company="ne1410s">
// Copyright (c) ne1410s. All rights reserved.
// </copyright>

namespace ne14.portal.business;

using Azure.Storage.Blobs;

/// <inheritdoc cref="IBlobRepository"/>
public class AzureBlobRepository(BlobServiceClient blobService) : IBlobRepository
{
    /// <inheritdoc/>
    public async Task<Guid> UploadAsync(string containerName, string fileName, Stream content)
    {
        var container = blobService.GetBlobContainerClient(containerName);
        await container.CreateIfNotExistsAsync();

        var reference = Guid.NewGuid();
        var blob = container.GetBlobClient(reference.ToString());
        await blob.UploadAsync(content);
        await blob.SetMetadataAsync(new Dictionary<string, string> { ["filename"] = fileName });

        return reference;
    }
}
