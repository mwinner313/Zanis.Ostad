<template>
  <div>
    <el-row :gutter="40">
      <el-col v-for="item in courseTitles" :span="6">
        <el-card class="box-card bg-c-blue">
          <i class="fas fa-file-alt text-white float-right "></i>
          <h4 class="clearfix text-white"> {{item.name}}</h4>
          <p  class="title-description">{{item.description}}</p>
          <h4 class="card-number">
            <el-button @click="selectedCourseTitleId=item.id" type="success" plain>شروع آموزش</el-button>
          </h4>
        </el-card>
      </el-col>
    </el-row>
    <AddCourseDialog preSelectedCourseTitleId="selectedCourseTitleId" @close="selectedCourseTitleId=undefined" :isOpen="!!selectedCourseTitleId"></AddCourseDialog>
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
    color:white;
    text-align: justify;
    padding-top:10px;
  }
</style>
