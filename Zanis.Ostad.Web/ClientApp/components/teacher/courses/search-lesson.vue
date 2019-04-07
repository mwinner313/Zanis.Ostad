<template>
  <el-dialog
    :visible.sync="isOpen"
    width="100%"
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
        show-icon>لطفا با پرکردن ورودی های زیر به نسبت انتخاب کردن درسهای مرتبط به دوره ی آموزشی خود اقدام کنید
      </el-alert>
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
        <el-col :md="10">
          <el-button :disabled="canNotSearchForLessons" @click="getListData" type="success">بگرد</el-button>
          <el-button @click="addCourseToList" type="primary" :disabled="!selectedLessons">افزودن</el-button>
        </el-col>
      </el-row>
      <br>
    </el-container>
    <!-- lessonItems -->
    <el-container>
      <el-row>
        <el-col :md="12" :lg="12">
          <el-table :data="lessonItems">
            <el-table-column>
              <template slot-scope="scope">
                <el-checkbox
                  name="type"
                  :label="scope.row.id"
                  v-model="selectedLessons"
                  border>
                </el-checkbox>
              </template>
            </el-table-column>
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
        </el-col>
        <el-col :md="12" :lg="12">
          <el-card>
            <div slot="header" class="clearfix">
              دروس انتخاب شده
            </div>
            <ul>
              <li v-for="item in finalySelectedItems">{{item.fieldName}}</li>
            </ul>
          </el-card>
        </el-col>
      </el-row>
    </el-container>
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
        selectedLessons: [],
        finalySelectedItems: [],
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
            console.log(res.data, 'item');
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
      /* selectLessonItem(lesson) {
         this.$emit("lessonSelected", lesson);
         this.$emit("close");
       },*/
      getGradeItems() {
        axios.get("/api/Grades").then(res => {
          this.gradeItems = res.data;
        });
      },
      getListData() {
        this.getlessons();
      },
      addCourseToList() {
        this.finalySelectedItems.push(this.lessonItems.filter((item) =>
          this.selectedLessons.some((select) =>
            item.id === select
          )));
        console.log(this.finalySelectedItems);
      }
    },
    computed: {
      canNotSearchForLessons() {
        return (!this.termSearch || !this.selectedGradeId || !this.fieldId);
      },
      // transferData(){
      //   return this.lessonItems.map(x=>({key:x.id,label:x.lessonName}))
      // }
    },
    mounted() {
      this.getGradeItems();
    }
  };
</script>

<style>
</style>

