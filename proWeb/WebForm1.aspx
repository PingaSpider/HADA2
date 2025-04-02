<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="WebForm1.aspx.cs" Inherits="proWeb.WebForm1" %>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <h2>Products management</h2>
    
    <div style="margin-bottom: 20px;">
        <asp:Label ID="lblMessage" runat="server" ForeColor="Red"></asp:Label>
    </div>
    
    <div class="form-row">
        <asp:Label ID="lblCode" runat="server" Text="Code"></asp:Label>
        <asp:TextBox ID="txtCode" runat="server" Width="350px"></asp:TextBox>
    </div>
    
    <div class="form-row">
        <asp:Label ID="lblName" runat="server" Text="Name"></asp:Label>
        <asp:TextBox ID="txtName" runat="server" Width="350px"></asp:TextBox>
    </div>
    
    <div class="form-row">
        <asp:Label ID="lblAmount" runat="server" Text="Amount"></asp:Label>
        <asp:TextBox ID="txtAmount" runat="server" Width="200px"></asp:TextBox>
    </div>
    
    <div class="form-row">
        <asp:Label ID="lblCategory" runat="server" Text="Category"></asp:Label>
        <asp:DropDownList ID="ddlCategory" runat="server" Width="200px"></asp:DropDownList>
    </div>
    
    <div class="form-row">
        <asp:Label ID="lblPrice" runat="server" Text="Price"></asp:Label>
        <asp:TextBox ID="txtPrice" runat="server" Width="200px"></asp:TextBox>
    </div>
    
    <div class="form-row">
        <asp:Label ID="lblCreationDate" runat="server" Text="Creation Date"></asp:Label>
        <asp:TextBox ID="txtCreationDate" runat="server" Width="350px"></asp:TextBox>
    </div>
    
    <div class="buttons">
        <asp:Button ID="btnCreate" runat="server" Text="Create" />
        <asp:Button ID="btnUpdate" runat="server" Text="Update" />
        <asp:Button ID="btnDelete" runat="server" Text="Delete" />
        <asp:Button ID="btnRead" runat="server" Text="Read" />
        <asp:Button ID="btnReadFirst" runat="server" Text="Read First" />
        <asp:Button ID="btnReadPrev" runat="server" Text="Read Prev" />
        <asp:Button ID="btnReadNext" runat="server" Text="Read Next" />
    </div>
</asp:Content>