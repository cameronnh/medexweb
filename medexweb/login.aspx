<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="medexweb.login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style>
        body{
            background-color:lightpink;
        }
    </style>
</head>
<body>
    <form id="Login" runat="server">
        <div>           
            <table style="margin:auto;border:0px solid white">
                <tr>                   
                        <td><asp:Label ID="Label1" runat="server" Text="Username"></asp:Label></td>  
                        <td><asp:TextBox ID="txtUserName" runat="server"></asp:TextBox></td>                    
                </tr>

                <tr>
                        <td><asp:Label ID="Label2" runat="server" Text="Password"></asp:Label></td>                                        
                        <td><asp:TextBox ID="txtPassword" runat="server"></asp:TextBox></td>                      
                </tr>

                 <tr>
                    <td></td>                   
                        <td><asp:Button ID="btnLogin" runat="server" Text="Login" OnClick="btnLogin_Click" />  
                </tr>

                 <tr>
                    <td></td>                   
                        <td><asp:Label ID="lblErrorMessage" runat="server" Text="Inncorrect Username or Password"></asp:Label></td>                      
                </tr>

            </table>
        </div>
    </form>
</body>
</html>
