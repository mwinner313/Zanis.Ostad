<template>
  <header class="main-header small-header">
    <main-nav></main-nav>
    <div id="header-content-wrapper">
      <h3>{{title}}</h3>
    </div>
  </header>
</template>

<script>
  import LoginDialog from './login-dialog'
  import MainNav from './main-nav'

  export default {
    name: "",
    props:{
      title:String
    },
    components: {
      'login-dialog': LoginDialog,
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
