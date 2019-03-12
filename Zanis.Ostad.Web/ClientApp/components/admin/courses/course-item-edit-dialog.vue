<template>
  <el-dialog
    :title="item.title"
    :visible.sync="isOpen"
    @beforeClose="$emit('close')"
    append-to-body
    width="40%"
    @closed="$emit('close')"
    :before-close="isOpen">
    <el-form ref="form" :model="form">
      <el-form-item prop="state" label="انتخاب وضعیت">
        <el-select v-model="form.state" placeholder="انتخاب وضعیت" width="100%">
          <el-option
            label="تایید"
            :value="5">
          </el-option>

          <el-option
            label="رد"
            :value="10">
          </el-option>
          <el-option
            label="در انتظار تعیین وضعیت"
            disabled
            :value="0">
          </el-option>
          <el-option
            disabled
            label="رد شده توسط استاد"
            :value="15">
          </el-option>
        </el-select>
      </el-form-item>

      <el-form-item prop="title" label="عنوان">
        <el-input v-model="form.title"></el-input>
      </el-form-item>

      <el-form-item prop="isPreview">
        <el-checkbox v-model="form.isPreview">این مورد پیش نمایشی و رایگان میباشد</el-checkbox>
      </el-form-item>

      <el-form-item prop="order" label="ترتیب">
        <el-input-number v-model="form.order"  :min="1" ></el-input-number>
      </el-form-item>

      <el-form-item prop="adminMessageForTeacher" label="پیام ارسالی برای مدرس در مورد این سرفصل">
        <el-input type="textarea" v-model="form.adminMessageForTeacher" multiline></el-input>
      </el-form-item>
    <input type="file" ref=""/>
    </el-form>
    <span slot="footer" class="dialog-footer">
    <el-button @click="$emit('close')">بستن</el-button>
    <el-button type="primary" @click="submit">ثبت</el-button>
  </span>
  </el-dialog>
</template>

<script>
  import axios from 'axios'

  export default {
    name: "",
    props: {
      isOpen: {
        type: Boolean
      },
      item: {
        type: Object,default:{}
      }
    },
    data() {
      return {
        form: {}
      }
    },
    watch: {
      item(newVal) {
        this.form = Object.assign({}, newVal);
      }
    },
    methods: {
      submit() {
        this.$refs.form.validate((valid) => {
          if (valid) {
            axios.put('/api/courses/courseItem', this.form).then(res => {
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
    width: 100%;
  }
</style>
