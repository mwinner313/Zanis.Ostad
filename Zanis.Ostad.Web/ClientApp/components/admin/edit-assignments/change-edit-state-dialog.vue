<template>
  <el-dialog
    :title="item.title"
    :visible.sync="isOpen"
    width="40%"
    @closed="$emit('close')"
   >
    <el-form ref="form" :model="form">
      <el-form-item prop="categoryId" label="انتخاب وضعیت">
        <el-select v-model="form.status" placeholder="انتخاب وضعیت" width="100%">
          <el-option
            label="تایید"
            :value="2">
          </el-option>

          <el-option
            label="رد"
            :value="3">
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
  import EventBus from './../../../event-bus'
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
          status: 2
        }
      }
    },
    methods:{
      submit(){
        console.log(this.item)
        this.$refs.form.validate((valid) => {
          if (valid) {
            axios.patch('\n' +
              '/api/EditAssignment/change_state', {editId:this.item.id,status:this.form.status}).then(res => {
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
