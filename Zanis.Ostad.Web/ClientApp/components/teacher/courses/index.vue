<template>
  <el-card>
    <h4 style="display:inline;">دروس تدریس شده</h4>
    <div>
      <el-button @click="isAddingNewCourse=true" class="left">
        افزودن دوره جدید
      </el-button>
    </div>
    <el-table height="500" :data="courseData" size="large">
      <el-table-column label="عنوان">
        <template slot-scope="scope">
          {{scope.row.title}}
        </template>
      </el-table-column>

      <el-table-column label="قیمت (تومان)">
        <template slot-scope="scope">
          {{scope.row.priceAsTomans}}
        </template>
      </el-table-column>

      <el-table-column label="وضعیت" width="180">
        <template slot-scope="scope">
          <el-tag class="previewState" v-if="scope.row.approvalStatus===0">
            در انتظار تایید
          </el-tag>
          <el-tag class="previewState" v-if="scope.row.approvalStatus===5" type="success">تایید شده</el-tag>
          <el-tag class="previewState" v-if="scope.row.approvalStatus===10" type="danger">رد شده</el-tag>
          <el-tag class="previewState" v-if="scope.row.approvalStatus===15" type="warning">غیر فعال توسط مدرس</el-tag>
        </template>
      </el-table-column>

      <el-table-column label="جزئیات" width="150">
        <template slot-scope="scope">
          <el-button @click="selectedCourseId=scope.row.id">
            مشاهده
          </el-button>
        </template>
      </el-table-column>


      <el-table-column label="عملیات">
        <template slot-scope="scope">
          <el-row type="flex">
            <el-button @click="changingApprovalStateItem=scope.row" class="deactive">
              تغییر وضعیت
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

    <CourseDetails v-if="selectedCourseId" :isOpen="!!selectedCourseId"
                   :courseId="selectedCourseId" @close="selectedCourseId=undefined"></CourseDetails>

    <add-course @close="isAddingNewCourse=false" :isOpen="isAddingNewCourse"></add-course>
    <approval-state-changer v-if="changingApprovalStateItem"
                            :isOpen="changingApprovalStateItem"
                            :item="changingApprovalStateItem"
                            @close="getCourse"
    ></approval-state-changer>
  </el-card>
</template>

<script>
  import AddCourse from './add-course-dialog';
  import ApprovalStateChanger from './approval-state-changer';
  import CourseDetails from './course-details.vue';
  import axios from "axios";

  export default {
    data() {
      return {
        query: {
          pageSize: 10
        },
        courseData: [],
        isAddingNewCourse: false,
        selectedCourseId: false,
        changingApprovalStateItem: undefined,
        meta: {}
      };

    },
    components: {
      ApprovalStateChanger,
      AddCourse,
      CourseDetails
    },
    methods: {
      getCourse() {
        this.changingApprovalStateItem = undefined;
        axios.get("/api/TeacherAccount/courses", {
          params: this.query
        }).then(res => {
          this.courseData = res.data.items;
          this.meta = {allCount: res.data.allCount};
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
    },

    mounted() {
      this.getCourse();
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

  .left {
    float: left !important;
  }

  .previewState {
    width: 100%;
    text-align: center;
  }
</style>
