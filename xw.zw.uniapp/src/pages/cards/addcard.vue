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
        <m-input type="text" focus clearable v-model="cardnumber" placeholder="请输入账号"></m-input>
      </view>
      <view class="input-row border">
        <text class="title">开户行：</text>
        <m-input type="text" displayable v-model="password" placeholder="请输入密码"></m-input>
      </view>
      <view class="input-row border">
        <text class="title">邮箱：</text>
        <m-input type="text" clearable v-model="email" placeholder="请输入邮箱"></m-input>
      </view>
      <view class="input-row border">
        <text class="title">密码：</text>
        <m-input type="password" displayable v-model="password" placeholder="请输入密码"></m-input>
      </view>
      <view class="input-row border">
        <text class="title">手机号:</text>
        <m-input type="text" displayable v-model="password" placeholder="请输入密码"></m-input>
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
          name: "广大银行"
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
      account: "",
      password: "",
      email: ""
    };
  },
  methods: {
    radioChange(evt) {
      for (let i = 0; i < this.items.length; i++) {
        if (this.items[i].value === evt.target.value) {
          this.current = i;
          break;
        }
      }
    },
    register() {
      /**
       * 客户端对账号信息进行一些必要的校验。
       * 实际开发中，根据业务需要进行处理，这里仅做示例。
       */
      if (this.account.length < 5) {
        uni.showToast({
          icon: "none",
          title: "账号最短为 5 个字符"
        });
        return;
      }
      if (this.password.length < 6) {
        uni.showToast({
          icon: "none",
          title: "密码最短为 6 个字符"
        });
        return;
      }
      if (this.email.length < 3 || !~this.email.indexOf("@")) {
        uni.showToast({
          icon: "none",
          title: "邮箱地址不合法"
        });
        return;
      }

      const data = {
        account: this.account,
        password: this.password,
        email: this.email
      };
      service.addUser(data);
      uni.showToast({
        title: "注册成功"
      });
      uni.navigateBack({
        delta: 1
      });
    }
  }
};
</script>

<style>
.uni-list-cell {
  justify-content: flex-start;
}
</style>
