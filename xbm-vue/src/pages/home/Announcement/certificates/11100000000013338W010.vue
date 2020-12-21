<template>
<!-- 建设工程规划许可证 -->
<div class="sub-content" v-if="detail">
					<div class="big-title">{{detail.jsxmmc}}</div>
					<div class="certificates-wrap clearfix">
						<div class="certificates-left jsgc">
							<span class="l-1">{{detail.licenseNumber}}</span>
							<span class="l-2">{{detail.issueunitname}}</span>
							<span class="l-3">
								{{detail.issueDate}}
							</span>
						</div>
						<div class="certificates-right jsgc">
							<span class="r-1">{{detail.jsdwgr}}</span>
							<span class="r-2">{{detail.jsxmmc}}</span>
							<span class="r-3">{{detail.jswz}}</span>
							<span class="r-4">{{detail.jsgm}}</span>
							<span class="r-10">
								<div class="title">
									<a href="#" id="attach0">{{detail.ftjfjmc}}</a>
								</div>
							</span>
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
    width: 848px;
    margin: 0 auto;
    font-family: simsun;
    font-size: 12px;
    color: #000;
    .certificates-left {
    width: 425px;
    height: 581px;
    float: left;
    position: relative;
     border:1px solid #a1a1a1;
   &.jsgc {
    background: url('~@/assets/images/certificates/jsgcghxk_left.png') no-repeat top left;
    background-color: #dfdee3;
    // background: url('~@/assets/images/certificates/certificates_jsgc_left.jpg') no-repeat top left;
    .l-1 {
        top: 148px;
          right: 37px;
        text-align: right;
        display: inline-block;
        width: 160px;
        text-align: center;
    }
    .l-2 {
        bottom: 116px;
        left: 231px;
        text-align: left;
    }
    .l-3 {
        bottom: 80px;
        left: 232px;
        text-align: left;
    }
  }
}
.certificates-right {
    width: 411px;
    height: 581px;
    float: right;
    position: relative;
     border:1px solid #a1a1a1;
    &.jsgc {
     background: url('~@/assets/images/certificates/jsgcghxk_right.png') no-repeat top left;
    // background: url('~@/assets/images/certificates/certificates_jsgc_right.jpg') no-repeat top left;
    .r-1,  .r-2, .r-3,  .r-4 {
      left: 153px;
    }
    .r-1 {
    top: 38px;
    text-align: left;
    }
    .r-2 {
        top: 70px;
        text-align: left;
    }
    .r-3 {
        top: 108px;
        text-align: left;
    }
    .r-4 {
        top: 146px;
        text-align: left;
    }
.r-10 {
    top: 196px;
    text-align: left;
    left: 36px;
    display: inline-block;
    width: calc(100% - 60px);
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
.certificates-left>*, .certificates-right>* {
    display: block;
    position: absolute;
}
</style>