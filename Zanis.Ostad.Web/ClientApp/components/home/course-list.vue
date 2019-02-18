<template>
  <section class="row  courses ">
   <div class="card">
     <div class="ml-3">
       <h5 v-if="lesson.courses.length" class="card-title">دوره های آموزشی</h5></div>
     <br v-if="lesson.courses.length"/>
     <br v-if="lesson.courses.length"/>
     <div class="list container">
       <h5 class="text-center" v-show="!lesson.courses.length">هنوز هیچ دوره آموزشی برای این درس وجود ندارد .</h5>
       <div class="row no-gutters">
         <div v-for="course in lesson.courses" class=" col-md-12 ">
           <div class="course">
             <router-link :to="'/course/'+course.id">
               <h6 class="text-black">{{course.title}}</h6>
             </router-link>
             <span>مدرس : {{course.teacher}}</span>
             <div class="price-cart">
               <span class="price"> {{course.priceAsTomans |currency}} تومان</span>
               <div>
                 <!--<button class="float-right btn-buy ">-->
                 <!--<i class="fas fa-shopping-cart "></i> &nbsp; افزودن به-->
                 <!--سبد خرید-->
                 <!--</button>-->
                 <router-link :to="'/course/'+course.id">
                   <button class="float-right btn-show">
                     &nbsp;مشاهده دوره
                   </button>
                 </router-link>
               </div>
             </div>
           </div>
         </div>
       </div>
     </div>
   </div>
  </section>
</template>

<script>
  export default {
    name: "",
    props: {
      lesson: {
        type: Object,
        default: {courses: []}
      }
    }
  }
</script>

<style scoped>

</style>
