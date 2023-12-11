// <copyright file="WeatherForecast.cs" company="ne1410s">
// Copyright (c) ne1410s. All rights reserved.
// </copyright>

namespace ne14.portal.api.Features.Weather;

/// <summary>
/// A weather forecast.
/// </summary>
public record WeatherForecast
{
    /// <summary>
    /// Gets or sets the date.
    /// </summary>
    public DateOnly Date { get; set; }

    /// <summary>
    /// Gets or sets the temperature in degrees c.
    /// </summary>
    public int TemperatureC { get; set; }

    /// <summary>
    /// Gets the temperature in degrees f.
    /// </summary>
    public int TemperatureF => 32 + (int)(this.TemperatureC / 0.5556);

    /// <summary>
    /// Gets or sets the summary.
    /// </summary>
    public string? Summary { get; set; }
}
