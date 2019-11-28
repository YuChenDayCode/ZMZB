lastScrollY=0; 
function  heartBeat(){ 
var diffY; 
if (document.documentElement && document.documentElement.scrollTop) 
diffY = document.documentElement.scrollTop; 
else if (document.body) 
diffY = document.body.scrollTop 
else 
    {/*Netscape   stuff*/} 

//alert(diffY); 
percent=.1*(diffY-lastScrollY); 
if(percent>0)percent=Math.ceil(percent); 
else percent=Math.floor(percent); 
document.getElementById("leftDIV").style.top=parseInt(document.getElementById("leftDIV").style.top)+percent+"px";
document.getElementById("rightDIV").style.top=parseInt(document.getElementById("leftDIV").style.top)+percent+"px";

lastScrollY=lastScrollY+percent;
//alert(lastScrollY);
}

sidebar1= "<div id=leftDIV style='left:0px; PosITION:absolute; TOP:100px;'>右侧广告</div>" 
sidebar2= "<div id=rightDIV style='right:0px; PosITION:absolute; TOP:100px;'>左侧广告</div> " 

document.write(sidebar1); 
document.write(sidebar2); 

//�������ɾ��󣬶�������������Ļ���ƶ���
window.setInterval("heartBeat()",1); 
//-->

function closeAD() 
{ 
document.getElementById("leftDIV").style.display='none'; 
document.getElementById("rightDIV").style.display='none'; 
}