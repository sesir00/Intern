<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="LandingPage.aspx.cs" Inherits="Demofirst.LandingPage" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Buyer Page</title>
    <!-- Bootstrap CSS for grid layout -->
    <link href="https://stackpath.bootstrapcdn.com/bootstrap/4.5.2/css/bootstrap.min.css" rel="stylesheet" />

    <!-- SweetAlert2 CSS -->
    <link href="https://cdn.jsdelivr.net/npm/sweetalert2@11/dist/sweetalert2.min.css" rel="stylesheet" />

    <!-- Font Awesome CDN -->
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/6.1.0/css/all.min.css"  />


    <style>
        body {
            background-color: #f9fafb; /* Light neutral background */
            font-family: Arial, sans-serif;
        }

        .container {
            max-width: 1200px;
            margin: 0 auto;
            padding: 20px;
            position: relative;
        }

        h3 {
            font-size: 24px;
            font-weight: bold;
            color: #333; /* Dark text for contrast */
            margin-bottom: 20px;
        }

        .product-card {
            width: 22%;
            background-color: #fff; /* Pure white for contrast */
            padding: 15px;
            border-radius: 10px;
            box-shadow: 0 4px 12px rgba(0, 0, 0, 0.05); /* Soft shadow for depth */
            margin: 10px;
            display: inline-block;
            vertical-align: top;
            transition: transform 0.3s ease;
        }

            .product-card:hover {
                transform: translateY(-5px); /* Lift effect on hover */
            }

            .product-card img {
                max-width: 100%;
                height: auto;
                border-radius: 8px; /* Slight rounding of image */
            }

        .product-details {
            margin-top: 10px;
            text-align: center;
        }

        .product-name {
            font-size: 18px;
            font-weight: bold;
            color: #1f2937; /* Darker gray for contrast */
        }

        .product-price {
            color: #f97316; /* Soft blue for price */
            font-size: 16px;
            margin-top: 5px;
        }

        /* Flex container for buttons */
        .button-container {
            display: flex;
            justify-content: center;
            gap: 10px; /* Space between buttons */
            margin-top: 10px;
        }

        .btn-buy, .btn-add-to-cart {
            padding: 10px 20px;
            border-radius: 8px;
            border: none;
            font-size: 14px;
            font-weight: bold;
            cursor: pointer;
            transition: background-color 0.3s ease, transform 0.2s ease;
        }

        .btn-buy {
            background-color: #3b82f6; /* Soft blue background */
            color: #fff;
        }

            .btn-buy:hover {
                background-color: #2563eb; /* Darker blue on hover */
                transform: translateY(-2px);
            }

            .btn-buy.disabled {
                background-color: #9ca3af; /* Gray for disabled state */
                cursor: not-allowed;
            }

        .btn-add-to-cart {
            background-color: #f97316; /* Vibrant orange for contrast */
            color: white;
            display: inline-flex;
            align-items: center;
        }

            .btn-add-to-cart:hover {
                background-color: #ea580c; /* Darker orange on hover */
                transform: translateY(-2px);
            }

            .btn-add-to-cart .cart-icon {
                margin-right: 8px;
                font-size: 16px;
            }

        /* Out of Stock Styling */
        .out-of-stock {
            color: #ef4444; /* Bright red for out-of-stock warning */
            font-size: 18px; /* Increased font size for better visibility */
            font-weight: bold;
            margin-top: 5px;
        }

        /* Logout Button Styling */
        .logout-btn {
            background-color: #f59e0b; /* Gold-ish yellow */
            color: #fff;
            padding: 10px 20px;
            border-radius: 8px;
            border: none;
            cursor: pointer;
            position: absolute;
            top: 20px;
            right: 20px; /* Align to top-right corner */
        }

            .logout-btn:hover {
                background-color: #d97706; /* Darker yellow on hover */
            }




        .cart-button {
            background-color: #007bff; /* Blue, matches MUI primary color */
            color: white;
            border: none;
            border-radius: 50%; /* Circular like MUI IconButton */
            width: 48px;
            height: 48px;
            display: flex;
            align-items: center;
            justify-content: center;
            cursor: pointer;
            transition: background-color 0.3s;
        }

            .cart-button:hover {
                background-color: #0056b3;
            }

        .cart-icon {
            font-size: 24px; /* Matches MUI default icon size */
        }

        .tooltip {
            position: relative;
            display: inline-block;
        }

            .tooltip .tooltiptext {
                visibility: hidden;
                width: 120px;
                background-color: #555;
                color: #fff;
                text-align: center;
                border-radius: 6px;
                padding: 5px;
                position: absolute;
                z-index: 1;
                bottom: 125%;
                left: 50%;
                margin-left: -60px;
                opacity: 0;
                transition: opacity 0.3s;
            }

            .tooltip:hover .tooltiptext {
                visibility: visible;
                opacity: 1;
            }

            .cart-icon{
                float:right;
                margin: 1% 3% 0 auto;
                resize:both;
                cursor: pointer; /* Add this to show pointer cursor */
            }







        /* Media Queries for Responsiveness */

        /* Mobile Devices */
        @media (max-width: 576px) {
            .product-card {
                width: 100%; /* Full width on mobile */
                margin-bottom: 20px;
            }

            .product-name {
                font-size: 16px; /* Slightly smaller text for mobile */
            }

            .product-price {
                font-size: 14px; /* Slightly smaller text for mobile */
            }

            .btn-buy, .btn-add-to-cart {
                font-size: 12px;
                padding: 8px 16px; /* Smaller padding on mobile */
            }

            .out-of-stock {
                font-size: 16px; /* Slightly smaller but still prominent */
            }

            /* Move the logout button to the center */
            .logout-btn {
                position: relative;
                top: 0;
                right: 0;
                text-align: center;
                margin: 10px 0;
            }
        }

        /* Tablets */
        @media (min-width: 577px) and (max-width: 768px) {
            .product-card {
                width: 48%; /* Two products per row on tablets */
                margin-bottom: 20px;
            }

            .btn-buy, .btn-add-to-cart {
                font-size: 14px;
                padding: 10px 20px;
            }
        }

        /* Laptops and Larger Devices */
        @media (min-width: 769px) {
            .product-card {
                width: 22%; /* Four products per row */
            }

            .btn-buy, .btn-add-to-cart {
                font-size: 14px;
                padding: 10px 20px;
            }
        }
    </style>

    <!-- jQuery -->
    <script src="https://code.jquery.com/jquery-3.6.0.min.js"></script>

    <!-- SweetAlert2 JS -->
    <script src="https://cdn.jsdelivr.net/npm/sweetalert2@11/dist/sweetalert2.all.min.js"></script>
</head>
<body>
    <form id="form1" runat="server">
        <div class="container">

            <div class="text-right">
                <div class="tooltip">
                    <asp:Button ID="btnCart" runat="server" CssClass="cart-button" OnClick="btnCart_Click" ToolTip="View Cart" />
                    <span class="material-icons cart-icon" style="position: absolute;">shopping_cart</span>
                    <span class="tooltiptext">Click to view cart</span>
                </div>
            </div>

            <div class="cart-icon" onclick="redirectToCart()">
                <i class="fa-solid fa-cart-shopping fa-bounce"></i>
            </div>

            <button class="logout-btn" runat="server" onserverclick="Logout_Click">Log out</button>
            <h3>Welcome to the Buyer Page</h3>
            <div class="row">
                <asp:Repeater ID="rptProducts" runat="server">
                    <ItemTemplate>
                        <div class="product-card">
                            <%--<img src='<%# Eval("ImagePath") %>' alt='<%# Eval("product_name") %>' />--%>
                            <div class="product-details">
                                <p class="product-name"><%# Eval("product_name") %></p>
                                <p class="product-price" runat="server">$<%# Eval("price") %></p>

                                <asp:Literal ID="ltQuantity" runat="server" Text='<%# GetQuantityText(Eval("quantity")) %>'></asp:Literal><br />

                                <%--                                <asp:Button ID="btnBuy" runat="server" Text="Buy Now"  CommandArgument='<%# Eval("id") + "," + Eval("price") + "," + Eval("product_name") %>'  CssClass='<%# Eval("quantity").ToString() == "0" ? "btn-buy disabled" : "btn-buy" %>' Enabled='<%# Eval("quantity").ToString() != "0" %>' OnClick="btnBuy_Click" />--%>

                                <asp:PlaceHolder ID="phBuyButton" runat="server" Visible='<%# Eval("quantity").ToString() != "0" %>'>
                                    <asp:PlaceHolder ID="PlaceHolder1" runat="server" Visible='<%# Eval("quantity").ToString() != "0" %>'>
                                        <div class="button-container">
                                            <asp:Button ID="btnBuy" runat="server" Text="Buy Now"
                                                CommandArgument='<%# Eval("id") + "," + Eval("price") + "," + Eval("product_name") + "," + Eval("quantity") %>'
                                                CssClass="btn-buy" OnClick="btnBuy_Click" />

                                            <asp:Button ID="btnaddtocart" runat="server" Text="Add to Cart"
                                                CommandArgument='<%# Eval("id") + "," + Eval("price") + "," + Eval("product_name") + "," + Eval("quantity") %>'
                                                CssClass="btn-add-to-cart" OnClick="btnCart_Click" />
                                            <%--  <button class="btn-add-to-cart">
                                                <span class="cart-icon">🛒</span> Add to Cart
                                            </button>--%>
                                        </div>
                                    </asp:PlaceHolder>
                                </asp:PlaceHolder>

                            </div>
                        </div>
                    </ItemTemplate>
                </asp:Repeater>
            </div>
        </div>
    </form>


    <script>
    function redirectToCart() {
        window.location.href = 'Cart.aspx'; // Redirects to Cart.aspx
    }
</script>
</body>
</html>
