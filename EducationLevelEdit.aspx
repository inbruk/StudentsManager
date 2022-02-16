<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Global.master" Inherits="StudentsManager.PresentationLayer.EducationLevelEdit" CodeBehind="EducationLevelEdit.aspx.cs" %>
<%@ Register TagPrefix="auxctrls" TagName="WindowLikeBorderPanel" Src="~/AuxiliaryControls/WindowLikeBorderPanel.ascx" %>

    <asp:Content ID="Content1" ContentPlaceHolderID="title" runat="Server">
        Уровни подготовки
    </asp:Content>

    <asp:Content ID="mainContent" ContentPlaceHolderID="main" runat="Server">

        <auxctrls:WindowLikeBorderPanel ID="wlbPanel" Width="1280" Height="729" Title="Уровни подготовки" runat="server" >
            <Contents>

                <table style="width:1254px;height:680px;padding:0px;margin:0px;" cellspacing="0" cellpadding="0">
                    <tr>
                        <td style="width:60px;height:30px;text-align:right;padding:5px;font-family:arial;font-size:12px;font-weight:normal;">
                            <asp:Label Text="Язык:" runat="server" />
                        </td>
                        <td style="height:30px;text-align:left;padding:5px;">
                            <telerik:RadDropDownList ID="ddlLanguage" Width="200px" runat="server" DataValueField="Id" DataTextField="Name"
                                AutoPostBack="true" OnItemSelected="ddlLanguage_ItemSelected" />
                        </td>
                    </tr>
                    <tr>
                        <td style="padding:0px;margin:0px;" colspan="2">

                            <telerik:RadGrid ID="mainGrid" Width="1252px" Height="636px" runat="server" 
                                AllowMultiRowSelection="False" AllowPaging="False" GridLines="None" AutoGenerateColumns="False" OnNeedDataSource="mainGrid_NeedDataSource"
                                OnDeleteCommand="mainGrid_DeleteCommand" OnItemCommand="mainGrid_ItemCommand"
                            >
                                <MasterTableView CommandItemDisplay="Top" DataKeyNames="Id">
                                    <Columns>
                                        <telerik:GridBoundColumn UniqueName="Name" DataField="Name" HeaderText="Название" ItemStyle-Width="200px"></telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn UniqueName="Description" DataField="Description" HeaderText="Описание"></telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn UniqueName="Position" DataField="Position" HeaderText="Позиция" ItemStyle-Width="50px" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" />
                                        <telerik:GridBoundColumn UniqueName="MinScore" DataField="MinScore" HeaderText="Баллы (min)" ItemStyle-Width="80px" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" />
                                        <telerik:GridBoundColumn UniqueName="MaxScore" DataField="MaxScore" HeaderText="Баллы (max)" ItemStyle-Width="80px" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" />
                                        <telerik:GridButtonColumn Text="Edit" ButtonType="ImageButton" CommandName="MyEditCommand" ImageUrl="~/Images/Edit.png" ItemStyle-Width="20px" />
                                        <telerik:GridButtonColumn ConfirmText="Вы действительно хотите удалить эту запись ?" ItemStyle-Width="20px" ConfirmDialogType="RadWindow" ConfirmTitle="Удалить" ButtonType="ImageButton" CommandName="Delete" />
                                    </Columns>
                                    <CommandItemSettings AddNewRecordText="  Добавить новый уровень образования" RefreshText="Обновить" AddNewRecordImageUrl="Images/AddRecord.png" />
                                </MasterTableView>
                            </telerik:RadGrid>

                        </td>
                    </tr>
                </table>

            </Contents>            
        </auxctrls:WindowLikeBorderPanel>

        <telerik:RadWindow ID="RadWindow1" runat="server" Width="330px" Height="240px" Behaviors="None" Modal="true" VisibleStatusbar="false" ReloadOnShow="false" >
            <ContentTemplate>
                <table style="border:0px;margin:0px;width:300px;padding:0px;">
                    <tbody>
                        <tr>
                            <td style="width:100px;height:20px;text-align:right;padding:5px;">
                                <asp:Label Text="Название *" runat="server" />
                            </td>
                            <td style="width:200px;height:20px">
                                <telerik:RadTextBox ID="tbxName" Width="200px" runat="server" MaxLength="50"/>
                            </td>
                        </tr>
                        <tr>
                            <td style="width:100px;height:20px;text-align:right;padding:5px;">
                                <asp:Label Text="Описание" runat="server" />
                            </td>
                            <td style="width:200px;height:20px">
                                <telerik:RadTextBox ID="tbxDescription" Width="200px" runat="server" MaxLength="512"/>
                            </td>
                        </tr>
                        <tr>
                            <td style="width:100px;height:20px;text-align:right;padding:5px;">
                                <asp:Label Text="Позиция *" runat="server" />
                            </td>
                            <td style="width:200px;height:20px">
                                <telerik:RadNumericTextBox ID="ntbxPosition" Width="200px" KeepNotRoundedValue="false" NumberFormat-DecimalDigits="0"
                                    MinValue="0" MaxValue="99" Culture="ru-RU" DataType="Byte" AllowOutOfRangeAutoCorrect="true" runat="server" />
                            </td>
                        </tr>
                        <tr>
                            <td style="width:100px;height:20px;text-align:right;padding:5px;">
                                <asp:Label Text="Баллы (max) *" runat="server" />
                            </td>
                            <td style="width:200px;height:20px">
                                <telerik:RadNumericTextBox ID="ntbxScore" Width="200px" runat="server" KeepNotRoundedValue="false" NumberFormat-DecimalDigits="0"
                                    MinValue="1" MaxValue="999" Culture="ru-RU" DataType="Int32" AllowOutOfRangeAutoCorrect="true" MaxLength="3" />
                            </td>
                        </tr>

                        <tr>
                            <td id="tdValidationArea" runat="server" style="width:290px;text-align:center;height:0px;padding:0px;" colspan="2">
                                <asp:Panel ID="pnlValidation" runat="server" Visible="false" Width="290px" >
                                    <asp:Label ID="lblValidationText" ForeColor="DarkRed" Text="" runat="server" />
                                </asp:Panel>
                            </td>
                        </tr>

                        <tr>
                            <td style="width:50%;height:30px;text-align:right;padding-top:5px;padding-left:10px;">
                                <asp:Button ID="btnUpdate" Width="90px" Text="Сохранить" OnClick="btnUpdate_Click" runat="server" />
                            </td>
                            <td style="width:50%;height:30px;text-align:right;padding-top:5px;padding-right:10px;">
                                <asp:Button ID="btnCancel" Width="90px" Text="Отмена" OnClick="btnCancel_Click" runat="server" />
                            </td>
                        </tr>
                    </tbody>
                </table>
            </ContentTemplate>
        </telerik:RadWindow>

        <telerik:RadWindowManager ID="RadWindowManager1" runat="server" EnableShadow="true"/>

    </asp:Content>