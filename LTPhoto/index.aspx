<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="index.aspx.cs" Inherits="LTPhoto.index" %>

<%@ Import Namespace="LTPhoto.Helpers" %>
<%@ Import Namespace="Newtonsoft.Json" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0, minimum-scale=1.0, user-scalable=no, maximum-scale=1.0" />
    <meta name="apple-mobile-web-app-capable" content="yes" />
    <meta name="apple-mobile-web-app-status-bar-style" content="black" />
    <meta name="apple-touch-fullscreen" content="yes" />
    <meta name="format-detection" content="telephone=no" />
    <meta name="HandheldFriendly" content="true" />
    <meta name="MobileOptimized" content="320" />
    <meta name="x5-fullscreen" content="true" />
    <meta name="x5-page-mode" content="app" />
    <meta name="x5-orientation" content="portrait" />
    <title></title>
    <link href="js/reset.css" rel="stylesheet" />
    <link href="js/index.css?r=1.7" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
        <div class="camera-area">
            <div class="camera-up">
                <a href="javascript:;" class="file"></a>
                <input type="file" name="fileToUpload" class="fileToUpload" style="display: none;" accept="image/*" />
            </div>
        </div>
        <div class="upload-progress"><span></span></div>
        <script src="https://cdn.bootcss.com/jquery/3.2.1/jquery.min.js"></script>
        <script src="js/MegaPixImage.js"></script>
        <script src="js/exif.js"></script>
        <script src="js/jpeg_encoder_basic.js"></script>
        <script type="text/javascript" src="js/upload.base64.js"></script>
        <%-- <script src="js/jquery.xml2json.js"></script>--%>
        <script src="js/layer.js"></script>
        <script src="js/index.js?v=2"></script>
        <script>
            var _global_arg = <%=Json%>;
            var _horiz_rect = <%=JsonConvert.SerializeObject(Comm.HorizRect)%>;
        </script>
        <div style="display: none;" id="uform">
            <div class="uiform">
                <h2>参与活动</h2>
                <p class="subtitle">请填写您的联系方式，参与活动并赢取大奖哦！</p>
                <div style="height: 10px;"></div>
                <table>
                    <tr>
                        <th>姓 名：</th>
                        <td>
                            <input type="text" id="tName" placeholder="请输入您的姓名" />
                        </td>
                    </tr>
                    <tr>
                        <th>手 机：</th>
                        <td>
                            <input type="text" id="tMobile" placeholder="请填写手机号码" />
                        </td>
                    </tr>
                </table>
                <div class="control">
                    <a href="javascript:;" id="btnnext" class="button button-green">下一步</a> <a href="javascript:;" id="btncancel" class="button">取 消</a>
                </div>
            </div>
        </div>
    </form>
</body>
</html>
