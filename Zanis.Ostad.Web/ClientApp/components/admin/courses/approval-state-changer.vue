<template>
  <el-dialog
    title="Tips"
    :visible.sync="isOpen"
    width="30%"
    @closed="$emit('close')"
    :before-close="isOpen">
    <span slot="footer" class="dialog-footer">
    <el-button @click="$emit('close')">بستن</el-button>
    <el-button type="primary" @click="submit">ثبت</el-button>
  </span>
  </el-dialog>
</template>

<script>
  export default {
    name: "",
    props: {
      isOpen: {
        type: Boolean
      }
    }
  }
</script>

<style scoped>

</style>
