<template>
	<div class="email-detail">
		<div class="handle-btn">
		 <el-button type="success" icon="el-icon-message" circle title="回复" v-if="!isOutBox" @click="replyEmail"></el-button>
		 <el-button type="success" class="fa fa-share" circle title="转发" v-if="!isOutBox" @click="forwardEmail"></el-button>
		</div>
		<div class="em-detail-top">
			<p class="em-det-p"><span>主 &nbsp;&nbsp;题:</span><b>{{detail.MX_THEME}}</b></p>
			<p class="em-det-p"><span>发件人:</span>
			<b title="发件人">{{detail.MX_SENDER}}</b>
			</p>
			<p class="em-det-p"><span>收件人:</span>
			<u :title="item.ZT==0?'未读':'已读'" :style="item.ZT==0?'color:red':''" v-for="(item,idx) in detail.DATE" :key="idx">{{item.NAME}}<i v-show="detail.DATE.length!==idx+1">,</i></u>
			</p>
			<p class="em-det-p"><span>时 &nbsp;&nbsp;间:</span>{{detail.MX_XTIME}}</p>
			<p class="em-det-p" style="padding:5px 0px 10px"><span style="vertical-align: top;">附 &nbsp;&nbsp;件:</span>
      <ul class="el-upload-list el-upload-list1 el-upload-list--text" style="display:inline-block;width:calc(100% - 70px)" v-if="detail.FILE.length">
          <li class="el-upload-list__item is-success"
                    v-for="(item,num) in detail.FILE"
                    :key="num">
                    <a
                      class="el-upload-list__item-name" style="display:inline;"
                      :href="'/jz/XBM_Service.bsp?GetDoc&Source='+item.AC_NAME" 
                      target="_blank" :download="item.SR_NAME">
                      <i class="el-icon-document"></i>
                      {{item.SR_NAME}}
                    </a>
                      <!-- <a class="file-download" :href="'/dghy/XBM_Service.bsp?GetDoc&Source='+item.AC_IDENT" :download="item.SR_NAME">下载</a> -->
                  </li>
                </ul></p>
        <div class="em-det-p"><span style="display: inline-block;vertical-align: top;">正 &nbsp;&nbsp;文:</span>	<div class="em-detail-content" v-html="detail.MX_CONTENT">
         </div></div>
		</div>
	</div>
</template>
<script>
import reply from "./EmailAdd";
export default {
  name: "email-detail",
  // props: ["detail"],
   props: {
    isOutBox: {
      type: Boolean,
      default: false
    },
    detail: {
      type: Object,
      default: null
    }
  },
  data: function() {
    return {
			data: null,
    };
  },

  created: function() {
    // console.log(this.detail, "555");
  },
  methods: {
		replyEmail:function(){
			this.$emit('reply')
		},
		forwardEmail:function(){
			this.$emit('forWard')
		}
	},
	components:{
		reply
	}
};
</script>
<style lang="scss" scoped>
.email-detail {
  position: relative;
  padding: 10px;
  background: #fff;
  height: 100%;
  border: 1px solid #ebebeb;
  border-radius: 3px;
  box-shadow: 1px 1px 1px #f1f1f1;
  .handle-btn {
    position: absolute;
    right: 20px;
    top: 20px;
  }
  .em-detail-top {
    // border-bottom: 1px solid #dbd6d6;
    padding: 10px;
    .em-det-p {
      line-height: 1.5;
      font-size: 13px;
      padding-bottom: 3px;
      > span {
        padding-right: 10px;
      }
      > b {
        font-weight: bolder;
      }
      > u {
        cursor: pointer;
        color: #999;
        margin-right: 2px;
        > i {
          padding-left: 1px;
        }
      }
      /deep/ .el-upload-list__item{
        &:first-child{
          margin-top:0px;
        }
      }
    }
  }
  .em-detail-content {
        font-size: 14px;
        padding: 10px;
        overflow: auto;
        display: inline-block;
        min-height: 250px;
        width: calc(100% - 70px);
        border: 1px solid #eaeaea;
        background: #fdfdfd;
  }
}
</style>