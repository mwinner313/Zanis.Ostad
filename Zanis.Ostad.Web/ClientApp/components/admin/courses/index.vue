<template>
  <el-card>
    <el-table height="500" :data="courseData" size="large" style="width: 100%">
      <el-table-column width="50" label="ردیف">
        <template slot-scope="scope">{{scope.row.id}}
        </template>
      </el-table-column>

      <el-table-column width="380" label="عنوان">
        <template slot-scope="scope">
          {{ scope.row.title}}
        </template>
      </el-table-column>

      <el-table-column label="کددرس">
        <template slot-scope="scope">
          {{ scope.row.lessonCode}}
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

      <el-table-column label="قیمت">
        <template slot-scope="scope">
          {{scope.row.price}}
        </template>
      </el-table-column>
      <el-table-column width="280" label="عملیات">
        <template slot-scope="scope">
          <div style>
            <el-button @click="showDetails(scope.row.id)">
              مشاهده
            </el-button>
            <el-button @click="changingApprovalStateItemId=scope.row.id" class="deactive">
              تغییر وضعیت
            </el-button>
          </div>
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
 <approval-state-changer></approval-state-changer>
  </el-card>
</template>

<script>
  import axios from "axios";
  import ApprovalStateChanger from './approval-state-changer'
  export default {
    name: "AdminListCourse",
    components:{
      ApprovalStateChanger
    },
    data() {
      return {
        query: {
          pageSize: 10
        },
        courseData: [],
        changingApprovalStateItemId: undefined,
        courseDetails: [],
        meta: {}
      };
    },
    methods: {
      getCourse() {
        axios.get("/api/Courses", {
          params: this.query
        }).then(res => {
          this.courseData = res.data.items;
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
            this.courseDetails = res.data.contents;
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
    },
    computed: {},
    mounted() {
      this.getCourse();
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
