ECSC.loadDwr("movieDwr");

var isUser = function() {
	var userName = $("#userName").val();
	var password = $("#password").val();
	var reUrl = $("#reUrl").val();
	if (userName == "") {
		alert("请输入用户名");
		getImage();
		return;
	} else if (password == "") {
		alert("请输入密码");
		getImage();
		return;
	} else {
		movieDwr.valCode($("#validateCode").val(),{
			callback : function(msg) {
				if (msg == 1) {
					alert("验证码错误!");
					getImage();
					return;
				} else {
					movieDwr.isUser(userName,password,{
						callback : function(user) {
							if (user == null || user.length == 0) {
								alert("用户名和密码错误");
								getImage();
							} else {
								alert("登陆成功！");
								if(gourl.length < 10){
									window.location = 'index.do';
								}else{
									window.location=gourl;
								}
							}
						},
						async : false
					});
				}
			},
			async : false
		});
	}
}
var doLocation = function(filmId, useType) {
	if (filmId == 0 || filmId == "0") {
		return;
	}

	// 鍒ゆ柇鍥剧墖鐢ㄩ�旓紙鎵剧數褰眔r娲诲姩锛�
	if (useType == 1 || useType == 2) {
		movieDwr.isFilmExist(filmId,{
			callback : function(msg) {
				if (msg == 0) {
					return;
				}
				window.location.href = ctx + "/findFilms.do?filmId=" + filmId;
			}
		});
	} else if (useType == 3) {
		movieDwr.isActivityExist(filmId,{
			callback : function(msg) {
				if (msg == 0) {
					return;
				}
				window.location.href = ctx + "/activityinfo.do?id=" + filmId;
			}
		});
	}
	return;
}