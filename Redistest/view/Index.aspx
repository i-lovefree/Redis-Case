<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Index.aspx.cs" Inherits="Redistest.view.Index" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
    <script type="text/javascript" src="../JS/jquery-2.1.1.js"></script>
    <script type="text/javascript">
        $(function () {
            var interval = setInterval(function () {
                var val = $("#nowtime").val();
                if (val == 0) {
                    clearInterval(interval);
                } else {
                    $("#nowtime").val(val - 1);
                }
            }, 1000);
        });

        function al() {
            alert("s");
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <p>This page is 'Index.aspx'</p>
        <hr />
        <asp:Label ID="Label5" runat="server" Text="Redis.Cache.Val：" AssociatedControlID="TextBox5"></asp:Label>
        <asp:TextBox ID="TextBox5" Enabled="false" runat="server"></asp:TextBox>

        <br />
        <asp:Button ID="Button1" runat="server" Text="设置Redis.Cache" onclick="Button1_Click" />
        <asp:Button ID="Button2" runat="server" Text="获取Redis.Cache" onclick="Button2_Click" />
        <asp:Button ID="Button3" runat="server" Text="清除Redis.Cache" onclick="Button3_Click"  />
        
        <br />
        <label for="nowtime">Session倒计时：</label><input type="text" disabled="disabled" value="120" id="nowtime" />
    </div>
        
    </form>
</body>
</html>
