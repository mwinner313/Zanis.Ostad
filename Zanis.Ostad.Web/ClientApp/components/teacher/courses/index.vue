<template>
  <el-card>
    <div>
      <el-button @click="isAddingNewCourse=true" class="left">افزودن دوره جدید</el-button>
    </div>
    <el-table height="500" :data="courceData" size="large" style="width: 100%">
      <el-table-column label="ردیف" width="60">
        <template slot-scope="scope">{{scope.row.id}}
</template>
      </el-table-column>

     
      <el-table-column label="کددرس" width="90">
<template slot-scope="scope">
  {{ scope.row.lessonCode}}
</template>
      </el-table-column>

      <el-table-column label="قیمت" width="90">
<template slot-scope="scope">
  {{scope.row.price}}
</template>
      </el-table-column>


      <el-table-column label="مقطع" width="120">
<template slot-scope="scope">
  {{ scope.row.gradeTitle}}
</template>
      </el-table-column>
      <el-table-column label="نام استاد">
<template slot-scope="scope">
  {{ scope.row.teacher}}
</template>
      </el-table-column>

         <el-table-column label="عنوان" width="240">
<template slot-scope="scope">
  {{ scope.row.title}}
</template>
      </el-table-column>


      <el-table-column label="جزئیات" width="150">
<template slot-scope="scope">
  <el-button @click="showDetails(scope.row.id)">
    مشاهده</el-button>
</template>
      </el-table-column>

       <el-table-column label="امکانات">
        <template slot-scope="scope">
         <el-row type="flex">
            <el-button @click="activeCourse(scope.row.id)" class="mgl-17">
            فعال</el-button>

            <el-button @click="deActiveCourse(scope.row.id)" class="deactive">
            غیر فعال</el-button>
         </el-row>
        </template>
      </el-table-column>

    </el-table>
    <el-pagination
      class="pagenation"
      @size-change="handleSizeChange"
      @current-change="handleCurrentChange"
      :current-page.sync="query.currentPage"
      :page-sizes="[10,15,20,30]"
      :page-size="query.pageSize"
      layout="total, sizes, prev, pager, next, jumper"
      :total="meta.allCount"
    ></el-pagination>

    <el-dialog title="جزئیات دوره" :visible.sync="courcedialog" width="80%">
      <el-row :gutter="40">
        <el-col :xs="12" :md="12" :lg="12" v-for="(item,index) in courcedetails" :key="index">
          <el-card class="card-item">
            <div slot="header" class="clearfix">
              <span>{{item.title}}</span>
              <span class="icon" v-html="previewIconCourse(item.contentType)"></span>
              
              <el-tag v-if="item.state==5" type="success" class="icon">تایید شده</el-tag>
              <el-tag v-else-if="item.state==0" class="icon">در انتظار تایید</el-tag>
              <el-tag v-else-if="item.state==10" type="danger" class="icon">رد شده</el-tag>
              <el-tag v-else-if="item.state==15" type="info" class="icon">رد شده توسط استاد</el-tag>

            </div>

            <div class="wrapper-body-card">
              {{item.adminMessageForTeacher}}
            </div>

            <div class="wrapper-download-link">

                <el-button type="primary" round class="downloadBtn">
                  <i class="fas fa-download customDownloadIcon"></i>
                   <a :href="''+item.filePath+''" class="white">دانلود</a>
                </el-button>
                         </div>
          </el-card>
        </el-col>
      </el-row>
    </el-dialog>
    <add-course @close="isAddingNewCourse=false" :isOpen="isAddingNewCourse"></add-course>
  </el-card>
</template>

<script>
import AddCourse from './add-course-dialog';
  import axios from "axios";
  export default {
    name: "AdminListCourse",
    data() {
      return {
        query: {
          pageSize: 10
        },
        courceData: [],
        isAddingNewCourse:false,
        courcedetails: [],
        courcedialog: false,
        meta: {}
      };

    },
     components:{
         AddCourse
       },
    methods: {
      getCourse() {
        axios.get("/api/Courses", {
          params: this.query
        }).then(res => {
          this.courceData = res.data.items;
          this.meta = res.data.allCount;
                });
      },
      handleSizeChange(val) {
        this.query.pageSize = val;
        this.getCourse();
      },
      handleCurrentChange(val) {
        this.query.pageOffset = (val - 1) * this.query.pageSize;
        this.query.currentPage = val;
        this.getCourse();
      },

      showDetails(id) {
        axios
          .get("/api/Courses/" + id)
          .then(res => {
            this.courcedetails = res.data.contents;
            this.courcedialog = true;
            console.log(res.data.contents);
                    })
          .catch(err => {});
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
      activeCourse(id){
        axios.patch('/api/TeacherAccount/courses/'+id+'/active')
        .then(res=>{
        this.$message({
          message: 'برای تغییر وضعیت این مورد ابتدا محتوا میبایست توسط مدیر سیستم تعیین وضعیت شود',
          type: 'success'
        });
        })
        .catch(err=>{

        })

      },
      deActiveCourse(id){

         axios.patch('/api/TeacherAccount/courses/'+id+'/deactive')
        .then(res=>{
          this.$message({
          message: 'برای تغییر وضعیت این مورد ابتدا محتوا میبایست توسط مدیر سیستم تعیین وضعیت شود',
          type: 'warning'
        });
        console.log(res.data);
        })
        .catch(err=>{

        })

      }
    },
   
    mounted() {
      this.getCourse();
    }
  };
</script>

<style scoped>
  .icon {
    float: left;
  }

  .downloadBtn {
        float: left;
        margin-bottom: 10px;
  }

  .card-item{
    margin-bottom: 50px;
  }

  .deactive{
    margin-right: 0;
  }
  .customDownloadIcon{
    margin: 0;
    line-height: 8px;
    padding-left: 5px;
    color: #fff;
    font-size: 14px;
  }
  .mgl-17{
    margin-left: 17px;
  }
.left {
  float: left  !important;
}
</style>
