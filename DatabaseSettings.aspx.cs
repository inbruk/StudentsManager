using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Linq;

using System.Data;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Web.Security;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

using Telerik.Web.UI;

using DTO = StudentsManager.BusinessLogicLayer.DataTransferObjects;
using StudentsManager.BusinessLogicLayer;
using StudentsManager.PresentationLayer.Tools;
using StudentsManager.PresentationLayer.AuxiliaryControls;

namespace StudentsManager.PresentationLayer
{
    public partial class DatabaseSettings : System.Web.UI.Page
    {
        protected void ShowDefaultMessage()
        {
            lblValidationText.Text =
                "Внимание ! При сохранении неправильных параметров БД будет недоступна в программе. То есть ничего работать не будет, даже логин. Если это случилось исправлять в web.config.";
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            // если параметр не задан (задан некорректно) или пользователь еще не залогинен, то на страницу входа
            if (AccountEngine.IsCurrentUserLoggedIn == false)
            {
                Response.Redirect("~/Default.aspx");
                return;
            }

            if(IsPostBack==false)
            {
                String configFileName = Request.PhysicalApplicationPath + "web.config";
                CommonSettingsTools.ConfigFilePathName = configFileName;

                LoadFormData();
                ShowDefaultMessage();
            }
        }        

        protected void LoadFormData()
        {
            String dataSource;
            String initialCatalog;
            String userId;
            String password;

            CommonSettingsTools.GetCurrentConnectionStringParameters( out dataSource, out initialCatalog, out userId, out password );

            tbxDataSource.Text = dataSource;
            tbxInitialCatalog.Text = initialCatalog;
            tbxUserId.Text = userId;
            tbxPassword.Text = password;

            tbxBackupPath.Text = CommonSettingsTools.GetValue("DatabaseBackupPath");
        }
        protected void SaveFormData()
        {
            String dataSource;
            String initialCatalog;
            String userId;
            String password;
            String backupPath;

            dataSource = tbxDataSource.Text;
            initialCatalog = tbxInitialCatalog.Text;
            userId = tbxUserId.Text;
            password = tbxPassword.Text;
            backupPath = tbxBackupPath.Text;

            CommonSettingsTools.SetValue("DatabaseBackupPath", backupPath);
            CommonSettingsTools.UpdateCurrentConnectionStringWithParameters(dataSource, initialCatalog, userId, password);
        }

        protected Boolean CheckFormParameters()
        {
            if (String.IsNullOrWhiteSpace(tbxDataSource.Text) == true)
            {
                lblValidationText.Text = "Ошибка валидации. Обязательный параметр DataSource пустой или не задан.";
                return false;
            }

            if (String.IsNullOrWhiteSpace(tbxInitialCatalog.Text) == true)
            {
                lblValidationText.Text = "Ошибка валидации. Обязательный параметр InitialCatalog пустой или не задан.";
                return false;
            }

            if (String.IsNullOrWhiteSpace(tbxUserId.Text) == true)
            {
                lblValidationText.Text = "Ошибка валидации. Обязательный параметр UserId пустой или не задан.";
                return false;
            }

            if (String.IsNullOrWhiteSpace(tbxPassword.Text) == true)
            {
                lblValidationText.Text = "Ошибка валидации. Обязательный параметр Password пустой или не задан.";
                return false;
            }

            if (String.IsNullOrWhiteSpace(tbxBackupPath.Text) == true)
            {
                lblValidationText.Text = "Ошибка валидации. Обязательный параметр BackupPath пустой или не задан.";
                return false;
            }

            ShowDefaultMessage();
            return true;
        }

        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            if (CheckFormParameters() == false) return;
            RadWindow1.Visible = false;
            RadWindowManager1.RadConfirm("Вы уверены в том, что хотите сохранить параметры БД ?", "confirmUpdateCallbackFn", 500, 150, null, "Подтверждение");
        }
        protected void btnUpdateAction_Click(object sender, EventArgs e)
        {
            //SaveFormData(); // разкоментарить при хостинге на локальном компе

            Response.Redirect("~/Default.aspx");
        }
        protected void btnMakeBackup_Click(object sender, EventArgs e)
        {
            RadWindow1.Visible = false;
            RadWindowManager1.RadConfirm("Вы уверены в том, что хотите создать бэкап БД ?", "confirmBackupCallbackFn", 500, 150, null, "Подтверждение");
        }

        protected void btnBackupAction_Click(object sender, EventArgs e)
        {
            //CommonSettingsTools.MakeDatabaseBackup(); // разкоментарить при хостинге на локальном компе

            RadWindow1.Visible = true;
            LoadFormData();
        }

        protected void btnCancelAction_Click(object sender, EventArgs e)
        {
            RadWindow1.Visible = true;
            LoadFormData();
        }
    }
}