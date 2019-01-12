<template>
  <div>
      <div  class="row  exam-smaples card-d">
      <div class="">  <h6 class="card-title">
        نمونه سوالات {{lesson.lessonName}}
      </h6></div>
      <h6 class="mt-5">کد درس : <span class="blue">{{lesson.lessonCode}}</span></h6>
        <div class="list">
          <ul>
            <li v-for="item in lesson.exams">{{item.title}}</li>
          </ul>
        </div>
        <br/>
        <div>
          <button v-show="!lesson.isOwnedByCurrentUser"  :disabled="isAddingToCart"
                  @click="addToCart"
                  class="btn btn-primary btn-block">
            <i class="fas fa-shopping-cart "></i>
            افزودن به سبد خرید
          </button>
        </div>
      </div>
    </div>
</template>

<script>

  import productTypes from '../../enums/prouctTypes';
  import EventBus from '../../event-bus';
  export default {
    name: "",
    props:{
      lesson:{
        type:Object
      },
    },
    data() {
      return {
        isAddingToCart: false
      }
    },
    methods: {
      async addToCart() {
        this.isAddingToCart = true;
        try {
          let res = await this.$http.post('/api/cart', {
            itemId: this.lesson.lessonId,
            itemType: productTypes.lessonExam
          });
          this.isAddingToCart = false;
          this.$swal({
            type: 'success',
            title: 'انجام شد',
            text: res.message,
            confirmButtonText: 'باشه !',
          });
          EventBus.$emit('cart-item-added');
        } catch (e) {
          this.isAddingToCart = false;
        }
      }
    },
  }
</script>

<style scoped>

</style>
