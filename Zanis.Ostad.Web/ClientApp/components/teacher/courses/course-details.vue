<template>
  <el-dialog :visible.sync="isOpen" @closed="$emit('close')" width="60%">
    <div slot="title">
      <p>
        {{courseDetail.title}}
        <el-button
          style="margin-left: 30px;"
          class="float-right"
          @click="editingCourseItem={courseId}"
          type="success"
          plain
        >+افزودن سر فصل</el-button>
        <el-button
          style="margin-left: 10px;"
          class="float-right"
          @click="updatingCourseId = courseId"
          type="warning"
          plain
        >ویرایش</el-button>
      </p>
      <small>
        <i class="el-icon-time"></i>
        {{ courseDetail.createdOn | moment("jYYYY/jM/jD HH:mm") }}
      </small>

      <p v-show="courseDetail.adminMessageForTeacher" class="message-admin-for-teacher-wrapper">
        <span>پیام مدیر</span>
        <br>
        {{courseDetail.adminMessageForTeacher}}
      </p>
    </div>
    <el-row :gutter="40">
      <el-col :xs="24" :md="24" :lg="24" v-for="(item,index) in courseDetail.contents" :key="index">
        <el-card class="card-item">
          <div slot="header" class="clearfix">
            <span>{{item.order}}.</span>

            <span class="icon" v-html="previewIconCourse(item.contentType)"></span>

            <span>{{item.title}}</span>

            <small>
              <i class="el-icon-time"></i>
              {{ item.createdOn | moment("jYYYY/jM/jD HH:mm") }}
            </small>

            <el-button
              @click="editingCourseItem=item"
              type="primary"
              plain
              size="small"
              class="float-right"
            >ویرایش</el-button>

            <el-tag v-if="item.state==5" type="success">تایید شده</el-tag>

            <el-tag v-else-if="item.state==0">در انتظار تایید</el-tag>

            <el-tag v-else-if="item.state==10" type="danger">رد شده</el-tag>

            <el-tag v-else-if="item.state==15" type="info">رد شده توسط استاد</el-tag>
          </div>

          <div class="wrapper-body-card">
            <el-card shadow="never" v-if="item.adminMessageForTeacher">
              <h5>توضیحات مدیر سیستم</h5>
              {{item.adminMessageForTeacher}}
            </el-card>
          </div>
          <div class="wrapper-download-link">
            <el-button type="success" class="downloadBtnCustom">
              <i class="fas fa-download customDownloadIcon"></i>
              <a :href="item.filePath" class="white">دانلود</a>
            </el-button>
          </div>
        </el-card>
      </el-col>

      <CourseItemAddEditDialog
        @close="getContent"
        :is-open="!!editingCourseItem"
        :item="editingCourseItem"
      ></CourseItemAddEditDialog>
      <CourseUpdateDialog
        urlToCall="/api/TeacherAccount/courses"
        @close="reloadIfUpdated"
        :is-open="!!updatingCourseId"
        :course-id="updatingCourseId"
      ></CourseUpdateDialog>
    </el-row>
  </el-dialog>
</template>

<script>
import axios from "axios";
import CourseItemAddEditDialog from "./course-item-add-edit-dialog";
import CourseUpdateDialog from "./course-edit-dialog";

export default {
  name: "",

  data() {
    return {
      courseDetail: [],
      editingCourseItem: undefined,
      updatingCourseId: undefined
    };
  },
  components: {
    CourseItemAddEditDialog,
    CourseUpdateDialog
  },

  props: {
    isOpen: {
      type: Boolean
    },

    courseId: {
      type: Number
    }
  },

  methods: {
    reloadIfUpdated(reload) {
      this.updatingCourseId = undefined;
      if (reload) this.getContent();
    },
    getContent() {
      this.editingCourseItem = undefined;
      axios.get("/api/TeacherAccount/courses/" + this.courseId).then(res => {
        this.courseDetail = res.data;
      });
    },

    previewIconCourse(contentType) {
      switch (contentType) {
        case 0:
          return '<i class="far fa-file-pdf"></i>';

        case 1:
          return '<i class="far fa-file-video"></i>';

        default:
          return "";
      }
    }
  },

  mounted() {
    this.getContent();
  }
};
</script>

<style>
.downloadBtnCustom {
  float: left;
  margin: 10px 0 !important;
}

.card-item {
  margin-bottom: 50px;
}

.deactive {
  margin-right: 0;
}

.customDownloadIcon {
  margin: 0;
  line-height: 8px;
  padding-left: 5px;
  color: #fff;
  font-size: 14px;
}

.mgl-17 {
  margin-left: 17px;
}

.white {
  color: #fff !important;
}

.message-admin-for-teacher-wrapper {
  border: 1px solid #c0c0c0;
  -webkit-border-radius: 5px;
  -moz-border-radius: 5px;
  border-radius: 5px;
  margin-top: 15px;
  padding: 10px;
}
</style>
