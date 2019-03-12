<template>
  <el-dialog width="80%" :visible.sync="isOpen">
    <div slot="title">
      <p>
        {{course.title}}

        <a :href="course.zipFilesPath">
          <el-button class="float-right" style="margin-left: 30px;" type="primary" plain>
            <i class="fas fa-download" style="font-size:13px;"></i>
            دانلود محتوای آپلود شده توسط استاد
          </el-button>
        </a>
        <el-button class="float-right" style="margin-left: 30px;" type="success" plain>افزودن سر فصل +</el-button>
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

      <el-col :xs="12" :md="12" :lg="12" v-for="item in course.contents" :key="item.id">

        <el-card class="card-item">
          <div slot="header" class="clearfix">
            <span>{{item.title}}</span>
            <span class="icon" v-html="previewIconCourse(item.contentType)"></span>
            <el-tag v-if="item.state==5" type="success" class="icon">تایید شده</el-tag>
            <el-tag v-else-if="item.state==0" class="icon">در انتظار تعیین وضعیت</el-tag>
            <el-tag v-else-if="item.state==10" type="danger" class="icon">رد شده</el-tag>
            <el-tag v-else-if="item.state==15" type="info" class="icon">رد شده توسط استاد</el-tag>
            <el-button  @click="editingCourseItem=item" type="primary" plain class="float-right">ویرایش</el-button>
          </div>
          <div class="body-card-container">
            {{item.teacherMessageForAdmin}}
          </div>
          <div class="download-link-wrapper">
            <a :href="item.filePath">
              <el-button type="success" plain>
                دانلود <i class="fas fa-download" style="font-size:13px;"></i>
              </el-button>
            </a>
          </div>
        </el-card>
      </el-col>
    </el-row>
    <CourseItemDetailsDialog @close="editingCourseItem=undefined" :is-open="!!editingCourseItem" :item="editingCourseItem"></CourseItemDetailsDialog>
  </el-dialog>
</template>

<script>
  import CourseItemDetailsDialog from './course-item-edit-dialog'
  export default {
    name: "",
    components:{
      CourseItemDetailsDialog
    },
    props: {
      isOpen: {
        type: Boolean,
      },
      course: {
        type: Object,
        default: {}
      },
    },
    data() {
      return {
        editingCourseItem : undefined,
      }
    },
    mounted() {
    },
    methods: {
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
</style>
