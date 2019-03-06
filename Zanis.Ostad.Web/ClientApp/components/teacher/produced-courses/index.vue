<template>
  <el-card>
    <el-pagination
      class="pagenation"
      @size-change="handleSizeChange"
      @current-change="handleCurrentChange"
      :current-page.sync="query.currentPage"
      :page-sizes="[10,15,20,30]"
      :page-size="query.pageSize"
      layout="total, sizes, prev, pager, next, jumper"
      :total="meta.allCount">
    </el-pagination>
  </el-card>
</template>

<script>
  import axios from 'axios';

  export default {
    name: "",
    data() {
      return {
        teacherData: [],
        meta:{},
      }
    },
    methods: {
      GetteacherData() {
        let storage = localStorage.getItem('Authorization');
        axios.get('/api/TeacherAccount/courses', {params: this.query}, {
          header: {
            'Authorization': storage
          }
        })
          .then(res => {
            this.teacherData = res.data.items;
          })
          .catch(err => {

          })
      },
      /*Pagination section setting*/
      handleSizeChange(val) {
        this.query.pageSize = val;
        this.GetteacherData();
      },
      handleCurrentChange(val) {
        this.query.pageOffset = (val - 1) * this.query.pageSize;
        this.query.currentPage = val;
        this.GetteacherData();
      },
    },
    mounted() {
      this.GetteacherData();
    }
  }
</script>

<style scoped>

</style>
