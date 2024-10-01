// <copyright file="BlobListing.cs" company="ne1410s">
// Copyright (c) ne1410s. All rights reserved.
// </copyright>

namespace ne14.portal.business.Features.Blob;

using System;

/// <summary>
/// A blob listing, e.g. for search results.
/// </summary>
/// <param name="BlobReference">The unique blob reference.</param>
/// <param name="FileName">The file name as originally supplied.</param>
/// <param name="FileSize">The size of the file, in bytes.</param>
public record BlobListing(Guid BlobReference, string FileName, long FileSize);
