<template>
  <view>
    <page-head :title="title"></page-head>
    <view class="uni-common-mt">
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
    </view>
  </view>
</template>
<script>
export default { data() {
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
  },};
</script>

<style scoped>
.title {
  padding: 10upx 25upx;
}
.uni-icon-clear,
.uni-icon-eye {
  color: #999;
}
</style>
