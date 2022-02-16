using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Data;
using System.Configuration;
using System.Web.Security;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

using Telerik.Web.UI;

using DTO = StudentsManager.BusinessLogicLayer.DataTransferObjects;
using StudentsManager.PresentationLayer.Tools;

namespace StudentsManager.PresentationLayer
{
    public partial class Global : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack == false)
            {
                // показываем отключенную часть меню, если пользователь администратор
                if (AccountEngine.IsCurrentUserLoggedIn == true)
                {
                    String userTypeName = AccountEngine.GetUserTypeName();
                    if (userTypeName == "Пользователи - администраторы")
                    {
                        RadMenuItem adminMenuItem = MainMenu.Items.FindItemByValue("mitAdministration");
                        adminMenuItem.Enabled = true;
                    }
                }
            }
        }

        protected void MainMenu_ItemClick(object sender, Telerik.Web.UI.RadMenuEventArgs e)
        {
            switch( e.Item.Value )
            {
                case "mitExit":
                    AccountEngine.LogOff();
                    Response.Redirect("~/Default.aspx");
                break;
                case "mitLanguages":
                    Response.Redirect("~/DictionariesEdit.aspx?DictionaryType=37BBAD70-320C-4DBE-B3E1-A98C8425BE4B");
                break;
                case "mitEventType":
                    Response.Redirect("~/DictionariesEdit.aspx?DictionaryType=ABBCAD53-4D6E-42C9-ADEB-8BA14E282435");
                break;
                case "mitAuditorium":
                    Response.Redirect("~/DictionariesEdit.aspx?DictionaryType=356A2EA2-DFC0-4139-B220-D80CE3429475");
                break;
                case "mitEquipment":
                    Response.Redirect("~/DictionariesEdit.aspx?DictionaryType=250F329D-2501-4D4E-B38B-870C7F895ED8");
                break;              
                case "mitUsersManagers":
                    Response.Redirect("~/UserEdit.aspx?UserType=EEA923AF-CC2A-40B8-AB4C-4CF25C5821EB");
                break;
                case "mitUsersAdministrators":
                    Response.Redirect("~/UserEdit.aspx?UserType=B3CF0861-B30C-4C65-B4D5-B501FCAC92F8");
                break;
                case "mitTutors":
                    Response.Redirect("~/EmployeeEdit.aspx?EmployeeType=FE29EFC8-6FEC-4A69-86E6-F89478152C34");
                break;
                case "mitAssistents":
                    Response.Redirect("~/EmployeeEdit.aspx?EmployeeType=948B9290-8FDB-4E73-A22C-26BABE6C993E");
                break;
                case "mitEducationLevel":
                    Response.Redirect("~/EducationLevelEdit.aspx");
                break;
                case "mitDatabaseSettings":
                    Response.Redirect("~/DatabaseSettings.aspx");
                break;
                case "mitPriceGroup":
                    Response.Redirect("~/PriceGroup.aspx");
                break;
                case "mitStudents":
                    Response.Redirect("~/StudentsEdit.aspx");
                break;
                case "mitFormedGroups":
                    Response.Redirect("~/GroupEdit.aspx?GroupState=A5E62117-A9C7-4832-AE48-F403CA877624");
                break;
                case "mitActiveGroups":
                    Response.Redirect("~/GroupEdit.aspx?GroupState=928D0D63-335E-4D27-A77F-06FC34B47E3F");
                break;
                default:break;
            }
        }
    }
}