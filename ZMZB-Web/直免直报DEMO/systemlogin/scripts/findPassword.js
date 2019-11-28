ECSC.loadDwr("movieDwr");
var findPassword = function() {
	var mail = $("#mail").val();
	if (mail == "") {
		alert("请输入邮箱号！");
		return;
	}
	movieDwr.isExistForPass(mail,{
		callback : function(flag) {
			if (flag == 0) {
				alert("发送邮箱失败！");
				return;
			}
			alert("亲爱的用户，请查看您的邮箱！");
		},
		async : false
	});

}