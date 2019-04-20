<template>
  <div>
    <el-dialog title="ثبت دوره" :visible.sync="isOpen" width="80%" @closed="$emit('close')">
      <el-row>
        <el-col :md="12">
          <el-card shadow="always">
            <el-form ref="form" :model="form">
              <el-form-item
                prop="title"
                :rules="[{ required: true, message: 'وارد کردن عنوان دوره الزامی می باشد'}]"
                label="عنوان">
                <el-input type="text" placeholder="عنوان دوره" v-model="form.title"></el-input>
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
                label=" قیمت (ریال)"
                prop="price"
                :rules="[
                  { required: true, message: 'وارد کردن قیمت الزامی می باشد'},
                  { type: 'number', message: 'فیمت باید عددی باشد'}
                ]">
                <el-input type="text" placeholder="قیمت (ریال)" v-model.number="form.price"></el-input>
              </el-form-item>
              <el-form-item label="پیام به مدیریت">
                <el-input type="textarea" v-model="form.teacherMessageForAdmin"></el-input>
              </el-form-item>

              <el-form-item>
                <el-button
                  type="warning"
                  class="w100"
                  @click="isLessonsearchDialog = {}"
                >انتخاب درس
                </el-button>
              </el-form-item>
              <el-table v-if="selectedLessons.length" :data="selectedLessons">
                <el-table-column label="نام درس">
                  <template slot-scope="scope">{{ scope.row.lessonName }}</template>
                </el-table-column>

                <el-table-column label="مقطع">
                  <template slot-scope="scope">{{ scope.row.gradeName }}</template>
                </el-table-column>

                <el-table-column label="رشته">
                  <template slot-scope="scope">{{ scope.row.fieldName }}</template>
                </el-table-column>
              </el-table>
              <el-form-item>
                <el-tag
                  class="w100"
                  type="danger"
                  v-if="!selectedLessons.length"
                >در حال حاظر درسی را انتخاب نکرده اید
                </el-tag>
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
            title="فایل های آموزشی خود را به ترتیب به عنوان سرفصل اضافه کنید"
            type="info"
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
                    <el-button style="float:left" @click="removeItem(item)">حذف</el-button>
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
                    <el-progress v-show="item.startToUpload" :percentage="item.uploadProgress"></el-progress>
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
        type : Boolean
      },
      preSelectedCourseCategoryId: {
        type : Number
      },
      customMessageForSuccess : {
        type : Function
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
        selectedLessons: [],
        close: false,
        editingItem: null,
        courseCategories: [],
        courseItems: [],
        form: {
          price: undefined,
          courseCategoryId: "",
          title: "",
          teacherMessageForAdmin: "",
          lessonFieldId: [],
        }
      };
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
        } else
          this.courseItems.push(item);
      },
      selectLessons(lessons) {
        this.selectedLessons = lessons;
        this.form.lessonFieldId = lessons.map(x => x.id);
      },
      closeSearchDialog() {
        this.isLessonsearchDialog = false;
      },
      processFile(event) {
        this.form.zipFile = event.target.files[0];
      },
      registerLesson() {
        if (this.courseItems.length == 0) {
          this.$message({
            type: "error",
            message: "کاربر گرامی برای این درس حداقل یک سرفصل را باید آپلود کنید"
          });
          return false;
        }
        if (this.selectedLessons.length == 0) {
          this.$message({
            type: "error",
            message: "کاربر گرامی برای این درس حداقل یک درس را باید اتخاب کنید"
          });
          return false;
        }
        this.$refs.form.validate(valid => {
          if (valid) {
            this.form.lessonFieldIds=this.selectedLessons.map(x=>x.id);
            axios
              .post("/api/TeacherAccount/courses", this.form)
              .then(res => {
                if (res.data.status == 1) {
                  this.sendCourseItems(res.data.data.id,0);
                }
              });
          }
        });
      },
      sendCourseItems(courseId, idx) {
        var vm=this;
        let currentItemToUpload = this.courseItems[idx];
        if (!currentItemToUpload) {
          this.showSuccessDialog();
          return;
        }
        currentItemToUpload.courseId = courseId;
        let data=new FormData();
        for (let prop in currentItemToUpload)
          data.append(prop, currentItemToUpload[prop]);
        vm.courseItems[idx].startToUpload=true;
        this.$http.post("/api/TeacherAccount/courses/courseItems", data, {
          headers: {
            'Content-Type': 'multipart/form-data'
          },
          onUploadProgress: (progressEvent) => {
            vm.courseItems[idx].uploadProgress = Math.floor((progressEvent.loaded * 100) / progressEvent.total);
            vm.$forceUpdate()
          }
        }).then(res => this.sendCourseItems(courseId, idx + 1));
      },
      editItem(item) {
        this.editingItem = item;
        this.editingItem.editing = true;
      },
      removeItem(item){
        this.courseItems.splice(this.courseItems.indexOf(item),1);
      },
      showSuccessDialog(){
        if(this.customMessageForSuccess){
          this.customMessageForSuccess();
          this.$emit('close')
        }else{
          this.message({
            type:'success',
            message:'دوره شما با موفقیت ثبت شد'
          });
          this.$emit('close')
        }
      }
    },
    watch:{
      preSelectedCourseCategoryId(val){
        this.form.courseCategoryId = val;
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

