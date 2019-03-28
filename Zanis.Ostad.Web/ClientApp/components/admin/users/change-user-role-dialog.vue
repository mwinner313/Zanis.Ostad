<template>
  <el-dialog :title="item.title" :visible.sync="isOpen" width="40%" @closed="$emit('close')">
    <el-form ref="form" :model="form">
      <el-form-item prop="roles" label="انتخاب نقش ها"></el-form-item>
      <el-checkbox v-for="role in roles" name="type" :label="role.name" v-model="user.roles" border></el-checkbox>
    </el-form>
    <span slot="footer" class="dialog-footer">
      <el-button @click="$emit('close')">بستن</el-button>
      <el-button type="primary" @click="submit">ثبت</el-button>
    </span>
  </el-dialog>
</template>

<script>
import axios from "axios";
import EventBus from "./../../../event-bus";

export default {
  name: "",
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
      form: {},
      roles: [],
      user: { roles: [] }
    };
  },
  mounted() {
    this.user = Object.assign({}, this.item);
    axios.get("/api/admin/roles").then(res => {
      this.roles = res.data;
    });
  },

  methods: {
    submit() {
      this.$refs.form.validate(valid => {
        if (valid) {
          axios
            .post("/api/Admin/change_user_roles", {
              userId: this.item.id,
              roles: this.user.roles
            })
            .then(res => {
              this.$emit("close", { reloadData: true });
            });
        }
      });
    }
  }
};
</script>

<style scoped>
.el-select {
  width: 100%;
}
</style>
