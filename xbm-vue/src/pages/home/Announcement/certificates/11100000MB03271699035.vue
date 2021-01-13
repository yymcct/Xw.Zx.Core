<template>
<!-- 建设项目用地预审与选址意见书 -->
<div class="sub-content" v-if="detail">
					<div class="big-title">{{detail.xmmc}}</div>
					<div class="certificates-wrap clearfix">
						<div class="certificates-left jsgc">
             
							<span class="l-1">{{detail.licenseNumber}}</span>
							<span class="l-2">{{detail.issueunitname}}</span>
							<span class="l-3">
							{{detail.issueDate}}
							</span>
						</div>
						<div class="certificates-right jsgc">
               <div class="jsgc-inner">
							<div class="r-1"><span class="rt-text">{{detail.xmmc}}</span></div>
							<div class="r-2"><span class="rt-text">{{detail.xmdm}}</span></div>
							<div class="r-3"><span class="rt-text">{{detail.jsdwmc}}</span></div>
							<div class="r-4"><span class="rt-text">{{detail.xmjsyj}}</span></div>
							<div class="r-5"><span class="rt-text">{{detail.xmnxwz}}</span></div>
							<div class="r-6"><span class="rt-text">{{detail.nydmjhgdlmx}}</span></div>
							<div class="r-7"><span class="rt-text">{{detail.njsgm}}</span></div>
							<div class="r-10">
								<div class="title">
									{{detail.ftjfjmc}}
                  <!-- <a href="#" id="attach0"></a> -->
								</div>
							</div>
               </div>
						</div>
					</div>
				</div>
</template>

<script>
export default {
  name: "right1",
  components: {},
  props:['data'],
  data() {
    return {
      detail: {},
      loading: false
    };
  },
  mounted() {
    this.detail=this.data;
  },
  methods: {
  }
};
</script>

<style  lang="scss" scoped>
  .sub-content {
    color: #666;
     .big-title {
    padding: 10px 0;
    font-size: 1.5em;
    text-align: center;
    color: #333;
    font-family: "Microsoft Yahei";
 }
 .certificates-wrap {
    width: 888px;
    margin: 0 auto;
    font-family: simsun;
    font-size: 12px;
    color: #000;
    .certificates-left {
    width: 411px;
    height: 581px;
    float: left;
    position: relative;
    border:1px solid #a1a1a1;
     
   &.jsgc {
    // background: url('~@/assets/images/certificates/jsgcghxk_left1.png') no-repeat top left;
    background: url('~@/assets/images/certificates/ydys_left.jpg') no-repeat top left;
    .l-1,.l-2,.l-3{
     display: block;
    position: absolute;
    }
    .l-1 {
       width: 150px;
        top: 185px;
        right: 32px;
        text-align: center;
      }
.l-2 {
    bottom: 126px;
    left: 231px;
    text-align: left;
}
.l-3 {
    bottom: 91px;
    left: 232px;
    text-align: left;
}
  }
}
.certificates-right {
    width: 450px;
    height: 581px;
    float: right;
    position: relative;
    border: 1px solid #a1a1a1;
    &.jsgc {
    //  background: url('~@/assets/images/certificates/jsgcghxk_right.png') no-repeat top left;
    background: url('~@/assets/images/certificates/ydys_right.jpg') no-repeat top left;
    .jsgc-inner{
          position: absolute;
          height: 396px;
          width: 378px;
          left: 32px;
          top: 20px;
    }
    .r-1,  .r-2, .r-3,  .r-4,.r-5,.r-6,.r-7 {
        width: 250px;
        margin-left:127px;
        padding: 0px 5px;
        .rt-text{
            display: table-cell;
            vertical-align:middle;
            height:36px;
        }
    }
 .r-6 {
    height:51px
  }
.r-10 {
    padding: 20px 10px 5px;
   a {
    line-height: 20px;
     }
      }
   }
}
}
  }
  .clearfix {
    zoom: 1;
    &:after{
    content: "";
    display: block;
    clear: both;
    height: 0;
    }
}
</style>