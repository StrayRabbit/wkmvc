$(function () {
    //开始备份
    $(".btn-save").click(function () {
        $.ajax({
            type: "Post",
            url: $("#Url").val(),
            data: {},
            dataType: "json",
            beforeSend: function () {
                dig.loading("正在备份");
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
});