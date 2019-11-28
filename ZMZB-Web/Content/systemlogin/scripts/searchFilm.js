ECSC.loadDwr("movieDwr");
var searchFilmByName = function() {
	var filmName = $("#filmName").val();
	var sheng = $("#sheng").val();
	var shi = $("#shi").val();
	movieDwr.getFilmsByName(filmName,sheng,shi,function(msg) {
		$("#show-ing").html("");
		for (var i = 0; i < msg.length; i++) {
			$("#show-ing").append("<li><a href='" + ctx + "/findFilms.do?filmId=" + msg[i].FILM_ID + "&provinceId=" + msg[i].PROVINCE_ID + "&cityId=" + msg[i].CITY_ID + "'>" + msg[i].FILM_NAME + "</a></li>");
		}
	});
}

var changeLabel = function(flag) {
	if (flag == 1) {
		$("#label1").attr("style","background:#cc0000; color:#FFFF66;")
		$("#d1").attr("style","display:block;");
		$("#dateFlag").val(1);
		$("#label2").attr("style","background: none repeat scroll 0 0 #EEEEEE;");
		$("#d2").attr("style","display:none;")
		$("#label3").attr("style","background: none repeat scroll 0 0 #EEEEEE;");
		$("#d3").attr("style","display:none;")
	}else if(flag == 3){
		$("#label3").attr("style","background:#cc0000; color:#FFFF66;")
		$("#d3").attr("style","display:block;");
		$("#dateFlag").val(0);
		$("#label2").attr("style","background: none repeat scroll 0 0 #EEEEEE;");
		$("#d2").attr("style","display:none;")
		$("#label1").attr("style","background: none repeat scroll 0 0 #EEEEEE;");
		$("#d1").attr("style","display:none;")
	} else {
		$("#label2").attr("style","background:#cc0000; color:#FFFF66;")
		$("#d2").attr("style","display:block;");
		$("#dateFlag").val(2);
		$("#label1").attr("style","background: none repeat scroll 0 0 #EEEEEE;");
		$("#d1").attr("style","display:none;")
		$("#label3").attr("style","background: none repeat scroll 0 0 #EEEEEE;");
		$("#d3").attr("style","display:none;")
	}
}

var condition = function() {
	var sellType = $("#sellType").val();
	var sectionId = $("#sectionId").val();
	var provinceId = $("#provinceId").val();
	var cityId = $("#cityId").val();
	var filmId = $("#filmId").val();
	var dateFlag = $("#dateFlag").val();
	var scheduleDateToday = $("#scheduleDate1").val();
	var scheduleDateTomorrow = $("#scheduleDate2").val();

	$("#zuowei").attr("href",ctx + "/findFilms.do?provinceId=" + provinceId + "&cityId=" + cityId + "&filmId=" + filmId + "&sellType=1" + "&sectionId=" + sectionId + "&dateFlag=" + dateFlag + "&scheduleDateToday=" + scheduleDateToday + "&scheduleDateTomorrow=" + scheduleDateTomorrow+"#zuowei");
//	$("#allArea").attr("href",ctx + "/findFilms.do?provinceId=" + provinceId + "&cityId=" + cityId + "&filmId=" + filmId + "&sellType=" + sellType + "&sectionId=0&dateFlag=" + dateFlag + "&scheduleDateToday=" + scheduleDateToday + "&scheduleDateTomorrow=" + scheduleDateTomorrow+"#zuowei");
	//$("#" + flag).attr("href",ctx + "/findFilms.do?provinceId=" + provinceId + "&cityId=" + cityId + "&filmId=" + filmId + "&sellType=" + sellType + "&sectionId=" + sectionId + "&dateFlag=" + dateFlag + "&scheduleDateToday=" + scheduleDateToday + "&scheduleDateTomorrow=" + scheduleDateTomorrow);
}

var choseArea = function(el) {
	var sellType = $("#sellType").val();
	var sectionId = $(el).attr("id");
	var provinceId = $("#sheng").val();
	var cityId = $("#shi").val();
	var filmId = $("#filmId").val();
	var dateFlag = $("#dateFlag").val();
	$("sectionId").val(sectionId);
	$(el).attr("href",ctx + "/findFilms.do?provinceId=" + provinceId + "&cityId=" + cityId + "&filmId=" + filmId + "&sellType=" + sellType + "&sectionId=" + sectionId + "&flag=" + dateFlag);
}

var hideArea = function(el){
	showhidden('qy',0);
	var area = $(el).html();
	$("#currentArea").html(area);
}