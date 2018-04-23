<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="admin.aspx.cs" Inherits="LTPhoto.admin" %>

<%@ Register TagPrefix="webdiyer" Namespace="Wuqi.Webdiyer" Assembly="AspNetPager, Version=7.5.1.0, Culture=neutral, PublicKeyToken=fb0a0fe055d40fd4" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <link href="js/reset.css" rel="stylesheet" />
    <link href="js/admin.css?v=3" rel="stylesheet" />
    <script src="https://cdn.bootcss.com/jquery/3.2.1/jquery.min.js"></script>
    <!-- 最新版本的 Bootstrap 核心 CSS 文件 -->
    <link rel="stylesheet" href="https://cdn.bootcss.com/bootstrap/3.3.7/css/bootstrap.min.css" integrity="sha384-BVYiiSIFeK1dGmJRAkycuHAHRg32OmUcww7on3RYdg4Va+PmSTsz/K68vbdEjh4u" crossorigin="anonymous"/>
</head>
<body>

    <script src="js/jquery.lazyload.min.js"></script>
    <form id="form1" runat="server"><div class="header">
            <div class="container">
                母亲的美少女时代照片集
                <span class="search">
                    <asp:TextBox ID="txKey" runat="server" PlaceHolder="支持姓名，手机号模糊搜索" ></asp:TextBox> 
                    <asp:Button ID="btnSearch" runat="server" Text="搜索"  OnClick="btnSearch_OnClick"/>
                    <asp:LinkButton runat="server" ID="lkCancel" Text="取消搜索" Visible="False" OnClick="lkCancel_OnClick"></asp:LinkButton>
                </span>
            </div>
        </div>
        <div class="person-list clearfix">
            <asp:Repeater runat="server" ID="rp" OnItemDataBound="rp_OnItemDataBound">
                <ItemTemplate>
                    <div class="person">
                        <div class="imgwrapper">
                            <a href="<%#Eval("PhotoSnapPath") %>" target="_blank">
                                <img  data-original="<%#Eval("PhotoSnapPath") %>" class="lazy" /></a>
                        </div>
                        <div class="person-info">
                            <p>
                                <label>姓名：</label><%#Sugg(Eval("XM")) %>
                            </p>
                            <p>
                                <label>手机：</label><%#Sugg(Eval("Mobile")) %>
                            </p>

                            <p>
                                <label>时间：</label><%#Eval("SnapTime") %>
                            </p>
                            <div style="text-align: center; padding-top: 5px;">
                                <asp:Button runat="server" ID="btnDelete" OnClick="btnDelete_OnClick" CssClass="button" Text="删除" />
                            </div>
                        </div>
                    </div>
                </ItemTemplate>
            </asp:Repeater>
        </div>
        <div class="page-wrapper">
            <div class="page-wrapper-cc">
                <webdiyer:AspNetPager CssClass="pages" ShowCustomInfoSection="Left" CustomInfoHTML="第%CurrentPageIndex%页，共%PageCount%页，第页显示%PageSize%条" CurrentPageButtonClass="cpb" PagingButtonSpacing="0" ID="pager1" runat="server" RecordCount="228"
                    FirstPageText="首页" LastPageText="尾页" NextPageText="下一页" PrevPageText="上一页" OnPageChanged="AspNetPager4_OnPageChanged">
                </webdiyer:AspNetPager>
            </div>
        </div>
        <div style="height: 60px;">
        </div>
    </form>
    <script>
        $(function () {
            $(".imgwrapper").each(function () {
                var img = $(this).children("img");
                $(this).width(img.width()).height(img.height());
            });
            $("img.lazy").lazyload({
                effect: "fadeIn"
            });
        });
    </script>
</body>
</html>
