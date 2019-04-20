<template>
  <div id="background">
    <div class="container">
      <div class="row">
        <div class="col-12">
          <div id="start-teaching-banner-image">
            <button @click="showAddCourseDialog" class="submit">شروع تدریس</button>
          </div>

          <div class="content-box">
            <h4 class="content-box-title">توضیحات تدریس</h4>
            <p class="content-box-text">
              دانشگاه پیام نور یکی از دانشگاه های بزرگ کشور است که دانشجویان زیادی در سراسر کشور دارد. به دلیل سیستم
              آموزشی این دانشگاه که بر پایه آموزش از راه دور بنیان نهاده شده است، محدودیت زیادی در ساعات آموزش دروس در
              این دانشگاه وجود دارد و این ساعات صرف رفع اشکال و یا آموزش فشرده درس می گردد،‌ از طرفی بسیاری از دانشجویان
              این دانشگاه بدلیل مشکلات شغلی و یا دوری مسافت امکان شرکت در همین کلاس های محدود را نیز ندارند
              لذا در راستای رفع مشکلات آموزشی دانشجویان، شرکت رایان پژوهان زانیس اقدام به تولید بسته های آموزشی در رشته
              ها مختلف دانشگاهی نموده است
            </p>
          </div>

          <div class="content-box">
            <h4 class="content-box-title">انواع محتوا</h4>
            <div role="tablist" class="accordion">
              <div class="item" :key="item.id" v-for="item in courseCategories">
                <span
                  @click="item.isOpen=!item.isOpen"
                  v-b-toggle="'accordion-'+item.id"
                  variant="info"
                >
                 <i class="fas fa-chevron-down yellow when-opened"></i>
                 <i class="fas fa-chevron-left yellow when-closed"></i>
                  {{item.name}}</span>
                <b-collapse :id="'accordion-'+item.id" role="tabpanel">
                  <div class="container">
                    <div class="row">
                      <div class="col-sm-3">
                        <img :src="item.imagePath" alt>
                      </div>
                      <div class="col-sm-9">
                        <p>{{item.description}}</p>
                      </div>
                    </div>
                  </div>
                </b-collapse>
              </div>
            </div>
          </div>

          <div class="content-box">
            <h4 class="content-box-title">توضیحات تدریس</h4>
            <p class="content-box-text">
              دانشگاه پیام نور یکی از دانشگاه های بزرگ کشور است که دانشجویان زیادی در سراسر کشور دارد. به دلیل سیستم
              آموزشی این دانشگاه که بر پایه آموزش از راه دور بنیان نهاده شده است، محدودیت زیادی در ساعات آموزش دروس در
              این دانشگاه وجود دارد و این ساعات صرف رفع اشکال و یا آموزش فشرده درس می گردد،‌ از طرفی بسیاری از دانشجویان
              این دانشگاه بدلیل مشکلات شغلی و یا دوری مسافت امکان شرکت در همین کلاس های محدود را نیز ندارند
              لذا در راستای رفع مشکلات آموزشی دانشجویان، شرکت رایان پژوهان زانیس اقدام به تولید بسته های آموزشی در رشته
              ها مختلف دانشگاهی نموده است
            </p>
            <button class="start-teaching-button" @click="showAddCourseDialog">شروع تدریس</button>
            <div class="clearfix"></div>
          </div>
        </div>
      </div>
    </div>
    <AddCourseDialog @close="isAddingNewCourse=false" :customMessageForSuccess="showSuccessAfterAddingCourse"
                     :isOpen="isAddingNewCourse"></AddCourseDialog>
  </div>
</template>

<script>
  import AddCourseDialog from '../../teacher/courses/add-course-dialog'
  import storage from "storage-helper";
  import EventBus from "../../../event-bus";

  export default {
    name: "",
    components: {
      AddCourseDialog
    },
    data() {
      return {
        isAddingNewCourse: false,
        courseCategories: [],
      };
    },
    methods: {
      showAddCourseDialog() {
        let authorization = storage.getItem('Authorization');
        if ( [undefined,'undefined',null].indexOf(authorization)  ==  -1 )
          this.isAddingNewCourse = true;
        else {
          this.$root.$emit("bv::show::modal", "login-dialog");
          EventBus.$on("user-comes-in", () => {
            this.isAddingNewCourse = true;
          });
        }
      },
      showSuccessAfterAddingCourse() {
        this.$swal(
          {
            title: "ارسال دوره با موفقیت انجام شد",
            text: "دوره شما با موفقیت ثبت شد بعد از بررسی توسط همکاران ما دوره شما تایید و در پنل کاربری خود قادر به مشاهده وضعیت دوره ثبت شده اعم از خرید دانشجویان ویرایش حذف وافزودن دوره جدید خواهید بود نکته قابل توجه این که دوره شما توسط تدوین گر های ما تدوین و سپس بر روی سایت نمایش داده خواهد شد لذا تا تکمیل فرایند های مربوطه صبور باشید . از این که استاد زانیس را محلی برای تدریس خود انتخاب نموده اید متشکریم.",
            type: "success",
            confirmButtonText: "بستن"
          })
      }
    },
    mounted() {
      this.$http.get("/api/CourseCategories").then(res => {
        this.courseCategories = res.data;
      });
    }
  };
</script>

<style scoped lang="scss">
  @import "../../../assets/_variables.scss";

  #start-teaching-banner-image {
    position: relative;
    width: 100%;
    margin: 50px 0;
    height: auto;
    background-repeat: no-repeat;
    background-size: 100% 100%;
    height: 417px;
    background-image: url("../../../assets/images/shoro-amozesh-2.jpg");
    .submit {
      transition: all 0.7s;
      left: 173px;
      bottom: 55px;
      cursor: pointer;
      font-weight: bold;
      padding: 10px 51px;
      color: #676767;
      height: 60px;
      font-size: 20px;
      position: absolute;
      background-image: linear-gradient(
          to right,
          #f0fd63,
          #edfd82,
          #ebfc9e,
          #ebfbb8,
          #edf9d0
      );
      &:hover {
        box-shadow: 0px 0px 20px $yellow;
        background-image: linear-gradient(
            to right,
            #e6f63e,
            #ecf843,
            #f3fa49,
            #f9fd4e,
            #ffff53
        );
      }
    }
  }

  .content-box {
    background-color: white;
    padding: 16px;
    margin: 15px 0;
    border-radius: 8px;
    .content-box-title {
      position: relative;
      padding: 20px 0;
      color: #737373;
      &:after {
        top: 31px;
        content: " ";
        background-color: #afafaf;
        width: 83%;
        height: 2px;
        position: absolute;
        left: 0px;
      }
    }
    .content-box-text {
      line-height: 30px;
      color: #404040;
      text-align: justify;
    }
  }

  .yellow {
    color: $yellow
  }

  #background {
    background-color: #f9f9f9 !important;
  }

  .accordion {
    .item {
      margin: 10px;
      box-shadow: 0 1px 3px rgba(0, 0, 0, 0.12), 0 1px 2px rgba(0, 0, 0, 0.24);
      transition: all 0.3s cubic-bezier(0.25, 0.8, 0.25, 1);
      &:hover {
        box-shadow: 0 14px 28px rgba(0, 0, 0, 0.25),
        0 10px 10px rgba(0, 0, 0, 0.22);
      }
      span {
        display: block;
        cursor: pointer;
        padding: 15px;

        color: #6b6b6b;
      }
      img {
        width: 100%;
        margin-top: 31px;
      }
      p {
        color: #404040;
        padding: 15px;
        text-align: justify;
        line-height: 34px;
      }
    }
  }

  .collapsed > .when-opened,
  :not(.collapsed) > .when-closed {
    display: none;
  }

  .start-teaching-button {
    transition: all 0.7s;
    cursor: pointer;
    font-weight: bold;
    padding: 10px 51px;
    color: #676767;
    float: left;
    height: 60px;
    font-size: 20px;
    background-image: linear-gradient(
        to right,
        #f0fd63,
        #edfd82,
        #ebfc9e,
        #ebfbb8,
        #edf9d0
    );
    &:hover {
      box-shadow: 0px 0px 20px $yellow;
      background-image: linear-gradient(
          to right,
          #e6f63e,
          #ecf843,
          #f3fa49,
          #f9fd4e,
          #ffff53
      );
    }
  }
</style>
