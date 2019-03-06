<template>
  <el-card>
    <div class="wrapper-form-teacher">
    <el-row >
      <el-form label-width="100px" :label-position="labelPosition">
        <el-form-item>
          <el-input v-model="Price" placeholder="قیمت"></el-input>
        </el-form-item>
        <el-form-item>
          <el-input v-model="Description" placeholder="توضیحات"></el-input>
        </el-form-item>
        <el-form-item >
          <el-select class="selectcource" v-model="CourseTitleId" filterable placeholder="دروس" @change="changeitem">
            <el-option
              v-for="item in CourceItem"
              :key="item.value"
              :label="item.name"
              :value="item.id">
            </el-option>
          </el-select>
        </el-form-item>
        <el-button @click="RegisterTeacherCource">ثبت درس</el-button>
      </el-form>
    </el-row>
    </div>
  </el-card>
</template>

<script>
  import axios from 'axios';
  import _ from 'lodash';

  export default {
    name: "",
    data() {
      return {
        labelPosition: 'right',
        CourceItem: [],
        meta: {},
        Price: '',
        Description: '',
        CourseTitleId: '',
        access: localStorage.getItem('Authorization'),
      }
    },
    methods: {
      GetCourceTitle() {
        /* let access= localStorage.getItem('Authorization');*/
        axios.get('/api/CourseTitles', {
          headers: {
            'Authorization': this.access
          }
        })
          .then(res => {
            this.CourceItem = res.data;
            console.log(this.CourceItem);
          })
      },
      RegisterTeacherCource() {
        let data = new FormData();
        data.append('Price',this.Price);
        data.append('Description',this.Description);
        data.append('CourseTitleId',this.CourseTitleId);
      },
      changeitem() {

      },
     /* uploader setting section*/
      handleRemove(file,fileList){
        console.log(file);
      },
      handlePreview(file){

      }
    },
    mounted() {
      this.GetCourceTitle();
    }
  }
</script>

<style scoped>
.selectcource{
  width: 100%;
}
</style>
