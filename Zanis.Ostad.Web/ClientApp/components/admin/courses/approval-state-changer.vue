<template>
  <el-dialog
    :title="item.title"
    :visible.sync="isOpen"
    width="40%"
    @closed="$emit('close')"
    :before-close="isOpen">
    <el-form ref="form" :model="form">
      <el-form-item prop="categoryId" label="انتخاب وضعیت">
        <el-select v-model="form.selectedStatus" placeholder="انتخاب وضعیت" width="100%">
          <el-option
            label="تایید"
            :value="5">
          </el-option>

          <el-option
            label="رد"
            :value="10">
          </el-option>
        </el-select>
      </el-form-item>
    </el-form>
    <span slot="footer" class="dialog-footer">
    <el-button @click="$emit('close')">بستن</el-button>
    <el-button type="primary" @click="submit">ثبت</el-button>
  </span>
  </el-dialog>
</template>

<script>
  import axios from  'axios'
  export default {
    name: "",
    props: {
      isOpen: {
        type: Boolean
      },
      item:{
        type:Object
      }
    },
    data() {
      return {
        form: {
          selectedStatus: 5
        }
      }
    },
    methods:{
      submit(){
        this.$refs.form.validate((valid) => {
          if (valid) {
            axios.patch('/api/courses/change_approval_status', {courseApprovalStatus:this.form.selectedStatus,courseId:this.item.id}).then(res => {
              this.$emit('close');
            });
          }
        });
      }
    }
  }
</script>

<style scoped>
  .el-select {
   width:100%;
  }
</style>
