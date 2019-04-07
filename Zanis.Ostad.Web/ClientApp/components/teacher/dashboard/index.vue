<template>
  <div>
    <el-row :gutter="10">
      <el-col v-for="item in courseTitles" :span="12">
        <el-card class="box-card">
          <h4 class="clearfix "> {{item.name}}</h4>
          <el-row :gutter="50">
            <br>
            <el-col style="text-align: center;" :span="6">
              <img :src="item.imagePath" width="162" alt="">
            </el-col>
            <el-col  :span="18">
              <p  class="title-description">  {{item.description}}</p>
            </el-col>
          </el-row>
          <h4 class="card-number">
            <el-button @click="selectedCourseTitleId=item.id" type="success" plain>شروع آموزش</el-button>
          </h4>
        </el-card>
      </el-col>
    </el-row>
    <AddCourseDialog :preSelectedCourseTitleId="selectedCourseTitleId" @close="selectedCourseTitleId=undefined" :isOpen="!!selectedCourseTitleId"></AddCourseDialog>
  </div>
</template>

<script>
  import AddCourseDialog from './../courses/add-course-dialog'
  export default {
    name: "",
    components:{
      AddCourseDialog
    },
    data() {
      return {
        selectedCourseTitleId:undefined,
        courseTitles: []
      }
    },
    mounted() {
      this.$http.get('/api/courseTitles').then(res => {
        this.courseTitles = res.data
      });
    }
  }
</script>

<style scoped>
  .text-white {
    color: white;
  }

  .bg-c-blue {
    background: linear-gradient(45deg, #4099ff, #73b4ff);
  }

  .bg-c-green {
    background: linear-gradient(45deg, #2ed8b6, #59e0c5);
  }

  .bg-c-green2 {
    background: linear-gradient(45deg, #59ad6c, #b1e895);
  }

  .bg-c-blue2 {
    background: linear-gradient(45deg, #17a2b8, #b1e895);
  }

  .bg-c-yellow {
    background: linear-gradient(45deg, #FFB64D, #ffcb80);
  }

  .bg-c-orange {
    background: linear-gradient(45deg, #ffc107, #f56c6c);
  }

  .bg-c-pink {
    background: linear-gradient(45deg, #FF5370, #ff869a);
  }

  .bg-c-purple {
    background: linear-gradient(45deg, #f965e7, #f5b8f9);
  }

  i {
    font-size: 60px;
  }

  .card-number {
    color: white;
    font-size: 70px;
    float: left;
  }
  .title-description{
    text-align: justify;
    padding-top:10px;
  }
  .box-card{
    margin-top: 10px;
  }
</style>
