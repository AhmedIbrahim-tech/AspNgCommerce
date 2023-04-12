namespace ECommerce.API.Contract;

public class ApiRoute
{
    public const string Version = "v1";

    public static class Account
    {
        public const string Login = $"api/{Version}/Account/Login";
    }
}
