<template>
  <div class="featered-item box d-flex">
    <img src="../../../assets/images/5920.jpg" alt>
    <div class="container">
      <div class="row">
        <div class="col-sm-12">
          <h3 class="title">عنوان دوره</h3>
          <span class="datetime">5 آبان 1398</span>
          <p
            class="description"
          >متن متن متن متن متن متن متن متن متن متن متن متن متن متن متن متن متن متن متن متن متن متن متن متن متن متن</p>
        </div>
      </div>
      <div class="teacher-cover">
        <span>محمد علی قنبری طاهری</span>
        <img src="../../../assets/images/5920.jpg" alt>
      </div>
      <div class="course-duration">متن متن متن متن متن</div>
      <button class="show-course xanis-btn">مشاهده دوره</button>
    </div>
  </div>
</template>

<script>
export default {};
</script>

<style lang="scss" scoped>

.featered-item {
  position: relative;
  padding: 14px;
  .title,
  .datetime,
  .description {
    padding: 26px;
    font-weight: 200;
    color: #717171;
  }
  .course-duration {
    padding-right: 26px;
    font-weight: 400;
    color: #717171;
  }
  .teacher-cover {
    position: absolute;
    font-weight: 100;
    left: 14px;
    top: 17px;
    img {
      border-radius: 50%;
      margin-right: 5px;
      width: 76px;
    }
  }
  button.show-course {
    padding: 13px 33px;
    position: absolute;
    left: 22px;
    bottom: 14px;
  }
}
</style>