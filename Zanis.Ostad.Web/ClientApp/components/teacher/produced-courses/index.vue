<template>
  <el-card>
    <div class="wrapper-form-teacher">
      <el-form label-width="100px" :label-position="labelPosition">
        <el-form-item label="قیمت">
          <el-input v-model="Price"></el-input>
        </el-form-item>
        <el-form-item label="توضیحات">
          <el-input v-model="Description"></el-input>
        </el-form-item>
        <el-form-item label="آیتم درس ها">
          <el-select v-model="CourseTitleId" placeholder="Select">
            <el-option
              v-for="item in CourceItem"
              :key="item.value"
              :label="item.name"
              :value="item.id"
            >
            </el-option>
          </el-select>
        </el-form-item>
        <el-button @click="GetCourceTitle">test</el-button>
      </el-form>
    </div>
  </el-card>
</template>

<script>
  import axios from 'axios';

  export default {
    name: "",
    data() {
      return {
        labelPosition: 'right',
        CourceItem: [],
        meta:{},
        Price:'',
        Description:'',
        CourseTitleId:'',
        access:localStorage.getItem('Authorization'),
      }
    },
    methods: {
     GetCourceTitle(){
     /* let access= localStorage.getItem('Authorization');*/
      axios.get('/api/CourseTitles',{
        headers: {
          'Authorization': this.access
        }
      })
        .then(res=>{
          this.CourceItem=res.data;
          console.log(this.CourceItem);
        })
     },
      RegisterTeacherCource(){
        let data = new FormData();
      }
    },
    mounted() {
      this.GetCourceTitle();
    }
  }
</script>

<style scoped>

</style>
