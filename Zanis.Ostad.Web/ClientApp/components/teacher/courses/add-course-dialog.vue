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
                <el-button
                  type="warning"
                  class="w100"
                  @click="isLessonsearchDialog = true"
                >انتخاب درس</el-button>
              </el-form-item>

              <el-form-item>
                <el-tag
                  class="w100"
                  type="danger"
                  v-if="!itemSelectedLesson"
                >در حال حاظر درسی را انتخاب نکرده اید</el-tag>

                <el-tag class="w100" v-else>{{itemSelectedLesson}}</el-tag>
              </el-form-item>

              <el-form-item>
                <!-- <input
                  type="file"
                  name="myfile"
                  accept=".zip"
                  @change="processFile"
                  ref="filePicker"
                  style="display: none"
                >

                <el-button type="info" @click="$refs.filePicker.click()">انتخاب فایل</el-button>
                <el-tag type="warning" v-if="form.zipFile">{{form.zipFile.name}}</el-tag>
                <br>-->
                <el-button
                  type="primary"
                  class="sendBtn w100"
                  size="medium"
                  @click="registerLesson"
                >ارسال</el-button>
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
          <div class="upload-course-item-wrapper">
            <div class="btn-upload-wrapper">
              <el-button type="primary" @click="addCourseItem=true">افزودن سرفصل جدید</el-button>
            </div>
            <div class="details-item-wrapper mg-r-t-15">
              <div
                class="item"
                v-for="(item,index) in courseItems"
                :key="index"
              >
                <div class="header-item">
                  <p>
                    عنوان:
                    <span>{{item.title}}</span>
                    <el-button style="float:left" @click="editItem(item)">ویرایش</el-button>
                  </p>
                </div>
                <div class="content-item">
                  <p>
                    توضیحات
                    <span>{{item.teacherMessageForAdmin}}</span>
                  </p>
                </div>
                <div class="footer-item">
                  <div class="file-name-wrapper">
                    <span>نام فایل</span>
                    <el-tag>{{item.file.name}}</el-tag>
                  </div>
                </div>
              </div>
            </div>
          </div>
        </el-col>
      </el-row>
    </el-dialog>

    <!-- searchLessonItem -->
    <searchLesson
      :isOpen="isLessonsearchDialog"
      @close="selectedLesson=undefined"
      v-on:lessonSelected="selectLesson($event)"
      v-on:close="closeSearchgDialog"
    ></searchLesson>

    <!-- addCourseListDialog -->
    <addCourseItemDialog
      @submit="addItemToCourseItems"
      :isOpen="addCourseItem"
      :isEdited="itemEdited"
      @close="addCourseList=false"
    ></addCourseItemDialog>
  </div>
</template>
<script>
import axios from "axios";
import searchLesson from "./search-lesson";
import addCourseItemDialog from "./add-course-item-dialog";
export default {
  name: "add-course-dialog",

  props: {
    isOpen: {
      type: Boolean
    }
  },

  components: {
    searchLesson,
    addCourseItemDialog
  },

  data() {
    return {
      addCourseItem: undefined,
      isLessonsearchDialog: false,
      selectedTeacherItem: "",
      itemSelectedLesson: "",
      selectedLesson: null,
      close: false,
      itemEdited:null,
      uploadProgress: 0,
      courseTitles: [],
      responseCourseId: 0,
      courseItems: [],
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
    getCourseTitles() {
      axios.get("/api/courseTitles").then(res => {
        this.courseTitles = res.data;
      });
    },
    addItemToCourseItems(item) {
      this.courseItems.push(item);
      console.log(this.courseItems);
    },

    selectLesson(item) {
      this.itemSelectedLesson =
        item.gradeName + " - " + item.fieldName + " - " + item.lessonName;

      this.form.lessonFieldId = item.id;
    },

    closeSearchgDialog() {
      this.isLessonsearchDialog = false;
    },

    processFile(event) {
      this.form.zipFile = event.target.files[0];
    },

    registerLesson() {
      // validationArea
      this.responseCourseId = 0;
      if (this.courseItems.length == 0) {
        this.$message({
          type: "error",
          message: "کاربر گرامی برای این درس حداقل یک سرفصل را باید آپلود کنید"
        });

        return false;
      }
      this.$refs.form.validate(valid => {
        if (valid) {
          // validation file
          // if (!this.form.zipFile) {
          //   this.$message({
          //     message: "لطفا فایل ارسالی را انتخاب کنید",
          //     type: "warning"
          //   });
          //   return;
          // }
          let data = new FormData();

          data.append("Price", this.form.price);

          data.append("Description", this.form.description);

          data.append("TeacherMessageForAdmin", this.form.teacherMessage);

          data.append("CourseTitleId", this.form.courseTitle);

          data.append("LessonFieldId", this.form.lessonFieldId);

          // data.append("ZipFile", this.form.zipFile);

          axios

            .post("/api/TeacherAccount/courses", data, {
              headers: {
                "Content-Type": "multipart/form-data"
              }

              // onUploadProgress: progressEvent => {
              //   this.uploadProgress = Math.floor(
              //     (progressEvent.loaded * 100) / progressEvent.total
              //   );
              // }
            })

            .then(res => {
              if (res.data.status == 1) {
                this.$message({
                  message: "دوره شما با موفقیت ثبت شد",
                  type: "success"
                });
                this.courseItems.forEach(element => {
                  console.log(element, "el");
                });

                this.responseCourseId = res.data.data.id;

                this.$emit("close");
              }
            });
        }
      });
    },
    editItem(item) {
      this.editItem=item;
      console.log(this.editItem);
    }
  },

  mounted() {
    this.getCourseTitles();
  }
};
</script>
<style scoped>
.item {
  border: 1px solid #ccc;
  padding: 10px;
  margin-bottom: 15px;
  border-radius: 5px;
}

.footer-item {
  border-top: 1px solid #ccc;
  padding: 10px;
}
.btn-upload-wrapper {
  margin-top: 20px;
  text-align: center;
}
.mg-r-t-15 {
  margin-right: 15px;
  margin-top: 15px;
}
.el-card__header {
  padding-bottom: 0px !important;
}
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

