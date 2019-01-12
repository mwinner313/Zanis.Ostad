<template>
  <div @click="select" :class="['box' ,((size==='sm')?'grow-1':'grow-2') ,(isActive?'yellow-deep':'')]">
    <a  class="product-type-image ">
      <figure class="image-1">
        <img
          :src="imagePath"
          class="image-1"
          alt=""
        />
      </figure>
    </a>
    <slot name="title"></slot>

  </div>
</template>

<script>
    export default {
        name: "",
        props:{
        imagePath:String,
        isActive:Boolean,
        size:String
      },
      methods:{
        select(){
          this.$emit('select');

        }
      }
    }
</script>

<style scoped>

</style>
