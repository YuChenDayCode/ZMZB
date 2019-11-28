ECSC.loadDwr("movieDwr");
var resendCode = function(el){
	if($(el).css("color")!='#ccc'){
		$(el).css("color","#ccc");
		window.setTimeout(function(){
			$(el).css("color","#134A80");
		},5000);
		var orderId = $(el).attr("id");
		if(orderId != ""){
			movieDwr.resendCode(orderId,{
				callback:function(msg){
					alert(msg);
				},
				async : false
			});
		}else{
			alert("对不起，该订单不存在");
		}
	
	}
	
}