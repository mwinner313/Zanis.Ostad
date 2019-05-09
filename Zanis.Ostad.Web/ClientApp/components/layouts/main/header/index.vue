<template>
  <div>
    <header class="main-header">
      <div id="intro-wrapper">
        <div class="title-container">
          <div style="position:relative">
            <h1>
              آموزش تصویری و تخصصی
              دروس دانشگـاهی
            </h1>
          </div>
        </div>
        <div class="vertical-line"></div>
        <MouseScroll></MouseScroll>
      </div>
    </header>
    <div class="yellow-small-separator"></div>
  </div>
</template>

<script>
import MouseScroll from "./mouse-scrool";
export default {
  components: { MouseScroll },
  name: "main-layout-header",
  methods: {
    showLoginModal() {
      this.$root.$emit("bv::show::modal", "login-dialog");
    }
  }
};
</script>

<style scoped lang="scss">
@import "../../../../assets/_variables.scss";

.yellow-small-separator {
  height: 40px;
  background-color: $yellow;
}
.main-header {
  background: url("../../../../assets/images/header-image.jpg") no-repeat fixed;
  background-size: cover;
  min-height: 91vh;
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
    position: relative;
    h1 {
      @keyframes left-to-right {
        from {
          opacity: 0;
          left: -300px;
        }
        to {
          opacity: 1;
          left: 20px;
        }
      }

      animation-name: left-to-right;
      animation-duration: 2s;
      animation-delay: 1.5s;
      animation-fill-mode: forwards;
      color: white;
      left: -100px;
      position: absolute;
      opacity: 0;
      text-align: left;
      width: 508px;
      line-height: 63px;
    }
    .title-container {
      overflow: hidden;
      height: 121px;
      left: 100px;
      position: absolute;
      bottom: 39vh;
      width: 529px;
    }
    .vertical-line {
      @keyframes grow {
        from {
          height: 0;
        }
        to {
          height: 104px;
        }
      }
      animation-name: grow;
      animation-duration: 0.5s;
      animation-delay: 1s;
      animation-fill-mode: forwards;
      animation-timing-function: linear;
      z-index: 10;
      position: absolute;
      width: 95px;
      left: 0px;
      height: 0;
      bottom: 39vh;
      border-right: 3px solid white;
    }
    min-height: 91vh;
    @media screen and (min-device-width: 768px) and (max-device-width: 1024px) {
    }
    @media only screen and (max-width: 600px) {
      min-height: 343px;
    }
  }
}
</style>
