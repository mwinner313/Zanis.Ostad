<template>
  <el-dialog
    title="لیست سرفصلها"
    :visible.sync="isOpen"
    @closed="$emit('close')"
    @beforeClose="$emit('close')"
    width="60%"
  >
    <el-row :gutter="20">
      <el-col :md="16" :lg="16">
        <el-card shadow="never">
          <h5 style="text-align:center">سرفصلها</h5>
          <el-table :data="courseData.contents" style="width: 100%">
            <el-table-column>
              <template slot-scope="scope">
                <el-checkbox
                  name="type"
                  :label="scope.row.id"
                  v-model="selectedItemIds"
                  border
                >{{scope.row.title}}</el-checkbox>
              </template>
            </el-table-column>
            <el-table-column>
              <template slot-scope="scope">
                <el-tag v-if="scope.row.latestEditStatus==null" type="info">در انتظار انتساب</el-tag>
                <el-tag v-if="scope.row.latestEditStatus==2" type="success">تدوین شده</el-tag>
                <el-tag v-if="scope.row.latestEditStatus==0" type="info">در انتظار تدوین</el-tag>
                <el-tag v-if="scope.row.latestEditStatus==1" type="warning">در انتظار تعیین وضعیت</el-tag>
                <el-tag  v-if="scope.row.latestEditStatus==3" type="danger">رد شده</el-tag>
              </template>
            </el-table-column>
          </el-table>
        </el-card>
      </el-col>

      <el-col :md="8" :lg="8">
        <el-card shadow="never">

          <h5 style="text-align:center">تدوینگرها</h5>
          <el-form @submit.native.prevent :inline="true">
            <el-form-item>
              <el-input  clearable @change="getEditorList"
               placeholder="جستجو" v-model="query.search"></el-input>
            </el-form-item>
          </el-form>

          <el-table :data="editorList.items" style="width: 100%">

            <el-table-column >
              <template slot-scope="scope">{{scope.row.fullName}}</template>
            </el-table-column>

            <el-table-column>
              <template slot-scope="scope">
                <el-button
                  plain
                  type="success"
                  v-show="selectedItemIds.length"
                  @click="assignSelectedItemsToEditor(scope.row.id)"
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
        });
    },
    getEditorList() {
      axios.get("/api/Editors", { params: this.query }).then(res => {
        this.editorList = res.data;
      });
    },

    assignSelectedItemsToEditor(editorId) {
      if (this.selectedItemIds.length === 0) {
        this.$message.error("سرفصلی را انتخاب نکرده اید");
        return;
      }

      this.$confirm('از انتساب این موارد اطمینان دارید؟', 'هشدار', {
        confirmButtonText: 'بلی',
        cancelButtonText: 'خیر',
        type: 'هشدار'
      }).then(() => {
        axios
          .post("/api/EditAssignment", {
            editorId,
            courseItemIds: this.selectedItemIds
          })
          .then(res => {
            if (res.data.status == 1) {
              this.selectedItemIds=[];
              this.$message({
                type: "success",
                message: res.data.message || "عملیات با موفقیت انجام شد"
              });
            }
          });
      })


    }
  }
};
</script>

<style>

</style>
