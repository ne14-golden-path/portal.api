// <copyright file="PdfDomainService.cs" company="ne1410s">
// Copyright (c) ne1410s. All rights reserved.
// </copyright>

namespace ne14.portal.business;

/// <summary>
/// Domain service for document upload.
/// </summary>
public class PdfDomainService(
    IBlobRepository blobRepo,
    PdfConversionRequiredProducer mqProducer)
{
    private const string TriageContainer = "triage";

    /// <summary>
    /// Processes document uploads by uploading to a triage account and
    /// notifying the system of the event. The process is automated thereafter.
    /// </summary>
    /// <param name="input">The input.</param>
    /// <param name="fileName">The file name.</param>
    /// <returns>The temporary triage id.</returns>
    public async Task<Guid> UploadToTriage(Stream input, string fileName)
    {
        var guid = await blobRepo.UploadAsync(TriageContainer, fileName, input);
        mqProducer.Produce(new(guid));
        return guid;
    }
}
