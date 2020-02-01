<template>
  <view>
    <view class="uni-padding-wrap uni-common-mt">
      <form @submit="formSubmit" @reset="formReset">
        <view class="uni-form-item uni-column">
          <input class="uni-input" name="input" placeholder="卡号" />
        </view>
       <view class="uni-form-item uni-column">
          <input class="uni-input" name="input" placeholder="开户行" />
        </view>
        <view class="uni-form-item uni-column">
          <input class="uni-input" name="input" placeholder="账单邮箱" />
        </view>
        <view class="uni-form-item uni-column">
          <input class="uni-input" name="input" placeholder="密码" />
        </view>
        <view class="uni-form-item uni-column">
          <input class="uni-input" name="input" placeholder="持卡人" />
        </view>
        <view class="uni-form-item uni-column">
          <input class="uni-input" name="input" placeholder="电话" />
        </view>
        <view class="uni-btn-v">
          <button formType="submit">提交</button>
          <button
            type="button"
            class="mui-btn mui-btn-primary mui-btn-outlined"
            formType="reset"
          >联系客服</button>
        </view>
      </form>
    </view>
  </view>
</template>
<script>
export default {
  data() {
    return {
      title: "请填写资料",
      items: [
        {
          value: "USA",
          name: "美国"
        },
        {
          value: "CHN",
          name: "中国",
          checked: "true"
        },
        {
          value: "BRA",
          name: "巴西"
        },
        {
          value: "JPN",
          name: "日本"
        },
        {
          value: "ENG",
          name: "英国"
        },
        {
          value: "FRA",
          name: "法国"
        }
      ],
      current: 0,
      pickerHidden: true,
      chosen: ""
    };
  },
  methods: {
    pickerConfirm: function(e) {
      this.pickerHidden = true;
      this.chosen = e.target.value;
    },
    pickerCancel: function(e) {
      this.pickerHidden = true;
    },
    pickerShow: function(e) {
      this.pickerHidden = false;
    },
    formSubmit: function(e) {
      console.log(
        "form发生了submit事件，携带数据为：" + JSON.stringify(e.detail.value)
      );
    },
    formReset: function(e) {
      console.log("清空数据");
      this.chosen = "";
    }
  }
};
</script>

<style>
.uni-form-item .title {
  padding: 20upx 0;
}
</style>
