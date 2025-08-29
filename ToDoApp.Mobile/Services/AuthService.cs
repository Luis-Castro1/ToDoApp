using System.Net.Http.Json;
using ToDoApp.Mobile.Core.Interfaces.Services;
using ToDoApp.Mobile.Core.Dtos;

namespace ToDoApp.Mobile.Services
{
    public class AuthService : IAuthService
    {
        private readonly HttpClient _httpClient;

        public AuthService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<AuthResponseDto> LoginAsync(string user, string password)
        {
            var loginDto = new LoginDto(user, password);


            var response = await _httpClient.PostAsJsonAsync("Auth/login", loginDto);

            if (!response.IsSuccessStatusCode)
            {
                var errorResponse = await response.Content.ReadAsStringAsync();
                throw new Exception($"Login failed: {errorResponse}");
            }
                return await response.Content.ReadFromJsonAsync<AuthResponseDto>();
        }

        public Task LogoutAsync()
        {
            throw new NotImplementedException();
        }
    }
}
