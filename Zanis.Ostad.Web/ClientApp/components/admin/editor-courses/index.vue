<template>
  <el-card>
    <h3 style="display:inline;">تدوین</h3>
    <div class="float-right">
      <el-form :inline="true">
        <el-form-item label="جستجو">
          <el-input @change="getData" placeholder="جستجو" v-model="query.search"></el-input>
        </el-form-item>
      </el-form>
    </div>

    <el-table height="500" :data="listData.items" size="large">
      <el-table-column label="عنوان">
        <template slot-scope="scope">{{scope.row.title}}</template>
      </el-table-column>

      <el-table-column label="تاریخ">
        <template slot-scope="scope">{{scope.row.createdOn | moment("jYYYY/jM/jD HH:mm")}}</template>
      </el-table-column>

      <el-table-column label="وضعیت">
        <template slot-scope="scope">
          <el-tag class="previewState" v-if="scope.row.status===0">در انتظار ویرایش</el-tag>
          <el-tag class="previewState" v-if="scope.row.status===1" type="success">در انتظار تایید</el-tag>
          <el-tag class="previewState" v-if="scope.row.status===2" type="danger">تایید شده</el-tag>
          <el-tag class="previewState" v-if="scope.row.status===3" type="warning">تایید نشده</el-tag>
        </template>
      </el-table-column>

      <el-table-column label="عملیات">
        <template slot-scope="scope">
          <div style="display:flex;">
            <el-button type="primary">
              <a :href="scope.row.filePath" class="white">دانلود فایل ارسالی شما</a>
            </el-button>

            <el-button type="primary">
              <a :href="scope.row.courseItemFilePath" class="white">دانلود فایل اصلی</a>
            </el-button>
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
  </el-card>
</template>

<script>
import axios from "axios";
export default {
  data() {
    return {
      query: {
        pageSize: 10,
        search: ""
      },
      listData: [],
      meta: {}
    };
  },
  methods: {
    getData() {
      axios.get("/api/EditAssignment", { params: this.query }).then(res => {
        console.log(res.data);
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
    this.getData = _.debounce(this.getData, 100);
  }
};
</script>

<style>
.mt-10 {
  margin-top: 10px;
}
</style>
