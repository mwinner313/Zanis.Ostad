<template>
  <div>
    <el-dialog title="ثبت دوره" :visible.sync="isOpen" width="80%" @closed="$emit('close')">
      <el-row>
        <el-col :md="12">
          <el-card shadow="always">
            <el-form ref="form" :model="form">
              <el-progress v-show="uploadProgress" :percentage="uploadProgress"></el-progress>
              <el-form-item
                prop="courseTitle"
                :rules="[{ required: true, message: 'وارد کردن عنوان دوره الزامی می باشد'}]"
                label="عنوان">
                <el-input type="text" placeholder="عنوان دوره" v-model="form.courseTitle"></el-input>
              </el-form-item>
              <el-form-item
                prop="courseCategoryId"
                :rules="[{ required: true, message: 'انتخاب کردن عنوان نوع دوره الزامی می باشد'}]"
                label="نوع دوره">

                <el-select v-model="form.courseCategoryId" placeholder="نوع دوره" class="w100">
                  <el-option
                    v-for="item in courseCategories"
                    :key="item.value"
                    :label="item.name"
                    :value="item.id"
                  ></el-option>
                </el-select>
              </el-form-item>
              <el-form-item
                prop="description"
                label="توضیحات"
              >
                <el-input type="textarea" placeholder="توضیحات" v-model="form.description"></el-input>
              </el-form-item>
              <el-form-item
                label="قیمت"
                prop="price"
                :rules="[
                  { required: true, message: 'وارد کردن قیمت الزامی می باشد'},
                  { type: 'number', message: 'فیمت باید عددی باشد'}
                ]">
                <el-input type="text" placeholder="قیمت" v-model.number="form.price"></el-input>
              </el-form-item>
              <el-form-item label="پیام به مدیریت">
                <el-input type="textarea" v-model="form.teacherMessage"></el-input>
              </el-form-item>

              <el-form-item>
                <el-button
                  type="warning"
                  class="w100"
                  @click="isLessonsearchDialog = {}"
                >انتخاب درس
                </el-button>
              </el-form-item>

              <el-form-item>
                <el-tag
                  class="w100"
                  type="danger"
                  v-if="!itemSelectedLesson"
                >در حال حاظر درسی را انتخاب نکرده اید
                </el-tag>
                <el-tag class="w100" v-else>{{itemSelectedLesson}}</el-tag>
              </el-form-item>

              <el-form-item>

                <button
                  type="primary"
                  class="sendBtn w100 xanis-btn"
                  size="medium"
                  @click.prevent="registerLesson"
                >ارسال
                </button>
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
              <button class="xanis-btn-secondary" @click.prevent="editingItem={}">افزودن سرفصل جدید</button>
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
                    توضیحات <br><br>
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
      :isOpen="!!isLessonsearchDialog"
      @close="isLessonsearchDialog=undefined"
      @lessonsSelected="selectLessons"
    ></searchLesson>

    <!-- addCourseListDialog -->
    <addCourseItemDialog
      @submit="addItemToCourseItems"
      :isOpen="!!editingItem"
      :editingItem="editingItem"
      @close="editingItem=undefined"
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
      },
      preSelectedCourseTitleId: {
        type: Number
      }
    },

    components: {
      searchLesson,
      addCourseItemDialog
    },

    data() {
      return {
        isLessonsearchDialog: false,
        selectedTeacherItem: "",
        itemSelectedLesson: "",
        selectedLesson: null,
        close: false,
        editingItem: null,
        uploadProgress: 0,
        courseCategories: [],
        responseCourseId: 0,
        courseItems: [],
        form: {
          price: undefined,
          courseCategoryId: "",
          courseTitle: "",
          teacherMessage: "",
          lessonFieldId: [],
        }
      };
    },
    updated() {
      if (this.preSelectedCourseTitleId)
        this.form.courseTitleId = this.preSelectedCourseTitleId;
    },
    methods: {
      getCourseCategories() {
        axios.get("/api/courseCategories").then(res => {
          this.courseCategories = res.data;
        });
      },
      addItemToCourseItems(item) {
        if (item.editing) {
          item.editing = undefined;
          this.courseItems.splice(_.findIndex(x => x.editing), 1, item);
        }else
        this.courseItems.push(item);
      },
      selectLessons(lessonIds) {
        this.form.lessonFieldId=lessonIds;
      },
      closeSearchDialog() {
        this.isLessonsearchDialog = false;
      },
      processFile(event) {
        this.form.zipFile = event.target.files[0];
      },
      registerLesson() {
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
            axios
              .post("/api/TeacherAccount/courses", {
                price: this.form.price,
                description: this.form.description,
                teacherMessageForAdmin: this.form.teacherMessage,
                courseCategoryId: this.form.courseCategoryId,
                title: this.form.courseTitle,
                lessonFieldIds: this.form.lessonFieldId
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
        this.editingItem = item;
        this.editingItem.editing = true;
      }
    },
    mounted() {
      this.getCourseCategories();
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
    text-align: left;
  }

  .mg-r-t-15 {
    margin-right: 15px;
    margin-top: 15px;
  }

  .w100 {
    width: 100%;
  }

  .description-Added-Course .title-Description {
    text-align: justify;
    line-height: 10px;
  }
</style>

