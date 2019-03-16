<template>
  <el-row>
    <el-col :md="16" :lg="16">
      <el-card>
        <el-table :data="notifiData.items" style="width: 100%" height="600">
          <el-table-column label="تاریخ" width="180">
            <template slot-scope="scope">
              <i class="el-icon-time"></i>
              {{ scope.row.createdOn | moment("jYYYY/jM/jD HH:mm")}}
            </template>
          </el-table-column>

          <el-table-column label="پیام">
            <template slot-scope="scope">{{ scope.row.text}}</template>
          </el-table-column>

          <el-table-column label="وضعیت">
            <template slot-scope="scope">
              <el-button @click="showItem(scope.row)">مشاهده</el-button>
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
    </el-col>
<course-detils-dialog  v-if="selectedCourseId" :isOpen="selectedCourseId" 
    :courseId="selectedCourseId" @close="selectedCourseId=undefined"></course-detils-dialog>  
  </el-row>
</template>

<script>
import CourseDetilsDialog from '../../teacher/courses/course-details'
import axios from "axios";
export default {
  name: "myMessage",
  data() {
    return {
      notifiData: [],
      meta: {},
      query: {
        justNewOnes: false,
        noPaginate: false,
        pageSize: 10,
        pageOffset: 0
      },
      courseId: 0,
      selectedCourseId: undefined
    };
  },
  components: {
    CourseDetilsDialog
  },
  methods: {
    getNotifi() {
      axios.get("/api/Notification", { params: this.query }).then(res => {
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
    },
    showItem(item) {
      console.log(item)
      switch (item.relatedItemType) {
        case 1:
          this.selectedCourseId =item.jsonExtraData.CourseId ;
          break;
      }
    }
  },

  mounted() {
    this.getNotifi();
  }
};
</script>

<style>
</style>
