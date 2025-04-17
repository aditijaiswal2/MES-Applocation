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

        // Manaage WorkCenters : MES WorkCenter
        public const string GetMESWorkcenters = $"{BaseUrl}/MESWorkCenters/getwc";
        public const string LocationQR = $"{BaseUrl}/MESWorkCenters/wcQR";
        public const string AddLocation = $"{BaseUrl}/MESWorkCenters/addwc";
        public const string EditLocation = $"{BaseUrl}/MESWorkCenters/editwc";
        public const string DeleteLocation = $"{BaseUrl}/MESWorkCenters";

        //Receiving   
        public const string GetReceiving = $"{BaseUrl}/ReceivingData/getrd";
        public const string DeleteReceiving = $"{BaseUrl}/ReceivingData";
        public const string AddQRCode = $"{BaseUrl}/QRCode/locQR";
        public const string AddReceiving = $"{BaseUrl}/ReceivingData/addrd";
        public const string AddprintQRCode = $"{BaseUrl}/QRCode/IncomlocQR";

        //RotorsIncoming    
        public const string AddRotorIncoming = $"{BaseUrl}/IncomingInspection/addincoming";
        public const string AddImagesIncoming = $"{BaseUrl}/IncomingImages/addbi";
        public const string CheckSerialNumber = $"{BaseUrl}/IncomingInspection/CheckSerialExists";
        public const string GetImagesbySerialNumber = $"{BaseUrl}/IncomingImages/getImages";



        // RotorSales 

        public const string GetIncomingDataBySerialNumber = $"{BaseUrl}/RotorSales/GetBySerialNumber";
        public const string GetAllIncominginspectionData = $"{BaseUrl}/RotorSales/GetAll";
        public const string AddRotorSalesData = $"{BaseUrl}/RotorSales/AddSalesData";
        public const string GetAllSalesData = $"{BaseUrl}/RotorSales/GetAllSalesData";

        // RotorSales saved data
        public const string AddRotorSalesSavedData = $"{BaseUrl}/RotorSalesSaveData/AddSalesSaveData";
        public const string GetAllSalessavedData = $"{BaseUrl}/RotorSalesSaveData/GetRecentSalesData";

        // Rotor Production Data
        public const string AddRotorProductionData = $"{BaseUrl}/RotorProduction/AddProductionData";
        public const string GetAllProductionData = $"{BaseUrl}/RotorProduction/GetAllProductionData";

        // Rotor Production savedData
        public const string SaveproductionsavedData = $"{BaseUrl}/RotorProductionSaveData/AddProductionSaveData";
        public const string GetSavedproductionData = $"{BaseUrl}/RotorProductionSaveData/GetRecentProductionSaveData";

        // new rotor data
        public const string AddNewRotorData = $"{BaseUrl}/NewRotorDetails/SaveNewRotor";

        // Rotor Grinding Data
        public const string AddRotorGrindingData = $"{BaseUrl}/RotorGrinding/AddGrindingData";
        public const string GetAllGrindingData = $"{BaseUrl}/RotorGrinding/GetAllGrindingData";

        // Rotor Grinding Saved  Data
        public const string AddRotorGrindingSavedData = $"{BaseUrl}/RotorGrindingSaved/AddGrindingSaveData";
        public const string GetRotorGrindingSavedDatabyserialnumber = $"{BaseUrl}/RotorGrindingSaved/GGSDBSN";
        public const string GetAllSavedGrindingData = $"{BaseUrl}/RotorGrindingSaved/GetAllSavedGrindingData";

    }
}
