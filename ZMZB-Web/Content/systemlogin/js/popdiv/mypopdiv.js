(function($) {
	$.fn.popdiv = function(targetId,customsetting) {
		var _seft = this;
		var targetId = $(targetId);
		targetId.hide();
		var setting = {
				background:"#FFF",
				position:"absolute",
				border:"1px solid #000"
			};
		if (customsetting) {
			$.extend(setting, customsetting);
		}
		this.click(function(){
			var A_top = $(this).offset().top + $(this).outerHeight(true);  //  1
			var A_left =  $(this).offset().left;
			targetId.bgiframe();
			if(!setting.top)
			{
				setting["top"] = A_top+"px";
			}
			if(!setting.left)
			{
				setting["left"] = A_left+"px";
			}
			targetId.css(setting);
			targetId.show();
		});
		
		$(document).click(function(event){
			if(event.target.id!=_seft.selector.substring(1)){
				targetId.hide();	
			}
		});
		
		targetId.click(function(e){
			e.stopPropagation(); //  2
		});
		return this;
	}
})(jQuery);