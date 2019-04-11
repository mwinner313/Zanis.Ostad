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

      <el-form-item prop="teacherMessageForAdmin" label="پیام ارسالی برای مدیر در مورد این سرفصل">
        <el-input type="textarea" v-model="form.teacherMessageForAdmin" multiline></el-input>
      </el-form-item>

      <el-card>
        <el-button @click="$refs.filePicker.click()">انتخاب فایل</el-button>
        <el-tag type="warning" v-if="form.file.name">{{form.file.name}}</el-tag>
        <el-tag type="info" v-if="!form.file.name">لطفا فایل اموزشی مربوط به این سر فصل از دوره را انتخاب کنید</el-tag>
      </el-card>

      <input type="file" accept=".pdf" @change="selectFile" ref="filePicker" style="display: none">

    </el-form>
    <span slot="footer" class="dialog-footer">
      <el-button @click="$emit('close')">بستن</el-button>
      <button class="xanis-btn" @click.prevent="submit">ثبت</button>
    </span>
  </el-dialog>
</template>

<script>
  export default {
    data() {
      return {
        form: {
          title: "",
          teacherMessageForAdmin: "",
          file: {}
        },
        uploadProgress: 0
      };
    },
    props: {
      isOpen: {
        type: Boolean
      },
      editingItem: {
        type: Object,
      }
    },
    watch:{
      editingItem(item){
        if(!item.file){
          this.form= {
            title: "",
              teacherMessageForAdmin: "",
              file: {}
          }
        }else
        this.form=Object.assign({},item);
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
          if (valid && this.form.file.name){
            this.$emit("submit", Object.assign({}, this.form));
            this.$refs.form.resetFields();
            this.form.file = "";
            this.$emit("close");
          }else
            this.$message({
              type:'warning',
              message:'لطفا ورودی ها را چک کنید'
            })
        });
      }
    }
  };
</script>

<style>
</style>
