function reupload() { layer.closeAll(); }


function __msgbox(e) {
    layer.open({
        content: e
        , skin: 'msg'
        , time: 2 //2秒后自动关闭
    });
}
function share() {
    var html = "<div class='share'>" +
        "<img src='background/click.png'/>" +
        "</div>";
    var sharelayer = layer.open({
        type: 1, style: 'border:none; background-color:transparent;',
        content: html,
        shadeClose:true,
        success: function () {
            var w = $(window).width();
            var h = $(window).height();
            $(".share").height(h).width(w);
            $(".share").on("touchstart touchmove click", function(){ layer.close(sharelayer); });
        }
    });
}
$(function () {
    var sucUpload = function (text) {
       
        var html = "<div class='preview'><img src='" + text + "'/>" +
            "<div class='bt'>" +
            "<a href='javascript:reupload()' class='button'>重新上传</a> <a href='javascript:share()' class='button button-green'>分享点赞</a>" +
            "</div>" +
            "</div>";
        var pageii = layer.open({
            type: 1,
            content: html,
            shadeClose: false,
            success: function () {
                var w = $(window).width();
                var h = $(window).height();
                $(".preview").height(h).width(w);
            }
        });
    };

    function selectFile() {
        $('input.fileToUpload').trigger("click");
    }

    function showForm() {
        var formlayer = layer.open({
            type: 1,
            style: 'border:none; background-color:transparent;border-radius: 5px;',
            content: $("#uform").html(),
            fixed:false,
            shadeClose: false,
            success: function (elem) {
                $(elem).find("a.button").click(function () {
                    var buttonid = ($(this).attr("id"));
                    switch (buttonid) {
                        case 'btnnext':
                            var arg = {
                                xm: $(elem).find("#tName").val(),
                                mobile: $(elem).find("#tMobile").val()
                            }
                            if (arg.xm.length < 2) {
                                __msgbox('请填写真实姓名');
                                return;
                            }
                            if (!/^(((13[0-9]{1})|(15[0-9]{1})|(18[0-9]{1}))+\d{8})$/.test(arg.mobile)){
                                __msgbox('手机号不正确');
                                return;
                            }
                            _global_arg = arg;
                            layer.close(formlayer);
                            selectFile();
                            break;
                        case 'btncancel':
                            layer.close(formlayer);
                            break;
                    }
                });
                $(elem).find("input:first").focus();
            }
        });
    }
    $("a.file").click(function () {
        if (_global_arg == null){
            showForm();
        } else{
            selectFile();
        }
         
    });

    $('input.fileToUpload').UploadImg({
        url: "b64.ashx",
        width: '640',
        //height: '320',
        quality: '0.8', //压缩率，默认值为0.8
        // 如果quality是1 宽和高都未设定 则上传原图
        mixsize: '10000000',
        type: 'image/png,image/jpg,image/jpeg,image/pjpeg,image/gif,image/bmp,image/x-png',
        before: function (blob) {
            // var imageMy = $('#my_face');
            // imageMy.attr('src', blob);
        },
        error: function (res) {
            __msgbox('error:' + res);
        },
        success: function (res) {
            __msgbox('生成成功!长按图片分享吧..亲');
            sucUpload(res);
            $(".upload-progress").hide().find("span").empty();
            setTimeout(function () { share(); }, 500);
            setTimeout(function(){ $(".bt").fadeIn(); }, 700);
        },
        progress: function (percent) { $(".upload-progress").show().find("span").html(percent + "%"); }
    });

    //$(".camera-area").fileUpload({
    //    "url": "savetofile.ashx",
    //    "file": "myFile",

    //    uploadComplete: function (text) {

    //    }
    //});

    var fixRect = function () {
        var w = $(window).width();
        var h = $(window).height();
        var imgw = 640;
        var imgh = 1020;
        var rs = _horiz_rect.split(',');
        var rect = {
            x: parseInt($.trim(rs[0])),
            y: parseInt($.trim(rs[1])),
            width: parseInt($.trim(rs[2])),
            height: parseInt($.trim(rs[3]))
        };

        $(".camera-area").css({
            top: (rect.y / imgh) * h,
            left: (rect.x / imgw) * w,
            width: (rect.width / imgw) * w,
            height: (rect.height / imgh) * h

        });

    };
    $(window).on("resize", fixRect);
    fixRect();
})