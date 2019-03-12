<template>
  <div>
    <el-dialog title="افزودن دوره" :visible.sync="isOpen" width="80%" @closed="$emit('close')">
      <el-form>
        <el-form-item label="قیمت">
          <el-input placeholder="قیمت" v-model="price"></el-input>
        </el-form-item>

        <el-form-item label="توضیحات">
          <el-input placeholder="توضیحات" v-model="description"></el-input>
        </el-form-item>

        <el-form-item label="عناوین">
          <el-select v-model="courseTitle" placeholder="عناوین" class="w100">
            <el-option
              v-for="item in courseTitles"
              :key="item.value"
              :label="item.name"
              :value="item.id"
            ></el-option>
          </el-select>
        </el-form-item>
        <el-form-item>
          <el-button class="w100" @click="isLessonsearchDialog = true">انتخاب درس</el-button>
        </el-form-item>
        <el-form-item label="پیام به مدیریت">
          <el-input type="textarea" v-model="teacherMessage"></el-input>
        </el-form-item>
        <el-form-item>
          <el-upload
            class="upload-demo"
            ref="upload"
            action="https://jsonplaceholder.typicode.com/posts/"
            :auto-upload="false"
          >
            <el-button slot="trigger" size="small">انتخاب فایل</el-button>
            <el-button
              style="margin-left: 10px;"
              size="small"
              type="success"
              @click="submitUpload"
            >ارسال</el-button>
          </el-upload>
        </el-form-item>
      </el-form>
      <el-button @click="registerPeriod">ثبت دوره</el-button>
    </el-dialog>

    <!-- searchLessonItem -->
    <el-dialog title="جستجو" :visible.sync="isLessonsearchDialog" width="46%" @change="lessonSelected">
      <el-container>
        <el-row :gutter="5" type="flex">
          <el-col :md="12">
            <el-form>
              <el-form-item>
                <el-input placeholder="کلمه" v-model="termSearch"></el-input>
              </el-form-item>
            </el-form>
          </el-col>
          <el-col :md="12">
            <el-select v-model="selectedGrade" filterable placeholder="مقطع">
              <el-option
                v-for="item in gradeItems"
                :key="item.value"
                :label="item.name"
                :value="item.id"
              ></el-option>
            </el-select>
          </el-col>
          <el-col :md="12">
            <el-select
              @change="getLessonFields"
              v-model="fieldId"
              filterable
              remote
              reserve-keyword
              placeholder="رشته"
              :remote-method="getFieldItems"
              :loading="loading"
            >
              <el-option
                v-for="item in fieldItems"
                :key="item.id"
                :label="item.name"
                :value="item.id"
              >
                {{item.name}}
                -
                <span
                  style="color:#ff8787;font-size:12px"
                >{{item.gradeName}}</span>
              </el-option>
            </el-select>
          </el-col>
        </el-row>
        <br>
      </el-container>
      <!-- lessonItems -->
      <el-table :data="lessonItems" style="width: 100%">
        <el-table-column width="300" label="نام درس">
          <template slot-scope="scope">{{ scope.row.lessonName }}</template>
        </el-table-column>

        <el-table-column width="130" label="مقطع">
          <template slot-scope="scope">{{ scope.row.gradeName }}</template>
        </el-table-column>

        <el-table-column width="130" label="رشته">
          <template slot-scope="scope">{{ scope.row.fieldName }}</template>
        </el-table-column>

        <el-table-column width="130" label="عملیات">
          <template slot-scope="scope">
            <el-button
              @click="selectLessonItem(scope.row)"
              type="primary"
              class="white"
              plain
            >انتخاب</el-button>
          </template>
        </el-table-column>
      </el-table>
    </el-dialog>
  </div>
</template>
<script>
import axios from "axios";

export default {
  name: "add-course-dialog",
  props: {
    isOpen: {
      type: Boolean
    }
  },
  data() {
    return {
      price: "",
      description: "",
      courseTitle: "",
      teacherMessage: "",
      lessonFieldId: "",
      isLessonsearchDialog: false,
      termSearch: "",
      selectedGrade: "",
      fieldId: "",
      courseTitles: [],
      gradeItems: [],
      fieldItems: [],
      lessonItems: []
    };
  },

  methods: {
    getCourseTitle() {
      axios.get("/api/courseTitles").then(res => {
        this.courseTitles = res.data;
      });
    },
    getTermGradeInput(term) {
      axios
        .get("/api/GradeFieldLessons/with-details", { params: { term } })
        .then(res => {
          // console.log(this.fieldsItemLessons);
        });
    },
    // getGradeItems
    getGradeItems() {
      axios.get("/api/Grades").then(res => {
        this.gradeItems = res.data;
      });
    },
    getFieldItems(fieldSearchTearm) {
      axios
        .get("/api/Fields", {
          params: { GradeId: this.selectedGrade, SearchText: fieldSearchTearm }
        })
        .then(res => {
          this.fieldItems = res.data;
          console.log(this.fieldItems);
        });
    },
    getlessons() {
      axios
        .get("/api/GradeFieldLessons/with-details", {
          params: {
            Term: this.termSearch,
            FieldId: this.fieldId,
            GradeId: this.selectedGrade
          }
        })
        .then(res => {
          this.lessonItems = res.data;
          console.log(res.data);
        });
    },
    // changeFieldItem
    getLessonFields() {
      this.getlessons();
    },
    selectLessonItem(lessonItem) {
      this.$emit("lessonSelected", this.handel);
    },
    registerPeriod() {
      //let data = new FormData();
    }
  },
  mounted() {
    this.getCourseTitle();
    this.getGradeItems();
  }
};
</script>
<style>
.w100 {
  width: 100%;
}
.el-upload,
.el-upload-dragger {
  width: 100%;
}
.a {
  z-index: -1;
}
.white {
  color: #fff !important;
}
</style>

