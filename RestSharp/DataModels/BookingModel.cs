using Newtonsoft.Json;

namespace RestSharpTest.DataModels
{
    public class BookingModel
    {
        [JsonProperty("bookingid")]
        public int BookingId { get; set; }

        [JsonProperty("booking")]
        public BookingDetailsModel Booking { get; set; }
    }
}
