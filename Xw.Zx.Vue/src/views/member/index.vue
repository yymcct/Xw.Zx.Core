
<template>
  <section>
    <!--TODO:配置查询条件-->
    <el-col :span="24" class="toolbar" style="padding-bottom: 0px">
      <el-form :inline="true" :model="filters">
        <el-form-item>
          <el-select v-model="filters.vipType" placeholder="请选择">
            <el-option label="全部类型" value="999"></el-option>
            <el-option label="客户" value="0"></el-option>
            <el-option label="业务经理" value="10"></el-option>
            <el-option label="运营中心" value="20"></el-option>
            <el-option label="大区经理" value="30"></el-option>
            <el-option label="分公司" value="40"></el-option>
          </el-select>
        </el-form-item>
        <el-form-item>
          <el-input
            class="keyword"
            v-model="filters.keywords"
            placeholder="角色,姓名,手机号,备注,会员类型"
          ></el-input>
        </el-form-item>
        <el-form-item>
          <el-date-picker
            v-model="filters.addTimeStart"
            type="date"
            placeholder="开始时间"
            align="right"
            :picker-options="glpickerOptions"
            value-format="yyyy-MM-dd"
          ></el-date-picker>
          <el-date-picker
            v-model="filters.addTimeEnd"
            type="date"
            placeholder="结束时间"
            align="right"
            :picker-options="glpickerOptions"
            value-format="yyyy-MM-dd"
          ></el-date-picker>
        </el-form-item>
        <el-form-item>
          <el-button type="primary" @click="getMemberMDtos">查询</el-button>
        </el-form-item>
      </el-form>
    </el-col>

    <!--列表-->
    <el-table
      :data="memberMDtos"
      highlight-current-row
      v-loading="listLoading"
      style="width: 100%"
    >
      <el-table-column
        prop="id"
        label="Id"
        width="80px"
        sortable
      ></el-table-column>
      <el-table-column
        prop="roleName"
        label="角色"
        width="100px"
        sortable
      ></el-table-column>
      <el-table-column prop="realName" label="姓名" width="140px" sortable>
        <template slot-scope="scope">
          <p style="font-weight: bold">{{ scope.row.realName }}</p>
          <p style="color: #999999; font-weight: bold">
            {{ scope.row.phone }}
          </p>
          <p>
            <el-link type="primary" @click="showParentTree(scope.row)"
              >查看团队树</el-link
            >
          </p>
        </template>
      </el-table-column>
      <!-- <el-table-column
        prop="phone"
        label="手机"
        width="120px"
        sortable
      ></el-table-column> -->
      <el-table-column
        prop="memberVipTypeName"
        label="级别"
        width="120px"
        sortable
      ></el-table-column>
      <el-table-column prop="inviteName" label="上级" width="120px" sortable>
        <template slot-scope="scope">
          <p style="font-weight: bold">{{ scope.row.inviteName }}</p>
          <p style="color: #999999; font-weight: bold">
            {{ scope.row.invitePhone }}
          </p>
          <p>
            <el-link type="primary" @click="showChangeInvite(scope.row)"
              >更改上级</el-link
            >
          </p>
        </template>
      </el-table-column>
      <!-- <el-table-column prop="invitePhone" label="推荐人电话" width="150px" sortable></el-table-column> -->
      <el-table-column
        prop="aliPayAccount"
        label="支付宝"
        width="150px"
        sortable
      ></el-table-column>
      <el-table-column prop="remark" label="备注" sortable></el-table-column>
      <el-table-column
        prop="createDate"
        label="添加时间"
        width="100px"
        sortable
      ></el-table-column>
      <el-table-column label="操作" width="140px">
        <template scope="scope">
          <el-dropdown @command="handleDropdownCommand">
            <span class="el-dropdown-link">
              更多操作<i class="el-icon-arrow-down el-icon--right"></i>
            </span>
            <el-dropdown-menu slot="dropdown">
              <el-dropdown-item :command="composeValue('edit', scope.row)"
                >编辑</el-dropdown-item
              >
              <el-dropdown-item :command="composeValue('giveCoupon', scope.row)"
                >发放优惠券</el-dropdown-item
              >
            </el-dropdown-menu>
          </el-dropdown>
          <!-- <i
            class="el-icon-edit"
            style="margin: 0 5px; font-weight: bold; cursor: pointer"
            @click="showEdit(scope.row)"
          ></i> -->
          <!-- <i
            class="el-icon-delete"
            style="margin: 0 5px; font-weight:bold;cursor: pointer;"
            @click="handleDel(scope.$index, scope.row)"
          ></i> -->
        </template>
      </el-table-column>
    </el-table>

    <!--工具条align='center'-->
    <el-col :span="24" class="toolbar" align="right">
      <el-pagination
        @size-change="handleSizeChange"
        @current-change="handleCurrentChange"
        :current-page="requestParams.page"
        :page-sizes="[10, 50, 100, 500]"
        :page-size="requestParams.pageSize"
        layout="total, sizes, prev, pager, next, jumper"
        :total="total"
        background
      ></el-pagination>
    </el-col>

    <edit-member
      v-model="edit.show"
      :memberId="edit.memberId"
      @change="getMemberMDtos()"
    />
    <update-vip
      :action="updateVipAction"
      :member="updateMember"
      @change="updateVipChange"
    ></update-vip>
    <chage-invite
      v-model="changInvite.show"
      :memberId="changInvite.memberId"
      @change="changInviteHandle"
    />

    <parent-tree v-model="parentTree.show" :memberId="parentTree.memberId" />

    <give-coupon v-model="giveCoupon.show" :memberId="giveCoupon.memberId" />
  </section>
</template>

<script>
import { api_getMemberMDtos } from "../../api/api";
import editMember from "./editMember";
import chageInvite from "./chageInvite";
import UpdateVip from "./updateVip";
import parentTree from "@/components/parentTree";
import giveCoupon from "./giveCoupon";
export default {
  components: {
    editMember,
    UpdateVip,
    chageInvite,
    parentTree,
    giveCoupon,
  },
  data() {
    return {
      requestParams: {
        page: 1,
        pageSize: 10,
        filters: "",
        sorts: "-id",
      },

      filters: {
        keywords: null,
        addTimeStart: null,
        addTimeEnd: null,
        vipType: "999",
      },
      memberMDtos: [],
      total: 0,
      listLoading: false,

      editForm: null,
      editAction: "none",
      updateMember: null,
      updateVipAction: "none",
      edit: {
        show: false,
        memberId: 0,
      },
      changInvite: {
        show: false,
        memberId: 0,
      },
      parentTree: {
        show: false,
        memberId: 0,
      },
      giveCoupon: {
        show: false,
        memberId: 0,
      },
    };
  },
  methods: {
    handleSizeChange(val) {
      this.requestParams.pageSize = val;
      this.getMemberMDtos();
    },
    handleCurrentChange(val) {
      this.requestParams.page = val;
      this.getMemberMDtos();
    },
    getMemberMDtos() {
      this.listLoading = true;
      this.page = 1;
      this.requestParams.filters = "";

      if (this.filters.vipType !== "999")
        this.requestParams.filters += `MemberVipType==${this.filters.vipType},`;

      if (this.filters.keywords)
        this.requestParams.filters += `(RoleName|Phone|Remark|RealName)@=${this.filters.keywords},`;

      if (this.filters.addTimeStart)
        this.requestParams.filters += `CreateDate>=${this.filters.addTimeStart},`;
      if (this.filters.addTimeEnd)
        this.requestParams.filters += `CreateDate<=${this.filters.addTimeEnd},`;

      api_getMemberMDtos(this.requestParams).then((respone) => {
        this.listLoading = false;
        this.memberMDtos = respone.result;
        this.total = respone.total;
      });
    },

    //显示新增界面
    handleAdd: function () {
      this.editAction = "add";
    },
    handleUpdateVip(index, row) {
      this.updateMember = Object.assign({}, row);
      this.updateVipAction = "edit";
    },

    updateVipChange(cancel) {
      this.updateVipAction = "none";
      if (cancel != "cancel") {
        this.getMemberMDtos();
      }
    },
    showChangeInvite(row) {
      this.changInvite.memberId = row.id;
      this.changInvite.show = true;
    },
    changInviteHandle() {
      this.getMemberMDtos();
    },
    showParentTree(row) {
      this.parentTree.memberId = row.id;
      this.parentTree.show = true;
    },
    composeValue(item, data) {
      return {
        button: item,
        data: data,
      };
    },
    handleDropdownCommand(command) {
      const _this = this;
      const showEdit = (row) => {
        _this.edit.memberId = row.id;
        _this.edit.show = true;
      };
      const showGiveCoupon = (row) => {
        _this.giveCoupon.memberId = row.id;
        _this.giveCoupon.show = true;
      };
      if (command.button == "edit") {
        showEdit(command.data);
      }
      if (command.button == "giveCoupon") {
        showGiveCoupon(command.data);
      }
    },
  },

  mounted() {
    this.getMemberMDtos();
  },
};
</script>

<style scoped>
.keyword {
  width: 400px;
}
p {
  padding: 0px;
  margin: 0px;
}
</style>