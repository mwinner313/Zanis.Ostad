<template>
  <el-dialog title="تدوین" :visible.sync="dialogVisible" width="65%" @close="$emit('close')">
    <el-row :gutter="5">
      <el-col :md="14">
        <el-card>
          <h6>لیست سرفصل ها</h6>
          <el-table :data="course.contents">
            <el-table-column>
              <template slot-scope="{row:item}">
                <el-checkbox v-model="selectedItems" :label="item.id" border>{{item.title}}</el-checkbox>
              </template>
            </el-table-column>
          </el-table>
        </el-card>
      </el-col>
      <el-col :md="10">
        <el-card>
          <h6>لیست تدوین گر ها</h6>
          <el-table :data="editors.items">
            <el-table-column>
              <template slot-scope="{row:editor}">
                <span>{{editor.fullName}}</span>
              </template>
            </el-table-column>
            <el-table-column>
              <template slot-scope="{row:editor}">
                <el-button
                  @click="askUserForAssign(editor)"
                  class="float-right"
                  size="mini"
                  plain
                  type="success"
                >انتساب</el-button>
              </template>
            </el-table-column>
          </el-table>
        </el-card>
      </el-col>
    </el-row>
    <span slot="footer" class="dialog-footer">
      <el-button @click="$emit('close')">بستن</el-button>
    </span>
  </el-dialog>
</template>

<script>
export default {
  props: {
    courseId: {
      type: Number,
      default: () => 0
    }
  },
  data() {
    return {
      dialogVisible: false,
      editors: { items: [] },
      course: { contents: [] },
      selectedItems: []
    };
  },
  watch: {
    courseId(val) {
      this.dialogVisible = !!val;
      if (!this.dialogVisible) {
        this.selectedItems = [];
      } else {
        this.loadCourse();
      }
    }
  },
  methods: {
    async loadEditors() {
      let { data: editors } = await this.$http.get("/api/editors");
      this.editors = editors;
    },
    async loadCourse() {
      let { data: course } = await this.$http.get(
        "/api/courses/" + this.courseId
      );
      this.course = course;
      this.course.contents = course.contents.filter(x => x.contentType == 0);
    },
    askUserForAssign(editor) {
      if (!this.selectedItems.length) {
        this.$message({
          type: "warning",
          message: "لطفا سرفصل یا سرفصل های مورد نظر خود را انتخاب کنید."
        });
        return;
      }
      this.$confirm("از انتساب این موارد اطمینان دارید؟", "هشدار!", {
        type: "warning",
        confirmButtonText: "بلی",
        cancelButtonText: "خیر"
      }).then(res => {
        this.$http
          .post("/api/editassignments", {
            courseItemIds: this.selectedItems,
            editorId: editor.id
          })
          .then(res => {
            this.$message({
              type: "success",
              message: " انجام شد. " + res.data.message
            });
          });
      });
    }
  },
  mounted() {
    this.loadEditors();
  }
};
</script>

<style lang="scss" scoped>
</style>
