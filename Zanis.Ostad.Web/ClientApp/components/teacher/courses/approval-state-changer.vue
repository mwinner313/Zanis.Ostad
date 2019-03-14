<template>
  <el-dialog :visible.sync="isOpen" width="40%" @closed="$emit('close')" :title="item.title">
  
    <el-form ref="form" :model="form">
  
      <el-form-item prop="categoryId" label="انتخاب وضعیت">
  
        <el-select v-model="form.selectedStatus" placeholder="انتخاب وضعیت" width="100%">
  
          <el-option label="تایید" :value="5"></el-option>
  
          <el-option label="رد" :value="10"></el-option>
  
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
  import axios from "axios";
  
  export default {
  
    name: "",
  
    props: {
  
      isOpen: {
  
        type: Boolean
  
      },
  
      item: {
  
        type: Object
  
      }
  
    },
  
    data() {
  
      return {
  
        form: {
  
          selectedStatus: 5
  
        }
  
      };
  
    },
  
    methods: {
  
      submit() {
  
        if (this.$refs.form.model.selectedStatus == 5) {
  
          axios
  
            .patch("/api/TeacherAccount/courses/" + this.item.id + "/active")
  
            .then(res => {
  
              this.$message({
  
                message: "برای تغییر وضعیت این مورد ابتدا محتوا میبایست توسط مدیر سیستم تعیین وضعیت شود",
  
                type: "success"
  
              });
  
              this.$emit('close');
  
            });
  
        }
  
        if (this.$refs.form.model.selectedStatus == 10) {
  
          axios
  
            .patch("/api/TeacherAccount/courses/" + this.item.id + "/deactive")
  
            .then(res => {
  
              this.$message({
  
                message: "برای تغییر وضعیت این مورد ابتدا محتوا میبایست توسط مدیر سیستم تعیین وضعیت شود",
  
                type: "warning"
  
              });
  
              this.$emit('close');
  
            });
  
        }
  
      }
  
    }
  
  };
</script>

<style scoped>
  .el-select {
  
    width: 100%;
  
  }
</style>
