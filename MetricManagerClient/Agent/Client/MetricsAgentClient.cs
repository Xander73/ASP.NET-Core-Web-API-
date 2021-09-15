using MetricsManagerClient.Client.Interfaces;
using System;
using System.Net.Http;
using Microsoft.Extensions.Logging;
using System.Text.Json;
using MetricsManagerClient.Responses;
using MetricsManagerClient.Requests;

namespace MetricsManagerClient.Client
{
    public class MetricsAgentClient : IMetricsAgentClient
    {
        private readonly HttpClient _httpClient;
        JsonSerializerOptions options;

        public MetricsAgentClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
            options = new JsonSerializerOptions {PropertyNameCaseInsensitive = true};
        }


        public AllCpuMetricsApiResponse GetAllCpuMetrics(GetAllCpuMetricsApiRequest request)
        {
            var fromTime = request.FromTime.TotalSeconds + 1; //+1 - чтобы не дублировалась последняя строка таблицы
            var toTime = request.ToTime.TotalSeconds;  
            //Привожу значения к строке, т.к. в запросе не передаются числа более семи знаков
            var httpRequest = new HttpRequestMessage(
                HttpMethod.Get,
                $"{request.ClientBaseAddres}/api/metrics/cpu/from/{fromTime.ToString()}/to/{toTime.ToString()}" 
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
                //_logger.LogError(ex.Message);
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
                $"{request.ClientBaseAddres}/api/metrics/dotnet/from/{fromTime.ToString()}/to/{toTime.ToString()}" 
                );
            try
            {
                HttpResponseMessage response = _httpClient.SendAsync(httpRequest).Result;
                using var responseStream = response.Content.ReadAsStreamAsync().Result;
                
                return JsonSerializer.DeserializeAsync<AllDotNetMetricsApiResponse>(responseStream, options).Result;
            }
            catch (Exception ex)
            {
                //_logger.LogError(ex.Message);
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
                $"{request.ClientBaseAddres}/api/metrics/hdd/from/{fromTime.ToString()}/to/{toTime.ToString()}" 
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
                //_logger.LogError(ex.Message);
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
                $"{request.ClientBaseAddres}/api/metrics/network/from/{fromTime.ToString()}/to/{toTime.ToString()}" 
                );

            try
            {
                HttpResponseMessage response = _httpClient.SendAsync(httpRequest).Result;
                using var responseStream = response.Content.ReadAsStreamAsync().Result;
                return JsonSerializer.DeserializeAsync<AllNetworkMetricsApiResponse>(responseStream, options).Result;
            }
            catch (Exception ex)
            {
                //_logger.LogError(ex.Message);
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
                $"{request.ClientBaseAddres}/api/metrics/ram/from/{fromTime.ToString()}/to/{toTime.ToString()}" 
                );

            try
            {
                HttpResponseMessage response = _httpClient.SendAsync(httpRequest).Result;
                using var responseStream = response.Content.ReadAsStreamAsync().Result;
                return JsonSerializer.DeserializeAsync<AllRamMetricsApiResponse>(responseStream, options).Result;
            }
            catch (Exception ex)
            {
                //_logger.LogError(ex.Message);
            }
            return null;
        }
    }
}
