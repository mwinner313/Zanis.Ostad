<template>
  <div>
    <small-header title="دروس من"></small-header>
    <section class="container">
      <div class="row">
        <div class=" col-sm-12">
          <div class="gray-shadow">
            <div class="container no-padding mt-5">
              <div class="row no-gutters">
                <div class=" col-sm-6">
                  <a :class="['grade', currentTab === tabs.lessonExams?'active':'']"
                     @click="currentTab=tabs.lessonExams"> <span>نمونه سوالات</span></a>
                </div>
                <div class=" col-sm-6">
                  <a :class="['grade', currentTab === tabs.courses?'active':'']" @click="currentTab=tabs.courses">
                    <span>دوره های آموزشی</span></a>
                </div>
              </div>
            </div>
            <div v-show="currentTab===tabs.lessonExams">
              <h5 v-show="!lessonExams.length" class="text-center box-mp">هنوز هیچ نمونه سوالی نخریده اید!</h5>
              <ul v-show="lessonExams.length" class="column-group">
                <li v-for="lesson in lessonExams" @click="selectLesson(lesson)">{{lesson.lessonName}}</li>
              </ul>
            </div>
            <div v-show="currentTab === tabs.courses">
              <h5 v-show="!courses.length" class="text-center box-mp">هنوز هیچ دوره آموزشی نخریده اید!</h5>
              <ul v-show="courses.length" class="column-group">
                <li v-for="course in courses">{{course.title}}</li>
              </ul>
            </div>
          </div>
        </div>
      </div>
      <div class="row">
        <lesson-exam v-if="selectedLesson" :lesson="selectedLesson"></lesson-exam>

      </div>
    </section>
  </div>
</template>

<script>
  import SmallHeader from '../layouts/main/header/index-small-image'
  import LessonExam from './lesson-exam'

  export default {
    name: "",
    components: {
      SmallHeader,
      LessonExam
    },
    data() {
      return {
        lessonExams: [],
        courses: [],
        selectedLesson:null,
        currentTab: 'lessonExams',
        tabs: { lessonExams: 'lessonExams', courses: 'courses'}
      }
    },
    methods: {
      async loadCourses() {
        this.courses = (await this.$http.get('/api/account/Courses', {params: {pageSize: 1000}})).data
      },
      async loadLessonExams() {
        this.lessonExams = (await this.$http.get('/api/account/LessonExams', {params: {pageSize: 1000}})).data
      },
      async selectLesson(item){
        this.selectedLesson = item;
      }
    },
    mounted() {
      this.loadCourses();
      this.loadLessonExams();
    }
  }
</script>

<style scoped>

</style>
