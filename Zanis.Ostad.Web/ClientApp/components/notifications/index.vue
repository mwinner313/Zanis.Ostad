<template>
  <el-row>
    <el-col :md="24" :lg="24">
      <el-card>
        <div class="block">
          <el-timeline>
            <el-timeline-item
              v-for="item in notifications.items"
              :key="item.id"
              :timestamp="getDate(item)"
              placement="top"
            >
              <el-card>
                <p>{{item.text}}</p>
              </el-card>
            </el-timeline-item>
          </el-timeline>
        </div>
      </el-card>
    </el-col>
  </el-row>
</template>

<script>
import CourseDetailsDialog from "../teacher/courses/course-details";
import axios from "axios";
import persianDate from "persian-date";
export default {
  name: "myMessage",
  data() {
    return {
      notifications: {},
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
    CourseDetailsDialog: CourseDetailsDialog
  },
  methods: {
    getNotifications() {
      axios.get("/api/Notification", { params: this.query }).then(res => {
        this.notifications = res.data;
        this.meta = { allCount: res.data.allCount };
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
      switch (item.relatedItemType) {
        case 1:
          this.selectedCourseId = item.jsonExtraData.CourseId;
          break;
      }
    }
  },

  mounted() {
    this.getNotifications();
  }
};
</script>

<style>
</style>
