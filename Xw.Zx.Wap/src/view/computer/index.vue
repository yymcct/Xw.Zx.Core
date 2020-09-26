<template>
  <div class="wrapper">
    <!-- <div class="wrapper-bg" :style="{backgroundImage:'url('+require('../../assets/images/computer/bg.png')+') '}"></div> -->
    <div
      class="wrapper-bg"
      :style="{backgroundImage:'url('+require('../../assets/images/computer/bg.png')+') '}"
    >
      <div class="block">
        <div class="block-head"></div>
        <div class="block-content">
          <div class="block-content-1">
            <ul>
              <li>
                8月20日,
                <span>最高人民法院</span>明确规定法律支持的利率是
                <span>年化15.4%</span>
              </li>
              <li>由此带来的结果是大量的贷款机构的利率超过了司法保护上限</li>
            </ul>
          </div>
          <div class="block-content-2">
            <p class="block-content-2-head">你是否有过以下顾虑?</p>
            <p>
              你
              <span>是否借过</span>网贷或者小贷? 如果有，那你的
              <span>福音</span>来了！
            </p>

            <p>
              如果按照司法保护上限，你
              <span>应该还多少？</span>
            </p>
            <p>
              如果只还法律支持的利息，你
              <span>可以少还多少？</span>
            </p>
            <p @click="goComputer">请进入网贷利息减免计算器</p>
            <p @click="goComputer">
              算一下你的利息可以
              <span>节省多少钱？</span>
            </p>
          </div>
        </div>
      </div>
      <div class="cesuan" @click="goComputer">
        <img class="cesuan-btn" :src="require('../../assets/images/computer/cesuan.png')" alt />
        <img class="cesuan-dj" :src="require('../../assets/images/computer/dj.gif')" alt />
      </div>
      <div class="block">
        <div class="block-head"></div>
        <div class="block-content">
          <div class="block-content-3">
            <p>
              8月20日，
              <span>最高人民法院</span>召开新闻发布会，正式发布新修订的《
              <span>最高人民法院关于审理民间借贷案件适用法律若干问题的规定</span>》
            </p>
            <p>
              将
              <span>第二十六条修改</span>为:
            </p>
            <p
              style="text-indent: 2em;padding-top:0px"
            >“出借人请求借款人按照合同约定利率支付利息的，人民法院应予支持，但是双方约定的利率超过合同成立时一年期贷款市场报价利率四倍的除外。</p>
            <div class="block-content-3-b">
              <p>前款所称‘一年期贷款市场报价利率’，是指中国人民银行授权全国银行间同业拆借中心自2019年8月20日起每月发布的一年期贷款市场报价利率。”</p>
            </div>
            <div class="block-content-3-f">
              <p></p>
            </div>
            <div class="block-content-3-b2">
              <p>4倍LPR=15.4%</p>
            </div>
          </div>
        </div>
      </div>

      <div class="cesuan" @click="goComputer">
        <img class="cesuan-btn" :src="require('../../assets/images/computer/cesuan.png')" alt />
        <img class="cesuan-dj" :src="require('../../assets/images/computer/dj.gif')" alt />
      </div>
    </div>
  </div>
  <!-- <div class="wrapper"> -->

  <!-- </div> -->
</template>

<script>
export default {
  name: "",
  props: [""],
  data() {
    return {};
  },

  components: {},

  computed: {},

  beforeMount() {},

  mounted() {},

  methods: {
    goComputer() {
      this.$router.push({ path: `/sqb/computer/computer` });
    },
  },

  watch: {},
};
</script>
<style lang='scss' scoped>
.wrapper {
  width: 100%;
  min-height: 100vh;
  background-size: 100%;
  background-repeat: no-repeat;
  background: linear-gradient(0deg, rgb(69, 69, 241) 0%, rgb(20, 28, 200) 100%);

  &-bg {
    width: 100%;
    min-height: 100vh;
    background-size: 100%;
    background-repeat: no-repeat;
    padding-top: 120px;
    padding-bottom: 20px;
    .block {
      &-head {
        background-image: linear-gradient(
          0deg,
          rgb(139, 131, 251) 6%,
          rgb(79, 61, 255) 100%
        );
        box-shadow: 0px 2.5px 5px 0px rgba(41, 31, 235, 0.75);
        margin: 0px 5px;
        border-radius: 7px;
        height: 14px;
      }
      &-content {
        background-image: linear-gradient(
          0deg,
          rgb(123, 113, 255) 0%,
          rgb(97, 90, 238) 48%,
          rgb(143, 136, 236) 100%
        );
        min-height: 100px;
        margin: 0px 15px;
        padding: 20px 10px;
        &-1 {
          padding: 0 10px;
          background-color: #fff;
          font-size: 11px;
          line-height: 20px;
          span {
            color: red;
          }
          ul {
            padding: 10px;
            padding-left: 20px;

            li {
              padding: 10px 0;
              list-style-type: disc;
              line-height: 18px;
            }
          }
        }
        &-2 {
          margin-top: 20px;
          //margin-bottom: 20px;
          font-size: 13px;
          span {
            color: red;
          }
          p {
            background-color: #fff;
            margin-top: 2px;
            padding: 5px 5px;
            line-height: 22px;
          }
          &-head {
            background-color: rgb(72, 61, 234) !important;
            color: #fff;
          }
        }
        &-3 {
          font-size: 13px;
          span {
            color: red;
          }
          p {
            background-color: #fff;
            padding: 5px 5px;
            line-height: 22px;
          }
          &-b {
            background-color: #fff;
            padding: 5px;
            padding-bottom: 0px;
            box-sizing: border-box;

            p {
              border: 2px solid red;
            }
          }
          &-f {
            display: flex;
            background-color: #fff;
            width: 100%;
            box-sizing: border-box;
            p {
              width: 156px;
              height: 10px;
              border-right: 2px solid red;
            }
          }
          &-b2 {
            background-color: #fff;
            padding: 5px;
            padding-top: 0px;
            padding-bottom: 20px;
            box-sizing: border-box;

            p {
              margin: 0px 100px;
              font-weight: bold;
              text-align: center;
              color: red;
              border: 2px solid red;
            }
          }
        }
      }
    }
    .cesuan {
      margin: 20px 20px 30px 20px;
      position: relative;
      &-btn {
        width: 100%;
      }
      &-dj {
        position: absolute;
        width: 100px;
        top: 20px;
        right: 0px;
      }
    }
  }
}
</style>

