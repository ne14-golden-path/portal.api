// <copyright file="PdfDomainService.cs" company="ne1410s">
// Copyright (c) ne1410s. All rights reserved.
// </copyright>

namespace ne14.portal.business;

using ne14.portal.business.Features.Blob;

/// <summary>
/// Domain service for managing pdfs.
/// </summary>
public class PdfDomainService(
    IBlobRepository blobRepo,
    PdfConversionRequiredProducer mqProducer)
{
    private const string TriageContainer = "triage";
    private const string ConvertedContainer = "converted";

    /// <summary>
    /// Processes document uploads by uploading to a triage account and
    /// notifying the system of the event. The process is automated thereafter.
    /// </summary>
    /// <param name="userId">The user id.</param>
    /// <param name="input">The input.</param>
    /// <param name="fileName">The file name.</param>
    /// <returns>The temporary triage id.</returns>
    public async Task<Guid> UploadToTriage(string userId, Stream input, string fileName)
    {
        var blobId = await blobRepo.UploadAsync(TriageContainer, userId, fileName, input);
        mqProducer.Produce(new(userId, fileName, blobId));
        return blobId;
    }

    /// <summary>
    /// Lists converted documents for the specified user.
    /// </summary>
    /// <param name="userId">The user id.</param>
    /// <returns>A list of documents.</returns>
    public async Task<List<BlobListing>> ListConverted(string userId)
        => await blobRepo.ListAsync(ConvertedContainer, userId);

    /// <summary>
    /// Downloads a blob for the specified user.
    /// </summary>
    /// <param name="userId">The user id.</param>
    /// <param name="blobReference">The blob reference.</param>
    /// <returns>Blob meta.</returns>
    public async Task<BlobMeta> Download(string userId, Guid blobReference)
        => await blobRepo.DownloadAsync(ConvertedContainer, userId, blobReference);
}
