<template>
    <el-dialog  :visible.sync="isOpen" @closed="$emit('close')" width="80%">
                <div slot="title">
            <p>
                {{courseDetail.title}}
            </p>
            <small><i class="el-icon-time"></i>{{ courseDetail.createdOn | moment("jYYYY/jM/jD HH:mm") }}</small>
        </div>
        <el-row :gutter="40">
    
            <el-col :xs="12" :md="12" :lg="12" v-for="(item,index) in courseDetail.contents" :key="index">
    
                <el-card class="card-item">
    
                    <div slot="header" class="clearfix">
                         <span>{{item.order}}.</span>
    
                        <span class="icon" v-html="previewIconCourse(item.contentType)"></span>
    
    
                       
                        <span>{{item.title}}</span>
    
                        <small><i class="el-icon-time"></i>{{ item.createdOn | moment("jYYYY/jM/jD HH:mm") }}</small>

                        <el-button  @click="editingCourseItem=item" type="primary" plain size="small" class="float-right">ویرایش</el-button>

    
                        <el-tag v-if="item.state==5" type="success" >تایید شده</el-tag>
    
    
    
                        <el-tag v-else-if="item.state==0" >در انتظار تایید</el-tag>
    
    
    
                        <el-tag v-else-if="item.state==10" type="danger">رد شده</el-tag>
    
    
    
                        <el-tag v-else-if="item.state==15" type="info">رد شده توسط استاد</el-tag>

                        
    
                    </div>
    
                    <div class="wrapper-body-card">{{item.adminMessageForTeacher}}</div>
                    <div class="wrapper-download-link">
    
                        <el-button type="primary" round class="downloadBtn">
    
                            <i class="fas fa-download customDownloadIcon"></i>
    
    
    
                            <a :href="item.filePath" class="white">دانلود</a>
    
                        </el-button>
    
                    </div>
    
                </el-card>
    
            </el-col>

             <CourseItemAddEditDialog @close="getContent" :is-open="!!editingCourseItem" :item="editingCourseItem"></CourseItemAddEditDialog>
    
        </el-row>
    
    </el-dialog>
</template>

<script>
    import axios from "axios";
    import CourseItemAddEditDialog from './course-item-add-edit-dialog'
    
    export default {
    
        name: "",
    
    
        data() {
    
            return {
    
                courseDetail: [],

                editingCourseItem : undefined,
    
            };
    
        },
        components:{
        CourseItemAddEditDialog
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
    
            getContent() {
                this.editingCourseItem=undefined;
    
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

<style scoped>
 .downloadBtn {
    float: left;
    margin-bottom: 10px;
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
    
</style>
