<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="Demofirst.Login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Login</title>
        <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.0/dist/css/bootstrap.min.css" rel="stylesheet" />
            <!-- jQuery (Required for Toastr.js) -->
        <script src="https://cdnjs.cloudflare.com/ajax/libs/jquery/3.6.0/jquery.min.js"></script>
            <!-- Toastr JS -->
        <script src="https://cdnjs.cloudflare.com/ajax/libs/toastr.js/latest/toastr.min.js"></script>


    <style>
        .container {
            max-width: 400px;
            margin-top: 50px;
        }
    </style>
</head>
<body>
    <div class="container">
    <form id="form1" runat="server" DefaultButton="submitbtn">
        <h2 class="text-center mb-4">Login Form</h2>

        <div class="mb-3">
            <asp:TextBox ID="Username" runat="server" CssClass="form-control" Placeholder="Username"></asp:TextBox>
            <asp:RequiredFieldValidator ID="rfvUsername" runat="server" ControlToValidate="Username" ErrorMessage="Username is required." CssClass="text-danger" Display="Dynamic" />
        </div>

        <div class="mb-3">
            <asp:TextBox ID="Password" runat="server" CssClass="form-control" Placeholder="Password" TextMode="Password"></asp:TextBox>
            <asp:RequiredFieldValidator ID="rfvPassword" runat="server" ControlToValidate="Password" ErrorMessage="Password is required." CssClass="text-danger" Display="Dynamic" />
            <asp:RegularExpressionValidator ID="revPassword" runat="server" ControlToValidate="Password" ErrorMessage="Password must be at least 6 characters long." CssClass="text-danger" Display="Dynamic" 
                ValidationExpression="^.{6,}$" />
        </div>

        <div class="d-grid">
            <asp:Button ID="submitbtn" runat="server" CssClass="btn btn-primary" Text="Submit" OnClick="submitbtn_Click" />
        </div>

        <!-- Don't have an account? -->
        <div class="text-center mt-3">
            <p>Don't have an account? <a href="Signup.aspx" class="text-primary">Sign up</a></p>
        </div>
    </form>
    </div>
    
    <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.3.0/dist/js/bootstrap.bundle.min.js"></script>
    <!-- Toastr JS -->
    <script src="https://cdnjs.cloudflare.com/ajax/libs/toastr.js/latest/toastr.min.js"></script>
</body>
</html>
