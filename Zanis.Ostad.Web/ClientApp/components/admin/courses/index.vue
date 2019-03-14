<template>
  <el-card>
    <h3 style="display:inline;">دوره ها</h3>
    <div class="float-right">
    <el-form :inline="true">
      <el-form-item  label="جستجو">
        <el-input @change="loadData" placeholder="جستجو" v-model="query.search"></el-input>
      </el-form-item>
      <el-form-item label="وضعیت">
        <el-select v-model="query.status" @change="getCourses" placeholder="وضعیت">
          <el-option
            label="همه"
            value="">
          </el-option>
          <el-option  v-for="state in [{id:0,title:'در انتظار تعیین وضعیت'},
                                       {id:5,title:'تایید شده'},
                                       {id:10,title:'رد شده'},
                                       {id:15,title:'غیر فعال توسط مدرس'}]"
                      :key="state.id"
                      :label="state.title"
                      :value="state.id">
          </el-option>
        </el-select>
      </el-form-item>
      <el-form-item>
        <el-checkbox label="خوانده نشده ها" v-model="query.notSeen" @change="getCourses"></el-checkbox>
      </el-form-item>
    </el-form>
    </div>
    <el-table v-loading="isLoading" height="720" :data="courseData" size="large" style="width: 100%">
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
      <el-table-column label="وضعیت" width="200">
        <template slot-scope="scope">
          <el-tag v-if="scope.row.approvalStatus===0">در انتظار تعیین وضعیت</el-tag>
          <el-tag v-if="scope.row.approvalStatus===5" type="success">تایید شده</el-tag>
          <el-tag v-if="scope.row.approvalStatus===10" type="danger">رد شده</el-tag>
          <el-tag v-if="scope.row.approvalStatus===15" type="warning">غیر فعال توسط مدرس</el-tag>
        </template>
      </el-table-column>
      <el-table-column width="280" label="عملیات">
        <template slot-scope="scope">
          <div style>
            <el-button @click="showDetails(scope.row.id)">
              مشاهده
            </el-button>
            <el-button @click="changingApprovalStateItem=scope.row" class="deactive">
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
    <approval-state-changer v-if="changingApprovalStateItem"
                            :item="changingApprovalStateItem"
                            :is-open="!!changingApprovalStateItem"
                            @close="getCourses"></approval-state-changer>

    <course-details v-if="!!selectedCourseId" :isOpen="!!selectedCourseId" @close="selectedCourseId=undefined"
                    :courseId="selectedCourseId"></course-details>
  </el-card>
</template>

<script>
  import axios from "axios";
  import ApprovalStateChanger from './approval-state-changer'
  import CourseDetails from './course-details'

  export default {
    name: "AdminListCourse",
    components: {
      ApprovalStateChanger,
      CourseDetails
    },
    data() {
      return {
        query: {
          pageSize: 10
        },
        courseData: [],
        changingApprovalStateItem: undefined,
        selectedCourseId: undefined,
        meta: {}
      };
    },
    methods: {
      getCourses() {
        this.isLoading = true;
        this.changingApprovalStateItem = undefined;
        this.selectedCourseId = undefined;
        axios.get("/api/Courses", {
          params: this.query
        }).then(res => {
          this.isLoading = false;
          this.courseData = res.data.items;
          this.meta = {allCount: res.data.allCount};
        });
      },
      handleSizeChange(val) {
        this.query.pageSize = val;
        this.getCourses();
      },
      handleCurrentChange(val) {
        this.query.pageOffset = (val - 1) * this.query.pageSize;
        this.query.currentPage = val;
        this.getCourses();
      },
      showDetails(id) {
        this.selectedCourseId=id;
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
    mounted() {
      this.getCourses();
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
