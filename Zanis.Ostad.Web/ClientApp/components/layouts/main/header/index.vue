<template>
 <div>
   <header class="main-header">
     <main-nav></main-nav>
     <div id="intro-wrapper" class="container">
       <div class="row">
         <div class="col-12">
           <h1>آموزش تصویری و تخصصی دروس دانشگاهی </h1>
           <h2>  آموزش ، آزمون، حل تمرین</h2>
           <h2>  نمونه سوالات 10 ترم</h2>
         </div>
       </div>
     </div>
   </header>
   <div class="yellow-large-separator"></div>
 </div>
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
