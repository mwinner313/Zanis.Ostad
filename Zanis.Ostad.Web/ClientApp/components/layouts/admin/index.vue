<template>
  <el-container class="admin-wrapper">
    <el-header class="header" style="padding:0px;background-color:#409eff; position:relative">
      <span class="menu-toggle" @click="toggleSideMenu"><i class="el-icon-menu"></i> </span>
      <span>استاد زانیس</span>
      <div style="position: absolute;
     left: 23px;
     top: 22px;">
      <span style="cursor:pointer;">
        <el-badge :value="3" class="item">
          user   <i class="fas fa-user"></i>
        </el-badge>
      </span>
      </div>
    </el-header>
    <el-container>
      <el-aside :width="asideWidth">
        <el-menu default-active="2" ref="sideMenu"  class="el-menu-vertical-demo"
                 :collapse="isCollapse">
          <!--<el-submenu index="1">-->
          <!--<template slot="title">-->
          <!--<i class="el-icon-location"></i>-->
          <!--<span slot="title">Navigator One</span>-->
          <!--</template>-->
          <!--<el-menu-item-group>-->
          <!--<span slot="title">Group One</span>-->
          <!--<el-menu-item index="1-1">item one</el-menu-item>-->
          <!--<el-menu-item index="1-2">item two</el-menu-item>-->
          <!--</el-menu-item-group>-->
          <!--<el-menu-item-group title="Group Two">-->
          <!--<el-menu-item index="1-3">item three</el-menu-item>-->
          <!--</el-menu-item-group>-->
          <!--<el-submenu index="1-4">-->
          <!--<span slot="title">item four</span>-->
          <!--<el-menu-item index="1-4-1">item one</el-menu-item>-->
          <!--</el-submenu>-->
          <!--</el-submenu>-->
          <router-link to="/admin/dashboard">
            <el-menu-item index="2">
              <i class="el-icon-menu"></i>
              <span slot="title">داشبورد</span>
            </el-menu-item>
          </router-link>
          <router-link to="/admin/tickets">
            <el-menu-item index="1">
              <i class="el-icon-tickets"></i>
              <span slot="title">تیکت ها</span>
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
.page-content{
  margin:0px;
}
  .admin-wrapper {
    width: 100%;
    height: 100%;
    overflow: hidden;
  }
</style>

<script>
  export default {

    data() {
      return {
        asideWidth: '200px',
        isCollapse: false,
        form: {
          name: '',
          region: '',
          date1: '',
          date2: '',
          delivery: false,
          type: [],
          resource: '',
          desc: ''
        }
      };
    },
    mounted() {
      if (window.location.pathname === "/admin")
        this.$router.push({name: 'dashboard', params: {}})
    },
    methods: {
      toggleSideMenu() {
        this.isCollapse = !this.isCollapse;
        window.setTimeout(() =>
          this.asideWidth = this.isCollapse ? "67px" : '203px', 100)
      },

    },

  }
</script>
