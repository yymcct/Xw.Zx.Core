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

## 图片
/images ->  "require('@/assets/images/expo/zhibo.png')"

## API
import { api_GetMeetingList } from "@/api/meetingApi";

## 路由
@click="$router.push({ path: `/meeting/expo/company/${meetingId}-${item.id}`})"
@click.stop="$router.push({ path: `/meeting/expo/product/${meetingId}-${pro.id}`})"
this.$router.go(n)

watch: {
$route() {
    this.meetingId = this.$route.params.id;
}
}

<router-link :to="{ name: 'user', params: { userId: 123 }}">User</router-link>

router.push({ path: `/user/${userId}` })

this.companyId = this.$route.params.id;

```
  <van-nav-bar
    :title="$route.meta.title"
    left-arrow
    @click-left="$router.push(`/meeting/expo/${meetingId}/user`)"
  />

 text-indent:2em;

```

## 微信分享
 wxShare() {
  if (this.meeting == null) {
    setTimeout(() => {
      this.wxShare();
    }, 500);
  } else {
    const meeting = this.meeting;
    console.log("weixin", meeting);
    this.$globalFun.wxShare(location.href, {
      title: meeting.sortName + this.$route.meta.title,
      desc: `${meeting.beginDate}至${meeting.endDate.substr(8, 2)}日,${
        meeting.address
      }`,
      link: location.href,
      imgUrl: meeting.wxSharePicture
        ? meeting.wxSharePicture
        : meeting.banner,
      success: function() {}
    });
  }
}
## 检查是否登录
this.$globalFun.userInfoAPI.ifLogin(this.postCompanyMemberHits);

## 引用vuex
import { mapGetters } from 'vuex'
  computed: {
    ...mapGetters({
      meetingId: "meeting/meetingId",
      meeting: "meeting/meeting",
      user: "user/user"
    })
  },
this.$store.getters['meeting/meetingId']


## 检查是否微信环境
const isWeixin = () =>
      /micromessenger/.test(navigator.userAgent.toLowerCase());

字体
标准 16px
小号 14px
小小号 13px

标题
#333333
描述
#999999;

#8a8a8a
#ff5000

```
    display: flex;
    flex-direction: column;
    justify-content: flex-start;
    align-items: flex-start;
```

 <van-button type="primary" color="linear-gradient(to right, #ff7a00, #ff5000)">主要按钮</van-button>

 ### 提示
  this.$toast("账号最短为 5 个字符");



生成兑换码, 
使用兑换码
个人信息中显示会员级别

-- 