ECSC.loadDwr("movieDwr");
$(document).ready(function() {
	var start = $("#start").val();
	var end = $("#end").val();
	if (start == "" && end == "") {
		defaultDate();
	}
	var orderTp = $("#orderT").val();
	$("[name='orderType']").val(orderTp);
	if (orderTp == 1) {
		$("#showHeader").html("<th width='138'>订单号</th><th width='80'>手机号码</th><th width='70'>订购日期</th><th width='50'>支付状态</th><th width='80'>影院</th><th width='80'>订单详情</th>");
		$("#ticketOrder").attr("style","display:block;");
		$("#payOrder").attr("style","display:none;");
		$("#dzjOrder").attr("style","display:none;");
		return;
	} else if (orderTp == 2) {
		$("#showHeader").html("<th width='120'>充值订单号</th><th width='100'>会员账户</th><th width='50'>充值金额</th><th width='100'>充值时间</th><th width='50'>支付状态</th><th width='100'>充值赠送积分</th><th width='50'>充值等级</th><th>充值月数</th>");
		$("#ticketOrder").attr("style","display:none;");
		$("#payOrder").attr("style","display:block;");
		$("#dzjOrder").attr("style","display:none;");
		return;
	}
	$("#showHeader").html("<th width='138'>订单号</th><th width='80'>手机号码</th><th width='70'>订购日期</th><th width='50'>支付状态</th><th width='80'>影院</th><th width='80'>订单详情</th>");
	$("#dzjOrder").attr("style","display:block;");
	$("#payOrder").attr("style","display:none;");
	$("#ticketOrder").attr("style","display:none;");
});

var defaultDate = function() {
	var d, s, m, n;
	d = new Date();
	s = d.getUTCFullYear() + "-";
	m = (d.getMonth() + 1);
	n = d.getDate();
	if (m < 10) {
		s += "0" + m + "-";
	} else {
		s += m + "-";
	}
	if (n < 10) {
		s += "0" + n;
	} else {
		s += n;
	}
	$("#start").val(s);
	$("#end").val(s);
}

var checkOrder = function() {
	var startTime = $("#start").val();
	var endTime = $("#end").val();
	var orderNum = $("#orderNum").val();
	if (orderNum == null || orderNum == "") {
		if (startTime == null || startTime == "" || endTime == null || endTime == "") {
			alert("请完整输入查询条件(可凭订单类型和订单号查询)");
			return false;
		}
		return true;
	}
	return true;
}
var changeHeader = function() {
	var orderType = $("[name='orderType']").val();
	if (orderType == 1) {
		$("#showHeader").html("<th width='138'>受理单号</th><th width='80'>手机号码</th><th width='70'>订购日期</th><th width='50'>支付状态</th><th width='80'>影院</th><th width='80'>订单详情</th>");
		$("#ticketOrder").attr("style","display:block;");
		$("#payOrder").attr("style","display:none;");
		$("#dzjOrder").attr("style","display:none;");
		return;
	} else if (orderType == 2) {
		$("#showHeader").html("<th width='120'>充值订单号</th><th width='100'>会员账户</th><th width='50'>充值金额</th><th width='100'>充值时间</th><th width='50'>支付状态</th><th width='100'>充值赠送积分</th><th width='50'>充值等级</th><th>充值月数</th>");
		$("#ticketOrder").attr("style","display:none;");
		$("#payOrder").attr("style","display:block;");
		$("#dzjOrder").attr("style","display:none;");
		return;
	}
	$("#showHeader").html("<th width='138'>受理单号</th><th width='80'>手机号码</th><th width='70'>订购日期</th><th width='50'>支付状态</th><th width='80'>影院</th><th width='80'>订单详情</th>");
	$("#dzjOrder").attr("style","display:block;");
	$("#payOrder").attr("style","display:none;");
	$("#ticketOrder").attr("style","display:none;");
}

var getImage = function() {
	$("#imagesIndex").attr("src",ctx + "/imageCode?" + Math.random());
}

var repay = function(el) {
	var orderId = $(el).attr("id");
	var imageCode = '';
	movieDwr.resendOrder(orderId,imageCode,{
		callback : function(result) {
			if (result.result == '2') {
				alert(result.desc);
				return;
			} else {
				ECSC.showWindow("验证码：<input id='validateCode' name='validateCode' type='text' style='width: 57px;vertical-align: bottom;' /><img style='vertical-align: bottom;cursor: pointer;' id='imagesIndex' src='" + ctx + "/imageCode?" + Math.random() + "' onclick='getImage();'  width='70' height='25' title='换一张'><label onclick ='getImage();'>[换一张]</label>","重新支付",{
					buttons : [{
						label : "确定",
						fn : function() {
							imageCode = $("#validateCode").val();
							movieDwr.resendOrder(orderId,imageCode,{
								callback : function(result) {
									if (result.result == '1') {
										alert(result.desc);
										return;
									}
									// 待验证：验证待付费的订单组是否超过设定时间(目前设定为10分钟)，验证通过调支付接口
									$("#insertForm").html("");
									$("#insertForm").append("<form target='_blank' action='http://www.haoduopiao.com/mto-pay/servlet/bestpay/post' id ='paysubmit' method='post'><input name='orderId' id='orderId' value='" + result.orderId + "'/><input name='key' id='key' value='" + result.key + "'/><input name='time' id='time' value='" + result.time + "'/></form>");
									$("#paysubmit").submit();
								},
								async : false
							});
							this.close();
						}
					}],
					width : 300,
					height : 60,
					closeable : false
				});

			}
		},
		async : false
	});

}