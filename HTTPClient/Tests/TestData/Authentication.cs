using HTTPClient.DataModels;

namespace HTTPClient.Tests.TestData
{
    public class Authentication
    {
        public static UserTokenModel userTokenDetails()
        {
            return new UserTokenModel
            {
                Username = "admin",
                Password = "password123"
            };
        }
    }
}