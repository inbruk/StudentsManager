<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Global.master" Inherits="StudentsManager.PresentationLayer.UserEdit" CodeBehind="UserEdit.aspx.cs" %>
<%@ Register TagPrefix="auxctrls" TagName="WindowLikeBorderPanel" Src="~/AuxiliaryControls/WindowLikeBorderPanel.ascx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="title" runat="Server">
    Редактирование пользователей
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
                            <telerik:GridBoundColumn UniqueName="Login" DataField="Login" HeaderText="Имя входа" ItemStyle-Width="150px"></telerik:GridBoundColumn>
                            <telerik:GridBoundColumn UniqueName="Password" DataField="Password" HeaderText="Пароль" ItemStyle-Width="150px"></telerik:GridBoundColumn>
                            <telerik:GridBoundColumn UniqueName="SurnameAndInitials" DataField="SurnameAndInitials" HeaderText="Фамилия И.О."></telerik:GridBoundColumn>
                            <telerik:GridBoundColumn UniqueName="EMail" DataField="EMail" HeaderText="Эл. почта" ItemStyle-Width="200px"></telerik:GridBoundColumn>
                            <telerik:GridBoundColumn UniqueName="Phone" DataField="Phone" HeaderText="Телефон" ItemStyle-Width="100px"></telerik:GridBoundColumn>
                            <telerik:GridButtonColumn Text="Edit" ButtonType="ImageButton" CommandName="MyEditCommand" ImageUrl="~/Images/Edit.png" ItemStyle-Width="20px" />
                            <telerik:GridButtonColumn ConfirmText="Вы действительно хотите удалить эту запись ?" ItemStyle-Width="20px"
                                ConfirmDialogType="RadWindow" ConfirmTitle="Удалить" ButtonType="ImageButton" CommandName="Delete" ></telerik:GridButtonColumn>
                        </Columns>
                        <CommandItemSettings AddNewRecordText=" Добавить нового пользователя" RefreshText="Обновить"  AddNewRecordImageUrl="Images/AddRecord.png" />
                    </MasterTableView>
                </telerik:RadGrid>

            </Contents>            
        </auxctrls:WindowLikeBorderPanel>

    <telerik:RadWindow ID="RadWindow1" runat="server" Width="370px" Height="300px" Behaviors="None" Modal="true" VisibleStatusbar="false" ReloadOnShow="false" >
        <ContentTemplate>
            <table style="border:0px;margin:0px;width:333px;padding:0px;">
                <tbody>
                    <tr>
                        <td style="width:100px;height:20px;text-align:left;padding:5px;">
                            <asp:Label Text="Имя входа *" runat="server" />
                        </td>
                        <td style="width:220px;height:20px">
                            <telerik:RadTextBox ID="tbxLogin" Width="220px" runat="server" MaxLength="32" />
                        </td>
                    </tr>
                    <tr>
                        <td style="width:100px;height:20px;text-align:left;padding:5px;">
                            <asp:Label Text="Пароль *" runat="server" />
                        </td>
                        <td style="width:220px;height:20px">
                            <telerik:RadTextBox ID="tbxPassword" Width="220px" runat="server" MaxLength="50" />
                        </td>
                    </tr>
                    <tr>
                        <td style="width:100px;height:20px;text-align:left;padding:5px;">
                            <asp:Label Text="Фамилия И. О." runat="server" />
                        </td>
                        <td style="width:220px;height:20px">
                            <telerik:RadTextBox ID="tbxSurnameAndInitials" Width="220px" runat="server" MaxLength="50" />
                        </td>
                    </tr>
                    <tr>
                        <td style="width:100px;height:20px;text-align:left;padding:5px;">
                            <asp:Label Text="Эл. почта" runat="server" />
                        </td>
                        <td style="width:220px;height:20px">
                            <telerik:RadTextBox ID="tbxEMail" Width="220px" runat="server" MaxLength="16" />
                        </td>
                    </tr>
                    <tr>
                        <td style="width:100px;height:20px;text-align:left;padding:5px;">
                            <asp:Label Text="Телефон *" runat="server" />
                        </td>
                        <td style="width:220px;height:20px">
                            <telerik:RadTextBox ID="tbxPhone" Width="220px" runat="server" MaxLength="16" />
                        </td>
                    </tr>
                    <tr>
                        <td style="width:100px;height:20px;text-align:left;padding:5px;">
                            <asp:Label Text="Тип пользователя" runat="server" />
                        </td>
                        <td style="width:220px;height:20px">
                            <telerik:RadDropDownList ID="ddlUserType" Width="220px" runat="server" DataValueField="Id" DataTextField="Name" />
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