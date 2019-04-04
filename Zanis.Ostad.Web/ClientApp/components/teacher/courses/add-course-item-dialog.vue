<template>
  <el-dialog
    title="افزودن سرفصل"
    :visible.sync="isOpen"
    @closed="$emit('close')"
    append-to-body
    width="40%"
  >
    <el-progress v-show="uploadProgress" :percentage="uploadProgress"></el-progress>
    <el-form ref="form" :model="form">
      <el-form-item prop="title" label="عنوان">
        <el-input v-model="form.title"></el-input>
      </el-form-item>

      <el-form-item prop="TeacherMessageForAdmin" label="پیام ارسالی برای مدیر در مورد این سرفصل">
        <el-input type="textarea" v-model="form.TeacherMessageForAdmin" multiline></el-input>
      </el-form-item>
      <el-button @click="$refs.filePicker.click()">انتخاب فایل</el-button>
      <el-tag type="warning" v-if="form.file.name">{{form.file.name}}</el-tag>
      <input type="file" accept=".pdf" @change="selectFile" ref="filePicker" style="display: none">
    </el-form>
    <span v-for="i in isEdited">{{i}}</span>
    <span slot="footer" class="dialog-footer">
      <el-button @click="$emit('close')">بستن</el-button>
      <el-button type="primary" @click="submit">ثبت</el-button>
    </span>
  </el-dialog>
</template>

<script>
export default {
  data() {
    return {
      form: {
        title: "",
        TeacherMessageForAdmin: "",
        file: {}
      },      
      uploadProgress: 0
    };
  },
  props: {
    isOpen: {
      type: Boolean
    },
    isEdited:{
      type:Object,
    }
  },
  methods: {
    selectFile(e) {
      this.form.file = e.target.files[0];
    },
    resetProgressState() {
      this.$emit("close");
      this.uploadProgress = 0;
    },
    submit() {
        this.$refs.form.validate(valid => {
        this.$emit("submit",Object.assign({},this.form));
        this.$refs.form.resetFields();
        this.form.file = "";
        this.$emit("close");
      });
    }
  }
};
</script>

<style>
</style>
