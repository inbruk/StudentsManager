<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Global.master" Inherits="StudentsManager.PresentationLayer.Default" CodeFile="Default.aspx.cs" %>

    <asp:Content ID="Content1" ContentPlaceHolderID="title" runat="Server">
        Страница входа
    </asp:Content>

    <asp:Content ID="bottomContent" ContentPlaceHolderID="bottom" runat="Server">
        <telerik:RadWindowManager ID="RadWindowManager1" runat="server">
            <Windows>
                <telerik:RadWindow ID="LoginWindow" runat="server" Width="256px" Height="200px" Modal="true" Behaviors="None" 
                    Title="Вход в систему" VisibleStatusbar="false"  >
                    <ContentTemplate>
                        <div style="width:240px;height:160px">
                            <div class="formRow">
                                <div class="formItem">
                                    <div class="formLabelShort">
                                        <asp:Label Text="Логин :" CssClass="formLabelControl" runat="server" Height="20px" Width="100px"/>
                                    </div>
                                    <div class="formValueShort">
                                        <telerik:RadTextBox ID="tbxLogin" CssClass="formTextBoxControl" runat="server" Height="20px" Width="100px" Text="Administrator"/>
                                    </div>
                                </div>
                            </div>
                            <div class="formRow">
                                <div class="formItem">
                                    <div class="formLabelShort">
                                        <asp:Label Text="Пароль :" CssClass="formLabelControl" runat="server" Height="20px" Width="100px" />
                                    </div>
                                    <div class="formValueShort">
                                        <telerik:RadTextBox ID="tbxPassword" runat="server" Height="20px" Width="100px" Text="admin123" />
                                    </div>
                                </div>
                            </div>
                            <asp:Panel ID="pnlValidation" runat="server" Visible="false" >
                                <div class="formRow">
                                    <div class="formItem" style="width:220px;padding-left:10px;padding-right:10px;">
                                        <asp:Label ID="lblValidationText" ForeColor="DarkRed" Text="" runat="server" />
                                    </div>
                                </div>
                            </asp:Panel> 
                            <div class="formButtonRow" style="">
                                <asp:Button ID="btnLogOn" Text="Войти  " Width="100px" runat="server" OnClick="btnLogOn_Click" OnClientClick="return true;" />
                            </div>
                        </div>
                    </div>
                    <asp:Panel ID="pnlRunTheLoginWindow" runat="server">
                        <asp:HiddenField ID="runTheLoginWindow" ClientIDMode="Static" Value="true" runat="server" />
                    </asp:Panel>
                    </ContentTemplate>
                </telerik:RadWindow>
            </Windows>
        </telerik:RadWindowManager>

        <script type="text/javascript">
            window.onload = function ()
            {
                var runTheLoginWindow = document.getElementById('runTheLoginWindow');
                if (runTheLoginWindow != null)
                {
                    window.radopen(null, "LoginWindow");
                }
            }
        </script>

    </asp:Content>

