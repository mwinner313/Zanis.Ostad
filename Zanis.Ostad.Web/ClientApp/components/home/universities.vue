<template>
  <div class="container">
    <h3 class="two-side-title">دانشگاه ها</h3>
    <div class="row">
      <div class="col-sm-12 d-flex flex-wrap universities">
        <div class="university-box">
          <figure><img src="../../assets/images/engineering-1.png" alt=""></figure>
          <span>فنی و مهندسی</span>
        </div>
        <div class="university-box">
          <figure><img src="../../assets/images/engineering-1.png" alt=""></figure>
          <span>فنی و مهندسی</span>
        </div>
        <div class="university-box">
          <figure><img src="../../assets/images/engineering-1.png" alt=""></figure>
          <span>فنی و مهندسی</span>
        </div>
        <div class="university-box">
          <figure><img src="../../assets/images/engineering-1.png" alt=""></figure>
          <span>فنی و مهندسی</span>
        </div>
        <div class="university-box">
          <figure><img src="../../assets/images/engineering-1.png" alt=""></figure>
          <span>فنی و مهندسی</span>
        </div>
        <div class="university-box">
          <figure><img src="../../assets/images/engineering-1.png" alt=""></figure>
          <span>فنی و مهندسی</span>
        </div>
        <div class="university-box">
          <figure><img src="../../assets/images/engineering-1.png" alt=""></figure>
          <span>فنی و مهندسی</span>
        </div>
        <div class="university-box">
          <figure><img src="../../assets/images/engineering-1.png" alt=""></figure>
          <span>فنی و مهندسی</span>
        </div>
        <div class="university-box">
          <figure><img src="../../assets/images/engineering-1.png" alt=""></figure>
          <span>فنی و مهندسی</span>
        </div>
        <div class="university-box">
          <figure><img src="../../assets/images/engineering-1.png" alt=""></figure>
          <span>فنی و مهندسی</span>
        </div>
      </div>

    </div>
  </div>
</template>

<script>
    export default {
        name: ""
    }
</script>

<style scoped lang="scss">

  .universities {
    margin: 27px 0 78px 0;
  }

  .university-box {
    width: 212px;
    margin: 5px;
  figure {
    margin: 0;
  }
  background-color: white;
  box-shadow: 1px 0px 13px -7px #000000;
  span {
    font-size: 25px;
    padding-bottom: 24px;
    color: #535252;
    font-weight: lighter;
    display: block;
  }
  text-align: center;
  }
  .two-side-title {
    color: #535252;
    text-align: center;
    position: relative;
    &:before, &:after {
      top: 13px;
      content: " ";
      background-color: #afafaf;
      width: 44%;
      height: 2px;
      position: absolute;
    }
    &:before {
      right: 0px;
    }
    &:after {
      left: 0px;
    }
  }
</style>
