<%@ Page Title="Farmer Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="FarmerPage.aspx.cs" Inherits="WebApplication3.FarmerPage" %>

<asp:Content ID="MainContent" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container mt-4">
    <div class="row">
        <div class="col-md-6">
            <h2>Add New Product</h2>
            <asp:ValidationSummary ID="ValidationSummary1" runat="server" CssClass="text-danger alert alert-danger" ValidationGroup="AddProductGroup" />
            <div class="mb-3">
                <asp:Label ID="lblName" runat="server" Text="Name:" CssClass="form-label"></asp:Label>
                <asp:TextBox ID="txtName" runat="server" CssClass="form-control" ValidationGroup="AddProductGroup"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtName" ErrorMessage="Name is required." CssClass="text-danger" ValidationGroup="AddProductGroup" />
            </div>
            <div class="mb-3">
                <asp:Label ID="lblCategory" runat="server" Text="Category:" CssClass="form-label"></asp:Label>
                <asp:TextBox ID="txtCategory" runat="server" CssClass="form-control" ValidationGroup="AddProductGroup"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtCategory" ErrorMessage="Category is required." CssClass="text-danger" ValidationGroup="AddProductGroup" />
            </div>
            <div class="mb-3">
                <asp:Label ID="lblProductionDate" runat="server" Text="Production Date:" CssClass="form-label"></asp:Label>
                <asp:TextBox ID="txtProductionDate" runat="server" CssClass="form-control" TextMode="Date" ValidationGroup="AddProductGroup"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtProductionDate" ErrorMessage="Production Date is required." CssClass="text-danger" ValidationGroup="AddProductGroup" />
            </div>
            <asp:Button ID="btnAddProduct" runat="server" Text="Add Product" CssClass="btn btn-primary" OnClick="btnAddProduct_Click" ValidationGroup="AddProductGroup" />
        </div>
    </div>

    <div class="row mt-4">
        <div class="col-md-12">
            <h2>Product List</h2>
            <asp:GridView ID="gvProducts" runat="server" CssClass="table table-striped" AutoGenerateColumns="false" DataKeyNames="Id"
                OnRowEditing="gvProducts_RowEditing" OnRowUpdating="gvProducts_RowUpdating" OnRowCancelingEdit="gvProducts_RowCancelingEdit" OnRowDeleting="gvProducts_RowDeleting">
                <Columns>
                    <asp:BoundField DataField="Name" HeaderText="Name" />
                    <asp:BoundField DataField="Category" HeaderText="Category" />
                    <asp:BoundField DataField="ProductionDate" HeaderText="Production Date" DataFormatString="{0:yyyy-MM-dd}" />

                    <asp:CommandField ShowEditButton="true" ShowDeleteButton="true" />
                </Columns>
            </asp:GridView>
        </div>
    </div>
</div>
</asp:Content>
