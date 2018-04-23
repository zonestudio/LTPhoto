$.fn.UploadImg = function (o) {
    this.change(function () {
        var file = this.files['0'];
        console.log(file);
        $('#error').html(file.type);
        if (file.size && file.size > o.mixsize) {
            o.error('大小超过限制');
            this.value = '';
        } else if (o.type && o.type.indexOf(file.type) < 0) {
            o.error('格式不正确');
            this.value = '';
        } else {
            var URL = URL || webkitURL;
            var blob = URL.createObjectURL(file);
            o.before(blob);
            o['loading'] = layer.open({
                type: 2
                , content: '照片上传中'
            });
            _compress(blob, file);
            this.value = '';
        }
    });

    function _compress(blob, file) {
        var img = new Image();
        img.src = blob;
        img.onload = function () {
            var canvas = document.createElement('canvas');
            var ctx = canvas.getContext('2d');
            if (!o.width && !o.height && o.quality == 1) {
                var w = this.width;
                var h = this.height;
            } else {
                var w = o.width || this.width;
                var h = o.height || w / this.width * this.height;
            }
            $(canvas).attr({
                width: w,
                height: h
            });
            ctx.drawImage(this, 0, 0, w, h);
            var base64 = canvas.toDataURL(file.type, (o.quality || 0.8) * 1);
            if (navigator.userAgent.match(/iphone/i)) {
                var myorientation = 0;
                EXIF.getData(file, function () {
                    //图片方向角  
                    var Orientation = null;
                    // alert(EXIF.pretty(this));  
                    EXIF.getAllTags(this);
                    //alert(EXIF.getTag(this, 'Orientation')); 
                    myorientation = EXIF.getTag(this, 'Orientation');
                    //return;  

                    //                      alert(myorientation.toString());
                    var mpImg = new MegaPixImage(img);
                    mpImg.render(canvas, {
                        maxWidth: w,
                        maxHeight: h,
                        quality: o.quality || 0.8,
                        orientation: myorientation
                    });
                    base64 = canvas.toDataURL(file.type, o.quality || 0.8);
                    _ajaximg(base64, file.type);
                });
            }

            // 修复android
            if (navigator.userAgent.match(/Android/i)) {
                var encoder = new JPEGEncoder();
                base64 = encoder.encode(ctx.getImageData(0, 0, w, h), o.quality * 100 || 80);
                _ajaximg(base64, file.type);
            }

        };
    }

    var uploadProgress = function(evt) {
        if (evt.lengthComputable){
            var percentComplete = Math.round(evt.loaded * 100 / evt.total);
            if(percentComplete<100)
            o.progress(percentComplete);
        }
    };
    var uploadComplete = function(evt) {
        layer.close(o['loading']);
        o.success(evt.target.responseText);
    };
    function _ajaximg(base64, type, file) {
       

        var fd = new FormData();//创建表单数据对象
        
        fd.append("base64Img", base64);//将文件添加到表单数据中
        fd.append("xm", _global_arg.xm);//姓名
        fd.append("mobile", _global_arg.mobile);//手机

        // funs.previewImage(file);//上传前预览图片，也可以通过其他方法预览txt
        
        var xhr = new XMLHttpRequest();
        xhr.upload.addEventListener("progress", uploadProgress, false);//监听上传进度
        xhr.addEventListener("load", uploadComplete, false);
        xhr.addEventListener("error", function(e){ o.error(e); }, false);
        xhr.open("POST", o.url);
        xhr.send(fd);

        //$.post(o.url, {
        //    base64Img: base64
        //}, function (res){
        //    layer.close(l);
        //    o.success(res);
            
        //});

    }
};