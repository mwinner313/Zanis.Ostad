<template>
 <div>
   <div class="yellow-small-separator"></div>
   <div class="teach-as-teacher">
      <h3>به عنوان مدرس در استاد زانیس آموزش دهید</h3>
     <button>شروع تدریس</button>
   </div>
 </div>
</template>

<script>
    export default {
        name: ""
    }
</script>

<style scoped lang="scss">

  @import '../../assets/_variables.scss';
  .yellow-small-separator {
    height: 40px;
    background-color: $yellow;
  }



  .teach-as-teacher {
    text-align: center;
    h3 {
      text-align: center;
      color: white;
      padding-top: 50px;
      font-size: 50px;
    }
    button {
      font-size: 30px;
      margin-top: 54px;
      padding: 11px;
      transition: all 0.3s;
      font-weight: 500;
      &:hover {
        box-shadow: 1px 0px 10px yellow;
        font-size: 25px;
      }
    }
    height: 400px;
    background: url('../../assets/images/start-to-teach.png') no-repeat;
    background-size: 100% 100%;

  }

</style>
