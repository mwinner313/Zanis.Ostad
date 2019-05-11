<template>
  <el-row>
    <el-col :md="24" :lg="24">
      <el-card>
        <div class="block">
          <el-timeline>
            <el-timeline-item
              v-for="item in notifications"
              :key="item.id"
              :timestamp="getDate(item)"
              placement="top"
            >
              <el-card>
                <el-badge v-if="!item.isSeen" is-dot class="float-right"></el-badge>
                <p>{{item.text}}</p>
                <el-button type="info" @click="showItem(item)" class="float-right">مشاهده</el-button>
                <div class="clearfix"></div>
              </el-card>
            </el-timeline-item>
            <el-button @click="loadMore" v-if="!hideLoadMoreBtn" type="warning" >قدیمیتر</el-button>
          </el-timeline>
        </div>
      </el-card>
    </el-col>
    <CourseDetailsDialog
      :notif="selectedNotif"
      :courseId="selectedCourseId"
      @close="selectedCourseId=undefined"
    ></CourseDetailsDialog>
  </el-row>
</template>

<script>
import CourseDetailsDialog from "./course-details";
import axios from "axios";
import persianDate from "persian-date";
export default {
  name: "myMessage",
  data() {
    return {
      notifications: [],
      selectedNotif:{},
      meta: {},
      query: {
        justNewOnes: false,
        noPaginate: false,
        pageSize: 10,
        pageOffset: 0
      },
      hideLoadMoreBtn:false,
      courseId: 0,
      selectedCourseId: undefined
    };
  },
  components: {
    CourseDetailsDialog
  },
  methods: {
    loadMore() {
      this.query.pageOffset += 10;
      this.getNotifications();
    },
    getNotifications() {
      axios.get("/api/Notification", { params: this.query }).then(res => {
        this.notifications = this.notifications.concat(res.data.items);
        this.meta = { allCount: res.data.allCount };
        if(!res.data.items.length){
            this.hideLoadMoreBtn=true
        }
      });
    },
    getDate(item) {
      return new persianDate(new Date(item.createdOn)).format(
        "YYYY/MM/DD, dddd HH:mm"
      );
    },
    handleSizeChange(val) {
      this.query.pageSize = val;
      this.getNotifications();
    },
    handleCurrentChange(val) {
      this.query.pageOffset = (val - 1) * this.query.pageSize;
      this.query.currentPage = val;
      this.getNotifications();
    },
    showItem(item) {
      this.selectedNotif=item;
      switch (item.relatedItemType) {
        case 1:
          this.selectedCourseId = item.jsonExtraData.CourseId;
          break;
      }
      this.markNotificationAsRead(item);
    },
    markNotificationAsRead(item) {
      this.$http.patch("/api/notification/is_seen/" + item.id).then(res => {
        item.isSeen = true;
      });
    }
  },

  mounted() {
    this.getNotifications();
  }
};
</script>

<style>
</style>
