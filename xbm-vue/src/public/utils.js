// import Vue from "vue";
import axios from 'axios'
import { getLogin } from "@/public/auth";
import '../../static/js/configUrl.js';
var dghyUrl = getBasicUrl();
//换肤加class函数
 function toggleClass(element, className) {
    if (!element || !className) {
      return
    }
    element.className = className;
}
const fetchData = function(url = "", params = {}, type = "POST") {
    if (type.toUpperCase() === "GET") {
        return axios.get(url, {params: params});
    } else if (type.toUpperCase() === "POST") {
        return axios.post(url, params);
    }
};
// 两边间隔2px
function getTextDisplayWidth(text, fontSize) {
    if (text) {
        return getTextLen(text) * fontSize + 4;
    }
    return 0;
}
// 获得字符串长度，中文1，英文0.5
function getTextLen(str) {
    let realLenth = 0;
    let len = str.length;
    let charCode = -1;
    for (let i = 0; i < len; i++) {
        charCode = str.charCodeAt(i);
        if (charCode >= 0 && charCode <= 128) {
            realLenth += 1;
        } else {
            realLenth += 2;
        }
    }
    return Math.ceil(realLenth / 2);
}
// 格式化后端返回的树结构数据
function forMateData(data, relationField, idFiled) {
    //relationField 父节点id idFiled节点id
    let result = [];
    if (data) {
      if (!relationField) {
        relationField = "parentId";
      }
      if (!idFiled) {
        idFiled = pId;
      }
      let treeData = JSON.parse(JSON.stringify(data));
      treeData.forEach(item => {
        if (!item.children) {
        //   item["children"] = [];
        }
        let pId = item[relationField];
        if (!pId) {
          result.push(item);
        } else {
          let parent = treeData.find(node => {
            return node[idFiled] == pId;
          });
          if (!parent) {
            result.push(item);
            return;
          } else if (!parent["children"]) {
            parent["children"] = [];
          }
          parent["children"].push(item);
        }
      });
    }
    return result;
  }
  function addDate(date, days) {
    var date = new Date(date);
    days && date.setDate(date.getDate() + days);
    var month = date.getMonth() + 1;
    var day = date.getDate();
    var hours = date.getHours();
    var minutes = date.getMinutes();
    var mm = "'" + month + "'";
    var dd = "'" + day + "'";
    var hh = "'" + hours + "'";
    var MM = "'" + minutes + "'";
    //单位数前面加0
    if (mm.length == 3) {
      month = "0" + month;
    }
    if (dd.length == 3) {
      day = "0" + day;
    }
    if (hh.length == 3) {
      hours = "0" + hours;
    }
    if (MM.length == 3) {
      minutes = "0" + minutes;
    }
    var time =
      date.getFullYear() + "-" + month + "-" + day + " " + hours + ":" + minutes;
    return time;
  }
  function getNowTime() {
    var date = new Date();
    // var seperator1 = "-";//设置成自己想要的日期格式 年/月/日
    // var seperator2 = ":";//设置成自己想要的时间格式 时:分:秒
    var month = date.getMonth() + 1;//月
    var days = date.getDate();//日
    var hours = date.getHours();//时
    var minutes = date.getMinutes();//分
    var seconds = date.getSeconds();//分
    var hh = "'" + hours + "'";
    var mm = "'" + minutes + "'";
    var ss = "'" + seconds + "'";
    if (month >= 1 && month <= 9)
    {
        month = "0" + month;
    }
    if (days >= 0 && days <= 9)
    {
        days = "0" + days;
    }
    if (hh.length == 3) {
        hours = "0" + hours;
    }
    if (mm.length == 3) {
        minutes = "0" + minutes;
    }
    if (ss.length == 3) {
        seconds = "0" + seconds;
    }
    var currentdate = date.getFullYear() + '-' + month + '-' + days
        + " " + hours + ":" + minutes + ":" + seconds;
    return currentdate;
}
function openDGHYApplication(){
  var username='';
  var password='';
  var strCookie = getLogin();
    var arrCookie = strCookie.split(";");
    var a=arrCookie[0].indexOf('=');
    var b=arrCookie[1].indexOf('=');
    username=arrCookie[0].substring(a+1,arrCookie[0].length);
    password =arrCookie[1].substring(b+1,arrCookie[1].length);
    if(arrCookie.length>2){
      let c=arrCookie[2].indexOf('=');
      let temp=arrCookie[2].substring(c+1,arrCookie[2].length);
      username=temp?temp:username;
    }
    var path=decodeURIComponent(dghyUrl+"?parmas="+username+'&parmas1='+password);
    window.open(path, "_blank");
    // return path
}
export { fetchData, getTextDisplayWidth, toggleClass,forMateData,addDate,getNowTime,openDGHYApplication};