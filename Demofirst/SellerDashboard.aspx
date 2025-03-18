<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SellerDashboard.aspx.cs" Inherits="Demofirst.SellerDashboard" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Seller Dashboard</title>
    <!-- Toastr CSS -->
    <link href="https://cdnjs.cloudflare.com/ajax/libs/toastr.js/latest/toastr.min.css" rel="stylesheet" />

    <!-- Bootstrap CSS (for responsive grid and buttons) -->
    <link href="https://stackpath.bootstrapcdn.com/bootstrap/4.5.2/css/bootstrap.min.css" rel="stylesheet" />

    <!-- Custom CSS for styling -->
    <style>
        body {
            background-color: #f4f7f6;
            font-family: Arial, sans-serif;
        }

        .container {
            max-width: 1200px;
            margin: 0 auto;
            padding: 20px;
        }

        h3 {
            text-align: center;
            margin-bottom: 30px;
        }

        .btn {
            margin-right: 10px;
        }

        .btn-warning, .btn-danger {
            font-size: 14px;
        }

        .grid-container {
            display: flex;
            flex-wrap: wrap;
            gap: 15px;
        }

        .product-card {
            width: 22%;
            background-color: #fff;
            padding: 15px;
            border-radius: 8px;
            box-shadow: 0 2px 10px rgba(0, 0, 0, 0.1);
            transition: all 0.3s ease;
        }

            .product-card:hover {
                transform: translateY(-5px);
                box-shadow: 0 5px 20px rgba(0, 0, 0, 0.15);
            }

            .product-card img {
                max-width: 100%;
                height: auto;
                border-radius: 8px;
            }

        .product-details {
            text-align: center;
            margin-top: 10px;
        }

        .product-name {
            font-size: 18px;
            font-weight: bold;
        }

        .product-price {
            color: #e74c3c;
            font-size: 16px;
            margin-top: 5px;
        }

        .product-actions {
            margin-top: 15px;
        }

        .form-control {
            border-radius: 5px;
            margin-bottom: 15px;
        }

        .logout-btn {
            background-color: #ff4e4e;
            color: #fff;
            padding: 10px 20px;
            border-radius: 5px;
            border: none;
            cursor: pointer;
        }

            .logout-btn:hover {
                background-color: #e74c3c;
            }

        .add-edit-container {
            margin-bottom: 40px;
        }

        .product-table {
            width: 100%;
            border-collapse: collapse;
        }

            .product-table th, .product-table td {
                padding: 10px;
                text-align: center;
                border: 1px solid #ddd;
            }

            .product-table th {
                background-color: #f8f8f8;
            }
    </style>

    <!-- jQuery (Required for Toastr) -->
    <script src="https://code.jquery.com/jquery-3.6.0.min.js"></script>

    <!-- Toastr JS -->
    <script src="https://cdnjs.cloudflare.com/ajax/libs/toastr.js/latest/toastr.min.js"></script>
</head>
<body>
    <form runat="server">
    <div class="container">
        <button class="logout-btn" runat="server" onserverclick="Logout_Click">Log out</button>

        <h3>Seller Dashboard</h3>

        <div class="add-edit-container">
            <asp:TextBox ID="txtProductID" runat="server" Visible="false"></asp:TextBox>
            <asp:TextBox ID="txtProductName" runat="server" placeholder="Product Name" CssClass="form-control"></asp:TextBox>
            <asp:TextBox ID="txtQuantity" runat="server" placeholder="Quantity" CssClass="form-control"></asp:TextBox>
            <asp:TextBox ID="txtPrice" runat="server" placeholder="Price" CssClass="form-control"></asp:TextBox>
            <asp:DropDownList ID="ddlCategory" runat="server" CssClass="form-control">
                <asp:ListItem Text="Select Category" Value="" />
            </asp:DropDownList>

            <asp:FileUpload ID="fileUpload" runat="server" CssClass="form-control" />

            <asp:Button ID="btnSave" runat="server" Text="Save" CssClass="btn btn-success" OnClick="btnSave_Click" />
        </div>

        <asp:GridView ID="gvSellerProducts" runat="server" AutoGenerateColumns="False" CssClass="table table-bordered" OnRowCommand="gvSellerProducts_RowCommand">
            <Columns>
                <asp:TemplateField HeaderText="ID">
                    <ItemTemplate>
                        <asp:Label ID="lblProductID" runat="server" Text='<%# Eval("id") %>' Visible="false" />
                        <asp:Label ID="lblProductIDDisplay" runat="server" Text='<%# Eval("id") %>' />
                    </ItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="Product Name">
                    <ItemTemplate>
                        <asp:Label ID="lblProductName" runat="server" Text='<%# Eval("product_name") %>' />
                    </ItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="Stock">
                    <ItemTemplate>
                        <asp:Label ID="lblQuantity" runat="server" Text='<%# Eval("quantity") %>' />
                    </ItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="Price">
                    <ItemTemplate>
                        <asp:Label ID="lblPrice" runat="server" Text='<%# Eval("price") %>' />
                    </ItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="Product Image">
                    <ItemTemplate>
                        <asp:FileUpload ID="imgProductImage" accept="image/*" runat="server" Width="100px" Height="100px"/>
                    </ItemTemplate>
                </asp:TemplateField>
                

                <asp:TemplateField HeaderText="Actions">
                    <ItemTemplate>
                        <asp:Button ID="btnEdit" runat="server" Text="Edit" CommandName="EditProduct" CommandArgument='<%# Container.DataItemIndex %>' CssClass="btn btn-warning" />
                        <asp:Button ID="btnDelete" runat="server" Text="Delete" CommandName="DeleteProduct" CommandArgument='<%# Eval("id") %>' CssClass="btn btn-danger" OnClientClick="return confirm('Are you sure?');" />
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>

    </div>

    </form>
</body>
</html>
