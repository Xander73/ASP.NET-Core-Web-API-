using MetricsManager.Client.Interfaces;
using MetricsManager.Requests;
using MetricsManager.Responses;
using System;
using System.Net.Http;
using Microsoft.Extensions.Logging;
using System.Text.Json;

namespace MetricsManager.Client
{
    public class MetricsAgentClient : IMetricsAgentClient
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger _logger;
        JsonSerializerOptions options;

        public MetricsAgentClient(HttpClient httpClient, ILogger logger)
        {
            _httpClient = httpClient;
            _logger = logger;
            options = new JsonSerializerOptions {PropertyNameCaseInsensitive = true};
        }


        public AllCpuMetricsApiResponse GetAllCpuMetrics(GetAllCpuMetricsApiRequest request)
        {
            var fromTime = request.FromTime.TotalSeconds + 1; //+1 - чтобы не дублировалась последняя строка таблицы
            var toTime = request.ToTime.TotalSeconds;  
            //Привожу значения к строке, т.к. в запросе не передаются числа более семи знаков
            var httpRequest = new HttpRequestMessage(
                HttpMethod.Get,
                $"{request.ClientBaseAddres}/api/cpumetrics/from/{fromTime.ToString()}/to/{toTime.ToString()}" 
                );
            try
            {
                HttpResponseMessage response = _httpClient.SendAsync(httpRequest).Result;
                using (var responseStream = response.Content.ReadAsStreamAsync().Result)
                {                    
                    var result = JsonSerializer.DeserializeAsync<AllCpuMetricsApiResponse>(responseStream, options).Result;
                    return result;
                }                
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }
            return null;
        }


        public AllDotNetMetricsApiResponse GetAllDotNetMetrics(GetAllDotNetMetricsApiRequest request)
        {
            var fromTime = request.FromTime.TotalSeconds + 1; //+1 - чтобы не дублировалась последняя строка таблицы
            var toTime = request.ToTime.TotalSeconds;
            //Привожу значения к строке, т.к. в запросе не передаются числа более семи знаков
            var httpRequest = new HttpRequestMessage(
                HttpMethod.Get,
                $"{request.ClientBaseAddres}/api/dotnetmetrics/from/{fromTime.ToString()}/to/{toTime.ToString()}" 
                );
            try
            {
                HttpResponseMessage response = _httpClient.SendAsync(httpRequest).Result;
                using var responseStream = response.Content.ReadAsStreamAsync().Result;
                
                return JsonSerializer.DeserializeAsync<AllDotNetMetricsApiResponse>(responseStream, options).Result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }
            return null;
        }


        public AllHddMetricsApiResponse GetAllHddMetrics(GetAllHddMetricsApiRequest request)
        {
            var fromTime = request.FromTime.TotalSeconds + 1; //+1 - чтобы не дублировалась последняя строка таблицы
            var toTime = request.ToTime.TotalSeconds;
            //Привожу значения к строке, т.к. в запросе не передаются числа более семи знаков
            var httpRequest = new HttpRequestMessage(
                HttpMethod.Get,
                $"{request.ClientBaseAddres}/api/hddmetrics/from/{fromTime.ToString()}/to/{toTime.ToString()}" 
                );

            try
            {
                HttpResponseMessage response = _httpClient.SendAsync(httpRequest).Result;
                using var responseStream = response.Content.ReadAsStreamAsync().Result;
                var result = JsonSerializer.DeserializeAsync<AllHddMetricsApiResponse>(responseStream, options).Result;
                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }
            return null;
        }


        public AllNetworkMetricsApiResponse GetAllNetworkMetrics(GetAllNetworkMetricsApiRequest request)
        {
            var fromTime = request.FromTime.TotalSeconds + 1; //+1 - чтобы не дублировалась последняя строка таблицы
            var toTime = request.ToTime.TotalSeconds;
            //Привожу значения к строке, т.к. в запросе не передаются числа более семи знаков
            var httpRequest = new HttpRequestMessage(
                HttpMethod.Get,
                $"{request.ClientBaseAddres}/api/networkmetrics/from/{fromTime.ToString()}/to/{toTime.ToString()}" 
                );

            try
            {
                HttpResponseMessage response = _httpClient.SendAsync(httpRequest).Result;
                using var responseStream = response.Content.ReadAsStreamAsync().Result;
                return JsonSerializer.DeserializeAsync<AllNetworkMetricsApiResponse>(responseStream, options).Result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }
            return null;
        }


        public AllRamMetricsApiResponse GetAllRamMetrics(GetAllRamMetricsApiRequest request)
        {
            var fromTime = request.FromTime.TotalSeconds + 1; //+1 - чтобы не дублировалась последняя строка таблицы
            var toTime = request.ToTime.TotalSeconds;
            //Привожу значения к строке, т.к. в запросе не передаются числа более семи знаков
            var httpRequest = new HttpRequestMessage(
                HttpMethod.Get,
                $"{request.ClientBaseAddres}/api/rammetrics/from/{fromTime.ToString()}/to/{toTime.ToString()}" 
                );

            try
            {
                HttpResponseMessage response = _httpClient.SendAsync(httpRequest).Result;
                using var responseStream = response.Content.ReadAsStreamAsync().Result;
                return JsonSerializer.DeserializeAsync<AllRamMetricsApiResponse>(responseStream, options).Result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }
            return null;
        }
    }
}
