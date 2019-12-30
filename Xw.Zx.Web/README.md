# hblive

## Project setup
```
npm install
```

### Compiles and hot-reloads for development
```
npm run serve
```

### Compiles and minifies for production
```
npm run build
```

### Run your tests
```
npm run test
```

### Lints and fixes files
```
npm run lint
```

### Customize configuration
See [Configuration Reference](https://cli.vuejs.org/config/).

## 微信小程序单位换算
1rpx = 0.5px

12pt   16px
11pt   15px
10.5pt 14px
10pt   13px

0.8em
https://www.cnblogs.com/lbnnbs/p/7518934.html

/images ->  "require('@/assets/images/expo/zhibo.png')"



## 连接 

import { api_GetMeetingList } from "@/api/meetingApi";

@click="$router.push({ path: `/expo/company/${meetingId}-${item.id}`})"

 @click.stop="$router.push({ path: `/expo/product/${meetingId}-${pro.id}`})"

watch: {
$route() {
    this.meetingId = this.$route.params.id;
}
}

this.companyId = this.$route.params.id;


Toast('提交成功!');

## 微信分享
this.$globalFun.wxShare(location.href.split("#")[0], {
        title: this.meeting.sortName,
        desc: `${this.meeting.beginDate}至${this.meeting.endDate},${this.meeting.companyCount}家展商 | ${this.meeting.hit}位访问者`,
        link: location.href,
        imgUrl: this.meeting.banner,
        success: function() {}
    });
