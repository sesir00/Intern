<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Checkout.aspx.cs" Inherits="Demofirst.Checkout" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Checkout Page</title>
    <link href="https://stackpath.bootstrapcdn.com/bootstrap/4.5.2/css/bootstrap.min.css" rel="stylesheet" />
    <script src="https://stackpath.bootstrapcdn.com/bootstrap/4.5.2/js/bootstrap.bundle.min.js"></script>
    <script src="https://cdn.jsdelivr.net/npm/sweetalert2@11"></script>
    <meta name="viewport" content="width=device-width, initial-scale=1" />
    <style>
        body {
            font-family: Arial, sans-serif;
            background-color: #f8f9fa;
        }

        .checkout-container {
            margin: 20px auto;
            padding: 20px;
            background-color: #fff;
            border-radius: 8px;
            box-shadow: 0 4px 8px rgba(0, 0, 0, 0.1);
        }

        .checkout-header {
            text-align: center;
            margin-bottom: 30px;
        }

        .checkout-btn {
            background-color: #007bff;
            color: white;
            padding: 10px 20px;
            border: none;
            border-radius: 4px;
            cursor: pointer;
            width: 100%;
        }

            .checkout-btn:hover {
                background-color: #0056b3;
            }

        .order-summary {
            background-color: #f8f9fa;
            padding: 20px;
            border-radius: 8px;
            margin-top: 20px;
        }

        .summary-details {
            text-align: center;
        }

        /* Make the radio buttons responsive */
        .radio-button-container {
            display: flex;
            flex-direction: column;
            gap: 10px;
        }

        .radio-button__input {
            position: absolute;
            opacity: 0;
            width: 0;
            height: 0;
        }

        .radio-button__label {
            padding-left: 30px;
            position: relative;
            font-size: 15px;
            color: #333;
            font-weight: 600;
            cursor: pointer;
            text-transform: uppercase;
            transition: all 0.3s ease;
        }

        .radio-button__custom {
            position: absolute;
            top: 0;
            left: 0;
            width: 20px;
            height: 20px;
            border-radius: 50%;
            border: 2px solid #555;
            transition: all 0.3s ease;
        }

        .radio-button__input:checked + .radio-button__label .radio-button__custom {
            background-color: #4c8bf5;
            border-color: transparent;
            transform: scale(0.8);
            box-shadow: 0 0 20px #4c8bf580;
        }

        .radio-button__input:checked + .radio-button__label {
            color: #4c8bf5;
        }

        .radio-button__label:hover .radio-button__custom {
            transform: scale(1.2);
            border-color: #4c8bf5;
            box-shadow: 0 0 20px #4c8bf580;
        }

        /* Responsive margins and layout */
        @media (max-width: 767px) {
            .checkout-container {
                padding: 15px;
            }
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div class="container checkout-container">
            <div class="checkout-header">
                <h2>Checkout</h2>
            </div>

            <div class="row">
                <!-- Delivery Details -->
                <div class="col-lg-8 col-md-7 col-sm-12">
                    <h4>Delivery Details</h4>
                    <div class="form-group">
                        <label for="name">Your Name</label>
                        <asp:TextBox ID="txtName" runat="server" CssClass="form-control" placeholder="Enter your name"></asp:TextBox>
                    </div>

                    <div class="form-row">
                        <div class="form-group col-md-6">
                            <label for="country">Country</label>
                            <asp:DropDownList ID="ddlCountry" runat="server" CssClass="form-control">
                                <asp:ListItem Text="Select Country" Value="0"></asp:ListItem>
                                <asp:ListItem Text="United States" Value="US"></asp:ListItem>
                                <asp:ListItem Text="Canada" Value="CA"></asp:ListItem>
                                <asp:ListItem Text="Nepal" Value="NP"></asp:ListItem>
                            </asp:DropDownList>
                        </div>

                        <div class="form-group col-md-6">
                            <label for="city">City</label>
                            <asp:TextBox ID="txtCity" runat="server" CssClass="form-control" placeholder="Enter your city"></asp:TextBox>
                        </div>
                    </div>

                    <div class="form-group">
                        <label for="email">Email</label>
                        <asp:TextBox ID="txtEmail" runat="server" CssClass="form-control" placeholder="Enter your email"></asp:TextBox>
                    </div>

                    <div class="form-group">
                        <label for="phone">Phone Number</label>
                        <asp:TextBox ID="txtPhone" runat="server" CssClass="form-control" placeholder="Enter your phone number"></asp:TextBox>
                    </div>

                    <h4>Payment</h4>
                    <div class="form-group">
                        <label for="paymentMethod">Payment Method</label>
                        <div class="radio-button-container">
                            <!-- Credit Card Option -->
                            <div class="radio-button">
                                <input type="radio" id="radio1" class="radio-button__input" name="paymentMethod" value="CreditCard" />
                                <label for="radio1" class="radio-button__label">
                                    <span class="radio-button__custom"></span>
                                    Credit Card
                                </label>
                            </div>

                            <!-- Paypal Option -->
                            <div class="radio-button">
                                <input type="radio" id="radio2" class="radio-button__input" name="paymentMethod" value="Paypal" />
                                <label for="radio2" class="radio-button__label">
                                    <span class="radio-button__custom"></span>
                                    Paypal
                                </label>
                            </div>

                            <!-- Cash on Delivery Option -->
                            <div class="radio-button">
                                <input type="radio" id="radio3" class="radio-button__input" name="paymentMethod" value="COD" />
                                <label for="radio3" class="radio-button__label">
                                    <span class="radio-button__custom"></span>
                                    Cash on Delivery
                                </label>
                            </div>
                        </div>
                    </div>
                </div>

                <!-- Order Summary -->
                <div class="col-lg-4 col-md-5 col-sm-12">
                    <h4>Order Summary</h4>
                    <div class="order-summary">
                        <p>Subtotal: <strong>$<asp:Label ID="lblSubtotal" runat="server" Text="8094.00"></asp:Label></strong></p>
                        <p>VAT: <strong>$<asp:Label ID="lblTax" runat="server" Text="199.00"></asp:Label></strong></p>
                        <p>Shipping: <strong>$<asp:Label ID="lblShipping" runat="server" Text="99.00"></asp:Label></strong></p>
                        <hr />
                        <h5>Total: <strong>$<asp:Label ID="lblTotal" runat="server" Text="8392.00"></asp:Label></strong></h5>
                    </div>

                    <div class="summary-details">
                        <asp:Button ID="btnProceedToPayment" runat="server" Text="Proceed to Payment" CssClass="btn checkout-btn" OnClientClick="return validateForm();" OnClick="ProceedBtn_Click" />
                    </div>
                </div>
            </div>
        </div>
    </form>

    <script type="text/javascript">
        function validateForm() {
            // Get the values from the form fields
            var name = document.getElementById('<%= txtName.ClientID %>').value;
            var country = document.getElementById('<%= ddlCountry.ClientID %>').value;
            var city = document.getElementById('<%= txtCity.ClientID %>').value;
            var email = document.getElementById('<%= txtEmail.ClientID %>').value;
            var phone = document.getElementById('<%= txtPhone.ClientID %>').value;
            var paymentMethod = document.querySelector('input[name="paymentMethod"]:checked');

            // Check if any required field is empty
            if (name === "" || country === "0" || city === "" || email === "" || phone === "" || !paymentMethod) {
                Swal.fire({
                    title: 'Error!',
                    text: 'Please fill in all fields!',
                    icon: 'error',
                    confirmButtonText: 'OK'
                });
                return false; // Return false if validation fails
            }
            return true; // Return true if validation passes
        }

        <%--function btnProceedToPayment_Click() {
            // Perform form validation
            if (validateForm()) {
                // If form is valid, get the values from the form
                var name = document.getElementById('<%= txtName.ClientID %>').value;
                var country = document.getElementById('<%= ddlCountry.ClientID %>').value;
                var city = document.getElementById('<%= txtCity.ClientID %>').value;
                var email = document.getElementById('<%= txtEmail.ClientID %>').value;
                var phone = document.getElementById('<%= txtPhone.ClientID %>').value;

                // Manually capture the selected payment method
                var paymentMethod = document.querySelector('input[name="paymentMethod"]:checked').value;

                // Create the SweetAlert2 popup with the order details
                Swal.fire({
                    title: 'Order Details',
                    html: '<b>Name:</b> ' + name + '<br>' +
                        '<b>Country:</b> ' + country + '<br>' +
                        '<b>City:</b> ' + city + '<br>' +
                        '<b>Email:</b> ' + email + '<br>' +
                        '<b>Phone:</b> ' + phone + '<br>' +
                        '<b>Payment Method:</b> ' + paymentMethod,
                    icon: 'info',
                    confirmButtonText: 'Proceed to Payment'
                }).then((result) => {
                    if (result.isConfirmed) {
                        // Redirect to the payment confirmation page
                        window.location.href = 'LandingPage.aspx';
                    }
                });
            } else {
                // Handle case where form validation fails (optional, already handled in validateForm)
                Swal.fire({
                    title: 'Error',
                    text: 'Please fill in all the required fields.',
                    icon: 'error',
                    confirmButtonText: 'OK'
                });
            }--%>
        }


    </script>
</body>
</html>
