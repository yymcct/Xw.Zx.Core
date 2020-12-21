<template>
  <div class="grid-inner-content">
    <div class="panel-header">邮件</div>
    <div class="panel-body">
      <el-row :gutter="10" style="height:100%">
        <el-col :span="12" style="height:calc(100% - 10px)">
          <div class="align-items" @click="addTab('inBox')">
            <p class="items-title">收件箱</p>
            <div class="items-text first-items">
              <div class="text">
                <strong style="color: #f5564a;">{{data.sjwd}}</strong>
                <br>
                <small>未读</small>
              </div>
              <div class="icon bg-orange">
                <i class="fa fa-envelope"></i>
              </div>
            </div>
            <div class="items-text first-items">
              <div class="text">
                <strong style="color: #11b97b;">{{data.sjzs}}</strong>
                <br>
                <small>总数</small>
              </div>
              <div class="icon bg-green">
                <i class="fa fa-envelope-open"></i>
              </div>
            </div>
          </div>
        </el-col>
        <el-col :span="12">
          <div class="align-items" @click="addTab('outBox')">
            <p class="items-title">发件箱</p>
            <div class="items-text">
              <div class="text">
                <strong style="color:#11b2f8">{{data.fjzs}}</strong>
                <br>
                <small>总数</small>
              </div>
              <div class="icon bg-blue">
                <i class="fa fa-paper-plane-o"></i>
              </div>
            </div>
          </div>
          <div class="align-items" @click="addTab('draft')">
            <p class="items-title">草稿箱</p>
            <div class="items-text">
              <div class="text">
                <strong style="color:#7e8df8">{{data.cgzs}}</strong>
                <br>
                <small>总数</small>
              </div>
              <div class="icon bg-purple">
                <i class="fa fa-calendar-o"></i>
              </div>
            </div>
          </div>
        </el-col>
      </el-row>
    </div>
  </div>
</template>

<script>
import { homeEmailCount } from "@/public/apiService/home";
export default {
  name: "Home",
  data: function() {
    return {
      data: "",
      list: []
    };
  },
  created() {},
  mounted() {
    this.getData();
  },
  methods: {
    getData() {
      homeEmailCount()
        .then(res => {
          this.data = res.data||{
            sjwd:0,
            sjzs:0,
            fjzs:0,
            cgzs:0
          }
        })
        .catch(err => {
          console.log(err);
        });
    },
    addTab(a) {
      this.$router.push("/manage/email");
      this.$store.commit("curSideName", a);
      this.$store.commit("manageMenuDefault", {
        BA_PATH: "/manage/email",
        Ba_Name: "内部邮件"
      });
    }
  },
  components: {}
};
</script>

<style lang="scss" scoped>
/deep/ .panel-body{
 .el-col-12{
height:calc(50% - 10px);
  }
.align-items {
  height: 100%;
  padding: 5px 15px;
  margin-bottom: 10px;
  background: #fff !important;
  -webkit-box-shadow: 2px 2px 2px rgba(0, 0, 0, 0.1),
    -1px 0 2px rgba(0, 0, 0, 0.05);
  box-shadow: 2px 2px 2px rgba(0, 0, 0, 0.1), -1px 0 2px rgba(0, 0, 0, 0.05);
  .first-items {
    margin: 15px 0px;
  }
  .items-text {
    display: flex;
    padding: 5px;
    cursor: pointer;
    &:hover{
      background: #f7fbfd;
    }
  }
  .items-title {
    font-size: 14px;
  }
  .icon {
    width: 40px;
    height: 40px;
    line-height: 40px;
    text-align: center;
    min-width: 40px;
    max-width: 40px;
    color: #fff;
    border-radius: 50%;
    margin-right: 15px;
  }
  strong {
    font-size: 1.5em;
    color: #333;
    font-weight: 700;
    line-height: 1;
  }
  small {
    color: #aaa;
    text-transform: uppercase;
    font-size: 80%;
    font-weight: 400;
  }
  .text {
    flex: 1;
  }
  .bg-red {
    background: #ff7676 !important;
  }
  .bg-green {
    background: #ecf8f1 !important;
    color: #11b97b;
    font-weight: bolder;
    // background: #54e69d !important;
  }
  .bg-orange {
    background: #fff0e5 !important;
    color: #f5564a;
    // background: #ffc36d !important;
  }
  .bg-blue {
    background: #e5f7fe;
    color: #11b2f8;
    font-weight: bolder;
  }
  .bg-purple {
    background: #f2f3fe;
    color: #7e8df8;
    font-weight: bolder;
  }
}
}

</style>
