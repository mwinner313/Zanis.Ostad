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
              <button class="submit">شروع تدریــس</button>
            </router-link>
          </div>
        </article>
      </div>
      <div class="row mt-5">
        <h3 class="selected-field-title">{{field.name}}</h3>
        <div class="tab-container">
          <header>
            <div id="material-tabs">
              <a
                @click="changeTabByQuery('courses')"
                id="tab1-tab"
                :class="['tab-key',this.$route.query.contentType =='courses'?'active':'']"
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
                @click="changeTabByQuery('lessonExams')"
                id="tab2-tab"
                :class="['tab-key',this.$route.query.contentType =='lessonExams'?'active':'']"
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
            <div class="row" v-show="$route.query.contentType === 'courses'">
              <div class="container">
                <div class="row my-5">
                  <div
                    @click="query.courseCategoryId=cat.id"
                    v-for="cat in courseCategories"
                    :key="cat.id"
                    class="col-md-3"
                  >
                    <div
                      :class="['box','course-category',query.courseCategoryId===cat.id?'active':'']"
                    >
                      <span>{{cat.name}}</span>
                    </div>
                  </div>
                </div>
                <a href="#" ref="courseListTop"></a>
                <ContentLoader v-if="isLoadingCourses"></ContentLoader>
                <div class="row" v-if="!isLoadingCourses">
                  <div class="col-md-4 p-1" v-for="course in courses.items" :key="course.id">
                    <div class="box">
                      <router-link :to="`/course/${course.id}-${course.permalink}`">
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
                        <router-link :to="`/course/${course.id}-${course.permalink}`">
                          <button class="float-right show-course">مشاهده</button>
                        </router-link>
                        <div class="clearfix"></div>
                      </div>
                    </div>
                  </div>
                </div>
                <div class="row d-flex align-content-center justify-content-center">
                  <b-pagination
                    v-model="query.pageNumber"
                    :total-rows="courses.allCount"
                    :per-page="query.pageSize"
                    aria-controls="my-table"
                  ></b-pagination>
                </div>
              </div>
            </div>
            <div v-show="$route.query.contentType ==='lessonExams'">
              <div class="box" v-if="!$route.params.fieldId">
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
              <div class="lesson-exams box" v-if="$route.params.fieldId">
                <ul>
                  <li v-for="lesson in lessons" :key="lesson.id">
                    <router-link :to="`/lesson-exam/${lesson.id}`">
                      <span class="lesson-title">{{lesson.lessonName}}</span>
                      <!-- <span class="files-count">۲ فایل</span> -->
                    </router-link>
                  </li>
                </ul>
              </div>
            </div>
          </div>
        </div>
      </div>
    </main>
    <FieldSelector
      :contentType="contentType"
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
import ContentLoader from "../content-loader";

export default {
  name: "",
  data() {
    return {
      contentType: "courses",
      isLoadingCourses: false,
      college: {},
      selectedGrade: undefined,
      courses: [],
      grades: [],
      grade: {},
      field: {},
      lessons: [],
      courseCategories: [],
      query: {
        pageSize: 15,
        pageOffset: 0,
        pageNumber: 1,
        courseCategoryId: undefined
      }
    };
  },
  components: {
    FieldSelector,
    FeaturedCourse,
    ContentLoader
  },
  methods: {
    changeTabByQuery(val) {
      this.$router.push({
        name: this.$route.name,
        params: this.$route.params,
        query: { ...this.$route.query, contentType: val }
      });
    },
    async loadCourses() {
      let params = this.$route.params;
      this.isLoadingCourses = true;
      let { data: courses } = await this.$http.get("/api/courses/actives", {
        params: {
          gradeId: params.gradeId,
          fieldId: params.fieldId,
          ...this.query
        }
      });
      window.setTimeout(() => (this.isLoadingCourses = false), 1500);
      this.courses = courses;
    },
    getSvgColor(tabKey) {
      return tabKey == this.$route.query.contentType
        ? "rgb(238, 234, 129)"
        : "#424f5a";
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
    async loadLessonsIfGradeAndFieldExists() {
      let params = this.$route.params;
      if (params.gradeId && params.fieldId) {
        let { data: lessons } = await this.$http.get("/api/GradeFieldLessons", {
          params: { fieldId: params.fieldId, gradeId: params.gradeId }
        });
        this.lessons = lessons;
      }
    },
    loadCourseCategories() {
      this.$http
        .get("/api/courseCategories")
        .then(({ data: courseCategories }) => {
          this.courseCategories = courseCategories;
        });
    },
    loadGradeIfExistsInParams() {
      if (this.$route.params.gradeId)
        this.$http
          .get("/api/grades/" + this.$route.params.gradeId)
          .then(({ data: grade }) => {
            this.grade = grade;
            if (this.$route.params.gradePermaLink != grade.permaLink)
              this.chageRouteParam("gradePermaLink", grade.permaLink);
          });
    },
    loadFieldIfExistsInParams() {
      if (this.$route.params.fieldId)
        this.$http
          .get("/api/fields/" + this.$route.params.fieldId)
          .then(({ data: field }) => {
            this.field = field;
            if (this.$route.params.fieldPermaLink != field.permaLink)
              this.chageRouteParam("fieldPermaLink", field.permaLink);
          });
    },
    setPageTitle() {
      let title = this.college.name;
      if (this.field.id)
        title = title + " | " + this.grade.name + " | " + this.field.name;
      window.document.title = title + " | استاد زانیس";
    }
  },
  watch: {
    "query.courseCategoryId"() {
      this.loadCourses();
    },
    "$route.params.gradeId"(val) {
      this.loadGradeIfExistsInParams();
    },
    "$route.query.contentType"(val) {
      this.contentType = val;
    },
    async "$route.params.fieldId"(val) {
      await this.loadFieldIfExistsInParams();
      await this.loadCourses();
      await this.loadLessonsIfGradeAndFieldExists();
      this.setPageTitle();
    },
    "query.pageNumber"(val) {
      this.query.pageOffset = this.query.pageSize * (val - 1);
      this.loadCourses();
      window.scrollTo({
        top: this.$refs.courseListTop.offsetTop - 250,
        left: 0,
        behavior: "smooth"
      });
    }
  },
  updated() {
    if (!this.$route.query.contentType) {
      this.changeTabByQuery("courses");
    }
  },
  async mounted() {
    if (!this.$route.query.contentType) {
      this.changeTabByQuery("courses");
    }
    await this.loadUniversity();
    await this.loadGradeIfExistsInParams();
    await this.loadFieldIfExistsInParams();
    await this.loadLessonsIfGradeAndFieldExists();
    await this.loadGrades();
    this.loadCourseCategories();
    if (this.$route.params.collegePermaLink !== this.college.permaLink)
      this.chageRouteParam("collegePermaLink", this.college.permaLink);
    await this.loadCourses();
    this.setPageTitle();
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
.selected-field-title {
  color: #7d7d7d;
  font-weight: 300;
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
.show-course {
  outline: none !important;
  cursor: pointer;
  border: 0px solid red;
  padding: 7px 10px;
  border-radius: 3px;
  color: #ffffff;
  background-color: #00bfb0;
}
.course-category {
  text-align: center;
  cursor: pointer;
  transition: all 0.3s;
  &.active {
    box-shadow: 0px 5px 8px #f5afa8;
  }
  span {
    display: block;
    padding: 17px;
    font-size: 13px;
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
  position: relative;
  width: 100%;
  height: auto;
  background-repeat: no-repeat;
  background-size: 100% 100%;
  height: 260px;
  background-image: url("../../../assets/images/01.png");
  .submit {
    transition: all 0.7s;
    right: 218px;
    bottom: 51px;
    cursor: pointer;
    font-weight: bold;
    padding: 10px 23px;
    color: #676767;
    height: 37px;
    font-size: 12px;
    position: absolute;
    border-radius: 10px;
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
.box-round {
  margin-top: 100px;
  position: relative;
  box-shadow: 0 1px 8px rgba(0, 0, 0, 0.12), 0 1px 2px rgba(0, 0, 0, 0.24);
  border-radius: 40px;
  padding: 24px;
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
.lesson-exams {
  ul {
    list-style: none;
    padding: 0;
    li {
      a {
        color: black;
        display: block;
      }
      cursor: pointer;
      &:first-child {
        border-top: none;
      }
      border-top: 1px solid #b3b3b3;
    }
  }
  .lesson-title {
    padding: 30px;
    display: inline-block;
  }
  .files-count {
    padding: 30px;
    float: left;
  }
}
</style>
