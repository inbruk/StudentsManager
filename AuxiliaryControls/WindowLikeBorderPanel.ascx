<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="WindowLikeBorderPanel.ascx.cs" Inherits="StudentsManager.PresentationLayer.AuxiliaryControls.WindowLikeBorderPanel" %>

        <div id="OuterDiv" style="width:1280px;height:729px;padding:0px;margin:0px" runat="server">
            <div id="InnerDiv" class="WinLikeBorder_Office2010Silver" runat="server"
                style="width:1270px; height:719px; transform: none; backface-visibility: visible; visibility: visible;padding:5px;">

                <table id="InnerTable" class="WinLikeBorder_rwTable" cellspacing="0" cellpadding="0" style="width:1270px; height:719px;" runat="server">
                    <tbody>
                        <tr class="WinLikeBorder_rwTitleRow">
                            <td class="WinLikeBorder_rwCorner WinLikeBorder_rwTopLeft"> </td>
                            <td class="WinLikeBorder_rwTitlebar">   
                                <span id="TitleSpan" runat="server" />
                            </td>
                            <td class="WinLikeBorder_rwCorner WinLikeBorder_rwTopRight"> </td>
                        </tr>
                        <tr >
                            <td class="WinLikeBorder_rwCorner WinLikeBorder_rwBodyLeft"> </td>
                            <td class="WinLikeBorder_rwWindowContent"> 
                                <asp:Panel ID="pnlContent" runat="server" />
                            </td>
                            <td class="WinLikeBorder_rwCorner WinLikeBorder_rwBodyRight"> </td>
                        </tr>
                        <tr>
                            <td class="WinLikeBorder_rwCorner WinLikeBorder_rwFooterLeft"> </td>
                            <td class="WinLikeBorder_rwFooterCenter"> </td>
                            <td class="WinLikeBorder_rwCorner WinLikeBorder_rwFooterRight"> </td>
                        </tr>
                    </tbody>
                </table>

            </div>
        </div>