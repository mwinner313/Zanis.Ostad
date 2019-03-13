<template>
  <div>
  
    <el-dialog title="افزودن دوره" :visible.sync="isOpen" width="80%" @closed="$emit('close')">
  
      <el-row>
  
        <el-col :md="12">
  
          <el-form>
  
            <el-progress v-show="uploadProgress" :percentage="uploadProgress"></el-progress>
  
            <el-form-item label="قیمت">
  
              <el-input placeholder="قیمت" v-model="form.price"></el-input>
  
            </el-form-item>
  
  
  
            <el-form-item label="توضیحات">
  
              <el-input placeholder="توضیحات" v-model="form.description"></el-input>
  
            </el-form-item>
  
  
  
            <el-form-item label="عناوین">
  
              <el-select v-model="form.courseTitle" placeholder="عناوین" class="w100">
  
                <el-option v-for="item in courseTitles" :key="item.value" :label="item.name" :value="item.id"></el-option>
  
              </el-select>
  
            </el-form-item>
  
            <el-form-item>
  
              <el-button class="w100" @click="isLessonsearchDialog = true">انتخاب درس</el-button>
  
            </el-form-item>
  
            <el-form-item>
  
              <el-tag type="danger" v-if="itemSelectedLesson==''">در حال حاظر درسی را انتخاب نکرده اید</el-tag>
  
              <el-tag v-else>{{itemSelectedLesson}}</el-tag>
  
            </el-form-item>
  
            <el-form-item label="پیام به مدیریت">
  
              <el-input type="textarea" v-model="form.teacherMessage"></el-input>
  
            </el-form-item>
  
  
  
            <el-form-item>
  
              <input type="file" name="myfile" @change="processFile" ref="filePicker" style="display: none">
  
              <el-button @click="$refs.filePicker.click()">انتخاب فایل</el-button>
  
              <br>
  
              <el-button style="margin-left: 10px;color:#fff !important" size="medium " type="primary" @click="registerLesson">ارسال</el-button>
  
  
  
            </el-form-item>
  
          </el-form>
  
        </el-col>
  
        <el-col :md="12">
  
          <el-alert title="توضیحات" type="info" description="در این بخش توضیحات قرار می گیرد" :closable="false" show-icon></el-alert>
  
        </el-col>
  
      </el-row>
  
    </el-dialog>
  
  
  
    <!-- searchLessonItem -->
  
    <searchLesson :isOpen="isLessonsearchDialog" @close="selectedLesson=undefined" v-on:lessonSelected="getItems($event)" v-on:close="closeSearchgDialog"></searchLesson>
  
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
  
        const config = {
  
          onUploadProgress: function(progressEvent) {
  
            this.fileUploadPercentCompleted = Math.round(
  
              (progressEvent.loaded * 100) / progressEvent.total
  
            );
  
          }
  
        };
  
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
  
              this.$emit('close');
  
            }
  
          });
  
      }
  
    },
  
  
  
    mounted() {
  
      this.getCourseTitle();
  
    }
  
  };
</script>
<style>
  .w100 {
  
    width: 100%;
  
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

