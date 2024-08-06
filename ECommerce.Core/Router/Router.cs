namespace ECommerce.Core.Router;

public static class Router
{
    #region Const Params
    public const string root = "api";
    public const string version = "v1";
    public const string Rule = root + "/" + version + "/";
    public const string SingleRoute = "{id:int}";
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
    public static class EagerProducts
    {
        public const string Prefix = Rule + "EagerProduct/";

        public const string ListProduct = Prefix + "ListProduct";
        public const string ProductTypes = Prefix + "ProductTypes";
        public const string ProductBrands = Prefix + "ProductBrands";
        public const string GetById = Prefix + "GetByID" + "/" + SingleRoute;
        public const string Create = Prefix + "Create";
        public const string Edit = Prefix + "Edit";
        public const string Delete = Prefix + "Delete" + "/" + SingleRoute;

    }

    #endregion

    #region Category
    public static class Category
    {
        public const string Prefix = Rule + "Category/";
        public const string ListCategory = Prefix + "ListCategory";
        public const string GetById = Prefix + "GetByID" + "/" + SingleRoute;
        public const string Create = Prefix + "Create";
        public const string Edit = Prefix + "Edit";
        public const string Delete = Prefix + "Delete" + "/" + SingleRoute;

    }
    #endregion

    #region Product Types
    public static class ProductTypes
    {
        public const string Prefix = Rule + "ProductType/";

        public const string ListProductTypes = Prefix + "ListProductTypes";
        public const string GetById = Prefix + "GetByID" + "/" + SingleRoute;
        public const string Create = Prefix + "Create";
        public const string Edit = Prefix + "Edit" + "/" + SingleRoute;
        public const string Delete = Prefix + "Delete" + "/" + SingleRoute;

    }

    #endregion

    #region Product Brands
    public static class ProductBrands
    {
        public const string Prefix = Rule + "ProductBrand/";

        public const string ListProductBrands = Prefix + "ListProductBrands";
        public const string GetById = Prefix + "GetByID" + "/" + SingleRoute;
        public const string Create = Prefix + "Create";
        public const string Edit = Prefix + "Edit" + "/" + SingleRoute;
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
        public const string ConfirmEmail = Prefix + "/confirmemail";
        public const string InitializationAddress = Prefix + "address";
        public const string UpdateAddress = Prefix + "UpdateAddress";
        public const string RefreshToken = Prefix + "/refreshtoken";

    }

    public static class Roles
    {
        public const string Prefix = Rule + "roles";

        public const string GetAll = Prefix;
        public const string GetById = Prefix + "/{id}";
        public const string Create = Prefix;
        public const string Update = Prefix + "/{id}";
        public const string Delete = Prefix + "/{id}";
        public const string Assign = Prefix + "/assign";
        public const string Remove = Prefix + "/remove";
    }

    #endregion

    #region Order
    public static class Order
    {
        public const string Prefix = Rule + "Order/";

        public const string Create = Prefix + "CreateOrder";
        public const string GetOrdersForUser = Prefix + "GetOrdersForUser";
        public const string GetOrderByIdForUser = Prefix + "GetOrderByIdForUser" + "/" + SingleRoute;
        public const string DeliveryMethod = Prefix + "DeliveryMethod";
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

