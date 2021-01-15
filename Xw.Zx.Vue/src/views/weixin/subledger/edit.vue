

<template>
  <section>
    <!--TODO:删减编辑界面数据-->
    <el-dialog
      :title="id==0 ? '添加':'编辑'"
      :visible.sync="dialogVisible"
      :close-on-click-modal="false"
      @close="cancelSubmit"
    >
      <el-form :model="editForm" label-width="80px" :rules="editFormRules" ref="editForm">
            <el-row>
        <el-col :span="12">
           <el-tooltip class="item" effect="dark" content="TODO" placement="top-start">
          <el-form-item label="transactionID" prop="transactionID">
            <el-input v-model="editForm.transactionID"></el-input>
            </el-form-item>
            </el-tooltip>
         </el-col>
        </el-row>
        <el-row>
        <el-col :span="12">
           <el-tooltip class="item" effect="dark" content="TODO" placement="top-start">
          <el-form-item label="out_Order_No" prop="out_Order_No">
            <el-input v-model="editForm.out_Order_No"></el-input>
            </el-form-item>
            </el-tooltip>
         </el-col>
        </el-row>
        <el-row>
        <el-col :span="12">
           <el-tooltip class="item" effect="dark" content="TODO" placement="top-start">
          <el-form-item label="amount" prop="amount">
            <el-input v-model="editForm.amount"></el-input>            </el-form-item>
            </el-tooltip>
         </el-col>
        </el-row>
        <el-row>
        <el-col :span="12">
           <el-tooltip class="item" effect="dark" content="TODO" placement="top-start">
          <el-form-item label="subCharge" prop="subCharge">
            <el-input v-model="editForm.subCharge"></el-input>            </el-form-item>
            </el-tooltip>
         </el-col>
        </el-row>
        <el-row>
        <el-col :span="12">
           <el-tooltip class="item" effect="dark" content="TODO" placement="top-start">
          <el-form-item label="tranTime" prop="tranTime">
           <el-date-picker v-model="editForm.tranTime" type="datetime" placeholder="时间" align="right" :picker-options="glpickerOptions"></el-date-picker>
            </el-form-item>
            </el-tooltip>
         </el-col>
        </el-row>
        <el-row>
        <el-col :span="12">
           <el-tooltip class="item" effect="dark" content="TODO" placement="top-start">
          <el-form-item label="payState" prop="payState">
            <el-input v-model="editForm.payState"></el-input>
            </el-form-item>
            </el-tooltip>
         </el-col>
        </el-row>
        <el-row>
        <el-col :span="12">
           <el-tooltip class="item" effect="dark" content="TODO" placement="top-start">
          <el-form-item label="subState" prop="subState">
            <el-input v-model="editForm.subState"></el-input>
            </el-form-item>
            </el-tooltip>
         </el-col>
        </el-row>
        <el-row>
        <el-col :span="12">
           <el-tooltip class="item" effect="dark" content="TODO" placement="top-start">
          <el-form-item label="payDescription" prop="payDescription">
            <el-input v-model="editForm.payDescription"></el-input>
            </el-form-item>
            </el-tooltip>
         </el-col>
        </el-row>
      </el-form>
      <div slot="footer" class="dialog-footer">
        <el-button @click ="cancelSubmit">取消</el-button>
        <el-button type="primary" @click="editSubmit">提交</el-button>
      </div>
    </el-dialog>
  </section>
</template>

<script>
import api from "@/api/app";
import { type } from "os";

export default {
  name: "WechatOrdersEdit",
  components: {
  },
  props: {
    value: Boolean,
    id: Number,
  },
  watch: {
   value: {
      handler(val) {
        this.dialogVisible = val;
        if (this.id > 0) {
          this.initEdit();
        } else {
          this.initAdd();
        }
      },
    },
  },
  data() {
    return {   
      dialogVisible: false,
      editLoading: false,
      editFormRules: {
         transactionID: [{ required: true, message: "不可为空", trigger: "blur" }],
         out_Order_No: [{ required: true, message: "不可为空", trigger: "blur" }],
         amount: [{ required: true, message: "不可为空", trigger: "blur" }],
             subCharge: [{ required: true, message: "不可为空", trigger: "blur" }],
            tranTime: [ { required: true, message: '请选择日期', trigger: 'change' }],
         payState: [{ required: true, message: "不可为空", trigger: "blur" }],
         subState: [{ required: true, message: "不可为空", trigger: "blur" }],
         payDescription: [{ required: true, message: "不可为空", trigger: "blur" }],
        
      },
//TODO:删减编辑界面数据
      editForm: {
        transactionID: "",
        out_Order_No: "",
        amount: "",
            subCharge: "",
            tranTime: "",
        payState: "",
        subState: "",
        payDescription: "",
      },     
    };
  },
  methods: {
    initEdit() {
      api.wechatOrders
        .get(this.id)
        .then((res) => {
                this.editForm.transactionID = res.result.transactionID;
                this.editForm.out_Order_No = res.result.out_Order_No;
                this.editForm.amount = res.result.amount;
                this.editForm.subCharge = res.result.subCharge;
                this.editForm.tranTime = res.result.tranTime;
                this.editForm.payState = res.result.payState;
                this.editForm.subState = res.result.subState;
                this.editForm.payDescription = res.result.payDescription;
          
      });
    },
    initAdd() {
                    this.editForm.transactionID= "";
                        this.editForm.out_Order_No= "";
                        this.editForm.amount= "";
                            this.editForm.subCharge= "";
                            this.editForm.tranTime= "";
                        this.editForm.payState= "";
                        this.editForm.subState= "";
                        this.editForm.payDescription= "";
            },
    //提交
    editSubmit: function() {
     const handlePostSucess = () => {
        this.editLoading = false;
        this.$message({
            message: "提交成功",
            type: "success",
        });

        this.$refs["editForm"].resetFields();
        this.dialogVisible = false;
        this.$emit("input", false);
        this.$emit("change");
      };
      this.$refs.editForm.validate(valid => {
        if (valid) {
          this.$confirm("确认提交吗？", "提示", {}).then(() => {
            this.editLoading = true;
            if(this.id){
                api.wechatOrders.put(this.id, this.editForm).then(res => {
                    handlePostSucess();
                }).catch(()=>{
                    this.editLoading = false;
                });
            }else{
                api.wechatOrders.post(this.editForm).then(res => {
                    handlePostSucess();
                }).catch(()=>{
                    this.editLoading = false;
                });
            }
          });
        }
      });
    },
    cancelSubmit: function(){
      this.dialogVisible = false;
      this.$emit('input',false);
    }
  },    
  mounted() {    
  }
};
</script>

<style scoped>
</style>