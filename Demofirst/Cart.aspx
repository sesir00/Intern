<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Cart.aspx.cs" Inherits="Demofirst.Cart" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Shopping Cart</title>
    <link href="https://stackpath.bootstrapcdn.com/bootstrap/4.5.2/css/bootstrap.min.css" rel="stylesheet" />
    <script src="https://cdn.jsdelivr.net/npm/sweetalert2@11"></script>
    <style>
        body {
            font-family: Arial, sans-serif;
            background-color: #f8f9fa;
        }

        .cart-container {
            margin: 50px auto;
            padding: 20px;
            max-width: 900px;
            background-color: #fff;
            border-radius: 8px;
            box-shadow: 0 4px 8px rgba(0, 0, 0, 0.1);
        }

        .cart-header {
            text-align: center;
            margin-bottom: 30px;
            position: relative;
        }

        .cart-table {
            width: 100%;
            border-collapse: collapse;
            margin-bottom: 20px;
        }

            .cart-table th,
            .cart-table td {
                padding: 15px;
                text-align: left;
                border-bottom: 1px solid #ddd;
            }

            .cart-table th {
                background-color: #f5f5f5;
                font-weight: bold;
            }

        .cart-actions {
            text-align: right;
            margin-top: 20px;
        }

        .checkout-btn {
            background-color: #007bff;
            color: white;
            padding: 10px 20px;
            border: none;
            border-radius: 4px;
            cursor: pointer;
        }

            .checkout-btn:hover {
                background-color: #0056b3;
            }

        .btn-remove {
            background-color: #dc3545;
            color: white;
            padding: 8px 12px;
            border: none;
            border-radius: 4px;
            cursor: pointer;
        }

            .btn-remove:hover {
                background-color: #c82333;
            }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div class="cart-container">
            <div class="cart-header">
                <a href="LandingPage.aspx" class="home-btn" style="text-decoration:none; color:black;">Home
                       <svg xmlns="http://www.w3.org/2000/svg" width="16" height="16" fill="currentColor" class="bi bi-house" viewBox="0 0 16 16">
                        <path d="M8.707 1.5a1 1 0 0 0-1.414 0L.646 8.146a.5.5 0 0 0 .708.708L2 8.207V13.5A1.5 1.5 0 0 0 3.5 15h9a1.5 1.5 0 0 0 1.5-1.5V8.207l.646.647a.5.5 0 0 0 .708-.708L13 5.793V2.5a.5.5 0 0 0-.5-.5h-1a.5.5 0 0 0-.5.5v1.293zM13 7.207V13.5a.5.5 0 0 1-.5.5h-9a.5.5 0 0 1-.5-.5V7.207l5-5z" />
                       </svg>
                </a>
                <h2>Your Shopping Cart</h2>
            </div>

            <%--            Wrap GridView in UpdatePanel--%>
            <asp:ScriptManager ID="ScriptManager1" runat="server" />

            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>
                    <asp:GridView ID="gvCart" runat="server" AutoGenerateColumns="False" CssClass="cart-table">
                        <Columns>
                            <asp:BoundField DataField="id" HeaderText="Product ID" />

                            <asp:BoundField DataField="product_name" HeaderText="Product Name" />

                            <asp:TemplateField HeaderText="Quantity">
                                <ItemTemplate>
                                    <asp:Button ID="btnDecrease" runat="server" Text="-"
                                        CommandArgument='<%# Eval("id") %>'
                                        OnClick="btnDecrease_Click"
                                        OnClientClick='<%# "return confirmDecrease(this, " + Eval("quantity") + ");" %>'
                                        CssClass="btn-quantity" />


                                    <asp:TextBox ID="txtQuantity" runat="server" Text='<%# Eval("quantity") %>' Width="40px" Enabled="false" />

                                    <asp:Button ID="btnIncrease" runat="server" Text="+"
                                        CommandArgument='<%# Eval("id") %>'
                                        OnClick="btnIncrease_Click"
                                        CssClass="btn-quantity" />
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:BoundField DataField="price" HeaderText="Price" DataFormatString="{0:C2}" />

                            <asp:TemplateField HeaderText="Total">
                                <ItemTemplate>
                                    $<%# Eval("quantity") != DBNull.Value && Eval("price") != DBNull.Value 
                                ? (Convert.ToDouble(Eval("quantity")) * Convert.ToDouble(Eval("price"))).ToString("F2") 
                                : "0.00" %>
                                </ItemTemplate>
                            </asp:TemplateField>


                            <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:Button ID="btnRemove" runat="server" Text="Remove"
                                        CommandArgument='<%# Eval("id") %>'
                                        OnClick="btnRemove_Click" CssClass="btn-remove" />
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                </ContentTemplate>
            </asp:UpdatePanel>


            <%-- Proceed to Checkout button --%>
            <div class="cart-actions">
                <asp:Button ID="btnCheckout" runat="server" Text="Proceed to Checkout" CssClass="checkout-btn" OnClick="btnCheckout_Click" />
            </div>
            <%--            </asp:ScriptManager>--%>




            <%--            <div class="cart-actions">
                <asp:Button ID="btnCheckout" runat="server" Text="Proceed to Checkout" CssClass="checkout-btn" />
            </div>--%>
        </div>
    </form>



    <script type="text/javascript">
        function confirmDecrease(button, quantity) {
            if (quantity == 1) {
                // Use SweetAlert to confirm removal when quantity is 1
                Swal.fire({
                    title: 'Do you want to remove this product from the cart?',
                    showCancelButton: true,
                    confirmButtonText: 'Yes, remove it!',
                    cancelButtonText: 'No, keep it',
                    icon: 'warning'
                }).then((result) => {
                    if (result.isConfirmed) {
                        // Simulate a remove button click
                        var removeButton = button.closest('tr').querySelector(".btn-remove");
                        removeButton.click(); // Calls the server-side removal
                    }
                });
                return false; // Prevent the default button action
            }
            return true; // Allow the server-side action to continue for normal decrease
        }

    </script>


</body>
</html>
