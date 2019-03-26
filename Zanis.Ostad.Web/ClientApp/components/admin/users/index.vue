<template>
  <el-card>
    <h3 style="display:inline;"> کاربران</h3>
    <div class="float-right">
      <el-form :inline="true">
        <el-form-item  label="جستجو">
          <el-input @change="loadData" placeholder="جستجو" v-model="query.search"></el-input>
        </el-form-item>
        <el-form-item label="بخش">
          <el-select v-model="query.roleId" @change="loadData" placeholder="بخش">
            <el-option
              label="همه"
              value="">x
            </el-option>
            <el-option  v-for="role in roles"
                        :key="role.id"
                        :label="role.title"
                        :value="role.id">
            </el-option>

          </el-select>
        </el-form-item>

      </el-form>
    </div>
    <el-table
      height="654"
      v-loading="isLoading"
      :data="tableData"
      size="medium"
      style="width: 100%">


      <el-table-column
        label="شناسه کاربری"
        prop="userName">
      </el-table-column>

      <el-table-column
        label="نام کاربری"
        prop="fullName">
      </el-table-column>
      <el-table-column align="right">
        <template slot="header" slot-scope="scope">
          نقش
        </template>
        <template slot-scope="scope">
       <el-tag style="margin:3px" v-for="role in scope.row.roles">{{role}}</el-tag>
        </template>
      </el-table-column>

      <el-table-column align="right">
        <template slot="header" slot-scope="scope">
          عملیات
        </template>
        <template slot-scope="scope">
          <el-button @click="showUser(scope.row)" type="success" plain>مشاهده</el-button>
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
      :total="meta.allCount">
    </el-pagination>

    <UserDetailsDialog
      v-if="selectedUser"
      :item="selectedUser"
      :is-open="!!selectedUser"
      @close="selectedUser=undefined"
    ></UserDetailsDialog>
  </el-card>
</template>

<script>
  import axios from 'axios';
  import UserDetailsDialog from './user-details-dialog'
  export default {
    components:{UserDetailsDialog},
    data() {
      return {
        selectedUser: null,
        tableData: [],
        meta:{},
        isLoading: false,
        roles:[],
        query:{pageSize:10,roleId:undefined}
      }
    },
    methods: {
      showUser(user){
        this.selectedUser=user;
      },
      handleSizeChange(val) {
        this.query.pageSize = val;
        this.loadData();
      },
      handleCurrentChange(val) {
        this.query.pageOffset = (val-1) * this.query.pageSize;
        this.query.currentPage = val;
        this.loadData();
      },
      loadData(){
        this.isLoading = true;
        axios.get('/api/admin/users',{params:this.query}).then(res => {
          this.tableData = res.data.items;
          this.meta={allCount:res.data.allCount};
          this.isLoading = false;
        })
      },
    },
    mounted() {
      this.loadData();
      this.loadData = _.debounce(this.loadData, 400);
    }
  }
</script>

