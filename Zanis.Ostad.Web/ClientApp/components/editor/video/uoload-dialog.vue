<template>
  <el-dialog :visible.sync="isOpen" @closed="$emit('close')" width="30%">
    <el-progress v-show="uploadProgress" :percentage="uploadProgress"></el-progress>
    <el-form ref="form" :model="form">
      <el-button @click="$refs.filePicker.click()">انتخاب فایل</el-button>
      <el-tag type="warning" v-if="formFile.name">{{formFile.name}}</el-tag>
      <input type="file" accept=".zip" @change="selectFile" ref="filePicker" style="display: none">
      <el-button type="submit" @click="submit">ثبت</el-button>
    </el-form>
  </el-dialog>
</template>

<script>
import axios from "axios";
export default {
  name: "upload",

  data() {
    return {
      formFile: {},
      uploadProgress: 0
    };
  },
  props: {
    isOpen: {
      type: Boolean
    },
    itemId: {
      type: Number
    }
  },
  methods: {
    selectFile(e) {
      this.formFile = e.target.files[0];
    },
    resetProgressState() {
      this.$emit("close");
      this.uploadProgress = 0;
    },
    submit() {
      let data = new FormData();
      data.append("EditAssignmnetId", this.itemId);
      data.append("File", this.formFile);
      axios
        .post("/api/EditorAccount", data, {
          headers: {
            "Content-Type": "multipart/form-data"
          },
          onUploadProgress: progressEvent => {
            this.uploadProgress = Math.floor(
              (progressEvent.loaded * 100) / progressEvent.total
            );
          }
        })
        .then(res => {
          if (res.data.status == 1) {
            this.$$emit("close");
            this.$message({
              message: "فایل شما با موفقیت ارسال شد",
              type: "success"
            });
          }
        });
    }
  },
  
};
</script>

<style>
</style>
