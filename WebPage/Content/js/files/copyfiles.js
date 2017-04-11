$(function () {
    signfiles.initFiles($("#path").val());
    //保存
    $(".btn-save").click(function () {
        $.ajax({
            type: "Post",
            url: "/Com/Upload/CopyFiles",
            data: { files: $("#operationfiles").val(), path: $("#path").val() },
            dataType: "json",
            beforeSend: function () {
                dig.loading("正在复制文件");
                $(".btn-save").attr("disabled", "disabled");
            },
            success: function (data) {
                if (data.Status == "y") {
                    var dialog = top.dialog.get(window);
                    dig.successcallback(data.Msg, function () {
                        if (dialog == "undefined" || dialog == undefined) {
                            location.reload();
                        }
                        else {
                            dialog.close('yes').remove();
                        }
                    });
                } else {
                    dig.error(data.Msg);
                }
                $(".btn-save").attr("disabled", false);

            },
            error: function (e) {
                sweetAlert.close();
                $(".btn-save").attr("disabled", false);
                console.log(e);
            }
        });
    });
    //返回上级目录
    $(".btn-higher-up").click(function () {
        signfiles.OpenParentFolder();
    });
});
var signfiles = {
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
    }
}