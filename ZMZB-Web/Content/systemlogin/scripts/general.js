/**
 * This function is for stripping leading and trailing spaces
 */
String.prototype.trim = function(){
    return this.replace(/^\s*(\S*(\s+\S+)*)\s*$/, "$1");
};

/**
 * This function is used to set cookies
 */
function setEcscCookie(name, value, expires, path, domain, secure){
    document.cookie = name + "=" + escape(value) + ((expires) ? "; expires=" + expires.toGMTString() : "") + ((path) ? "; path=" + path : "") + ((domain) ? "; domain=" + domain : "") + ((secure) ? "; secure" : "");
}

/**
 * This function is used to get cookies
 */
function getEcscCookie(name){
    var prefix = name + "=";
    var start = document.cookie.indexOf(prefix);
    
    if (start == -1) {
        return null;
    }
    
    var end = document.cookie.indexOf(";", start + prefix.length);
    if (end == -1) {
        end = document.cookie.length;
    }
    
    var value = document.cookie.substring(start + prefix.length, end);
    return unescape(value);
}

/**
 * This function is used to delete cookies
 */
function deleteEcscCookie(name, path, domain){
    if (getCookie(name)) {
        document.cookie = name + "=" + ((path) ? "; path=" + path : "") +
        ((domain) ? "; domain=" + domain : "") +
        "; expires=Thu, 01-Jan-70 00:00:01 GMT";
    }
}

// Show the document's title on the status bar
window.defaultStatus = document.title;

var ECSC = {};
ECSC.ctxPath = ctx;
ECSC.readyFun = [];

ECSC.include = function(file){
    document.writeln('<script type="text/javascript" src="' + file + '"></script>');
};

ECSC.loadDwr = function(dwrs){
    ECSC.loadJs(dwrs, ctx + "/dwr/interface/");
};

ECSC.loadJs = function(jss, path){
    var jss = (jss ? jss : "").split(",");
    for (var i = 0; i < jss.length; i++) {
        ECSC.include(path + jss[i] + ".js");
    }
};

ECSC.apply = function(o, c, defaults){
    if(defaults){
        ECSC.apply(o, defaults);
    }
    if(o && c && typeof c == 'object'){
        for(var p in c){
            o[p] = c[p];
        }
    }
    return o;
};


ECSC.alert = function(msg, title, callback,option){
    msg = '<div style="text-align:left;margin:10px;">' + msg + '</div>';
    var closeFun = function(){
        this.close();
        if (callback) {
            if (typeof callback === 'function') {
                callback();
            } else if (typeof callback === 'object') {
                callback.fn(callback.params);
            }
        }
    };
    var ori = {};
    ECSC.apply(ori,option || {},{
        title: title || '信息',
        autosize: true,
        width: 300,
        height: 100,
        lightbox: true,
        button: {
            ok: ['确定', closeFun]
        },
        onShow: function(){
            $('div.dialog button').focus();
        }
    });
    Dialogs.alert(msg,ori);
};

ECSC.confirm = function(msg, title, callback){
    msg = '<div style="text-align:left;margin:10px;">' + msg + '</div>';
    var okFun = function(){
        this.close();
        if (callback) {
            if (typeof callback === 'function') {
                callback();
            } else if (typeof callback === 'object' && callback.ok_fn) {
                callback.ok_fn(callback.ok_params);
            }
        }
    };
    
    var closeFun = function(){
        this.close();
        if (callback) {
            if (typeof callback === 'function') {
                callback();
            } else if (typeof callback === 'object' && callback.no_fn) {
                callback.no_fn(callback.no_params);
            }
        }
    };
    
    Dialogs.confirm(msg, {
        title: title || '信息',
        autosize: true,
        width: 300,
        height: 100,
        lightbox: true,
        button: {
            ok: ['确定', okFun],
            cancel: ['取消', closeFun]
        },
        onShow: function(){
            $('div.dialog button').focus();
        }
    });
};

ECSC.showWindow = function(msg, title, options){
    var btns = {};
    if (options.buttons) {
        for (var i = 0; i < options.buttons.length; i++) {
            var obj = options.buttons[i];
            btns['label' + i] = [obj.label, obj.fn];
        }
    }
    
    msg = '<div style="text-align:left;margin:10px;">' + msg + '</div>';
    Dialogs.opendialog(msg, {
        title: title || '信息',
        width: options.width || 500,
        height: options.height || 500,
        lightbox: true,
        bgcolor: options.bgcolor || '',
        closable: options.closable !== undefined ? options.closable : true,
        button: btns
    });
};

ECSC.loading = function(msg){
    ECSC.showWindow('<div style="text-align:center;width:100%;"><img src="' + ctx + '/images/loading.gif" width="32" height="32" align="middle" />　' + msg + '</div>', '提示', {
        closable: false,
        height: 50
    });
};

ECSC.closeWindow = function(fun, time){
    setTimeout('Dialogs.close();', time || 0);
    setTimeout(fun, time || 0);
};

ECSC.loadingForFee = function(fun){
    ECSC.showWindow('<div style="text-align:center;width:100%;"><img src="' + ctx + '/images/loading.gif" width="32" height="32" align="middle" />　数据加载中，请稍候！</div>', '提示', {
        closable: false,
        height: 50
    });
    setTimeout(fun, 3000);
}

function myAddBookmark(title,url){
    if((typeof window.sidebar == "object") && (typeof window.sidebar.addPanel == "function"))//Gecko
    {
    	window.sidebar.addPanel(title,url,"");
    }else{
        window.external.AddFavorite(url,title);
    }
}
function SetHome(obj, vrl) {
	try {
		obj.style.behavior = 'url(#default#homepage)';
		obj.setHomePage(vrl);
	} catch (e) {
		if (window.netscape) {
			try {
				netscape.security.PrivilegeManager
						.enablePrivilege("UniversalXPConnect");
			} catch (e) {
				alert("此操作被浏览器拒绝！\n请在浏览器地址栏输入“about:config”并回车\n然后将[signed.applets.codebase_principal_support]设置为'true'");
			}
			var prefs = Components.classes['@mozilla.org/preferences-service;1']
					.getService(Components.interfaces.nsIPrefBranch);
			prefs.setCharPref('browser.startup.homepage', vrl);
		}
	}
}
$(document).ready(function(){
   	var pathName = location.pathname;
    ChangeMenuImg(pathName);
});

var ChangeMenuImg = function(pathname) {
    var flag = true;
    var id = 0;
    switch (flag) {
    	//电影	
    	case (pathname.indexOf('/movie') > -1) && true:
    		$("#headerMenu >li").each(function(i){$(this).attr("class","menu4")});
    		$("#headerMenu >li#menu_movie").attr("class","menu5");
    		break;     		
        // 主页
        default:
    		$("#headerMenu >li").each(function(i){$(this).attr("class","menu4")});
    		$("#headerMenu >li#menu_index").attr("class","menu5");
            break;    
	}
}

function UrlSearch(){
    var name, value;
    var str = location.href; //取得整个地址栏
    var num = str.indexOf("?")
    str = str.substr(num + 1); //取得所有参数
    var arr = str.split("&"); //各个参数放到数组里
    for (var i = 0; i < arr.length; i++) {
        num = arr[i].indexOf("=");
        if (num > 0) {
            name = arr[i].substring(0, num);
            value = arr[i].substr(num + 1);
            this[name] = value;
        }
    }
}