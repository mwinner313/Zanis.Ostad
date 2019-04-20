<template>
  <div class="container">
    <header class="page-header">
      <simple-svg
        :filepath="college.iconPath"
        fill="rgb(101, 100, 100)"
        stroke="rgb(101, 100, 100)"
        :width="'140px'"
        :height="'131px'"
        :id="'play-svg'"
      />
      <h1>{{college.name}}</h1>
      <div class="line"></div>
    </header>
    <main class="container">
      <div class="row">
        <aside class="col-sm-3 p-1">
          <div class="box">
            <div class="grades">
              <span>مقاطع</span>
              <div class="line"></div>
              <ul>
                <li @click="selectedGrade = grade" v-for="grade in grades" :key="grade.id">
                  <a>{{grade.name}}</a>
                </li>
              </ul>
            </div>
          </div>
        </aside>

        <article class="col-sm-9 p-1">
          <div id="start-teaching-banner-image">
            <router-link to="/start-teaching">
              <button class="submit">شروع تدریس</button>
            </router-link>
            <p>دانشگاه پیام نور یکی از دانشگاه های بزرگ کشور است که دانشجویان زیادی در سراسر کشور دارد. به دلیل سیستم آموزشی این دانشگاه که بر پایه آموزش از راه دور بنیان نهاده شده است، محدودیت زیادی در ساعات آموزش دروس در این دانشگاه وجود دارد و این ساعات صرف رفع اشکال و یا آموزش فشرده درس می گردد،‌ از طرفی بسیاری از دانشجویان این دانشگاه بدلیل مشکلات شغلی و یا دوری مسافت امکان شرکت در همین کلاس های محدود را نیز ندارند لذا در راستای رفع مشکلات آموزشی دانشجویان، شرکت رایان پژوهان زانیس اقدام به تولید بسته های آموزشی در رشته ها مختلف دانشگاهی نموده است </p>
          </div>
        </article>
      </div>
      <div class="row mt-5">
        <div class="tab-container">
          <header>
            <div id="material-tabs">
              <a
                id="tab1-tab"
                @click="activeTabKey='courses'"
                :class="['tab-key',this.activeTabKey=='courses'?'active':'']"
              >
                <simple-svg
                  filepath="/assets/svg/play-b.svg"
                  :fill="getSvgColor('courses')"
                  :stroke="getSvgColor('courses')"
                  :width="'38px'"
                  :height="'50px'"
                  :id="'play-svg'"
                />دوره های آموزشی
              </a>
              <a
                id="tab2-tab"
                @click="activeTabKey='lessonExams'"
                :class="['tab-key',this.activeTabKey=='lessonExams'?'active':'']"
              >
                <simple-svg
                  filepath="/assets/svg/paper.svg"
                  :fill="getSvgColor('lessonExams')"
                  :stroke="getSvgColor('lessonExams')"
                  :width="'38px'"
                  :height="'50px'"
                  :id="'play-svg'"
                />نمونه سوالات
              </a>
              <span class="yellow-bar"></span>
            </div>
          </header>

          <div class="tab-content">
            <div class="row" v-show="activeTabKey==='courses'">
              <div class="container">
                <div class="row">
                  <div class="col-md-4 p-1" v-for="course in courses.items" :key="course.id">
                    <div class="box">
                      <router-link :to="`/course/${course.id}-${course.permaLink}`">
                        <div
                          class="course-cover"
                          :style="`background-image: url(${course.imagePath||'/assets/images/NP.jpg'})`"
                        ></div>
                      </router-link>
                      <span class="course-title">{{course.title}}</span>
                      <span
                        class="course-grade"
                      >({{ course.relatedLessonFields && course.relatedLessonFields[0].gradeName}})</span>
                      <div class="button-duration-wrapper">
                        <i class="far fa-clock"></i>
                        <span>{{course.duration}}</span>
                        <router-link :to="`/course/${course.id}-${course.permaLink}`">
                          <button class="float-right">مشاهده</button>
                        </router-link>
                        <div class="clearfix"></div>
                      </div>
                    </div>
                  </div>
                </div>
                <div class="row d-flex align-content-center justify-content-center">
                  <!-- <b-pagination
                    v-model="currentPage"
                    :total-rows="rows"
                    :per-page="perPage"
                    aria-controls="my-table"
                  ></b-pagination>-->
                </div>
              </div>
            </div>
            <div v-show="activeTabKey==='lessonExams'"></div>
          </div>
        </div>
      </div>
    </main>
    <FieldSelector
      :college="college"
      :grade="selectedGrade"
      :isOpen="!!selectedGrade"
      @close="selectedGrade=undefined"
    ></FieldSelector>
  </div>
</template>

<script>
import FieldSelector from "./field-selector";
import FeaturedCourse from "./featured-course";
export default {
  name: "",
  data() {
    return {
      college: {},
      selectedGrade: undefined,
      courses: [],
      grades: [],
      activeTabKey: "lessonExams"
    };
  },
  components: {
    FieldSelector,
    FeaturedCourse
  },
  methods: {
    async loadCourses() {
      let params = this.$route.params;
      let { data: courses } = await this.$http.get("/api/courses/actives", {
        params: {
          gradeId: params.gradeId,
          fieldId: params.fieldId
        }
      });
      this.courses = courses;
    },
    getSvgColor(tabKey) {
      return tabKey == this.activeTabKey ? "rgb(238, 234, 129)" : "#424f5a";
    },
    async loadUniversity() {
      let { data: college } = await this.$http.get(
        "/api/colleges/" + this.$route.params.collegeId
      );
      this.college = college;
    },
    async loadGrades() {
      let { data: grades } = await this.$http.get("/api/grades");
      this.grades = grades;
    },
    chageRouteParam(param, val) {
      this.$router.push({
        name: this.$route.name,
        params: {
          ...this.$route.params,
          [param]: val
        }
      });
    },
    loadGradeIfExistsInParams() {
      if (this.$route.params.gradeId)
        this.$http
          .get("/api/grades/" + this.$route.params.gradeId)
          .then(({ data: grade }) => {
            if (this.$route.params.gradePermaLink != grade.permaLink)
              this.chageRouteParam("gradePermaLink", grade.permaLink);
          });
    },
    loadFieldIfExistsInParams() {
      if (this.$route.params.fieldId)
        this.$http
          .get("/api/fields/" + this.$route.params.fieldId)
          .then(({ data: field }) => {
            if (this.$route.params.fieldPermaLink != field.permaLink)
              this.chageRouteParam("fieldPermaLink", field.permaLink);
          });
    }
  },
  watch: {
    "$route.params.gradeId"(val) {
      this.loadGradeIfExistsInParams();
    },
    async "$route.params.fieldId"(val) {
      await this.loadFieldIfExistsInParams();
      await this.loadCourses();
    }
  },
  async mounted() {
    await this.loadUniversity();
    await this.loadGradeIfExistsInParams();
    await this.loadFieldIfExistsInParams();
    await this.loadGrades();
    if (this.$route.params.collegePermaLink !== this.college.permaLink)
      this.chageRouteParam("collegePermaLink", this.college.permaLink);
    await this.loadCourses();
  }
};
</script>


<style scoped lang="scss">
@import "../../../assets/_variables.scss";

.page-header {
  display: flex;
  align-items: center;
  justify-content: center;
  margin-top: 100px;
  h1 {
    white-space: nowrap;
    margin: 0 10px;
    color: #656464;
  }
}
.line {
  width: 100%;
  height: 2px;
  background: #b9b5b5;
}
main {
  margin-top: 10px;
}
.box {
  box-shadow: 0 1px 8px rgba(0, 0, 0, 0.12), 0 1px 2px rgba(0, 0, 0, 0.24);
}
.grades {
  padding: 10px;
  font-size: 17px;
  span {
    margin: 10px 14px;
    display: block;
  }
  ul {
    margin: 0 -40px !important;
    padding: 0px;
    list-style: none;
    display: inline;
    li {
      .bridge {
        position: absolute;
        width: 30px;
        height: 40px;
        top: 0;
        left: -26px;
        z-index: 100;
      }
      position: relative;
      cursor: pointer;
      padding: 11px 0;
      transition: all 1s;
      &:hover {
        ul {
          opacity: 1;
          z-index: 100;
          display: block;
        }
        background: #e4e4e4;
      }
      &::before {
        content: "\2022";
        color: #ffc822;
        font-weight: bold;
        display: inline-block;
        width: 1em;
        margin-right: 10px;
      }
    }
  }
}

.tab-container {
  width: 100%;
  padding: 0;
  margin: 10px;
  border-radius: 5px;
}

header {
  position: relative;
}

.hide {
  display: none;
}

.tab-content {
  padding: 25px;
}
.tab-key {
  width: 49%;
  cursor: pointer;
}
#material-tabs {
  position: relative;
  display: block;
  padding: 0;
  border-bottom: 1px solid #e0e0e0;
  a {
    display: relative;
    .simple-svg-wrapper {
      svg {
        transition: all 2s;
      }
      display: inline;
      position: absolute;
      right: 132px;
      top: 19%;
    }
    &:not(.active):hover {
      background-color: inherit;
      color: #7c848a;
    }
    &.active {
      font-weight: 400;
      outline: none;
      font-size: 26px;
    }
    @media only screen and (max-width: 520px) {
      .nav-tabs#material-tabs > li > a {
        font-size: 11px;
      }
    }
    position: relative;
    display: inline-block;
    text-decoration: none;
    padding: 22px;
    text-transform: uppercase;
    font-size: 26px;
    font-weight: 300;
    color: #424f5a;
    text-align: center;
    outline: none;
  }
  .yellow-bar {
    position: absolute;
    z-index: 10;
    bottom: 0;
    height: 3px;
    background: #eeea81;
    display: block;
    left: 0;
    transition: left 0.2s ease;
    -webkit-transition: left 0.2s ease;
  }
}

#tab1-tab.active ~ span.yellow-bar {
  left: 50%;
  width: 50%;
}

#tab2-tab.active ~ span.yellow-bar {
  left: 0;
  width: 50%;
}

.course-cover {
  width: 100%;
  height: 180px;
  background-size: cover;
}
#start-teaching-banner-image {
  p{
    padding:30px;
    font-size:19px;
    color:white;
        background-color: rgba(37, 37, 37, 0.48);
    height: 100%;
  }
  position: relative;
  width: 100%;
  height: auto;
  background-repeat: no-repeat;
  background-size: 100% 100%;
  height: 326px;
  background-image: url("../../../assets/images/shoro-amozesh-2.jpg");
  .submit {
    transition: all 0.7s;
    left: 35px;
    bottom: 25px;
    cursor: pointer;
    font-weight: bold;
    padding: 10px 23px;
    color: #676767;
    height: 50px;
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

.course-title {
  padding: 12px 12px 0 0;
  display: block;
}
.course-grade {
  padding: 0 12px;
  font-size: 12px;
  color: red;
}
.button-duration-wrapper {
  margin: 50px 10px 10px 10px;
  padding: 10px 0px;
  border-top: 1px solid #b3b3b3;
  color: #908c8c;
  i {
    font-size: 25px;
    color: #908c8c;
  }
}
</style>
