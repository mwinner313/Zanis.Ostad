<template>
  <div class="container">
    <div class="row">
      <div class="col-sm-12 last-courses">
        <div class="title">
          <h3 >آخرین دوره ها</h3>
        </div>
        <swiper :options="swiperOption">
          <swiper-slide v-for="course in courses">
            <div  class=" col-md-12 p-1">
              <div class="course">
                <router-link :to="'/course/'+course.id">
                  <h6 class="text-black">{{course.title}}</h6>
                </router-link>
                <span>مدرس : {{course.teacher}}</span>
                <div class="price-cart">
                  <span class="price"> {{course.priceAsTomans |currency}} تومان</span>
                  <div>
                    <router-link :to="'/course/'+course.id">
                      <button class="float-right btn-show">
                        &nbsp;مشاهده دوره
                      </button>
                    </router-link>
                  </div>
                </div>
              </div>
            </div>
          </swiper-slide>
          <div class="swiper-pagination" slot="pagination"></div>
        </swiper>
      </div>
    </div>
  </div>

</template>

<script>
  export default {
    data() {
      return {
        courses:[
          {
            "teacher": "محمدرضا رهنما",
            "price": 1100,
            "priceAsTomans": 110,
            "title": "دوره شب امتحانی",
            "previewType": 0,
            "previewFilePath": null,
            "gradeTitle": null,
            "relatedFields": null,
            "contents": null,
            "isOwnedByCurrentUser": false,
            "id": 4,
            "lessonCode": null,
            "description": null,
            "teacherAvatar": null
          },
          {
            "teacher": "محمدرضا رهنما",
            "price": 1100,
            "priceAsTomans": 110,
            "title": "دوره شب امتحانی",
            "previewType": 0,
            "previewFilePath": null,
            "gradeTitle": null,
            "relatedFields": null,
            "contents": null,
            "isOwnedByCurrentUser": false,
            "id": 4,
            "lessonCode": null,
            "description": null,
            "teacherAvatar": null
          }, {
            "teacher": "محمدرضا رهنما",
            "price": 1100,
            "priceAsTomans": 110,
            "title": "دوره شب امتحانی",
            "previewType": 0,
            "previewFilePath": null,
            "gradeTitle": null,
            "relatedFields": null,
            "contents": null,
            "isOwnedByCurrentUser": false,
            "id": 4,
            "lessonCode": null,
            "description": null,
            "teacherAvatar": null
          }, {
            "teacher": "محمدرضا رهنما",
            "price": 1100,
            "priceAsTomans": 110,
            "title": "دوره شب امتحانی",
            "previewType": 0,
            "previewFilePath": null,
            "gradeTitle": null,
            "relatedFields": null,
            "contents": null,
            "isOwnedByCurrentUser": false,
            "id": 4,
            "lessonCode": null,
            "description": null,
            "teacherAvatar": null
          }, {
            "teacher": "محمدرضا رهنما",
            "price": 1100,
            "priceAsTomans": 110,
            "title": "دوره شب امتحانی",
            "previewType": 0,
            "previewFilePath": null,
            "gradeTitle": null,
            "relatedFields": null,
            "contents": null,
            "isOwnedByCurrentUser": false,
            "id": 4,
            "lessonCode": null,
            "description": null,
            "teacherAvatar": null
          }, {
            "teacher": "محمدرضا رهنما",
            "price": 1100,
            "priceAsTomans": 110,
            "title": "دوره شب امتحانی",
            "previewType": 0,
            "previewFilePath": null,
            "gradeTitle": null,
            "relatedFields": null,
            "contents": null,
            "isOwnedByCurrentUser": false,
            "id": 4,
            "lessonCode": null,
            "description": null,
            "teacherAvatar": null
          }
        ],
        swiperOption: {
          slidesPerView: 3,
          spaceBetween: 20,
          pagination: {
            el: '.swiper-pagination',
            clickable: true
          },
          breakpoints: {
            1024: {
              slidesPerView: 3,
              spaceBetween: 40
            },
            768: {
              slidesPerView: 2,
              spaceBetween: 30
            },
            640: {
              slidesPerView: 1,
              spaceBetween: 20
            },
            320: {
              slidesPerView: 1,
              spaceBetween: 10
            }
          }
        }
      }
    }
  }
</script>
<style scoped>
  .swiper-pagination{
    margin-top:30px !important;
  }
  .swiper-container{
    min-height: 215px;
  }

</style>
