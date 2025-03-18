<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Signup.aspx.cs" Inherits="Demofirst.Signup" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Signup</title>
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
        <form id="form1" runat="server" class="border p-4 shadow-sm rounded"  DefaultButton="submitbtn">
            <h2 class="text-center mb-4">Signup Form</h2>
            
            <div class="mb-3">
                <asp:TextBox ID="Username" runat="server" CssClass="form-control" Placeholder="Username"></asp:TextBox>
                <asp:RequiredFieldValidator ID="rfvUsername" runat="server" ControlToValidate="Username" ErrorMessage="Username is required." CssClass="text-danger" Display="Dynamic" />
            </div>
            
            <div class="mb-3">
                <asp:TextBox ID="Email" runat="server" CssClass="form-control" Placeholder="Email"></asp:TextBox>
                <asp:RequiredFieldValidator ID="rfvEmail" runat="server" ControlToValidate="Email" ErrorMessage="Email is required." CssClass="text-danger" Display="Dynamic" />
                <asp:RegularExpressionValidator ID="revEmail" runat="server" ControlToValidate="Email" ErrorMessage="Invalid email format." CssClass="text-danger" Display="Dynamic" 
                    ValidationExpression="^[^\s@]+@[^\s@]+\.[^\s@]+$" />
            </div>
            
            <div class="mb-3">
                <asp:TextBox ID="Password" runat="server" CssClass="form-control" Placeholder="Password" TextMode="Password"></asp:TextBox>
                <asp:RequiredFieldValidator ID="rfvPassword" runat="server" ControlToValidate="Password" ErrorMessage="Password is required." CssClass="text-danger" Display="Dynamic" />
                <asp:RegularExpressionValidator ID="revPassword" runat="server" ControlToValidate="Password" ErrorMessage="Password must be at least 6 characters long." CssClass="text-danger" Display="Dynamic" 
                    ValidationExpression="^.{6,}$" />
            </div>
            
            <!-- Role Selection (Buyer/Seller) -->
            <div class="mb-3">
                <label class="form-label">Select Role:</label>
                <div class="form-check">
                    <asp:RadioButton ID="Buyer" runat="server" CssClass="form-check-input" GroupName="UserRole" Checked="true" />
                    <label class="form-check-label">Buyer</label>
                </div>
                <div class="form-check">
                    <asp:RadioButton ID="Seller" runat="server" CssClass="form-check-input" GroupName="UserRole" />
                    <label class="form-check-label">Seller</label>
                </div>
            </div>

            <div class="d-grid">
                <asp:Button ID="submitbtn" runat="server" CssClass="btn btn-primary" Text="Submit" OnClick="submitbtn_Click" />
            </div>
        </form>
    </div>
    
    <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.3.0/dist/js/bootstrap.bundle.min.js"></script>
    <!-- Toastr JS -->
<script src="https://cdnjs.cloudflare.com/ajax/libs/toastr.js/latest/toastr.min.js"></script>

</body>
</html>
