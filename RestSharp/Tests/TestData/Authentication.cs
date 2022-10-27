using RestSharpTest.DataModels;

namespace RestSharpTest.Tests.TestData
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
