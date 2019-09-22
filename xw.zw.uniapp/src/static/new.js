console.log("AAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAABBBBBBBBBBBBB");
console.log(window.location.href);
console.log(document.cookie);
//document.write('<script type="text/javascript" src="//js.cdn.aliyun.dcloud.net.cn/dev/uni-app/uni.webview.0.1.52.js"></script>');
// var newscript = document.createElement('script');
// newscript.setAttribute('type','text/javascript');
// newscript.setAttribute('src','https://js.cdn.aliyun.dcloud.net.cn/dev/uni-app/uni.webview.0.2.2.js');
// var head = document.getElementsByTagName('head')[0];
// head.appendChild(newscript);

function plusReady() {
	console.log(plus.storage.getItem("12333"));
	plus.storage.setItem("12333", 'qqqqqqqqqqqqqqqqqqqqqqqqqqq');

	xhr = new plus.net.XMLHttpRequest();
	xhr.onerror = function(e) {
		var str = "lengthComputable=" + e.lengthComputable + "loaded=" + e.loaded + ";total=" + e.total;
		console.log("onerror: " + str);
	};
	xhr.onreadystatechange = function() {
		switch (xhr.readyState) {
			case 0:
				alert("xhr请求已初始化");
				break;
			case 1:
				alert("xhr请求已打开");
				break;
			case 2:
				alert("xhr请求已发送");
				break;
			case 3:
				alert("xhr请求已响应");
				break;
			case 4:
				if (xhr.status == 200) {
					alert("xhr请求成功：" + xhr.responseText);
				} else {
					alert("xhr请求失败：" + xhr.readyState);
				}
				break;
			default:
				break;
		}
	};
	// xhr.open("GET", 'http://139.155.8.217/api/Member/GetInviteUserPhone?id=1');
	// xhr.send();
	xhr.open("POST", "http://139.155.8.217/api/Sync/SyncAsync");
	var data = {
		Mail: "1920249011111111111111@qq.com",
		Sid: "fcc7Rkf4RmMfU1IPbqi9A52V,4,c9du-YpJvbq0.",
		Cookie: "pgv_pvi=7372097536; RK=NbhlGJuJRz; ptcz=d1ab0013e91d678c776fa11601fc970a7c16ba4e10decb8bdb651c8f5e3efde5; webp=1; pgv_pvid=5233643110; pgv_info=ssid=s3697213894; pac_uid=0_5d4f78e505e7c; pgv_si=s4314483712; ptisp=cnc; wimrefreshrun=0&; qm_logintype=qq; qm_flag=0; qm_domain=https://mail.qq.com; edition=mail.qq.com; foxacc=68771803&1|19202490&1; newpt=2; ptui_loginuin=68771803; mcookie=0&y; FTN5K=c80dc502; qm_ptsk=68771803&@I3kQ0U6Wt|19202490&@stHtW9C4m; promote_iphone=1; CCSHOW=000000; device=iPad; qm_loginfrom=68771803&psaread|19202490&wpt; tinfo=1568463737.0000*; username=19202490&19202490; uin=o0019202490; skey=@zEoPX2s47; luin=o0019202490; lskey=0001000014ae03a92227d6962006626b2baa1f5642d1c1553e55aa2e455cf8c0a9bb0aafb85b93ee0dda5853; p_uin=o0019202490; pt4_token=sv3fRq2OcyJiF76rLcnWLSrBfY-26tHtWZVkZP98Q-s_; p_skey=B0NBtD3FiWalk7s6JCS1sh2DYevBqLkZkbeiWGeYSLk_; p_luin=o0019202490; p_lskey=000400006143d3ab3602365213939f24f2af808389afa117ab81711b5c9ad92851caa6a541375f86de767dad; qqmail_alias=xiawei1981@foxmail.com; msid=fcc7Rkf4RmMfU1IPT_69AZ2X,4,c9du-YpJvbq0.; sid=19202490&8285558e8257fb77f664672dbd08806f,c9du-YpJvbq0.; qm_username=19202490; ssl_edition=sail.qq.com; pcache=ddc9e0c35836438MTU3MTA1NTc3Ng@19202490@4; mpwd=1A01962AD3F0C5DA6F61B555E27DA21E93DDC420CA9FA5785E41DBB34EC9D915@19202490@4; qm_sk=68771803&fKLQO-Ct|19202490&bqi9A52V; new_mail_num=68771803&0|19202490&228; device=; qm_ssum=68771803&ee4eb361c3068f263f1deb80b8917911|19202490&345bed9450434ab745acb4969c4b8346",
	};
	xhr.setRequestHeader('Content-Type','application/json');
	// 发送HTTP请求
	xhr.send(JSON.stringify(data)); 
	console.log('aaaaa');
}
if (window.plus) {
	plusReady();
} else {
	document.addEventListener('plusready', plusReady, false);
}

// document.addEventListener('UniAppJSBridgeReady', function() {
// 	console.log('测试:UniAppJSBridgeReady');
// 	uni.postMessage({
// 		data: {
// 			action: 'postMessage'
// 		}
// 	});
// });
