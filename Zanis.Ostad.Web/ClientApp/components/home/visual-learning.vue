<template>
  <section class="visual-learning">
    <div class="container">
      <div class="row">
        <div class="col-md-6 image-container">
          <img src="../../assets/images/ss/02.png" alt>
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
            لذا در راستای رفع مشکلات آموزشی دانشجویان، استاد زانیس اقدام به تولید
            بسته های آموزشی در رشته ها مختلف دانشگاهی نموده است
          </p>
        </div>
      </div>
    </div>
  </section>
</template>

<script>
export default {
  name: ""
};
</script>

<style scoped lang="scss">
@import "../../assets/_mixins.scss";

.visual-learning {
  padding: 36px 0 0 0;

  @keyframes move {
    0% {
      margin-top: 0px;
    }
    50% {
      margin-top: 40px;
    }
    100% {
      margin-top: 0px;
    }
  }
  .image-container {
    img{
        animation: move 2.7s infinite;
        animation-timing-function:ease-in-out	;
    }
    background-image: url("../../assets/images/ss/01.png");
    background-size: cover;
    min-height: 584px;
    display: flex;
    flex-direction: column;
    justify-content: center;
  }
  img {
    max-width: 100%;
    max-height: 583px;
  }
  h3 {
    position: relative;
    margin-bottom: 13px;
    &:after {
      top: 17px;
      content: " ";
      background-color: #afafaf;
      width: 66%;
      height: 2px;
      position: absolute;
      left: 0px;
    }
  }
  p {
    line-height: 2;
  }
  .text-content {
    padding-top: 76px;
  }
  @include mobile {
    padding: 28px 0 !important;
    .text-content {
      margin-top: 27px;
      h3 {
        text-align: center;
      }
    }
  }
}
</style>
