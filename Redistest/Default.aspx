<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="Redistest.Default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <h1>This Page is "Default.aspx"</h1>
        <hr />

        <asp:Label ID="Label1" runat="server" Text="Server_Name：" AssociatedControlID="TextBox1"></asp:Label>
        <asp:TextBox ID="TextBox1" Enabled="false" runat="server"></asp:TextBox>
        <br />
        <asp:Label ID="Label2" runat="server" Text="Local_Addr：" AssociatedControlID="TextBox2"></asp:Label>
        <asp:TextBox ID="TextBox2" Enabled="false" runat="server"></asp:TextBox>
        <br />
        <asp:Label ID="Label3" runat="server" Text="LOCAL_ADDR：" AssociatedControlID="TextBox3"></asp:Label>
        <asp:TextBox ID="TextBox3" Enabled="false" runat="server"></asp:TextBox>
        <br />

        <hr />
        <asp:Label ID="Label5" runat="server" Text="Redis.Cache.Val：" AssociatedControlID="TextBox5"></asp:Label>
        <asp:TextBox ID="TextBox5" Enabled="false" runat="server"></asp:TextBox>

        <br />
        <br />
        <asp:Button ID="Button1" runat="server" Text="设置Redis.Cache" onclick="Button1_Click" />
        <asp:Button ID="Button2" runat="server" Text="获取Redis.Cache" onclick="Button2_Click" />
        <asp:Button ID="Button3" runat="server" Text="清除Redis.Cache" onclick="Button3_Click"  />
        
        <br />
        <br />
        <label for="nowtime">倒计时：</label><input type="text" disabled="disabled" value="120" id="nowtime" />
    </div>
    </form>
</body>
</html>
