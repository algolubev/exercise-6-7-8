// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Identity.Web;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Threading.Tasks;
using WebMvc.Models;

namespace TodoListClient.Services
{
    public static class TodoListServiceExtensions
    {
        public static void AddTodoListService(this IServiceCollection services, IConfiguration configuration)
        {
            // https://docs.microsoft.com/en-us/dotnet/standard/microservices-architecture/implement-resilient-applications/use-httpclientfactory-to-implement-resilient-http-requests
            services.AddHttpClient<IWeatherForecastService, WeatherForecastService>();
        }
    }

    public class WeatherForecastService : IWeatherForecastService
    {
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly HttpClient _httpClient;
        private readonly string _TodoListScope = string.Empty;
        private readonly string _TodoListBaseAddress = string.Empty;
        private readonly ITokenAcquisition _tokenAcquisition;

        public WeatherForecastService(ITokenAcquisition tokenAcquisition, HttpClient httpClient, IConfiguration configuration, IHttpContextAccessor contextAccessor)
        {
            _httpClient = httpClient;
            _tokenAcquisition = tokenAcquisition;
            _contextAccessor = contextAccessor;
            _TodoListScope = configuration["TodoList:TodoListScope"];
            _TodoListBaseAddress = configuration["TodoList:TodoListBaseAddress"];
        }

        public async Task<IEnumerable<WeatherForecast>> GetAsync()
        {
            await PrepareAuthenticatedClient();
            var weatherForecasts = await _httpClient.GetFromJsonAsync<List<WeatherForecast>>($"{ _TodoListBaseAddress}/weatherforecast");
            return weatherForecasts;
        }

        private async Task PrepareAuthenticatedClient()
        {
            var accessToken = await _tokenAcquisition.GetAccessTokenForUserAsync(new[] { _TodoListScope });
            Debug.WriteLine($"access token-{accessToken}");
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
            _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

    }
}