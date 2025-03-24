namespace MES.Client.Utitlity
{
    public class ApiConstants
    {
       private const string BaseUrl = "https://localhost:7172/api";
       // private const string BaseUrl = "http://maag-image-capture.dover-global.net/api";
      
        public const string GetUserRoles = "api/Users/getuserroles";
      
        public const string Adduser = $"{BaseUrl}/LoginDetails/add";
        public const string GetUser = $"{BaseUrl}/Users/getuser";
        public const string GetRole = "api/Role/getrole";
        public const string AppLogin = $"{BaseUrl}/Account/login";
        public const string UpdateUser = $"{BaseUrl}/Users/Ur";
     
        public const string LDAPLogin = $"{BaseUrl}/Account/login-ldap";
        public const string LoginUserDetails = $"{BaseUrl}/users/adduser";
        public const string LoginUserDelete = $"{BaseUrl}/users";
        public const string AddUser = $"{BaseUrl}/users/adduser";
      
    }
}
