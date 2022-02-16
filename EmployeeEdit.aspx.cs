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
    public partial class EmployeeEdit : System.Web.UI.Page
    {
        private Guid _currEmployeeType;

        List<DTO.DictionaryItem4List> ListOfEmployeeTypes
        {
            get
            {
                List<DTO.DictionaryItem4List> result;

                if( Session["EmployeeEdit_ListOfEmployeeTypes"]==null )
                {
                    Guid EmployeeTypeId = DictionaryTypeTools.Read("Тип сотрудника").Id;
                    result = DictionaryTools.ReadAllByTypeId(EmployeeTypeId);
                    Session["EmployeeEdit_ListOfEmployeeTypes"] = result;
                    return result;
                }
                else
                {
                    result = (List<DTO.DictionaryItem4List>) Session["EmployeeEdit_ListOfEmployeeTypes"];
                }
                return result;
            }
        }

        protected RadGrid MainGrid { get { return (RadGrid)wlbPanel.getInternalControl("MainGrid"); } }

        protected void GetCurrentEmployeeType()
        {
            // вытаскиваем из параметра тип словаря для редактирования
            String paramStr = (String)Request["EmployeeType"];
            if (paramStr != null)
            {
                if (Guid.TryParse(paramStr, out _currEmployeeType) == false)
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

        protected String GetCurrentEmployeeTypeName()
        {
            DTO.DictionaryItem4List dicItem = ListOfEmployeeTypes.SingleOrDefault(x => x.Id == _currEmployeeType);
            if( dicItem!=null )
            {
                return dicItem.Name;
            }
            else
            {
                return String.Empty;
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
            
            // делаем это всегда, а не только при первомм заходе, для того чтобы можно было переключаться со словаря на словарь
            GetCurrentEmployeeType();

            wlbPanel.Title = GetCurrentEmployeeTypeName();

            if (IsPostBack == false)
            {
                RefreshDDLInWindow();
                RefreshGrid();
            }
        }

        // обновим содержимое дроп даун листа на окне редактирования
        protected void RefreshDDLInWindow()
        {
            ddlEmployeeType.DataSource = ListOfEmployeeTypes;
            ddlEmployeeType.DataBind();
        }

        protected void RefreshGrid()
        {
            // грузим нужные данные из БД
            List<DTO.Employee4List> data = EmployeeTools.ReadAllByTypeId(_currEmployeeType);

            // ребиндим их в грид
            MainGrid.DataSource = data;
            MainGrid.DataBind();
        }

        protected void mainGrid_DeleteCommand(object sender, GridCommandEventArgs e)
        {
            Guid id = (Guid)((GridDataItem)e.Item).GetDataKeyValue("Id");
            
            try
            {
                EmployeeTools.Delete(id);
            }
            catch
            {
                RadWindowManager1.RadAlert(
                    "При удалении возникли ошибки. Скорее всего это связанно с тем, что в БД есть сущности ссылающиеся на удаляемую. Удалите их и попробуйте снова.",
                    480, 180, "Ошибка при удалении сотрудника", null
                );
            }

            RefreshGrid();
        }

        protected void mainGrid_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            // грузим нужные данные из БД
            List<DTO.Employee4List> data = EmployeeTools.ReadAllByTypeId(_currEmployeeType);

            // ребиндим их в грид
            MainGrid.DataSource = data;
        }

        private Boolean IsNewFlag // используется, только во время редактирования
        {
            set
            {
                Session["EmployeeEdit_isNewFlag"] = value;
            }
            get
            {
                Boolean result = (Boolean)Session["EmployeeEdit_isNewFlag"];
                return result;
            }
        }
        private DTO.Employee CurrItem // используется, только во время редактирования
        {
            set
            {
                Session["EmployeeEdit_CurrItem"] = value;
            }
            get
            {
                DTO.Employee result = (DTO.Employee)Session["EmployeeEdit_CurrItem"];
                return result;
            }
        }

        protected Int32 GetEmployeeTypeDDLIndexById(Guid EmployeeTypeId)
        {
            List<DTO.DictionaryItem4List> listOfEmployeeTypes = ListOfEmployeeTypes;
            IEnumerable<DTO.DictionaryItem4List> iEnumerableOfEmployeeTypes = listOfEmployeeTypes.AsEnumerable<DTO.DictionaryItem4List>();
            DTO.DictionaryItem4List currItem = iEnumerableOfEmployeeTypes.SingleOrDefault(x => x.Id == EmployeeTypeId);
            Int32 index = listOfEmployeeTypes.IndexOf(currItem);

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
                RadWindow1.Title = "Добавление нового сотрудника";

                RadTextBox tbxName               = (RadTextBox)RadWindow1.ContentContainer.FindControl("tbxName");
                RadTextBox tbxSurname            = (RadTextBox)RadWindow1.ContentContainer.FindControl("tbxSurname");
                RadTextBox tbxPatronomic         = (RadTextBox)RadWindow1.ContentContainer.FindControl("tbxPatronomic");
                RadTextBox tbxSurnameAndInitials = (RadTextBox)RadWindow1.ContentContainer.FindControl("tbxSurnameAndInitials");
                RadTextBox tbxEMail              = (RadTextBox)RadWindow1.ContentContainer.FindControl("tbxEMail");
                RadTextBox tbxPhone              = (RadTextBox)RadWindow1.ContentContainer.FindControl("tbxPhone");

                tbxName.Text = "";
                tbxSurname.Text = "";
                tbxPatronomic.Text = "";
                tbxSurnameAndInitials.Text = "";
                tbxEMail.Text = "";
                tbxPhone.Text = "";

                ddlEmployeeType.SelectedIndex = GetEmployeeTypeDDLIndexById(_currEmployeeType);
            }
            else
            {
                RadWindow1.Title = "Редактирование сотрудника";

                GridDataItem item = (GridDataItem)e.Item;
                Guid currEditedItemId = (Guid)item.GetDataKeyValue("Id");
                CurrItem = EmployeeTools.Read(currEditedItemId);

                RadTextBox tbxName               = (RadTextBox)RadWindow1.ContentContainer.FindControl("tbxName");
                RadTextBox tbxSurname            = (RadTextBox)RadWindow1.ContentContainer.FindControl("tbxSurname");
                RadTextBox tbxPatronomic         = (RadTextBox)RadWindow1.ContentContainer.FindControl("tbxPatronomic");
                RadTextBox tbxSurnameAndInitials = (RadTextBox)RadWindow1.ContentContainer.FindControl("tbxSurnameAndInitials");
                RadTextBox tbxEMail              = (RadTextBox)RadWindow1.ContentContainer.FindControl("tbxEMail");
                RadTextBox tbxPhone              = (RadTextBox)RadWindow1.ContentContainer.FindControl("tbxPhone");

                tbxName.Text               = CurrItem.Name;
                tbxSurname.Text            = CurrItem.Surname;
                tbxPatronomic.Text         = CurrItem.Patronomic;
                tbxSurnameAndInitials.Text = CurrItem.SurnameAndInitails;
                tbxEMail.Text              = CurrItem.Email;
                tbxPhone.Text              = CurrItem.Phone;

                ddlEmployeeType.SelectedIndex = GetEmployeeTypeDDLIndexById(CurrItem.EmployeeType);
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
            RadWindow1.Height = 340;

            pnlValidation.Visible = false;
            lblValidationText.Text = "";
        }
        protected void ShowValidatorsMessage(String msg)
        {
            tdValidationArea.Style["padding"] = "5px";
            tdValidationArea.Style["height"] = "90px";
            RadWindow1.Height = 440;

            pnlValidation.Visible = true;
            lblValidationText.Text = msg;
        }

        protected Boolean IsEnteredDataValid()
        {
            String errorMessage = null;

            RadTextBox tbxName = (RadTextBox)RadWindow1.ContentContainer.FindControl("tbxName");
            RadTextBox tbxSurname = (RadTextBox)RadWindow1.ContentContainer.FindControl("tbxSurname");
            RadTextBox tbxPatronomic = (RadTextBox)RadWindow1.ContentContainer.FindControl("tbxPatronomic");
            RadTextBox tbxSurnameAndInitials = (RadTextBox)RadWindow1.ContentContainer.FindControl("tbxSurnameAndInitials");
            RadTextBox tbxEMail = (RadTextBox)RadWindow1.ContentContainer.FindControl("tbxEMail");
            RadTextBox tbxPhone = (RadTextBox)RadWindow1.ContentContainer.FindControl("tbxPhone");

            // проверим корректно ли заполнено поле с именем
            errorMessage = ValidationTools.CheckName(tbxName.Text);
            if (errorMessage != null)
            {
                ShowValidatorsMessage(errorMessage);
                return false;
            }

            // проверим корректно ли заполнено поле с фамилией
            errorMessage = ValidationTools.CheckSurname(tbxSurname.Text);
            if (errorMessage != null)
            {
                ShowValidatorsMessage(errorMessage);
                return false;
            }

            // проверим существует ли работник с таким именем/фамилией
            Guid currId = IsNewFlag ? Guid.Empty : CurrItem.Id;
            if (EmployeeTools.IsNameAndSurnameExists(currId, tbxName.Text, tbxSurname.Text) == true)
            {
                ShowValidatorsMessage("Такие имя и фамилия уже есть в БД. Комбинация полей \"Имя\" и \"Фамилия\" должна быть уникальной.");
                return false;
            }

            // проверим корректно ли заполнено поле с отчеством
            errorMessage = ValidationTools.CheckPatronomic(tbxPatronomic.Text);
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

            RadTextBox tbxName               = (RadTextBox)RadWindow1.ContentContainer.FindControl("tbxName");
            RadTextBox tbxSurname            = (RadTextBox)RadWindow1.ContentContainer.FindControl("tbxSurname");
            RadTextBox tbxPatronomic         = (RadTextBox)RadWindow1.ContentContainer.FindControl("tbxPatronomic");
            RadTextBox tbxSurnameAndInitials = (RadTextBox)RadWindow1.ContentContainer.FindControl("tbxSurnameAndInitials");
            RadTextBox tbxEMail              = (RadTextBox)RadWindow1.ContentContainer.FindControl("tbxEMail");
            RadTextBox tbxPhone              = (RadTextBox)RadWindow1.ContentContainer.FindControl("tbxPhone");

            Guid EmployeeTypeId;
            Guid.TryParse(ddlEmployeeType.SelectedValue, out EmployeeTypeId);

            if (IsNewFlag == true)
            {
                DTO.Employee newUserItem = new DTO.Employee();

                newUserItem.Name               = tbxName.Text;
                newUserItem.Surname            = tbxSurname.Text;
                newUserItem.Patronomic         = tbxPatronomic.Text;
                newUserItem.SurnameAndInitails = tbxSurnameAndInitials.Text;
                newUserItem.Email              = tbxEMail.Text;
                newUserItem.Phone              = tbxPhone.Text;

                newUserItem.EmployeeType       = EmployeeTypeId;

                EmployeeTools.Create(newUserItem);
            }
            else
            {
                CurrItem.Name               = tbxName.Text;
                CurrItem.Surname            = tbxSurname.Text;
                CurrItem.Patronomic         = tbxPatronomic.Text;
                CurrItem.SurnameAndInitails = tbxSurnameAndInitials.Text;
                CurrItem.Email              = tbxEMail.Text;
                CurrItem.Phone              = tbxPhone.Text;

                CurrItem.EmployeeType = EmployeeTypeId;

                EmployeeTools.Update(CurrItem);
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