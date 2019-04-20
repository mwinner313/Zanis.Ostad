<template>
  <el-dialog width="60%" @close="$emit('close')" :visible.sync="isOpen">
    <div slot="title">
      <p>
        {{course.title}}
        <el-button class="float-right" @click="editingCourseItem={courseId,order:0}" style="margin-left: 30px;"
                   type="success"
                   plain>افزودن سر فصل +
        </el-button>
        <br>
        <small><i class="el-icon-time"></i>{{ course.createdOn | moment("jYYYY/jM/jD HH:mm") }}</small>
      </p>
    </div>
    <el-alert
      title="توضیحات دوره برای دانشجو"
      type="info"
      :closable="false"
      :description="course.description"
      show-icon>
    </el-alert>
    <br>
    <el-alert
      title="توضیحات دوره برای مدیر سیستم از طرف استاد"
      type="info"
      :closable="false"
      :description="course.teacherMessageForAdmin"
      show-icon>
    </el-alert>
    <br>
    <el-row :gutter="40">

      <el-col :lg="24" v-for="item in course.contents" :key="item.id">
        <el-card class="card-item">
          <div slot="header" class="clearfix">
            <el-button @click="editingCourseItem=item" type="primary" plain class="float-right">ویرایش</el-button>
            <h5> {{item.order}}.
              <span class="icon" v-html="previewIconCourse(item.contentType)"></span>
              <span>{{item.title}}</span></h5>
            <small><i class="el-icon-time"></i>{{ course.createdOn | moment("jYYYY/jM/jD HH:mm") }}</small>
            <el-tag v-if="item.state==5" type="success" class="icon">تایید شده</el-tag>
            <el-tag v-else-if="item.state==0" class="icon">در انتظار تعیین وضعیت</el-tag>
            <el-tag v-else-if="item.state==10" type="danger" class="icon">رد شده</el-tag>
            <el-tag v-else-if="item.state==15" type="info" class="icon">رد شده توسط استاد</el-tag>
          </div>

          <div class="body-card-container">
            <el-card v-if="item.teacherMessageForAdmin" shadow="never">
              <h5>توضیحات برای مدیر سیستم از طرف استاد</h5>
              <p> {{item.teacherMessageForAdmin}}</p>
            </el-card>
            <br>
            <el-card v-if="item.adminMessageForTeacher" shadow="never">
              <h5>توضیحات برای استاد از طرف مدیر سیستم</h5>
              <p> {{item.adminMessageForTeacher}}</p>
            </el-card>
          </div>
          <div class="download-link-wrapper clearfix">
            <a :href="item.filePath" class="float-right ">
              <el-button type="success" plain>
                دانلود <i class="fas fa-download" style="font-size:13px;"></i>
              </el-button>
            </a>
          </div>
        </el-card>
      </el-col>
    </el-row>
    <CourseItemAddEditDialog @close="loadData" :is-open="!!editingCourseItem"
                             :item="editingCourseItem"></CourseItemAddEditDialog>
  </el-dialog>
</template>

<script>
  import axios from "axios";
  import CourseItemAddEditDialog from './course-item-add-edit-dialog'

  export default {
    name: "",
    components: {
      CourseItemAddEditDialog
    },
    props: {
      isOpen: {
        type: Boolean,
      },
      courseId: {
        type: Number,
        default: undefined
      },
    },
    data() {
      return {
        editingCourseItem: undefined,
        course: {}
      }
    },
    mounted() {
      this.loadData()
    },
    methods: {
      loadData() {
        this.editingCourseItem = undefined;
        axios
          .get("/api/Courses/" + this.courseId)
          .then(res => {
            console.log(res.data);
            this.course = res.data;
          })
          .catch(err => {
          });
      },
      previewIconCourse(contentType) {
        switch (contentType) {
          case 0:
            return '<i class="far fa-file-pdf"></i>';
          case 1:
            return '<i class="far fa-file-video"></i>';
          default:
            return '';
        }
      },
    }
  }
</script>

<style scoped>
  .download-link-wrapper {
    margin-top: 15px;
  }

  .card-item {
    margin-bottom: 10px;
  }
</style>
