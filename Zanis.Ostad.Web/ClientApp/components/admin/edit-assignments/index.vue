<template>
  <el-card>
    <h3 style="display:inline;">تدوین</h3>
    <div class="float-right">
      <el-form :inline="true">
        <el-form-item label="جستجو">
          <el-input @change="getData" placeholder="جستجو" v-model="query.search"></el-input>
        </el-form-item>

        <el-form-item label="وضعیت">
          <el-select v-model="query.Status" @change="getData" placeholder="وضعیت">
            <el-option label="همه" value></el-option>
            <el-option label="در انتظار تدوین" value="0"></el-option>
            <el-option label="تایید شده" value="2"></el-option>
            <el-option label="رد شده" value="3"></el-option>
            <el-option label="در انتظار تایید" value="1"></el-option>
          </el-select>
        </el-form-item>
      </el-form>
    </div>

    <el-table height="700" :data="listData.items" size="large">
      <el-table-column label="عنوان">
        <template slot-scope="scope">{{scope.row.title}}</template>
      </el-table-column>

      <el-table-column label="تاریخ">
        <template slot-scope="scope">{{scope.row.createdOn | moment("jYYYY/jM/jD HH:mm")}}</template>
      </el-table-column>

      <el-table-column label="تدوینگر">
        <template slot-scope="scope">{{scope.row.editorFullName}}</template>
      </el-table-column>

      <el-table-column label="وضعیت">
        <template slot-scope="scope">
          <el-tag class="previewState" v-if="scope.row.status===0">در انتظار تدوین</el-tag>
          <el-tag class="previewState" v-if="scope.row.status===2" type="success">تایید شده</el-tag>
          <el-tag class="previewState" v-if="scope.row.status===3" type="danger">رد شده</el-tag>
          <el-tag class="previewState" v-if="scope.row.status===1" type="warning">در انتظار تایید</el-tag>
        </template>
      </el-table-column>

      <el-table-column width="450" label="عملیات">
        <template slot-scope="scope">
          <div style="display:flex;">
            <a :href="scope.row.filePath">
              <el-button v-if="scope.row.filePath" plain type="primary">دانلود فایل ارسالی</el-button>
            </a>
            <a :href="scope.row.courseItemFilePath">
              <el-button plain>دانلود فایل اصلی</el-button>
            </a>
            <el-button @click="changingStateItem=scope.row" plain type="success">تعیین وضعیت</el-button>
          </div>
        </template>
      </el-table-column>
    </el-table>
    <el-pagination
      class="pagenation"
      @size-change="handleSizeChange"
      @current-change="handleCurrentChange"
      :current-page.sync="query.currentPage"
      :page-sizes="[10,15,20,30]"
      :page-size="query.pageSize"
      layout="total, sizes, prev, pager, next, jumper"
      :total="meta.allCount"
    ></el-pagination>

    <ChangeEditStateDialog
      v-if="changingStateItem"
      :item="changingStateItem"
      :is-open="!!changingStateItem"
      @close="getData"
    ></ChangeEditStateDialog>
  </el-card>
</template>

<script>
import axios from "axios";
import ChangeEditStateDialog from "./change-edit-state-dialog";
import _ from "lodash";
export default {
  components: {
    ChangeEditStateDialog
  },
  data() {
    return {
      query: {
        pageSize: 10,
        search: "",
        Status: ""
      },
      changingStateItem: undefined,
      listData: [],
      meta: {}
    };
  },
  methods: {
    getData() {
      this.changingStateItem = undefined;
      axios.get("/api/EditAssignment", { params: this.query }).then(res => {
        this.listData = res.data;
        this.meta = { allCount: res.data.allCount };
      });
    },
    handleSizeChange(val) {
      this.query.pageSize = val;
      this.getData();
    },
    handleCurrentChange(val) {
      this.query.pageOffset = (val - 1) * this.query.pageSize;
      this.query.currentPage = val;
      this.getData();
    }
  },
  mounted() {
    this.getData();
    this.getData = _.debounce(this.getData, 500);
  }
};
</script>

<style>
.mt-10 {
  margin-top: 10px;
}
</style>
