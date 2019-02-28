<template>
  <div>
    <windows-app-download-box></windows-app-download-box>
    <br>
    <el-row :gutter="40">
      <el-col :span="12">
        <el-card>
          <h3 style="display:inline;">دروس من</h3>
          <el-table
            height="654"
            v-loading="isLoading"
            :data="tableData.items"
            size="medium"
            style="width: 100%">

            <el-table-column
              label="عنوان درس"
              prop="lessonName">
            </el-table-column>

            <el-table-column
              label="کد درس"
              prop="lessonCode">
            </el-table-column>

            <el-table-column align="right">
              <template slot="header">
                عملیات
              </template>
              <template slot-scope="scope">
                <el-button @click="showContentList(scope.row)" type="success" plain>مشاهده لیست نمونه سوالات</el-button>
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
            :total="tableData.allCount">
          </el-pagination>
        </el-card>
      </el-col>
      <el-col v-if="selectedLesson" :span="12">
        <el-card>
          <el-alert
            :title="selectedLesson.description"
            type="info"
            :closable="false"
            show-icon>
          </el-alert>
          <br>
          <h4 style="display:inline;">محتوای دوره</h4>
          <el-table
            v-loading="isLoading"
            :data="selectedLesson.contents"
            size="medium"
            style="width: 100%">
            <el-table-column
              label="عنوان"
              prop="title">
            </el-table-column>
          </el-table>
        </el-card>
      </el-col>
    </el-row>
  </div>

</template>

<script>
  import axios from 'axios';
  import WindowsAppDownloadBox from '../windows-app-download-box'
  export default {
    name: "",
    components:{
      WindowsAppDownloadBox
    },
    data() {
      return {
        selectedLesson :undefined,
        tableData: {items: [], allCount: 0},
        isLoading: false,
        meta: {},
        query: {pageSize: 10},
      }
    },
    mounted() {
      this.loadData()
    },
    methods: {
      handleSizeChange(val) {
        this.query.pageSize = val;
        this.loadData();
      },
      handleCurrentChange(val) {
        this.query.pageOffset = (val - 1) * this.query.pageSize;
        this.query.currentPage = val;
        this.loadData();
      },
      showContentList(item){
        this.selectedLesson = item;
      },
      loadData() {
        this.isLoading = true;
        axios.get('/api/account/courses', { params: this.query }).then(res => {
          this.tableData = res.data;
          this.isLoading = false;
        });
      }
    }
  }
</script>

<style scoped>

</style>
