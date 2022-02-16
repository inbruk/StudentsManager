<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Global.master" Inherits="StudentsManager.PresentationLayer.StudentEdit" CodeBehind="StudentsEdit.aspx.cs" %>
<%@ Register TagPrefix="auxctrls" TagName="WindowLikeBorderPanel" Src="~/AuxiliaryControls/WindowLikeBorderPanel.ascx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="title" runat="Server">
    Редактирование студентов
</asp:Content>

<asp:Content ID="mainContent" ContentPlaceHolderID="main" runat="Server">

    <table style="width:1280px;height:729px;padding:0px;margin:0px;" cellspacing="0" cellpadding="0">
        <tr>
            <td style="width:980px;height:550px;padding:0px;margin:0px;" >
                <auxctrls:WindowLikeBorderPanel ID="wlbPanel" Width="980" Height="550" runat="server" Title="Список студентов" >
                    <Contents>

                        <table style="width:952px;height:490px;padding:0px;margin:0px;" cellspacing="0" cellpadding="0">
                            <tr>
                                <td class="formLabelMedium1" >
                                    <asp:Label Text="Язык" runat="server" />
                                </td>
                                <td class="formValueMedium2" >
                                    <telerik:RadDropDownList ID="ddlFilterLanguage" Width="190px" runat="server" DataValueField="Id" DataTextField="Name" AutoPostBack="true" OnItemSelected="ddlFilterLanguage_ItemSelected" />
                                </td>
                                <td class="formLabelMedium15">
                                    <asp:Label Text="Ур. подготовки" runat="server" />
                                </td>
                                <td class="formValueMedium2" >
                                    <telerik:RadDropDownList ID="ddlFilterEducationLevel" Width="190px" runat="server" DataValueField="Id" DataTextField="Name" AutoPostBack="true" OnItemSelected="ddlFilterEducationLevel_ItemSelected"  />
                                </td>
                                <td class="formLabelMedium1" >
                                    <asp:Label Text="Группа" runat="server" />
                                </td>
                                <td class="formValueMedium2" style="width:204px" >
                                    <telerik:RadDropDownList ID="ddlFilterGroup" Width="190px" runat="server" DataValueField="Id" DataTextField="Name" AutoPostBack="true" />
                                </td>
                            </tr>
                            <tr>
                                <td class="formLabelMedium1" >
                                    <asp:Label Text="Фамилия" runat="server" />
                                </td>
                                <td class="formValueMedium15" >
                                    <telerik:RadTextBox ID="tbxFilterSurname" Width="190px" runat="server" />
                                </td>
                                <td class="formLabelMedium15" >
                                    <asp:Label Text="Имя" runat="server" />
                                </td>
                                <td class="formValueMedium15">
                                    <telerik:RadTextBox ID="tbxFilterName" Width="190px" runat="server" />
                                </td>
                                <td class="formLabelMedium1" >
                                    <asp:Label Text="Ценовая гр." runat="server" />
                                </td>
                                <td class="formValueMedium2" style="width:204px" >
                                    <telerik:RadDropDownList ID="ddlFilterPriceGroup" Width="190px" runat="server" DataValueField="Id" DataTextField="Name" />
                                </td>
                            </tr>
                            <tr>
                                <td class="toolBar" colspan="6">
                                    &nbsp;&nbsp;
                                    <asp:LinkButton ID="lbtnAddNew" CssClass="toolBarLinkButton" runat="server" OnClick="lbtnAddNew_Click">
                                        <img src="/Images/AddRecord.png" alt="" style="border:0px;margin-top:3px;"/>
                                        &nbsp;<span style="margin:0px;"> Добавить нового студента </span>
                                    </asp:LinkButton>
                                    &nbsp;&nbsp;&nbsp;
                                    <asp:LinkButton ID="lbtnDeleteCurrent" CssClass="toolBarLinkButton" runat="server" OnClick="lbtnDeleteCurrent_Click">
                                        <img src="/Images/Delete.png" alt="" style="border:0px;margin-top:3px;"/>
                                        &nbsp;<span style="margin:0px;"> Удалить выбранного студента </span>
                                    </asp:LinkButton>                                
                                    &nbsp;&nbsp;&nbsp;
                                    <asp:LinkButton ID="lbtnRefresh" CssClass="toolBarLinkButton" runat="server" OnClick="lbtnRefresh_Click">
                                        <img src="/Images/Refresh.png" alt="" style="border:0px;margin-top:3px;"/>
                                        &nbsp;<span style="margin:0px;"> Обновить/применить фильтры </span>
                                    </asp:LinkButton>
                                    &nbsp;&nbsp;&nbsp;
                                    <asp:LinkButton ID="lbtnClearFilters" CssClass="toolBarLinkButton" runat="server" OnClick="lbtnClearFilters_Click" >
                                        <img src="/Images/no_img.png" alt="" style="border:0px;margin-top:3px;"/>
                                        &nbsp;<span style="margin:0px;"> Очистить фильтры </span>
                                    </asp:LinkButton>                                 
                                </td>
                            </tr>
                            <tr>
                                <td style="padding:0px;margin:0px;" colspan="6">

                                    <telerik:RadGrid ID="mainGrid" Width="952px" Height="412px" runat="server" ClientSettings-Scrolling-AllowScroll="true" ClientSettings-Selecting-AllowRowSelect="true"
                                        AllowMultiRowSelection="False" AllowPaging="true" PageSize="13" AutoGenerateColumns="False" HeaderStyle-HorizontalAlign="Center" 
                                        OnNeedDataSource="mainGrid_NeedDataSource" ClientSettings-Resizing-AllowColumnResize="true" 
                                        ClientSettings-EnablePostBackOnRowClick="true" OnSelectedIndexChanged="mainGrid_SelectedIndexChanged" OnPageIndexChanged="mainGrid_PageIndexChanged"
                                    >
                                        <PagerStyle PageSizeControlType="None" /> 
                                        <MasterTableView CommandItemDisplay="None" DataKeyNames="Id">                                            
                                            <Columns>    
                                                <telerik:GridBoundColumn UniqueName="Surname" DataField="Surname" HeaderText="Фамилия" HeaderStyle-Width="110" ></telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn UniqueName="Name" DataField="Name" HeaderText="Имя" HeaderStyle-Width="100"></telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn UniqueName="Patronomic" DataField="Patronomic" HeaderText="Отчество" HeaderStyle-Width="100"></telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn UniqueName="PriceGroupName" DataField="PriceGroupName" HeaderText="Ценовая группа" HeaderStyle-Width="100"></telerik:GridBoundColumn>

                                                <telerik:GridBoundColumn UniqueName="Language1Name" DataField="Language1Name" HeaderText="Язык 1" HeaderStyle-Width="100"></telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn UniqueName="Group1Name" DataField="Group1Name" HeaderText="Группа 1" HeaderStyle-Width="80"></telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn UniqueName="FirstTestScore1" DataField="FirstTestScore1" HeaderText="Баллы теста 1" HeaderStyle-Width="60"></telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn UniqueName="CurrentScore1" DataField="CurrentScore1" HeaderText="Текущие баллы 1" HeaderStyle-Width="60"></telerik:GridBoundColumn>

                                                <telerik:GridBoundColumn UniqueName="Phone" DataField="Phone" HeaderText="Телефон" HeaderStyle-Width="120"></telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn UniqueName="EMail" DataField="EMail" HeaderText="Эл. почта" HeaderStyle-Width="120"></telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn UniqueName="SurnameAndInitials" DataField="SurnameAndInitials" HeaderText="Фамилия И.О." HeaderStyle-Width="120"></telerik:GridBoundColumn>

                                                <telerik:GridBoundColumn UniqueName="Language2Name" DataField="Language2Name" HeaderText="Язык 2" HeaderStyle-Width="100"></telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn UniqueName="Group2Name" DataField="Group2Name" HeaderText="Группа 2" HeaderStyle-Width="80"></telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn UniqueName="FirstTestScore2" DataField="FirstTestScore2" HeaderText="Баллы теста 2" HeaderStyle-Width="60"></telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn UniqueName="CurrentScore2" DataField="CurrentScore2" HeaderText="Текущие баллы 2" HeaderStyle-Width="60"></telerik:GridBoundColumn>

                                                <telerik:GridBoundColumn UniqueName="Language3Name" DataField="Language3Name" HeaderText="Язык 3" HeaderStyle-Width="100"></telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn UniqueName="Group3Name" DataField="Group3Name" HeaderText="Группа 3" HeaderStyle-Width="80"></telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn UniqueName="FirstTestScore3" DataField="FirstTestScore3" HeaderText="Баллы теста 3" HeaderStyle-Width="60"></telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn UniqueName="CurrentScore3" DataField="CurrentScore3" HeaderText="Текущие баллы 3" HeaderStyle-Width="60"></telerik:GridBoundColumn>

                                            </Columns>
                                        </MasterTableView>
                                    </telerik:RadGrid>

                                </td>
                            </tr>
                        </table>

                    </Contents>            
                </auxctrls:WindowLikeBorderPanel>
            </td>
            <td style="width:300px;height:554px;padding:0px;margin:0px;" >
                <auxctrls:WindowLikeBorderPanel ID="wblShedulerPanel" Width="300" Height="554" runat="server" Title="События выбранного студента" >
                    <Contents>
                    </Contents>            
                </auxctrls:WindowLikeBorderPanel>
            </td>
        </tr>
        <tr>
            <td style="width:1280px;height:170px;padding:0px;margin:0px;" colspan="2">
                <auxctrls:WindowLikeBorderPanel ID="wblEditorPanel" Width="1280" Height="170" runat="server" Title="Редактирование выбранного студента" >
                    <Contents>
                        <table style="width:1250px;height:120px;padding:0px;margin:0px;" cellspacing="0" cellpadding="0">
                            <tr>
                                <td class="formLabelMedium1" >
                                    <asp:Label Text="Фамилия *" runat="server" />
                                </td>
                                <td class="formValueMedium15">
                                    <telerik:RadTextBox ID="tbxSurname" Width="140px" runat="server" MaxLength="50" />
                                </td>

                                <td class="formLabelMedium1" >
                                    <asp:Label Text="Фамилия И.О." runat="server" />
                                </td>
                                <td class="formValueMedium15">
                                    <telerik:RadTextBox ID="tbxSurnameAndInitials" Width="140px" runat="server" MaxLength="50" />
                                </td>

                                <td class="formLabelMedium2" style="text-align:center">
                                    <asp:Label Text="Ценовая группа" runat="server" />
                                </td>
                                
                                <td style="width:550px;height:90px;padding:0px;" colspan="3" rowspan="3">
                                    <table style="width:550px;height:90px;padding:0px;margin:0px;" cellspacing="0" cellpadding="0">
                                        <tr>
                                            <td style="width:80px;height:90px;padding:0px;">
                                                <telerik:RadTabStrip runat="server" ID="RadTabStrip1" Width="80" Height="90"
                                                    Orientation="VerticalLeft" SelectedIndex="0" MultiPageID="RadMultiPage1">
                                                    <Tabs>
                                                        <telerik:RadTab Text="Язык 1" Height="30"/>
                                                        <telerik:RadTab Text="Язык 2" Height="30"/>
                                                        <telerik:RadTab Text="Язык 3" Height="30"/>
                                                    </Tabs>
                                                </telerik:RadTabStrip>
                                            </td>
                                            <td style="width:467px;height:90px;padding:0px;">
                                                <telerik:RadMultiPage runat="server" ID="RadMultiPage1" SelectedIndex="0" Width="467px" Height="90" BorderStyle="Solid" BorderColor="Gray" BorderWidth="1" >
                                                    <telerik:RadPageView runat="server" ID="RadPageView1" >
                                                        <table style="width:465px;height:88px;padding:0px;margin:0px;" cellspacing="0" cellpadding="0">
                                                            <tr>
                                                                <td class="formLabelMedium1" style="width:60px" >
                                                                    <asp:Label Text="Язык 1" runat="server" />
                                                                </td>
                                                                <td class="formValueMedium15" style="width:170px" >
                                                                    <telerik:RadDropDownList ID="ddlFormLanguage1" Width="160px" runat="server" DataValueField="Id" DataTextField="Name" AutoPostBack="true" OnItemSelected="ddlFormLanguage1_ItemSelected"  />
                                                                </td>
                                                                <td class="formLabelMedium1" style="width:60px" >
                                                                    <asp:Label Text="Группа 1" runat="server" />
                                                                </td>
                                                                <td class="formValueMedium15" style="width:170px" >
                                                                    <telerik:RadDropDownList ID="ddlFormGroup1" Width="160px" runat="server" DataValueField="Id" DataTextField="Name" AutoPostBack="true" />
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td class="formLabelMedium1" style="width:60px" >
                                                                    <asp:Label Text="Текущие баллы 1" runat="server" />
                                                                </td>
                                                                <td class="formValueMedium15" style="width:170px" >
                                                                    <telerik:RadNumericTextBox ID="ntbxFormCurrentScore1" Width="160px" runat="server" KeepNotRoundedValue="false" NumberFormat-DecimalDigits="0"
                                                                        MinValue="0" MaxValue="999" Culture="ru-RU" DataType="Int32" AllowOutOfRangeAutoCorrect="true" MaxLength="3" Value="0" 
                                                                        AutoPostBack="true" OnTextChanged="ntbxCurrentScore1_ChildrenCreated" />
                                                                </td>
                                                                <td class="formLabelMedium1" style="width:60px" >
                                                                    <asp:Label Text="Баллы на тесте 1" runat="server" />
                                                                </td>
                                                                <td class="formValueMedium15" style="width:170px" >
                                                                    <telerik:RadNumericTextBox ID="ntbxFormFirstTestScore1" Width="160px" runat="server" KeepNotRoundedValue="false" NumberFormat-DecimalDigits="0"
                                                                        MinValue="0" MaxValue="999" Culture="ru-RU" DataType="Int32" AllowOutOfRangeAutoCorrect="true" MaxLength="3" Value="0" />
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </telerik:RadPageView>
                                                    <telerik:RadPageView runat="server" ID="RadPageView2" >
                                                        <table style="width:465px;height:88px;padding:0px;margin:0px;" cellspacing="0" cellpadding="0">
                                                            <tr>
                                                                <td class="formLabelMedium1" style="width:60px" >
                                                                    <asp:Label Text="Язык 2" runat="server" />
                                                                </td>
                                                                <td class="formValueMedium15" style="width:170px" >
                                                                    <telerik:RadDropDownList ID="ddlFormLanguage2" Width="160px" runat="server" DataValueField="Id" DataTextField="Name" 
                                                                        AutoPostBack="true" OnItemSelected="ddlFormLanguage2_ItemSelected" />
                                                                </td>
                                                                <td class="formLabelMedium1" style="width:60px" >
                                                                    <asp:Label Text="Группа 2" runat="server" />
                                                                </td>
                                                                <td class="formValueMedium15" style="width:170px" >
                                                                    <telerik:RadDropDownList ID="ddlFormGroup2" Width="160px" runat="server" DataValueField="Id" DataTextField="Name" AutoPostBack="true" />
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td class="formLabelMedium1" style="width:60px" >
                                                                    <asp:Label Text="Текущие баллы 2" runat="server" />
                                                                </td>
                                                                <td class="formValueMedium15" style="width:170px" >
                                                                    <telerik:RadNumericTextBox ID="ntbxFormCurrentScore2" Width="160px" runat="server" KeepNotRoundedValue="false" NumberFormat-DecimalDigits="0"
                                                                        MinValue="0" MaxValue="999" Culture="ru-RU" DataType="Int32" AllowOutOfRangeAutoCorrect="true" MaxLength="3" Value="0" 
                                                                        AutoPostBack="true" OnTextChanged="ntbxCurrentScore2_TextChanged" />
                                                                </td>
                                                                <td class="formLabelMedium1" style="width:60px" >
                                                                    <asp:Label Text="Баллы на тесте 2" runat="server" />
                                                                </td>
                                                                <td class="formValueMedium15" style="width:170px" >
                                                                    <telerik:RadNumericTextBox ID="ntbxFormFirstTestScore2" Width="160px" runat="server" KeepNotRoundedValue="false" NumberFormat-DecimalDigits="0"
                                                                        MinValue="0" MaxValue="999" Culture="ru-RU" DataType="Int32" AllowOutOfRangeAutoCorrect="true" MaxLength="3" Value="0" />
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </telerik:RadPageView>
                                                    <telerik:RadPageView runat="server" ID="RadPageView3" >
                                                        <table style="width:465px;height:88px;padding:0px;margin:0px;" cellspacing="0" cellpadding="0">
                                                            <tr>
                                                                <td class="formLabelMedium1" style="width:60px" >
                                                                    <asp:Label Text="Язык 3" runat="server" />
                                                                </td>
                                                                <td class="formValueMedium15" style="width:170px" >
                                                                    <telerik:RadDropDownList ID="ddlFormLanguage3" Width="160px" runat="server" DataValueField="Id" DataTextField="Name" 
                                                                        AutoPostBack="true"  OnItemSelected="ddlFormLanguage3_ItemSelected" />
                                                                </td>
                                                                <td class="formLabelMedium1" style="width:60px" >
                                                                    <asp:Label Text="Группа 3" runat="server" />
                                                                </td>
                                                                <td class="formValueMedium15" style="width:170px" >
                                                                    <telerik:RadDropDownList ID="ddlFormGroup3" Width="160px" runat="server" DataValueField="Id" DataTextField="Name" AutoPostBack="true" />
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td class="formLabelMedium1" style="width:60px" >
                                                                    <asp:Label Text="Текущие баллы 3" runat="server" />
                                                                </td>
                                                                <td class="formValueMedium15" style="width:170px" >
                                                                    <telerik:RadNumericTextBox ID="ntbxFormCurrentScore3" Width="160px" runat="server" KeepNotRoundedValue="false" NumberFormat-DecimalDigits="0"
                                                                        MinValue="0" MaxValue="999" Culture="ru-RU" DataType="Int32" AllowOutOfRangeAutoCorrect="true" MaxLength="3" Value="0" 
                                                                        AutoPostBack="true" OnTextChanged="ntbxCurrentScore3_TextChanged" />
                                                                </td>
                                                                <td class="formLabelMedium1" style="width:60px" >
                                                                    <asp:Label Text="Баллы на тесте 3" runat="server" />
                                                                </td>
                                                                <td class="formValueMedium15" style="width:170px" >
                                                                    <telerik:RadNumericTextBox ID="ntbxFormFirstTestScore3" Width="160px" runat="server" KeepNotRoundedValue="false" NumberFormat-DecimalDigits="0"
                                                                        MinValue="0" MaxValue="999" Culture="ru-RU" DataType="Int32" AllowOutOfRangeAutoCorrect="true" MaxLength="3" Value="0" />
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </telerik:RadPageView>
                                                </telerik:RadMultiPage>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td class="formLabelMedium1" >
                                    <asp:Label Text="Имя *" runat="server" />
                                </td>
                                <td class="formValueMedium15">
                                    <telerik:RadTextBox ID="tbxName" Width="140px" runat="server" MaxLength="50" />
                                </td>

                                <td class="formLabelMedium1" >
                                    <asp:Label Text="Эл. почта" runat="server" />
                                </td>
                                <td class="formValueMedium15">
                                    <telerik:RadTextBox ID="tbxEMail" Width="140px" runat="server" MaxLength="16" />
                                </td>

                                <td class="formValueMedium2" style="text-align:center">
                                    <telerik:RadDropDownList ID="ddlPriceGroup" Width="140px" runat="server" DataValueField="Id" DataTextField="Name" AutoPostBack="true" />
                                </td>
                            </tr>
                            <tr>
                                <td class="formLabelMedium1" >
                                    <asp:Label Text="Отчество" runat="server" />
                                </td>
                                <td class="formValueMedium15">
                                    <telerik:RadTextBox ID="tbxPatronomic" Width="140px" runat="server" MaxLength="50" />
                                </td>

                                <td class="formLabelMedium1" >
                                    <asp:Label Text="Телефон *" runat="server" />
                                </td>
                                <td class="formValueMedium15">
                                    <telerik:RadTextBox ID="tbxPhone" Width="140px" runat="server" MaxLength="16" />
                                </td>

                                <td class="formValueMedium2">
                                </td>
                            </tr>
                            <tr>
                                <td id="tdValidationArea" runat="server" style="text-align:center;height:30px;padding:0px;" colspan="6">
                                    <asp:Panel ID="pnlValidation" runat="server" Visible="false" Width="1050" Height="30" >
                                        <asp:Label ID="lblValidationText" ForeColor="DarkRed" Text="" runat="server"/>
                                    </asp:Panel>
                                </td>
                                <td style="width:100px" >
                                    <telerik:RadButton ID="btnSave" Width="90px" Text="Сохранить  " runat="server" OnClick="btnSave_Click" />
                                </td>
                                <td style="width:100px">
                                    <telerik:RadButton ID="btnCancel" Width="90px" Text="Отмена  " runat="server" OnClick="btnCancel_Click"  />
                                </td>
                            </tr>
                        </table>
                    </Contents>            
                </auxctrls:WindowLikeBorderPanel>
            </td>            
        </tr>
    </table>

    <asp:LinkButton ID="btnDeleteAction" OnClick="btnDeleteAction_Click" runat="server" />

    <asp:LinkButton ID="btnCancelAction" OnClick="btnCancelAction_Click" runat="server" />

    <script type="text/javascript">
        function confirmDeleteCallbackFn(arg) {
            if (arg) //the user clicked OK
            {
                __doPostBack("<%=btnDeleteAction.UniqueID %>", "");
            }
            else {
                __doPostBack("<%=btnCancelAction.UniqueID %>", "");
            }
        }
    </script>

    <telerik:RadWindowManager ID="RadWindowManager1" runat="server" Visible="true" />

</asp:Content>