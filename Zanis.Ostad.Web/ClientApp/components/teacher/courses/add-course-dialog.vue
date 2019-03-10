<template>
  <el-dialog
    title="افزودن دوره"
    :visible.sync="isOpen"
    width="30%"
    append-to-body
    @closed="$emit('close')"
  >
    <el-form>
      <el-form-item>
        <el-input placeholder="قیمت" v-model="price"></el-input>
      </el-form-item>

      <el-form-item>
        <el-input placeholder="توضیحات" v-model="description"></el-input>
      </el-form-item>

      <el-form-item>
        <el-select v-model="courseTitle" placeholder="عناوین" class="w100">
          <el-option
            v-for="item in courseTitles"
            :key="item.value"
            :label="item.name"
            :value="item.id"
          ></el-option>
        </el-select>
      </el-form-item>
      <el-form-item>
        <el-input type="textarea" v-model="teacherMessage"></el-input>
      </el-form-item>
    </el-form>
    <el-button>ثبت دوره</el-button>
  </el-dialog>
</template>
<script>
import axios from "axios";
export default {
  name: "add-course-dialog",
  props: {
    isOpen: {
      type: Boolean
    }
  },
  data() {
    return {
      price: '',
      description: '',
      courseTitle: '',
      teacherMessage:'',
      courseTitles: []
    };
  },
  methods: {
    getCourseTitle() {
      axios.get("/api/courseTitles").then(res => {
        this.courseTitles = res.data;
        console.log(this.courseTitles);
      });
    }
  },
  mounted() {
    this.getCourseTitle();
  }
};
</script>
<style>
.w100 {
  width: 100%;
}
</style>

