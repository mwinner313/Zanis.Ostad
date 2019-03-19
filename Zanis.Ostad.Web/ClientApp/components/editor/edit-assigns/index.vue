<template>
<el-row >
    <el-col :md="16" :lg="16">
        <el-card>
            <h4 style="display:inline;">لیست ویدئوها</h4>

            <el-table height="500" :data="editAssigns.items" size="large" style="width: 100%">

            <el-table-column label="عنوان" width="480">
                <template slot-scope="scope">
                {{scope.row.title}}
                </template>
            </el-table-column>

            <el-table-column label="وضعیت" width="180">
                <template slot-scope="scope">
                <el-tag  class="previewState" v-if="scope.row.status===0">در انتظار ویرایش</el-tag>
                <el-tag  class="previewState" v-if="scope.row.status===1" type="success">در انتظار تایید</el-tag>
                <el-tag  class="previewState" v-if="scope.row.status===2" type="danger">تایید شده</el-tag>
                <el-tag  class="previewState" v-if="scope.row.status===3" type="warning">تایید نشده</el-tag>
                </template>
            </el-table-column>


            <el-table-column label="عملیات" width="200">
                <template slot-scope="scope">
                <el-button>

                    <a :href="scope.row.courseItemFilePath" class="white">دانلود</a>
                </el-button>
                <el-button @click="uploadDialog=scope.row.courseItemId">آپلود</el-button>
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
            :total="meta.allCount"
            ></el-pagination>

        </el-card>
    </el-col>

      <upload
        v-if="uploadDialog"
       :isOpen="uploadDialog"
       :itemId="uploadDialog"
       @close="uploadDialog=undefined"
       @changeStatus="getEditAssigns"
      ></Upload>
  </el-row>
</template>

<script>


  import axios from "axios";
  import Upload from './upload-dialog'
  export default {
    name: "videoList",
    data() {
      return {
        query: {
          pageSize: 10
        },
        editAssigns: {},
        uploadDialog: false,
        meta: {}
      };

    },
    components: {
      Upload
    },
    methods: {
      getEditAssigns() {
          axios.get("/api/EditorAccount/EditAssigns", {
          params: this.query
        }).then(res => {

          this.editAssigns = res.data;
          this.meta = {allCount: res.data.allCount};
        });
      },
      handleSizeChange(val) {
        this.query.pageSize = val;
        this.getEditAssigns();
      },
      handleCurrentChange(val) {
        this.query.pageOffset = (val - 1) * this.query.pageSize;
        this.query.currentPage = val;
        this.getEditAssigns();
      },
    },

    mounted() {
      this.getEditAssigns();
    }
  };
</script>

<style scoped>
  .downloadBtn {
    float: left;
    margin-bottom: 10px;
  }

  .card-item {
    margin-bottom: 50px;
  }

  .deactive {
    margin-right: 0;
  }

  .customDownloadIcon {
    margin: 0;
    line-height: 8px;
    padding-left: 5px;
    color: #fff;
    font-size: 14px;
  }

  .mgl-17 {
    margin-left: 17px;
  }

  .left {
    float: left !important;
  }
  .previewState{
    width: 100%;
    text-align:center;
  }
  .white {
  color: #000 !important;
}
</style>
