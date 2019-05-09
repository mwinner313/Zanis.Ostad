<template>
  <el-dialog
    title="ویرایش دوره"
    :visible.sync="isOpen"
    @closed="$emit('close')"
    width="40%"
    append-to-body
  >
    <el-form ref="form" :model="form">
      <el-form-item
        prop="title"
        :rules="[{ required: true, message: 'عنوان الزامی می باشد'}]"
        label="عنوان"
      >
        <el-input v-model="form.title"></el-input>
      </el-form-item>

      <el-form-item
        prop="description"
        :rules="[{ required: true, message: 'توضیحات دوره الزامی می باشد'}]"
        label="توضیحات دوره"
      >
        <el-input type="textarea" :rows="4" v-model="form.description"></el-input>
      </el-form-item>

      <el-form-item
        prop="price"
        :rules="[{ required: true, message: 'قیمت الزامی می باشد'}]"
        label="قیمت"
      >
        <el-input v-model="form.price"></el-input>
      </el-form-item>

      <el-form-item
        prop="courseCategoryId"
        :rules="[{ required: true, message: 'انتخاب کردن عنوان نوع دوره الزامی می باشد'}]"
        label="نوع دوره"
      >
        <el-select v-model="form.courseCategoryId" placeholder="نوع دوره" class="w100">
          <el-option
            v-for="item in courseCategories"
            :key="item.value"
            :label="item.name"
            :value="item.id"
          ></el-option>
        </el-select>
      </el-form-item>
      <el-card shadow="never">
        <el-form-item>
          <el-button type="warning" class="w100" @click="isLessonsearchDialog = true">انتخاب درس</el-button>
          <el-tag
            class="w100"
            type="danger"
            v-if="!selectedLessons.length"
          >در حال حاظر درسی را انتخاب نکرده اید</el-tag>
        </el-form-item>
        <el-table v-if="selectedLessons.length" :data="selectedLessons">
          <el-table-column label="نام درس">
            <template slot-scope="scope">{{ scope.row.lessonName }}</template>
          </el-table-column>

          <el-table-column label="مقطع">
            <template slot-scope="scope">{{ scope.row.gradeName }}</template>
          </el-table-column>

          <el-table-column label="رشته">
            <template slot-scope="scope">{{ scope.row.fieldName }}</template>
          </el-table-column>
        </el-table>
      </el-card>
    </el-form>
    <SearchLesson
      :isOpen="isLessonsearchDialog"
      @close="isLessonsearchDialog=false"
      @lessonsSelected="selectLessons"
      :preSelectedItems="selectedLessons"
    ></SearchLesson>
    <span slot="footer" class="dialog-footer">
      <el-button @click="$emit('close')">بستن</el-button>

      <el-button @click="registerLesson" type="success">ثبت</el-button>
    </span>
  </el-dialog>
</template>
<script>
import axios from "axios";
import SearchLesson from "./search-lesson";
export default {
  name: "",
  components: {
    SearchLesson
  },
  data() {
    return {
      form: {},
      courseCategories: [],
      selectedLessons: [],
      isLessonsearchDialog: false
    };
  },
  props: {
    urlToCall: {
      type: String
    },
    isOpen: {
      type: Boolean
    },
    courseId: {
      type: Number
    }
  },
  methods: {
    selectLessons(lessons) {
      this.selectedLessons = lessons;
    },
    registerLesson() {
      if (this.selectedLessons.length == 0) {
        this.$message({
          type: "error",
          message: "کاربر گرامی برای این درس حداقل یک درس را باید اتخاب کنید"
        });
        return false;
      }
      this.$refs.form.validate(valid => {
        if (valid) {
          this.form.lessonFieldIds = this.selectedLessons.map(x => x.id);
          axios
            .put(this.urlToCall, {
              courseId: this.courseId,
              ...this.form
            })
            .then(res => {
              this.$message({
                message: "انجام شد",
                type: "success"
              });
              this.$emit("close", true);
            });
        }
      });
    },
    getCourseCategories() {
      axios.get("/api/courseCategories").then(res => {
        this.courseCategories = res.data;
      });
    }
  },
  watch: {
    async courseId(val) {
      let { data: course } = await this.$http.get(
        "/api/TeacherAccount/courses/" + val
      );
      this.form = course;
      this.selectedLessons = course.relatedLessonFields;
    }
  },
  mounted() {
    this.getCourseCategories();
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
