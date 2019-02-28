<template>
  <el-card>
    <h3 style="display:inline;">تیکت ها</h3>
    <div class="float-right">
      <el-button @click="isNewTicketDialogOpen=true" type="primary" plain>جدید +</el-button>
    </div>
    <el-table
      height="654"
      v-loading="isLoading"
      :row-class-name="tableRowClassName"
      :data="tableData"
      size="medium"
      style="width: 100%">

      <el-table-column label="عنوان">
        <template slot-scope="scope">
          {{scope.row.ticketReason}}
          <el-badge style="margin-top: 14px;" v-if="scope.row.ticketOwnerUnReadedMessagesCount" :value="scope.row.ticketOwnerUnReadedMessagesCount"/>
        </template>
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
          <i class="el-icon-time"></i>
          <span>{{scope.row.createdOn | moment("jYYYY/jM/jD HH:mm")}}</span>
        </template>
      </el-table-column>
      <el-table-column align="right">
        <template slot="header">
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
    <AddTicketDialog @close="reloadDataAfterAddingTicket" :isOpen="isNewTicketDialogOpen"></AddTicketDialog>
  </el-card>
</template>

<script>
  import axios from 'axios';
  import Messenger from './messenger';
  import AddTicketDialog from './addTicketDialog'
  import EventBus from '../../../event-bus'

  export default {
    components: {
      Messenger,
      AddTicketDialog
    },
    data() {
      return {
        selectedTicket: null,
        tableData: [],
        meta: {},
        isLoading: false,
        ticketCategories: [],
        query: {state: '0', notSeen: false, pageSize: 10},
        isNewTicketDialogOpen: false
      }
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

      changeState(data) {
        this.tableData.find(x => x.id === this.selectedTicket.id).state = data;
      },
      tableRowClassName({row}) {
        if (row.ticketOwnerUnReadedMessagesCount > 0)
          return 'warning-row';
        return '';
      },
      loadData() {
        this.isLoading = true;
        axios.get('/api/account/tickets', {params: this.query}).then(res => {
          this.tableData = res.data.items;
          this.meta = res.data.metaData;
          this.isLoading = false;
        });
      },
      reloadDataAfterAddingTicket() {
        this.isNewTicketDialogOpen = false;
        this.loadData();
      },
      showTicketItems(ticket) {
        this.isLoading = true;
        axios.get('/api/account/tickets/' + ticket.id).then(res => {
          if (ticket.ticketOwnerUnReadedMessagesCount)
            EventBus.$emit('userOpenedUnReadTicketItem');
          ticket.ticketOwnerUnReadedMessagesCount = 0;
          this.selectedTicket = res.data;
          this.isLoading = false;
        });
      },
      loadTicketCategories() {
        axios.get('/api/ticketCategory/').then(res => {
          this.ticketCategories = res.data;
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
