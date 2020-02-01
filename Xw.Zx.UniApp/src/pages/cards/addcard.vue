<template>
  <view class="content">
    <view class="input-group">
      <view class="uni-list">
        <radio-group @change="radioChange">
          <label
            class="uni-list-cell uni-list-cell-pd"
            v-for="(item, index) in items"
            :key="item.value"
          >
            <view>
              <radio :value="item.value" :checked="index === current" />
            </view>
            <view>{{item.name}}</view>
          </label>
        </radio-group>
      </view>
      <view class="input-row border">
        <text class="title">卡号：</text>
        <m-input type="text" focus displayable v-model="card.cardNum" placeholder="请输入信用卡号"></m-input>
      </view>
      <view class="input-row border">
        <text class="title">账单邮箱：</text>
        <m-input type="text" displayable v-model="card.email" placeholder="请输入账单邮箱"></m-input>
      </view>
      <view class="input-row border">
        <text class="title">授权码：</text>
        <m-input type="password" displayable v-model="card.password" placeholder="请输入邮箱授权码"></m-input>
      </view>
    </view>
    <view class="btn-row">
      <button type="primary" class="primary" @tap="register">添加</button>
    </view>
  </view>
</template>

<script>
import service from "../../service.js";
import mInput from "../../components/m-input.vue";

export default {
  components: {
    mInput
  },
  data() {
    return {
      items: [
        {
          value: "0",
          name: "招商银行",
          checked: "true"
        },
        {
          value: "1",
          name: "浦发银行"
        },
        {
          value: "2",
          name: "中信银行"
        },
        {
          value: "3",
          name: "平安银行"
        },
        {
          value: "4",
          name: "光大银行"
        },
        {
          value: "5",
          name: "华夏银行"
        },
        {
          value: "6",
          name: "民生银行"
        }
      ],
      current: 0,
      card: {
        id:0,
        cardNum: "",
        bank: 0,
        email: "",
        password: ""
      },
      user: null
    };
  },
  methods: {
    radioChange(evt) {
      for (let i = 0; i < this.items.length; i++) {
        if (this.items[i].value === evt.target.value) {
          this.card.bank = i;
          break;
        }
      }
    },
    register() {
      if (this.card.cardNum.length < 5) {
        uni.showToast({
          icon: "none",
          title: "账号最短为 5 个字符"
        });
        return;
      }
      if (this.card.password.length < 12) {
        uni.showToast({
          icon: "none",
          title: "授权码最短为 12 个字符"
        });
        return;
      }
      if (this.card.email.length < 3 || !~this.card.email.indexOf("@")) {
        uni.showToast({
          icon: "none",
          title: "邮箱地址不合法"
        });
        return;
      }
      uni.request({
        url: `${this.baseUrl}/api/BankCard/Post`,
        data: this.card,
        method: "POST",
        header: {
          "Content-Type": "application/json",
          Authorization: `Bearer ` + this.user.token
        },
        success: res => {
          if (res.data.statusCode == 200) {
            uni.showModal({
              title: "提示",
              content: "成功添加, 继续添加?",
              success: function(res) {
                if (res.confirm) {
                  uni.navigateTo({ url: "../cards/addcard" });
                  //this.card.cardNum = "";
                  //this.card.bank = 0;
                } else if (res.cancel) {
                  uni.navigateTo({ url: "../main/main" });
                }
              }
            });
          } else {
            uni.showToast({
              icon: "none",
              title: res.data.msg
            });
          }
        },
        fail: () => {
          uni.showToast({
            icon: "none",
            title: "网络异常"
          });
        }
      });
    }
  },
  onLoad: function() {
    this.user = this.getUser("../main/main");
    if (!user) {
      return false;
    }
  }
};
</script>

<style>
.uni-list-cell {
  justify-content: flex-start;
}
</style>
