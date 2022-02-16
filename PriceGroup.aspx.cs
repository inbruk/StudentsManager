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
    public partial class PriceGroup : System.Web.UI.Page
    {
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
                RefreshGrid();
            }
        }

        protected void SemiUpdateGrid()
        {
            // грузим нужные данные из БД
            List<DTO.PriceGroup> data = PriceGroupTools.ReadAll();

            // ребиндим их в грид
            MainGrid.DataSource = data;
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
                PriceGroupTools.Delete(id);
            }
            catch
            {
                RadWindowManager1.RadAlert(
                    "При удалении возникли ошибки. Скорее всего это связанно с тем, что в БД есть сущности ссылающиеся на удаляемую. Удалите их и попробуйте снова.",
                    480, 180, "Ошибка при удалении ценовой группы", null
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
                Session["PriceGroup_isNewFlag"] = value;
            }
            get
            {
                Boolean result = (Boolean)Session["PriceGroup_isNewFlag"];
                return result;
            }
        }
        private DTO.PriceGroup CurrItem // используется, только во время редактирования
        {
            set
            {
                Session["PriceGroup_CurrItem"] = value;
            }
            get
            {
                DTO.PriceGroup result = (DTO.PriceGroup)Session["PriceGroup_CurrItem"];
                return result;
            }
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
                RadWindow1.Title = "Добавление новой ценовой группы";

                RadTextBox             tbxName        = (RadTextBox)RadWindow1.ContentContainer.FindControl("tbxName");
                RadTextBox      tbxDescription        = (RadTextBox)RadWindow1.ContentContainer.FindControl("tbxDescription");
                RadNumericTextBox ntbxPosition        = (RadNumericTextBox)RadWindow1.ContentContainer.FindControl("ntbxPosition");
                RadNumericTextBox ntbxIndexInPercents = (RadNumericTextBox)RadWindow1.ContentContainer.FindControl("ntbxIndexInPercents");

                tbxName.Text = "";
                tbxDescription.Text = "";
                ntbxPosition.Text = "";
                ntbxIndexInPercents.Text = "";
            }
            else
            {
                RadWindow1.Title = "Редактирование ценовой группы";

                GridDataItem item = (GridDataItem)e.Item;
                Guid currEditedItemId = (Guid)item.GetDataKeyValue("Id");
                CurrItem = PriceGroupTools.Read(currEditedItemId);

                RadTextBox tbxName = (RadTextBox)RadWindow1.ContentContainer.FindControl("tbxName");
                RadTextBox tbxDescription = (RadTextBox)RadWindow1.ContentContainer.FindControl("tbxDescription");
                RadNumericTextBox ntbxPosition = (RadNumericTextBox)RadWindow1.ContentContainer.FindControl("ntbxPosition");
                RadNumericTextBox ntbxIndexInPercents = (RadNumericTextBox)RadWindow1.ContentContainer.FindControl("ntbxIndexInPercents");

                tbxName.Text = CurrItem.Name;
                tbxDescription.Text = CurrItem.Description;
                ntbxPosition.Text = CurrItem.Position.ToString();
                ntbxIndexInPercents.Text = CurrItem.IndexInPercents.ToString();
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
            RadWindow1.Height = 230;

            pnlValidation.Visible = false;
            lblValidationText.Text = "";
        }
        protected void ShowValidatorsMessage(String msg)
        {
            tdValidationArea.Style["padding"] = "5px";
            tdValidationArea.Style["height"] = "40px";
            RadWindow1.Height = 280;

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
            if (PriceGroupTools.IsNameExists(currId, tbxName.Text) == true)
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

            // проверим заполнено ли поле "Коэффициент в %"
            if (String.IsNullOrWhiteSpace(ntbxIndexInPercents.Text))
            {
                ShowValidatorsMessage("Для сохранения поля \"Коэффициент в %\" должно быть заполнено.");
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
            RadNumericTextBox ntbxIndexInPercents = (RadNumericTextBox)RadWindow1.ContentContainer.FindControl("ntbxIndexInPercents");

            Byte position;
            Int32 indexInPercents;

            Byte.TryParse(ntbxPosition.Text, out position);
            Int32.TryParse(ntbxIndexInPercents.Text, out indexInPercents);

            if (IsNewFlag == true)
            {
                DTO.PriceGroup newUserItem = new DTO.PriceGroup();

                newUserItem.Name = tbxName.Text;
                newUserItem.Description = tbxDescription.Text;
                newUserItem.Position = position;
                newUserItem.IndexInPercents = indexInPercents;

                PriceGroupTools.Create(newUserItem);
            }
            else
            {
                CurrItem.Name = tbxName.Text;
                CurrItem.Description = tbxDescription.Text;
                CurrItem.Position = position;
                CurrItem.IndexInPercents = indexInPercents;

                PriceGroupTools.Update(CurrItem);
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