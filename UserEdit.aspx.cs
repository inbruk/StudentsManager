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

namespace StudentsManager.PresentationLayer
{
    public partial class UserEdit : System.Web.UI.Page
    {
        private Guid _currUserType;

        List<DTO.DictionaryItem4List> ListOfUserTypes
        {
            get
            {
                List<DTO.DictionaryItem4List> result;

                if( Session["UserEdit_ListOfUserTypes"]==null )
                {
                    Guid userTypeId = DictionaryTypeTools.Read("Тип пользователя").Id;
                    result = DictionaryTools.ReadAllByTypeId(userTypeId);
                    Session["UserEdit_ListOfUserTypes"] = result;
                    return result;
                }
                else
                {
                    result = (List<DTO.DictionaryItem4List>) Session["UserEdit_ListOfUserTypes"];
                }
                return result;
            }
        }

        protected RadGrid MainGrid { get { return (RadGrid)wlbPanel.getInternalControl("mainGrid"); } }

        protected void GetCurrentUserType()
        {
            // вытаскиваем из параметра тип словаря для редактирования
            String paramStr = (String)Request["UserType"];
            if (paramStr != null)
            {
                if (Guid.TryParse(paramStr, out _currUserType) == false)
                {
                    paramStr = null;
                }
            }

            // если параметр не задан или задан некорректно, то уходим на основную страницу
            if (paramStr == null)
            {
                Response.Redirect("~/StudentsEdit.aspx");
                return;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            // если параметр не задан (задан некорректно) или пользователь еще не залогинен, то на страницу входа
            if (AccountEngine.IsCurrentUserLoggedIn == false)
            {
                Response.Redirect("~/Default.aspx");
                return;
            }

            // если пользователь не администратор, то нужно уходить на главную страницу 
            String userTypeName = AccountEngine.GetUserTypeName();
            if (userTypeName != "Пользователи - администраторы")
            {
                Response.Redirect("~/StudentsEdit.aspx");
                return;
            }
            
            // делаем это всегда, а не только при первомм заходе, для того чтобы можно было переключаться со словаря на словарь
            GetCurrentUserType();

            // заполним заголовок панели исходя из типа пользователя
            DTO.DictionaryItem4List currItem = ListOfUserTypes.SingleOrDefault( x => x.Id==_currUserType );
            if( currItem!=null )
            {
                wlbPanel.Title = currItem.Name;
            }

            if (IsPostBack == false)
            {
                RefreshDDLInWindow();
                RefreshGrid();
            }
        }

        // обновим содержимое дроп даун листа на окне редактирования
        protected void RefreshDDLInWindow()
        {
            ddlUserType.DataSource = ListOfUserTypes;
            ddlUserType.DataBind();
        }

        protected void RefreshGrid()
        {
            // грузим нужные данные из БД
            List<DTO.User4List> data = UserTools.ReadAllByTypeId(_currUserType);

            // ребиндим их в грид
            MainGrid.DataSource = data;
            MainGrid.DataBind();
        }

        protected void mainGrid_DeleteCommand(object sender, GridCommandEventArgs e)
        {
            Guid id = (Guid)((GridDataItem)e.Item).GetDataKeyValue("Id");

            try
            {
                UserTools.Delete(id);
            }
            catch
            {
                RadWindowManager1.RadAlert(
                    "При удалении возникли ошибки. Скорее всего это связанно с тем, что в БД есть сущности ссылающиеся на удаляемую. Удалите их и попробуйте снова.",
                    480, 180, "Ошибка при удалении пользователя", null
                );
            }

            RefreshGrid();
        }

        protected void mainGrid_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            // грузим нужные данные из БД
            List<DTO.User4List> data = UserTools.ReadAllByTypeId(_currUserType);

            // ребиндим их в грид
            MainGrid.DataSource = data;
        }

        private Boolean IsNewFlag // используется, только во время редактирования
        {
            set
            {
                Session["UserEdit_isNewFlag"] = value;
            }
            get
            {
                Boolean result = (Boolean)Session["UserEdit_isNewFlag"];
                return result;
            }
        }
        private DTO.User CurrItem // используется, только во время редактирования
        {
            set
            {
                Session["UserEdit_CurrItem"] = value;
            }
            get
            {
                DTO.User result = (DTO.User)Session["UserEdit_CurrItem"];
                return result;
            }
        }

        protected Int32 GetUserTypeDDLIndexById(Guid userTypeId)
        {
            List<DTO.DictionaryItem4List> listOfUserTypes = ListOfUserTypes;
            IEnumerable<DTO.DictionaryItem4List> iEnumerableOfUserTypes = listOfUserTypes.AsEnumerable<DTO.DictionaryItem4List>();
            DTO.DictionaryItem4List currItem = iEnumerableOfUserTypes.SingleOrDefault(x => x.Id == userTypeId);
            Int32 index = listOfUserTypes.IndexOf(currItem);

            return index;
        }

        protected void mainGrid_ItemCommand(object sender, GridCommandEventArgs e)
        {
            switch (e.CommandName)
            {
                case "InitInsert":
                    IsNewFlag = true;
                    break;
                case "MyEditCommand":
                    IsNewFlag = false;
                    break;
                default: return;
            }

            if (IsNewFlag == true)
            {
                RadWindow1.Title = "Добавление нового пользователя";

                RadTextBox tbxLogin              = (RadTextBox)RadWindow1.ContentContainer.FindControl("tbxLogin");
                RadTextBox tbxPassword           = (RadTextBox)RadWindow1.ContentContainer.FindControl("tbxPassword");
                RadTextBox tbxSurnameAndInitials = (RadTextBox)RadWindow1.ContentContainer.FindControl("tbxSurnameAndInitials");
                RadTextBox tbxEMail              = (RadTextBox)RadWindow1.ContentContainer.FindControl("tbxEMail");
                RadTextBox tbxPhone              = (RadTextBox)RadWindow1.ContentContainer.FindControl("tbxPhone");

                tbxLogin.Text = "";
                tbxPassword.Text = "";
                tbxSurnameAndInitials.Text = "";
                tbxEMail.Text = "";
                tbxPhone.Text = "";

                ddlUserType.SelectedIndex = GetUserTypeDDLIndexById(_currUserType);
            }
            else
            {
                RadWindow1.Title = "Редактирование пользователя";

                GridDataItem item = (GridDataItem)e.Item;
                Guid currEditedItemId = (Guid)item.GetDataKeyValue("Id");
                CurrItem = UserTools.Read(currEditedItemId);

                RadTextBox tbxLogin              = (RadTextBox)RadWindow1.ContentContainer.FindControl("tbxLogin");
                RadTextBox tbxPassword           = (RadTextBox)RadWindow1.ContentContainer.FindControl("tbxPassword");
                RadTextBox tbxSurnameAndInitials = (RadTextBox)RadWindow1.ContentContainer.FindControl("tbxSurnameAndInitials");
                RadTextBox tbxEMail              = (RadTextBox)RadWindow1.ContentContainer.FindControl("tbxEMail");
                RadTextBox tbxPhone              = (RadTextBox)RadWindow1.ContentContainer.FindControl("tbxPhone");

                tbxLogin.Text              = CurrItem.Login;
                tbxPassword.Text           = CurrItem.Password;
                tbxSurnameAndInitials.Text = CurrItem.SurnameAndInitials;
                tbxEMail.Text              = CurrItem.EMail;
                tbxPhone.Text              = CurrItem.Phone;

                ddlUserType.SelectedIndex = GetUserTypeDDLIndexById(CurrItem.UserType);
            }

            HideValidatorsMessage();

            RadWindow1.VisibleOnPageLoad = true;
            RadWindow1.Visible = true;
            e.Canceled = true;
        }

        protected void HideValidatorsMessage()
        {
            tdValidationArea.Style["padding"] = "0px";
            tdValidationArea.Style["height"] = "0px";
            RadWindow1.Height = 300;

            pnlValidation.Visible = false;
            lblValidationText.Text = "";
        }
        protected void ShowValidatorsMessage(String msg)
        {
            tdValidationArea.Style["padding"] = "5px";
            tdValidationArea.Style["height"] = "90px";
            RadWindow1.Height = 400;

            pnlValidation.Visible = true;
            lblValidationText.Text = msg;
        }

        protected Boolean IsEnteredDataValid()
        {
            String errorMessage = null;

            RadTextBox tbxLogin = (RadTextBox)RadWindow1.ContentContainer.FindControl("tbxLogin");
            RadTextBox tbxPassword = (RadTextBox)RadWindow1.ContentContainer.FindControl("tbxPassword");
            RadTextBox tbxSurnameAndInitials = (RadTextBox)RadWindow1.ContentContainer.FindControl("tbxSurnameAndInitials");
            RadTextBox tbxEMail = (RadTextBox)RadWindow1.ContentContainer.FindControl("tbxEMail");
            RadTextBox tbxPhone = (RadTextBox)RadWindow1.ContentContainer.FindControl("tbxPhone");

            // проверим корректно ли заполнено поле с логином
            errorMessage = ValidationTools.CheckLogin(tbxLogin.Text);
            if ( errorMessage!=null )
            {
                ShowValidatorsMessage(errorMessage);
                return false;
            }

            // проверим существует ли запись с таким логином в текущем словаре
            Guid currId = IsNewFlag ? Guid.Empty : CurrItem.Id;
            if ( UserTools.IsLoginExists(currId, tbxLogin.Text) == true )
            {
                ShowValidatorsMessage("Такой логин уже есть в БД. Значение поля \"Имя входа\" должно быть уникальным.");
                return false;
            }

            // проверим корректно ли заполнено поле с паролем
            errorMessage = ValidationTools.CheckPassword(tbxPassword.Text);
            if (errorMessage != null)
            {
                ShowValidatorsMessage(errorMessage);
                return false;
            }

            // проверим корректно ли заполнено поле с фамилией и инициалами
            errorMessage = ValidationTools.CheckSurnameAndInitials(tbxSurnameAndInitials.Text);
            if (errorMessage != null)
            {
                ShowValidatorsMessage(errorMessage);
                return false;
            }

            // проверим корректно ли заполнено поле с EMail
            errorMessage = ValidationTools.CheckEMail(tbxEMail.Text);
            if (errorMessage != null)
            {
                ShowValidatorsMessage(errorMessage);
                return false;
            }

            // проверим корректно ли заполнено поле с Phone
            errorMessage = ValidationTools.CheckPhone(tbxPhone.Text);
            if (errorMessage != null)
            {
                ShowValidatorsMessage(errorMessage);
                return false;
            }

            HideValidatorsMessage();
            return true; // все данные корректны
        }

        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            if (IsEnteredDataValid() == false) return;

            RadTextBox tbxLogin              = (RadTextBox)RadWindow1.ContentContainer.FindControl("tbxLogin");
            RadTextBox tbxPassword           = (RadTextBox)RadWindow1.ContentContainer.FindControl("tbxPassword");
            RadTextBox tbxSurnameAndInitials = (RadTextBox)RadWindow1.ContentContainer.FindControl("tbxSurnameAndInitials");
            RadTextBox tbxEMail              = (RadTextBox)RadWindow1.ContentContainer.FindControl("tbxEMail");
            RadTextBox tbxPhone              = (RadTextBox)RadWindow1.ContentContainer.FindControl("tbxPhone");

            Guid userTypeId;
            Guid.TryParse(ddlUserType.SelectedValue, out userTypeId);

            if (IsNewFlag == true)
            {
                DTO.User newUserItem = new DTO.User();

                newUserItem.Login              = tbxLogin.Text;
                newUserItem.Password           = tbxPassword.Text;
                newUserItem.SurnameAndInitials = tbxSurnameAndInitials.Text;
                newUserItem.EMail              = tbxEMail.Text;
                newUserItem.Phone              = tbxPhone.Text;

                newUserItem.UserType           = userTypeId;

                UserTools.Create(newUserItem);
            }
            else
            {
                CurrItem.Login = tbxLogin.Text;
                CurrItem.Password = tbxPassword.Text;
                CurrItem.SurnameAndInitials = tbxSurnameAndInitials.Text;
                CurrItem.EMail = tbxEMail.Text;
                CurrItem.Phone = tbxPhone.Text;

                CurrItem.UserType = userTypeId;

                UserTools.Update(CurrItem);
            }

            RadWindow1.VisibleOnPageLoad = false;
            RadWindow1.Visible = false;
            RefreshGrid();
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            RadWindow1.VisibleOnPageLoad = false;
            RadWindow1.Visible = false;
        }
    }
}