using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace MauiApp1
{
    public class ApiService
    {
        private readonly HttpClient _httpClient;
        private const string BaseUrl = "https://localhost:7287/api/quiz";

        public ApiService()
        {
            _httpClient = new HttpClient();
        }

        public async Task<List<Question>> GetQuestionsAsync()
        {
            try
            {
                return await _httpClient.GetFromJsonAsync<List<Question>>($"{BaseUrl}/questions");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Błąd podczas pobierania danych: {ex.Message}");
                return new List<Question>();
            }
        }
    }
}