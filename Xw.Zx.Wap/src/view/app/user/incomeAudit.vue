<template>
  <div class="wrapper">
    <div class="bar">
      <van-nav-bar
        :title="$route.meta.title"
        left-arrow
        @click-left="$router.go(-1)"
      />
    </div>
    <div class="container">
      <div
        class="card"
        v-for="(iteam, index) in incomeDetail"
        v-bind:key="index"
      >
        <div class="card-left">
          <p>申请人: {{ iteam.memberDto.realName }}</p>
          <p>时间: {{ iteam.detailsDto.addTime }}</p>
          <p>电话: {{ iteam.memberDto.phone }}</p>
          <p>地址: {{ iteam.memberDto.address }}</p>
          <p>状态: {{ iteam.detailsDto.withdrawDepositStateName }}</p>
          <p
            v-if="
              iteam.detailsDto.withdrawDepositState == 3 ||
              iteam.detailsDto.withdrawDepositState == 2
            "
          >
            备注:
            <span>{{ iteam.detailsDto.remark }}</span>
          </p>
          <div
            class="card-left-auditbtn"
            v-if="iteam.detailsDto.withdrawDepositState == 0"
          >
            <van-button
              class="card-left-auditbtn-btn"
              color="#999"
              round
              plain
              size="small"
              @click="postAudit(iteam.detailsDto.timestamp, false)"
            >
              拒绝
            </van-button>
            <van-button
              class="card-left-auditbtn-btn"
              type="primary"
              round
              size="small"
              color="linear-gradient(to right, #ff7a00, #ff5000)"
              @click="postAudit(iteam.detailsDto.timestamp, true)"
            >
              通过
            </van-button>
            <!-- <van-button
              class="foot-btn"
              color="#999"
              round
              plain
              size="small"
              @click="bindClick(iteam.memberDto.id)"
            >
              收益
            </van-button> -->
          </div>
        </div>
        <div class="card-right">
          <span>{{ iteam.detailsDto.amount }}元</span>
        </div>
      </div>
    </div>
  </div>
</template>

<script>
import api from "@/api/sqbApi";
export default {
  name: "",
  props: [""],
  data() {
    return {
      incomeDetail: null,
    };
  },

  components: {},

  computed: {},

  beforeMount() {
    this.getAuditDetails();
  },

  mounted() {},

  methods: {
    getAuditDetails() {
      api.withdrawDeposit.getAuditDetails().then((res) => {
        this.incomeDetail = res.result;
      });
    },
    postAudit(timestamp, ispass) {
      const _this = this;
      api.withdrawDeposit
        .auditwithdrawDeposit({ timestamp, ispass })
        .then((res) => {
          _this.$toast(res.msg);
          _this.getAuditDetails();
        })
        .catch(() => {
          _this.getAuditDetails();
        });
    },
  },

  watch: {},
};
</script>
<style lang='scss' scoped>
.card {
  margin-bottom: 10px;
  background-color: white;
  padding: 10px;
  font-size: 16px;
  display: flex;
  flex-direction: row;
  justify-content: flex-start;
  align-items: flex-start;
  height: 170px;
  &-left {
    width: calc(100% - 100px);
    p {
      font-size: 16px;
      line-height: 24px;
    }
    &-auditbtn {
      margin-top: 15px;
      display: flex;
      flex-direction: row;
      justify-content: space-between;
      align-items: flex-start;
      &-btn {
        width: 120px;
      }
    }
  }
  &-right {
    width: 100px;
    height: 100%;
    display: flex;
    flex-direction: row;
    justify-content: flex-end;
    align-items: center;
    span {
      font-size: 30px;
      font-weight: bolder;
      margin-left: 5px;
      margin-right: 5px;
      color: #ff5000;
    }
  }
}
</style>