<template>
  <el-dialog
    title="جستجو"
    :visible.sync="isOpen"
    width="46%"
    append-to-body
    modal-append-to-body
    @closed="$emit('close')"
  >
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
          <el-button @click="selectLessonItem(scope.row)" type="primary" class="white" plain>انتخاب</el-button>
        </template>
      </el-table-column>
    </el-table>
  </el-dialog>
</template>

<script>
import axios from "axios";
export default {
  name: "test",
  props: {
    isOpen: {
      type: Boolean
    }
  },
  data() {
    return {
      termSearch: "",
      selectedGrade: "",
      fieldId: "",
      gradeItems: [],
      fieldItems: [],
      lessonItems: []
    };
  },
  methods: {
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
        });
    },
    getFieldItems(fieldSearchTearm) {
      axios
        .get("/api/Fields", {
          params: { GradeId: this.selectedGrade, SearchText: fieldSearchTearm }
        })
        .then(res => {
          this.fieldItems = res.data;
        });
    },
    selectLessonItem(id) {
      this.$emit("lessonSelected", id);
    
    },
    // changeFieldItem
    getLessonFields() {
      this.getlessons();
    },
    // getGradeItems
    getGradeItems() {
      axios.get("/api/Grades").then(res => {
        this.gradeItems = res.data;
      });
    }
  },
  mounted() {
    this.getGradeItems();
  }
};
</script>

<style>
</style>

