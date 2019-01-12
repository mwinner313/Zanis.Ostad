<template>
  <div>
    <div class="wrapper">
      <router-view></router-view>
      <main-layout-footer></main-layout-footer>
      <login-dialog></login-dialog>
      <register-dialog></register-dialog>
    </div>
  </div>
</template>

<script>
  import Footer from "./footer/index"
  import LoginDialog from './header/login-dialog'
  import RegisterDialog from './header/register-dialog'

  export default {
    name: "",
    components: {
      'main-layout-footer': Footer,
      LoginDialog,
      RegisterDialog
    }
  }
</script>

<style scoped>

</style>
