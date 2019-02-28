<template>
  <div>
    <el-row :gutter="40">
      <el-col :span="6">
        <el-card class="box-card  bg-c-pink">
          <h4 class="text-white"> کل اساتید</h4>
          <i class="fas fa-chalkboard-teacher text-white"></i>
          <h4 class="card-number">۱۲۳</h4>
        </el-card>
      </el-col>
      <el-col :span="6">
        <el-card class="box-card bg-c-yellow">
          <h4 class="text-white"> کل دانشجویان</h4>
          <i class="fas fa-user-graduate text-white"></i>
          <h4 class="card-number">۱۲۳</h4>
        </el-card>
      </el-col>
      <el-col :span="6">
        <el-card class="box-card bg-c-green">
          <h4 class="text-white"> کل دوره های آموزشی</h4>
          <i class="fas fa-film text-white"></i>
          <h4 class="card-number">۱۲۳</h4>
        </el-card>
      </el-col>
      <el-col :span="6">
        <el-card class="box-card bg-c-blue">
          <h4 class="text-white"> فروش این ماه</h4>
          <i class="fas fa-money-check-alt text-white"></i>
          <h4 class="card-number">۱۲۳</h4>
        </el-card>
      </el-col>
    </el-row>
    <br>
    <el-row :gutter="40">
      <el-col :span="6">
        <el-card class="box-card  bg-c-purple ">
          <h4 class="text-white"> بازدید امروز</h4>
          <i class="fas fa-eye text-white"></i>
          <h4 class="card-number">۱۲۳</h4>
        </el-card>
      </el-col>
      <el-col :span="6">
        <el-card class="box-card bg-c-green2">
          <h4 class="text-white">تیکت های باز</h4>
          <i class="fas fa-ticket-alt text-white"></i>
          <h4 class="card-number">۱۲۳</h4>
        </el-card>
      </el-col>
      <el-col :span="6">
        <el-card class="box-card bg-c-orange">
          <h4 class="text-white"> کل دوره های آموزشی</h4>
          <i class="fas fa-film text-white"></i>
          <h4 class="card-number">۱۲۳</h4>
        </el-card>
      </el-col>
      <el-col :span="6">
        <el-card class="box-card bg-c-blue2">
          <h4 class="text-white"> فروش این ماه</h4>
          <i class="fas fa-money-check-alt text-white"></i>
          <h4 class="card-number">۱۲۳</h4>
        </el-card>
      </el-col>
    </el-row>
  </div>
</template>

<script>
  export default {
    name: ""
  }
</script>

<style scoped>
  .text-white{
    color:white;
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
  .bg-c-orange{
    background: linear-gradient(45deg, #ffc107, #f56c6c);
  }
  .bg-c-pink {
    background: linear-gradient(45deg, #FF5370, #ff869a);
  }

  .bg-c-purple {
    background: linear-gradient(45deg, #f965e7, #f5b8f9);
  }

  i{
    font-size:60px;
  }
  .card-number{
    color:white;
    font-size: 70px;
    float:left;
  }
</style>
