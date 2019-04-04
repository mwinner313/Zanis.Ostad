<template>
  <el-dialog
    :visible.sync="isOpen"
    width="50%"
    append-to-body
    modal-append-to-body
    @closed="$emit('close')"
  >
    <div slot="title">
      <span>جستجو</span>
      <br>
      <el-alert
        :closable="false"
        type="info"
        center
        show-icon
      >لطفا با پرکردن ورودی های زیر به نسبت انتخاب کردن درسهای مرتبط به دوره ی آموزشی خود اقدام کنید</el-alert>
    </div>
    <el-container>
      <el-row :gutter="5" type="flex">
        <el-col :md="12" :lg="12">
          <el-form @submit.native.prevent>
            <el-form-item>
              <el-input placeholder="نام درس" v-model="termSearch"></el-input>
            </el-form-item>
          </el-form>
        </el-col>
        <el-col :md="12">
          <el-select v-model="selectedGradeId" filterable placeholder="مقطع">
            <el-option
              v-for="item in gradeItems"
              :key="item.value"
              :label="item.name"
              :value="item.id"
            ></el-option>
          </el-select>
        </el-col>
        <el-col :md="12">
          <!-- @change="getLessonFields" -->
          <el-select
            v-model="fieldId"
            filterable
            remote
            reserve-keyword
            placeholder="رشته"
            :remote-method="getFieldItems"
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
        <el-col :md="2">
          <el-button :disabled="canSearchForLessons" @click="getListData" type="success">بگرد</el-button>
        </el-col>
      </el-row>
      <br>
    </el-container>
    <!-- lessonItems -->
    <el-table :data="lessonItems">
      <el-table-column label="نام درس">
        <template slot-scope="scope">{{ scope.row.lessonName }}</template>
      </el-table-column>

      <el-table-column label="مقطع">
        <template slot-scope="scope">{{ scope.row.gradeName }}</template>
      </el-table-column>

      <el-table-column label="رشته">
        <template slot-scope="scope">{{ scope.row.fieldName }}</template>
      </el-table-column>

      <el-table-column label="عملیات">
        <template slot-scope="scope">
          <el-button @click="selectLessonItem(scope.row)" type="primary" class="white" plain>انتخاب</el-button>
        </template>
      </el-table-column>
    </el-table>
  </el-dialog>
</template>

<script>
import axios from "axios";
export default {
  name: "search-Lesson",
  props: {
    isOpen: {
      type: Boolean
    }
  },
  data() {
    return {
      termSearch: "",
      selectedGradeId: "",
      fieldId: "",
      gradeItems: [],
      fieldItems: [],
      lessonItems: [],
      close: false
    };
  },
  methods: {
    getlessons() {
      axios
        .get("/api/GradeFieldLessons/with-details", {
          params: {
            Term: this.termSearch,
            FieldId: this.fieldId,
            GradeId: this.selectedGradeId
          }
        })
        .then(res => {
          this.lessonItems = res.data;
        });
    },
    getFieldItems(fieldSearchTearm) {
      axios
        .get("/api/Fields", {
          params: {
            GradeId: this.selectedGradeId,
            SearchText: fieldSearchTearm
          }
        })
        .then(res => {
          this.fieldItems = res.data;
        });
    },
    selectLessonItem(id) {
      this.$emit("lessonSelected", id);
      this.$emit("close");
    },
    // changeFieldItem
    // getLessonFields() {
    //   this.getlessons();
    // },
    // getGradeItems
    getGradeItems() {
      axios.get("/api/Grades").then(res => {
        this.gradeItems = res.data;
      });
    },
    getListData() {
      this.getlessons();
    }
  },
  computed: {
    canSearchForLessons() {
      return !this.termSearch && !this.selectedGradeId && !this.fieldId;
    }
  },
  mounted() {
    this.getGradeItems();
  }
};
</script>

<style>
</style>

