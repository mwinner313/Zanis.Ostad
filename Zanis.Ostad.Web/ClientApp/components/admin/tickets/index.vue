<template>
  <el-card>
    <h3 style="display:inline;">تیکت ها</h3>
    <div class="float-right">
      <el-form :inline="true">
        <el-form-item  label="جستجو">
          <el-input @change="loadData" placeholder="جستجو" v-model="query.search"></el-input>
        </el-form-item>
        <el-form-item label="بخش">
          <el-select v-model="query.categoryId" @change="loadData" placeholder="بخش">
            <el-option
              label="همه"
              value="">
            </el-option>
            <el-option  v-for="cat in ticketCategories"
               :key="cat.id"
              :label="cat.title"
              :value="cat.id">
            </el-option>

          </el-select>
        </el-form-item>
        <el-form-item label="وضعیت">
          <el-select v-model="query.state" @change="loadData" placeholder="وضعیت">
            <el-option
              label="همه"
              value="">
            </el-option>
            <el-option
              label="باز"
              value="0">
            </el-option>
            <el-option
              label="بسته"
              value="1">
            </el-option>
          </el-select>
        </el-form-item>
        <el-form-item>
          <el-checkbox label="خوانده نشده ها" v-model="query.notSeen" @change="loadData"></el-checkbox>
        </el-form-item>
      </el-form>
    </div>
    <el-table
      height="654"
      v-loading="isLoading"
      :row-class-name="tableRowClassName"
      :data="tableData"
      size="medium"
      style="width: 100%">

      <el-table-column
        label="عنوان"
        prop="ticketReason">
      </el-table-column>

      <el-table-column
        label="شناسه کاربری"
        prop="userUserName">
      </el-table-column>

      <el-table-column
        label="نام کاربری"
        prop="userFullName">
      </el-table-column>

      <el-table-column label="وضعیت">
        <template
          slot-scope="{row}">
          <el-tag v-if="row.state===1" type="success">بسته</el-tag>
          <el-tag v-if="row.state===0"> &nbsp;&nbsp; باز&nbsp;&nbsp;</el-tag>
        </template>
      </el-table-column>

      <el-table-column
        label="بخش"
        prop="categoryTitle">
      </el-table-column>
      <el-table-column
        label="تاریخ ثبت">
        <template slot-scope="scope">
          {{ scope.createdOn | moment("jYYYY/jM/jD HH:mm") }}
        </template>
      </el-table-column>
      <el-table-column label="پیام خوانده نشده">
        <template slot-scope="scope" v-if="scope.row.operatorUnReadedMessagesCount">
          {{ scope.row.operatorUnReadedMessagesCount }}
        </template>
      </el-table-column>
      <el-table-column align="right">
        <template slot="header" slot-scope="scope">
         عملیات
        </template>
        <template slot-scope="scope">
          <el-button @click="showTicketItems(scope.row)" type="success" plain>مشاهده</el-button>
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
    <messenger @ticketstatechange="changeState" v-if="!!selectedTicket" @close="selectedTicket=undefined"
               :isOpen="!!selectedTicket" :ticket="selectedTicket"></messenger>
  </el-card>
</template>

<script>
  import axios from 'axios';
  import Messenger from './messenger';

  export default {
    components: {Messenger},
    data() {
      return {
        selectedTicket: null,
        tableData: [],
        meta:{},
        isLoading: false,
        ticketCategories:[],
        query:{state:'0',notSeen:false,pageSize:10}
      }
    },
    methods: {
      handleSizeChange(val) {
        this.query.pageSize = val;
        this.loadData();
      },
      handleCurrentChange(val) {
        this.query.pageOffset = (val-1) * this.query.pageSize;
        this.query.currentPage = val;
        this.loadData();
      },
      changeState(data) {
        this.tableData.find(x => x.id === this.selectedTicket.id).state = data;
      },
      tableRowClassName({row}) {
        if (row.operatorUnReadedMessagesCount > 0)
          return 'warning-row';
        return '';
      },
      loadData(){
        this.isLoading = true;
        axios.get('/api/tickets',{params:this.query}).then(res => {
          this.tableData = res.data.items;
          this.meta=res.data.metaData
          this.isLoading = false;
        })
      },
      showTicketItems(ticket) {
        this.isLoading = true;
        axios.get('/api/tickets/' + ticket.id).then(res => {
          ticket.operatorUnReadedMessagesCount = 0;
          this.selectedTicket = res.data;
          this.isLoading = false;
        })
      },
      loadTicketCategories(){
        axios.get('/api/ticketCategory/' ).then(res => {
          this.ticketCategories=res.data;
        })
      }
    },
    mounted() {
      this.loadTicketCategories();
      this.loadData();
      this.loadData = _.debounce(this.loadData, 400);
    }
  }
</script>

<style>
  .pagenation {
    margin-top: 15px;
  }

  .el-table .warning-row {
    background: oldlace;
  }
</style>
