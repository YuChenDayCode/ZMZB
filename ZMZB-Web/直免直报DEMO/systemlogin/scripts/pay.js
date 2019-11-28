ECSC.loadDwr("movieDwr");
function getImage(){
	$("#imagesIndex").attr("src", "imageCode?" + Math.random());
}
function showinfo(y,e){
	/*var viplevel = $("input[name='viplevel']:checked").val();
	var chargeMonth = $("input[name='chargeMonth']:checked").val();
	if(chargeMonth == "other"){
		if($("#countMonth").val() == ""){
			alert("请输入充值期限！");
			return ;
		}
		chargeMonth = $("#countMonth").val();
	}
	if(chargeMonth >0){
		var s="0";
		if (viplevel == 2) {
			s = chargeMonth * white;
			$("#czinfo").html("（<img src=\"images/white.png\" align=\"absmiddle\"/>&nbsp;白钻"+chargeMonth+"个月）");
		} else if (viplevel == 3) {
			s = chargeMonth * yellow;
			$("#czinfo").html("（<img src=\"images/yellow.png\" align=\"absmiddle\"/>&nbsp;黄钻"+chargeMonth+"个月）");
		} else if (viplevel == 4) {
			s = chargeMonth * red;
			$("#czinfo").html("（<img src=\"images/red.png\" align=\"absmiddle\"/>&nbsp;红钻"+chargeMonth+"个月）");
		}
		$("#total").html(s);
	}*/
	chargeMonth=$(e).val();
	$("#yuan").text(y);
}

function completePay(fkfs) {
	var serviceType;
	
	serviceType = fkfs;
	//var charge = $("#yuan").text();
	var viplevel = $("input[name='viplevel']:checked").val();
	var imageCode = $("#validateCode").val();
	if(imageCode.length<1){
		alert("请输入验证码!");
	}else{
		
		if(serviceType == 1){
			
			movieDwr.createOrder(viplevel,chargeMonth,imageCode,serviceType,{
			callback:function(result) {
					if(result.result == '1'){
						alert(result.desc);
						getImage();
						window.location.href = "refreshBalance.do";
						return;
					}else if(result.result == 'flagForLoginOut'){
						alert(result.desc);
						getImage();
						return;
					}else if(result.result == '0'){
						alert(result.desc);
						getImage();
						return;
					}
					$("#insertForm").html("");
					$("#insertForm").append("<form target='_blank' action='http://www.haoduopiao.com/mto-pay/servlet/bestpay/post' id ='paysubmit' method='post'><input name='orderId' id='orderId' value='"+result.orderId+"'/><input name='key' id='key' value='"+result.key+"'/><input name='time' id='time' value='"+result.time+"'/></form>");
					$("#paysubmit").submit();
				},
				async : false
			});
		}
		
		
		if(serviceType == 3){
			
			movieDwr.payForJewel(imageCode,chargeMonth,{
				callback:function(result) {
						alert(result.desc);
						getImage();
						window.location.href = "refreshBalance.do";
						return;
				},
				async : false
			});
		}
		
		
		
		
		
		
		/*movieDwr.createOrder(viplevel,chargeMonth,imageCode,serviceType,{
			callback:function(result) {
					if(result.result == '1'){
						alert(result.desc);
						getImage();
						return;
					}else if(result.result == 'flagForLoginOut'){
						alert(result.desc);
						getImage();
						return;
					}else if(result.result == '0'){
						alert(result.desc);
						getImage();
						return;
					}
					$("#insertForm").html("");
					$("#insertForm").append("<form target='_blank' action='http://www.haoduopiao.com/mto-pay/servlet/bestpay/post' id ='paysubmit' method='post'><input name='orderId' id='orderId' value='"+result.orderId+"'/><input name='key' id='key' value='"+result.key+"'/><input name='time' id='time' value='"+result.time+"'/></form>");
					$("#paysubmit").submit();
				},
				async : false
		});*/
	}
}