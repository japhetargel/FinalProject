using HTTPClient.DataModels;

namespace HTTPClient.Tests.TestData
{
    public class GenerateBooking
    {
        public static BookingDetailsModel bookingDetails()
        {
            DateTime dt = DateTime.UtcNow.Date;

            Bookingdates bookingDates = new Bookingdates();
            bookingDates.CheckIn = dt;
            bookingDates.CheckOut = dt.AddDays(1);

            return new BookingDetailsModel
            {
                FirstName = "Mang",
                LastName = "Kepweng",
                TotalPrice = 100,
                DepositPaid = true,
                BookingDates = bookingDates,
                AdditionalNeeds = "Allowance"
            };
        }
    }
}