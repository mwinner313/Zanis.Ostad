<template>
  <div>
    <el-dialog title="رشته ها" :visible.sync="isOpen" width="80%" @closed="$emit('close')">
      <ul>
        <li v-for="field in fields" :key="field.key" @click="$emit('close')">
          <router-link
            :to="`/college/${college.id}-${college.permaLink}/grade/${grade.id}-${grade.permaLink}/field/${field.id}-${field.permaLink}`"
          >{{field.name}}</router-link>
        </li>
      </ul>
    </el-dialog>
  </div>
</template>
<script>
import axios from "axios";

export default {
  props: {
    college: {
      type: Object,
      default(){
          return {};
      }
    },
    grade: {
      type: Object,
     default(){
          return {};
      } 
    },
    isOpen: { type: Boolean }
  },
  data() {
    return { fields: [] };
  },

  methods: {
    loadFields(gradeId) {
      this.$http.get("/api/fields", { params: { gradeId } }).then(fields => {
        this.fields = fields.data;
      });
    }
  },
  watch: {
    grade(val) {
      this.loadFields(val.id);
    }
  },
  mounted() {}
};
</script>
<style scoped lang="scss">
ul {
  padding: 0px;
  list-style: none;
  display: flex;
  flex-direction: row;
  flex-wrap: wrap;
  li {
    a {
      color: rgb(58, 58, 58); transition: all 0.3s;
      &:hover {
        color: #ffcf40;
      }
    }
    width: 50%;
    font-size: 17px;
    position: relative;
    cursor: pointer;
    padding: 11px 0;

    &::before {
      content: "\2022";
      color: #ffc822;
      font-weight: bold;
      display: inline-block;
      width: 1em;
      margin-right: 10px;
    }
  }
}
</style>

