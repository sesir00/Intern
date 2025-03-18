<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Users.aspx.cs" Inherits="Demofirst.Dao.Users" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>User Management</title>
       <!-- Toastr CSS -->
    <link href="https://cdnjs.cloudflare.com/ajax/libs/toastr.js/latest/toastr.min.css" rel="stylesheet"/>

    <!-- jQuery (Required for Toastr) -->
    <script src="https://code.jquery.com/jquery-3.6.0.min.js"></script>

    <!-- Toastr JS -->
    <script src="https://cdnjs.cloudflare.com/ajax/libs/toastr.js/latest/toastr.min.js"></script>


</head>
<body>
   <form id="form1" runat="server">
        <h2>User Management</h2>
        
        <asp:GridView ID="gvUsers" runat="server" AutoGenerateColumns="False" DataKeyNames="Id"
            OnRowEditing="gvUsers_RowEditing"
            OnRowCancelingEdit="gvUsers_RowCancelingEdit" OnRowDeleting="gvUsers_RowDeleting">
            <Columns>
                <asp:BoundField DataField="Id" HeaderText="ID" ReadOnly="True" />
                <asp:BoundField DataField="Name" HeaderText="Name" />
                <asp:BoundField DataField="Email" HeaderText="Email" />
                <asp:BoundField DataField="Phone" HeaderText="Phone" />
                <asp:BoundField DataField="Address" HeaderText="Address" />
                <asp:BoundField DataField="Type" HeaderText="Type" />
                <asp:BoundField DataField="is_active" HeaderText="IsActive" />
                <asp:TemplateField>
                    <ItemTemplate>
                        <!-- Edit button that redirects to EditUser.aspx with the Id as a query parameter -->
                        <a href="EditUser.aspx?id=<%# Eval("Id") %>">Edit</a>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField>
                    <ItemTemplate>
                        <!-- Delete button that redirects to DeleteUser.aspx with the Id as a query parameter -->
                        <a href="DeleteUser.aspx?id=<%# Eval("Id") %>">Delete</a>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>

       <!-- Hidden field to store User ID for updates -->
        <asp:HiddenField ID="hfUserId" runat="server" />
        <h3 id="formTitle" runat="server">Add User</h3>

        <asp:TextBox ID="txtName" runat="server" Placeholder="Name"></asp:TextBox>
        <asp:TextBox ID="txtEmail" runat="server" Placeholder="Email"></asp:TextBox>
        <asp:TextBox ID="txtPhone" runat="server" Placeholder="Phone"></asp:TextBox>
        <asp:TextBox ID="txtAddress" runat="server" Placeholder="Address"></asp:TextBox>
        <asp:TextBox ID="txtType" runat="server" Placeholder="Type"></asp:TextBox>
        <asp:DropDownList ID="ddlIsActive" runat="server">
            <asp:ListItem Text="Active" Value="A"></asp:ListItem>
            <asp:ListItem Text="Inactive" Value="I"></asp:ListItem>
        </asp:DropDownList>

        <asp:Button ID="btnSubmit" runat="server" Text="Add User" OnClick="btnAddUser_Click" CssClass="btn btn-primary" />
    </form>




    <script>
    function highlightForm() {
        document.getElementById("formTitle").style.color = "blue";
    }
</script>
</body>
</html>
