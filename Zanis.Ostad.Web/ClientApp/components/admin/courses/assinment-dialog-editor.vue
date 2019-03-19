<template>
  <el-dialog
    title="لیست سرفصلها"
    :visible.sync="isOpen"
    @closed="$emit('close')"
    @beforeClose="$emit('close')"
    width="60%"
  >
    <el-row :gutter="20">
      <el-col :md="18" :lg="18">
        <el-card shadow="never">
          <h5 style="text-align:center">سرفصلها</h5>
          <el-table :data="courseData.contents" style="width: 100%">
            <el-table-column>
              <template slot-scope="scope">
                <el-checkbox
                  @change="selected"
                  name="type"
                  :label="scope.row.id"
                  v-model="selectedItemIds"
                  border
                >{{scope.row.title}}</el-checkbox>
              </template>
            </el-table-column>
          </el-table>
        </el-card>
      </el-col>
      <el-col :md="6" :lg="6">
        <el-card shadow="never">
          <h5 style="text-align:center">تدوینگرها</h5>
          <el-form :inline="true">
            <el-form-item>
              <el-input @change="getEditorList" placeholder="جستجو" v-model="query.search"></el-input>
            </el-form-item>
          </el-form>
          <el-table :data="editorList.items" style="width: 100%">
            <el-table-column>
              <template slot-scope="scope">{{scope.row.fullName}}</template>
            </el-table-column>
            <el-table-column>
              <template slot-scope="scope">
                <el-button
                  type="primary"
                  v-show="selectedItemIds.length"
                  @click="checkSelectedItem(scope.row.id)"
                  class="white"
                >انتخاب</el-button>
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
  data() {
    return {
      courseData: [],
      selectedItemIds: [],
      editorList: {},
      query: {
        search: "",
        pageSize: 10
      },
      editorId: 0
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
    this.getEditorList();
  },
  methods: {
    loadData() {
      axios
        .get("/api/Courses/" + this.courseId)
        .then(res => {
          this.courseData = res.data;
          this.courseData.contents = this.courseData.contents.filter(
            p => p.contentType == 0
          );
        })
        .catch(err => {});
    },
    getEditorList() {
      axios.get("/api/Editors", { params: this.query }).then(res => {
        this.editorList = res.data;
      });
    },
    selected(e) {
      console.log(this.selectedItemIds);
    },
    checkSelectedItem(editorId) {
      if (this.selectedItemIds.length == 0) {
        this.$message.error("سرفصلی را انتخاب نکرده اید");
        return;
      }

      axios
        .post("/api/EditAssignment/bycourseitem", {
          editorId,
          courseItemIds: this.selectedItemIds
        })
        .then(res => {
          if (res.data.status == 1) {
            this.$message({
              type: "success",
              message: "عملیات با موفقیت انجام شد"
            });
          }
        });
    }
  }
};
</script>

<style>
.white {
  color: #fff !important;
}
</style>
