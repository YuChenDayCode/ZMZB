ECSC.loadDwr("movieDwr");
$(document).ready(function() {getImages(1);});
function getImages(){
	$("#imagesIndex").attr("src", ctx + "/imageCode?" + Math.random());
}

var TotalQuantity = 0;
var SinglePrice = 0;
var PriceName = "";
var SinglePriceInt = 0;

function setBasePrice(Price, PriceInt, PrName) {
	SinglePrice = Price;
	SinglePriceInt = PriceInt;
	PriceName = PrName;
	refshowxz();
}

function refshowxz() {
	if (SinglePrice == 0 || SinglePriceInt == 0) {
		alert("请选择需要购买的电子券类型");
	} else {
		TotalQuantity=$("#zhang").val();
		if(TotalQuantity.search(/^\d+$/)!=0) {
			alert("购买张数只能是整数.");
		} else if (TotalQuantity > 10) {
			alert("一次不能购买太多.");
		} else {
			if(zftype==1){
				$("#zhanghuye").attr("class","f-14 bold floatleft words width90 textmid line30");
				$("#wangyzf").attr("class","f-14 bold floatleft words width90 textmid bgqh line30");
				$("#jifendh").attr("class","f-14 bold floatleft words width90 textmid line30");
				$("#zftext").html("应付总款："+TotalQuantity*SinglePrice+"元(共"+TotalQuantity+"张票)");
			}else if(zftype==2){
				$("#zhanghuye").attr("class","f-14 bold floatleft words width90 textmid line30");
				$("#wangyzf").attr("class","f-14 bold floatleft words width90 textmid line30");
				$("#jifendh").attr("class","f-14 bold floatleft words width90 textmid bgqh line30");
				$("#zftext").html("应付总款："+TotalQuantity*SinglePriceInt+"分(共"+TotalQuantity+"张票)<span class=\"chengse\"> 注：用积分支付，积分=窗口价格*10*张数</span>");
			}else if(zftype==3){
				$("#zhanghuye").attr("class","f-14 bold floatleft words width90 textmid bgqh line30");
				$("#wangyzf").attr("class","f-14 bold floatleft words width90 textmid line30");
				$("#jifendh").attr("class","f-14 bold floatleft words width90 textmid line30");
				$("#zftext").html("应付总款："+TotalQuantity*SinglePrice+"元(共"+TotalQuantity+"张票)");
			}
		/*	$("#epayz").html(TotalQuantity);
			$("#epay").html();
			$("#bankz").html(TotalQuantity);
			$("#bank").html(TotalQuantity*SinglePriceInt);
			$("#dzjf").html(SinglePriceInt);
			$("#xzlxname").html("【" + PriceName + "】");*/
		}
	}
}
function setzfff(type,a){
	zftype=type;
	if(a!=false){
	refshowxz();
	}
}
		
function buyIt() {
	if (PriceName == "") {
		alert("请选择你要购买的电子券类型!");
	}else if (TotalQuantity < 1) {
		alert("请输入你需要购买的张数!");	
	}else{
		var recivePhone = $("#recivePhone").val();
		if (recivePhone == "") {
			alert("请输入接收电影票的手机号码.");
			$('#pimg').attr('src','styles/images/cuo.jpg');
		}else if(recivePhone.length!=11){
			alert("请输入接收正确的[11位]手机号码.");
			$('#pimg').attr('src','styles/images/cuo.jpg');
		}else{
			$('#pimg').attr('src','styles/images/dui.jpg');
			var imageCode = $("#validateCode").val();
			if (imageCode == "") {
				alert("请填写验证码.");
			}else{
				movieDwr.createOrderEticket(imageCode,zftype,cinemaID,recivePhone,PriceName,TotalQuantity,{
					callback:function(result) {
							if(result.result == '1'){
								if(memberType==2){
									ECSC.alert("<div style='text-align:center'>"+result.desc+"</div>","提示",function (){window.location.href=ctx+'/findOrder.do';});
								}else{
									ECSC.alert("<div style='text-align:center'>"+result.desc+"</div>","提示");
								}
								return;
							}else if(result.result == '2'){
								ECSC.alert("<div style='text-align:center'>"+result.desc+"</div>","提示");
								getImages();
								return;
							}else if(result.result == '0'){
								ECSC.alert("<div style='text-align:center'>"+result.desc+"</div>","提示",function (){window.location.href=ctx+'/findOrder.do';});
								return;
							}else if(result.result == '3'){
								$("#insertForm").html("");
								$("#insertForm").append("<form target='_blank' action='http://222.177.210.171:8181/mto-pay/servlet/bestpay/post' id ='paysubmit' method='post'><input name='orderId' id='orderId' value='"+result.orderId+"'/><input name='key' id='key' value='"+result.key+"'/><input name='time' id='time' value='"+result.time+"'/></form>");
								$("#paysubmit").submit();
							}
						},
					 	async : false
				});
			}
		}
	}
}