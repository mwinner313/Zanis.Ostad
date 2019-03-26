<template>
  <el-container class="admin-wrapper">
    <el-header class="header" style="padding:0px;    background-color: #1fc8db;
            background-image: linear-gradient(141deg, rgb(93, 122, 226) 0%, rgb(31, 200, 219) 51%, rgb(44, 181, 232) 75%); position:relative">
      <span class="menu-toggle" @click="toggleSideMenu"><i class="fas fa-bars"></i></span>
      <router-link to="/">
        <img :src="logo" height="48" alt=""></router-link>
      <div style="position: absolute;
     left: 23px;
     top: 22px;">
      <span style="cursor:pointer;">
      </span>
      </div>
    </el-header>
    <el-container>
      <el-aside :width="asideWidth">
        <el-menu default-active="2" ref="sideMenu" class="el-menu-vertical-demo"
                 :collapse="isCollapse">
          <router-link to="/admin/dashboard">
            <el-menu-item index="2">
              <i class="fas fa-tachometer-alt"></i>
              <span slot="title">داشبورد</span>
            </el-menu-item>
          </router-link>
          <router-link to="/admin/users">
            <el-menu-item index="6">
              <i class="fas fa-ticket-alt"></i>
              <span slot="title">کاربران</span>
            </el-menu-item>
          </router-link>
          <router-link to="/admin/tickets">
            <el-menu-item index="1">
              <i class="fas fa-ticket-alt"></i>
              <span slot="title"> تیکت ها<el-badge v-if="unReadTicketItemCount" :value="unReadTicketItemCount"/></span>
            </el-menu-item>
          </router-link>
          <router-link to="/admin/courses">
            <el-menu-item index="3">
              <i class="fas fa-ticket-alt"></i>
              <span slot="title">دوره ها <el-badge v-if="!!coursesOverView"
                                                   :value="coursesOverView.pendingToApproveByAdmin"/></span>
            </el-menu-item>
          </router-link>

           <router-link to="/admin/editor-assignments">
            <el-menu-item index="4">
              <i class="fas fa-ticket-alt"></i>
              <span slot="title">تدوین</span>
            </el-menu-item>
          </router-link>

          <router-link to="/admin/editor-list">
            <el-menu-item index="5">
              <i class="fas fa-ticket-alt"></i>
              <span slot="title">تدوینگرها</span>
            </el-menu-item>
          </router-link>

        </el-menu>
      </el-aside>
      <el-main class="page-content">
        <router-view></router-view>
      </el-main>
    </el-container>
  </el-container>
</template>

<style>
  .el-menu-vertical-demo:not(.el-menu--collapse) {
    width: 200px;

  }

  .el-menu-vertical-demo {

    height: 100%;
  }

  .header {
    display: flex;
    flex-direction: row;
    color: white;
    align-items: center;
  }

  .menu-toggle {
    padding: 0 24px 0 40px;
    color: white;
    cursor: pointer;
  }

  .page-content {
    background-image: url('/assets/images/mooning.png');
    background-repeat: repeat;
    margin: 0px;
  }

  .admin-wrapper {
    width: 100%;
    height: 100%;
    overflow: hidden;
  }
</style>

<script>
  import EventBus from '../../../event-bus';
  import axios from 'axios';
  import logo from '../../../assets/images/ostad-glass-CMYK.png'

  export default {

    data() {
      return {
        logo: logo,
        asideWidth: '200px',
        isCollapse: false,
        unReadTicketItemCount: undefined,
        coursesOverView: undefined
      };
    },
    mounted() {
      if (window.location.pathname === "/admin")
        this.$router.push({name: 'admin-dashboard', params: {}});
      this.loadSideBarNotificationsCount();
      this.loadCoursesOverView();
      EventBus.$on('adminOpenedUnReadTicketItem', () => {
        this.loadUnReadTicketItemsCount()
      });
      EventBus.$on('course-state-change', () => {
        this.loadCoursesOverView()
      })
    },
    methods: {
      toggleSideMenu() {
        this.isCollapse = !this.isCollapse;
        window.setTimeout(() =>
          this.asideWidth = this.isCollapse ? "67px" : '203px', 100)
      },
      loadSideBarNotificationsCount() {
        this.loadUnReadTicketItemsCount()
      },
      loadUnReadTicketItemsCount() {
        axios.get('/api/tickets', {params: {pageOffset: 0, pageSize: 1}}).then(res => {
          this.unReadTicketItemCount = res.data.metaData.unReadTicketItemCount;
        });
      },
      loadCoursesOverView() {
        axios.get('/api/courses/overview').then(res => {
          this.coursesOverView = res.data;
        });
      }
    },
  }
</script>
<style>
  i {
    font-size: 20px;
    margin: 5px;
  }
</style>
