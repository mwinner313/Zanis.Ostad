<template>
  <el-dialog
    :title="item.title"
    :visible.sync="isOpen"
    @beforeClose="resetProgressState"
    append-to-body
    width="40%"
    @closed="resetProgressState"
  >
    <el-progress v-show="uploadProgress" :percentage="uploadProgress"></el-progress>
    <el-form ref="form" :model="form">
      <el-form-item
        prop="title"
        :rules="[{ required: true, message: 'عنوان الزامی می باشد'}]"
        label="عنوان"
      >
        <el-input v-model="form.title"></el-input>
      </el-form-item>

      <el-form-item prop="isPreview">
        <el-checkbox v-model="form.isPreview">این مورد پیش نمایشی و رایگان میباشد</el-checkbox>
      </el-form-item>

      <el-form-item
        prop="order"
        :rules="[{ required: true, message: 'ترتیب الزامی می باشد'}]"
        label="ترتیب"
      >
        <el-input-number v-model.number="form.order" :min="0"></el-input-number>
      </el-form-item>

      <el-form-item prop="adminMessageForTeacher" label="پیام ارسالی برای مدیر در مورد این سرفصل">
        <el-input type="textarea" v-model="form.TeacherMessageForAdmin" multiline></el-input>
      </el-form-item>
      <el-button @click="$refs.filePicker.click()">انتخاب فایل</el-button>
      <el-tag type="warning" v-if="form.file.name">{{form.file.name}}</el-tag>
      <input
        type="file"
        accept="video/mp4, .pdf"
        @change="selectFile"
        ref="filePicker"
        style="display: none"
      >
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
      type: Object,
      default(){
        return {};
      }
    }
  },
  data() {
    return {
      form: { file: {} },
      uploadProgress: 0
    };
  },
  watch: {
    item(newVal) {
      this.form = Object.assign({}, newVal, { file: {} });
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
        if (valid) {
          let data = new FormData();
          for (let prop in this.form) data.append(prop, this.form[prop]);
          let action = this.form.id ? axios.put : axios.post;
          action("/api/TeacherAccount/courses/courseItems", data, {
            headers: {
              "Content-Type": "multipart/form-data"
            },
            onUploadProgress: progressEvent => {
              this.uploadProgress = Math.floor(
                (progressEvent.loaded * 100) / progressEvent.total
              );
            }
          }).then(res => {
            this.$emit("close", true);
            this.uploadProgress = 0;
            this.$message({
              message: "با موفقیت انجام شد",
              type: "success"
            });
          });
        }
      });
    }
  }
};
</script>

<style scoped>
.el-select {
  width: 100%;
}
</style>
