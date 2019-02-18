<template>
  <div>
    <main-header></main-header>
    <main class="">
      <div class="container p-0">
        <search-box-mobile></search-box-mobile>
        <search-box></search-box>
        <product-selector></product-selector>

      </div>
      <section class="visual-learning">
        <div class="container">
          <div class="row">
            <div class="col-md-6 image-container">
             <figure> <img src="../../assets/images/visual-teaching.png" alt=""></figure>
            </div>
            <div class="col-md-6 text-content">
              <h3>آموزش تصویری</h3>
              <p class="text-justify">
                دانشگاه پیام نور یکی از دانشگاه های بزرگ کشور است که دانشجویان زیادی در سراسر کشور دارد. به دلیل سیستم
                آموزشی این دانشگاه که بر پایه آموزش از راه دور بنیان نهاده شده است، محدودیت زیادی در ساعات آموزش دروس در
                این دانشگاه وجود دارد و این ساعات صرف رفع اشکال و یا آموزش فشرده درس می گردد،‌ از طرفی بسیاری از
                دانشجویان این دانشگاه بدلیل مشکلات شغلی و یا دوری مسافت امکان شرکت در همین کلاس های محدود را نیز ندارند
              </p>
              <p class="text-justify">
                لذا در راستای رفع مشکلات آموزشی دانشجویان، شرکت رایان پژوهان زانیس اقدام به تولید
                بسته های آموزشی در رشته ها مختلف دانشگاهی نموده است</p>

            </div>
          </div>
        </div>
      </section>
      <CoursesSlider></CoursesSlider>
    </main>
  </div>
</template>

<script>
  import productSelector from './product-selector';
  import SearchBox from './search-box';
  import SearchBoxMobile from './search-box-mobile';
  import MainHeader from '../layouts/main/header/index';
  import CoursesSlider from './courses-slider';

  export default {
    name: "home",
    components: {
      productSelector,
      SearchBox,
      SearchBoxMobile,
      MainHeader,
      CoursesSlider
    }
  }
</script>

<style scoped>
</style>
