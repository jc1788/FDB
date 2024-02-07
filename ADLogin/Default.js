var mySso = "No";
var myCnt = 10;

window.setTimeout(chkSsoData, 1000)

function chkSsoData() {
    var oTdAgrUMsg = document.getElementById("TdAgrUMsg");

    if (oTdAgrUMsg != null) {
        oTdAgrUMsg.innerHTML = "帳號偵測中，請稍後 " + myCnt.toString() + " 秒";

        if (myCnt > 0) {
            window.setTimeout(chkSsoData, 1000);
        }
    }

    myCnt -= 1;

    if (mySso == "No" && myCnt < 0) {
        var oTableLogin = document.getElementById("TableLogin");
        var oTrAuto = document.getElementById("TrAuto");

        oTableLogin["style"]["display"] = "";
        oTableLogin["style"]["visibility"] = "";
        oTrAuto["style"]["display"] = "none";
        oTrAuto["style"]["visibility"] = "hidden";

        //alert("無法偵測本機登入帳號資料");
        oTdAgrUMsg.innerHTML = "無法偵測到帳號，請自行登入";
    }
}

function setSsoType(sType) {
    mySso = sType;
}