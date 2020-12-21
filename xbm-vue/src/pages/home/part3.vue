<template>
  <div class="part3-box">
    <ul>
      <li class="yellow" @click="toPath('/approval/personalInformation')">
        <i class="icon iconfont">&#xe615;</i>
        <div>
          <h4>行政审批</h4>
        </div>
      </li>
        <li class="lightblue" @click="toPath('/manage')">
        <i class="icon iconfont">&#xe603;</i>
        <div>
          <h4>政务管理</h4>
        </div>
      </li>
      <li class="blue" @click="getLoginInfo">
        <i class="icon iconfont">&#xe607;</i>
        <div>
          <h4>多规合一</h4>
        </div>
      </li>
      <li class="darkyellow" @click="toPath('/cksl/receiptManage')">
        <i class="icon iconfont">&#xe62d;</i>
        <div>
          <h4>窗口受理</h4>
        </div>
      </li>
    </ul>
  </div>
</template>

<script>
import { getLogin,getUserInfo } from "@/public/auth";
import { openDGHYApplication } from "@/public/utils";
export default {
  name: "part3",
  data() {
    return {
      user:getUserInfo()
    };
  },
  created(){},
  computed:{
    token:function(){
      let temp=this.$store.state.user.token;
        this.user=getUserInfo()
      return temp
    }
  },
  methods: {
     getLoginInfo:function(){
     if(!this.user||!this.token){
      this.$confirm('暂未登录,请先登录!', '登录提示', {
            confirmButtonText: '登录',
            cancelButtonText: '取消',
            type: 'warning'
          }).then(() => {
            this.$router.push('/login');
          }).catch(()=>{
            console.log('取消..');
          })
     }else{
       openDGHYApplication()
      }
   
    },
    toPath(path) {
      this.$router.push(path);
    },
  }
};
</script>

<style lang="scss" scoped>
.part3-box {
  height: 100%;
  ul {
    display: flex;
    height: 100%;
  }
  li {
    flex: 1;
    color: #fff;
    padding-top: 25px;
    text-align: center;
    cursor: pointer;
    i {
      font-size: 28px;
      vertical-align: top;
    }
    div {
      display: inline-block;
      text-align: left;
      padding-left: 7px;
    }
    h4 {
      font-size: 24px;
      line-height: 36px;
      font-weight: normal;
    }
  }
  li:hover {
    transform: scale(1.05);
  }
  .blue {
    background-color: #07438b;
  }
  .lightblue {
    background-color: #2588c4;
  }
  .yellow {
    background-color: #f4a23c;
  }
  .darkyellow {
    background-color: #f17f39;
  }
}
</style>