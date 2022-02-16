<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Global.master" Inherits="StudentsManager.PresentationLayer.DatabaseSettings" CodeFile="DatabaseSettings.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="title" runat="Server">
        Параметры базы данных
    </asp:Content>

    <asp:Content ID="bottomContent" ContentPlaceHolderID="bottom" runat="Server">        
 
        <telerik:RadWindow ID="RadWindow1" runat="server" Width="330px" Height="320px" Visible="true" VisibleOnPageLoad="true" 
            Behaviors="None" Modal="false" VisibleStatusbar="false" ReloadOnShow="false" Title="Параметры базы данных" 
        >
            <ContentTemplate>
                <table style="border:0px;margin:0px;width:300px;padding:0px;">
                    <tbody>
                        <tr>
                            <td style="width:100px;height:20px;text-align:right;padding:5px;">
                                <asp:Label Text="Data Source *" runat="server" />
                            </td>
                            <td style="width:200px;height:20px">
                                <telerik:RadTextBox ID="tbxDataSource" Width="200px" runat="server" MaxLength="256"/>
                            </td>
                        </tr>
                        <tr>
                            <td style="width:100px;height:20px;text-align:right;padding:5px;">
                                <asp:Label Text="Initial Catalog *" runat="server" />
                            </td>
                            <td style="width:200px;height:20px">
                                <telerik:RadTextBox ID="tbxInitialCatalog" Width="200px" runat="server" MaxLength="256"/>
                            </td>
                        </tr>
                        <tr>
                            <td style="width:100px;height:20px;text-align:right;padding:5px;">
                                <asp:Label Text="User ID *" runat="server" />
                            </td>
                            <td style="width:200px;height:20px">
                                <telerik:RadTextBox ID="tbxUserId" Width="200px" runat="server" MaxLength="256"/>
                            </td>
                        </tr>
                        <tr>
                            <td style="width:100px;height:20px;text-align:right;padding:5px;">
                                <asp:Label Text="Password *" runat="server" />
                            </td>
                            <td style="width:200px;height:20px">
                                <telerik:RadTextBox ID="tbxPassword" Width="200px" runat="server" MaxLength="256"/>
                            </td>
                        </tr>
                        <tr>
                            <td style="width:100px;height:20px;text-align:right;padding:5px;">
                                <asp:Label Text="Путь для бэкапа  БД *" runat="server" />
                            </td>
                            <td style="width:200px;height:20px">
                                <telerik:RadTextBox ID="tbxBackupPath" Width="200px" runat="server" MaxLength="512"/>
                            </td>
                        </tr>

                        <tr>
                            <td id="tdValidationArea" runat="server" style="width:290px;text-align:center;height:0px;padding:0px;" colspan="2">
                                <asp:Panel ID="pnlValidation" runat="server" Visible="true" Width="305px" >
                                    <asp:Label ID="lblValidationText" ForeColor="DarkRed"  runat="server" />
                                </asp:Panel>
                            </td>
                        </tr>

                        <tr>
                            <td style="width:100px;height:20px;text-align:left;padding:5px;">
                                <asp:Button ID="btnUpdate" Width="90px" Text="Сохранить" OnClick="btnUpdate_Click" runat="server" />
                            </td>
                            <td style="width:200px;height:20px;text-align:right;padding:5px;padding-left:0px">
                                <asp:Button ID="btnMakeBackup" Width="130px" Text="Создать бэкап" OnClick="btnMakeBackup_Click" runat="server" />
                            </td>
                        </tr>
                    </tbody>
                </table>

            </ContentTemplate>
        </telerik:RadWindow>

        <asp:LinkButton ID="btnUpdateAction" OnClick="btnUpdateAction_Click" runat="server" />

        <asp:LinkButton ID="btnBackupAction" OnClick="btnBackupAction_Click" runat="server" />

        <asp:LinkButton ID="btnCancelAction" OnClick="btnCancelAction_Click" runat="server" />

        <script type="text/javascript">
            function confirmUpdateCallbackFn(arg) {
                if (arg) //the user clicked OK
                {
                    __doPostBack("<%=btnUpdateAction.UniqueID %>", "");
                }
                else {
                    __doPostBack("<%=btnCancelAction.UniqueID %>", "");
                }
            }
        </script>

        <script type="text/javascript">
            function confirmBackupCallbackFn(arg) {
                if (arg) //the user clicked OK
                {
                    __doPostBack("<%=btnBackupAction.UniqueID %>", "");
                }
                else {
                    __doPostBack("<%=btnCancelAction.UniqueID %>", "");
                }
            }
        </script>

        <telerik:RadWindowManager ID="RadWindowManager1" runat="server" Visible="true" />

    </asp:Content>