(function () {
    $(".spiner-example").show();
})(jQuery)
$(window).load(function () { $(".spiner-example").hide(); });
$(function () {
    //初始化CheckBox
    $(".icheck_box").iCheck({
        checkboxClass: 'icheckbox_minimal-red',
        radioClass: 'iradio_minimal-red',
        increaseArea: '20%' // optional
    });
    //ios7 复选框
    var elems = Array.prototype.slice.call(document.querySelectorAll('.js-switch'));
    elems.forEach(function (html) {
        var switchery = new Switchery(html, { color: 'rgb(237, 85, 101)' });
    });
    //全选 反选
    $('input[name="checkall"]').on('ifChecked', function (event) {
        $("input[name='checkbox_name']").iCheck('check');
    });
    $('input[name="checkall"]').on('ifUnchecked', function (event) {
        $("input[name='checkbox_name']").iCheck('uncheck');
    });
    //下拉菜单
    $('.select2').select2();
    //工具提示
    $("[data-toggle='tooltip']").tooltip();
    //使用col插件实现表格头宽度拖拽
    $(".table").colResizable();
   //列表选择删除
    $('#delete').click(function () {
        var vals = '';
        $('input[name="checkbox_name"]:checked').each(function () {
            vals += $(this).val() + ',';
        });
        if (vals == '' || vals == ',') {
            dig.error( "对不起，请选中您要操作的记录！");
            return;
        }
        var url = window.location.href.split('?')[0].toLowerCase();
        if (url.lastIndexOf('/index') > 0) {
            url = url.substring(0, url.indexOf('/index'));
        }
        url = url + '/Delete';
        var msg = "删除记录后不可恢复，您确定吗？";
        dig.confirm("删除确认", msg, function () {
            $.post(url, { idList: vals }, function (res) {
                if (res.Status == "y") {
                    dig.successcallback('删除成功', function () {
                        window.location.reload();
                    });
                }
                else {
                    dig.error(res.Msg);
                }
            }, "json");
        });
    });
    //刷新按钮
    $(".reload-link").mouseover(function () {
        $(this).find("i").addClass("fa-spin");
    }).mouseout(function () { $(this).find("i").removeClass("fa-spin"); });
    //返回
    $("#btn-dig-close").click(function () {
        dig.close();
    });
    //点击表格，选中/取消 复选框
    $("#dataTable tr").slice(1).each(function (g) {
        var p = $(this);
        $(this).children().slice(1).click(function () {
            if($($(p.children()[0]).children()[0]).children().eq(0).prop("checked"))
            {
                $($(p.children()[0]).children()[0]).iCheck("uncheck");
            }
            else
            {
                $($(p.children()[0]).children()[0]).iCheck("check");
            }
        });
    });
    $("#emailTable tr").slice(0).each(function (g) {
        var p = $(this);
        $(this).children().slice(1).click(function () {
            if ($($(p.children()[0]).children()[0]).children().eq(0).prop("checked")) {
                $($(p.children()[0]).children()[0]).iCheck("uncheck");
            }
            else {
                $($(p.children()[0]).children()[0]).iCheck("check");
            }
        });
    });
    //时间
    laydate.skin('molv');   
});
//时间选择器
function ldate(obj, format) {
    if (format == '') { format = "YYYY-MM-DD"; }
    var de = {
        elem: '#' + obj,
        format: format,
        festival: true,
        min: '2016-01-01 00:00:00', //设定最小日期
        max: '2099-06-16 23:59:59', //设定最大日期
        istime: true,
        istoday: false,
        choose: function (datas) {
            //end.min = datas; //开始日选好后，重置结束日的最小日期
            //end.start = datas //将结束日的初始值设定为开始日
        }
    };
    laydate(de);
}
//验证按钮权限
function VaildatePermission(v) {
    var permissionlist = v
    if (permissionlist != '' && permissionlist != undefined) {
        var permission = '';
        $.each(permissionlist, function (p, t) {
            permission += t.PERVALUE + ',';
        });
        permission = permission.toLowerCase();
        $('.ibox a[action]').each(function () {
            var action = $(this).attr('action');
            if (permission.indexOf(action.toLowerCase() + ',') < 0) {
                $(this).remove();
            }
        });
        $('.ibox a[listaction]').each(function () {
            var listaction = $(this).attr('listaction');
            if (permission.indexOf(listaction.toLowerCase() + ',') < 0) {
                $(this).remove();
            }
        });
    }
}
//加载提示 
var dig = {
    reload: function () {
        location.reload();
    },
    addPage: function (t,u, w, h,f) {
        top.dialog({
            title: t,
            url: u,
            width: w,
            height: h,
            onremove: function () {},
            onclose: f
        }).showModal();
    },
    remove: function () {
        var n = top.dialog.get(window);
        n.close().remove();
    },
    close: function () {
        var n = top.dialog.get(window);
        n.close();
    },
    success: function (t) {
        swal({
            title: t,
            text: '',
            type: "success",
            confirmButtonColor: "#DD6B55"
        });
    },
    successcallback: function (t, i) {
        swal({
            title: t,
            text: "",
            type: "success",
            showCancelButton: false,
            closeOnConfirm: false,
            confirmButtonText: "确定",
            confirmButtonColor: "#ec6c62"
        }, function () {
            i && i()
        })
    },
    error: function (t) {
        swal({
            title: t,
            text: '',
            type: "error",
            confirmButtonColor: "#DD6B55"
        });
    },
    confirm: function (t,n, i) {
        swal({
            title: t,
            text: n,
            type: "warning",
            showCancelButton: true,
            closeOnConfirm: false,
            confirmButtonText: "是的,我确定",
            confirmButtonColor: "#ec6c62",
            closeOnConfirm: false,
            closeOnCancel: false
        }, function (isConfirm) {
            if (isConfirm) { i && i(); }else {swal({title: '取消操作',text: '您已取消本次操作 :)',type: "error",confirmButtonColor: "#DD6B55"});}
        });
    },
    loading: function (t) {
        swal({
            title: '',
            text: t,
            imageUrl: "/Content/images/login/puffred.svg",
            imageSize: "50x50",
            showConfirmButton: false
        });
    },
    msgsuccess: function (n) {
        swal({
            title: n,
            text: '',
            type: "success",
            timer: 2000,
            showConfirmButton: false
        });
    },    
    msgerror: function (n) {
        swal({
            title: n,
            text: '',
            type: "error",
            timer: 2000,
            showConfirmButton: false
        });
    },
    msgwarning: function (n) {
        swal({
            title: n,
            text: '',
            type: "warning",
            timer: 2000,
            showConfirmButton: false
        });
    },
    upload: function (o, t) {
        top.dialog({
            title: '选择文件',
            url: '/Com/Upload/FileMain',
            width: 800,
            height: 480,
            data: o, // 给 iframe 的数据
            onclose: t,
            oniframeload: function () {
            }
        }).showModal();
        return false;
    },
    fileOperation: function (o,f,u,t) {
        top.dialog({
            title: '选择目标文件夹',
            url: u+'?files='+f,
            width: 800,
            height: 480,
            data: o, // 给 iframe 的数据
            onclose: t,
            oniframeload: function () {
            }
        }).showModal();
        return false;
    },
    logout: function () {
        swal({
            title: "退出系统",
            text: "您确定退出系统吗？",
            type: "warning",
            showCancelButton: true,
            closeOnConfirm: false,
            confirmButtonText: "是的,我确定",
            confirmButtonColor: "#ec6c62",
            closeOnConfirm: false,
            closeOnCancel: false
        }, function (isConfirm) {
            if (isConfirm) { window.location.href = "/sys/account"; } else { swal({ title: '取消操作', text: '您已取消本次操作 :)', type: "error", confirmButtonColor: "#DD6B55" }); }
        });
    },
    msg:function(t,n)
    {
        swal(t, n)
    }
}
//增删改提交ajax
var SubAjax = {
    Loading: function () {
        $(".btn-save").attr("disabled", "disabled").find("span").html("正在保存中...")
    },
    Success: function (result) {
        if (result.Status == undefined) {
            document.writeln(result);
        } else if (result.Status == "y") {
            var dialog = top.dialog.get(window);
            dig.successcallback(result.Msg, function () {                
                if (dialog == "undefined" || dialog == undefined)
                {
                    location.reload();
                }
                else
                {                    
                    dialog.close('yes').remove();
                }
                
            });
        } else {
            dig.error(result.Msg);
            SubAjax.Complete();
        }
    },
    SuccessBack: function (result) {
        if (result.Status == "y") {
            dig.successcallback(result.Msg, function () {
                if (result.ReUrl == 'undefined' || result.ReUrl == '' || result.ReUrl == undefined) {
                    history.go(-1);
                }
                else { window.location.href = result.ReUrl; }
            });
        } else {
            dig.error(result.Msg);
            SubAjax.Complete();
        }
    },
    Failure: function () {
        dig.error("网络超时,请稍后再试...");
        SubAjax.Complete();
    },
    Complete: function () {
        $(".btn-save").attr("disabled", false).find("span").html("确定保存");
    }
};
//Toastr提示框
var toasInfo = {
    message_t:function(n)
    {
        toastr.options = {
            "closeButton": true,
            "debug": true,
            "progressBar": true,
            "positionClass": "toast-top-right",
            "showDuration": "400",
            "hideDuration": "1000",
            "timeOut": "10000",
            "extendedTimeOut": "1000",
            "showEasing": "swing",
            "hideEasing": "linear",
            "showMethod": "fadeIn",
            "hideMethod": "fadeOut"
        }
        toastr.info(n);
    },
    info_t: function (t, n) {
        toastr.options = {
            "closeButton": true,
            "debug": true,
            "progressBar": true,
            "positionClass": "toast-top-right",
            "showDuration": "400",
            "hideDuration": "1000",
            "timeOut": "10000",
            "extendedTimeOut": "1000",
            "showEasing": "swing",
            "hideEasing": "linear",
            "showMethod": "fadeIn",
            "hideMethod": "fadeOut"
        }
        toastr.info(n,t);
    },
    message_b: function (n) {
        toastr.options = {
            "closeButton": true,
            "debug": true,
            "progressBar": true,
            "positionClass": "toast-bottom-right",
            "showDuration": "400",
            "hideDuration": "1000",
            "timeOut": "10000",
            "extendedTimeOut": "1000",
            "showEasing": "swing",
            "hideEasing": "linear",
            "showMethod": "fadeIn",
            "hideMethod": "fadeOut"
        }
        toastr.info(n);
    },
    info_b: function (t, n) {
        toastr.options = {
            "closeButton": true,
            "debug": true,
            "progressBar": true,
            "positionClass": "toast-bottom-right",
            "showDuration": "400",
            "hideDuration": "1000",
            "timeOut": "10000",
            "extendedTimeOut": "1000",
            "showEasing": "swing",
            "hideEasing": "linear",
            "showMethod": "fadeIn",
            "hideMethod": "fadeOut"
        }
        toastr.info(n, t);
    }
}
var toasSuccess = {
    message_t: function (n) {
        toastr.options = {
            "closeButton": true,
            "debug": true,
            "progressBar": true,
            "positionClass": "toast-top-right",
            "showDuration": "400",
            "hideDuration": "1000",
            "timeOut": "10000",
            "extendedTimeOut": "1000",
            "showEasing": "swing",
            "hideEasing": "linear",
            "showMethod": "fadeIn",
            "hideMethod": "fadeOut"
        }
        toastr.success(n);
    },
    info_t: function (t, n) {
        toastr.options = {
            "closeButton": true,
            "debug": true,
            "progressBar": true,
            "positionClass": "toast-top-right",
            "showDuration": "400",
            "hideDuration": "1000",
            "timeOut": "10000",
            "extendedTimeOut": "1000",
            "showEasing": "swing",
            "hideEasing": "linear",
            "showMethod": "fadeIn",
            "hideMethod": "fadeOut"
        }
        toastr.success(n, t);
    },
    message_b: function (n) {
        toastr.options = {
            "closeButton": true,
            "debug": true,
            "progressBar": true,
            "positionClass": "toast-bottom-right",
            "showDuration": "400",
            "hideDuration": "1000",
            "timeOut": "10000",
            "extendedTimeOut": "1000",
            "showEasing": "swing",
            "hideEasing": "linear",
            "showMethod": "fadeIn",
            "hideMethod": "fadeOut"
        }
        toastr.success(n);
    },
    info_b: function (t, n) {
        toastr.options = {
            "closeButton": true,
            "debug": true,
            "progressBar": true,
            "positionClass": "toast-bottom-right",
            "showDuration": "400",
            "hideDuration": "1000",
            "timeOut": "10000",
            "extendedTimeOut": "1000",
            "showEasing": "swing",
            "hideEasing": "linear",
            "showMethod": "fadeIn",
            "hideMethod": "fadeOut"
        }
        toastr.success(n, t);
    }
}
var toasWarning = {
    message_t: function (n) {
        toastr.options = {
            "closeButton": true,
            "debug": true,
            "progressBar": true,
            "positionClass": "toast-top-right",
            "showDuration": "400",
            "hideDuration": "1000",
            "timeOut": "10000",
            "extendedTimeOut": "1000",
            "showEasing": "swing",
            "hideEasing": "linear",
            "showMethod": "fadeIn",
            "hideMethod": "fadeOut"
        }
        toastr.warning(n);
    },
    info_t: function (t, n) {
        toastr.options = {
            "closeButton": true,
            "debug": true,
            "progressBar": true,
            "positionClass": "toast-top-right",
            "showDuration": "400",
            "hideDuration": "1000",
            "timeOut": "10000",
            "extendedTimeOut": "1000",
            "showEasing": "swing",
            "hideEasing": "linear",
            "showMethod": "fadeIn",
            "hideMethod": "fadeOut"
        }
        toastr.warning(n, t);
    },
    message_b: function (n) {
        toastr.options = {
            "closeButton": true,
            "debug": true,
            "progressBar": true,
            "positionClass": "toast-bottom-right",
            "showDuration": "400",
            "hideDuration": "1000",
            "timeOut": "10000",
            "extendedTimeOut": "1000",
            "showEasing": "swing",
            "hideEasing": "linear",
            "showMethod": "fadeIn",
            "hideMethod": "fadeOut"
        }
        toastr.warning(n);
    },
    info_b: function (t, n) {
        toastr.options = {
            "closeButton": true,
            "debug": true,
            "progressBar": true,
            "positionClass": "toast-bottom-right",
            "showDuration": "400",
            "hideDuration": "1000",
            "timeOut": "10000",
            "extendedTimeOut": "1000",
            "showEasing": "swing",
            "hideEasing": "linear",
            "showMethod": "fadeIn",
            "hideMethod": "fadeOut"
        }
        toastr.warning(n, t);
    }
}
var toasError = {
    message_t: function (n) {
        toastr.options = {
            "closeButton": true,
            "debug": true,
            "progressBar": true,
            "positionClass": "toast-top-right",
            "showDuration": "400",
            "hideDuration": "1000",
            "timeOut": "10000",
            "extendedTimeOut": "1000",
            "showEasing": "swing",
            "hideEasing": "linear",
            "showMethod": "fadeIn",
            "hideMethod": "fadeOut"
        }
        toastr.error(n);
    },
    info_t: function (t, n) {
        toastr.options = {
            "closeButton": true,
            "debug": true,
            "progressBar": true,
            "positionClass": "toast-top-right",
            "showDuration": "400",
            "hideDuration": "1000",
            "timeOut": "10000",
            "extendedTimeOut": "1000",
            "showEasing": "swing",
            "hideEasing": "linear",
            "showMethod": "fadeIn",
            "hideMethod": "fadeOut"
        }
        toastr.error(n, t);
    },
    message_b: function (n) {
        toastr.options = {
            "closeButton": true,
            "debug": true,
            "progressBar": true,
            "positionClass": "toast-bottom-right",
            "showDuration": "400",
            "hideDuration": "1000",
            "timeOut": "10000",
            "extendedTimeOut": "1000",
            "showEasing": "swing",
            "hideEasing": "linear",
            "showMethod": "fadeIn",
            "hideMethod": "fadeOut"
        }
        toastr.error(n);
    },
    info_b: function (t, n) {
        toastr.options = {
            "closeButton": true,
            "debug": true,
            "progressBar": true,
            "positionClass": "toast-bottom-right",
            "showDuration": "400",
            "hideDuration": "1000",
            "timeOut": "10000",
            "extendedTimeOut": "1000",
            "showEasing": "swing",
            "hideEasing": "linear",
            "showMethod": "fadeIn",
            "hideMethod": "fadeOut"
        }
        toastr.error(n, t);
    }
}