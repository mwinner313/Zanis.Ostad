<template>
  <el-dialog
    :visible.sync="isOpen"
    width="40%"
    @closed="$emit('close')"
    append-to-body
    modal-append-to-body
    :title="item.title"
  >
    <el-form>
      <el-form-item label="انتخاب وضعیت">
        <el-select
          placeholder="انتخاب بخش"
          v-model="categoryId"
          @change="changeItem"
          width="100%"
        >
          <el-option
            v-for="cat in ticketCategories"
            :key="cat.id"
            :label="cat.title"
            :value="cat.id"
          ></el-option>
        </el-select>
      </el-form-item>
    </el-form>

    <span slot="footer" class="dialog-footer">
      <el-button @click="$emit('close')">بستن</el-button>

      <el-button type="primary" @click="sumbit">ثبت</el-button>
    </span>
  </el-dialog>
</template>

<script>
import axios from "axios";

export default {
  name: "change-category-dialog",

  props: {
    isOpen: {
      type: Boolean
    },

    item: {
      type: Object
    }
  },

  data() {
    return {
      ticketCategories: [],
      categoryId: undefined,
     
    };
  },

  methods: {
    loadCategories() {
      axios.get("/api/ticketCategory/").then(res => {
        this.ticketCategories = res.data;
      });
    },
    // changeCategory
    changeItem() {},
    sumbit() {
      axios
        .patch("/api/tickets/changecategory", {
          categoryId: this.categoryId,
          ticketId: this.item.id
        })
        .then(response => {
          this.$emit('close');
          this.$message({
            message: 'تغییر با موفقیت انجام شد',
            type: 'success'
          });
          this.$emit('onCategoryChange');
        });
    }
  },
  mounted() {
    this.loadCategories();
  }
};
</script>

<style scoped>
.el-select {
  width: 100%;
}
</style>
