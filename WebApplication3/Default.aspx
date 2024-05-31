<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="WebApplication3._Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <main>
        <div class="row">
        </div>

        <asp:Panel ID="FarmerUsersPanel" runat="server" Visible="false">
            <div class="container mt-4">
                <h2 class="mb-4">Farmers</h2>
                <div class="mb-3">
                    <asp:Label runat="server" AssociatedControlID="Username" CssClass="form-label">Username</asp:Label>
                    <asp:TextBox runat="server" ID="Username" CssClass="form-control" ValidationGroup="AddUserGroup" />
                    <asp:RequiredFieldValidator runat="server" ControlToValidate="Username" CssClass="text-danger" ErrorMessage="The username field is required." ValidationGroup="AddUserGroup" />
                </div>
                <div class="mb-3">
                    <asp:Label runat="server" AssociatedControlID="Email" CssClass="form-label">Email</asp:Label>
                    <asp:TextBox runat="server" ID="Email" CssClass="form-control" TextMode="Email" ValidationGroup="AddUserGroup" />
                    <asp:RequiredFieldValidator runat="server" ControlToValidate="Email" CssClass="text-danger" ErrorMessage="The email field is required." ValidationGroup="AddUserGroup" />
                </div>
                <div class="mb-3">
                    <asp:Label runat="server" AssociatedControlID="Password" CssClass="form-label">Password</asp:Label>
                    <asp:TextBox runat="server" ID="Password" TextMode="Password" CssClass="form-control" ValidationGroup="AddUserGroup" />
                    <asp:RequiredFieldValidator runat="server" ControlToValidate="Password" CssClass="text-danger" ErrorMessage="The password field is required." ValidationGroup="AddUserGroup" />
                </div>
                <asp:Button runat="server" ID="AddUserButton" OnClick="AddUserButton_Click" Text="Add User" CssClass="btn btn-outline-dark mb-4" ValidationGroup="AddUserGroup" />

                <asp:GridView ID="FarmerUsersGridView" runat="server" CssClass="table table-striped" AutoGenerateColumns="false" DataKeyNames="Id"
                    OnRowEditing="FarmerUsersGridView_RowEditing" OnRowUpdating="FarmerUsersGridView_RowUpdating" 
                    OnRowCancelingEdit="FarmerUsersGridView_RowCancelingEdit" OnRowDeleting="FarmerUsersGridView_RowDeleting">
                    <Columns>
                        <asp:BoundField DataField="UserName" HeaderText="User Name" />
                        <asp:BoundField DataField="Email" HeaderText="Email" />
                        <asp:CommandField ShowEditButton="true" ShowDeleteButton="true" />
                    </Columns>
                </asp:GridView>
            </div>

            <div class="container mt-4">
                <h2 class="mb-4">Filter Products</h2>
                <div class="mb-3">
                    <asp:Label runat="server" AssociatedControlID="ddlCategory" CssClass="form-label">Category</asp:Label>
                    <asp:DropDownList runat="server" ID="ddlCategory" CssClass="form-control" ValidationGroup="FilterProductsGroup"></asp:DropDownList>
                </div>
                <div class="mb-3">
                    <asp:Label runat="server" AssociatedControlID="txtStartDate" CssClass="form-label">Start Date</asp:Label>
                    <asp:TextBox runat="server" ID="txtStartDate" CssClass="form-control" TextMode="Date" ValidationGroup="FilterProductsGroup"></asp:TextBox>
                </div>
                <div class="mb-3">
                    <asp:Label runat="server" AssociatedControlID="txtEndDate" CssClass="form-label">End Date</asp:Label>
                    <asp:TextBox runat="server" ID="txtEndDate" CssClass="form-control" TextMode="Date" ValidationGroup="FilterProductsGroup"></asp:TextBox>
                </div>
                <asp:Button runat="server" ID="btnFilterProducts" OnClick="btnFilterProducts_Click" Text="Filter" CssClass="btn btn-outline-dark mb-4" ValidationGroup="FilterProductsGroup" />

                <asp:GridView ID="gvProducts" runat="server" CssClass="table table-striped" AutoGenerateColumns="false">
                    <Columns>
                        <asp:BoundField DataField="Name" HeaderText="Name" />
                        <asp:BoundField DataField="Category" HeaderText="Category" />
                        <asp:BoundField DataField="ProductionDate" HeaderText="Production Date" DataFormatString="{0:yyyy-MM-dd}" />
                    </Columns>
                </asp:GridView>
            </div>
        </asp:Panel>
    </main>
</asp:Content>
