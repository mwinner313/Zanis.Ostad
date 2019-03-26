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
      <el-form-item  :rules="[{ required: true, message: 'وضعیت الزامیست'}]" prop="state" label="انتخاب وضعیت">
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

      <el-form-item  :rules="[{ required: true, message: 'عنوان  الزامیست'}]" prop="title" label="عنوان">
        <el-input v-model="form.title"></el-input>
      </el-form-item>

      <el-form-item prop="isPreview">
        <el-checkbox v-model="form.isPreview">این مورد پیش نمایشی و رایگان میباشد</el-checkbox>
      </el-form-item>

      <el-form-item  prop="order" label="ترتیب">
        <el-input-number v-model="form.order" :min="0"></el-input-number>
      </el-form-item>

      <el-form-item prop="adminMessageForTeacher" label="پیام ارسالی برای مدرس در مورد این سرفصل">
        <el-input type="textarea" v-model="form.adminMessageForTeacher" multiline></el-input>
      </el-form-item>
      <el-button  @click="$refs.filePicker.click()">انتخاب فایل</el-button>
      <el-tag type="warning" v-if="form.file.name">{{form.file.name}}</el-tag>
      <input type="file" @change="selectFile" accept="video/.mp4,.pdf" ref="filePicker" style="display: none"/>
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
        type: Object, default: {}
      }
    },
    data() {
      return {
        form: {file: {}},
        uploadProgress: 0
      }
    },
    watch: {
      item(newVal) {
        this.form = Object.assign({}, newVal, {file: {}});
      }
    },
    methods: {

      selectFile(e) {
        this.form.file = e.target.files[0];
      },

      resetProgressState() {
        this.$emit('close');
        this.uploadProgress = 0;
      },

      submit() {
        this.$refs.form.validate((valid) => {
          if (valid) {
            if(!this.form.file.name){
              this.$message({
                message:'لطفا فایل ارسالی را انتخاب کنید',
                type:'warning'
              })
              return ;
            }
            let data = new FormData();
            for (let prop in this.form)
              data.append(prop, this.form[prop]);
            let action = this.form.id ? axios.put : axios.post;
            action('/api/courses/courseItem', data, {
              headers: {
                'Content-Type': 'multipart/form-data'
              },
              onUploadProgress: (progressEvent) => {
                this.uploadProgress = Math.floor((progressEvent.loaded * 100) / progressEvent.total);
              }
            }).then(res => {
              this.$emit('close', true);
              this.$refs.form.resetFields();
              this.uploadProgress = 0;
              this.$message({
                message: 'با موفقیت انجام شد',
                type: 'success'
              });
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
