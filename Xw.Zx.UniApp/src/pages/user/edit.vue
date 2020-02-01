<template>
  <view class="content">
    <view class="input-group">
      <view class="input-row border">
        <text class="title">姓名：</text>
        <m-input type="text" focus clearable v-model="realName" placeholder="请输入真实姓名"></m-input>
      </view>

      <view class="input-row border">
        <text class="title">地址：</text>
        <m-input
          type="text"
          disabled="disabled"
          v-model="address"
          placeholder="请选择地址"
          @touchstart="changeShow('QS_Picekr_city')"
        ></m-input>
      </view>
      <view class="input-row border">
        <text class="title">支付宝：</text>
        <m-input type="text" :disabled="memberDto.aliPayAccount!=''"  v-model="aliAccount" placeholder="提现账户,设置后不能修改"></m-input>
      </view>
      <view class="input-row border">
        <text class="title">邮箱：</text>
        <m-input type="text" v-model="email" placeholder="请输入账单邮箱"></m-input>
      </view>
       <view class="input-row border">
        <text class="title">验证码：</text>
        <m-input type="text" v-model="smsCheck" placeholder="请输入短信验证码"></m-input>
      </view>
    </view>
    <view class="btn-row" v-show="memberDto.phone!=''">
      <button type="primary" :disabled="!enableSend" hover-class="none" @tap="getCode">获取验证码</button>
    </view>
    <view class="btn-row">
      <QSpicker
        type="city"
        ref="QS_Picekr_city"
        mode="top"
        top="200px"
        pickerId="city_1"
        :dataSet="citySet"
        showReset
        @confirm="confirm($event)"
      />
      <button type="primary" class="primary"  hover-class="none" @tap="edit">提交</button>
    </view>
  </view>
</template>

<script>
import mInput from "../../components/m-input.vue";
import QSpicker from "@/components/QuShe-picker/QuShe-picker.vue";
export default {
  components: {
    mInput,
    QSpicker
  },
  data() {
    return {
      user: null,
      memberDto:null,
      realName: "",
      address: "",
      aliAccount:"",
      cityCode: "",
      email : "",
       enableSend: true,
      smsCheck: "",
      citySet: {
        defaultValue: [0, 0, 0]
      }
    };
  },
  methods: {
    changeShow(name) {
      this.$refs[name].show();
    },
    getCode: function() {
      var _phone = this.memberDto.phone;
      var _this = this;
      _this.enableSend = false;
      uni.request({
        url: `${this.baseUrl}/api/Member/GetSmsCode?phone=${_phone}`,
        method: "GET",
        success: res => {
          if (res.data.statusCode == 200) {
            uni.showToast({
              icon: "none",
              title: "验证码已发送"
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
    },
    edit() {
      if (this.realName.length < 2) {
        uni.showToast({
          icon: "none",
          title: "请填写真实姓名!"
        });
        return;
      }
      if (this.cityCode.length < 2) {
        uni.showToast({
          icon: "none",
          title: "请选择地址"
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
      if (this.smsCheck.length < 4) {
        uni.showToast({
          icon: "none",
          title: "请填写验证码"
        });
        return;
      }
      uni.request({
        url: `${this.baseUrl}/api/Member/PostMember`,
        data: {
          realName: this.realName,
          cityCode: this.cityCode,
          address: this.address,
          aliAccount: this.aliAccount,
          email: this.email,
	        phone:this.memberDto.phone,
          smsCheck:this.smsCheck,
        },
        method: "POST",
        header: {
          "Content-Type": "application/json",
          Authorization: `Bearer ` + this.user.token
        },
        success: res => {
          if (res.data.statusCode == 200) {
            uni.showToast({
              icon: "none",
              title: "修改成功"
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
    },
    getSelf: function() {
      uni.request({
        url: `${this.baseUrl}/api/Member/GetSelf`,
        method: "GET",
        header: {
          "Content-Type": "application/json",
          Authorization: `Bearer ` + this.user.token
        },
        success: res => {
          if (res.data.statusCode == 200) {
			  console.log("aliPayAccount:" + res.data.result.aliPayAccount);
			  console.log("email:" + res.data.result.email);
            this.realName = res.data.result.realName;
            this.cityCode = res.data.result.cityCode;
            this.address = res.data.result.address;
            this.aliAccount = res.data.result.aliPayAccount;
			if(res.data.result.email == null)
			{
			 this.email ="";
			}else{
             this.email = res.data.result.email;
			}
			//this.email = res.data.result.email;
            this.memberDto = res.data.result;
           
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
    },
    confirm(res) {
      this.address = res.data.label;
      this.cityCode = res.data.cityCode;
      console.log(this.address);
      console.log(this.cityCode);
    }
  },
  onLoad: function() {
    this.user = this.getUser("../user/user");
    if (!this.user) {
      return false;
    }
    this.getSelf();
  }
};
</script>

<style>
</style>