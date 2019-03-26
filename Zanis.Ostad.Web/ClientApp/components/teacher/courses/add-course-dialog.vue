<template>
  <div>
    <el-dialog title="افزودن دوره" :visible.sync="isOpen" width="80%" @closed="$emit('close')">
      <el-row>
        <el-col :md="12">
          <el-card shadow="always">
            <el-form ref="form" :model="form">
              <el-progress v-show="uploadProgress" :percentage="uploadProgress"></el-progress>

              <el-form-item
                label="قیمت"
                prop="price"
                :rules="[
  
                  { required: true, message: 'وارد کردن قیمت الزامی می باشد'},
  
                  { type: 'number', message: 'فیمت باید عددی باشد'}
  
                ]"
              >
                <el-input type="text" placeholder="قیمت" v-model.number="form.price"></el-input>
              </el-form-item>

              <el-form-item
                prop="description"
                :rules="[
  
                  { required: true, message: 'وارد کردن توضیحات الزامی می باشد'}]"
                label="توضیحات"
              >
                <el-input placeholder="توضیحات" v-model="form.description"></el-input>
              </el-form-item>

              <el-form-item label="پیام به مدیریت">
                <el-input type="textarea" v-model="form.teacherMessage"></el-input>
              </el-form-item>

              <el-form-item
                prop="courseTitle"
                :rules="[{ required: true, message: 'انتخاب کردن عنوان الزامی می باشد'}]"
                label="عنوان"
              >
                <el-select v-model="form.courseTitle" placeholder="عنوان" class="w100">
                  <el-option
                    v-for="item in courseTitles"
                    :key="item.value"
                    :label="item.name"
                    :value="item.id"
                  ></el-option>
                </el-select>
              </el-form-item>

              <el-form-item>
                <el-button type="warning" class="w100" @click="isLessonsearchDialog = true">انتخاب درس</el-button>
              </el-form-item>

              <el-form-item>
                <el-tag
                  class="w100"
                  type="danger"
                  v-if="itemSelectedLesson==''"
                >در حال حاظر درسی را انتخاب نکرده اید</el-tag>

                <el-tag class="w100" v-else>{{itemSelectedLesson}}</el-tag>
              </el-form-item>

              <el-form-item>
                <input
                  type="file"
                  name="myfile"
                  accept=".zip"
                  @change="processFile"
                  ref="filePicker"
                  style="display: none">

                <el-button type="info" @click="$refs.filePicker.click()">انتخاب فایل</el-button>
                <el-tag type="warning" v-if="form.zipFile">{{form.zipFile.name}}</el-tag>
                <br>
                <el-button type="primary" class="sendBtn w100" size="medium" @click="registerLesson">ارسال</el-button>
              </el-form-item>
            </el-form>
          </el-card>
        </el-col>

        <el-col :md="12">
          <el-alert
            style="margin-right:10px"
            title="توضیحات"
            type="info"
            description="در این بخش توضیحات قرار می گیرد"
            :closable="false"
            show-icon
          ></el-alert>
        </el-col>
      </el-row>
    </el-dialog>

    <!-- searchLessonItem -->
    <searchLesson
      :isOpen="isLessonsearchDialog"
      @close="selectedLesson=undefined"
      v-on:lessonSelected="getItems($event)"
      v-on:close="closeSearchgDialog"
    ></searchLesson>
  </div>
</template>
<script>
import axios from "axios";

import searchLesson from "./search-lesson";

export default {
  name: "add-course-dialog",

  props: {
    isOpen: {
      type: Boolean
    }
  },

  components: {
    searchLesson
  },

  data() {
    return {
      isLessonsearchDialog: false,

      selectedTeacherItem: "",

      itemSelectedLesson: "",

      selectedLesson: null,

      close: false,

      uploadProgress: 0,

      courseTitles: [],

      form: {
        price: 0,

        description: "",

        courseTitle: "",

        teacherMessage: "",

        lessonFieldId: 0,

        zipFile: ""
      }
    };
  },

  methods: {
    getCourseTitle() {
      axios.get("/api/courseTitles").then(res => {
        this.courseTitles = res.data;
      });
    },

    getItems(item) {
      this.itemSelectedLesson =
        item.gradeName + " - " + item.fieldName + " - " + item.lessonName;

      this.form.lessonFieldId = item.id;

      console.log(item);
    },

    closeSearchgDialog() {
      this.isLessonsearchDialog = false;
    },

    processFile(event) {
      this.form.zipFile = event.target.files[0];
    },

    registerLesson() {
      // validationArea

      this.$refs.form.validate(valid => {
        if (valid) {
          // validation file
          if (!this.form.zipFile) {
            this.$message({
              message: "لطفا فایل ارسالی را انتخاب کنید",
              type: "warning"
            });
            return;
          }
          let data = new FormData();

          data.append("Price", this.form.price);

          data.append("Description", this.form.description);

          data.append("TeacherMessageForAdmin", this.form.teacherMessage);

          data.append("CourseTitleId", this.form.courseTitle);

          data.append("LessonFieldId", this.form.lessonFieldId);

          data.append("ZipFile", this.form.zipFile);

          axios

            .post("/api/TeacherAccount/courses", data, {
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
                this.$message({
                  message: "دوره شما با موفقیت ثبت شد",

                  type: "success"
                });

                this.$emit("close");
              }
            });
        }
      });
    }
  },

  mounted() {
    this.getCourseTitle();
  }
};
</script>
<style scoped>
.w100 {
  width: 100%;
}
.sendBtn {
  margin-left: 10px;
  color: #fff !important;
}
.el-upload,
.el-upload-dragger {
  width: 100%;
}

.a {
  z-index: -1;
}

.white {
  color: #fff !important;
}

.el-tag {
  display: inline;

  float: left;

  text-align: center;

  font-size: 14px;
}

.description-Added-Course {
  margin-right: 16px;
}

.description-Added-Course .title-Description {
  text-align: justify;

  line-height: 10px;
}
</style>

