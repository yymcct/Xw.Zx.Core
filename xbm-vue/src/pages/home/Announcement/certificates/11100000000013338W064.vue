<template>
<!-- 建设用地规划许可证 -->
<div class="sub-content" v-if="detail">
					<div class="big-title">{{detail.ydxmmc}}</div>
					<div class="certificates-wrap clearfix">
						<div class="certificates-left jsgc">
							<span class="l-1">{{detail.licenseNumber}}</span>
							<span class="l-2">{{detail.issueunitname}}</span>
							<span class="l-3">
								{{detail.issueDate}}
							</span>
						</div>
						<div class="certificates-right jsgc">
							<span class="r-1">{{detail.holder}}</span>
							<span class="r-2">{{detail.ydxmmc}}</span>
							<span class="r-3">{{detail.pzydjg}}</span>
							<span class="r-4">{{detail.pzydwh}}</span>
							<span class="r-5">{{detail.ydwz}}</span>
							<span class="r-6">{{detail.ydmj}}</span>
							<span class="r-7">{{detail.tdyt}}</span>
							<span class="r-8">{{detail.jsgm}}</span>
							<span class="r-9">{{detail.tdqdfs}}</span>
							<span class="r-10">
								<div class="title">
                  {{detail.ftjfjmc}}
									<!-- <a href="#" id="attach0"></a> -->
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
      detail: null,
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
    width: 420px;
    height: 581px;
    float: left;
    position: relative;
    border: 1px solid #a1a1a1;
   &.jsgc {
    // background: url('~@/assets/images/certificates/jsgcghxk_left1.png') no-repeat top left;
    background: url('~@/assets/images/certificates/jsyd_left.jpg') no-repeat top left;
    background-color: #dfdee3;
    .l-1 {
    top: 153px;
    right: 68px;
    text-align: right;
}
.l-2 {
    bottom: 109px;
    left: 231px;
    text-align: left;
}
.l-3 {
    bottom: 73px;
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
    border: 1px solid #a1a1a1;
    &.jsgc {
    //  background: url('~@/assets/images/certificates/jsgcghxk_right.png') no-repeat top left;
    background: url('~@/assets/images/certificates/jsyd_right.jpg') no-repeat top left;
    .r-1,  .r-2, .r-3,  .r-4,.r-5,.r-6,.r-7,.r-8,.r-9 {
      left: 129px;
    }
    .r-1 {
    top: 37px;
    text-align: left;
}
 .r-2 {
    top: 66px;
    text-align: left;
}
 .r-3 {
    top: 95px;
    text-align: left;
}
 .r-4 {
    top: 125px;
    text-align: left;
}
 .r-5 {
    top: 155px;
    text-align: left;
}
 .r-6 {
    top: 185px;
    text-align: left;
}
 .r-7 {
    top: 214px;
    text-align: left;
}
 .r-8 {
    top: 243px;
    text-align: left;
}
 .r-9 {
    top: 272px;
    text-align: left;
}
.r-10 {
    top: 328px;
    text-align: left;
    left: 60px;
    display: inline-block;
    width: calc(100% - 100px);
   a {
    line-height: 20px;
    // color: blue;
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