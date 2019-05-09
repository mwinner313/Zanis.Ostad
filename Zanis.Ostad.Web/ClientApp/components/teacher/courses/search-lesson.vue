<template>
  <el-dialog
    :visible.sync="isOpen"
    width="80%"
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
    <el-row :gutter="5" type="flex">
      <el-col>
        <el-form :inline="true" @submit.native.prevent>
          <el-form-item>
            <el-select v-model="selectedGradeId" filterable placeholder="مقطع">
              <el-option
                v-for="item in gradeItems"
                :key="item.value"
                :label="item.name"
                :value="item.id"
              ></el-option>
            </el-select>
          </el-form-item>
          <el-form-item>
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
          </el-form-item>
          <el-form-item>
            <el-input placeholder=" نام درس یا کد درس" v-model="termSearch"></el-input>
          </el-form-item>
          <el-button :disabled="canNotSearchForLessons" @click="getListData" type="success">بگرد</el-button>
        </el-form>
      </el-col>
    </el-row>
    <br>
    <el-row :gutter="10">
      <el-col v-show="lessonItems.length" :md="12" :lg="12">
        <el-card>
          <p>
            دروس موجود
            <el-button @click="addLessonToList" type="primary" class="float-right">افزودن</el-button>
          </p>
          <el-table :data="lessonItems">
            <el-table-column>
              <template slot-scope="scope">
                <el-checkbox name="type" :label="scope.row.id" v-model="selectedLessons" border></el-checkbox>
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
        </el-card>
      </el-col>
      <el-col :md="12" :lg="12" v-show="finalySelectedItems.length">
        <el-card>
          <p>
            دروس انتخابی شما
            <button @click="submit" class="xanis-btn float-right">ثبت</button>
          </p>
          <el-table :data="finalySelectedItems">
            <el-table-column label="نام درس">
              <template slot-scope="scope">{{ scope.row.lessonName }}</template>
            </el-table-column>

            <el-table-column label="مقطع">
              <template slot-scope="scope">{{ scope.row.gradeName }}</template>
            </el-table-column>

            <el-table-column label="رشته">
              <template slot-scope="scope">{{ scope.row.fieldName }}</template>
            </el-table-column>

            <el-table-column label>
              <template slot-scope="scope">
                <i class="fas fa-trash-alt pointer" @click="deleteItemSelected(scope.row.id)"></i>
              </template>
            </el-table-column>
          </el-table>
        </el-card>
      </el-col>
    </el-row>
  </el-dialog>
</template>

<script>
import axios from "axios";

export default {
  name: "search-Lesson",
  props: {
    isOpen: {
      type: Boolean
    },
    preSelectedItems: {
      type: Array,
      default() {
        return [];
      }
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
      selectedLessonDelete: [],
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
    getGradeItems() {
      axios.get("/api/Grades").then(res => {
        this.gradeItems = res.data;
      });
    },
    getListData() {
      this.getlessons();
    },
    addLessonToList() {
      this.finalySelectedItems.push(
        ...this.lessonItems.filter(item =>
          this.selectedLessons.some(
            select =>
              item.id === select &&
              !this.finalySelectedItems.some(x => x.id === select)
          )
        )
      );
      this.selectedLessons = [];
    },
    deleteItemSelected(id) {
      this.finalySelectedItems = this.finalySelectedItems.filter(
        item => item.id !== id
      );
    },
    submit() {
      this.$emit("lessonsSelected", this.finalySelectedItems);
      this.$emit("close");
    }
  },
  computed: {
    canNotSearchForLessons() {
      return !this.termSearch || !this.selectedGradeId || !this.fieldId;
    }
  },
  watch: {
    preSelectedItems(val) {
      this.finalySelectedItems = val;
    }
  },
  mounted() {
    this.getGradeItems();
  }
};
</script>

<style>
.pointer {
  cursor: pointer;
}
</style>

