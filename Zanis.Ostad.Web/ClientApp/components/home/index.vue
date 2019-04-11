<template>
  <div>
    <main-header></main-header>
    <main>
      <universities></universities>
      <teach-as-teacher></teach-as-teacher>
      <visual-learning></visual-learning>
    </main>
  </div>
</template>

<script>
  import MainHeader from '../layouts/main/header/index';
  import universities from './universities';
  import TeachAsTeacher from './teach-as-teacher';
  import VisualLearning from './visual-learning';
  export default {
    name: "home",
    components: {
      TeachAsTeacher,
      VisualLearning,
      universities,
      MainHeader,
    }
  }
</script>

<style scoped>
  main {
    margin: 40px 0 0 0;
  }
</style>
