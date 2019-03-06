<template>
   <el-card>
     <el-table
       height="500"
       :data="courceData"
       size="medium"
       style="width: 100%">
       <el-table-column label="ردیف">
         <template slot-scope="scope">
           {{  scope.row.id}}
         </template>
       </el-table-column>

       <el-table-column label="کددرس">
         <template slot-scope="scope">
           {{  scope.row.lessonCode}}
         </template>
       </el-table-column>

       <el-table-column label="قیمت">
         <template slot-scope="scope">
           {{  scope.row.price}}
         </template>
       </el-table-column>

       <el-table-column label="عنوان">
         <template slot-scope="scope">
           {{  scope.row.title}}
         </template>
       </el-table-column>

       <el-table-column label="مقطع">
         <template slot-scope="scope">
           {{  scope.row.gradeTitle}}
         </template>
       </el-table-column>
         <el-table-column label="نام استاد">
           <template slot-scope="scope">
             {{  scope.row.teacher}}
           </template>
       </el-table-column>
     </el-table>
     <el-pagination
       class="pagenation"
       @size-change="handleSizeChange"
       @current-change="handleCurrentChange"
       :current-page.sync="query.currentPage"
       :page-sizes="[10,15,20,30]"
       :page-size="query.pageSize"
       layout="total, sizes, prev, pager, next, jumper"
       :total="meta.allCount">
     </el-pagination>
   </el-card>
</template>

<script>
  import axios from 'axios';
    export default {
        name: "AdminListCourse",
      data(){
          return{
            query:{pageSize:10, Authorization: localStorage.getItem('Authorization')},
            courceData:[],
            meta:{},
          }
      },
      methods:{
        Getcourse(){
          axios.get('/api/Courses',{params:this.query})
            .then(res=>{
             this.courceData=res.data.items;
              this.meta=res.data.allCount;
            })
            .catch(err=>{

            })
        },
        handleSizeChange(val) {
          this.query.pageSize = val;
          this.Getcourse();
        },
        handleCurrentChange(val) {
          this.query.pageOffset = (val-1) * this.query.pageSize;
          this.query.currentPage = val;
          this.Getcourse();
        },
      },
      mounted(){
          this.Getcourse();
      }
    }
</script>

<style scoped>

</style>
