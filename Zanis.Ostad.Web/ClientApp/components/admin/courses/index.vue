<template>
  <el-card>
    <h3 style="display:inline;">دوره ها</h3>
    <div class="float-right">
      <el-form :inline="true">
        <el-form-item label="کد یا نام استاد ">
          <el-input @change="getCourses" placeholder="جستجو" v-model="query.teacherUserName"></el-input>
        </el-form-item>

        <el-form-item label="کد درس">
          <el-input @change="getCourses" placeholder="جستجو" v-model="query.lessonCode"></el-input>
        </el-form-item>

        <el-form-item label="کد رشته">
          <el-input @change="getCourses" placeholder="جستجو" v-model="query.fieldCode"></el-input>
        </el-form-item>

        <el-form-item label="مقطع">
          <el-select v-model="query.gradeId" @change="getCourses" placeholder="مقطع">
            <el-option label="همه" value></el-option>
            <el-option
              v-for="grade in grades"
              :key="grade.id"
              :label="grade.name"
              :value="grade.id"
            ></el-option>
          </el-select>
        </el-form-item>
        <el-form-item label="عنوان دوره">
          <el-select v-model="query.courseTitleId" @change="getCourses" placeholder="عنوان دوره">
            <el-option label="همه" value></el-option>
            <el-option
              v-for="title in courseTitles"
              :key="title.id"
              :label="title.name"
              :value="title.id"
            ></el-option>
          </el-select>
        </el-form-item>
        <el-form-item label="وضعیت">
          <el-select v-model="query.status" @change="getCourses" placeholder="وضعیت">
            <el-option label="همه" value></el-option>
            <el-option
              v-for="state in [{id:0,title:'در انتظار تعیین وضعیت'},
                                       {id:5,title:'تایید شده'},
                                       {id:10,title:'رد شده'},
                                       {id:15,title:'غیر فعال توسط مدرس'}]"
              :key="state.id"
              :label="state.title"
              :value="state.id"
            ></el-option>
          </el-select>
        </el-form-item>
      </el-form>
    </div>
    <el-table
      v-loading="isLoading"
      height="720"
      :data="courseData"
      size="large"
      style="width: 100%"
    >
      <el-table-column width="50" label="ردیف">
        <template slot-scope="scope">{{scope.row.id}}</template>
      </el-table-column>

      <el-table-column label="عنوان" width="350">
        <template slot-scope="scope">{{ scope.row.title}}</template>
      </el-table-column>

      <el-table-column label="نام استاد" width="100">
        <template slot-scope="scope">{{ scope.row.teacher}}</template>
      </el-table-column>

      <el-table-column label="قیمت" width="100">
        <template slot-scope="scope">{{scope.row.priceAsTomans}} تومان</template>
      </el-table-column>
      <el-table-column label="وضعیت" width="200">
        <template slot-scope="scope">
          <el-tag v-if="scope.row.approvalStatus===0">در انتظار تعیین وضعیت</el-tag>
          <el-tag v-if="scope.row.approvalStatus===5" type="success">تایید شده</el-tag>
          <el-tag v-if="scope.row.approvalStatus===10" type="danger">رد شده</el-tag>
          <el-tag v-if="scope.row.approvalStatus===15" type="warning">غیر فعال توسط مدرس</el-tag>
        </template>
      </el-table-column>
      <el-table-column label="عملیات">
        <template slot-scope="scope">
          <el-button type="primary" plain size="mini" @click="showDetails(scope.row.id)">مشاهده</el-button>
          <el-button
            type="success"
            plain
            size="mini"
            @click="changingApprovalStateItem=scope.row"
          >تغییر وضعیت</el-button>
          <el-button type="danger" plain size="mini" @click="setEdititng(scope.row.id)">تدوین</el-button>
          <a target="_blank" :href="`/course/${scope.row.id}-${scope.row.permalink}`">
            <el-button type="warning" plain size="mini">مشاهده در وبسایت</el-button>
          </a>
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
    <approval-state-changer :item="changingApprovalStateItem" @close="getCourses"></approval-state-changer>
    <course-details @close="getCourses" :courseId="selectedCourseId"></course-details>
    <AssignForEditDialog :courseId="courseToEditId" @close="setCourseToEditIdUndefined"></AssignForEditDialog>
  </el-card>
</template>

<script>
import axios from "axios";
import ApprovalStateChanger from "./approval-state-changer";
import CourseDetails from "./course-details";
import AssignForEditDialog from "./assign-for-edit-dialog";
export default {
  name: "AdminListCourse",
  components: {
    ApprovalStateChanger,
    CourseDetails,
    AssignForEditDialog
  },
  data() {
    return {
      isLoading: false,
      selectedCourseId: 0,
      courseToEditId: 0,
      grades: [],
      courseData: [],
      courseTitles: [],
      changingApprovalStateItem: {},
      query: {
        pageSize: 10
      },
      meta: {}
    };
  },
  methods: {
    setCourseToEditIdUndefined() {
      this.courseToEditId = 0;
    },
    getCourses() {
      this.isLoading = true;
      this.changingApprovalStateItem = {};
      this.selectedCourseId = 0;
      axios
        .get("/api/Courses", {
          params: this.query
        })
        .then(res => {
          this.isLoading = false;
          this.courseData = res.data.items;
          this.meta = { allCount: res.data.allCount };
        });
    },
    getGrades() {
      axios.get("/api/grades").then(res => {
        this.grades = res.data;
      });
    },
    getCourseTitles() {
      axios.get("/api/courseCategories").then(res => {
        this.courseTitles = res.data;
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
      this.selectedCourseId = id;
    },
    setEdititng(id) {
      this.courseToEditId = id;
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

  computed: {},
  mounted() {
    this.getCourses();
    this.getGrades();
    this.getCourseTitles();
  }
};
</script>

<style scoped>
.icon {
  float: left;
}
.el-button {
  margin-right: 0px;
}
.download {
  color: #000;
  float: left;
}

.card-item {
  margin-bottom: 50px;
}
</style>
