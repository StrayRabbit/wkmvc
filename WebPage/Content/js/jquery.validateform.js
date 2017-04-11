if (function (n) {
    function i(n, t) {
        for (var i = window,
        r = (n || "").split(".") ; i && r.length;) i = i[r.shift()];
        return typeof i == "function" ? i : (t.push(n), Function.constructor.apply(null, t))
}
    function r(n) {
        return n === "GET" || n === "POST"
}
    function e(n, t) {
        r(t) || n.setRequestHeader("X-HTTP-Method-Override", t)
}
    function o(t, i, r) {
        var u;
        r.indexOf("application/x-javascript") === -1 && (u = (t.getAttribute("data-ajax-mode") || "").toUpperCase(), n(t.getAttribute("data-ajax-update")).each(function (t, r) {
            var f;
            switch (u) {
            case "BEFORE":
                f = r.firstChild;
                n("<div />").html(i).contents().each(function () {
                    r.insertBefore(this, f)
});
                break;
            case "AFTER":
                n("<div />").html(i).contents().each(function () {
                    r.appendChild(this)
});
                break;
            default:
                n(r).html(i)
}
}))
}
    function u(t, u) {
        var s, h, f, c; (s = t.getAttribute("data-ajax-confirm"), !s || window.confirm(s)) && (h = n(t.getAttribute("data-ajax-loading")), c = t.getAttribute("data-ajax-loading-duration") || 0, n.extend(u, {
    type: t.getAttribute("data-ajax-method") || undefined,
    url: t.getAttribute("data-ajax-url") || undefined,
    beforeSend: function (n) {
                var r;
                return e(n, f),
                r = i(t.getAttribute("data-ajax-begin"), ["xhr"]).apply(this, arguments),
                r !== !1 && h.show(c),
                r
},
    complete: function () {
                h.hide(c);
                i(t.getAttribute("data-ajax-complete"), ["xhr", "status"]).apply(this, arguments)
},
    success: function (n, r, u) {
                o(t, n, u.getResponseHeader("Content-Type") || "text/html");
                i(t.getAttribute("data-ajax-success"), ["data", "status", "xhr"]).apply(this, arguments)
},
    error: i(t.getAttribute("data-ajax-failure"), ["xhr", "status", "error"])
}), u.data.push({
    name: "X-Requested-With",
    value: "XMLHttpRequest"
}), f = u.type.toUpperCase(), r(f) || (u.type = "POST", u.data.push({
    name: "X-HTTP-Method-Override",
    value: f
})), n.ajax(u))
}
    function s(t) {
        var i = n(t).data(f);
        return !i || !i.validate || i.validate()
}
    var t = "unobtrusiveAjaxClick",
    f = "unobtrusiveValidation";
    n(document).on("click", "a[data-ajax=true]",
    function (n) {
        n.preventDefault();
        u(this, {
    url: this.href,
    type: "GET",
    data: []
})
});
    n(document).on("click", "form[data-ajax=true] input[type=image]",
    function (i) {
        var r = i.target.name,
        u = n(i.target),
        f = u.parents("form")[0],
        e = u.offset();
        n(f).data(t, [{
    name: r + ".x",
    value: Math.round(i.pageX - e.left)
},
        {
    name: r + ".y",
    value: Math.round(i.pageY - e.top)
}]);
        setTimeout(function () {
            n(f).removeData(t)
},
        0)
});
    n(document).on("click", "form[data-ajax=true] :submit",
    function (i) {
        var r = i.target.name,
        u = n(i.target).parents("form")[0];
        n(u).data(t, r ? [{
    name: r,
    value: i.target.value
}] : []);
        setTimeout(function () {
            n(u).removeData(t)
},
        0)
});
    n(document).on("submit", "form[data-ajax=true]",
    function (i) {
        var r = n(this).data(t) || []; (i.preventDefault(), s(this)) && u(this, {
    url: this.action,
    type: this.method || "GET",
    data: r.concat(n(this).serializeArray())
})
})
}(jQuery), "undefined" == typeof jQuery) throw new Error("Bootstrap's JavaScript requires jQuery");+
function (n, t, i) {
    function h(t, i) {
        var u = (n(window).width() - t.outerWidth()) / 2,
        r = (n(window).height() - t.outerHeight()) / 2,
        r = (document.documentElement.scrollTop ? document.documentElement.scrollTop : document.body.scrollTop) + (r > 0 ? r : 0);
        t.css({
            left: u
        }).animate({
            top: r
        },
        {
            duration: i,
            queue: !1
        })
    }
    function s() {
        if (n("#Validform_msg").length !== 0) return !1;
        f = n('<div id="Validform_msg"><div class="Validform_title">' + u.tit + '<a class="Validform_close" href="javascript:void(0);">&chi;<\/a><\/div><div class="Validform_info"><\/div><div class="iframe"><iframe frameborder="0" scrolling="no" height="100%" width="100%"><\/iframe><\/div><\/div>').appendTo("body");
        f.find("a.Validform_close").click(function () {
            return f.hide(),
            o = !0,
            e && e.focus().addClass("Validform_error"),
            !1
        }).focus(function () {
            this.blur()
        });
        n(window).bind("scroll resize",
        function () {
            o || h(f, 400)
        })
    }
    var e = null,
    f = null,
    o = !0,
    u = {
        tit: "提示信息",
        w: {
            "*": "不能为空！",
            "*6-16": "请填写6到16位任意字符！",
            n: "请填写数字！",
            "n6-16": "请填写6到16位数字！",
            s: "不能输入特殊字符！",
            "s6-18": "请填写6到18位字符！",
            p: "请填写邮政编码！",
            m: "请填写手机号码！",
            e: "邮箱地址格式不对！",
            url: "请填写网址！",
            am: "请输入正确的金额"
        },
        def: "请填写正确信息！",
        undef: "datatype未定义！",
        reck: "两次输入的内容不一致！",
        r: "通过信息验证！",
        c: "正在检测信息…",
        s: "请{填写|选择}{0|信息}！",
        v: "所填信息没有经过验证，请稍后…",
        p: "正在提交数据…"
    },
    r;
    n.Tipmsg = u;
    r = function (t, u, f) {
        var u = n.extend({},
        r.defaults, u),
        e;
        if (u.datatype && n.extend(r.util.dataType, u.datatype), e = this, e.tipmsg = {
            w: {}
        },
        e.forms = t, e.objects = [], f === !0) return !1;
        t.each(function () {
            var f, t;
            if (this.validform_inited == "inited") return !0;
            this.validform_inited = "inited";
            f = this;
            f.settings = n.extend({},
            u);
            t = n(f);
            f.validform_status = "normal";
            t.data("tipmsg", e.tipmsg);
            t.delegate("[datatype]", "blur",
            function () {
                var n = arguments[1];
                r.util.check.call(this, t, n)
            });
            t.delegate(":text", "keypress",
            function (n) {
                n.keyCode == 13 && t.find(":submit").length == 0 && t.submit()
            });
            r.util.enhance.call(t, f.settings.tiptype, f.settings.usePlugin, f.settings.tipSweep);
            f.settings.btnSubmit && t.find(f.settings.btnSubmit).bind("click",
            function () {
                return t.trigger("submit"),
                !1
            });
            t.submit(function () {
                var n = r.util.submitForm.call(t, f.settings);
                return n === i && (n = !0),
                n
            });
            t.find("[type='reset']").add(t.find(f.settings.btnReset)).bind("click",
            function () {
                r.util.resetForm.call(t)
            })
        }); (u.tiptype == 1 || (u.tiptype == 2 || u.tiptype == 3) && u.ajaxPost) && s()
    };
    r.defaults = {
        tiptype: 1,
        tipSweep: !1,
        showAllError: !1,
        postonce: !1,
        ajaxPost: !1
    };
    r.util = {
        dataType: {
            "*": /[\w\W]+/,
            "*6-16": /^[\w\W]{6,16}$/,
            n: /^\d+$/,
            "n6-16": /^\d{6,16}$/,
            s: /^[\u4E00-\u9FA5\uf900-\ufa2d\w\.\s]+$/,
            "s6-18": /^[\u4E00-\u9FA5\uf900-\ufa2d\w\.\s]{6,18}$/,
            p: /^[0-9]{6}$/,
            m: /^13[0-9]{9}$|14[0-9]{9}|15[0-9]{9}$|18[0-9]{9}$/,
            e: /^\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$/,
            url: /^(\w+:\/\/)?\w+(\.\w+)+.*$/,
            am: /^(([0-9]|([1-9][0-9]{0,9}))((\.[0-9]{1,2})?))$/
        },
        toString: Object.prototype.toString,
        isEmpty: function (t) {
            return t === "" || t === n.trim(this.attr("tip"))
        },
        getValue: function (t) {
            var u, f = this;
            return t.is(":radio") ? (u = f.find(":radio[name='" + t.attr("name") + "']:checked").val(), u = u === i ? "" : u) : t.is(":checkbox") ? (u = "", f.find(":checkbox[name='" + t.attr("name") + "']:checked").each(function () {
                u += n(this).val() + ","
            }), u = u === i ? "" : u) : u = t.val(),
            u = n.trim(u),
            r.util.isEmpty.call(t, u) ? "" : u
        },
        enhance: function (t, i, u, f) {
            var e = this;
            e.find("[datatype]").each(function () {
                t == 2 ? n(this).parent().next().find(".Validform_checktip").length == 0 && (n(this).parent().next().append("<span class='Validform_checktip' />"), n(this).siblings(".Validform_checktip").remove()) : (t == 3 || t == 4) && n(this).siblings(".Validform_checktip").length == 0 && (n(this).parent().append("<span class='Validform_checktip' />"), n(this).parent().next().find(".Validform_checktip").remove())
            });
            e.find("input[recheck]").each(function () {
                if (this.validform_inited == "inited") return !0;
                this.validform_inited = "inited";
                var t = n(this),
                i = e.find("input[name='" + n(this).attr("recheck") + "']");
                i.bind("keyup",
                function () {
                    if (i.val() == t.val() && i.val() != "") {
                        if (i.attr("tip") && i.attr("tip") == i.val()) return !1;
                        t.trigger("blur")
                    }
                }).bind("blur",
                function () {
                    if (i.val() != t.val() && t.val() != "") {
                        if (t.attr("tip") && t.attr("tip") == t.val()) return !1;
                        t.trigger("blur")
                    }
                })
            });
            e.find("[tip]").each(function () {
                if (this.validform_inited == "inited") return !0;
                this.validform_inited = "inited";
                var i = n(this).attr("tip"),
                t = n(this).attr("altercss");
                n(this).focus(function () {
                    n(this).val() == i && (n(this).val(""), t && n(this).removeClass(t))
                }).blur(function () {
                    n.trim(n(this).val()) === "" && (n(this).val(i), t && n(this).addClass(t))
                })
            });
            e.find(":checkbox[datatype],:radio[datatype]").each(function () {
                if (this.validform_inited == "inited") return !0;
                this.validform_inited = "inited";
                var t = n(this),
                i = t.attr("name");
                e.find("[name='" + i + "']").filter(":checkbox,:radio").bind("click",
                function () {
                    setTimeout(function () {
                        t.trigger("blur")
                    },
                    0)
                })
            });
            e.find("select[datatype][multiple]").bind("click",
            function () {
                var t = n(this);
                setTimeout(function () {
                    t.trigger("blur")
                },
                0)
            });
            r.util.usePlugin.call(e, i, t, u, f)
        },
        usePlugin: function (t, i, u, f) {
            var e = this,
            t = t || {},
            o;
            if (e.find("input[plugin='swfupload']").length && typeof swfuploadhandler != "undefined" && (o = {
                custom_settings: {
                form: e,
                showmsg: function (n, t) {
                        r.util.showmsg.call(e, n, i, {
                obj: e.find("input[plugin='swfupload']"),
                type: t,
                sweep: u
            })
            }
            }
            },
            o = n.extend(!0, {},
            t.swfupload, o), e.find("input[plugin='swfupload']").each(function (t) {
                if (this.validform_inited == "inited") return !0;
                this.validform_inited = "inited";
                n(this).val("");
                swfuploadhandler.init(o, t)
            })), e.find("input[plugin='datepicker']").length && n.fn.datePicker && (t.datepicker = t.datepicker || {},
            t.datepicker.format && (Date.format = t.datepicker.format, delete t.datepicker.format), t.datepicker.firstDayOfWeek && (Date.firstDayOfWeek = t.datepicker.firstDayOfWeek, delete t.datepicker.firstDayOfWeek), e.find("input[plugin='datepicker']").each(function () {
                if (this.validform_inited == "inited") return !0;
                this.validform_inited = "inited";
                t.datepicker.callback && n(this).bind("dateSelected",
                function () {
                    var i = new Date(n.event._dpCache[this._dpId].getSelected()[0]).asString(Date.format);
                    t.datepicker.callback(i, this)
            });
                n(this).datePicker(t.datepicker)
            })), e.find("input[plugin*='passwordStrength']").length && n.fn.passwordStrength && (t.passwordstrength = t.passwordstrength || {},
            t.passwordstrength.showmsg = function (n, t, f) {
                r.util.showmsg.call(e, t, i, {
                obj: n,
                type: f,
                sweep: u
            })
            },
            e.find("input[plugin='passwordStrength']").each(function () {
                if (this.validform_inited == "inited") return !0;
                this.validform_inited = "inited";
                n(this).passwordStrength(t.passwordstrength)
            })), f != "addRule" && t.jqtransform && n.fn.jqTransSelect) {
                if (e[0].jqTransSelected == "true") return;
                e[0].jqTransSelected = "true";
                var s = function (t) {
                    var i = n(".jqTransformSelectWrapper ul:visible");
                    i.each(function () {
                        var i = n(this).parents(".jqTransformSelectWrapper:first").find("select").get(0);
                        t && i.oLabel && i.oLabel.get(0) == t.get(0) || n(this).hide()
                    })
                },
                h = function (t) {
                    n(t.target).parents(".jqTransformSelectWrapper").length === 0 && s(n(t.target))
                },
                c = function () {
                    n(document).mousedown(h)
                };
                t.jqtransform.selector ? (e.find(t.jqtransform.selector).filter('input:submit, input:reset, input[type="button"]').jqTransInputButton(), e.find(t.jqtransform.selector).filter("input:text, input:password").jqTransInputText(), e.find(t.jqtransform.selector).filter("input:checkbox").jqTransCheckBox(), e.find(t.jqtransform.selector).filter("input:radio").jqTransRadio(), e.find(t.jqtransform.selector).filter("textarea").jqTransTextarea(), e.find(t.jqtransform.selector).filter("select").length > 0 && (e.find(t.jqtransform.selector).filter("select").jqTransSelect(), c())) : e.jqTransform();
                e.find(".jqTransformSelectWrapper").find("li a").click(function () {
                    n(this).parents(".jqTransformSelectWrapper").find("select").trigger("blur")
                })
            }
        },
        getNullmsg: function (n) {
            var r = this,
            f = /[\u4E00-\u9FA5\uf900-\ufa2da-zA-Z\s]+/g,
            t, i = n[0].settings.label || ".Validform_label";
            if (i = r.siblings(i).eq(0).text() || r.siblings().find(i).eq(0).text() || r.parent().siblings(i).eq(0).text() || r.parent().siblings().find(i).eq(0).text(), i = i.replace(/\s(?![a-zA-Z])/g, "").match(f), i = i ? i.join("") : [""], f = /\{(.+)\|(.+)\}/, t = n.data("tipmsg").s || u.s, i != "") {
                if (t = t.replace(/\{0\|(.+)\}/, i), r.attr("recheck")) return t = t.replace(/\{(.+)\}/, ""),
                r.attr("nullmsg", t),
                t
            } else t = r.is(":checkbox,:radio,select") ? t.replace(/\{0\|(.+)\}/, "") : t.replace(/\{0\|(.+)\}/, "$1");
            return t = r.is(":checkbox,:radio,select") ? t.replace(f, "$2") : t.replace(f, "$1"),
            r.attr("nullmsg", t),
            t
        },
        getErrormsg: function (t, i, r) {
            var f = i.match(/^(.+?)((\d+)-(\d+))?$/),
            o,
            s,
            e;
            if (r == "recheck") return t.data("tipmsg").reck || u.reck;
            if (s = n.extend({},
            u.w, t.data("tipmsg").w), f[0] in s) return t.data("tipmsg").w[f[0]] || u.w[f[0]];
            for (e in s) if (e.indexOf(f[1]) != -1 && /^(.+?)(\d+)-(\d+)$/.test(e)) return o = (t.data("tipmsg").w[e] || u.w[e]).replace(/(.*?)\d+(.+?)\d+(.*)/, "$1" + f[3] + "$2" + f[4] + "$3"),
            t.data("tipmsg").w[f[0]] = o,
            o;
            return t.data("tipmsg").def || u.def
        },
        _regcheck: function (n, t, f, e) {
            var e = e,
            s = null,
            o = !1,
            a = /\/.+\//g,
            w = /^(.+?)(\d+)-(\d+)$/,
            v = 3,
            h, c, p, b;
            if (a.test(n)) {
                var k = n.match(a)[0].slice(1, -1),
                y = n.replace(a, ""),
                d = RegExp(k, y);
                o = d.test(t)
            } else if (r.util.toString.call(r.util.dataType[n]) == "[object Function]") o = r.util.dataType[n](t, f, e, r.util.dataType),
            o === !0 || o === i ? o = !0 : (s = o, o = !1);
            else {
                if (!(n in r.util.dataType)) if (h = n.match(w), h) {
                    for (p in r.util.dataType) if ((c = p.match(w), c) && h[1] === c[1]) {
                        var l = r.util.dataType[p].toString(),
                        y = l.match(/\/[mgi]*/g)[1].replace("/", ""),
                        g = new RegExp("\\{" + c[2] + "," + c[3] + "\\}", "g");
                        l = l.replace(/\/[mgi]*/g, "/").replace(g, "{" + h[2] + "," + h[3] + "}").replace(/^\//, "").replace(/\/$/, "");
                        r.util.dataType[n] = new RegExp(l, y);
                        break
                    }
                } else o = !1,
                s = e.data("tipmsg").undef || u.undef;
                r.util.toString.call(r.util.dataType[n]) == "[object RegExp]" && (o = r.util.dataType[n].test(t))
            }
            return o ? (v = 2, s = f.attr("sucmsg") || e.data("tipmsg").r || u.r, f.attr("recheck") && (b = e.find("input[name='" + f.attr("recheck") + "']:first"), t != b.val() && (o = !1, v = 3, s = f.attr("errormsg") || r.util.getErrormsg.call(f, e, n, "recheck")))) : (s = s || f.attr("errormsg") || r.util.getErrormsg.call(f, e, n), r.util.isEmpty.call(f, t) && (s = f.attr("nullmsg") || r.util.getNullmsg.call(f, e))),
            {
                passed: o,
                type: v,
                info: s
            }
        },
        regcheck: function (n, t, i) {
            var h = this,
            s = null,
            f, e, u, o;
            if (i.attr("ignore") === "ignore" && r.util.isEmpty.call(i, t)) return i.data("cked") && (s = ""),
            {
                passed: !0,
                type: 4,
                info: s
            };
            for (i.data("cked", "cked"), f = r.util.parseDatatype(n), u = 0; u < f.length; u++) {
                for (o = 0; o < f[u].length; o++) if (e = r.util._regcheck(f[u][o], t, i, h), !e.passed) break;
                if (e.passed) break
            }
            return e
        },
        parseDatatype: function (n) {
            var u = /\/.+?\/[mgi]*(?=(,|$|\||\s))|[\w\*-]+/g,
            f = n.match(u),
            e = n.replace(u, "").replace(/\s*/g, "").split(""),
            t = [],
            r = 0,
            i;
            for (t[0] = [], t[0].push(f[0]), i = 0; i < e.length; i++) e[i] == "|" && (r++, t[r] = []),
            t[r].push(f[i + 1]);
            return t
        },
        showmsg: function (t, u, e, s) {
            if (t != i && (s != "bycheck" || !e.sweep || (!e.obj || e.obj.is(".Validform_error")) && typeof u != "function")) {
                if (n.extend(e, {
                    curform: this
                }), typeof u == "function") {
                    u(t, e, r.util.cssctl);
                    return
                } (u == 1 || s == "byajax" && u != 4) && f.find(".Validform_info").html(t); (u == 1 && s != "bycheck" && e.type != 2 || s == "byajax" && u != 4) && (o = !1, f.find(".iframe").css("height", f.outerHeight()), f.show(), h(f, 100));
                u == 2 && e.obj && (e.obj.parent().next().find(".Validform_checktip").html(t), r.util.cssctl(e.obj.parent().next().find(".Validform_checktip"), e.type)); (u == 3 || u == 4) && e.obj && (e.obj.siblings(".Validform_checktip").html(t), r.util.cssctl(e.obj.siblings(".Validform_checktip"), e.type))
            }
        },
        cssctl: function (n, t) {
            switch (t) {
                case 1:
                    n.removeClass("Validform_right Validform_wrong").addClass("Validform_checktip Validform_loading");
                    break;
                case 2:
                    n.removeClass("Validform_wrong Validform_loading").addClass("Validform_checktip Validform_right");
                    break;
                case 4:
                    n.removeClass("Validform_right Validform_wrong Validform_loading").addClass("Validform_checktip");
                    break;
                default:
                    n.removeClass("Validform_right Validform_loading").addClass("Validform_checktip Validform_wrong")
            }
        },
        check: function (t, i, f) {
            var s = t[0].settings,
            i = i || "",
            l = r.util.getValue.call(t, n(this)),
            a,
            h,
            v,
            o,
            c,
            y,
            p,
            w;
            if (s.ignoreHidden && n(this).is(":hidden") || n(this).data("dataIgnore") === "dataIgnore") return !0;
            if (s.dragonfly && !n(this).data("cked") && r.util.isEmpty.call(n(this), l) && n(this).attr("ignore") != "ignore") return !1;
            if (a = r.util.regcheck.call(t, n(this).attr("datatype"), l, n(this)), l == this.validform_lastval && !n(this).attr("recheck") && i == "") return a.passed ? !0 : !1;
            if (this.validform_lastval = l, e = h = n(this), !a.passed) return r.util.abort.call(h[0]),
            f || (r.util.showmsg.call(t, a.info, s.tiptype, {
                obj: n(this),
                type: a.type,
                sweep: s.tipSweep
            },
            "bycheck"), s.tipSweep || h.addClass("Validform_error")),
            !1;
            if (v = n(this).attr("ajaxurl"), !v || r.util.isEmpty.call(n(this), l) || f) v && r.util.isEmpty.call(n(this), l) && (r.util.abort.call(h[0]), h[0].validform_valid = "true");
            else return (o = n(this), o[0].validform_subpost = i == "postform" ? "postform" : "", o[0].validform_valid === "posting" && l == o[0].validform_ckvalue) ? "ajax" : (o[0].validform_valid = "posting", o[0].validform_ckvalue = l, r.util.showmsg.call(t, t.data("tipmsg").c || u.c, s.tiptype, {
                obj: o,
                type: 1,
                sweep: s.tipSweep
            },
            "bycheck"), r.util.abort.call(h[0]), c = n.extend(!0, {},
            s.ajaxurl || {}), y = {
                type: "POST",
                cache: !1,
                url: v,
                data: "param=" + encodeURIComponent(l) + "&name=" + encodeURIComponent(n(this).attr("name")),
                success: function (i) {
                    n.trim(i.status) === "y" ? (o[0].validform_valid = "true", i.info && o.attr("sucmsg", i.info), r.util.showmsg.call(t, o.attr("sucmsg") || t.data("tipmsg").r || u.r, s.tiptype, {
                        obj: o,
                        type: 2,
                        sweep: s.tipSweep
                    },
                    "bycheck"), h.removeClass("Validform_error"), e = null, o[0].validform_subpost == "postform" && t.trigger("submit")) : (o[0].validform_valid = i.info, r.util.showmsg.call(t, i.info, s.tiptype, {
                        obj: o,
                        type: 3,
                        sweep: s.tipSweep
                    }), h.addClass("Validform_error"));
                    h[0].validform_ajax = null
                },
                error: function (n) {
                    if (n.status == "200") return n.responseText == "y" ? c.success({
                        status: "y"
                    }) : c.success({
                        status: "n",
                        info: n.responseText
                    }),
                    !1;
                    if (n.statusText !== "abort") {
                        var i = "status: " + n.status + "; statusText: " + n.statusText;
                        r.util.showmsg.call(t, i, s.tiptype, {
                            obj: o,
                            type: 3,
                            sweep: s.tipSweep
                        });
                        h.addClass("Validform_error")
                    }
                    return o[0].validform_valid = n.statusText,
                    h[0].validform_ajax = null,
                    !0
                }
            },
            c.success && (p = c.success, c.success = function (n) {
                y.success(n);
                p(n, o)
            }), c.error && (w = c.error, c.error = function (n) {
                y.error(n) && w(n, o)
            }), c = n.extend({},
            y, c, {
                dataType: "json"
            }), h[0].validform_ajax = n.ajax(c), "ajax");
            return f || (r.util.showmsg.call(t, a.info, s.tiptype, {
                obj: n(this),
                type: a.type,
                sweep: s.tipSweep
            },
            "bycheck"), h.removeClass("Validform_error")),
            e = null,
            !0
        },
        submitForm: function (t, i, f, o, s) {
            var h = this,
            v, l, a, y, c, p, w, b, f;
            if (h[0].validform_status === "posting" || t.postonce && h[0].validform_status === "posted" || (v = t.beforeCheck && t.beforeCheck(h), v === !1)) return !1;
            if (l = !0, h.find("[datatype]").each(function () {
                var o, f, s;
                if (i) return !1;
                if (t.ignoreHidden && n(this).is(":hidden") || n(this).data("dataIgnore") === "dataIgnore") return !0;
                if (o = r.util.getValue.call(h, n(this)), e = f = n(this), a = r.util.regcheck.call(h, n(this).attr("datatype"), o, n(this)), !a.passed) return (r.util.showmsg.call(h, a.info, t.tiptype, {
                obj: n(this),
                type: a.type,
                sweep: t.tipSweep
            }), f.addClass("Validform_error"), !t.showAllError) ? (f.focus(), l = !1, !1) : (l && (l = !1), !0);
                if (n(this).attr("ajaxurl") && !r.util.isEmpty.call(n(this), o)) {
                    if (this.validform_valid !== "true") return (s = n(this), r.util.showmsg.call(h, h.data("tipmsg").v || u.v, t.tiptype, {
                obj: s,
                type: 3,
                sweep: t.tipSweep
            }), f.addClass("Validform_error"), s.trigger("blur", ["postform"]), !t.showAllError) ? (l = !1, !1) : (l && (l = !1), !0)
            } else n(this).attr("ajaxurl") && r.util.isEmpty.call(n(this), o) && (r.util.abort.call(this), this.validform_valid = "true");
                r.util.showmsg.call(h, a.info, t.tiptype, {
                obj: n(this),
                type: a.type,
                sweep: t.tipSweep
            });
                f.removeClass("Validform_error");
                e = null
            }), t.showAllError && h.find(".Validform_error:first").focus(), l) {
                if (y = t.beforeSubmit && t.beforeSubmit(h), y === !1) return !1;
                if (h[0].validform_status = "posting", t.ajaxPost || o === "ajaxPost") c = n.extend(!0, {},
                t.ajaxpost || {}),
                c.url = f || c.url || t.url || h.attr("action"),
                r.util.showmsg.call(h, h.data("tipmsg").p || u.p, t.tiptype, {
                    obj: h,
                    type: 1,
                    sweep: t.tipSweep
                },
                "byajax"),
                s ? c.async = !1 : s === !1 && (c.async = !0),
                c.success && (p = c.success, c.success = function (i) {
                    t.callback && t.callback(i);
                    h[0].validform_ajax = null;
                    h[0].validform_status = n.trim(i.status) === "y" ? "posted" : "normal";
                    p(i, h)
                }),
                c.error && (w = c.error, c.error = function (n) {
                    t.callback && t.callback(n);
                    h[0].validform_status = "normal";
                    h[0].validform_ajax = null;
                    w(n, h)
                }),
                b = {
                    type: "POST",
                    async: !0,
                    data: h.serializeArray(),
                    success: function (i) {
                        n.trim(i.status) === "y" ? (h[0].validform_status = "posted", r.util.showmsg.call(h, i.info, t.tiptype, {
                            obj: h,
                            type: 2,
                            sweep: t.tipSweep
                        },
                        "byajax")) : (h[0].validform_status = "normal", r.util.showmsg.call(h, i.info, t.tiptype, {
                            obj: h,
                            type: 3,
                            sweep: t.tipSweep
                        },
                        "byajax"));
                        t.callback && t.callback(i);
                        h[0].validform_ajax = null
                    },
                    error: function (n) {
                        var i = "status: " + n.status + "; statusText: " + n.statusText;
                        r.util.showmsg.call(h, i, t.tiptype, {
                            obj: h,
                            type: 3,
                            sweep: t.tipSweep
                        },
                        "byajax");
                        t.callback && t.callback(n);
                        h[0].validform_status = "normal";
                        h[0].validform_ajax = null
                    }
                },
                c = n.extend({},
                b, c, {
                    dataType: "json"
                }),
                h[0].validform_ajax = n.ajax(c);
                else return t.postonce || (h[0].validform_status = "normal"),
                f = f || t.url,
                f && h.attr("action", f),
                t.callback && t.callback(h)
            }
            return !1
        },
        resetForm: function () {
            var n = this;
            n.each(function () {
                this.reset && this.reset();
                this.validform_status = "normal"
            });
            n.find(".Validform_right").text("");
            n.find(".passwordStrength").children().removeClass("bgStrength");
            n.find(".Validform_checktip").removeClass("Validform_wrong Validform_right Validform_loading");
            n.find(".Validform_error").removeClass("Validform_error");
            n.find("[datatype]").removeData("cked").removeData("dataIgnore").each(function () {
                this.validform_lastval = null
            });
            n.eq(0).find("input:first").focus()
        },
        abort: function () {
            this.validform_ajax && this.validform_ajax.abort()
        }
    };
    n.Datatype = r.util.dataType;
    r.prototype = {
        dataType: r.util.dataType,
        eq: function (t) {
            var i = this;
            return t >= i.forms.length ? null : (t in i.objects || (i.objects[t] = new r(n(i.forms[t]).get(), {},
            !0)), i.objects[t])
        },
        resetStatus: function () {
            var t = this;
            return n(t.forms).each(function () {
                this.validform_status = "normal"
            }),
            this
        },
        setStatus: function (t) {
            var i = this;
            return n(i.forms).each(function () {
                this.validform_status = t || "posting"
            }),
            this
        },
        getStatus: function () {
            var t = this;
            return n(t.forms)[0].validform_status
        },
        ignore: function (t) {
            var i = this,
            t = t || "[datatype]";
            return n(i.forms).find(t).each(function () {
                n(this).data("dataIgnore", "dataIgnore").removeClass("Validform_error")
            }),
            this
        },
        unignore: function (t) {
            var i = this,
            t = t || "[datatype]";
            return n(i.forms).find(t).each(function () {
                n(this).removeData("dataIgnore")
            }),
            this
        },
        addRule: function (t) {
            for (var f = this,
            t = t || [], e, u, i = 0; i < t.length; i++) {
                e = n(f.forms).find(t[i].ele);
                for (u in t[i]) u !== "ele" && e.attr(u, t[i][u])
            }
            return n(f.forms).each(function () {
                var t = n(this);
                r.util.enhance.call(t, this.settings.tiptype, this.settings.usePlugin, this.settings.tipSweep, "addRule")
            }),
            this
        },
        ajaxPost: function (t, i, u) {
            var f = this;
            return n(f.forms).each(function () {
                (this.settings.tiptype == 1 || this.settings.tiptype == 2 || this.settings.tiptype == 3) && s();
                r.util.submitForm.call(n(f.forms[0]), this.settings, t, u, "ajaxPost", i)
            }),
            this
        },
        submitForm: function (t, u) {
            var f = this;
            return n(f.forms).each(function () {
                var f = r.util.submitForm.call(n(this), this.settings, t, u);
                f === i && (f = !0);
                f === !0 && this.submit()
            }),
            this
        },
        resetForm: function () {
            var t = this;
            return r.util.resetForm.call(n(t.forms)),
            this
        },
        abort: function () {
            var t = this;
            return n(t.forms).each(function () {
                r.util.abort.call(this)
            }),
            this
        },
        check: function (t, i) {
            var i = i || "[datatype]",
            e = this,
            u = n(e.forms),
            f = !0;
            return u.find(i).each(function () {
                r.util.check.call(this, u, "", t) || (f = !1)
            }),
            f
        },
        config: function (t) {
            var i = this;
            return t = t || {},
            n(i.forms).each(function () {
                var i = n(this);
                this.settings = n.extend(!0, this.settings, t);
                r.util.enhance.call(i, this.settings.tiptype, this.settings.usePlugin, this.settings.tipSweep)
            }),
            this
        }
    };
    n.fn.Validform = function (n) {
        return new r(this, n)
    };
    n.Showmsg = function (n) {
        s();
        r.util.showmsg.call(t, n, 1, {})
    };
    n.Hidemsg = function () {
        f.hide();
        o = !0
    }
}(jQuery, window); +
//function (n) {
//    "use strict";
//    var t = n.fn.jquery.split(" ")[0].split(".");
//    if (t[0] < 2 && t[1] < 9 || 1 == t[0] && 9 == t[1] && t[2] < 1) throw new Error("Bootstrap's JavaScript requires jQuery version 1.9.1 or higher");
//}(jQuery); +
function (n) {
    "use strict";
    function t() {
        var i = document.createElement("bootstrap"),
        t = {
            WebkitTransition: "webkitTransitionEnd",
            MozTransition: "transitionend",
            OTransition: "oTransitionEnd otransitionend",
            transition: "transitionend"
        },
        n;
        for (n in t) if (void 0 !== i.style[n]) return {
            end: t[n]
        };
        return !1
    }
    n.fn.emulateTransitionEnd = function (t) {
        var i = !1,
        u = this,
        r;
        n(this).one("bsTransitionEnd",
        function () {
            i = !0
        });
        return r = function () {
            i || n(u).trigger(n.support.transition.end)
        },
        setTimeout(r, t),
        this
    };
    n(function () {
        n.support.transition = t();
        n.support.transition && (n.event.special.bsTransitionEnd = {
            bindType: n.support.transition.end,
            delegateType: n.support.transition.end,
            handle: function (t) {
                if (n(t.target).is(this)) return t.handleObj.handler.apply(this, arguments)
            }
        })
    })
}(jQuery); +
function (n) {
    "use strict";
    function u(i) {
        return this.each(function () {
            var r = n(this),
            u = r.data("bs.alert");
            u || r.data("bs.alert", u = new t(this));
            "string" == typeof i && u[i].call(r)
        })
    }
    var i = '[data-dismiss="alert"]',
    t = function (t) {
        n(t).on("click", i, this.close)
    },
    r;
    t.VERSION = "3.3.4";
    t.TRANSITION_DURATION = 150;
    t.prototype.close = function (i) {
        function e() {
            r.detach().trigger("closed.bs.alert").remove()
        }
        var f = n(this),
        u = f.attr("data-target"),
        r;
        u || (u = f.attr("href"), u = u && u.replace(/.*(?=#[^\s]*$)/, ""));
        r = n(u);
        i && i.preventDefault();
        r.length || (r = f.closest(".alert"));
        r.trigger(i = n.Event("close.bs.alert"));
        i.isDefaultPrevented() || (r.removeClass("in"), n.support.transition && r.hasClass("fade") ? r.one("bsTransitionEnd", e).emulateTransitionEnd(t.TRANSITION_DURATION) : e())
    };
    r = n.fn.alert;
    n.fn.alert = u;
    n.fn.alert.Constructor = t;
    n.fn.alert.noConflict = function () {
        return n.fn.alert = r,
        this
    };
    n(document).on("click.bs.alert.data-api", i, t.prototype.close)
}(jQuery); +
function (n) {
    "use strict";
    function i(i) {
        return this.each(function () {
            var u = n(this),
            r = u.data("bs.button"),
            f = "object" == typeof i && i;
            r || u.data("bs.button", r = new t(this, f));
            "toggle" == i ? r.toggle() : i && r.setState(i)
        })
    }
    var t = function (i, r) {
        this.$element = n(i);
        this.options = n.extend({},
        t.DEFAULTS, r);
        this.isLoading = !1
    },
    r;
    t.VERSION = "3.3.4";
    t.DEFAULTS = {
        loadingText: "loading..."
    };
    t.prototype.setState = function (t) {
        var r = "disabled",
        i = this.$element,
        f = i.is("input") ? "val" : "html",
        u = i.data();
        t += "Text";
        null == u.resetText && i.data("resetText", i[f]());
        setTimeout(n.proxy(function () {
            i[f](null == u[t] ? this.options[t] : u[t]);
            "loadingText" == t ? (this.isLoading = !0, i.addClass(r).attr(r, r)) : this.isLoading && (this.isLoading = !1, i.removeClass(r).removeAttr(r))
        },
        this), 0)
    };
    t.prototype.toggle = function () {
        var t = !0,
        i = this.$element.closest('[data-toggle="buttons"]'),
        n;
        i.length ? (n = this.$element.find("input"), "radio" == n.prop("type") && (n.prop("checked") && this.$element.hasClass("active") ? t = !1 : i.find(".active").removeClass("active")), t && n.prop("checked", !this.$element.hasClass("active")).trigger("change")) : this.$element.attr("aria-pressed", !this.$element.hasClass("active"));
        t && this.$element.toggleClass("active")
    };
    r = n.fn.button;
    n.fn.button = i;
    n.fn.button.Constructor = t;
    n.fn.button.noConflict = function () {
        return n.fn.button = r,
        this
    };
    n(document).on("click.bs.button.data-api", '[data-toggle^="button"]',
    function (t) {
        var r = n(t.target);
        r.hasClass("btn") || (r = r.closest(".btn"));
        i.call(r, "toggle");
        t.preventDefault()
    }).on("focus.bs.button.data-api blur.bs.button.data-api", '[data-toggle^="button"]',
    function (t) {
        n(t.target).closest(".btn").toggleClass("focus", /^focus(in)?$/.test(t.type))
    })
}(jQuery); +
function (n) {
    "use strict";
    function i(i) {
        return this.each(function () {
            var u = n(this),
            r = u.data("bs.carousel"),
            f = n.extend({},
            t.DEFAULTS, u.data(), "object" == typeof i && i),
            e = "string" == typeof i ? i : f.slide;
            r || u.data("bs.carousel", r = new t(this, f));
            "number" == typeof i ? r.to(i) : e ? r[e]() : f.interval && r.pause().cycle()
        })
    }
    var t = function (t, i) {
        this.$element = n(t);
        this.$indicators = this.$element.find(".carousel-indicators");
        this.options = i;
        this.paused = null;
        this.sliding = null;
        this.interval = null;
        this.$active = null;
        this.$items = null;
        this.options.keyboard && this.$element.on("keydown.bs.carousel", n.proxy(this.keydown, this));
        "hover" == this.options.pause && !("ontouchstart" in document.documentElement) && this.$element.on("mouseenter.bs.carousel", n.proxy(this.pause, this)).on("mouseleave.bs.carousel", n.proxy(this.cycle, this))
    },
    u,
    r;
    t.VERSION = "3.3.4";
    t.TRANSITION_DURATION = 600;
    t.DEFAULTS = {
        interval: 5e3,
        pause: "hover",
        wrap: !0,
        keyboard: !0
    };
    t.prototype.keydown = function (n) {
        if (!/input|textarea/i.test(n.target.tagName)) {
            switch (n.which) {
                case 37:
                    this.prev();
                    break;
                case 39:
                    this.next();
                    break;
                default:
                    return
            }
            n.preventDefault()
        }
    };
    t.prototype.cycle = function (t) {
        return t || (this.paused = !1),
        this.interval && clearInterval(this.interval),
        this.options.interval && !this.paused && (this.interval = setInterval(n.proxy(this.next, this), this.options.interval)),
        this
    };
    t.prototype.getItemIndex = function (n) {
        return this.$items = n.parent().children(".item"),
        this.$items.index(n || this.$active)
    };
    t.prototype.getItemForDirection = function (n, t) {
        var i = this.getItemIndex(t),
        f = "prev" == n && 0 === i || "next" == n && i == this.$items.length - 1,
        r,
        u;
        return f && !this.options.wrap ? t : (r = "prev" == n ? -1 : 1, u = (i + r) % this.$items.length, this.$items.eq(u))
    };
    t.prototype.to = function (n) {
        var i = this,
        t = this.getItemIndex(this.$active = this.$element.find(".item.active"));
        if (!(n > this.$items.length - 1) && !(0 > n)) return this.sliding ? this.$element.one("slid.bs.carousel",
        function () {
            i.to(n)
        }) : t == n ? this.pause().cycle() : this.slide(n > t ? "next" : "prev", this.$items.eq(n))
    };
    t.prototype.pause = function (t) {
        return t || (this.paused = !0),
        this.$element.find(".next, .prev").length && n.support.transition && (this.$element.trigger(n.support.transition.end), this.cycle(!0)),
        this.interval = clearInterval(this.interval),
        this
    };
    t.prototype.next = function () {
        if (!this.sliding) return this.slide("next")
    };
    t.prototype.prev = function () {
        if (!this.sliding) return this.slide("prev")
    };
    t.prototype.slide = function (i, r) {
        var e = this.$element.find(".item.active"),
        u = r || this.getItemForDirection(i, e),
        l = this.interval,
        f = "next" == i ? "left" : "right",
        a = this,
        o,
        s,
        h,
        c;
        return u.hasClass("active") ? this.sliding = !1 : (o = u[0], s = n.Event("slide.bs.carousel", {
            relatedTarget: o,
            direction: f
        }), (this.$element.trigger(s), !s.isDefaultPrevented()) ? ((this.sliding = !0, l && this.pause(), this.$indicators.length) && (this.$indicators.find(".active").removeClass("active"), h = n(this.$indicators.children()[this.getItemIndex(u)]), h && h.addClass("active")), c = n.Event("slid.bs.carousel", {
            relatedTarget: o,
            direction: f
        }), n.support.transition && this.$element.hasClass("slide") ? (u.addClass(i), u[0].offsetWidth, e.addClass(f), u.addClass(f), e.one("bsTransitionEnd",
        function () {
            u.removeClass([i, f].join(" ")).addClass("active");
            e.removeClass(["active", f].join(" "));
            a.sliding = !1;
            setTimeout(function () {
                a.$element.trigger(c)
            },
            0)
        }).emulateTransitionEnd(t.TRANSITION_DURATION)) : (e.removeClass("active"), u.addClass("active"), this.sliding = !1, this.$element.trigger(c)), l && this.cycle(), this) : void 0)
    };
    u = n.fn.carousel;
    n.fn.carousel = i;
    n.fn.carousel.Constructor = t;
    n.fn.carousel.noConflict = function () {
        return n.fn.carousel = u,
        this
    };
    r = function (t) {
        var o, r = n(this),
        u = n(r.attr("data-target") || (o = r.attr("href")) && o.replace(/.*(?=#[^\s]+$)/, "")),
        e,
        f;
        u.hasClass("carousel") && (e = n.extend({},
        u.data(), r.data()), f = r.attr("data-slide-to"), f && (e.interval = !1), i.call(u, e), f && u.data("bs.carousel").to(f), t.preventDefault())
    };
    n(document).on("click.bs.carousel.data-api", "[data-slide]", r).on("click.bs.carousel.data-api", "[data-slide-to]", r);
    n(window).on("load",
    function () {
        n('[data-ride="carousel"]').each(function () {
            var t = n(this);
            i.call(t, t.data())
        })
    })
}(jQuery); +
function (n) {
    "use strict";
    function r(t) {
        var i, r = t.attr("data-target") || (i = t.attr("href")) && i.replace(/.*(?=#[^\s]+$)/, "");
        return n(r)
    }
    function i(i) {
        return this.each(function () {
            var u = n(this),
            r = u.data("bs.collapse"),
            f = n.extend({},
            t.DEFAULTS, u.data(), "object" == typeof i && i); !r && f.toggle && /show|hide/.test(i) && (f.toggle = !1);
            r || u.data("bs.collapse", r = new t(this, f));
            "string" == typeof i && r[i]()
        })
    }
    var t = function (i, r) {
        this.$element = n(i);
        this.options = n.extend({},
        t.DEFAULTS, r);
        this.$trigger = n('[data-toggle="collapse"][href="#' + i.id + '"],[data-toggle="collapse"][data-target="#' + i.id + '"]');
        this.transitioning = null;
        this.options.parent ? this.$parent = this.getParent() : this.addAriaAndCollapsedClass(this.$element, this.$trigger);
        this.options.toggle && this.toggle()
    },
    u;
    t.VERSION = "3.3.4";
    t.TRANSITION_DURATION = 350;
    t.DEFAULTS = {
        toggle: !0
    };
    t.prototype.dimension = function () {
        var n = this.$element.hasClass("width");
        return n ? "width" : "height"
    };
    t.prototype.show = function () {
        var f, r, e, u, o, s;
        if (!this.transitioning && !this.$element.hasClass("in") && (r = this.$parent && this.$parent.children(".panel").children(".in, .collapsing"), !(r && r.length && (f = r.data("bs.collapse"), f && f.transitioning)) && (e = n.Event("show.bs.collapse"), this.$element.trigger(e), !e.isDefaultPrevented()))) {
            if (r && r.length && (i.call(r, "hide"), f || r.data("bs.collapse", null)), u = this.dimension(), this.$element.removeClass("collapse").addClass("collapsing")[u](0).attr("aria-expanded", !0), this.$trigger.removeClass("collapsed").attr("aria-expanded", !0), this.transitioning = 1, o = function () {
                this.$element.removeClass("collapsing").addClass("collapse in")[u]("");
                this.transitioning = 0;
                this.$element.trigger("shown.bs.collapse")
            },
            !n.support.transition) return o.call(this);
            s = n.camelCase(["scroll", u].join("-"));
            this.$element.one("bsTransitionEnd", n.proxy(o, this)).emulateTransitionEnd(t.TRANSITION_DURATION)[u](this.$element[0][s])
        }
    };
    t.prototype.hide = function () {
        var r, i, u;
        if (!this.transitioning && this.$element.hasClass("in") && (r = n.Event("hide.bs.collapse"), this.$element.trigger(r), !r.isDefaultPrevented())) return i = this.dimension(),
        this.$element[i](this.$element[i]())[0].offsetHeight,
        this.$element.addClass("collapsing").removeClass("collapse in").attr("aria-expanded", !1),
        this.$trigger.addClass("collapsed").attr("aria-expanded", !1),
        this.transitioning = 1,
        u = function () {
            this.transitioning = 0;
            this.$element.removeClass("collapsing").addClass("collapse").trigger("hidden.bs.collapse")
        },
        n.support.transition ? void this.$element[i](0).one("bsTransitionEnd", n.proxy(u, this)).emulateTransitionEnd(t.TRANSITION_DURATION) : u.call(this)
    };
    t.prototype.toggle = function () {
        this[this.$element.hasClass("in") ? "hide" : "show"]()
    };
    t.prototype.getParent = function () {
        return n(this.options.parent).find('[data-toggle="collapse"][data-parent="' + this.options.parent + '"]').each(n.proxy(function (t, i) {
            var u = n(i);
            this.addAriaAndCollapsedClass(r(u), u)
        },
        this)).end()
    };
    t.prototype.addAriaAndCollapsedClass = function (n, t) {
        var i = n.hasClass("in");
        n.attr("aria-expanded", i);
        t.toggleClass("collapsed", !i).attr("aria-expanded", i)
    };
    u = n.fn.collapse;
    n.fn.collapse = i;
    n.fn.collapse.Constructor = t;
    n.fn.collapse.noConflict = function () {
        return n.fn.collapse = u,
        this
    };
    n(document).on("click.bs.collapse.data-api", '[data-toggle="collapse"]',
    function (t) {
        var u = n(this);
        u.attr("data-target") || t.preventDefault();
        var f = r(u),
        e = f.data("bs.collapse"),
        o = e ? "toggle" : u.data();
        i.call(f, o)
    })
}(jQuery); +
function (n) {
    "use strict";
    function r(t) {
        t && 3 === t.which || (n(o).remove(), n(i).each(function () {
            var r = n(this),
            i = u(r),
            f = {
                relatedTarget: this
            };
            i.hasClass("open") && (i.trigger(t = n.Event("hide.bs.dropdown", f)), t.isDefaultPrevented() || (r.attr("aria-expanded", "false"), i.removeClass("open").trigger("hidden.bs.dropdown", f)))
        }))
    }
    function u(t) {
        var i = t.attr("data-target"),
        r;
        return i || (i = t.attr("href"), i = i && /#[A-Za-z]/.test(i) && i.replace(/.*(?=#[^\s]*$)/, "")),
        r = i && n(i),
        r && r.length ? r : t.parent()
    }
    function e(i) {
        return this.each(function () {
            var r = n(this),
            u = r.data("bs.dropdown");
            u || r.data("bs.dropdown", u = new t(this));
            "string" == typeof i && u[i].call(r)
        })
    }
    var o = ".dropdown-backdrop",
    i = '[data-toggle="dropdown"]',
    t = function (t) {
        n(t).on("click.bs.dropdown", this.toggle)
    },
    f;
    t.VERSION = "3.3.4";
    t.prototype.toggle = function (t) {
        var f = n(this),
        i,
        o,
        e;
        if (!f.is(".disabled, :disabled")) {
            if (i = u(f), o = i.hasClass("open"), r(), !o) {
                if ("ontouchstart" in document.documentElement && !i.closest(".navbar-nav").length && n('<div class="dropdown-backdrop"/>').insertAfter(n(this)).on("click", r), e = {
                    relatedTarget: this
                },
                i.trigger(t = n.Event("show.bs.dropdown", e)), t.isDefaultPrevented()) return;
                f.trigger("focus").attr("aria-expanded", "true");
                i.toggleClass("open").trigger("shown.bs.dropdown", e)
            }
            return !1
        }
    };
    t.prototype.keydown = function (t) {
        var e, o, s, h, f, r;
        if (/(38|40|27|32)/.test(t.which) && !/input|textarea/i.test(t.target.tagName) && (e = n(this), t.preventDefault(), t.stopPropagation(), !e.is(".disabled, :disabled"))) {
            if (o = u(e), s = o.hasClass("open"), !s && 27 != t.which || s && 27 == t.which) return 27 == t.which && o.find(i).trigger("focus"),
            e.trigger("click");
            h = " li:not(.disabled):visible a";
            f = o.find('[role="menu"]' + h + ', [role="listbox"]' + h);
            f.length && (r = f.index(t.target), 38 == t.which && r > 0 && r--, 40 == t.which && r < f.length - 1 && r++, ~r || (r = 0), f.eq(r).trigger("focus"))
        }
    };
    f = n.fn.dropdown;
    n.fn.dropdown = e;
    n.fn.dropdown.Constructor = t;
    n.fn.dropdown.noConflict = function () {
        return n.fn.dropdown = f,
        this
    };
    n(document).on("click.bs.dropdown.data-api", r).on("click.bs.dropdown.data-api", ".dropdown form",
    function (n) {
        n.stopPropagation()
    }).on("click.bs.dropdown.data-api", i, t.prototype.toggle).on("keydown.bs.dropdown.data-api", i, t.prototype.keydown).on("keydown.bs.dropdown.data-api", '[role="menu"]', t.prototype.keydown).on("keydown.bs.dropdown.data-api", '[role="listbox"]', t.prototype.keydown)
}(jQuery); +
function (n) {
    "use strict";
    function i(i, r) {
        return this.each(function () {
            var f = n(this),
            u = f.data("bs.modal"),
            e = n.extend({},
            t.DEFAULTS, f.data(), "object" == typeof i && i);
            u || f.data("bs.modal", u = new t(this, e));
            "string" == typeof i ? u[i](r) : e.show && u.show(r)
        })
    }
    var t = function (t, i) {
        this.options = i;
        this.$body = n(document.body);
        this.$element = n(t);
        this.$dialog = this.$element.find(".modal-dialog");
        this.$backdrop = null;
        this.isShown = null;
        this.originalBodyPad = null;
        this.scrollbarWidth = 0;
        this.ignoreBackdropClick = !1;
        this.options.remote && this.$element.find(".modal-content").load(this.options.remote, n.proxy(function () {
            this.$element.trigger("loaded.bs.modal")
        },
        this))
    },
    r;
    t.VERSION = "3.3.4";
    t.TRANSITION_DURATION = 300;
    t.BACKDROP_TRANSITION_DURATION = 150;
    t.DEFAULTS = {
        backdrop: !0,
        keyboard: !0,
        show: !0
    };
    t.prototype.toggle = function (n) {
        return this.isShown ? this.hide() : this.show(n)
    };
    t.prototype.show = function (i) {
        var r = this,
        u = n.Event("show.bs.modal", {
            relatedTarget: i
        });
        this.$element.trigger(u);
        this.isShown || u.isDefaultPrevented() || (this.isShown = !0, this.checkScrollbar(), this.setScrollbar(), this.$body.addClass("modal-open"), this.escape(), this.resize(), this.$element.on("click.dismiss.bs.modal", '[data-dismiss="modal"]', n.proxy(this.hide, this)), this.$dialog.on("mousedown.dismiss.bs.modal",
        function () {
            r.$element.one("mouseup.dismiss.bs.modal",
            function (t) {
                n(t.target).is(r.$element) && (r.ignoreBackdropClick = !0)
            })
        }), this.backdrop(function () {
            var f = n.support.transition && r.$element.hasClass("fade"),
            u;
            r.$element.parent().length || r.$element.appendTo(r.$body);
            r.$element.show().scrollTop(0);
            r.adjustDialog();
            f && r.$element[0].offsetWidth;
            r.$element.addClass("in").attr("aria-hidden", !1);
            r.enforceFocus();
            u = n.Event("shown.bs.modal", {
                relatedTarget: i
            });
            f ? r.$dialog.one("bsTransitionEnd",
            function () {
                r.$element.trigger("focus").trigger(u)
            }).emulateTransitionEnd(t.TRANSITION_DURATION) : r.$element.trigger("focus").trigger(u)
        }))
    };
    t.prototype.hide = function (i) {
        i && i.preventDefault();
        i = n.Event("hide.bs.modal");
        this.$element.trigger(i);
        this.isShown && !i.isDefaultPrevented() && (this.isShown = !1, this.escape(), this.resize(), n(document).off("focusin.bs.modal"), this.$element.removeClass("in").attr("aria-hidden", !0).off("click.dismiss.bs.modal").off("mouseup.dismiss.bs.modal"), this.$dialog.off("mousedown.dismiss.bs.modal"), n.support.transition && this.$element.hasClass("fade") ? this.$element.one("bsTransitionEnd", n.proxy(this.hideModal, this)).emulateTransitionEnd(t.TRANSITION_DURATION) : this.hideModal())
    };
    t.prototype.enforceFocus = function () {
        n(document).off("focusin.bs.modal").on("focusin.bs.modal", n.proxy(function (n) {
            this.$element[0] === n.target || this.$element.has(n.target).length || this.$element.trigger("focus")
        },
        this))
    };
    t.prototype.escape = function () {
        this.isShown && this.options.keyboard ? this.$element.on("keydown.dismiss.bs.modal", n.proxy(function (n) {
            27 == n.which && this.hide()
        },
        this)) : this.isShown || this.$element.off("keydown.dismiss.bs.modal")
    };
    t.prototype.resize = function () {
        this.isShown ? n(window).on("resize.bs.modal", n.proxy(this.handleUpdate, this)) : n(window).off("resize.bs.modal")
    };
    t.prototype.hideModal = function () {
        var n = this;
        this.$element.hide();
        this.backdrop(function () {
            n.$body.removeClass("modal-open");
            n.resetAdjustments();
            n.resetScrollbar();
            n.$element.trigger("hidden.bs.modal")
        })
    };
    t.prototype.removeBackdrop = function () {
        this.$backdrop && this.$backdrop.remove();
        this.$backdrop = null
    };
    t.prototype.backdrop = function (i) {
        var e = this,
        f = this.$element.hasClass("fade") ? "fade" : "",
        r,
        u;
        if (this.isShown && this.options.backdrop) {
            if (r = n.support.transition && f, this.$backdrop = n('<div class="modal-backdrop ' + f + '" />').appendTo(this.$body), this.$element.on("click.dismiss.bs.modal", n.proxy(function (n) {
                return this.ignoreBackdropClick ? void (this.ignoreBackdropClick = !1) : void (n.target === n.currentTarget && ("static" == this.options.backdrop ? this.$element[0].focus() : this.hide()))
            },
            this)), r && this.$backdrop[0].offsetWidth, this.$backdrop.addClass("in"), !i) return;
            r ? this.$backdrop.one("bsTransitionEnd", i).emulateTransitionEnd(t.BACKDROP_TRANSITION_DURATION) : i()
        } else !this.isShown && this.$backdrop ? (this.$backdrop.removeClass("in"), u = function () {
            e.removeBackdrop();
            i && i()
        },
        n.support.transition && this.$element.hasClass("fade") ? this.$backdrop.one("bsTransitionEnd", u).emulateTransitionEnd(t.BACKDROP_TRANSITION_DURATION) : u()) : i && i()
    };
    t.prototype.handleUpdate = function () {
        this.adjustDialog()
    };
    t.prototype.adjustDialog = function () {
        var n = this.$element[0].scrollHeight > document.documentElement.clientHeight;
        this.$element.css({
            paddingLeft: !this.bodyIsOverflowing && n ? this.scrollbarWidth : "",
            paddingRight: this.bodyIsOverflowing && !n ? this.scrollbarWidth : ""
        })
    };
    t.prototype.resetAdjustments = function () {
        this.$element.css({
            paddingLeft: "",
            paddingRight: ""
        })
    };
    t.prototype.checkScrollbar = function () {
        var n = window.innerWidth,
        t;
        n || (t = document.documentElement.getBoundingClientRect(), n = t.right - Math.abs(t.left));
        this.bodyIsOverflowing = document.body.clientWidth < n;
        this.scrollbarWidth = this.measureScrollbar()
    };
    t.prototype.setScrollbar = function () {
        var n = parseInt(this.$body.css("padding-right") || 0, 10);
        this.originalBodyPad = document.body.style.paddingRight || "";
        this.bodyIsOverflowing && this.$body.css("padding-right", n + this.scrollbarWidth)
    };
    t.prototype.resetScrollbar = function () {
        this.$body.css("padding-right", this.originalBodyPad)
    };
    t.prototype.measureScrollbar = function () {
        var n = document.createElement("div"),
        t;
        return n.className = "modal-scrollbar-measure",
        this.$body.append(n),
        t = n.offsetWidth - n.clientWidth,
        this.$body[0].removeChild(n),
        t
    };
    r = n.fn.modal;
    n.fn.modal = i;
    n.fn.modal.Constructor = t;
    n.fn.modal.noConflict = function () {
        return n.fn.modal = r,
        this
    };
    n(document).on("click.bs.modal.data-api", '[data-toggle="modal"]',
    function (t) {
        var r = n(this),
        f = r.attr("href"),
        u = n(r.attr("data-target") || f && f.replace(/.*(?=#[^\s]+$)/, "")),
        e = u.data("bs.modal") ? "toggle" : n.extend({
            remote: !/#/.test(f) && f
        },
        u.data(), r.data());
        r.is("a") && t.preventDefault();
        u.one("show.bs.modal",
        function (n) {
            n.isDefaultPrevented() || u.one("hidden.bs.modal",
            function () {
                r.is(":visible") && r.trigger("focus")
            })
        });
        i.call(u, e, this)
    })
}(jQuery); +
function (n) {
    "use strict";
    function r(i) {
        return this.each(function () {
            var u = n(this),
            r = u.data("bs.tooltip"),
            f = "object" == typeof i && i; (r || !/destroy|hide/.test(i)) && (r || u.data("bs.tooltip", r = new t(this, f)), "string" == typeof i && r[i]())
        })
    }
    var t = function (n, t) {
        this.type = null;
        this.options = null;
        this.enabled = null;
        this.timeout = null;
        this.hoverState = null;
        this.$element = null;
        this.init("tooltip", n, t)
    },
    i;
    t.VERSION = "3.3.4";
    t.TRANSITION_DURATION = 150;
    t.DEFAULTS = {
        animation: !0,
        placement: "top",
        selector: !1,
        template: '<div class="tooltip" role="tooltip"><div class="tooltip-arrow"><\/div><div class="tooltip-inner"><\/div><\/div>',
        trigger: "hover focus",
        title: "",
        delay: 0,
        html: !1,
        container: !1,
        viewport: {
            selector: "body",
            padding: 0
        }
    };
    t.prototype.init = function (t, i, r) {
        var f, e, u, o, s;
        if (this.enabled = !0, this.type = t, this.$element = n(i), this.options = this.getOptions(r), this.$viewport = this.options.viewport && n(this.options.viewport.selector || this.options.viewport), this.$element[0] instanceof document.constructor && !this.options.selector) throw new Error("`selector` option must be specified when initializing " + this.type + " on the window.document object!");
        for (f = this.options.trigger.split(" "), e = f.length; e--;) if (u = f[e], "click" == u) this.$element.on("click." + this.type, this.options.selector, n.proxy(this.toggle, this));
        else "manual" != u && (o = "hover" == u ? "mouseenter" : "focusin", s = "hover" == u ? "mouseleave" : "focusout", this.$element.on(o + "." + this.type, this.options.selector, n.proxy(this.enter, this)), this.$element.on(s + "." + this.type, this.options.selector, n.proxy(this.leave, this)));
        this.options.selector ? this._options = n.extend({},
        this.options, {
            trigger: "manual",
            selector: ""
        }) : this.fixTitle()
    };
    t.prototype.getDefaults = function () {
        return t.DEFAULTS
    };
    t.prototype.getOptions = function (t) {
        return t = n.extend({},
        this.getDefaults(), this.$element.data(), t),
        t.delay && "number" == typeof t.delay && (t.delay = {
            show: t.delay,
            hide: t.delay
        }),
        t
    };
    t.prototype.getDelegateOptions = function () {
        var t = {},
        i = this.getDefaults();
        return this._options && n.each(this._options,
        function (n, r) {
            i[n] != r && (t[n] = r)
        }),
        t
    };
    t.prototype.enter = function (t) {
        var i = t instanceof this.constructor ? t : n(t.currentTarget).data("bs." + this.type);
        return i && i.$tip && i.$tip.is(":visible") ? void (i.hoverState = "in") : (i || (i = new this.constructor(t.currentTarget, this.getDelegateOptions()), n(t.currentTarget).data("bs." + this.type, i)), clearTimeout(i.timeout), i.hoverState = "in", i.options.delay && i.options.delay.show ? void (i.timeout = setTimeout(function () {
            "in" == i.hoverState && i.show()
        },
        i.options.delay.show)) : i.show())
    };
    t.prototype.leave = function (t) {
        var i = t instanceof this.constructor ? t : n(t.currentTarget).data("bs." + this.type);
        return i || (i = new this.constructor(t.currentTarget, this.getDelegateOptions()), n(t.currentTarget).data("bs." + this.type, i)),
        clearTimeout(i.timeout),
        i.hoverState = "out",
        i.options.delay && i.options.delay.hide ? void (i.timeout = setTimeout(function () {
            "out" == i.hoverState && i.hide()
        },
        i.options.delay.hide)) : i.hide()
    };
    t.prototype.show = function () {
        var c = n.Event("show.bs." + this.type),
        l,
        p,
        h;
        if (this.hasContent() && this.enabled) {
            if (this.$element.trigger(c), l = n.contains(this.$element[0].ownerDocument.documentElement, this.$element[0]), c.isDefaultPrevented() || !l) return;
            var u = this,
            r = this.tip(),
            a = this.getUID(this.type);
            this.setContent();
            r.attr("id", a);
            this.$element.attr("aria-describedby", a);
            this.options.animation && r.addClass("fade");
            var i = "function" == typeof this.options.placement ? this.options.placement.call(this, r[0], this.$element[0]) : this.options.placement,
            v = /\s?auto?\s?/i,
            y = v.test(i);
            y && (i = i.replace(v, "") || "top");
            r.detach().css({
                top: 0,
                left: 0,
                display: "block"
            }).addClass(i).data("bs." + this.type, this);
            this.options.container ? r.appendTo(this.options.container) : r.insertAfter(this.$element);
            var f = this.getPosition(),
            o = r[0].offsetWidth,
            s = r[0].offsetHeight;
            if (y) {
                var w = i,
                b = this.options.container ? n(this.options.container) : this.$element.parent(),
                e = this.getPosition(b);
                i = "bottom" == i && f.bottom + s > e.bottom ? "top" : "top" == i && f.top - s < e.top ? "bottom" : "right" == i && f.right + o > e.width ? "left" : "left" == i && f.left - o < e.left ? "right" : i;
                r.removeClass(w).addClass(i)
            }
            p = this.getCalculatedOffset(i, f, o, s);
            this.applyPlacement(p, i);
            h = function () {
                var n = u.hoverState;
                u.$element.trigger("shown.bs." + u.type);
                u.hoverState = null;
                "out" == n && u.leave(u)
            };
            n.support.transition && this.$tip.hasClass("fade") ? r.one("bsTransitionEnd", h).emulateTransitionEnd(t.TRANSITION_DURATION) : h()
        }
    };
    t.prototype.applyPlacement = function (t, i) {
        var r = this.tip(),
        l = r[0].offsetWidth,
        e = r[0].offsetHeight,
        o = parseInt(r.css("margin-top"), 10),
        s = parseInt(r.css("margin-left"), 10),
        h,
        f,
        u;
        isNaN(o) && (o = 0);
        isNaN(s) && (s = 0);
        t.top = t.top + o;
        t.left = t.left + s;
        n.offset.setOffset(r[0], n.extend({
            using: function (n) {
                r.css({
                    top: Math.round(n.top),
                    left: Math.round(n.left)
                })
            }
        },
        t), 0);
        r.addClass("in");
        h = r[0].offsetWidth;
        f = r[0].offsetHeight;
        "top" == i && f != e && (t.top = t.top + e - f);
        u = this.getViewportAdjustedDelta(i, t, h, f);
        u.left ? t.left += u.left : t.top += u.top;
        var c = /top|bottom/.test(i),
        a = c ? 2 * u.left - l + h : 2 * u.top - e + f,
        v = c ? "offsetWidth" : "offsetHeight";
        r.offset(t);
        this.replaceArrow(a, r[0][v], c)
    };
    t.prototype.replaceArrow = function (n, t, i) {
        this.arrow().css(i ? "left" : "top", 50 * (1 - n / t) + "%").css(i ? "top" : "left", "")
    };
    t.prototype.setContent = function () {
        var n = this.tip(),
        t = this.getTitle();
        n.find(".tooltip-inner")[this.options.html ? "html" : "text"](t);
        n.removeClass("fade in top bottom left right")
    };
    t.prototype.hide = function (i) {
        function f() {
            "in" != u.hoverState && r.detach();
            u.$element.removeAttr("aria-describedby").trigger("hidden.bs." + u.type);
            i && i()
        }
        var u = this,
        r = n(this.$tip),
        e = n.Event("hide.bs." + this.type);
        return this.$element.trigger(e),
        e.isDefaultPrevented() ? void 0 : (r.removeClass("in"), n.support.transition && r.hasClass("fade") ? r.one("bsTransitionEnd", f).emulateTransitionEnd(t.TRANSITION_DURATION) : f(), this.hoverState = null, this)
    };
    t.prototype.fixTitle = function () {
        var n = this.$element; (n.attr("title") || "string" != typeof n.attr("data-original-title")) && n.attr("data-original-title", n.attr("title") || "").attr("title", "")
    };
    t.prototype.hasContent = function () {
        return this.getTitle()
    };
    t.prototype.getPosition = function (t) {
        t = t || this.$element;
        var u = t[0],
        r = "BODY" == u.tagName,
        i = u.getBoundingClientRect();
        null == i.width && (i = n.extend({},
        i, {
            width: i.right - i.left,
            height: i.bottom - i.top
        }));
        var f = r ? {
            top: 0,
            left: 0
        } : t.offset(),
        e = {
            scroll: r ? document.documentElement.scrollTop || document.body.scrollTop : t.scrollTop()
        },
        o = r ? {
            width: n(window).width(),
            height: n(window).height()
        } : null;
        return n.extend({},
        i, e, o, f)
    };
    t.prototype.getCalculatedOffset = function (n, t, i, r) {
        return "bottom" == n ? {
            top: t.top + t.height,
            left: t.left + t.width / 2 - i / 2
        } : "top" == n ? {
            top: t.top - r,
            left: t.left + t.width / 2 - i / 2
        } : "left" == n ? {
            top: t.top + t.height / 2 - r / 2,
            left: t.left - i
        } : {
            top: t.top + t.height / 2 - r / 2,
            left: t.left + t.width
        }
    };
    t.prototype.getViewportAdjustedDelta = function (n, t, i, r) {
        var f = {
            top: 0,
            left: 0
        },
        e,
        u,
        o,
        s,
        h,
        c;
        return this.$viewport ? (e = this.options.viewport && this.options.viewport.padding || 0, u = this.getPosition(this.$viewport), /right|left/.test(n) ? (o = t.top - e - u.scroll, s = t.top + e - u.scroll + r, o < u.top ? f.top = u.top - o : s > u.top + u.height && (f.top = u.top + u.height - s)) : (h = t.left - e, c = t.left + e + i, h < u.left ? f.left = u.left - h : c > u.width && (f.left = u.left + u.width - c)), f) : f
    };
    t.prototype.getTitle = function () {
        var t = this.$element,
        n = this.options;
        return t.attr("data-original-title") || ("function" == typeof n.title ? n.title.call(t[0]) : n.title)
    };
    t.prototype.getUID = function (n) {
        do n += ~~(1e6 * Math.random());
        while (document.getElementById(n));
        return n
    };
    t.prototype.tip = function () {
        return this.$tip = this.$tip || n(this.options.template)
    };
    t.prototype.arrow = function () {
        return this.$arrow = this.$arrow || this.tip().find(".tooltip-arrow")
    };
    t.prototype.enable = function () {
        this.enabled = !0
    };
    t.prototype.disable = function () {
        this.enabled = !1
    };
    t.prototype.toggleEnabled = function () {
        this.enabled = !this.enabled
    };
    t.prototype.toggle = function (t) {
        var i = this;
        t && (i = n(t.currentTarget).data("bs." + this.type), i || (i = new this.constructor(t.currentTarget, this.getDelegateOptions()), n(t.currentTarget).data("bs." + this.type, i)));
        i.tip().hasClass("in") ? i.leave(i) : i.enter(i)
    };
    t.prototype.destroy = function () {
        var n = this;
        clearTimeout(this.timeout);
        this.hide(function () {
            n.$element.off("." + n.type).removeData("bs." + n.type)
        })
    };
    i = n.fn.tooltip;
    n.fn.tooltip = r;
    n.fn.tooltip.Constructor = t;
    n.fn.tooltip.noConflict = function () {
        return n.fn.tooltip = i,
        this
    }
}(jQuery); +
function (n) {
    "use strict";
    function r(i) {
        return this.each(function () {
            var u = n(this),
            r = u.data("bs.popover"),
            f = "object" == typeof i && i; (r || !/destroy|hide/.test(i)) && (r || u.data("bs.popover", r = new t(this, f)), "string" == typeof i && r[i]())
        })
    }
    var t = function (n, t) {
        this.init("popover", n, t)
    },
    i;
    if (!n.fn.tooltip) throw new Error("Popover requires tooltip.js");
    t.VERSION = "3.3.4";
    t.DEFAULTS = n.extend({},
    n.fn.tooltip.Constructor.DEFAULTS, {
        placement: "right",
        trigger: "click",
        content: "",
        template: '<div class="popover" role="tooltip"><div class="arrow"><\/div><h3 class="popover-title"><\/h3><div class="popover-content"><\/div><\/div>'
    });
    t.prototype = n.extend({},
    n.fn.tooltip.Constructor.prototype);
    t.prototype.constructor = t;
    t.prototype.getDefaults = function () {
        return t.DEFAULTS
    };
    t.prototype.setContent = function () {
        var n = this.tip(),
        i = this.getTitle(),
        t = this.getContent();
        n.find(".popover-title")[this.options.html ? "html" : "text"](i);
        n.find(".popover-content").children().detach().end()[this.options.html ? "string" == typeof t ? "html" : "append" : "text"](t);
        n.removeClass("fade top bottom left right in");
        n.find(".popover-title").html() || n.find(".popover-title").hide()
    };
    t.prototype.hasContent = function () {
        return this.getTitle() || this.getContent()
    };
    t.prototype.getContent = function () {
        var t = this.$element,
        n = this.options;
        return t.attr("data-content") || ("function" == typeof n.content ? n.content.call(t[0]) : n.content)
    };
    t.prototype.arrow = function () {
        return this.$arrow = this.$arrow || this.tip().find(".arrow")
    };
    i = n.fn.popover;
    n.fn.popover = r;
    n.fn.popover.Constructor = t;
    n.fn.popover.noConflict = function () {
        return n.fn.popover = i,
        this
    }
}(jQuery); +
function (n) {
    "use strict";
    function t(i, r) {
        this.$body = n(document.body);
        this.$scrollElement = n(n(i).is(document.body) ? window : i);
        this.options = n.extend({},
        t.DEFAULTS, r);
        this.selector = (this.options.target || "") + " .nav li > a";
        this.offsets = [];
        this.targets = [];
        this.activeTarget = null;
        this.scrollHeight = 0;
        this.$scrollElement.on("scroll.bs.scrollspy", n.proxy(this.process, this));
        this.refresh();
        this.process()
    }
    function i(i) {
        return this.each(function () {
            var u = n(this),
            r = u.data("bs.scrollspy"),
            f = "object" == typeof i && i;
            r || u.data("bs.scrollspy", r = new t(this, f));
            "string" == typeof i && r[i]()
        })
    }
    t.VERSION = "3.3.4";
    t.DEFAULTS = {
        offset: 10
    };
    t.prototype.getScrollHeight = function () {
        return this.$scrollElement[0].scrollHeight || Math.max(this.$body[0].scrollHeight, document.documentElement.scrollHeight)
    };
    t.prototype.refresh = function () {
        var t = this,
        i = "offset",
        r = 0;
        this.offsets = [];
        this.targets = [];
        this.scrollHeight = this.getScrollHeight();
        n.isWindow(this.$scrollElement[0]) || (i = "position", r = this.$scrollElement.scrollTop());
        this.$body.find(this.selector).map(function () {
            var f = n(this),
            u = f.data("target") || f.attr("href"),
            t = /^#./.test(u) && n(u);
            return t && t.length && t.is(":visible") && [[t[i]().top + r, u]] || null
        }).sort(function (n, t) {
            return n[0] - t[0]
        }).each(function () {
            t.offsets.push(this[0]);
            t.targets.push(this[1])
        })
    };
    t.prototype.process = function () {
        var n, i = this.$scrollElement.scrollTop() + this.options.offset,
        f = this.getScrollHeight(),
        e = this.options.offset + f - this.$scrollElement.height(),
        t = this.offsets,
        r = this.targets,
        u = this.activeTarget;
        if (this.scrollHeight != f && this.refresh(), i >= e) return u != (n = r[r.length - 1]) && this.activate(n);
        if (u && i < t[0]) return this.activeTarget = null,
        this.clear();
        for (n = t.length; n--;) u != r[n] && i >= t[n] && (void 0 === t[n + 1] || i < t[n + 1]) && this.activate(r[n])
    };
    t.prototype.activate = function (t) {
        this.activeTarget = t;
        this.clear();
        var r = this.selector + '[data-target="' + t + '"],' + this.selector + '[href="' + t + '"]',
        i = n(r).parents("li").addClass("active");
        i.parent(".dropdown-menu").length && (i = i.closest("li.dropdown").addClass("active"));
        i.trigger("activate.bs.scrollspy")
    };
    t.prototype.clear = function () {
        n(this.selector).parentsUntil(this.options.target, ".active").removeClass("active")
    };
    var r = n.fn.scrollspy;
    n.fn.scrollspy = i;
    n.fn.scrollspy.Constructor = t;
    n.fn.scrollspy.noConflict = function () {
        return n.fn.scrollspy = r,
        this
    };
    n(window).on("load.bs.scrollspy.data-api",
    function () {
        n('[data-spy="scroll"]').each(function () {
            var t = n(this);
            i.call(t, t.data())
        })
    })
}(jQuery); +
function (n) {
    "use strict";
    function r(i) {
        return this.each(function () {
            var u = n(this),
            r = u.data("bs.tab");
            r || u.data("bs.tab", r = new t(this));
            "string" == typeof i && r[i]()
        })
    }
    var t = function (t) {
        this.element = n(t)
    },
    u,
    i;
    t.VERSION = "3.3.4";
    t.TRANSITION_DURATION = 150;
    t.prototype.show = function () {
        var t = this.element,
        f = t.closest("ul:not(.dropdown-menu)"),
        i = t.data("target"),
        u;
        if (i || (i = t.attr("href"), i = i && i.replace(/.*(?=#[^\s]*$)/, "")), !t.parent("li").hasClass("active")) {
            var r = f.find(".active:last a"),
            e = n.Event("hide.bs.tab", {
                relatedTarget: t[0]
            }),
            o = n.Event("show.bs.tab", {
                relatedTarget: r[0]
            }); (r.trigger(e), t.trigger(o), o.isDefaultPrevented() || e.isDefaultPrevented()) || (u = n(i), this.activate(t.closest("li"), f), this.activate(u, u.parent(),
            function () {
                r.trigger({
                    type: "hidden.bs.tab",
                    relatedTarget: t[0]
                });
                t.trigger({
                    type: "shown.bs.tab",
                    relatedTarget: r[0]
                })
            }))
        }
    };
    t.prototype.activate = function (i, r, u) {
        function e() {
            f.removeClass("active").find("> .dropdown-menu > .active").removeClass("active").end().find('[data-toggle="tab"]').attr("aria-expanded", !1);
            i.addClass("active").find('[data-toggle="tab"]').attr("aria-expanded", !0);
            o ? (i[0].offsetWidth, i.addClass("in")) : i.removeClass("fade");
            i.parent(".dropdown-menu").length && i.closest("li.dropdown").addClass("active").end().find('[data-toggle="tab"]').attr("aria-expanded", !0);
            u && u()
        }
        var f = r.find("> .active"),
        o = u && n.support.transition && (f.length && f.hasClass("fade") || !!r.find("> .fade").length);
        f.length && o ? f.one("bsTransitionEnd", e).emulateTransitionEnd(t.TRANSITION_DURATION) : e();
        f.removeClass("in")
    };
    u = n.fn.tab;
    n.fn.tab = r;
    n.fn.tab.Constructor = t;
    n.fn.tab.noConflict = function () {
        return n.fn.tab = u,
        this
    };
    i = function (t) {
        t.preventDefault();
        r.call(n(this), "show")
    };
    n(document).on("click.bs.tab.data-api", '[data-toggle="tab"]', i).on("click.bs.tab.data-api", '[data-toggle="pill"]', i)
}(jQuery); +
function (n) {
    "use strict";
    function i(i) {
        return this.each(function () {
            var u = n(this),
            r = u.data("bs.affix"),
            f = "object" == typeof i && i;
            r || u.data("bs.affix", r = new t(this, f));
            "string" == typeof i && r[i]()
        })
    }
    var t = function (i, r) {
        this.options = n.extend({},
        t.DEFAULTS, r);
        this.$target = n(this.options.target).on("scroll.bs.affix.data-api", n.proxy(this.checkPosition, this)).on("click.bs.affix.data-api", n.proxy(this.checkPositionWithEventLoop, this));
        this.$element = n(i);
        this.affixed = null;
        this.unpin = null;
        this.pinnedOffset = null;
        this.checkPosition()
    },
    r;
    t.VERSION = "3.3.4";
    t.RESET = "affix affix-top affix-bottom";
    t.DEFAULTS = {
        offset: 0,
        target: window
    };
    t.prototype.getState = function (n, t, i, r) {
        var u = this.$target.scrollTop(),
        f = this.$element.offset(),
        e = this.$target.height();
        if (null != i && "top" == this.affixed) return i > u ? "top" : !1;
        if ("bottom" == this.affixed) return null != i ? u + this.unpin <= f.top ? !1 : "bottom" : n - r >= u + e ? !1 : "bottom";
        var o = null == this.affixed,
        s = o ? u : f.top,
        h = o ? e : t;
        return null != i && i >= u ? "top" : null != r && s + h >= n - r ? "bottom" : !1
    };
    t.prototype.getPinnedOffset = function () {
        if (this.pinnedOffset) return this.pinnedOffset;
        this.$element.removeClass(t.RESET).addClass("affix");
        var n = this.$target.scrollTop(),
        i = this.$element.offset();
        return this.pinnedOffset = i.top - n
    };
    t.prototype.checkPositionWithEventLoop = function () {
        setTimeout(n.proxy(this.checkPosition, this), 1)
    };
    t.prototype.checkPosition = function () {
        var i, e, o;
        if (this.$element.is(":visible")) {
            var s = this.$element.height(),
            r = this.options.offset,
            f = r.top,
            u = r.bottom,
            h = n(document.body).height();
            if ("object" != typeof r && (u = f = r), "function" == typeof f && (f = r.top(this.$element)), "function" == typeof u && (u = r.bottom(this.$element)), i = this.getState(h, s, f, u), this.affixed != i) {
                if (null != this.unpin && this.$element.css("top", ""), e = "affix" + (i ? "-" + i : ""), o = n.Event(e + ".bs.affix"), this.$element.trigger(o), o.isDefaultPrevented()) return;
                this.affixed = i;
                this.unpin = "bottom" == i ? this.getPinnedOffset() : null;
                this.$element.removeClass(t.RESET).addClass(e).trigger(e.replace("affix", "affixed") + ".bs.affix")
            }
            "bottom" == i && this.$element.offset({
                top: h - s - u
            })
        }
    };
    r = n.fn.affix;
    n.fn.affix = i;
    n.fn.affix.Constructor = t;
    n.fn.affix.noConflict = function () {
        return n.fn.affix = r,
        this
    };
    n(window).on("load",
    function () {
        n('[data-spy="affix"]').each(function () {
            var r = n(this),
            t = r.data();
            t.offset = t.offset || {};
            null != t.offsetBottom && (t.offset.bottom = t.offsetBottom);
            null != t.offsetTop && (t.offset.top = t.offsetTop);
            i.call(r, t)
        })
    })
}(jQuery),
function (n) {
    "use strict";
    var t = function (i, r) {
        this.$element = n(i);
        this.options = n.extend({},
        t.defaults, r)
    },
    i;
    t.defaults = {
        transition_delay: 300,
        refresh_speed: 50,
        display_text: "none",
        use_percentage: !0,
        percent_format: function (n) {
            return n + "%"
        },
        amount_format: function (n, t) {
            return n + " / " + t
        },
        update: n.noop,
        done: n.noop,
        fail: n.noop
    };
    t.prototype.transition = function () {
        var u = this.$element,
        f = u.parent(),
        o = this.$back_text,
        e = this.$front_text,
        i = this.options,
        h = u.attr("aria-valuetransitiongoal"),
        c = u.attr("aria-valuemin") || 0,
        l = u.attr("aria-valuemax") || 100,
        a = f.hasClass("vertical"),
        v = i.update && typeof i.update == "function" ? i.update : t.defaults.update,
        y = i.done && typeof i.done == "function" ? i.done : t.defaults.done,
        p = i.fail && typeof i.fail == "function" ? i.fail : t.defaults.fail,
        s,
        r;
        if (!h) {
            p("aria-valuetransitiongoal not set");
            return
        }
        s = Math.round(100 * (h - c) / (l - c));
        i.display_text !== "center" || o || e || (this.$back_text = o = n("<span>", {
            "class": "progressbar-back-text"
        }).prependTo(f), this.$front_text = e = n("<span>", {
            "class": "progressbar-front-text"
        }).prependTo(u), a ? (r = f.css("height"), o.css({
            height: r,
            "line-height": r
        }), e.css({
            height: r,
            "line-height": r
        }), n(window).resize(function () {
            r = f.css("height");
            o.css({
                height: r,
                "line-height": r
            });
            e.css({
                height: r,
                "line-height": r
            })
        })) : (r = f.css("width"), e.css({
            width: r
        }), n(window).resize(function () {
            r = f.css("width");
            e.css({
                width: r
            })
        })));
        setTimeout(function () {
            var n, t, r, p, w, b;
            a ? u.css("height", s + "%") : u.css("width", s + "%");
            b = setInterval(function () {
                a ? (r = u.height(), p = f.height()) : (r = u.width(), p = f.width());
                n = Math.round(100 * r / p);
                t = Math.round(r / p * (l - c));
                n >= s && (n = s, t = h, y(), clearInterval(b));
                i.display_text !== "none" && (w = i.use_percentage ? i.percent_format(n) : i.amount_format(t, l), i.display_text === "fill" ? u.text(w) : i.display_text === "center" && (o.text(w), e.text(w)));
                u.attr("aria-valuenow", t);
                v(n)
            },
            i.refresh_speed)
        },
        i.transition_delay)
    };
    i = n.fn.progressbar;
    n.fn.progressbar = function (i) {
        return this.each(function () {
            var u = n(this),
            r = u.data("bs.progressbar"),
            f = typeof i == "object" && i;
            r || u.data("bs.progressbar", r = new t(this, f));
            r.transition()
        })
    };
    n.fn.progressbar.Constructor = t;
    n.fn.progressbar.noConflict = function () {
        return n.fn.progressbar = i,
        this
    }
}(window.jQuery);

$.fn.initValidform = function () {
    var n = function (n) {
        $(n).Validform({
            tiptype: function (n, t, i) {
                if (!t.obj.is("form")) {
                    t.obj.parent("div").find(".Validform_checktip").length == 0 && (t.obj.parent("div").append("<label class='Validform_checktip' />"), t.obj.parent("div").next().find(".Validform_checktip").remove());
                    var r = t.obj.parent("div").find(".Validform_checktip");
                    i(r, t.type);
                    r.text(n)
                }
            },
            showAllError: !0
        })
    };
    return $(this).each(function () {
        n($(this))
    })
}