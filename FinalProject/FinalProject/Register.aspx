<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Register.aspx.cs" Inherits="FinalProject.Register" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link href="Content/bootstrap.css" rel="stylesheet" />
    <title></title>
    <style type="text/css">
        .auto-style1 {
            width: 358px;
        }
        .auto-style2 {
            width: 205px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div style="padding-top: 150px">
           <div class="container well well-lg" style="width: 471px; margin-top:150px; height: 491px;";>
                   <div class="titlehead" style="text-align: center">
                        <h3>Register New User:</h3>
                   </div>
                   <br />
                   <table style="width:100%;" class="table">
                            <tr>
                                <td class="auto-style2">
                                    <br />
                                    Username:</td>
                                <td>
                                    <asp:TextBox class="form-control" ID="txtRegUsername" runat="server" TabIndex="1" OnTextChanged="txtRegUsername_TextChanged"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td class="auto-style2">
                                    <br />
                                    Password:<br />
                               <br />
                                </td>
                                <td class="auto-style1"><asp:TextBox class="form-control" ID="txtRegPassword" runat="server" TabIndex="2" TextMode="Password"></asp:TextBox></td>
                            </tr>
                            <tr>
                                <td class="auto-style2">
                                    <br />
                                    Confirm Password: 
                                    <br />
                                    <br />
                                </td>
                                <td><asp:TextBox class="form-control" ID="txtRegConfPass" runat="server" TabIndex="3" TextMode="Password"></asp:TextBox></td>
                            </tr>
                       </table>
                    <asp:Label ID="lblRegStatus" runat="server" ForeColor="#006600"></asp:Label>
                   <br />
                    <asp:Button ID="btnRegister" style="display:inline-block" Text="Register" class="btn btn-info" runat="server" OnClick="btnRegister_Click"/>
                   &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 
               
                    <a href="javascript: history.go(-1);">Back</a>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    <a href="Login.aspx">Login</a>
                   <br />
               </div>
               </div>
    </form>
</body>
</html>
