<template>
  <div>
    <main-header></main-header>
    <main class="container p-0">
      <search-box></search-box>
      <product-selector></product-selector>

    </main>
  </div>
</template>

<script>
  import productSelector from './product-selector';
  import SearchBox from './search-box';
  import MainHeader from '../layouts/main/header/index';
  export default {
    name: "home",
    components:{
      productSelector,
      SearchBox,
      MainHeader
    }
  }
</script>

<style scoped>
</style>
