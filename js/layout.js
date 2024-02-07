$(function(){

  $("#MENU1").hover(function(){
  	$("#SUB1").stop(true , true).slideDown(200)}, function(){$("#SUB1").stop(true,true).slideUp(200)
  });

  $("#MENU2").hover(function(){
  	$("#SUB2").stop(true , true).slideDown(200)}, function(){$("#SUB2").stop(true,true).slideUp(200)
  });

  $("#MENU3").hover(function(){
  	$("#SUB3").stop(true , true).slideDown(200)}, function(){$("#SUB3").stop(true,true).slideUp(200)
  });

  $("#MENU4").hover(function(){
  	$("#SUB4").stop(true , true).slideDown(200)}, function(){$("#SUB4").stop(true,true).slideUp(200)
  });

  $("#MENU5").hover(function(){
  	$("#SUB5").stop(true , true).slideDown(200)}, function(){$("#SUB5").stop(true,true).slideUp(200)
  });
  $("#MENU6").hover(function(){
    $("#SUB6").stop(true , true).slideDown(200)}, function(){$("#SUB6").stop(true,true).slideUp(200)
  });

})



$(function(){
  
  $(".IfThis_Left ul li").mouseover( function(){
    
    var NN = $(this).index();
    
    $(".IfThis_right .IfThisContent").eq(NN).fadeIn(800).siblings().fadeOut(800);
  
  });
  
});