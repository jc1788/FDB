第一步驟

var app = new Vue({
    el: '#app01',
    data:{
        value_1: 52,
        value_2: 123,
        value_3: 779,
    }
  });



// var app = new Vue({
    //   el: '#app01',
    //   data   
    // })
    // window.setInterval(() => {

    //   data.value_1 = Math.floor(Math.random()*50)
    // }, 100)

    // (data)=>"aaa"

    // function(data){
    //   return "aaa"
    // }

    //jq非同步，箭頭函式
    //找到index.json這個檔案，取回來做{}內的事情
    $.get('index.json').done((d) => {
        var app = new Vue({
          el: '#app01',
          data: d
        });
        new Vue({
          el: '#app02',
          data: d
        });
  
        // console.log("bbb")
      })
      // window.setInterval(()=>{console.log("ee")},10)