namespace MES.Client.Utitlity
{
    public class ApiConstants
    {
       private const string BaseUrl = "https://localhost:7172/api";
      //  private const string BaseUrl = "https://maag-mes.dover-global.net/api";

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

        // Manaage RotorsStyle 
        public const string GetRotorsStyle = $"{BaseUrl}/RotorsStyle/getwc";
        public const string LocationRotorsStyle = $"{BaseUrl}/RotorsStyle/wcQR";
        public const string AddRotorsStyle = $"{BaseUrl}/RotorsStyle/addwc";
        public const string EditRotorsStyle = $"{BaseUrl}/RotorsStyle/editwc";
        public const string DeleteRotorsStyle = $"{BaseUrl}/RotorsStyle";

        // Manage SaddlePartNumber 
        public const string GetSPN = $"{BaseUrl}/SaddlePartNumber/getwc";
        public const string LocationSPN = $"{BaseUrl}/SaddlePartNumber/wcQR";
        public const string AddSPN = $"{BaseUrl}/SaddlePartNumber/addwc";
        public const string EditSPN = $"{BaseUrl}/SaddlePartNumber/editwc";
        public const string DeleteSPN = $"{BaseUrl}/SaddlePartNumber";

        // Manage NewBoxRequiredNumber 
        public const string GetNewBoxRequiredNumber = $"{BaseUrl}/NewBoxRequiredNumber/getwc";
        public const string LocationNewBoxRequiredNumber = $"{BaseUrl}/NewBoxRequiredNumber/wcQR";
        public const string AddNewBoxRequiredNumber = $"{BaseUrl}/NewBoxRequiredNumber/addwc";
        public const string EditNewBoxRequiredNumber = $"{BaseUrl}/NewBoxRequiredNumber/editwc";
        public const string DeleteNewBoxRequiredNumber = $"{BaseUrl}/NewBoxRequiredNumber";

        // Manage Other
        public const string GetOther = $"{BaseUrl}/Other/getwc";
        public const string LocationOther = $"{BaseUrl}/Other/wcQR";
        public const string AddOther = $"{BaseUrl}/Other/addwc";
        public const string EditOther = $"{BaseUrl}/Other/editwc";
        public const string DeleteOther = $"{BaseUrl}/Other";

        // Manage Material
        public const string GetMaterial = $"{BaseUrl}/Material/getwc";
        public const string LocationMaterial = $"{BaseUrl}/Material/wcQR";
        public const string AddMaterial = $"{BaseUrl}/Material/addwc";
        public const string EditMaterial = $"{BaseUrl}/Material/editwc";
        public const string DeleteMaterial = $"{BaseUrl}/Material";

        // Manage Type 
        public const string GetType = $"{BaseUrl}/TypeDetails/getwc";
        public const string LocationType = $"{BaseUrl}/TypeDetails/wcQR";
        public const string AddType = $"{BaseUrl}/TypeDetails/addwc";
        public const string EditType = $"{BaseUrl}/TypeDetails/editwc";
        public const string DeleteType = $"{BaseUrl}/TypeDetails";


        //Receiving   
        public const string GetReceiving = $"{BaseUrl}/ReceivingData/getrd";
        public const string DeleteReceiving = $"{BaseUrl}/ReceivingData";
        public const string AddQRCode = $"{BaseUrl}/QRCode/locQR";
        public const string AddReceiving = $"{BaseUrl}/ReceivingData/addrd";
        public const string AddprintQRCode = $"{BaseUrl}/QRCode/IncomlocQR";

        //RotorsIncoming    
        public const string GetRotorIncoming = $"{BaseUrl}/IncomingInspection/getIncomingData";
        public const string AddRotorIncoming = $"{BaseUrl}/IncomingInspection/addincoming";
        public const string AddImagesIncoming = $"{BaseUrl}/IncomingImages/addbi";
        public const string DeleteImagesIncoming = $"{BaseUrl}/IncomingImages/Delete";

        public const string CheckSerialNumber = $"{BaseUrl}/IncomingInspection/CheckSerialExists";
        public const string GetImagesbySerialNumber = $"{BaseUrl}/IncomingImages/getImagestodisplayinsales";
        public const string GetsalesEmails = $"{BaseUrl}/Users/getuserlist";


        // FinalInspection Save data  
        public const string AddFinalIISavedData = $"{BaseUrl}/RotorsFinalInspectionsave/AddFinalSaveData";
        public const string GetAllFIsavedData = $"{BaseUrl}/RotorsFinalInspectionsave/GetRecentIncomingData";
        public const string UpdateRotorFISavedData = $"{BaseUrl}/RotorsFinalInspectionsave/UpdateRotorIISavedData";
        public const string CheckIfFinalExists = $"{BaseUrl}/RotorsFinalInspectionsave/CheckIfRotorExists";
        public const string GetIncomingImageById= $"{BaseUrl}/RotorsFinalInspectionsave/IncomingImages";
        public const string UpdateIncomingImage = $"{BaseUrl}/IncomingImages/UpdateRotorIISavedData";


        //Incoming Feed Rolls Data 

        public const string AddFeedrollsIncoming = $"{BaseUrl}/IncomingFeedrolls/adddata";

        // RotorInspection Save data  
        public const string AddRotorIISavedData = $"{BaseUrl}/RotorIncomingInspectionSaveData/AddIncomingSaveData";
        public const string GetAllIIsavedData = $"{BaseUrl}/RotorIncomingInspectionSaveData/GetRecentIncomingData";
        public const string UpdateRotorIISavedData = $"{BaseUrl}/RotorIncomingInspectionSaveData/UpdateRotorIISavedData";
        public const string CheckIfRotorExists = $"{BaseUrl}/RotorIncomingInspectionSaveData/CheckIfRotorExists";


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
        public const string UpdateRotorCategorizationType = $"{BaseUrl}/RotorProductionSaveData/updateRotorCategorizationType";
        public const string GetAllProductionSavesData = $"{BaseUrl}/RotorProductionSaveData/GetAllProductionData";

        // new rotor data
        public const string AddNewRotorData = $"{BaseUrl}/NewRotorDetails/SaveNewRotor";
        public const string GetLatestSerialNumber = $"{BaseUrl}/NewRotorDetails/GetLatestSerialNumber";
        public const string GetAllNewRotorData = $"{BaseUrl}/NewRotorDetails/GetAllNewRotorData";
        public const string UpdateRIsdelete = $"{BaseUrl}/NewRotorDetails/updateRIsdelete";
        public const string UpNewISDeleteStatus = $"{BaseUrl}/NewRotorDetails/UpNewISDelete";

        public const string EditRiceivedata = $"{BaseUrl}/NewRotorDetails/editreceiveData";
        public const string EditNewReciveRotorData = $"{BaseUrl}/NewRotorDetails/editnewrotorreceiveData";


        // Rotor Grinding Data
        public const string AddRotorGrindingData = $"{BaseUrl}/RotorGrinding/AddGrindingData";
        public const string GetAllGrindingData = $"{BaseUrl}/RotorGrinding/GetAllGrindingData";

        // Rotor Grinding Saved  Data
        public const string AddRotorGrindingSavedData = $"{BaseUrl}/RotorGrindingSaved/AddGrindingSaveData";
        public const string GetRotorGrindingSavedDatabyserialnumber = $"{BaseUrl}/RotorGrindingSaved/GGSDBSN";
        public const string GetAllSavedGrindingData = $"{BaseUrl}/RotorGrindingSaved/GetAllSavedGrindingData";
        public const string UpdateRotorGrindingSavedData = $"{BaseUrl}/RotorGrindingSaved/UpdateGrindingSaveData";


        // Moving to outside operation Rotor Grinding Data
        public const string AddMovedOOPRotorGrindingData = $"{BaseUrl}/RotorDamageGrindingDataFromGrinding/AddMovedOOPGD";
        public const string GetAllMovedOOPGrindingData = $"{BaseUrl}/RotorDamageGrindingDataFromGrinding/GetAllMovedOOPGrindingData";
        public const string GetAllSaveGrindingData = $"{BaseUrl}/RotorDamageGrindingSaved/GetAllGrindingData";

        // Rotor Damage Grinding savedData
        public const string SaveDamageGrindingsavedData = $"{BaseUrl}/RotorDamageGrindingSaved/AddDamageGSDDSaveData";
        public const string GetSavedDamageGrindingData = $"{BaseUrl}/RotorDamageGrindingSaved/GetRecentDDGSaveData";


        // Rotor Damage Grinding Submited Data
        public const string AddDDGSubmitedData = $"{BaseUrl}/RotorDamageGrindingSubmited/AddDDGPSumbitedData";
        public const string GetAllDDGSubmitedData = $"{BaseUrl}/RotorDamageGrindingSubmited/GetAllDDGSubmitedData";

        // Rotor Final Inspection
        public const string GetDataFromGrinding = $"{BaseUrl}/RotorGrinding";
        public const string CheckSerialNumbersales = $"{BaseUrl}/RotorsFinalInspection/CheckSerialExists";
        public const string CheckGrindingSerial = $"{BaseUrl}/RotorGrinding/CheckSerialExists";
        public const string GetAllInspectionData = $"{BaseUrl}/IncomingInspection/GetAll";
        public const string AddFinalInspectionData = $"{BaseUrl}/RotorsFinalInspection/AddFinalInspection";
        public const string AddImagesFinal = $"{BaseUrl}/FinalInspectionImages/addbi";
        public const string UpdateImagesFinal = $"{BaseUrl}/FinalInspectionImages/Update";

        public const string GetImagesFinal = $"{BaseUrl}/FinalInspectionImages/getImages";
        public const string GetAllFinalInspectionData = $"{BaseUrl}/RotorsFinalInspection/GetAllFIData";
        public const string GetIncomingInspectionBySerial = $"{BaseUrl}/IncomingInspection/serialnumber";


        public const string GetFinalImagesbySerialNumber = $"{BaseUrl}/FinalInspectionImages/getImages";

        // Sales AttachedFile 
        public const string AddSalesAttachedFile = $"{BaseUrl}/SalesAttachedFile/addpi";
        public const string GetSalesAttachedFileBySerialNumber = $"{BaseUrl}/SalesAttachedFile/GFile";


        // Rotor Secondary WorkCenters Submited Data
        public const string AddSWSubmitedData = $"{BaseUrl}/SecondaryWorkCenters/AddSWData";
        public const string GetAllSWSubmitedData = $"{BaseUrl}/SecondaryWorkCenters/GetAllSWData";
        public const string GetSWSubmitedDatabyserialno = $"{BaseUrl}/SecondaryWorkCenters/GSW";

        // RotorSales Cleareance 
        public const string AddRotorSalesClearanceData = $"{BaseUrl}/RotorSalesClearance/AddRotorSalesClearance";
        public const string GetAllSalesClearanceData = $"{BaseUrl}/RotorSalesClearance/GetAllSalesClearanceData";
        public const string GetSalesClearanceDataBySLno = $"{BaseUrl}/RotorSalesClearance/GetSCDatabySerialNo";


        //shipping page 
        public const string AddShippingImages = $"{BaseUrl}/ShipmentImage/addbi";
        public const string ShipData = $"{BaseUrl}/Shipping/Ship";
        public const string GetAllShippedData = $"{BaseUrl}/Shipping/GetAllShippedData";
        public const string GetShippedImagesbySerialNumber = $"{BaseUrl}/ShipmentImage/getImages";

        // SaveEnterd Rotor Grinding Details
        public const string AddSaveEnterdRotorGrindingDetails = $"{BaseUrl}/SaveEnterdRotorGrindingDetails/AddGrindingSaveData";
        public const string GetAllSavedEnterdRotorGrindingDetails = $"{BaseUrl}/SaveEnterdRotorGrindingDetails/GetAllSavedGrindingData";
        public const string GetSaveEnterdRotorGrindingDetailsbyserialnumber = $"{BaseUrl}/SaveEnterdRotorGrindingDetails/GGSDBSN";


        // Manaage Dealy Reason 
        public const string GetMESDealyReason = $"{BaseUrl}/MESDelayReason/getdr";
        public const string DealyReasonQR = $"{BaseUrl}/MESDelayReason/drQR";
        public const string AddDealyReason = $"{BaseUrl}/MESDelayReason/adddr";
        public const string EditDealyReason = $"{BaseUrl}/MESDelayReason/editdr";
        public const string DeleteDealyReason = $"{BaseUrl}/MESDelayReason";

        // Get report in excel
        public const string GetReportinexcel = $"{BaseUrl}/Report/downloadreport";


        // Feed Roles Sales Page
        public const string GetAllFeedRolesIncominginspectionData = $"{BaseUrl}/FeedRoleSalesSumited/GetAll";


        // recieved page get the data from incoming saved and submit table

        public const string GetRecentIncomingsavedDatabasedonserialnumber = $"{BaseUrl}/RotorIncomingInspectionSaveData/GetRecentIncomingsavedDatabasedonserialnumber";

        public const string GetRecentIncomingsabmitDatabasedonserialnumber = $"{BaseUrl}/RotorIncomingInspectionSaveData/GetRecentIncomingsabmitDatabasedonserialnumber";

    }
}
