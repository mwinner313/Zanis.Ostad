<template>
  <el-container class="user-wrapper">
    <el-header
      class="header"
      style="padding:0px;    background-color: #1fc8db;
            background-image: linear-gradient(141deg, rgb(93, 122, 226) 0%, rgb(31, 200, 219) 51%, rgb(44, 181, 232) 75%); position:relative"
    >
      <span class="menu-toggle" @click="toggleSideMenu">
        <i class="fas fa-bars"></i>
      </span>
      <router-link to="/">
        <img :src="logo" height="48" alt>
      </router-link>
      <div style="position: absolute;
     left: 23px;
     top: 22px;">
        <span style="cursor:pointer;"></span>
      </div>
    </el-header>
    <el-container>
      <el-aside :width="asideWidth">
        <el-menu
          default-active="2"
          ref="sideMenu"
          class="el-menu-vertical-demo"
          :collapse="isCollapse"
        >
          <router-link to="/user/dashboard">
            <el-menu-item index="2">
              <i class="fas fa-tachometer-alt"></i>
              <span slot="title">داشبورد</span>
            </el-menu-item>
          </router-link>
          <router-link to="/user/tickets">
            <el-menu-item index="1">
              <i class="fas fa-ticket-alt"></i>
              <span slot="title">تیکت ها
                <el-badge v-if="unReadTicketItemCount" :value="unReadTicketItemCount"/>
              </span>
            </el-menu-item>
          </router-link>
          <router-link to="/user/courses">
            <el-menu-item index="3">
              <i class="fas fa-book-open"></i>
              <span slot="title">دروس من </span>
            </el-menu-item>
          </router-link>
          <router-link to="/user/exam-samples">
            <el-menu-item index="4">
              <i class="fas fa-file-alt"></i>
              <span slot="title">نمونه سوالات </span>
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
i {
  font-size: 20px;
  margin: 5px;
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

.user-wrapper {
  width: 100%;
  height: 100%;
  overflow: hidden;
}
</style>

<script>
import axios from "axios";
import EventBus from "../../../event-bus";
import logo from "../../../assets/images/ostad-glass-CMYK.png";

export default {
  data() {
    return {
      logo: logo,
      unReadTicketItemCount: undefined,
      asideWidth: "200px",
      isCollapse: false
    };
  },
  mounted() {
    if (window.location.pathname === "/user")
      this.$router.push({ name: "user-dashboard", params: {} });
    this.loadSideBarNotificationsCount();
    EventBus.$on("userOpenedUnReadTicketItem", () => {
      this.loadUnReadTicketItemsCount();
    });
  },
  methods: {
    toggleSideMenu() {
      this.isCollapse = !this.isCollapse;
      window.setTimeout(
        () => (this.asideWidth = this.isCollapse ? "67px" : "203px"),
        100
      );
    },
    loadSideBarNotificationsCount() {
      this.loadUnReadTicketItemsCount();
    },
    loadUnReadTicketItemsCount() {
      axios
        .get("/api/account/tickets", { params: { pageOffset: 0, pageSize: 1 } })
        .then(res => {
          this.unReadTicketItemCount = res.data.metaData.unReadTicketItemCount;
        });
    }
  }
};
</script>
