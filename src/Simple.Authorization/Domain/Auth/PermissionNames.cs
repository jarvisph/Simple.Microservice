namespace Simple.Authorization.Domain.Auth
{
    public static class PermissionNames
    {

        public const string Authorization = "Authorization";

        public const string Authorization_Admin = Authorization + ".Admin";
        public const string Authorization_Admin_Create = Authorization_Admin + ".Create";
        public const string Authorization_Admin_Edit = Authorization_Admin + ".Edit";
        public const string Authorization_Admin_Delete = Authorization_Admin + ".Delete";

        public const string Authorization_Role = Authorization + ".Role";
        public const string Authorization_Role_Create = Authorization_Role + ".Create";
        public const string Authorization_Role_Edit = Authorization_Role + ".Edit";
        public const string Authorization_Role_Delete = Authorization_Role + ".Delete";

    }
}
