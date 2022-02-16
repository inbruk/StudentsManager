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
    public partial class EducationLevelEdit : System.Web.UI.Page
    {
        List<DTO.DictionaryItem4List> ListOfLanguages
        {
            get
            {
                List<DTO.DictionaryItem4List> result;

                if( Session["EducationLevel_ListOfLanguages"]==null )
                {
                    Guid LanguageId = DictionaryTypeTools.Read("Языки").Id;
                    result = DictionaryTools.ReadAllByTypeId(LanguageId);
                    Session["EducationLevel_ListOfLanguages"] = result;
                    return result;
                }
                else
                {
                    result = (List<DTO.DictionaryItem4List>) Session["EducationLevel_ListOfLanguages"];
                }
                return result;
            }
        }

        protected RadDropDownList DdlLanguage { get{ return (RadDropDownList) wlbPanel.getInternalControl("DdlLanguage"); } }
        protected RadGrid MainGrid { get{ return (RadGrid) wlbPanel.getInternalControl("MainGrid"); } }


        protected void Page_Load(object sender, EventArgs e)
        {
            // если параметр не задан (задан некорректно) или пользователь еще не залогинен, то на страницу входа
            if (AccountEngine.IsCurrentUserLoggedIn == false)
            {
                Response.Redirect("~/Default.aspx");
                return;
            }
            
            if (IsPostBack == false)
            {
                Session["EducationLevel_ListOfLanguages"] = null;

                RefreshDDLInWindow();
                RefreshGrid();
            }
        }

        // обновим содержимое дроп даун листа на окне редактирования
        protected void RefreshDDLInWindow()
        {
            DdlLanguage.DataSource = ListOfLanguages;
            DdlLanguage.DataBind();
        }

        protected Guid GetCurrentLanguageId()
        {
            // получим ид текущего языка из выпадающего списка
            String langIdStr = DdlLanguage.SelectedValue;
            Guid langId;

            if (Guid.TryParse(langIdStr, out langId) == true)
            {
                return langId;
            }
            else
            {
                return Guid.Empty;
            }
        }

        protected void SemiUpdateGrid()
        {
            Guid langId = GetCurrentLanguageId();

            if (langId != Guid.Empty)
            {
                // грузим нужные данные из БД
                List<DTO.EducationLevel4List> data = EducationLevelTools.ReadAllByLaguageId(langId);

                // ребиндим их в грид
                MainGrid.DataSource = data;
            }
            else
            {
                MainGrid.DataSource = null;
            }
        }

        protected void RefreshGrid()
        {
            SemiUpdateGrid();
            MainGrid.DataBind();
        }

        protected void mainGrid_DeleteCommand(object sender, GridCommandEventArgs e)
        {
            Guid id = (Guid)((GridDataItem)e.Item).GetDataKeyValue("Id");
            
            try
            {
                EducationLevelTools.Delete(id);
            }
            catch
            {
                RadWindowManager1.RadAlert(
                    "При удалении возникли ошибки. Скорее всего это связанно с тем, что в БД есть сущности ссылающиеся на удаляемую. Удалите их и попробуйте снова.",
                    480, 180, "Ошибка при удалении уровня подготовки", null
                );
            }

            RefreshGrid();
        }

        protected void mainGrid_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            SemiUpdateGrid();
        }

        private Boolean IsNewFlag // используется, только во время редактирования
        {
            set
            {
                Session["EducationLevel_isNewFlag"] = value;
            }
            get
            {
                Boolean result = (Boolean)Session["EducationLevel_isNewFlag"];
                return result;
            }
        }
        private DTO.EducationLevel CurrItem // используется, только во время редактирования
        {
            set
            {
                Session["EducationLevel_CurrItem"] = value;
            }
            get
            {
                DTO.EducationLevel result = (DTO.EducationLevel)Session["EducationLevel_CurrItem"];
                return result;
            }
        }

        protected Int32 GetLanguageDDLIndexById(Guid LanguageId)
        {
            List<DTO.DictionaryItem4List> listOfLanguages = ListOfLanguages;
            IEnumerable<DTO.DictionaryItem4List> iEnumerableOfLanguages = listOfLanguages.AsEnumerable<DTO.DictionaryItem4List>();
            DTO.DictionaryItem4List currItem = iEnumerableOfLanguages.SingleOrDefault(x => x.Id == LanguageId);
            Int32 index = listOfLanguages.IndexOf(currItem);

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
                RadWindow1.Title = "Добавление нового уровня образования";

                RadTextBox             tbxName = (RadTextBox)RadWindow1.ContentContainer.FindControl("tbxName");
                RadTextBox      tbxDescription = (RadTextBox)RadWindow1.ContentContainer.FindControl("tbxDescription");
                RadNumericTextBox ntbxPosition = (RadNumericTextBox)RadWindow1.ContentContainer.FindControl("ntbxPosition");
                RadNumericTextBox    ntbxScore = (RadNumericTextBox)RadWindow1.ContentContainer.FindControl("ntbxScore");

                tbxName.Text = "";
                tbxDescription.Text = "";
                ntbxPosition.Text = "";
                ntbxScore.Text = "";
            }
            else
            {
                RadWindow1.Title = "Редактирование уровня образования";

                GridDataItem item = (GridDataItem)e.Item;
                Guid currEditedItemId = (Guid)item.GetDataKeyValue("Id");
                CurrItem = EducationLevelTools.Read(currEditedItemId);

                RadTextBox tbxName = (RadTextBox)RadWindow1.ContentContainer.FindControl("tbxName");
                RadTextBox tbxDescription = (RadTextBox)RadWindow1.ContentContainer.FindControl("tbxDescription");
                RadNumericTextBox ntbxPosition = (RadNumericTextBox)RadWindow1.ContentContainer.FindControl("ntbxPosition");
                RadNumericTextBox ntbxScore = (RadNumericTextBox)RadWindow1.ContentContainer.FindControl("ntbxScore");

                tbxName.Text = CurrItem.Name;
                tbxDescription.Text = CurrItem.Description;
                ntbxPosition.Text = CurrItem.Position.ToString();
                ntbxScore.Text = CurrItem.MaxScore.ToString();
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
            RadWindow1.Height = 220;

            pnlValidation.Visible = false;
            lblValidationText.Text = "";
        }
        protected void ShowValidatorsMessage(String msg)
        {
            tdValidationArea.Style["padding"] = "5px";
            tdValidationArea.Style["height"] = "40px";
            RadWindow1.Height = 270;

            pnlValidation.Visible = true;
            lblValidationText.Text = msg;
        }

        protected Boolean IsEnteredDataValid()
        {
            RadTextBox tbxName = (RadTextBox)RadWindow1.ContentContainer.FindControl("tbxName");

            // проверим заполнено ли поле с названием
            if (String.IsNullOrWhiteSpace(tbxName.Text))
            {
                ShowValidatorsMessage("Для сохранения поле \"Название\" должно быть заполнено.");
                return false;
            }

            // проверим существует ли запись с таким именем в текущем словаре (но не текущая)
            Guid currId = IsNewFlag ? Guid.Empty : CurrItem.Id;
            if (EducationLevelTools.IsNameExists(currId, tbxName.Text) == true)
            {
                ShowValidatorsMessage("Значение поля \"Название\" должно быть уникальным. Такое название уже есть в БД.");
                return false;
            }

            // проверим заполнено ли поле с позицией
            if (String.IsNullOrWhiteSpace(ntbxPosition.Text))
            {
                ShowValidatorsMessage("Для сохранения поля \"Позиция\" должно быть заполнено.");
                return false;
            }

            // проверим заполнено ли поле Баллы (max)
            if (String.IsNullOrWhiteSpace(ntbxScore.Text))
            {
                ShowValidatorsMessage("Для сохранения поля \"Баллы (max)\" должно быть заполнено.");
                return false;
            }   


            HideValidatorsMessage();
            return true; // все данные корректны
        }

        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            if (IsEnteredDataValid() == false) return;

            RadTextBox tbxName = (RadTextBox)RadWindow1.ContentContainer.FindControl("tbxName");
            RadTextBox tbxDescription = (RadTextBox)RadWindow1.ContentContainer.FindControl("tbxDescription");
            RadNumericTextBox ntbxPosition = (RadNumericTextBox)RadWindow1.ContentContainer.FindControl("ntbxPosition");
            RadNumericTextBox ntbxScore = (RadNumericTextBox)RadWindow1.ContentContainer.FindControl("ntbxScore");

            Byte position;
            Int32 score;

            Byte.TryParse(ntbxPosition.Text, out position);
            Int32.TryParse(ntbxScore.Text, out score);

            Guid languageId;
            Guid.TryParse(DdlLanguage.SelectedValue, out languageId);

            int minScore = EducationLevelTools.GetMinimumScoreByPositionAndMaxScore(languageId, position, score);

            if (IsNewFlag == true)
            {
                DTO.EducationLevel newUserItem = new DTO.EducationLevel();

                newUserItem.Name = tbxName.Text;
                newUserItem.Description = tbxDescription.Text;
                newUserItem.Position = position;
                newUserItem.MinScore = minScore;
                newUserItem.MaxScore = score;
                newUserItem.Language = languageId;

                EducationLevelTools.Create(newUserItem);
            }
            else
            {
                CurrItem.Name = tbxName.Text;
                CurrItem.Description = tbxDescription.Text;
                CurrItem.Position = position;
                CurrItem.MinScore = minScore;
                CurrItem.MaxScore = score;
                CurrItem.Language = languageId;

                EducationLevelTools.Update(CurrItem);
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

        protected void ddlLanguage_ItemSelected(object sender, DropDownListEventArgs e)
        {
            SemiUpdateGrid();
            MainGrid.DataBind();
        }
    }
}