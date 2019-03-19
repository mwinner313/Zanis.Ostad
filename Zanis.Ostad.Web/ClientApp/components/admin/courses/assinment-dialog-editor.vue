<template>
  <el-dialog
    title="لیست سرفصلها"
    :visible.sync="isOpen"
    @closed="$emit('close')"
    @beforeClose="$emit('close')"
    width="60%"
  >
    <el-table :data="courseData.contents" style="width: 100%">
      <el-table-column label="ردیف">
        <template slot-scope="scope">{{scope.row.id}}</template>
      </el-table-column>
      <el-table-column label="تاریخ">
        <template slot-scope="scope">{{scope.row.createdOn| moment("jYYYY/jM/jD HH:mm")}}</template>
      </el-table-column>
      <el-table-column label="عنوان">
        <template slot-scope="scope">{{scope.row.title}}</template>
      </el-table-column>
    </el-table>
  </el-dialog>
</template>

<script>
import axios from "axios";
export default {
  data() {
    return {
      courseData: []
    };
  },
  props: {
    isOpen: {
      type: Boolean
    },
    courseId: {
      type: Number
    }
  },
  mounted() {
    this.loadData();
  },
  methods: {
    loadData() {
      axios
        .get("/api/Courses/" + this.courseId)
        .then(res => {
          this.courseData = res.data;
          console.log(res.data.contents);
        })
        .catch(err => {});
    }
  }
};
</script>

<style>
</style>
