// <copyright file="PdfDomainService.cs" company="ne1410s">
// Copyright (c) ne1410s. All rights reserved.
// </copyright>

namespace ne14.portal.business;

using EnterpriseStartup.Blobs.Abstractions;
using EnterpriseStartup.SignalR;
using EnterpriseStartup.Utils.Pagination;
using FluentErrors.Extensions;

/// <summary>
/// Domain service for managing pdfs.
/// </summary>
public class PdfDomainService(
    IUserBlobRepository blobRepo,
    INotifier notifier,
    PdfConversionRequiredProducer mqProducer)
{
    private const string TriageContainer = "triage";
    private const string ConvertedContainer = "converted";

    /// <summary>
    /// Processes document uploads by uploading to a triage account and
    /// notifying the system of the event. The process is automated thereafter.
    /// </summary>
    /// <param name="userId">The user id.</param>
    /// <param name="blob">The blob data.</param>
    /// <returns>The temporary triage id.</returns>
    public async Task<Guid> UploadToTriage(string userId, BlobData blob)
    {
        var blobId = await blobRepo.UploadAsync(TriageContainer, userId, blob, true);
        var fileName = blob.MustExist().MetaData.FileName;
        mqProducer.Produce(new(userId, fileName, blobId));

        var payload = new { FileName = fileName, InboundBlobReference = blobId };
        const string text = "The file has been sent for conversion.";
        var notice = new Notice(NoticeLevel.Neutral, "Uploading File...", text, payload);
        await notifier.Notify(userId, notice);

        return blobId;
    }

    /// <summary>
    /// Lists converted documents for the specified user.
    /// </summary>
    /// <param name="userId">The user id.</param>
    /// <param name="paging">The paging request.</param>
    /// <returns>A list of documents.</returns>
    public async Task<LazyPageResult<BlobMetaData>> ListConverted(string userId, PageRequest paging)
        => await blobRepo.ListAsync(ConvertedContainer, userId, paging, false);

    /// <summary>
    /// Downloads a blob for the specified user.
    /// </summary>
    /// <param name="userId">The user id.</param>
    /// <param name="blobReference">The blob reference.</param>
    /// <returns>Blob meta.</returns>
    public async Task<BlobData> Download(string userId, Guid blobReference)
        => await blobRepo.DownloadAsync(ConvertedContainer, userId, blobReference, false);

    /// <summary>
    /// Deletes a blob for the specified user.
    /// </summary>
    /// <param name="userId">The user id.</param>
    /// <param name="blobReference">The blob reference.</param>
    /// <returns>Async task.</returns>
    public async Task Delete(string userId, Guid blobReference)
        => await blobRepo.DeleteAsync(ConvertedContainer, userId, blobReference, false);
}
