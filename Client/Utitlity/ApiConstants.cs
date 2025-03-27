namespace MES.Client.Utitlity
{
    public class ApiConstants
    {
       private const string BaseUrl = "https://localhost:7172/api";
        // private const string BaseUrl = "http://maag-image-capture.dover-global.net/api";

        public const string GetUserRoles = $"{BaseUrl}/Users/getuserroles";
        public const string GetRole = $"{BaseUrl}/Role/getrole";


        // Manage User
       // public const string AddUser = $"{BaseUrl}/LoginDetails/add";
        public const string GetUser = $"{BaseUrl}/Users/getuser";
        public const string AppLogin = $"{BaseUrl}/Account/login";
        public const string UpdateUser = $"{BaseUrl}/Users/Ur";
        public const string SaveUser = $"{BaseUrl}/Users/Ur";
        public const string DeleteUserByUsername = $"{BaseUrl}/Users";
        

        public const string LDAPLogin = $"{BaseUrl}/Account/login-ldap";
        public const string LoginUserDetails = $"{BaseUrl}/users/adduser";
        public const string LoginUserDelete = $"{BaseUrl}/users";
        public const string AddUser = $"{BaseUrl}/users/adduser";


       
       
       

    }
}
