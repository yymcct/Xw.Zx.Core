<template>
  <div class="box">
    <ul>
      <li v-for="(item,index) in lists" :key="index" @click="toCenter(item)">
        <div>
          <img :src="item.imgsrc" alt />
        </div>
        <p>{{item.text}}</p>
      </li>
    </ul>
  </div>
</template>
<script>
import { getLogin,getUserInfo } from "@/public/auth";
import { openDGHYApplication } from "@/public/utils";
export default {
  name: "home",
  data() {
    return {
      user:getUserInfo(),
      lists: [
        {
          imgsrc: require("../../assets/images/index/img31.png"),
          text: "行政审批",
          path: "approval"
        },
        {
          imgsrc: require("../../assets/images/index/img41.png"),
          text: "政务管理",
          path: "manage"
        },
        // {
        //   imgsrc: require("../../assets/images/index/img1.png"),
        //   text: "网上申请",
        //   path: "apply"
        // },
        // http://192.168.1.252:8099/dghy/index.html#/login
        {
          imgsrc: require("../../assets/images/index/img11.png"),
          text: "多规合一",
          path: ""
        },
        {
          imgsrc: require("../../assets/images/index/img21.png"),
          text: "窗口受理",
          path: "cksl/receiptManage"
        }
      ],
    };
  },
  created:function(){
  },
  computed:{
    token:function(){
      let temp=this.$store.state.user.token;
      this.user=getUserInfo();
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
    toCenter(item) {
      if (item.text != "多规合一") {
        this.$router.push(item.path);
        sessionStorage.setItem(
          "nav",
          JSON.stringify({ path: "application", index: "1" })
        );
      } else {
        this.getLoginInfo()
      }
    }
  }
};
</script>
<style lang="scss" scoped>
.box {
  padding: 60px 25px 0 25px;
  background: #fff;
  ul {
    overflow: hidden;
  }
  li {
    float: left;
    width: 464px;
    height: 342px;
    background: rgba(255, 255, 255, 1);
    box-shadow: 0px 2px 5px 0px rgba(51, 51, 51, 0.3);
    margin: 0 55px;
    margin-bottom: 65px;
    padding-top: 6px;
    text-align: center;
    cursor: pointer;
    img {
      width: 448px;
      height: 269px;
    }
    p {
      height: 68px;
      line-height: 68px;
      font-size: 25px;
      font-weight: 400;
      color: rgba(7, 67, 139, 1);
    }
    &:hover {
      transform: scale(1.01);
      -ms-transform: scale(1.01); /* IE 9 */
      -moz-transform: scale(1.01); /* Firefox */
      -webkit-transform: scale(1.01); /* Safari 和 Chrome */
      -o-transform: scale(1.01);
    }
  }
  li:nth-of-type(2n) {
    margin-right: 0;
  }
}
</style>