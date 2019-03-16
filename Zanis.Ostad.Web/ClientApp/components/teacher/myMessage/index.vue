<template>
  <div>
    <el-row>
      <el-col :md="16" :lg="16">
        <el-table :data="notifiData.items" style="width: 100%" height="600">
          <el-table-column label="تاریخ">
            <template slot-scope="scope">
              <i class="el-icon-time"></i>
              {{ scope.row.createdOn | moment("jYYYY/jM/jD HH:mm")}}
            </template>
          </el-table-column>

          <el-table-column label="پیام">
            <template slot-scope="scope">{{ scope.row.text}}</template>
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
      </el-col>
    </el-row>
  </div>
</template>

<script>
import axios from "axios";
export default {
  name: "myMessage",
  data() {
    return {
      notifiData: [],
      notifiParams: {
        justNewOnes: false,
        noPaginate: false,
        pageSize: 0,
        pageOffset: 0
      },
      meta: {},
      query: {
        pageSize: 10
      }
    };
  },
  methods: {
    getNotifi() {
      axios
        .get("/api/Notification", { params: this.notifiParams })
        .then(res => {
          this.notifiData = res.data;
          this.meta = { allCount: res.data.allCount };
        });
    },
    handleSizeChange(val) {
      this.query.pageSize = val;
      this.getNotifi();
    },
    handleCurrentChange(val) {
      this.query.pageOffset = (val - 1) * this.query.pageSize;
      this.query.currentPage = val;
      this.getNotifi();
    }
  },

  mounted() {
    this.getNotifi();
  }
};
</script>

<style>
</style>
