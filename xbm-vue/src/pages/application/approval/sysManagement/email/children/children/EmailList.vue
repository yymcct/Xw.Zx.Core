<template>
				<div class="email-content" v-if="emailList.length>0">
					<dl class="email-list " data-email-group="lastmore" id="list-email">
						<dd v-for="(item,idx) in emailList" @click="checkDetail(item,idx)" :title="item.ZT==0?'未读':'已读'" :key="idx" :class="activeIdx==idx?'active':''">
							<div class="mail-list-time">{{item.RQ}}</div>
							<h3 class="mail-list-user" :title="item.NAME" v-if="inBoxShow">{{item.NAME}}</h3>
              <h3 class="mail-list-user" :title="item.MX_CEPTER" v-if="otherShow">{{item.MX_CEPTER}}</h3>
							<p class="mail-list-title">{{item.BT}} </p>
							<div class="mail-list-flag">
								<img src="@/assets/images/email-1.png" v-if="item.ZT==0" width="16" height="16" alt="" title="未读" align="absmiddle">
								<img src="@/assets/images/readed.png" v-else width="16" height="16" alt="" title="已读" align="absmiddle">
							</div>
							<div class="mail-list-sign">
								<img src="@/assets/images/whitestar.png" width="16" height="16" alt="无" title="无" align="absmiddle">
							</div>
						</dd>
					</dl>
				</div>
</template>
<script>
export default {
  name: "EmailList",
  props: ["emailList",'type'],
  data() {
    return {
      list: [],
      activeIdx: null,
      otherShow:true,
      inBoxShow:false,

    };
  },
  created: function() {this.showType()},

  methods: {
    showType(){
      if(this.type=='inBox'){
        this.inBoxShow=true;
        this.otherShow=false;
      }
    },
    checkDetail: function(row, idx) {
      this.activeIdx = idx;
      this.$emit("checkDetail", row);
    }
  }
};
</script>
<style lang="scss" scoped>
.email-content {
  height: calc(100% - 40px);
  .email-list {
    height: 100%;
    overflow: auto;
    > dd {
      color: #555;
      position: relative;
      cursor: pointer;
      overflow: hidden;
      transition: padding 0.3s ease-in-out;
      &.active {
        background: #688ad0;
        border: 0;
        color: #fff !important;
        border-bottom: 1px solid #ddd;
        &:hover {
          background: #688ad0;
        }
      }
      &:hover {
        background: #b1cff4;
      }
    }
    > dd,
    > dl {
      line-height: 25px;
      margin: 0;
      padding: 3px 10px 3px 35px;
      border-bottom: 1px solid #ddd;
    }
    .mail-list-time {
      font-size: 12px;
      line-height: 32px;
      float: right;
    }
    .mail-list-user {
      font-size: 12px;
      line-height: 14px;
      height: 14px;
      overflow: hidden;
      margin-right: 20px;
      margin: 10px 20px 10px 0px;
      white-space: nowrap;
    }
    .mail-list-title {
      font-size: 12px;
      margin: 0;
      height: 25px;
      line-height: 25px;
      overflow: hidden;
      margin-right: 0px;
      white-space: nowrap;
    }
    .mail-list-flag {
      width: 16px;
      height: 16px;
      display: inline-block;
      position: absolute;
      left: 10px;
      top: 10px;
      line-height: 14px;
      transition: left 0.3s ease-in-out;
    }
    .mail-list-sign {
      width: 16px;
      height: 16px;
      display: inline-block;
      position: absolute;
      left: 10px;
      bottom: 8px;
      line-height: 14px;
      transition: left 0.3s ease-in-out;
    }
  }
}
.email-readmore {
  height: 40px;
  text-align: center;
  .el-pagination {
    padding-top: 10px;
  }
}
</style>
