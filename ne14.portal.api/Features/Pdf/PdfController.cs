// <copyright file="PdfController.cs" company="ne1410s">
// Copyright (c) ne1410s. All rights reserved.
// </copyright>

namespace ne14.portal.api.Features.Pdf;

using EnterpriseStartup.Auth;
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

        return await domainService.UploadToTriage(user.Id, str, file.FileName);
    }
}
