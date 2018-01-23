<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="FinalProject.Login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link href="Content/bootstrap.css" rel="stylesheet" />
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div style="padding-top: 150px">
            <div class="container well well-lg" style="width: 471px";>
                <div class="row">
                    <div class="col-md-12">
                        <div class="titlehead" style="text-align: center">
                            <h3> ∙  Niagara Catholic District School Board  ∙ </h3>
                        </div>
                        <br />
                        Username:
                        <asp:TextBox class="form-control" ID="txtLoginUsername" runat="server" TabIndex="1"></asp:TextBox>
                        <br />
                       Password:
                        &nbsp;<asp:TextBox class="form-control" ID="txtLoginPassword" runat="server" TabIndex="2" TextMode="Password"></asp:TextBox>
                        <br />
                        <br />
                        <asp:Button ID="btnLogin" style="display:inline-block" class="btn btn-info" runat="server" OnClick="btnLogin_Click" TabIndex="3" Text="Log In" />
                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <a href="Register.aspx">Register</a>
                            
                    </div>
                </div>
            </div>

          </div>
    </form>
</body>
</html>
