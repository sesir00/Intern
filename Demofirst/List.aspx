<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="List.aspx.cs" Inherits="Demofirst.List" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container mt-4">
        <h2 class="text-center">Sales Records</h2>

         <!-- Add loading indicator -->
        <div id="loading" class="text-center mb-3">
            <div class="spinner-border text-primary" role="status">
                <span class="visually-hidden">Loading...</span>
            </div>
        </div>

        <table id="salesTable" class=" table table-striped table-bordered">
            <thead>
                <tr>
                    <th>Sales ID</th>
                    <th>Product Name</th>
                    <th>Quantity</th>
                    <th>Total Price</th>
                    <th>Sale Date</th>
                </tr>
            </thead>
            <tbody>
                <asp:Repeater ID="rptSales" runat="server">
                    <ItemTemplate>
                        <tr>
                            <td><%# Eval("id") %></td>
                            <td><%# Eval("id") %></td>
                            <td><%# Eval("quantity_purchased") %></td>
                            <td><%# Eval("total_price", "{0:C}") %></td>
                            <td><%# Eval("purchase_date", "{0:yyyy-MM-dd}") %></td>
                        </tr>
                    </ItemTemplate>
                </asp:Repeater>
            </tbody>
        </table>
    </div>

    <script>
        $(document).ready(function () {
            // Hide loading indicator when table is initialized
            $('#loading').hide();

            $('#salesTable').DataTable({
                // Add DataTable options
                "order": [[0, "desc"]],
                "responsive": true,
                "initComplete": function () {
                    $('#loading').hide();
                }
            });        });
    </script>

</asp:Content>
