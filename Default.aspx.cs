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
    public partial class Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // запускаем окно, только если пользователь еще не залогинен
            pnlRunTheLoginWindow.Visible = !AccountEngine.IsCurrentUserLoggedIn;
            
            // отключаем обязательно меню, только для страницы входа
            RadMenu mainMenu = (RadMenu)this.Master.FindControl("MainMenu");
            mainMenu.Enabled = false;

            // если пользователь уже залгинен всегда идем на основною страницу
            if (AccountEngine.IsCurrentUserLoggedIn == true)
            {
                Response.Redirect("~/StudentsEdit.aspx");
            }

            if(IsPostBack==false)
            {
                
            }
        }
        protected void btnLogOn_Click(object sender, EventArgs e)
        {
            String login = tbxLogin.Text;
            if (String.IsNullOrWhiteSpace(login))
            {
                pnlValidation.Visible = true;
                lblValidationText.Text = "Для входа логин должен быть задан.";
                return;
            }

            String password = tbxPassword.Text;
            if (String.IsNullOrWhiteSpace(password))
            {
                pnlValidation.Visible = true;
                lblValidationText.Text = "Для входа пароль должен быть задан.";
                return;
            }

            if ( AccountEngine.LogOn(login, password) == false)
            {
                pnlValidation.Visible = true;
                lblValidationText.Text = "Нет пользователя с такими логином и паролем.";
            }
            else
            {
                pnlValidation.Visible = false;
                lblValidationText.Text = "";

                Response.Redirect("~/StudentsEdit.aspx");
            }
        }
    }
}
