namespace ECommerce.Core.Router;

public static class Router
{
    #region Const Params
    public const string root = "api";
    public const string version = "v1";
    public const string Rule = root + "/" + version + "/";
    public const string SingleRoute = "{id}";
    #endregion

    #region Product
    public static class SpecProduct
    {
        public const string Prefix = Rule + "SpecificationsProduct/";

        public const string ListProduct = Prefix + "ListProduct";
        public const string ProductTypes = Prefix + "ProductTypes";
        public const string ProductBrands = Prefix + "ProductBrands";
        public const string GetById = Prefix + "GetByID" + "/" + SingleRoute;
        public const string Create = Prefix + "Create";
        public const string Edit = Prefix + "Edit";
        public const string Delete = Prefix + "Delete" + "/" + SingleRoute;

    }
    #endregion


    #region Basket
    public static class Basket
    {
        public const string Prefix = Rule + "Basket/";

        public const string GetBasket = Prefix + "GetBasket";
        public const string UpdateBasket = Prefix + "UpdateBasket";
        public const string DeleteBasket = Prefix + "DeleteBasket";
    }
    #endregion

    #region Account
    public static class Account
    {
        public const string Prefix = Rule + "Account/";

        public const string Login = Prefix + "login";
        public const string Register = Prefix + "register";
        public const string CurrentUser = Prefix + "CurrentUser";
        public const string EmailExists = Prefix + "emailexists";
        public const string InitializationAddress = Prefix + "address";
        public const string UpdateAddress = Prefix + "UpdateAddress";
    }
    #endregion

    #region Error
    public static class Error
    {
        public const string Prefix = Rule + "Buggy/";

        public const string NotFound = Prefix + "NotFound";
        public const string ServerError = Prefix + "ServerError";
        public const string BadRequest = Prefix + "BadRequest";
        public const string GetBadRequestById = Prefix + "BadRequest" + "/" + SingleRoute;
    }
    #endregion
}

