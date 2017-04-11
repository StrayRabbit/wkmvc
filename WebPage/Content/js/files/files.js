$(function () {
    //获取IFrame传递过来的值
    var dialog = top.dialog.get(window);
    var data = dialog.data;
    $("#fileUrl").val(data);
    if (data != '' && data != undefined) {
        //文件定位
        var p = data.substring(0, data.lastIndexOf('/') + 1);
        $('#path').val(p);
        $('#jsons').val('{"path":"' + data + '"}');
    }
    signfiles.initFiles($("#path").val());
    //上传
    $(".sign-upload").click(function () {
        $("#fileUp").unbind();
        $("#fileUp").click();
    });
    //保存
    $(".btn-save").click(function () {
        dialog.close($("#jsons").val());
        dialog.remove();
    });
    //返回上级目录
    $(".btn-higher-up").click(function () {
        signfiles.OpenParentFolder();
    });
});
var signfiles = {
    signUpFile: function (upInput) {
        var subUrl = "/Com/Upload/SignUpFile?fileUp=fileUrl&isThumbnail=" + $("#isThumbnail").prop("checked") + "&isWater=" + $("#isWater").prop("checked");
        $("#forms").ajaxSubmit({
            beforeSubmit: function () {
                dig.loading("正在上传");
                $(".sign-upload").attr("disabled", "disabled");
            },
            success: function (data) {
                if (data.Status == "y") {
                    var res = eval('(' + data.Data + ')');
                    $('#fileUrl').val(res.path);
                    $('#jsons').val(data.Data);
                    //定位
                    var pa = res.path.substring(0, res.path.lastIndexOf('/') + 1);
                    $('#path').val(pa);
                    signfiles.initFiles(pa);
                    sweetAlert.close();
                } else {
                    dig.error(data.Msg);
                }
                $(".sign-upload").attr("disabled", false);

            },
            error: function (e) {
                sweetAlert.close();
                $(".sign-upload").attr("disabled", false);
                console.log(e);
            },
            url: subUrl,
            type: "post",
            dataType: "json",
            timeout: 600000
        });
    },
    initFiles: function (path) {
        if (path == $("#spath").val()) {
            $(".btn-higher-up").attr("disabled", "disabled");
        } else {
            $(".btn-higher-up").attr("disabled", false);
        }
        $.post("/Com/Upload/GetFileData", { path: path }, function (res) {
            if (res.Status == "y") {
                if (res.Data == "" || res.Data == null) {
                    dig.error("该目录下没有文件了！");
                    signfiles.OpenParentFolder();
                } else {
                    $("#filesPanel").empty();
                    $("#tlist").tmpl(res.Data).appendTo('#filesPanel');
                }
            } else if (res.Status == "empty") {
                $("#filesPanel").html('<div class="alert alert-warning text-center"><span style="font-size:16px;"><i class="fa fa-warning"></i>&nbsp;没有找到任何文件</span></div>');
            }
            else {
                dig.error(res.Msg);
            }
        }, "json");
    },
    OpenFolder: function (path) {
        var npath = $("#path").val() + path + "/";
        $("#path").val(npath);
        signfiles.initFiles(npath);
    },
    OpenParentFolder: function () {
        var p = $("#path").val();
        if (p == $("#spath").val()) return;
        p = p.substring(0, p.lastIndexOf('/'));
        p = p.substring(0, p.lastIndexOf('/')) + "/";
        $("#path").val(p);
        signfiles.initFiles(p);
    },
    SetFile: function (path, ext,size,name) {
        $("#fileUrl").val($("#path").val() + path);
        $('#jsons').val('{"path":"' + $("#path").val() + path + '","thumbpath":"' + $("#path").val() + "thumb_" + path + ' ","ext":"' + ext + '","unitsize":"' + size + '","newname":"' + name + '","oldname":"' + name + '"}');
    }
}