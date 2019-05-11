<template>
  <el-dialog :visible.sync="isOpen" @closed="$emit('close')" width="60%">
    <div slot="title">{{notif.text}}</div>
    <h5>عنوان</h5>
    <span>{{course.title}}</span>
    <br>
    <br>
    <br>
    <h5>وضعیت</h5>
    <el-tag v-if="course.approvalStatus===0">در انتظار تعیین وضعیت</el-tag>
    <el-tag v-if="course.approvalStatus===5" type="success">تایید شده</el-tag>
    <el-tag v-if="course.approvalStatus===10" type="danger">رد شده</el-tag>
    <el-tag v-if="course.approvalStatus===15" type="warning">غیر فعال توسط مدرس</el-tag>
    <br>
    <br>
    <br>
    <h5>متن پیام</h5>
    <span>{{course.adminMessageForTeacher}}</span>
  </el-dialog>
</template>

<script>
export default {
  props: {
    courseId: { type: Number },
    notif: {
      type: Object,
      default() {
        return {};
      }
    }
  },
  data() {
    return { isOpen: false };
  },
  watch: {
    courseId(val) {
      this.isOpen = !!val;
      if (this.isOpen) {
        this.loadCourse();
      }
    }
  },
  methods: {
    async loadCourse() {
      let { data: course } = await this.$http.get(
        "/api/courses/" + this.courseId
      );
      this.course = course;
    }
  }
};
</script>

<style lang="scss" scoped>
</style>