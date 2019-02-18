<template>
  <div>
    <div class="wrapper">
      <router-view></router-view>
      <main-layout-footer></main-layout-footer>
      <login-dialog></login-dialog>
      <register-dialog></register-dialog>
      <ResetPassDialog></ResetPassDialog>
    </div>
  </div>
</template>

<script>
  import Footer from "./footer/index"
  import LoginDialog from './header/login-dialog'
  import RegisterDialog from './header/register-dialog'
  import ResetPassDialog from './header/reset-password-dialog'

  export default {
    name: "",
    components: {
      'main-layout-footer': Footer,
      LoginDialog,
      RegisterDialog,
      ResetPassDialog
    }
  }
</script>

<style scoped>

</style>
