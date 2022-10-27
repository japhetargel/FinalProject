using HTTPClient.DataModels;
using HTTPClient.Resources;
using HTTPClient.Tests.TestData;
using Newtonsoft.Json;
using System.Text;

namespace HTTPClient.Helpers
{
    public class BookingHelpers
    {
        private HttpClient _httpClient;
        public async Task<HttpResponseMessage> AddNewBooking()
        {
            _httpClient = new HttpClient();
            _httpClient.DefaultRequestHeaders.Add("Accept", "application/json");
            var request = JsonConvert.SerializeObject(GenerateBooking.bookingDetails());
            var postRequest = new StringContent(request, Encoding.UTF8, "application/json");

            return await _httpClient.PostAsync(Endpoints.GetURL(Endpoints.UserEndpoint), postRequest);
        }
        public async Task<HttpResponseMessage> GetBookingById(int bookingId)
        {
            _httpClient = new HttpClient();
            _httpClient.DefaultRequestHeaders.Add("Accept", "application/json");

            return await _httpClient.GetAsync(Endpoints.GetUri(Endpoints.UserEndpoint) + "/" + bookingId);
        }
        public async Task<HttpResponseMessage> DeleteBookingById(int bookingId)
        {
            var token = await GetAuthToken();
            _httpClient = new HttpClient();
            _httpClient.DefaultRequestHeaders.Add("Accept", "application/json");
            _httpClient.DefaultRequestHeaders.Add("Cookie", "token=" + token);

            return await _httpClient.DeleteAsync(Endpoints.GetUri(Endpoints.UserEndpoint) + "/" + bookingId);
        }
        public async Task<HttpResponseMessage> UpdateBookingById(BookingDetailsModel booking, int bookingId)
        {
            var token = await GetAuthToken();
            _httpClient = new HttpClient();
            _httpClient.DefaultRequestHeaders.Add("Accept", "application/json");
            _httpClient.DefaultRequestHeaders.Add("Cookie", "token=" + token);
            var request = JsonConvert.SerializeObject(booking);
            var putRequest = new StringContent(request, Encoding.UTF8, "application/json");

            return await _httpClient.PutAsync(Endpoints.GetURL(Endpoints.UserEndpoint + "/" + bookingId), putRequest);
        }
        private async Task<string> GetAuthToken()
        {
            _httpClient = new HttpClient();
            _httpClient.DefaultRequestHeaders.Add("Accept", "application/json");
            var request = JsonConvert.SerializeObject(Authentication.userTokenDetails());
            var postRequest = new StringContent(request, Encoding.UTF8, "application/json");
            var httpResponse = await _httpClient.PostAsync(Endpoints.GetURL(Endpoints.AuthEndpoint), postRequest);
            var token = JsonConvert.DeserializeObject<TokenModel>(httpResponse.Content.ReadAsStringAsync().Result);

            return token.Token;
        }
    }
}