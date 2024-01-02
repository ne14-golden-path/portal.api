// <copyright file="StubBlobRepository.cs" company="ne1410s">
// Copyright (c) ne1410s. All rights reserved.
// </copyright>

namespace ne14.portal.business;

/// <inheritdoc cref="IBlobRepository"/>
public class StubBlobRepository : IBlobRepository
{
    /// <inheritdoc/>
    public Task<Guid> UploadAsync(string directory, Stream input)
    {
        return Task.FromResult(Guid.NewGuid());
    }
}
