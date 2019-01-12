<template>
  <header class="main-header">

    <main-nav></main-nav>
    <div id="header-content-wrapper">
      <!--<nav class="navbar navbar-expand-lg navbar-light ">-->
        <!--<div class="container " id="support-nav">-->
          <!--<ul class="navbar-nav mr-auto ">-->
            <!--<li class="nav-item active">-->
              <!--<a class="nav-link" href="#">درباره ما</a>-->
            <!--</li>-->
            <!--<li class="nav-item ">-->
              <!--<a class="nav-link no-margin" href="#">درباره ما</a>-->
            <!--</li>-->
          <!--</ul>-->
          <!--<div class="float-left">-->
            <!--<ul class="navbar-nav mr-auto ">-->
              <!--<li class="nav-item active ">-->
                <!--<a class="nav-link no-margin" href="#">درباره ما</a>-->
              <!--</li>-->
            <!--</ul>-->
          <!--</div>-->
        <!--</div>-->
      <!--</nav>-->
      <div class="d-flex " id="intro-image-wrapper">
        <img src="../../../../assets/images/header-lines.png" alt=""/>
      </div>
    </div>
  </header>
</template>

<script>
  import MainNav from './main-nav'
  export default {
    name: "main-layout-header",
    components: {
      MainNav
    },
    methods: {
      showLoginModal() {
        this.$root.$emit('bv::show::modal', 'login-dialog')
      }
    }
  }
</script>

<style scoped>

</style>
