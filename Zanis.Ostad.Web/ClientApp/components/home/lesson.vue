<template>
  <section>
    <small-header :title="lesson.lessonName"></small-header>
    <div class="container">
      <content-loader v-if="isLoading"></content-loader>
      <LessonExam :lesson="lesson" v-if="!isLoading"></LessonExam>
      <br>
      <CourseList :lesson="lesson" v-if="!isLoading"></CourseList>
      <div class="row">
        <div class="col-sm-12">
          <product-selector></product-selector>
        </div>
      </div>
    </div>
  </section>
</template>

<script>
  import LessonExam from './lesson-exam'
  import CourseList from './course-list'
  import ProductSelector from './product-selector'
  import SmallHeader from '../layouts/main/header/index-small-image'
  import ContentLoader from './content-loader'

  export default {
    name: "",
    components: {
      LessonExam,
      ProductSelector,
      SmallHeader,
      ContentLoader,
      CourseList
    },
    data() {
      return {
        lesson: {lessonName: '', courses:[]},
        isLoading: true,
      }
    },
    methods: {
      async loadLesson() {
        this.isLoading = true;
        this.lesson = (await this.$http.get("/api/gradefieldlessons/" + this.$route.params.lessonId)).data;
        this.isLoading = false;
      },
    },
    async mounted() {
      await this.loadLesson()
    },

    watch: {
      '$route'(to, from) {
        this.loadLesson();
        window.scroll({
          top: 0,
          left: 0,
          behavior: 'smooth'
        });
      }
    },
  }
</script>

<style scoped>

</style>
