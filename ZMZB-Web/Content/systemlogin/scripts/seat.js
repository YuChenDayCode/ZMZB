ECSC.loadDwr("movieDwr");
$(document).ready(function() {getImages(1);});
function getImages(ix){
	$("#imagesIndex").attr("src", ctx + "/imageCode?" + Math.random());
}
var Seatarr = new Array();
var MaxRow = new Array();
var MaxCol = new Array();
var Choicearr = new Array();
var MaxPieceNo = 0;
var MaxChoice = 4;
var currPieceNo=-1;

for (i = 0; i < MaxChoice; i++) {
	Choicearr[i] = new Seat("", "", 0, 0, 0);
}

function switchqy(i) {
	currPieceNo = (i);
	addzw();
}

function Seat(SeatNo, SeatPieceNo, SeatRow, SeatCol, GraphRow,GraphCol,SeatState) {
	this.SeatNo = SeatNo;
	this.SeatPieceNo = SeatPieceNo;
	this.SeatRow = SeatRow;
	this.SeatCol = SeatCol;
	this.SeatState = SeatState;
	this.GraphRow = GraphRow;
	this.GraphCol = GraphCol;
	
	if (SeatPieceNo > MaxPieceNo) {
		MaxPieceNo = SeatPieceNo;
	}
	if (typeof (MaxRow[SeatPieceNo]) == 'undefined') {
		MaxRow[SeatPieceNo] = 0;
	}
	if (typeof (MaxCol[SeatPieceNo]) == 'undefined') {
		MaxCol[SeatPieceNo] = new Array();
	}
	if (typeof (MaxCol[SeatPieceNo][GraphRow]) == 'undefined') {
		MaxCol[SeatPieceNo][GraphRow] = 0;
	}
	if (GraphRow > MaxRow[SeatPieceNo]) {
		MaxRow[SeatPieceNo] = GraphRow;
	}
	if (GraphCol > MaxCol[SeatPieceNo][GraphRow]) {
		MaxCol[SeatPieceNo][GraphRow] = GraphCol;
	}
}

function TestChoiced(SeatNo) {
	var ret = false;
	for (ii = 0; ii < MaxChoice; ii++) {
		if (Choicearr[ii].SeatNo == SeatNo) {
			ret = true;
			break;
		}
	}
	return ret;
}

function getTicketInfo(type) {
	TotalQuantity = 0;
	var s = "";
	for (ii = 0; ii < MaxChoice; ii++) {
		if (Choicearr[ii].SeatNo != "") {
			TotalQuantity++;
			if (type == 1) {
				s+="【"+hallName+Choicearr[ii].SeatPieceNo+"区"+Choicearr[ii].SeatRow+"排"+Choicearr[ii].SeatCol+"座 】";
			} else {
				s+=Choicearr[ii].SeatNo+",";
			}
		}
	}
	return s;
}

function refshowxz() {
	var s=getTicketInfo(1);
	$("#epayz").html(TotalQuantity);
	$("#epay").html(TotalQuantity*SinglePrice);
	$("#bankz").html(TotalQuantity);
	$("#bank").html(TotalQuantity*SinglePriceInt);
	$("#showxz").html("您已选择：<span>"+s+"</span>");
}

function AddSeatby(di) {
	var ret = false;
	for (ii = 0; ii < MaxChoice; ii++) {
		if (Choicearr[ii].SeatNo == "") {
			Choicearr[ii].SeatNo = Seatarr[di].SeatNo;
			Choicearr[ii].SeatPieceNo = Seatarr[di].SeatPieceNo;
			Choicearr[ii].SeatRow = Seatarr[di].SeatRow;
			Choicearr[ii].SeatCol = Seatarr[di].SeatCol;
			ret = true;
			break;
		}
	}
	refshowxz();
	return ret;
}

function RemoveSeatbyNo(SeatNo) {
	for (ii = 0; ii < MaxChoice; ii++) {
		if (Choicearr[ii].SeatNo == SeatNo) {
			Choicearr[ii].SeatNo = "";
			Choicearr[ii].SeatPieceNo = "";
			Choicearr[ii].SeatRow = 0;
			Choicearr[ii].SeatCol = 0;
			break;
		}
	}
	refshowxz();
}

function TestSeat(ai) {
	var ret = false;
	if (Seatarr[ai].SeatState == "0") {
		if (TestChoiced(Seatarr[ai].SeatNo)) {
			if (confirm('确定要取消这张票么?')){
				RemoveSeatbyNo(Seatarr[ai].SeatNo);
				bjseat(ai);
			}
		} else {
			ret = true;
		}
	} else {
		alert("这个位置已不能选择,请选其它的位置！");
	}
	return ret;
}

function choice(i) {
	if (i < 0) {
		alert("这个位置已不能选择,请选其它的位置！");
	} else {
		if (TestSeat(i)){
			if (AddSeatby(i)) {
				bjseat(i);
			} else {
				alert("已达到本次选票数量上限.");
			}
		}
	}
}

function bjseat(i){
	if (TestChoiced(Seatarr[i].SeatNo)) {
		$("#r"+Seatarr[i].SeatRow+"l"+Seatarr[i].SeatCol).html("<a href=\"javascript:choice("+i+");void(0);\" title =\""+Seatarr[i].SeatRow+"排"+Seatarr[i].SeatCol+"坐\"><img src=\"images/zw_4.png\"></img></a>");
	} else {
		$("#r"+Seatarr[i].SeatRow+"l"+Seatarr[i].SeatCol).html("<a href=\"javascript:choice("+i+");void(0);\" title =\""+Seatarr[i].SeatRow+"排"+Seatarr[i].SeatCol+"坐\"><img src=\"images/zw_1.png\"></img></a>");
	}
}
function selectzw(i,ids){
 
	
	if($("#selectzws").html().indexOf(ids)>=0){
		$("#selectzws span").each(function(){
			
			if($(this).attr("id")==ids){
				if (confirm('确定要取消这张票么?')){
					$(i).attr("src","images/1_03.png");
					$(this).remove();
					zhfktext(fkfs);
				}
			}
		});
	}else{
		if($("#selectzws span").length<8){
			
			$(i).attr("src","images/4_03.png");
			$("#selectzws").append("<span id=\""+ids+"\">【<span class=\"chengse bold\">"+$(i).attr("alt")+"</span>】</span>");
			zhfktext(fkfs);
		}else{
			alert('亲，一次只能购买四张票哟~');
		}
	}
}

function zhfktext(type){
	fkfs=type;
	if(type==1){
		$("#wangyingzhifu").attr("class","f-14 bold floatleft words width90 textmid bgqh line30");
		$("#jifenduihuan").attr("class","f-14 bold floatleft words width90 textmid line30");
		$("#zhanghuyue").attr("class","f-14 bold floatleft words width90 textmid line30");
		$("#fktext").html("<span>  应付总款："+($("#selectzws span").length/2)*SinglePrice+"元 （共"+$("#selectzws span").length/2+"张票）</span>");
	}
	else if(type==2){
		$("#wangyingzhifu").attr("class","f-14 bold floatleft words width90 textmid line30");
		$("#jifenduihuan").attr("class","f-14 bold floatleft words width90 textmid bgqh line30");
		$("#zhanghuyue").attr("class","f-14 bold floatleft words width90 textmid line30");
		$("#fktext").html("<span> 应付总积分："+($("#selectzws span").length/2)*SinglePriceInt+"分 （共"+$("#selectzws span").length/2+"张票）</span><span class=\"chengse\"> 注：用积分支付，积分=窗口价格*10*张数</span>");
	}
	else if(type==3){
		$("#wangyingzhifu").attr("class","f-14 bold floatleft words width90 textmid line30");
		$("#jifenduihuan").attr("class","f-14 bold floatleft words width90 textmid line30");
		$("#zhanghuyue").attr("class","f-14 bold floatleft words width90 textmid bgqh line30");
		$("#fktext").html("<span>  应付总款："+($("#selectzws span").length/2)*SinglePrice+"元 （共"+$("#selectzws span").length/2+"张票）</span>");
	}
}
//座位选择
function addzw(qhao){
	
	$("#selectzws span").remove();
	var temp='';
	var ptemp='';
	var intptemp;
	var imgsps=$("#zwlists .imgsps");
	var testps='';
	var imgs='';
	var pqs='';
	var maxwidth=0;
	$(Seatarr).each(function(k,v){
		if(v.SeatPieceNo==qhao){
			intptemp=v.GraphRow;
			var tempindex=0;
			var indeximg=0;
			if(ptemp.indexOf(v.GraphRow)<0){
				$(Seatarr).each(function(kk,v1){
					if(kk==0){
						imgs+="<div class=zwsh>";
					}
					if(v1.SeatPieceNo==qhao){
						if(intptemp==v1.GraphRow){
							///SeatNo, SeatPieceNo, SeatRow, SeatCol, GraphRow,GraphCol,SeatState
							var ifnull=function(){
								tempindex++;
								if(tempindex==v1.GraphCol){
									zjhtml();
								}else{
									indeximg++;
									if(indeximg==1){
										testps+="<span>"+v.SeatRow+"</span><div style='clear:both'></div>";
									}
									imgs+="<a class=null></a>";
									ifnull();
								}
							}
							var zjhtml=function(){
								indeximg++;
								if(indeximg*26>maxwidth){
									maxwidth=indeximg*26;
								}
								if(indeximg==1){
									testps+="<span>"+v.SeatRow+"</span><div style='clear:both'></div>";
								}
								if(v1.SeatState==0){
									imgs+="<img onclick=\"selectzw(this,'"+v1.SeatNo+"')\" alt='"+hallName+v.SeatPieceNo+"区"+v1.SeatRow+"排"+v1.SeatCol+"座' title='"+hallName+v.SeatPieceNo+"区"+v1.SeatRow+"排"+v1.SeatCol+"座' src=\"images/1_03.png\">";
								}else if(v1.SeatState==-1){
									imgs+="<img alt='不可售'   src=\"images/2_03.png\">";
								}else if(v1.SeatState==1){
									imgs+="<img alt='已售'   src=\"images/3_03.png\">";
								}else if(v1.SeatState==7){
									imgs+="<img alt='锁定'   src=\"images/2_03.png\">";
								}
							}
							ifnull();
						}
					}
					if(kk==Seatarr.length-1){
						imgs+="</div>";
					}
				});
				ptemp+=v.GraphRow+",";
			}
		}
	
		
		if(pqs.indexOf(v.SeatPieceNo)<0){
			pqs+=v.SeatPieceNo+",";
		}
	});
	$("#zwlists .textps").html(testps);
	imgsps.html(imgs);
	$("#zwlists .imgsps .zwsh").css("width",((maxwidth-1)+50));
	temp+="请选择座区:";
	$(pqs.split(",")).each(function(k,v){
		if(v!=''){
			temp+="<span style='cursor: pointer;' onclick=\"addzw('"+v+"')\">["+v+"]</span>";
		
		}
	});
	$("#quhao").html(temp);
	
	/*for (r = MaxRow[currPieceNo]; r >=1; r--) {
		if (r < 10) {
			temp = "0" + r;
		} else {
			temp = r;
		}
		$("#zwlist").append("<li>" + temp + "排</li>");
		for (l = 1; l <=MaxCol[currPieceNo][r]; l++) {
			if (l % 20 == 0) {
				$("#zwlist").append("<li><div style=\"width:30px\">&nbsp;</div></li>");
			}
			$("#zwlist").append("<li id=\"r"+r+"l"+l+"\"><img src=\"images/zw_0.gif\"></li>");
		}
		$("#zwlist").append("<div style='clear:both'></div>");
	}
	
	for (i = 0; i < Seatarr.length; i++) {
		if (Seatarr[i].SeatPieceNo == currPieceNo){
			if (Seatarr[i].SeatState == "0") {
				bjseat(i);
			} else if (Seatarr[i].SeatState == "1"){
				$("#r"+Seatarr[i].SeatRow+"l"+Seatarr[i].SeatCol).html("<a href=\"javascript:choice(-1);void(0);\" title =\""+Seatarr[i].SeatRow+"排"+Seatarr[i].SeatCol+"坐\"><img src=\"images/zw_2.png\"></img></a>");
			} else {
				$("#r"+Seatarr[i].SeatRow+"l"+Seatarr[i].SeatCol).html("<img src=\"images/zw_3.png\" title =\""+Seatarr[i].SeatRow+"排"+Seatarr[i].SeatCol+"坐\"></img>");
			}
		}
	}*/
}
//验证手机
function checkphone(){
	var recivePhone = $("#recivePhone").val();
	if (recivePhone == "") {
		$("#duic").attr("src","styles/images/cuo.jpg");
	}else if(recivePhone.length!=11){
		$("#duic").attr("src","styles/images/cuo.jpg");
	}
	else if(recivePhone>1)
	{
		$("#duic").attr("src","styles/images/dui.jpg");
	}else{
		
		$("#duic").attr("src","styles/images/cuo.jpg");
	}
}

//支付
var tempkey=0;
function buyIt() {
	tempkey=1;
	if ($("#selectzws span").length/2<1){
		alert("请先选位置.");	tempkey=0;
	}else{
		var recivePhone = $("#recivePhone").val();
		if (recivePhone == "") {
			$("#duic").attr("src","styles/images/cuo.jpg");
			tempkey=0;
		}else if(recivePhone.length!=11){
			$("#duic").attr("src","styles/images/cuo.jpg");tempkey=0;
		}
		else if(recivePhone>1)
		{	$("#duic").attr("src","styles/images/dui.jpg");
			var imageCode = $("#validateCode").val();
			if (imageCode == "") {
				alert("请填写验证码.");tempkey=0;
			}else{
				var pids='';
				$("#selectzws span").each(function(){
					var tempids=$(this).attr("id");
					if(tempids!=null&&tempids.length>1){
						pids+=tempids+',';
					}
				});
				movieDwr.createOrderTicket(imageCode,fkfs,pqid,recivePhone,pids,{
					callback:function(result) {
							if(result.result == '1'){
								ECSC.alert(result.desc,"提示");tempkey=0;
								return;
							}else if(result.result == '2'){
								ECSC.alert(result.desc,"提示",function (){tempkey=0;});
								getImages();
								return;
							}else if(result.result == '0'){
								ECSC.alert(result.desc,"提示",function (){window.location.href=ctx+'/findOrder.do';});tempkey=0;
								return;
							}else if(result.result == '4'){
								ECSC.alert(result.desc,"提示",function (){window.location.reload();});
								tempkey=0;
								return;
							}else if(result.result == '5'){
								ECSC.alert(result.desc,"提示",function (){window.location.reload();});
								tempkey=0;
								return;
							}else if(result.result == '3'){
								$("#insertForm").html("");
									$("#insertForm").append("<form target='_blank' action='http://222.177.210.171:8181/mto-pay/servlet/bestpay/post' id ='paysubmit' method='post'><input name='orderId' id='orderId' value='"+result.orderId+"'/><input name='key' id='key' value='"+result.key+"'/><input name='time' id='time' value='"+result.time+"'/></form>");
									$("#paysubmit").submit();
								tempkey=0;
							}
							
						},
					 	async : false
				});
			}
		}else{$("#duic").attr("src","styles/images/cuo.jpg");}
	}
	
//	if (type==1){
//	alert("支付共"+TotalQuantity+"张需要钱:"+(TotalQuantity*SinglePrice)+"元  "+getTicketInfo(1));	
//	}else{
//	alert("兑换共"+TotalQuantity+"张需要积分:"+(TotalQuantity*SinglePriceInt)+"分"+getTicketInfo(1));
//	}
}