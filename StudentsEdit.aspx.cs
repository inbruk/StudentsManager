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
    public partial class StudentEdit : System.Web.UI.Page
    {
        // фильтры ------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        
        protected RadDropDownList _ddlFilterLanguage { get { return (RadDropDownList)wlbPanel.getInternalControl("ddlFilterLanguage"); } }
        protected RadDropDownList _ddlFormLanguage1 { get { return (RadDropDownList)wblEditorPanel.getInternalControl("ddlFormLanguage1"); } }
        protected RadDropDownList _ddlFormLanguage2 { get { return (RadDropDownList)wblEditorPanel.getInternalControl("ddlFormLanguage2"); } }
        protected RadDropDownList _ddlFormLanguage3 { get { return (RadDropDownList)wblEditorPanel.getInternalControl("ddlFormLanguage3"); } }

        protected List<DTO.DictionaryItem4List> GetListOfLanguages()
        {
            Guid languageId = DictionaryTypeTools.Read("Языки").Id;
            List<DTO.DictionaryItem4List> listOfLanguages = DictionaryTools.ReadAllByTypeId(languageId);
            listOfLanguages.Insert(0, new DTO.DictionaryItem4List() { Id = Guid.Empty, Name = "Любой" });
            return listOfLanguages;
        }

        protected void RefreshDdlLanguages()
        {
            List<DTO.DictionaryItem4List> listOfLanguages = GetListOfLanguages();

            _ddlFilterLanguage.DataSource = listOfLanguages;
            _ddlFilterLanguage.DataBind();
            _ddlFilterLanguage.SelectedIndex = 0;

            _ddlFormLanguage1.DataSource = listOfLanguages;
            _ddlFormLanguage1.DataBind();
            _ddlFormLanguage1.SelectedIndex = 0;

            _ddlFormLanguage2.DataSource = listOfLanguages;
            _ddlFormLanguage2.DataBind();
            _ddlFormLanguage2.SelectedIndex = 0;

            _ddlFormLanguage3.DataSource = listOfLanguages;
            _ddlFormLanguage3.DataBind();
            _ddlFormLanguage3.SelectedIndex = 0;
        }

        protected List<DTO.EducationLevel4List> GetListOfEducationLevels(Guid langId)
        {
            List<DTO.EducationLevel4List> result;

            if (langId != Guid.Empty)
            {
                if (Session["StudentEdit_EducationLevels"] == null)
                {
                    result = EducationLevelTools.ReadAll();
                    Session["StudentEdit_EducationLevels"] = result;
                }
                else
                {
                    result = (List<DTO.EducationLevel4List>)Session["StudentEdit_EducationLevels"];
                }

                result = result.ToArray().Where(x => x.LanguageId == langId).ToList();
            }
            else
            {
                result = new List<DTO.EducationLevel4List>();
            }

            result.Insert(0, new DTO.EducationLevel4List() { Id = Guid.Empty, Name = "Любой" });

            return result;
        }

        protected RadDropDownList _ddlFilterEducationLevel { get { return (RadDropDownList)wlbPanel.getInternalControl("ddlFilterEducationLevel"); } }               
        
        protected Guid GetCurrentDropDownListId(RadDropDownList currDdlId)
        {
            // получим ид из выпадающего списка
            String langIdStr = currDdlId.SelectedValue;
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

        protected void Refresh_ddlEducationLevelChained(RadDropDownList currDdlLanguage, RadDropDownList currEducationLevelDdl)
        {
            Guid langId = GetCurrentDropDownListId(currDdlLanguage);
            List<DTO.EducationLevel4List> edLevels = GetListOfEducationLevels(langId);
            currEducationLevelDdl.DataSource = edLevels;
            currEducationLevelDdl.DataBind();
            currEducationLevelDdl.SelectedIndex = 0;
        }

        protected RadDropDownList _ddlFilterGroup { get { return (RadDropDownList)wlbPanel.getInternalControl("ddlFilterGroup"); } }
        protected RadDropDownList _ddlFormGroup1 { get { return (RadDropDownList)wblEditorPanel.getInternalControl("ddlFormGroup1"); } }
        protected RadDropDownList _ddlFormGroup2 { get { return (RadDropDownList)wblEditorPanel.getInternalControl("ddlFormGroup2"); } }
        protected RadDropDownList _ddlFormGroup3 { get { return (RadDropDownList)wblEditorPanel.getInternalControl("ddlFormGroup3"); } }

        protected List<DTO.Group4List> GetListOfGroups(Guid edLevelId)
        {
            List<DTO.Group4List> result;

            if (edLevelId != Guid.Empty)
            {
                if (Session["StudentEdit_Groups"] == null)
                {
                    result = GroupTools.ReadAllByEducationLevelId(edLevelId);
                    Session["StudentEdit_Groups"] = result;
                }
                else
                {
                    result = (List<DTO.Group4List>)Session["StudentEdit_Groups"];
                }

                result = result.ToArray().Where(x => x.EducationLevelId == edLevelId).ToList();
            }
            else
            {
                result = new List<DTO.Group4List>();
            }

            result.Insert(0, new DTO.Group4List() { Id = Guid.Empty, Name = "Любая" });

            return result;
        }


        protected void Refresh_ddlGroupByEducationLevelId(Guid currEducationLevelId, RadDropDownList currDdlGroup)
        {
            List<DTO.Group4List> edLevels = GetListOfGroups(currEducationLevelId);
            currDdlGroup.DataSource = edLevels;
            currDdlGroup.DataBind();
            currDdlGroup.SelectedIndex = 0;
        }

        protected void Refresh_ddlGroupChained(RadDropDownList currDdlEducationLevel, RadDropDownList currDdlGroup)
        {
            Guid edLevelId = GetCurrentDropDownListId(currDdlEducationLevel);
            Refresh_ddlGroupByEducationLevelId(edLevelId, currDdlGroup);
        }

        // возвращаемое значение это currEdLevelId
        protected Guid Refresh_ddlEducationLevelByLanguageAndScore(RadDropDownList currDdlLanguage, RadNumericTextBox ntbxCurrentScore, RadDropDownList currDdlGroup)
        {
            Guid langId = GetCurrentDropDownListId(currDdlLanguage);
            Int32 score;
            if ( Int32.TryParse(ntbxCurrentScore.Text, out score) == false )
            {
                score = 0;
            }

            Guid currEdLevelId = Guid.Empty; 
            DTO.EducationLevel el = EducationLevelTools.ReadByLanguageIdAndScore(langId, score);
            if( el!=null )
            {
                currEdLevelId = el.Id;
            }

            Refresh_ddlGroupByEducationLevelId(currEdLevelId, currDdlGroup);
            return currEdLevelId;
        }


        protected List<DTO.PriceGroup> GetListOfPriceGroups()
        {
            List<DTO.PriceGroup> result;

            if (Session["StudentEdit_PriceGroups"] == null)
            {
                result = PriceGroupTools.ReadAll();
                Session["StudentEdit_PriceGroups"] = result;
            }
            else
            {
                result = (List<DTO.PriceGroup>)Session["StudentEdit_PriceGroups"];
            }

            return result;
        }

        protected void Refresh_PriceGroupsDdl()
        {
            List<DTO.PriceGroup> currList = GetListOfPriceGroups();

            ddlPriceGroup.DataSource = currList;
            ddlPriceGroup.DataBind();
            ddlPriceGroup.SelectedIndex = 1;

            List<DTO.PriceGroup> currListExt = currList.Select(x => x).ToList(); // надо клонировать, чтобы не испортить оригинал
            currListExt.Insert(0, new DTO.PriceGroup() { Id = Guid.Empty, Name = "Любая" });

            ddlFilterPriceGroup.DataSource = currListExt;
            ddlFilterPriceGroup.DataBind();
            ddlFilterPriceGroup.SelectedIndex = 0;
        }

        protected void ddlFilterLanguage_ItemSelected(object sender, DropDownListEventArgs e)
        {
            Refresh_ddlEducationLevelChained(_ddlFilterLanguage, _ddlFilterEducationLevel);
        }

        protected void ddlFormLanguage1_ItemSelected(object sender, DropDownListEventArgs e)
        {
            Refresh_ddlEducationLevelByLanguageAndScore(ddlFormLanguage1, ntbxFormCurrentScore1, ddlFormGroup1);
        }

        protected void ddlFormLanguage2_ItemSelected(object sender, DropDownListEventArgs e)
        {
            Refresh_ddlEducationLevelByLanguageAndScore(ddlFormLanguage2, ntbxFormCurrentScore2, ddlFormGroup2);
        }

        protected void ddlFormLanguage3_ItemSelected(object sender, DropDownListEventArgs e)
        {
            Refresh_ddlEducationLevelByLanguageAndScore(ddlFormLanguage3, ntbxFormCurrentScore3, ddlFormGroup3);
        }

        protected void ddlFilterEducationLevel_ItemSelected(object sender, DropDownListEventArgs e)
        {
            Refresh_ddlGroupChained(_ddlFilterEducationLevel, _ddlFilterGroup);
        }

        protected void ntbxCurrentScore1_ChildrenCreated(object sender, EventArgs e)
        {
            Refresh_ddlEducationLevelByLanguageAndScore(ddlFormLanguage1, ntbxFormCurrentScore1, ddlFormGroup1);
        }

        protected void ntbxCurrentScore2_TextChanged(object sender, EventArgs e)
        {
            Refresh_ddlEducationLevelByLanguageAndScore(ddlFormLanguage2, ntbxFormCurrentScore2, ddlFormGroup2);
        }

        protected void ntbxCurrentScore3_TextChanged(object sender, EventArgs e)
        {
            Refresh_ddlEducationLevelByLanguageAndScore(ddlFormLanguage3, ntbxFormCurrentScore3, ddlFormGroup3);
        }

        // редактирование записи ---------------------------------------------------------------------------------------------------------------------------------------------------------------------

        protected Int32 GetLanguagesIndexById(Guid LanguageId)
        {
            List<DTO.DictionaryItem4List> listOfLanguage = GetListOfLanguages();
            IEnumerable<DTO.DictionaryItem4List> iEnumerableOfLanguage = listOfLanguage.AsEnumerable<DTO.DictionaryItem4List>();
            DTO.DictionaryItem4List currItem = iEnumerableOfLanguage.SingleOrDefault(x => x.Id == LanguageId);
            Int32 index = listOfLanguage.IndexOf(currItem);

            return index;
        }

        protected Int32 GetEducationLevelsIndexById(Guid langId, Guid EducationLevelId)
        {
            List<DTO.EducationLevel4List> listOfEducationLevel = GetListOfEducationLevels(langId);
            IEnumerable<DTO.EducationLevel4List> iEnumerableOfEducationLevel = listOfEducationLevel.AsEnumerable<DTO.EducationLevel4List>();
            DTO.EducationLevel4List currItem = iEnumerableOfEducationLevel.SingleOrDefault(x => x.Id == EducationLevelId);
            Int32 index = listOfEducationLevel.IndexOf(currItem);

            return index;
        }


        protected Int32 GetGroupsIndexById(Guid edLevelId, Guid GroupId)
        {
            List<DTO.Group4List> listOfGroup = GetListOfGroups(edLevelId);
            IEnumerable<DTO.Group4List> iEnumerableOfGroup = listOfGroup.AsEnumerable<DTO.Group4List>();
            DTO.Group4List currItem = iEnumerableOfGroup.SingleOrDefault(x => x.Id == GroupId);
            Int32 index = listOfGroup.IndexOf(currItem);

            return index;
        }

        protected Int32 GetPriceGroupsIndexById(Guid PriceGroupId)
        {
            List<DTO.PriceGroup> listOfPriceGroup = GetListOfPriceGroups();
            IEnumerable<DTO.PriceGroup> iEnumerableOfPriceGroup = listOfPriceGroup.AsEnumerable<DTO.PriceGroup>();
            DTO.PriceGroup currItem = iEnumerableOfPriceGroup.SingleOrDefault(x => x.Id == PriceGroupId);
            Int32 index = listOfPriceGroup.IndexOf(currItem);

            return index;
        }

        protected void mainGrid_SelectedIndexChanged(object sender, EventArgs e)
        {
            String selectedIndexStr = mainGrid.SelectedIndexes[0];
            Int32 selectedIndex = Int32.Parse(selectedIndexStr);

            selectedIndex = selectedIndex + mainGrid.CurrentPageIndex * 13; // для учета страницы, внимание сейчас 13 строк !!!


            DTO.Student4List curr4ListItem = _gridDataStorage[selectedIndex];

            CurrItem = curr4ListItem.GetStudentData(); // это нужно для сохранения, если нужно будет
            
            wblEditorPanel.SubControlsEnabled = true;
            IsNewFlag = false;
            
            tbxName.Text = curr4ListItem.Name;
            tbxSurname.Text = curr4ListItem.Surname;
            tbxPatronomic.Text = curr4ListItem.Patronomic;
            tbxSurnameAndInitials.Text = curr4ListItem.SurnameAndInitials;
            
            tbxPhone.Text = curr4ListItem.Phone;
            tbxEMail.Text = curr4ListItem.Email;
            
            ddlPriceGroup.SelectedIndex = GetPriceGroupsIndexById(curr4ListItem.PriceGroupId);

            // language 1 --------------

            if( curr4ListItem.Language1Id!=null && curr4ListItem.Language1Id!=Guid.Empty )
            {
                ddlFormLanguage1.SelectedIndex = GetLanguagesIndexById( (Guid) curr4ListItem.Language1Id );
            }
            else
            {
                ddlFormLanguage1.SelectedIndex = 0;
            }

            Int32 currScore1 = curr4ListItem.CurrentScore1!=null ? (Int32)curr4ListItem.CurrentScore1 : 0;
            ntbxFormCurrentScore1.Text = currScore1.ToString();

            Int32 firstTestScore1 = curr4ListItem.FirstTestScore1!=null ? (Int32)curr4ListItem.FirstTestScore1 : 0;
            ntbxFormFirstTestScore1.Text = firstTestScore1.ToString();

            if (curr4ListItem.Language1Id != null && curr4ListItem.Language1Id != Guid.Empty && curr4ListItem.Group1Id!=null)
            {
                Guid currEdLevel1Id = Refresh_ddlEducationLevelByLanguageAndScore(ddlFormLanguage1, ntbxFormCurrentScore1, ddlFormGroup1);
                ddlFormGroup1.SelectedIndex = GetGroupsIndexById(currEdLevel1Id, (Guid) curr4ListItem.Group1Id);
            }
            else
            {
                ddlFormGroup1.SelectedIndex = 0;
            }

            // language 2 --------------

            if( curr4ListItem.Language2Id!=null && curr4ListItem.Language2Id!=Guid.Empty )
            {
                ddlFormLanguage2.SelectedIndex = GetLanguagesIndexById( (Guid) curr4ListItem.Language2Id );
            }
            else
            {
                ddlFormLanguage2.SelectedIndex = 0;
            }

            Int32 currScore2 = curr4ListItem.CurrentScore2!=null ? (Int32)curr4ListItem.CurrentScore2 : 0;
            ntbxFormCurrentScore2.Text = currScore2.ToString();

            Int32 firstTestScore2 = curr4ListItem.FirstTestScore2!=null ? (Int32)curr4ListItem.FirstTestScore2 : 0;
            ntbxFormFirstTestScore2.Text = firstTestScore2.ToString();

            if (curr4ListItem.Language2Id != null && curr4ListItem.Language2Id != Guid.Empty && curr4ListItem.Group2Id != null)
            {
                Guid currEdLevel2Id = Refresh_ddlEducationLevelByLanguageAndScore(ddlFormLanguage2, ntbxFormCurrentScore2, ddlFormGroup2);
                ddlFormGroup2.SelectedIndex = GetGroupsIndexById(currEdLevel2Id, (Guid)curr4ListItem.Group2Id);
            }
            else
            {
                ddlFormGroup2.SelectedIndex = 0;
            }

            // language 3 --------------

            if( curr4ListItem.Language3Id!=null && curr4ListItem.Language3Id!=Guid.Empty )
            {
                ddlFormLanguage3.SelectedIndex = GetLanguagesIndexById( (Guid) curr4ListItem.Language3Id );
            }
            else
            {
                ddlFormLanguage3.SelectedIndex = 0;
            }

            Int32 currScore3 = curr4ListItem.CurrentScore3!=null ? (Int32)curr4ListItem.CurrentScore3 : 0;
            ntbxFormCurrentScore3.Text = currScore3.ToString();

            Int32 firstTestScore3 = curr4ListItem.FirstTestScore3!=null ? (Int32)curr4ListItem.FirstTestScore3 : 0;
            ntbxFormFirstTestScore3.Text = firstTestScore3.ToString();

            if (curr4ListItem.Language3Id != null && curr4ListItem.Language3Id != Guid.Empty && curr4ListItem.Group3Id != null)
            {
                Guid currEdLevel3Id = Refresh_ddlEducationLevelByLanguageAndScore(ddlFormLanguage3, ntbxFormCurrentScore3, ddlFormGroup3);
                ddlFormGroup3.SelectedIndex = GetGroupsIndexById(currEdLevel3Id, (Guid)curr4ListItem.Group3Id);
            }
            else
            {
                ddlFormGroup3.SelectedIndex = 0;
            }        
        }

        protected void lbtnAddNew_Click(object sender, EventArgs e)
        {
            mainGrid.SelectedIndexes.Clear();

            wblEditorPanel.SubControlsEnabled = true;
            IsNewFlag = true;
            wblEditorPanel.Title = "Создание нового студента";

            tbxName.Text = "";
            tbxSurname.Text = "";
            tbxPatronomic.Text = "";
            tbxSurnameAndInitials.Text = "";
            ddlPriceGroup.SelectedIndex = 1;

            tbxPhone.Text = "";
            tbxEMail.Text = "";

            ddlFormLanguage1.SelectedIndex = 0;
            ddlFormGroup1.SelectedIndex = 0;
            ntbxFormCurrentScore1.Text = "0";
            ntbxFormFirstTestScore1.Text = "0";

            ddlFormLanguage2.SelectedIndex = 0;
            ddlFormGroup2.SelectedIndex = 0;
            ntbxFormCurrentScore2.Text = "0";
            ntbxFormFirstTestScore2.Text = "0";

            ddlFormLanguage3.SelectedIndex = 0;
            ddlFormGroup3.SelectedIndex = 0;
            ntbxFormCurrentScore3.Text = "0";
            ntbxFormFirstTestScore3.Text = "0";
        }
        
        protected void lbtnDeleteCurrent_Click(object sender, EventArgs e)
        {
            if( mainGrid.SelectedIndexes.Count>0 )
            {
                RadWindowManager1.RadConfirm("А вы уверены в том, что хотите удалить выбранного студента ?", "confirmDeleteCallbackFn", 500, 150, null, "Подтверждение");
            }
            else
            {
                RadWindowManager1.RadPrompt("Для удаления нужно сначала выбрать студента в таблице (кликнуть на него мышкой).", null, 500, 150, null,"Пояснение", null);
            }
        }

        protected void btnDeleteAction_Click(object sender, EventArgs e)
        {
            String selectedIndexStr = mainGrid.SelectedIndexes[0];
            Int32 selectedIndex = Int32.Parse(selectedIndexStr);
            DTO.Student4List curr4ListItem = _gridDataStorage[selectedIndex];
            Guid selectedStudentId = curr4ListItem.Id;

            StudentTools.Delete(selectedStudentId);
            
            wblEditorPanel.SubControlsEnabled = false;
            IsNewFlag = false;

            mainGrid.SelectedIndexes.Clear();
            RefreshGrid();
        }

        protected void btnCancelAction_Click(object sender, EventArgs e)
        {
            ;
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            wblEditorPanel.SubControlsEnabled = false;
            IsNewFlag = false;

            mainGrid.SelectedIndexes.Clear();
        }

        protected void lbtnRefresh_Click(object sender, EventArgs e)
        {
            wblEditorPanel.SubControlsEnabled = false;
            IsNewFlag = false;

            mainGrid.SelectedIndexes.Clear();
            RefreshGrid();
        }

        protected void lbtnClearFilters_Click(object sender, EventArgs e)
        {
            ddlFilterLanguage.SelectedIndex = 0;
            ddlFilterEducationLevel.SelectedIndex = 0;
            ddlFilterGroup.SelectedIndex = 0;
            ddlFilterPriceGroup.SelectedIndex = 0;
            tbxFilterName.Text = "";
            tbxFilterSurname.Text = "";

            wblEditorPanel.SubControlsEnabled = false;
            IsNewFlag = false;

            mainGrid.SelectedIndexes.Clear();
            RefreshGrid();
        }

        DTO.Student _currItem = null;
        private DTO.Student CurrItem // используется, только во время редактирования
        {
            set
            {
                Session["StudentEdit_CurrItem"] = _currItem = value;
            }
            get
            {
                if (_currItem == null)
                {
                    _currItem = (DTO.Student)Session["StudentEdit_CurrItem"];
                }
                return _currItem;
            }
        }

        Boolean? _isNewFlag = null;
        private Boolean IsNewFlag // используется, только во время редактирования
        {
            set
            {
                _isNewFlag = value;
                Session["StudentEdit_isNewFlag"] = value;
            }
            get
            {
                if (_isNewFlag == null)
                {
                    _isNewFlag = (Boolean)Session["StudentEdit_isNewFlag"];
                }
                return (Boolean)_isNewFlag;
            }
        }
        
        protected void btnSave_Click(object sender, EventArgs e)
        {
            // перед тем как сохранять проверим валидацию
            if( IsEnteredDataValid()==false ) return;

            DTO.Student editItem;
            if( IsNewFlag==true )
            {
                editItem = new DTO.Student();
                editItem.Id = Guid.NewGuid();
            }
            else
            {
                editItem = CurrItem; // тут должно быть другое !!!
            }

            Guid PriceGroupId;
            Guid.TryParse(ddlPriceGroup.SelectedValue, out PriceGroupId);

            Guid Language1Id;
            Guid.TryParse(ddlFormLanguage1.SelectedValue, out Language1Id);

            Guid Language2Id;
            Guid.TryParse(ddlFormLanguage2.SelectedValue, out Language2Id);

            Guid Language3Id;
            Guid.TryParse(ddlFormLanguage3.SelectedValue, out Language3Id);

            Guid Group1Id;
            Guid.TryParse(ddlFormGroup1.SelectedValue, out Group1Id);

            Guid Group2Id;
            Guid.TryParse(ddlFormGroup2.SelectedValue, out Group2Id);

            Guid Group3Id;
            Guid.TryParse(ddlFormGroup3.SelectedValue, out Group3Id);
            
            Int32 CurrentScore1Id;
            Int32.TryParse(ntbxFormCurrentScore1.Text, out CurrentScore1Id);
            
            Int32 CurrentScore2Id;
            Int32.TryParse(ntbxFormCurrentScore2.Text, out CurrentScore2Id);            
            
            Int32 CurrentScore3Id;
            Int32.TryParse(ntbxFormCurrentScore3.Text, out CurrentScore3Id);

            Int32 FirstTestScore1Id;
            Int32.TryParse(ntbxFormFirstTestScore1.Text, out FirstTestScore1Id);
            
            Int32 FirstTestScore2Id;
            Int32.TryParse(ntbxFormFirstTestScore2.Text, out FirstTestScore2Id);            
            
            Int32 FirstTestScore3Id;
            Int32.TryParse(ntbxFormFirstTestScore3.Text, out FirstTestScore3Id);            

            editItem.Name = tbxName.Text;
            editItem.Surname = tbxSurname.Text;
            editItem.Patronomic = tbxPatronomic.Text;
            editItem.SurnameAndInitials = tbxSurnameAndInitials.Text;
            editItem.PriceGroup = PriceGroupId;

            editItem.Phone = tbxPhone.Text;
            editItem.Email = tbxEMail.Text;

            editItem.Language1 = Language1Id;
            editItem.Group1 = Group1Id;
            editItem.CurrentScore1 = CurrentScore1Id;
            editItem.FirstTestScore1 = FirstTestScore1Id;

            editItem.Language2 = Language2Id;
            editItem.Group2 = Group2Id;
            editItem.CurrentScore2 = CurrentScore2Id;
            editItem.FirstTestScore2 = FirstTestScore2Id;

            editItem.Language3 = Language3Id;
            editItem.Group3 = Group3Id;
            editItem.CurrentScore3 = CurrentScore3Id;
            editItem.FirstTestScore3 = FirstTestScore3Id;

            if (IsNewFlag == true)
            {
                StudentTools.Create(editItem);
            }
            else
            {
                StudentTools.Update(editItem);
            }
            
            wblEditorPanel.SubControlsEnabled = false;
            IsNewFlag = false;

            RefreshGrid();
        }

        // валидация при редактировании ---------------------------------------------------------------------------------------------------------------------------------------------------------------

        protected void HideValidatorsMessage()
        {
            pnlValidation.Visible = false;
            lblValidationText.Text = "";
        }
        protected void ShowValidatorsMessage(String msg)
        {
            pnlValidation.Visible = true;
            lblValidationText.Text = msg;
        }

        protected Boolean IsEnteredDataValid()
        {
            String errorMessage = null;

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

            // проверим существует ли студент с таким именем/фамилией
            Guid currId = IsNewFlag ? Guid.Empty : CurrItem.Id;
            if ( StudentTools.IsNameSurnameAndPatronomicExists(currId, tbxName.Text, tbxSurname.Text, tbxPatronomic.Text) == true)
            {
                ShowValidatorsMessage("Такие ФИО уже есть в БД. Комбинация полей \"Имя\", \"Фамилия\" и \"Отчество\" должна быть уникальной.");
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

        // работа с гридом ---------------------------------------------------------------------------------------------------------------------------------------------------------------------
        
        private List<DTO.Student4List> _gridDataStorage // используется для обновления содержимого формы редактирования
        {
            set
            {
                Session["StudentEdit_gridDataStorage"] = value;
            }
            get
            {
                List<DTO.Student4List> result = (List<DTO.Student4List>) Session["StudentEdit_gridDataStorage"];
                return result;
            }
        }

        protected void SemiRefreshGrid()
        {
            Guid filterLanguageId = GetCurrentDropDownListId(ddlFilterLanguage);
            Guid filterEducationLevel = GetCurrentDropDownListId(ddlFilterEducationLevel);
            Guid filterGroupId = GetCurrentDropDownListId(ddlFilterGroup);
            Guid filterPriceGroupId = GetCurrentDropDownListId(ddlFilterPriceGroup);
            String filterSurname = tbxFilterSurname.Text;
            String filterName = tbxFilterName.Text;


            // грузим нужные данные из БД
            List<DTO.Student4List> data = StudentTools.ReadAllWithFilters
            (
                filterLanguageId,
                filterEducationLevel,
                filterGroupId,
                filterPriceGroupId,
                filterSurname,
                filterName               
            );

            _gridDataStorage = data;

            // ребиндим их в грид
            mainGrid.DataSource = data;
        }

        protected void RefreshGrid()
        {
            SemiRefreshGrid();
            mainGrid.DataBind();
        }

        // общее ---------------------------------------------------------------------------------------------------------------------------------------------------------------------

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

                RefreshDdlLanguages();
                Refresh_ddlEducationLevelChained(_ddlFilterLanguage, _ddlFilterEducationLevel);
                Refresh_ddlGroupChained(_ddlFilterEducationLevel, _ddlFilterGroup);
                Refresh_ddlEducationLevelByLanguageAndScore(ddlFormLanguage1, ntbxFormCurrentScore1, ddlFormGroup1);
                Refresh_ddlEducationLevelByLanguageAndScore(ddlFormLanguage2, ntbxFormCurrentScore2, ddlFormGroup2);
                Refresh_ddlEducationLevelByLanguageAndScore(ddlFormLanguage3, ntbxFormCurrentScore3, ddlFormGroup3);
                Refresh_PriceGroupsDdl();

                RefreshGrid();

                wblEditorPanel.SubControlsEnabled = false;
                IsNewFlag = false;
            }
        }

        protected void mainGrid_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            SemiRefreshGrid();
        }

        protected void mainGrid_PageIndexChanged(object sender, GridPageChangedEventArgs e)
        {
            wblEditorPanel.SubControlsEnabled = false;
            IsNewFlag = false;
        }

    }
}