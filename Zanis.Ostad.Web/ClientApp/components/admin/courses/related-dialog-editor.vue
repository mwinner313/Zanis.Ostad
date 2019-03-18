<template>
  <el-dialog title="لیست سرفصلها" :visible.sync="isOpen" width="60%">
    <el-table height="720" :data="courseData" size="large" style="width: 100%">
      <el-table-column width="50" label="ردیف">
        <template slot-scope="scope">{{scope.row.approvalStatus}}</template>
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
  methods: {
    loadData() {
      axios
        .get("/api/Courses/" + this.courseId)
        .then(res => {
          this.courseData = res.data;
          console.log(this.courseData);
        })
        .catch(err => {});
    }
  },
  mounted() {
    this.loadData();
  }
};
</script>

<style>
</style>
