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

@font-face {
  font-family: BalvardiDastnevis;
  font-style: normal;
  src: url('../../../assets/fonts/balvardidastnevisfree.eot');
  src: url('../../../assets/fonts/balvardidastnevisfree.eot?#iefix') format('embedded-opentype'), /* IE6-8 */
  url('../../../assets/fonts/balvardidastnevisfree.woff2') format('woff2'), /* FF39+,Chrome36+, Opera24+*/
  url('../../../assets/fonts/balvardidastnevisfree.woff') format('woff'), /* FF3.6+, IE9, Chrome6+, Saf5.1+*/
  url('../../../assets/fonts/balvardidastnevisfree.ttf') format('truetype');
}

@font-face {
  font-family: "EntezareZohoor B3";
  font-style: normal;
  src: url('../../../assets/fonts/entezarb3-v2-0-1-p30download-com.eot');
  src: url('../../../assets/fonts/entezarb3-v2-0-1-p30download-com.eot?#iefix') format('embedded-opentype'), /* IE6-8 */
  url('../../../assets/fonts/entezarb3-v2-0-1-p30download-com.woff2') format('woff2'), /* FF39+,Chrome36+, Opera24+*/
  url('../../../assets/fonts/entezarb3-v2-0-1-p30download-com.woff') format('woff'), /* FF3.6+, IE9, Chrome6+, Saf5.1+*/
  url('../../../assets/fonts/entezarb3-v2-0-1-p30download-com.ttf') format('truetype');
}

@font-face {
  font-family: IRANSans;
  font-style: normal;
  font-weight: bold;
  src: url('../../../assets/fonts/eot/IRANSansWeb_Bold.eot');
  src: url('../../../assets/fonts/eot/IRANSansWeb_Bold.eot?#iefix') format('embedded-opentype'), /* IE6-8 */
  url('../../../assets/fonts/woff2/IRANSansWeb_Bold.woff2') format('woff2'), /* FF39+,Chrome36+, Opera24+*/
  url('../../../assets/fonts/woff/IRANSansWeb_Bold.woff') format('woff'), /* FF3.6+, IE9, Chrome6+, Saf5.1+*/
  url('../../../assets/fonts/ttf/IRANSansWeb_Bold.ttf') format('truetype');
}

@font-face {
  font-family: IRANSans;
  font-style: normal;
  font-weight: 500;
  src: url('../../../assets/fonts/eot/IRANSansWeb_Medium.eot');
  src: url('../../../assets/fonts/eot/IRANSansWeb_Medium.eot?#iefix') format('embedded-opentype'), /* IE6-8 */
  url('../../../assets/fonts/woff2/IRANSansWeb_Medium.woff2') format('woff2'), /* FF39+,Chrome36+, Opera24+*/
  url('../../../assets/fonts/woff/IRANSansWeb_Medium.woff') format('woff'), /* FF3.6+, IE9, Chrome6+, Saf5.1+*/
  url('../../../assets/fonts/ttf/IRANSansWeb_Medium.ttf') format('truetype');
}

@font-face {
  font-family: IRANSans;
  font-style: normal;
  font-weight: 300;
  src: url('../../../assets/fonts/eot/IRANSansWeb_Light.eot');
  src: url('../../../assets/fonts/eot/IRANSansWeb_Light.eot?#iefix') format('embedded-opentype'), /* IE6-8 */
  url('../../../assets/fonts/woff2/IRANSansWeb_Light.woff2') format('woff2'), /* FF39+,Chrome36+, Opera24+*/
  url('../../../assets/fonts/woff/IRANSansWeb_Light.woff') format('woff'), /* FF3.6+, IE9, Chrome6+, Saf5.1+*/
  url('../../../assets/fonts/ttf/IRANSansWeb_Light.ttf') format('truetype');
}

@font-face {
  font-family: IRANSans;
  font-style: normal;
  font-weight: 200;
  src: url('/fonts/eot/IRANSansWeb_UltraLight.eot');
  src: url('../../../assets/fonts/eot/IRANSansWeb_UltraLight.eot?#iefix') format('embedded-opentype'), /* IE6-8 */
  url('../../../assets/fonts/woff2/IRANSansWeb_UltraLight.woff2') format('woff2'), /* FF39+,Chrome36+, Opera24+*/
  url('../../../assets/fonts/woff/IRANSansWeb_UltraLight.woff') format('woff'), /* FF3.6+, IE9, Chrome6+, Saf5.1+*/
  url('../../../assets/fonts/ttf/IRANSansWeb_UltraLight.ttf') format('truetype');
}

@font-face {
  font-family: IRANSans;
  font-style: normal;
  font-weight: normal;
  src: url('../../../assets/fonts/eot/IRANSansWeb.eot');
  src: url('../../../assets/fonts/eot/IRANSansWeb.eot?#iefix') format('embedded-opentype'), /* IE6-8 */
  url('../../../assets/fonts/woff2/IRANSansWeb.woff2') format('woff2'), /* FF39+,Chrome36+, Opera24+*/
  url('../../../assets/fonts/woff/IRANSansWeb.woff') format('woff'), /* FF3.6+, IE9, Chrome6+, Saf5.1+*/
  url('../../../assets/fonts/ttf/IRANSansWeb.ttf') format('truetype');
}

*,
h1,
h2,
h3,
h4,
h5,
h6 {
  font-family: IRANSans;
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

button {
  outline: none !important;
  &:focus {
    outline: none !important;
  }
  cursor: pointer;
  border: 0px solid red;
  padding: 7px 10px;
  border-radius: 3px;
  color: #717171;
  background-color: #efe778;
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
