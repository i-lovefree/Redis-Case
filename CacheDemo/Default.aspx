<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="CacheDemo.Default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>测试</title>
    <script src="js/jquery-1.3.2.js"></script>
    <script type="text/javascript">
        
        function create(type, value) {
            var url = "ajaxwork.aspx";
            url += value != "" ? "?type=" + type + "&key=" + value : "?type=" + type;

            $.post(url, function (data) {
                var rs = new Function("return" + data)();

                $("#span" + type).html("用时：" + rs.time + "&nbsp;&nbsp;&nbsp;&nbsp;已完成：" + rs.curr + "%");

                $(".msg" + type).css("width", rs.curr + "%");

                if (rs.curr != "100") {
                    create(type, "get");
                }
            });
        }

        function create2(type2, value2) {
            var url2 = "ajaxwork2.aspx";
            url2 += value2 != "" ? "?type2=" + type2 + "&key2=" + value2 : "?type2=" + type2;

            $.post(url2, function (data2) {
                var rs2 = new Function("return" + data2)();

                $("#span1" + type2).html("用时：" + rs2.time + "&nbsp;&nbsp;&nbsp;&nbsp;已完成：" + rs2.curr + "%");

                $(".msg1" + type2).css("width", rs2.curr + "%");

                if (rs2.curr != "100") {
                    create2(type2, "get");
                }
            });
        }

    </script>
</head>
<body>
    <form id="form1" runat="server">

        <div>
            <asp:Button ID="Button1" Enabled="false" runat="server" Text="添加测试" OnClick="Button1_Click" />
            <asp:Button ID="Button2" Enabled="false" runat="server" Text="获取测试" OnClick="Button2_Click" />
            <asp:Button ID="Button3" Enabled="false" runat="server" Text="删除测试" OnClick="Button3_Click" />
        </div>

        <hr />

         <div>
            <br />
            <div style="width:500px; height:20px;border:1px solid">
                <div class="msg1" style="background-color:red; height:20px; width:0%;"></div>
            </div>
            <input type="button" onclick="create(1, '')" value="Redis缓存,增加测试" />&nbsp;&nbsp;&nbsp;&nbsp;
            <span id="span1"></span>
        </div>
        <div>
            <br />
            <div style="width:500px; height:20px;border:1px solid">
                <div class="msg2" style="background-color:red; height:20px; width:0%;"></div>
            </div>
            <input type="button" onclick="create(2, '')" value="Redis缓存,查询测试" />&nbsp;&nbsp;&nbsp;&nbsp;
            <span id="span2"></span>
        </div>
        <div>
            <br />
            <div style="width:500px; height:20px;border:1px solid">
                <div class="msg3" style="background-color:red; height:20px; width:0%;"></div>
            </div>
            <input type="button" onclick="create(3, '')" value="Redis缓存,删除测试" />&nbsp;&nbsp;&nbsp;&nbsp;
            <span id="span3"></span>
        </div>
        <hr />
        <div>
            <br />
            <div style="width:500px; height:20px;border:1px solid">
                <div class="msg11" style="background-color:red; height:20px; width:0%;"></div>
            </div>
            <input type="button" onclick="create2(1, '')" value="HttpRuntime缓存,增加测试" />&nbsp;&nbsp;&nbsp;&nbsp;
            <span id="span11"></span>
        </div>
        <div>
            <br />
            <div style="width:500px; height:20px;border:1px solid">
                <div class="msg12" style="background-color:red; height:20px; width:0%;"></div>
            </div>
            <input type="button" onclick="create2(2, '')" value="HttpRuntime缓存,查询测试" />&nbsp;&nbsp;&nbsp;&nbsp;
            <span id="span12"></span>
        </div>
        <div>
            <br />
            <div style="width:500px; height:20px;border:1px solid">
                <div class="msg13" style="background-color:red; height:20px; width:0%;"></div>
            </div>
            <input type="button" onclick="create2(3, '')" value="HttpRuntime缓存,删除测试" />&nbsp;&nbsp;&nbsp;&nbsp;
            <span id="span13"></span>
        </div>

        <hr />
        <label for="id_key">获取key：</label><input type="text" id="id_key" value="stu1" />&nbsp;&nbsp;&nbsp;&nbsp;
        <input type="button" onclick="getKey()" value="HttpRuntime缓存,获取Key" />
        <script type="text/javascript">
            function getKey(){
                var key = $("#id_key").val();
                var url = "getcache.aspx?key="+key;

                $.post(url, function (data) {
                    //alert(data);
                    var rs = new Function("return" + data)();
                    alert(rs.val);
                    //var jsonstr = JSON.parse(rs.val);
                    //alert(jsonstr[999]);
                });
            }

        </script>
    </form>
</body>
</html>
