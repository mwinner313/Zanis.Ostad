<template>
  <el-card>
    <h3 style="display:inline;">تدوینگرها</h3>
    <div class="float-right">
      <el-form :inline="true">
        <el-form-item label="جستجو">
          <el-input @change="getData" placeholder="جستجو" v-model="query.search"></el-input>
        </el-form-item>
      </el-form>
    </div>

    <el-table height="500" :data="listData.items" size="large">
      <el-table-column label="نام">
        <template slot-scope="scope">{{scope.row.fullName}}</template>
      </el-table-column>

      <el-table-column label="نام کاربری">
        <template slot-scope="scope">{{scope.row.userName}}</template>
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
      axios
        .get("/api/EditAssignment/editors", { params: this.query })
        .then(res => {
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
  }
};
</script>

<style>
</style>
