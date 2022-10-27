using RestSharpTest.DataModels;

namespace RestSharpTest.Tests.TestData
{
    public class GenerateBooking
    {
        public static BookingDetailsModel bookingDetails()
        {
            DateTime dt = DateTime.UtcNow.Date;

            Bookingdates bookingDates = new Bookingdates();
            bookingDates.Checkin = dt;
            bookingDates.Checkout = dt.AddDays(1);

            return new BookingDetailsModel
            {
                Firstname = "Mang",
                Lastname = "Kepweng",
                Totalprice = 100,
                Depositpaid = true,
                Bookingdates = bookingDates,
                Additionalneeds = "Allowance"
            };
        }
    }
}
