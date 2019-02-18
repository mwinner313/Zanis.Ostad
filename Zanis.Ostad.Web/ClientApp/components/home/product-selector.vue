<template>
  <div class="d-none d-sm-block">
    <section class="row">
      <div class="col-md-12 product-types p-0">
        <product-type :is-active="selectedProductCategory===1" @select="changeProductType(productTypes.lessonExam,1)"
                      size="sm"
                      image-path="/assets/images/نمونه سوالات1.svg">
          <p slot="title" class="product-title ">نمونه سوالات</p>
          <p slot="title" class="product-title">با کلید تست تشریحی</p>
        </product-type>
        <product-type :is-active="selectedProductCategory===2" @select="changeProductType(productTypes.teacherCourse,2)"
                      size="sm"
                      image-path="/assets/images/نمونه سوالات 2.svg">
          <p slot="title" class="product-title ">نمونه سوالات</p>
          <p slot="title" class="product-title">با حل تشریحی استاد</p>
        </product-type>
        <product-type :is-active="selectedProductCategory===3" @select="changeProductType(productTypes.teacherCourse,3)"
                      size="sm"
                      image-path="/assets/images/شب امتحان.svg">
          <p slot="title" class="product-title ">آموزش شب</p>
          <p slot="title" class="product-title">امتحانی دروس</p>
        </product-type>
        <product-type :is-active="selectedProductCategory===4" @select="changeProductType(productTypes.teacherCourse,4)"
                      size="sm"
                      image-path="/assets/images/خط کشی نکات مهم.svg">
          <p slot="title" class="product-title ">خط کشی نکات</p>
          <p slot="title" class="product-title">مهم در کتاب</p>
        </product-type>
        <product-type :is-active="selectedProductCategory===5" @select="changeProductType(productTypes.teacherCourse,5)"
                      size="lg"
                      image-path="/assets/images/کامل دروس.svg">
          <p slot="title" class="product-title">آموزش کامل دوره</p>
        </product-type>
      </div>
    </section>
    <!--grades-->
    <section v-if="selectedProductType" class="row gray-shadow">
      <a v-for="grade in grades" @click.prevent="selectGrade(grade.id)"
         :class="['col-sm-4','grade' ,(selectedGradeId===grade.id)?'active':'']"> <span>{{grade.name}}</span></a>
    </section>
    <!--college-field-header-->
    <section v-if="selectedGradeId && !isContentLoading" class="row  gray-shadow">
      <a class="col-sm-4 university"><span> دانشگاه ها</span></a>
      <a :class="[(selectedFieldId===0?'col-sm-8':'col-sm-4'), 'university']"><span>رشته ها</span></a>
      <a v-if="selectedFieldId" class="col-sm-4 university"><span> دروس مربوطه</span></a>
    </section>

    <content-loader v-show="isContentLoading"></content-loader>
    <section v-show="!isContentLoading && selectedGradeId" class="row gray-shadow">
      <div class="col-sm-4 p-0">
        <ul class="column-group">
          <li :class="[(college.id===selectedCollegeId?'active':'')]" @click="selectCollege(college.id)"
              v-for="college in colleges">{{college.name}}
          </li>
        </ul>
      </div>
      <div :class="[(selectedFieldId===0?'col-sm-8':'col-sm-4'),'p-0']">
        <ul class="column-group ">
          <li :class="[(field.id===selectedFieldId?'active':'')]" @click="selectField(field.id)"
              v-for="field in fields">{{field.name}}
          </li>
        </ul>
      </div>
      <div v-if="selectedFieldId" class="col-sm-4 p-0">
        <content-loader-small v-show="loadingLessons"></content-loader-small>
        <ul class="column-group" v-show="!loadingLessons">
          <li :class="[(lesson.id===selectedLessonId?'active':'')]" @click="selectLesson(lesson.id)"
              v-for="lesson in lessons">{{lesson.lessonName}}
          </li>
        </ul>
      </div>
    </section>
  </div>
</template>

<script>
  import ContentLoader from './content-loader'
  import ContentLoaderSmall from './content-loader-small'
  import ProductType from './product-type';
  import productTypes from '../../enums/prouctTypes';

  export default {
    name: "",
    components: {
      ProductType,
      ContentLoader,
      ContentLoaderSmall
    },
    data() {
      return {
        grades: [],
        colleges: [],
        fields: [],
        lessons: [],
        loadingLessons:false,
        productTypes,
        selectedGradeId: 0,
        selectedProductType: productTypes.lessonExam,
        selectedProductCategory: 0,
        selectedCollegeId: 0,
        selectedFieldId: 0,
        selectedLessonId: 0,
        isContentLoading: false
      }
    },
    methods: {
      loadColleges() {
        this.isContentLoading = true;
        this.$http.get('/api/colleges', {
          params: {
            productType: this.selectedProductType,
            gradeId: this.selectedGradeId
          }
        }).then(res => {
          setTimeout(() => {
            this.colleges = res.data;
            this.fillEmptySpaceInTable();
            this.isContentLoading = false;
          }, 1000)
        })
      },
      async loadGrades() {
        this.grades = (await this.$http.get('/api/grades', {
          params: {
            productType: this.selectedProductType,
          }
        })).data;
        this.fillEmptySpaceInTable();
      },
      async loadFields() {
        this.fields = (await this.$http.get('/api/fields', {
          params: {
            collegeId: this.selectedCollegeId,
            gradeId: this.selectedGradeId,
            productType: this.selectedProductType
          }
        })).data;
        this.fillEmptySpaceInTable();
      },

      loadLessons() {
        this.loadingLessons=true;
        this.$http.get('/api/gradefieldlessons', {
          params: {
            fieldId: this.selectedFieldId,
            productType: this.selectedProductType
          }
        }).then(res => {
         setTimeout(()=>{
           this.loadingLessons=false;
           this.lessons = res.data;
           this.fillEmptySpaceInTable();
         },1000)
        });
      },
      async changeProductType(type, cat) {
        this.selectedProductType = type;
        this.selectedProductCategory = cat;
        if (this.selectedGradeId) {
          await this.loadColleges()
        }
      },
      selectGrade(gradeId) {
        this.selectedGradeId = gradeId;
        this.selectedCollegeId = 0;
        this.lessons = [];
        this.fields = [];
        this.loadColleges();
      },
      async selectCollege(collegeId) {
        if(!collegeId)return;
        this.selectedCollegeId = collegeId;
        this.selectedFieldId = 0;
        this.selectedLessonId = 0;
        await this.loadFields();
      },
      selectField(fieldId) {
        if(!fieldId)return;
        this.selectedFieldId = fieldId;
        this.loadLessons();
      },
      selectLesson(lessonId) {
        if (!lessonId) return;
        this.selectedLessonId = lessonId;
        this.$router.push({name: 'lesson', params: {lessonId}});
        window.scroll({
          top: 0,
          left: 0,
          behavior: 'smooth'
        });
      },
      fillEmptySpaceInTable() {
        let max = Math.max(this.fields.filter(x => x.code).length, this.colleges.filter(x => x.code).length, this.lessons.filter(x => x.lessonCode).length);
        for (let x = this.colleges.length; x < max; x++)
          this.colleges.push({});
        if (max < this.colleges.length)
          this.colleges.splice(max, this.colleges.length);
        for (let x = this.lessons.length; x < max; x++)
          this.lessons.push({});
        if (max < this.lessons.length)
          this.lessons.splice(max, this.lessons.length);
        for (let x = this.fields.length; x < max; x++)
          this.fields.push({});
        if (max < this.fields.length)
          this.fields.splice(max, this.fields.length);
      }
    },
    async mounted() {
      await this.loadGrades();
      this.changeProductType(productTypes.lessonExam, 1);
      await this.selectGrade(this.grades[0].id);
    }
  }
</script>

<style scoped>

</style>
