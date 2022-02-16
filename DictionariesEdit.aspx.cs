using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

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
    public partial class DictionariesEdit : System.Web.UI.Page
    {
        private Guid _currDictionaryType;

        private DTO.DictionaryType _currDictionaryTypeItem = null;
        private DTO.DictionaryType CurrDictionaryTypeItem
        {
            set
            {
                _currDictionaryTypeItem = value;
                Session["DictionariesEdit_CurrDictionaryType"] = value;
            }
            get
            {
                if( _currDictionaryTypeItem==null )
                {
                    _currDictionaryTypeItem = (DTO.DictionaryType) Session["DictionariesEdit_CurrDictionaryType"];
                }

                DTO.DictionaryType result = _currDictionaryTypeItem;
                return result;
            }
        }

        protected RadGrid MainGrid { get { return (RadGrid)wlbPanel.getInternalControl("MainGrid"); } }

        protected void GetCurrentDictionaryType()
        {
            // вытаскиваем из параметра тип словаря для редактирования
            String paramStr = (String)Request["DictionaryType"];
            if (paramStr != null)
            {
                if (Guid.TryParse(paramStr, out _currDictionaryType) == false)
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

            // загрузим полностью запись типа словаря, чтобы получить название
            CurrDictionaryTypeItem = DictionaryTypeTools.Read( _currDictionaryType );
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
            GetCurrentDictionaryType();

            if (CurrDictionaryTypeItem != null)
            {
                wlbPanel.Title = CurrDictionaryTypeItem.Name;
            }

            if (IsPostBack == false)
            {
                RefreshGrid();
            }
        }

        protected void RefreshGrid()
        {
            // грузим нужные данные из БД
            List<DTO.DictionaryItem4List> data = DictionaryTools.ReadAllByTypeId(_currDictionaryType);

            // ребиндим их в грид
            MainGrid.DataSource = data;
            MainGrid.DataBind();
        }

        protected void mainGrid_DeleteCommand(object sender, GridCommandEventArgs e)
        {
            Guid id = (Guid)((GridDataItem)e.Item).GetDataKeyValue("Id");

            try
            {
                DictionaryTools.Delete(id);
            }
            catch
            {
                RadWindowManager1.RadAlert(
                    "При удалении возникли ошибки. Скорее всего это связанно с тем, что в БД есть сущности ссылающиеся на удаляемую. Удалите их и попробуйте снова.", 
                    480, 180, "Ошибка при удалении записи из словаря", null
                );
            }

            RefreshGrid();
        }

        protected void mainGrid_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)       
        {
            // грузим нужные данные из БД
            List<DTO.DictionaryItem4List> data = DictionaryTools.ReadAllByTypeId(_currDictionaryType);

            // ребиндим их в грид
            MainGrid.DataSource = data;
        }

        private Boolean IsNewFlag // используется, только во время редактирования
        {
            set
            {
                Session["DictionariesEdit_isNewFlag"] = value;
            }
            get
            {
                Boolean result = (Boolean)Session["DictionariesEdit_isNewFlag"];
                return result;
            }
        }
        private DTO.DictionaryItem CurrItem // используется, только во время редактирования
        {
            set 
            {
                Session["DictionariesEdit_CurrItem"] = value;
            }
            get
            {
                DTO.DictionaryItem result = (DTO.DictionaryItem)Session["DictionariesEdit_CurrItem"];
                return result;
            }
        }

        protected void mainGrid_ItemCommand(object sender, GridCommandEventArgs e)
        {          
            switch( e.CommandName )
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
                RadWindow1.Title = "Добавление новой записи";

                TextBox tbxName = (TextBox)RadWindow1.ContentContainer.FindControl("tbxName");
                TextBox tbxDescription = (TextBox)RadWindow1.ContentContainer.FindControl("tbxDescription");

                tbxName.Text = "";
                tbxDescription.Text = "";
            }
            else
            {
                RadWindow1.Title = "Редактирование записи";

                GridDataItem item = (GridDataItem)e.Item;
                Guid currEditedItemId = (Guid)item.GetDataKeyValue("Id");
                CurrItem = DictionaryTools.Read(currEditedItemId);

                TextBox tbxName = (TextBox)RadWindow1.ContentContainer.FindControl("tbxName");
                TextBox tbxDescription = (TextBox)RadWindow1.ContentContainer.FindControl("tbxDescription");

                tbxName.Text = CurrItem.Name;
                tbxDescription.Text = CurrItem.Description;
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
            tdValidationArea.Style["height"] = "40px";
            RadWindow1.Height = 350;

            pnlValidation.Visible = true;
            lblValidationText.Text = msg;
        }

        protected Boolean IsEnteredDataValid()
        {
            TextBox tbxName = (TextBox)RadWindow1.ContentContainer.FindControl("tbxName");

            // проверим заполнено ли поле с названием
            if (String.IsNullOrWhiteSpace(tbxName.Text))
            {
                ShowValidatorsMessage("Для сохранения поле \"Название\" должно быть заполнено.");
                return false;
            }

            // проверим существует ли запись с таким именем в текущем словаре (но не текущая)
            Guid currId = IsNewFlag ? Guid.Empty : CurrItem.Id;
            if (DictionaryTools.IsNameExists(currId, tbxName.Text) == true)
            {
                ShowValidatorsMessage("Значение поля \"Название\" должно быть уникальным. Такое название уже есть в БД.");
                return false;
            }

            HideValidatorsMessage();
            return true; // все данные корректны
        }

        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            if ( IsEnteredDataValid() == false ) return;

            TextBox tbxName = (TextBox)RadWindow1.ContentContainer.FindControl("tbxName");
            TextBox tbxDescription = (TextBox)RadWindow1.ContentContainer.FindControl("tbxDescription");

            if (IsNewFlag == true)
            {
                DTO.DictionaryItem newDicItem = new DTO.DictionaryItem();

                newDicItem.Name = tbxName.Text;
                newDicItem.Description = tbxDescription.Text;
                newDicItem.DictionaryType = _currDictionaryType;

                DictionaryTools.Create(newDicItem);
            }
            else
            {
                CurrItem.Name = tbxName.Text;
                CurrItem.Description = tbxDescription.Text;
                CurrItem.DictionaryType = _currDictionaryType;

                DictionaryTools.Update(CurrItem);
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