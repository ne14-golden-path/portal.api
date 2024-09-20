// <copyright file="WeatherForecastController.cs" company="ne1410s">
// Copyright (c) ne1410s. All rights reserved.
// </copyright>

namespace ne14.portal.api.Features.Weather;

using EnterpriseStartup.Auth;
using EnterpriseStartup.SignalR;
using Microsoft.AspNetCore.Mvc;

/// <summary>
/// Weather forecast controller.
/// </summary>
[ApiController]
[Route("[controller]")]
public class WeatherForecastController(
    ILogger<WeatherForecastController> logger,
    INotifier notifier) : ControllerBase
{
    private static readonly string[] Summaries = ["Chilly", "Cool", "Mild", "Warm", "Balmy"];

    /// <summary>
    /// Gets a forecast.
    /// </summary>
    /// <returns>A list of forecasts.</returns>
    [HttpGet(Name = "GetWeatherForecast")]
    public IEnumerable<WeatherForecast> Get()
    {
        var user = this.User.ToEnterpriseUser();
        logger.LogInformation("User {UserId} is getting a forecast!", user.Id);

        notifier.Notify(user.Id, new(NoticeLevel.Success, "ok", "golden"));

        return Enumerable.Range(1, 5).Select(index => new WeatherForecast
        {
            Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
            TemperatureC = Random.Shared.Next(-20, 55),
            Summary = Summaries[Random.Shared.Next(Summaries.Length)],
        })
        .ToArray();
    }
}
