// <copyright file="PdfController.cs" company="ne1410s">
// Copyright (c) ne1410s. All rights reserved.
// </copyright>

namespace ne14.portal.api.Features.Pdf;

using EnterpriseStartup.Auth;
using EnterpriseStartup.Blobs.Abstractions;
using EnterpriseStartup.Utils.Pagination;
using FluentErrors.Extensions;
using Microsoft.AspNetCore.Mvc;
using ne14.portal.business;

/// <summary>
/// Pdf controller.
/// </summary>
[ApiController]
[Route("[controller]")]
public class PdfController(PdfDomainService domainService) : ControllerBase
{
    /// <summary>
    /// Uploads a new file.
    /// </summary>
    /// <param name="file">The file.</param>
    /// <returns>A temporary reference.</returns>
    [HttpPost]
    public async Task<Guid> UploadAsync(IFormFile file)
    {
        file.MustExist();

        var user = this.User.ToEnterpriseUser();
        await using var str = file.OpenReadStream();
        var blobData = new BlobData(str, new(Guid.Empty, file.ContentType, file.FileName, file.Length));

        return await domainService.UploadToTriage(user.Id, blobData);
    }

    /// <summary>
    /// Gets a list of converted files.
    /// </summary>
    /// <param name="paging">Paging request.</param>
    /// <returns>A file list.</returns>
    [HttpGet]
    public async Task<LazyPageResult<BlobMetaData>> ListAsync([FromQuery]PageRequest paging)
    {
        var user = this.User.ToEnterpriseUser();
        return await domainService.ListConverted(user.Id, paging);
    }

    /// <summary>
    /// Downloads a file.
    /// </summary>
    /// <param name="blobReference">The blob references.</param>
    /// <returns>File result.</returns>
    [HttpGet("{blobReference}")]
    public async Task<IActionResult> DownloadAsync(Guid blobReference)
    {
        var user = this.User.ToEnterpriseUser();
        var blob = await domainService.Download(user.Id, blobReference);

        return this.File(blob.Content, blob.MetaData.ContentType, blob.MetaData.FileName);
    }

    /// <summary>
    /// Deletes a file.
    /// </summary>
    /// <param name="blobReference">The blob reference.</param>
    /// <returns>Async task.</returns>
    [HttpDelete("{blobReference}")]
    public async Task DeleteAsync(Guid blobReference)
    {
        var user = this.User.ToEnterpriseUser();
        await domainService.Delete(user.Id, blobReference);
    }
}
