(function($) {
    var isMobile = /Android|webOS|iPhone|iPad|iPod|BlackBerry|IEMobile|Opera Mini/i.test(navigator.userAgent);

    //MODE console hide
    let console = {
        isDev: true,
        log(...args) {
            if (!this.isDev) return
            window.console.log(...args)
        }
    }
   
    
    //lazy load
    $("img").lazyload();

    //menu
    $('.burger').click(function(e){
        $(this).next('.nav').addClass('open')
    })
    $('.nav .close').click(function(){
        $('header .nav').removeClass('open')
    })


    /*************
    load
    **************/
    $(window).on('load', (function () {
    }));
    /*************
    resize
    **************/
    $(window).on('resize', (function () {
        //myEfficientFn();
    }));
    /*************
    scroll
    **************/
    window.onscroll = function () {
        scrollFunction();
        headerScroll();
    }


    var myEfficientFn = debounce(function () {
        // All the taxing stuff you do
        console.log('resize')
    }, 250);

   // initPlayers($('#player-container').length);



    




})($);





/*************
 header
**************/
var headerScroll = debounce(function () {
    if (document.documentElement.scrollTop > 0) {
        $('.header').addClass('active')
    } else {
        $('.header').removeClass('active')
    }
}, 50); 


/************* 
top
**************/
function scrollFunction() {
    if(document.body.scrollTop > 100 || document.documentElement.scrollTop > 20 ){
        $('.toTopBox').fadeIn(300)
    } else {
        $('.toTopBox').fadeOut(300)
    }
}
function topFunction() {
    $('body,html').animate({
        scrollTop: 0          
    }, 500);
}


/**
     * debounce function
**/
function debounce(callback, delay) {
    let inDebounce
    return function () {
        var context = this
        var args = arguments
        clearTimeout(inDebounce)
        inDebounce = setTimeout(() =>
            callback.apply(context, args)
            , delay)
    }
}
/**
     * throttle function that catches and triggers last invocation
**/
function throttle(func, limit) {
    let lastFunc
    let lastRan
    return function () {
        var context = this
        var args = arguments
        if (!lastRan) {
            func.apply(context, args)
            lastRan = Date.now()
        } else {
            clearTimeout(lastFunc)
            lastFunc = setTimeout(function () {
                if ((Date.now() - lastRan) >= limit) {
                    func.apply(context, args)
                    lastRan = Date.now()
                }
            }, limit - (Date.now() - lastRan))
        }
    }
}





/**
* audio
**/
// function calculateTotalValue(length) {
//     var minutes = Math.floor(length / 60),
//         seconds_int = length - minutes * 60,
//         seconds_str = seconds_int.toString(),
//         seconds = seconds_str.substr(0, 2),
//         time = minutes + ':' + seconds

//     return time;
// }

// function calculateCurrentValue(currentTime) {
//     var current_hour = parseInt(currentTime / 3600) % 24,
//         current_minute = parseInt(currentTime / 60) % 60,
//         current_seconds_long = currentTime % 60,
//         current_seconds = current_seconds_long.toFixed(),
//         current_time = (current_minute < 10 ? "0" + current_minute : current_minute) + ":" + (current_seconds < 10 ? "0" + current_seconds : current_seconds);

//     return current_time;
// }

// function initProgressBar() {
//     var player = document.getElementById('player');
//     var length = player.duration
//     var current_time = player.currentTime;
    
//     // calculate total length of value
//     var totalLength = calculateTotalValue(length)
//     $(".end-time").html(totalLength);

//     // calculate current value time
//     var currentTime = calculateCurrentValue(current_time);
//     $(".start-time").html(currentTime);

//     var progressbar = document.getElementById('seekObj');
//     progressbar.value = (player.currentTime / player.duration);

//     progressbar.addEventListener("click", seek);
//     //dots.addEventListener("click", seek);

//     if (player.currentTime == player.duration) {
//         $('#play-btn').removeClass('pause');
//     }

//     function seek(evt) {
//         var percent = evt.offsetX / this.offsetWidth;
//         player.currentTime = percent * player.duration;
//         progressbar.value = percent / 100;
//     }
// };

// function initPlayers(num) {
//     // pass num in if there are multiple audio players e.g 'player' + i

//     for (var i = 0; i < num; i++) {
//         (function () {

//             // Variables
//             // ----------------------------------------------------------
//             // audio embed object
//             var playerContainer = document.getElementById('player-container'),
//                 player = document.getElementById('player'),
//                 isPlaying = false,
//                 playBtn = document.getElementById('play-btn');

//             // Controls Listeners
//             // ----------------------------------------------------------
//             if (playBtn != null) {
//                 playBtn.addEventListener('click', function () {
//                     togglePlay()
//                 });
//             }

//             // Controls & Sounds Methods
//             // ----------------------------------------------------------
//             function togglePlay() {
//                 if (player.paused === false) {
//                     player.pause();
//                     isPlaying = false;
//                     $('#play-btn').removeClass('pause');

//                 } else {
//                     player.play();
//                     $('#play-btn').addClass('pause');
//                     isPlaying = true;
//                 }
//             }
//         }());
//     }
// }