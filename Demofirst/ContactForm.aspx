<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ContactForm.aspx.cs" Inherits="Demofirst.ContactForm" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
    <style>
        #form1{
            

        }
        #btnSubmit{
            background-color:crimson;
        }
    </style>
<body>
    <form id="form1" runat="server">
        <h2>Contact us</h2>
        <div>
            <asp:Label ID="conName" runat="server" Text="Name:"></asp:Label>
            <asp:TextBox ID="Name" runat="server"></asp:TextBox>
            <asp:RequiredFieldValidator ID="rfvname" runat="server" ControlToValidate="Name" ErrorMessage="Name is required" ForeColor="Red"></asp:RequiredFieldValidator>
            <br />

            <asp:Label ID="conEmail" runat="server" Text="Email"></asp:Label>
            <asp:TextBox ID="Email" runat="server"></asp:TextBox>
            <asp:RequiredFieldValidator ID="rfvEmail" runat="server" ControlToValidate="Email" ErrorMessage="Email is required" ForeColor="Red"></asp:RequiredFieldValidator>
           <%-- <asp:RegularExpressionValidator ID="revEmail" runat="server" ControlToValidate="Email" ErrorMessage="Invalid Email Form" ValidationExpression="^[a-zA-Z0-9._/%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]$" ForeColor="Red"></asp:RegularExpressionValidator>--%>
            <asp:RegularExpressionValidator
                ID="RegularExpressionValidator1"
                runat="server"
                ControlToValidate="Email"
                ErrorMessage="Invalid Email Format"
                ValidationExpression="^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$"
                ForeColor="Red">
            </asp:RegularExpressionValidator>

            <br />

            <asp:Label ID="conMessage" runat="server" Text="Message"></asp:Label>
            <asp:TextBox ID="Message" runat="server"></asp:TextBox>
            <asp:RequiredFieldValidator ID="rfvMessage" runat="server" ControlToValidate="Message" ErrorMessage="Message cannot be empty" ForeColor="Red"></asp:RequiredFieldValidator>


            <br />

            <asp:Button ID="btnSubmit" runat="server" Text="Submit" OnClick="btnSubmit_Click" />
            <br />

            <asp:Label ID="conResult" runat="server" ForeColor="Green"></asp:Label>
        </div>
    </form>
</body>
</html>
