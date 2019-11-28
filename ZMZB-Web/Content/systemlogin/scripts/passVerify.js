var checkPass = function(){
	var newPass1 = $("#newPass1").val();
	var newPass2 = $("#newPass2").val();
	if(newPass1 == "" || newPass1 == null || newPass2 == "" || newPass2 == null){
		alert("新密码不能为空!");
		return false;
	}else if(newPass1 != newPass2){
		alert("您两次输入的密码不一致，请重新输入!");
		return false;
	}
	return true;
}