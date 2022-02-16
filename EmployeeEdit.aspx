<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Global.master" Inherits="StudentsManager.PresentationLayer.EmployeeEdit" CodeBehind="EmployeeEdit.aspx.cs" %>
<%@ Register TagPrefix="auxctrls" TagName="WindowLikeBorderPanel" Src="~/AuxiliaryControls/WindowLikeBorderPanel.ascx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="title" runat="Server">
    Редактирование сотрудников
</asp:Content>

<asp:Content ID="mainContent" ContentPlaceHolderID="main" runat="Server">

    <auxctrls:WindowLikeBorderPanel ID="wlbPanel" Width="1280" Height="729" runat="server" >
        <Contents>

            <telerik:RadGrid ID="mainGrid" Width="1252px" Height="678px" runat="server" 
                AllowMultiRowSelection="False" AllowPaging="False" GridLines="None" AutoGenerateColumns="False" OnNeedDataSource="mainGrid_NeedDataSource"
                OnDeleteCommand="mainGrid_DeleteCommand" OnItemCommand="mainGrid_ItemCommand" 
            >
                <MasterTableView CommandItemDisplay="Top" DataKeyNames="Id">
                    <Columns>    
                        <telerik:GridBoundColumn UniqueName="Surname" DataField="Surname" HeaderText="Фамилия" ItemStyle-Width="150px"></telerik:GridBoundColumn>
                        <telerik:GridBoundColumn UniqueName="Name" DataField="Name" HeaderText="Имя" ItemStyle-Width="150px"></telerik:GridBoundColumn>
                        <telerik:GridBoundColumn UniqueName="Patronomic" DataField="Patronomic" HeaderText="Отчество" ItemStyle-Width="150px"></telerik:GridBoundColumn>

                        <telerik:GridBoundColumn UniqueName="SurnameAndInitials" DataField="SurnameAndInitails" HeaderText="Фамилия И.О."></telerik:GridBoundColumn>
                        <telerik:GridBoundColumn UniqueName="EMail" DataField="EMail" HeaderText="Эл. почта" ItemStyle-Width="200px"></telerik:GridBoundColumn>
                        <telerik:GridBoundColumn UniqueName="Phone" DataField="Phone" HeaderText="Телефон" ItemStyle-Width="100px"></telerik:GridBoundColumn>

                        <telerik:GridButtonColumn Text="Edit" ButtonType="ImageButton" CommandName="MyEditCommand" ImageUrl="~/Images/Edit.png" ItemStyle-Width="20px" />
                        <telerik:GridButtonColumn ConfirmText="Вы действительно хотите удалить эту запись ?" ItemStyle-Width="20px"
                            ConfirmDialogType="RadWindow" ConfirmTitle="Удалить" ButtonType="ImageButton" CommandName="Delete" ></telerik:GridButtonColumn>
                    </Columns>
                    <CommandItemSettings AddNewRecordText=" Добавить нового сотрудника" RefreshText="Обновить"  AddNewRecordImageUrl="Images/AddRecord.png" />
                </MasterTableView>
            </telerik:RadGrid>

        </Contents>            
    </auxctrls:WindowLikeBorderPanel>

    <telerik:RadWindow ID="RadWindow1" runat="server" Width="330px" Height="340px" Behaviors="None" Modal="true" VisibleStatusbar="false" ReloadOnShow="false" >
        <ContentTemplate>
            <table style="border:0px;margin:0px;width:300px;height:180px;padding:0px;">
                <tbody>
                    <tr>
                        <td style="width:100px;height:20px;text-align:right;padding:5px;">
                            <asp:Label Text="Фамилия *" runat="server" />
                        </td>
                        <td style="width:200px;height:20px">
                            <telerik:RadTextBox ID="tbxSurname" Width="200px" runat="server" MaxLength="50" />
                        </td>
                    </tr>
                    <tr>
                        <td style="width:100px;height:20px;text-align:right;padding:5px;">
                            <asp:Label Text="Имя *" runat="server" />
                        </td>
                        <td style="width:200px;height:20px">
                            <telerik:RadTextBox ID="tbxName" Width="200px" runat="server" MaxLength="50" />
                        </td>
                    </tr>
                    <tr>
                        <td style="width:100px;height:20px;text-align:right;padding:5px;">
                            <asp:Label Text="Отчество" runat="server" />
                        </td>
                        <td style="width:200px;height:20px">
                            <telerik:RadTextBox ID="tbxPatronomic" Width="200px" runat="server" MaxLength="50" />
                        </td>
                    </tr>
                    <tr>
                        <td style="width:100px;height:20px;text-align:right;padding:5px;">
                            <asp:Label Text="Фамилия И.О." runat="server" />
                        </td>
                        <td style="width:200px;height:20px">
                            <telerik:RadTextBox ID="tbxSurnameAndInitials" Width="200px" runat="server" MaxLength="50" />
                        </td>
                    </tr>
                    <tr>
                        <td style="width:100px;height:20px;text-align:right;padding:5px;">
                            <asp:Label Text="Эл. почта" runat="server" />
                        </td>
                        <td style="width:200px;height:20px">
                            <telerik:RadTextBox ID="tbxEMail" Width="200px" runat="server" MaxLength="16" />
                        </td>
                    </tr>
                    <tr>
                        <td style="width:100px;height:20px;text-align:right;padding:5px;">
                            <asp:Label Text="Телефон *" runat="server" />
                        </td>
                        <td style="width:200px;height:20px">
                            <telerik:RadTextBox ID="tbxPhone" Width="200px" runat="server" MaxLength="16" />
                        </td>
                    </tr>
                    <tr>
                        <td style="width:100px;height:20px;text-align:right;padding:5px;">
                            <asp:Label Text="Тип пользователя:" runat="server" />
                        </td>
                        <td style="width:200px;height:20px">
                            <telerik:RadDropDownList ID="ddlEmployeeType" Width="200px" runat="server" DataValueField="Id" DataTextField="Name" />
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