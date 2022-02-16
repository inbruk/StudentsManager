<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Global.master" Inherits="StudentsManager.PresentationLayer.DictionariesEdit" CodeBehind="DictionariesEdit.aspx.cs" %>
<%@ Register TagPrefix="auxctrls" TagName="WindowLikeBorderPanel" Src="~/AuxiliaryControls/WindowLikeBorderPanel.ascx" %>

    <asp:Content ID="Content1" ContentPlaceHolderID="title" runat="Server">
        Редактирование словаря
    </asp:Content>

    <asp:Content ID="mainContent" ContentPlaceHolderID="main" runat="Server">

        <auxctrls:WindowLikeBorderPanel ID="wlbPanel" Width="1280" Height="729" runat="server" >
            <Contents>

                <telerik:RadGrid ID="mainGrid" Width="1252" Height="678px" runat="server" 
                    AllowMultiRowSelection="False" AllowPaging="False" GridLines="None" AutoGenerateColumns="False" OnNeedDataSource="mainGrid_NeedDataSource"
                    OnDeleteCommand="mainGrid_DeleteCommand" OnItemCommand="mainGrid_ItemCommand"
                >
                    <MasterTableView CommandItemDisplay="Top" DataKeyNames="Id">
                        <Columns>                    
                            <telerik:GridBoundColumn UniqueName="Name" DataField="Name" HeaderText="Название" ItemStyle-Width="200px"></telerik:GridBoundColumn>
                            <telerik:GridBoundColumn UniqueName="Description" DataField="Description" HeaderText="Описание"></telerik:GridBoundColumn>
                            <telerik:GridButtonColumn Text="Edit" ButtonType="ImageButton" CommandName="MyEditCommand" ImageUrl="~/Images/Edit.png" ItemStyle-Width="20px" />
                            <telerik:GridButtonColumn ConfirmText="Вы действительно хотите удалить эту запись ?" ItemStyle-Width="20px"
                                ConfirmDialogType="RadWindow" ConfirmTitle="Удалить" ButtonType="ImageButton" CommandName="Delete" ></telerik:GridButtonColumn>
                        </Columns>
                        <CommandItemSettings AddNewRecordText=" Добавить новую запись" RefreshText="Обновить"  AddNewRecordImageUrl="Images/AddRecord.png" />
                    </MasterTableView>
                </telerik:RadGrid>

            </Contents>            
        </auxctrls:WindowLikeBorderPanel>

        <telerik:RadWindow ID="RadWindow1" runat="server" Width="320px" Height="300px" Behaviors="None" Modal="true" VisibleStatusbar="false" ReloadOnShow="false" >
            <ContentTemplate>
                <table style="border:0px;margin:0px;width:300px;padding:0px;">
                    <tbody>
                        <tr>
                            <td style="width:100px;height:20px;text-align:left;padding:5px;">
                                <asp:Label Text="Название *" runat="server" />
                            </td>
                            <td style="width:200px;height:20px">
                                <asp:TextBox ID="tbxName" Width="190px" runat="server" MaxLength="50"/>
                            </td>
                        </tr>
                        <tr>
                            <td style="width:300px;height:20px;text-align:left;padding:5px;" colspan="2">
                                <asp:Label Text="Описание" runat="server" />
                            </td>                            
                        </tr>
                        <tr>
                            <td style="width:300px;height:140px;text-align:center;padding:0px;" colspan="2">
                                <asp:TextBox ID="tbxDescription" Width="290px" Height="140" runat="server" MaxLength="512" TextMode="MultiLine" Rows="10" />
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
                            <td style="width:50%;height:30px;text-align:right;padding:0px;padding-left:10px;">
                                <asp:Button ID="btnUpdate" Width="90px" Text="Сохранить" OnClick="btnUpdate_Click" runat="server" />
                            </td>
                            <td style="width:50%;height:30px;text-align:right;padding:0px;padding-right:10px;">
                                <asp:Button ID="btnCancel" Width="90px" Text="Отмена" OnClick="btnCancel_Click" runat="server" />
                            </td>
                        </tr>
                    </tbody>
                </table>
            </ContentTemplate>
        </telerik:RadWindow>

        <telerik:RadWindowManager ID="RadWindowManager1" runat="server" EnableShadow="true"/>

    </asp:Content>