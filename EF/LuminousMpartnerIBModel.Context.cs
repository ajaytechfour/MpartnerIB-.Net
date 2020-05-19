﻿//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace LuminousMpartnerIB.EF
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.Data.Objects;
    using System.Data.Objects.DataClasses;
    using System.Linq;
    
    public partial class LuminousMpartnerIBEntities : DbContext
    {
        public LuminousMpartnerIBEntities()
            : base("name=LuminousMpartnerIBEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public DbSet<DealersMaster> DealersMasters { get; set; }
        public DbSet<DealersMaster_History> DealersMaster_History { get; set; }
        public DbSet<DistributorMaster> DistributorMasters { get; set; }
        public DbSet<DistributorMaster_History> DistributorMaster_History { get; set; }
        public DbSet<Mapping_Company_Distributor_Dealers> Mapping_Company_Distributor_Dealers { get; set; }
        public DbSet<MHrOtp> MHrOtps { get; set; }
        public DbSet<State> States { get; set; }
        public DbSet<sysdiagram> sysdiagrams { get; set; }
        public DbSet<AlertNotificationAccessTable> AlertNotificationAccessTables { get; set; }
        public DbSet<AlertNotificationReadStatu> AlertNotificationReadStatus { get; set; }
        public DbSet<AlertNotification> AlertNotifications { get; set; }
        public DbSet<AlertNotifications_History> AlertNotifications_History { get; set; }
        public DbSet<App_Log> App_Log { get; set; }
        public DbSet<AppVersion> AppVersions { get; set; }
        public DbSet<Auditlog_Userverificationstatus> Auditlog_Userverificationstatus { get; set; }
        public DbSet<Card_ActionMaster> Card_ActionMaster { get; set; }
        public DbSet<Card_CardData> Card_CardData { get; set; }
        public DbSet<Card_ColourMaster> Card_ColourMaster { get; set; }
        public DbSet<Card_LanguageMaster> Card_LanguageMaster { get; set; }
        public DbSet<Card_ProviderMaster> Card_ProviderMaster { get; set; }
        public DbSet<catalog_MainPdf> catalog_MainPdf { get; set; }
        public DbSet<city_history> city_history { get; set; }
        public DbSet<contactUsDetail> contactUsDetails { get; set; }
        public DbSet<contactUsDetail_History> contactUsDetail_History { get; set; }
        public DbSet<CustomerModule> CustomerModules { get; set; }
        public DbSet<CustomerPermission> CustomerPermissions { get; set; }
        public DbSet<CustomerType> CustomerTypes { get; set; }
        public DbSet<EscalationMatrix> EscalationMatrices { get; set; }
        public DbSet<EscalationMatrix_History> EscalationMatrix_History { get; set; }
        public DbSet<FooterCategory> FooterCategories { get; set; }
        public DbSet<FooterCategoryHistory> FooterCategoryHistories { get; set; }
        public DbSet<MappingUrl_Page> MappingUrl_Page { get; set; }
        public DbSet<MPartnerServiceLog> MPartnerServiceLogs { get; set; }
        public DbSet<ParentCategory> ParentCategories { get; set; }
        public DbSet<ProductAccessTable> ProductAccessTables { get; set; }
        public DbSet<ProductLevelOne> ProductLevelOnes { get; set; }
        public DbSet<ProductLevelOneHistory> ProductLevelOneHistories { get; set; }
        public DbSet<ProductLevelThree> ProductLevelThrees { get; set; }
        public DbSet<ProductLevelTwo> ProductLevelTwoes { get; set; }
        public DbSet<ProductthreeImageMapping> ProductthreeImageMappings { get; set; }
        public DbSet<Suggestion> Suggestions { get; set; }
        public DbSet<useraccount> useraccounts { get; set; }
        public DbSet<useraccounthistory> useraccounthistories { get; set; }
        public DbSet<UserVerification> UserVerifications { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Region> Regions { get; set; }
        public DbSet<Users_History> Users_History { get; set; }
        public DbSet<UsersList> UsersLists { get; set; }
        public DbSet<UsersListHistory> UsersListHistories { get; set; }
        public DbSet<Logger> Loggers { get; set; }
        public DbSet<MostSearchItem> MostSearchItems { get; set; }
        public DbSet<ProductCatergory> ProductCatergories { get; set; }
        public DbSet<UserVerification_log> UserVerification_log { get; set; }
        public DbSet<AllPage> AllPages { get; set; }
        public DbSet<city> cities { get; set; }
        public DbSet<NotificationAccessTable> NotificationAccessTables { get; set; }
        public DbSet<NotificationReadStatu> NotificationReadStatus { get; set; }
        public DbSet<Notification> Notifications { get; set; }
        public DbSet<GCM_NotificationLog> GCM_NotificationLog { get; set; }
        public DbSet<ContestMaster> ContestMasters { get; set; }
        public DbSet<ContestPicture_Tab> ContestPicture_Tab { get; set; }
        public DbSet<Dealer_Save_Image_History> Dealer_Save_Image_History { get; set; }
        public DbSet<ParentCategoryhistory> ParentCategoryhistories { get; set; }
        public DbSet<ProductCatergoryHistory> ProductCatergoryHistories { get; set; }
        public DbSet<FAQ> FAQs { get; set; }
        public DbSet<FAQ_History> FAQ_History { get; set; }
        public DbSet<Card_dynamicPage> Card_dynamicPage { get; set; }
        public DbSet<Card_dynamicPage_History> Card_dynamicPage_History { get; set; }
        public DbSet<Price_SchemeAccessTable> Price_SchemeAccessTable { get; set; }
        public DbSet<NotificationsHistory> NotificationsHistories { get; set; }
        public DbSet<Dealer_Save_Image> Dealer_Save_Image { get; set; }
        public DbSet<Country> Countries { get; set; }
        public DbSet<PermotionsList> PermotionsLists { get; set; }
        public DbSet<PermotionsListHistory> PermotionsListHistories { get; set; }
        public DbSet<MediaData> MediaDatas { get; set; }
        public DbSet<MediaDataHistory> MediaDataHistories { get; set; }
        public DbSet<SaveNotificationSurvey> SaveNotificationSurveys { get; set; }
        public DbSet<ProductLevelTwoHistory> ProductLevelTwoHistories { get; set; }
        public DbSet<ProductAccessTableForProduct> ProductAccessTableForProducts { get; set; }
        public DbSet<ProductLevelThreeHistory> ProductLevelThreeHistories { get; set; }
        public DbSet<NotificationSurvey> NotificationSurveys { get; set; }
        public DbSet<NotificationSurveyHistory> NotificationSurveyHistories { get; set; }
        public DbSet<Mapping_Productlevelthree_TechnicalSpecification> Mapping_Productlevelthree_TechnicalSpecification { get; set; }
        public DbSet<Productlevelthree_ColumnMaster> Productlevelthree_ColumnMaster { get; set; }
    
        public virtual ObjectResult<userpermission_Result> userpermission(Nullable<int> mode, string userid)
        {
            var modeParameter = mode.HasValue ?
                new ObjectParameter("mode", mode) :
                new ObjectParameter("mode", typeof(int));
    
            var useridParameter = userid != null ?
                new ObjectParameter("userid", userid) :
                new ObjectParameter("userid", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<userpermission_Result>("userpermission", modeParameter, useridParameter);
        }
    
        public virtual ObjectResult<GetNotificationAccessTable_Result> GetNotificationAccessTable(Nullable<int> nid)
        {
            var nidParameter = nid.HasValue ?
                new ObjectParameter("Nid", nid) :
                new ObjectParameter("Nid", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<GetNotificationAccessTable_Result>("GetNotificationAccessTable", nidParameter);
        }
    
        public virtual ObjectResult<userIdForPushNotification_Result> userIdForPushNotification(Nullable<int> notificationId)
        {
            var notificationIdParameter = notificationId.HasValue ?
                new ObjectParameter("NotificationId", notificationId) :
                new ObjectParameter("NotificationId", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<userIdForPushNotification_Result>("userIdForPushNotification", notificationIdParameter);
        }
    
        public virtual ObjectResult<GetProductLevelFourAccessTable_Result> GetProductLevelFourAccessTable(Nullable<int> pid)
        {
            var pidParameter = pid.HasValue ?
                new ObjectParameter("Pid", pid) :
                new ObjectParameter("Pid", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<GetProductLevelFourAccessTable_Result>("GetProductLevelFourAccessTable", pidParameter);
        }
    
        public virtual ObjectResult<PermotonsListPagingScheme_Price_New_Result> PermotonsListPagingScheme_Price_New(string pagename)
        {
            var pagenameParameter = pagename != null ?
                new ObjectParameter("pagename", pagename) :
                new ObjectParameter("pagename", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<PermotonsListPagingScheme_Price_New_Result>("PermotonsListPagingScheme_Price_New", pagenameParameter);
        }
    
        public virtual ObjectResult<ProductLevelThreePaging_Result> ProductLevelThreePaging()
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<ProductLevelThreePaging_Result>("ProductLevelThreePaging");
        }
    
        public virtual ObjectResult<HomePage_Paging_Result> HomePage_Paging(Nullable<int> pageId, Nullable<int> totalPage, string pagename)
        {
            var pageIdParameter = pageId.HasValue ?
                new ObjectParameter("PageId", pageId) :
                new ObjectParameter("PageId", typeof(int));
    
            var totalPageParameter = totalPage.HasValue ?
                new ObjectParameter("TotalPage", totalPage) :
                new ObjectParameter("TotalPage", typeof(int));
    
            var pagenameParameter = pagename != null ?
                new ObjectParameter("pagename", pagename) :
                new ObjectParameter("pagename", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<HomePage_Paging_Result>("HomePage_Paging", pageIdParameter, totalPageParameter, pagenameParameter);
        }
    
        public virtual ObjectResult<get_UserProfile_Result> get_UserProfile(string userid)
        {
            var useridParameter = userid != null ?
                new ObjectParameter("Userid", userid) :
                new ObjectParameter("Userid", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<get_UserProfile_Result>("get_UserProfile", useridParameter);
        }
    
        public virtual ObjectResult<GetAlertNotificationByUserId_Result> GetAlertNotificationByUserId(string userid)
        {
            var useridParameter = userid != null ?
                new ObjectParameter("userid", userid) :
                new ObjectParameter("userid", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<GetAlertNotificationByUserId_Result>("GetAlertNotificationByUserId", useridParameter);
        }
    
        public virtual ObjectResult<getCatalog_Upper_Result> getCatalog_Upper(string userid, Nullable<int> prodcatid)
        {
            var useridParameter = userid != null ?
                new ObjectParameter("userid", userid) :
                new ObjectParameter("userid", typeof(string));
    
            var prodcatidParameter = prodcatid.HasValue ?
                new ObjectParameter("prodcatid", prodcatid) :
                new ObjectParameter("prodcatid", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<getCatalog_Upper_Result>("getCatalog_Upper", useridParameter, prodcatidParameter);
        }
    
        public virtual ObjectResult<getContactUs_Details_Result> getContactUs_Details(string userid)
        {
            var useridParameter = userid != null ?
                new ObjectParameter("Userid", userid) :
                new ObjectParameter("Userid", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<getContactUs_Details_Result>("getContactUs_Details", useridParameter);
        }
    
        public virtual ObjectResult<getCustomerPermission_New_Result> getCustomerPermission_New(string userId, string languagecode)
        {
            var userIdParameter = userId != null ?
                new ObjectParameter("UserId", userId) :
                new ObjectParameter("UserId", typeof(string));
    
            var languagecodeParameter = languagecode != null ?
                new ObjectParameter("Languagecode", languagecode) :
                new ObjectParameter("Languagecode", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<getCustomerPermission_New_Result>("getCustomerPermission_New", userIdParameter, languagecodeParameter);
        }
    
        public virtual ObjectResult<GetPrice_SchemeByUserId_Result> GetPrice_SchemeByUserId(string userid, string pagename)
        {
            var useridParameter = userid != null ?
                new ObjectParameter("userid", userid) :
                new ObjectParameter("userid", typeof(string));
    
            var pagenameParameter = pagename != null ?
                new ObjectParameter("Pagename", pagename) :
                new ObjectParameter("Pagename", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<GetPrice_SchemeByUserId_Result>("GetPrice_SchemeByUserId", useridParameter, pagenameParameter);
        }
    
        public virtual ObjectResult<MHrVarifyOtpNotification_Result> MHrVarifyOtpNotification(string empid, string imeinumber, string osversion, string devicename, string otp, string appid, string devid, string ostype)
        {
            var empidParameter = empid != null ?
                new ObjectParameter("empid", empid) :
                new ObjectParameter("empid", typeof(string));
    
            var imeinumberParameter = imeinumber != null ?
                new ObjectParameter("imeinumber", imeinumber) :
                new ObjectParameter("imeinumber", typeof(string));
    
            var osversionParameter = osversion != null ?
                new ObjectParameter("osversion", osversion) :
                new ObjectParameter("osversion", typeof(string));
    
            var devicenameParameter = devicename != null ?
                new ObjectParameter("devicename", devicename) :
                new ObjectParameter("devicename", typeof(string));
    
            var otpParameter = otp != null ?
                new ObjectParameter("otp", otp) :
                new ObjectParameter("otp", typeof(string));
    
            var appidParameter = appid != null ?
                new ObjectParameter("appid", appid) :
                new ObjectParameter("appid", typeof(string));
    
            var devidParameter = devid != null ?
                new ObjectParameter("devid", devid) :
                new ObjectParameter("devid", typeof(string));
    
            var ostypeParameter = ostype != null ?
                new ObjectParameter("ostype", ostype) :
                new ObjectParameter("ostype", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<MHrVarifyOtpNotification_Result>("MHrVarifyOtpNotification", empidParameter, imeinumberParameter, osversionParameter, devicenameParameter, otpParameter, appidParameter, devidParameter, ostypeParameter);
        }
    
        public virtual int update_Userverification(string userid, string osversion, string ostype, string appversion, string deviceid, string fcm_token, string token)
        {
            var useridParameter = userid != null ?
                new ObjectParameter("Userid", userid) :
                new ObjectParameter("Userid", typeof(string));
    
            var osversionParameter = osversion != null ?
                new ObjectParameter("osversion", osversion) :
                new ObjectParameter("osversion", typeof(string));
    
            var ostypeParameter = ostype != null ?
                new ObjectParameter("ostype", ostype) :
                new ObjectParameter("ostype", typeof(string));
    
            var appversionParameter = appversion != null ?
                new ObjectParameter("appversion", appversion) :
                new ObjectParameter("appversion", typeof(string));
    
            var deviceidParameter = deviceid != null ?
                new ObjectParameter("deviceid", deviceid) :
                new ObjectParameter("deviceid", typeof(string));
    
            var fcm_tokenParameter = fcm_token != null ?
                new ObjectParameter("fcm_token", fcm_token) :
                new ObjectParameter("fcm_token", typeof(string));
    
            var tokenParameter = token != null ?
                new ObjectParameter("token", token) :
                new ObjectParameter("token", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("update_Userverification", useridParameter, osversionParameter, ostypeParameter, appversionParameter, deviceidParameter, fcm_tokenParameter, tokenParameter);
        }
    
        public virtual ObjectResult<SP_UserPermission_bottomMenu_Result> SP_UserPermission_bottomMenu(string userid)
        {
            var useridParameter = userid != null ?
                new ObjectParameter("userid", userid) :
                new ObjectParameter("userid", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<SP_UserPermission_bottomMenu_Result>("SP_UserPermission_bottomMenu", useridParameter);
        }
    
        public virtual ObjectResult<Sp_MHrCreateOtp_Result> Sp_MHrCreateOtp(string empid, string imeinumber, string osversion, string devicename, string appversion)
        {
            var empidParameter = empid != null ?
                new ObjectParameter("empid", empid) :
                new ObjectParameter("empid", typeof(string));
    
            var imeinumberParameter = imeinumber != null ?
                new ObjectParameter("imeinumber", imeinumber) :
                new ObjectParameter("imeinumber", typeof(string));
    
            var osversionParameter = osversion != null ?
                new ObjectParameter("osversion", osversion) :
                new ObjectParameter("osversion", typeof(string));
    
            var devicenameParameter = devicename != null ?
                new ObjectParameter("devicename", devicename) :
                new ObjectParameter("devicename", typeof(string));
    
            var appversionParameter = appversion != null ?
                new ObjectParameter("appversion", appversion) :
                new ObjectParameter("appversion", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<Sp_MHrCreateOtp_Result>("Sp_MHrCreateOtp", empidParameter, imeinumberParameter, osversionParameter, devicenameParameter, appversionParameter);
        }
    
        public virtual ObjectResult<GetColumnNames_Result> GetColumnNames()
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<GetColumnNames_Result>("GetColumnNames");
        }
    
        public virtual ObjectResult<GetSurveyRecord_Result> GetSurveyRecord(Nullable<int> id)
        {
            var idParameter = id.HasValue ?
                new ObjectParameter("Id", id) :
                new ObjectParameter("Id", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<GetSurveyRecord_Result>("GetSurveyRecord", idParameter);
        }
    }
}
