<template>
  <div>

    <header class="main-header">
      <div id="intro-wrapper" class="container">
        <div class="row">
          <div class="col-12">
            <h1>آموزش تصویری و تخصصی دروس دانشگاهی </h1>
            <h2> آموزش ، آزمون، حل تمرین</h2>
            <h2> نمونه سوالات 10 ترم</h2>
          </div>
        </div>
      </div>
    </header>
    <div class="yellow-large-separator"></div>
  </div>
</template>

<script>

  export default {
    name: "main-layout-header",
    methods: {
      showLoginModal() {
        this.$root.$emit('bv::show::modal', 'login-dialog')
      }
    }
  }
</script>

<style scoped lang="scss">
  @import '../../../../assets/_variables.scss';

  .yellow-large-separator {
    height: 100px;
    background-color: $yellow;
  }
  .main-header {
    background: url('../../../../assets/images/header-image.jpg') no-repeat fixed;
    background-size: cover;
    min-height: 740px;
    &.small-header {
      min-height: 254px;
      @media screen and (max-device-width: 768px) {
        min-height: 166px;
      }
      background-color: rgba(0, 0, 0, 0.3);

      h3 {
        margin: 0;
        color: white;
        text-align: center;
        vertical-align: middle;
        line-height: 203px;
        font-size: 43px;
        @media screen and (max-device-width: 768px) {
          line-height: 86px;
          font-size: 17px;
        }
        @media screen and (min-device-width: 768px) and (max-device-width: 1024px) {
          line-height: 174px;
          font-size: 32px;
        }
      }
    }
    @media only screen and (max-width: 600px) {
      min-height: 343px;
    }
    #intro-wrapper {
      h1 {
        margin: 80px 20px 0 0;
        display: inline-block;
        color: #d6d4d3;
        font-family: 'EntezareZohoor B3';
        font-size: 70px;
        border-bottom: 3px solid white;
        border-top: 3px solid white;
      }
      h2 {
        margin: 55px 20px 0 0;
        font-family: BalvardiDastnevis;
        font-size: 58px;
        color: $yellow;
      }
      min-height: 870px;
      @media screen and (min-device-width: 768px) and (max-device-width: 1024px) {
      }
      @media only screen and (max-width: 600px) {
        min-height: 343px;
      }
    }
  }

</style>
