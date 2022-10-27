using HTTPClient.DataModels;
using HTTPClient.Helpers;
using HTTPClient.Tests.TestData;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using System.Net;

[assembly: Parallelize(Workers = 10, Scope = ExecutionScope.MethodLevel)]
namespace HTTPClient.Tests
{
    [TestClass]
    public class BookingTests
    {
        private BookingHelpers _bookingHelpers;

        [TestMethod]
        public async Task CreateBooking()
        {
            _bookingHelpers = new BookingHelpers();

            #region create data
            var addBooking = await _bookingHelpers.AddNewBooking();
            var getResponse = JsonConvert.DeserializeObject<BookingModel>(addBooking.Content.ReadAsStringAsync().Result);

            Assert.AreEqual(addBooking.StatusCode, HttpStatusCode.OK);
            #endregion

            #region get created data
            var getCreatedBooking = await _bookingHelpers.GetBookingById(getResponse.BookingId);
            var getCreatedBookingResponse = JsonConvert.DeserializeObject<BookingDetailsModel>(getCreatedBooking.Content.ReadAsStringAsync().Result);
            #endregion

            #region assert created data
            var expectedData = GenerateBooking.bookingDetails();
            Assert.AreEqual(expectedData.FirstName, getCreatedBookingResponse.FirstName);
            Assert.AreEqual(expectedData.LastName, getCreatedBookingResponse.LastName);
            Assert.AreEqual(expectedData.TotalPrice, getCreatedBookingResponse.TotalPrice);
            Assert.AreEqual(expectedData.DepositPaid, getCreatedBookingResponse.DepositPaid);
            Assert.AreEqual(expectedData.BookingDates.CheckIn, getCreatedBookingResponse.BookingDates.CheckIn);
            Assert.AreEqual(expectedData.BookingDates.CheckOut, getCreatedBookingResponse.BookingDates.CheckOut);
            Assert.AreEqual(expectedData.AdditionalNeeds, getCreatedBookingResponse.AdditionalNeeds);
            #endregion

            #region clean test data
            await _bookingHelpers.DeleteBookingById(getResponse.BookingId);
            #endregion
        }

        [TestMethod]
        public async Task UpdateBooking()
        {
            _bookingHelpers = new BookingHelpers();

            #region create data
            var addBooking = await _bookingHelpers.AddNewBooking();
            var getResponse = JsonConvert.DeserializeObject<BookingModel>(addBooking.Content.ReadAsStringAsync().Result);

            Assert.AreEqual(addBooking.StatusCode, HttpStatusCode.OK);
            #endregion

            #region get created data
            var getCreatedBooking = await _bookingHelpers.GetBookingById(getResponse.BookingId);
            var getCreatedBookingResponse = JsonConvert.DeserializeObject<BookingDetailsModel>(getCreatedBooking.Content.ReadAsStringAsync().Result);
            #endregion

            #region update data
            var updatedData = new BookingDetailsModel()
            {
                FirstName = "Jon",
                LastName = "Snow",
                TotalPrice = getCreatedBookingResponse.TotalPrice,
                DepositPaid = getCreatedBookingResponse.DepositPaid,
                BookingDates = getCreatedBookingResponse.BookingDates,
                AdditionalNeeds = getCreatedBookingResponse.AdditionalNeeds
            };
            var updateBooking = await _bookingHelpers.UpdateBookingById(updatedData, getResponse.BookingId);
            var getUpdateBookingResponse = JsonConvert.DeserializeObject<BookingDetailsModel>(updateBooking.Content.ReadAsStringAsync().Result);

            Assert.AreEqual(updateBooking.StatusCode, HttpStatusCode.OK);
            #endregion

            #region get updated data
            var getUpdatedBooking = await _bookingHelpers.GetBookingById(getResponse.BookingId);
            var getUpdatedBookingResponse = JsonConvert.DeserializeObject<BookingDetailsModel>(getUpdatedBooking.Content.ReadAsStringAsync().Result);
            #endregion

            #region assert updated data
            Assert.AreEqual(updatedData.FirstName, getUpdatedBookingResponse.FirstName);
            Assert.AreEqual(updatedData.LastName, getUpdatedBookingResponse.LastName);
            Assert.AreEqual(updatedData.TotalPrice, getUpdatedBookingResponse.TotalPrice);
            Assert.AreEqual(updatedData.DepositPaid, getUpdatedBookingResponse.DepositPaid);
            Assert.AreEqual(updatedData.BookingDates.CheckIn, getUpdatedBookingResponse.BookingDates.CheckIn);
            Assert.AreEqual(updatedData.BookingDates.CheckOut, getUpdatedBookingResponse.BookingDates.CheckOut);
            Assert.AreEqual(updatedData.AdditionalNeeds, getUpdatedBookingResponse.AdditionalNeeds);
            #endregion

            #region clean test data
            await _bookingHelpers.DeleteBookingById(getResponse.BookingId);
            #endregion
        }

        [TestMethod]
        public async Task DeleteCreatedBooking()
        {
            _bookingHelpers = new BookingHelpers();

            #region create data
            var addBooking = await _bookingHelpers.AddNewBooking();
            var getResponse = JsonConvert.DeserializeObject<BookingModel>(addBooking.Content.ReadAsStringAsync().Result);

            Assert.AreEqual(addBooking.StatusCode, HttpStatusCode.OK);
            #endregion

            #region delete test data
            var deleteBooking = await _bookingHelpers.DeleteBookingById(getResponse.BookingId);
            #endregion

            #region assert if data was successfully deleted
            Assert.AreEqual(deleteBooking.StatusCode, HttpStatusCode.Created);
            #endregion
        }

        [TestMethod]
        public async Task GetInvalidBookingId()
        {
            _bookingHelpers = new BookingHelpers();
            Random random = new Random();
            int randomNumber = random.Next(9000000, 999999999);

            #region get created data
            var getCreatedBooking = await _bookingHelpers.GetBookingById(randomNumber);
            #endregion

            #region assert invalid data
            Assert.AreEqual(getCreatedBooking.StatusCode, HttpStatusCode.NotFound);
            #endregion
        }
    }
}