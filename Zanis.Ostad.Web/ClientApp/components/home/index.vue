<template>
  <div>
    <main-header></main-header>
    <main>
      <Colleges></Colleges>
      <teach-as-teacher></teach-as-teacher>
      <visual-learning></visual-learning>
    </main>
  </div>
</template>

<script>
  import MainHeader from '../layouts/main/header/index';
  import Colleges from './colleges';
  import TeachAsTeacher from './teach-as-teacher';
  import VisualLearning from './visual-learning';

  export default {
    mounted() {
      window.document.title = "صفحه اصلی  | استاد زانیس"
    },
    name: "home",
    components: {
      TeachAsTeacher,
      VisualLearning,
      Colleges,
      MainHeader,
    }
  }
</script>

<style scoped>
  main {
    margin: 40px 0 0 0;
  }
</style>
