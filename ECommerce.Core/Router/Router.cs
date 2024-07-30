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

