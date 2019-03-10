<template>
  <el-card>
    <div>
      <el-button>افزودن دوره جدید</el-button>
      <addCourse></addCourse>
    </div>
    <el-table height="500" :data="courceData" size="large" style="width: 100%">
      <el-table-column label="ردیف">
        <template slot-scope="scope">{{scope.row.id}}
        </template>
      </el-table-column>

      <el-table-column label="عنوان">
        <template slot-scope="scope">
          {{ scope.row.title}}
        </template>
      </el-table-column>

      <el-table-column label="کددرس">
        <template slot-scope="scope">
          {{ scope.row.lessonCode}}
        </template>
      </el-table-column>

      <el-table-column label="قیمت">
        <template slot-scope="scope">
          {{scope.row.price}}
        </template>
      </el-table-column>


      <el-table-column label="مقطع">
        <template slot-scope="scope">
          {{ scope.row.gradeTitle}}
        </template>
      </el-table-column>
      <el-table-column label="نام استاد">
        <template slot-scope="scope">
          {{ scope.row.teacher}}
        </template>
      </el-table-column>

      <el-table-column label="جزئیات">
        <template slot-scope="scope">
          <el-button @click="showdetails(scope.row.id)">
            مشاهده
          </el-button>
        </template>
      </el-table-column>

      <el-table-column label="امکانات">
        <template slot-scope="scope">
          <el-row type="flex">
            <el-button @click="activeSection(scope.row.id)">
              فعال
            </el-button>

            <el-button @click="deActiveSection(scope.row.id)" class="deactive">
              غیر فعال
            </el-button>
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

    <el-dialog title="جزئیات دوره" :visible.sync="!!courcedetails" width="80%">
      <el-row :gutter="40">
        <el-col :xs="12" :md="12" :lg="12" v-for="(item,index) in courcedetails" :key="index">
          <el-card class="card-item">
            <div slot="header" class="clearfix">
              <span>{{item.title}}</span>
              <span class="icon" v-html="previewIconCourse(item.contentType)"></span>
            </div>

            <div class="wrapper-body-card">در این بخش توضیحات قرار می گیرد</div>

            <div class="wrapper-download-link">


              <a :href="''+item.filePath+''" class="download">دانلود</a>
            </div>
          </el-card>
        </el-col>
      </el-row>
    </el-dialog>
  </el-card>
</template>

<script>
  import addCourse from './add-course-dialog';
  import axios from "axios";

  export default {
    name: "AdminListCourse",
    data() {
      return {
        query: {
          pageSize: 10
        },
        courceData: [],
        courcedetails: [],
        courcedialog: false,
        meta: {}
      };
      components:{
        addCourse
      }
    },
    methods: {
      Getcourse() {
        axios.get("/api/Courses", {
          params: this.query
        }).then(res => {
          this.courceData = res.data.items;
          this.meta = res.data.allCount;
          console.log(res.data);
        });
      },
      handleSizeChange(val) {
        this.query.pageSize = val;
        this.Getcourse();
      },
      handleCurrentChange(val) {
        this.query.pageOffset = (val - 1) * this.query.pageSize;
        this.query.currentPage = val;
        this.Getcourse();
      },

      showdetails(id) {
        axios
          .get("/api/Courses/" + id)
          .then(res => {
            this.courcedetails = res.data.contents;
            this.courcedialog = true;
            console.log(res.data.contents);
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
      activeSection(id) {
        axios.patch('/api/TeacherAccount/courses/' + id + '/active')
          .then(res => {
            this.$message({
              message: 'برای تغییر وضعیت این مورد ابتدا محتوا میبایست توسط مدیر سیستم تعیین وضعیت شود',
              type: 'success'
            });
          })
          .catch(err => {

          })

      },
      deActiveSection(id) {

        axios.patch('/api/TeacherAccount/courses/' + id + '/deactive')
          .then(res => {
            this.$message({
              message: 'برای تغییر وضعیت این مورد ابتدا محتوا میبایست توسط مدیر سیستم تعیین وضعیت شود',
              type: 'warning'
            });
            console.log(res.data);
          })
          .catch(err => {

          })

      }
    },
    computed: {},
    mounted() {
      this.Getcourse();
    }
  };
</script>

<style scoped>
  .icon {
    float: left;
  }

  .download {
    color: #000;
    float: left;
  }

  .card-item {
    margin-bottom: 50px;
  }

  .deactive {
    margin-right: 0;
  }

</style>
