<template>
  <div>
    <div class="wrapper">
      <main-nav></main-nav>
      <router-view></router-view>
      <main-layout-footer></main-layout-footer>
      <login-dialog></login-dialog>
      <ResetPassDialog></ResetPassDialog>
      <RegisterAsTeacherDialog></RegisterAsTeacherDialog>
    </div>
  </div>
</template>

<script>
  import Footer from "./footer/index"
  import MainNav from './header/main-nav'
  import LoginDialog from './header/login-dialog'
  import RegisterDialog from './header/register-dialog'
  import RegisterAsTeacherDialog from './header/register-as-teacher-dialog'
  import ResetPassDialog from './header/reset-password-dialog'

  export default {
    name: "",
    components: {
      MainNav,
      'main-layout-footer': Footer,
      LoginDialog,
      RegisterAsTeacherDialog,
      RegisterDialog,
      ResetPassDialog
    }
  }
</script>

<style lang="scss">
@import '../../../assets/_variables.scss';
@mixin mobile {
  @media screen and (max-width: $tablet-width) {
    @content;
  }
}

@mixin tablet {
  @media screen and (min-width: $tablet-width) and (max-width: $desktop-width - 1px) {
    @content;
  }
}

@mixin desktop {
  @media screen and (min-width: $desktop-width) {
    @content;
  }
}

::selection {
  background: #9475cd;
  color: white;
}

.el-button {
  margin: 0 3px !important;
}

body, html {
  direction: rtl;
  height: 100%;

}

@media only screen and (min-device-width: 768px) and (max-device-width: 1024px) {
  .modal-dialog {
    max-width: 752px !important;
  }
}
.no-wrap {
  white-space: nowrap;
}

.modal-full .modal-dialog {
  min-width: 100%;
  margin: 0;
}
.wrapper {
  height: 100%;
  width: 100%;
  overflow: hidden;
}

.modal-full .modal-dialog .modal-content {
  min-height: 100vh;
}

.text-black {
  color: black;
}

.text-orange {
  color: orange;
}

.red {
  color: #bd2130;
}

.green {
  color: green;
}


.btn {
  box-shadow: 1px 2px 10px 2px #bdbdbd;
}

.btn-primary {
  background-color: $purple !important;
  border-color: $purple !important;
  box-shadow: 1px 2px 10px 2px #bdbdbd;
}

.btn-success {

  background-color: #ffc107 !important;
  border-color: #ffc107 !important;
}

input {
  outline: none !important;
  border: 0px solid red;
  padding: 7px 10px;
  border-radius: 15px;
}

a {
  color: white;
  &:link {
    text-decoration: none;
  }
  &:hover {
    color: white;
  }
}

</style>
