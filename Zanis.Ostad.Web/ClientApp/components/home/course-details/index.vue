<template>
  <section>
    <small-header :title="course.title"></small-header>
    <div class="course-header">
      <div class="container">
        <div class="row p-2">
          <div class="col-sm-2">
            <div class="teacher-avatar"
                 :style="imageUrl">
            </div>
          </div>
          <div class="col-sm-6 teacher-name">
            <p> مدرس : {{course.teacher}} </p>
            <p>
              قیمت : <span class="text-orange">{{course.priceAsTomans | currency}}  تومان</span>
            </p>
          </div>
          <div class="col-sm-4">
            <button @click.prevent="addToCart" class="add-cart float-right">افزودن به سبد خرید</button>
          </div>
        </div>
      </div>
    </div>
    <div class="video-container">
      <div class="container">
        <div class="row no-gutters">
          <div class="col-sm-12 " ref="videoContainer">
            <video-player v-if="playingItem" class="video-player-box"
                          :options="playerOptions"
                          :playsinline="true">
            </video-player>
          </div>
        </div>
      </div>
    </div>
    <div class="course-details">
      <div class="container">
        <div class="row">
          <div class="col-sm-12 " v-show="course.description">
            <div class="card-d">
              <h6 class="card-title">توضیحات</h6>
              <br>
              <br>
              <p class="text">{{course.description}}</p>
            </div>
          </div>
          <div class="col-sm-12 ">
            <div class="card-d">
              <h6 class="card-title">رشته های مرتبط</h6>
              <br>
              <br>
              <span class="red">{{course.gradeTitle}} - </span>
              <span v-for="field in course.relatedFields" class="green"> {{field}} , </span>
            </div>
          </div>
          <div class="col-sm-12 ">
            <div class="card-d">
              <h6 class="card-title">سرفصل</h6>
              <br>
              <br>
              <ul class="course-item-list">
                <li v-for="(item,index) in course.contents" :key="item.id"
                    :class="[
              (item.id===playingItem.id?'active':''),
              (item.isPreview?'':'lock')
              ]"
                    @click="show(item)">
                  {{index +1 +" - "}}{{item.title}}
                  <i v-show="!item.isPreview" class="fas fa-lock float-right"></i>
                </li>
              </ul>
            </div>
          </div>

        </div>
      </div>
    </div>
  </section>
</template>

<script>
  import SmallHeader from '../layouts/main/header/index-small-image'
  import productTypes from '../../enums/prouctTypes'
  import {videoPlayer} from 'vue-video-player'
  import 'video.js/dist/video-js.css'
  import EventBus from '../../event-bus';
  import _ from 'lodash'

  export default {

    name: "",
    components: {
      SmallHeader,
      videoPlayer
    },
    data() {
      return {
        course: {relatedFields: []},
        playingItem: {},
        videoContainerWidth: 100,
      }
    },
    computed: {
      imageUrl() {
        return `background-image: url('${this.course.teacherAvatar}')`;
      },
      playerOptions() {
        return {
          muted: false,
          language: 'fa',
          playbackRates: [0.7, 1.0, 1.5, 2.0],
          width: this.videoContainerWidth,
          sources: [{
            type: "video/mp4",
            src: this.playingItem.filePath
          }],
        }
      }
    },
    methods: {
      show(item) {
        if (!item.isPreview) {
          let firstFreeItemIdx = _.findIndex(this.course.contents, x => x.isPreview);
          this.$router.replace({...this.$route, query: {...this.$route.query, item: firstFreeItemIdx}});
          return
        }
        this.$router.replace({...this.$route, query: {...this.$route.query, item: this.course.contents.indexOf(item)}})
      },
      showSelectedItemFromQuery() {
        let item = (this.$route.query.item && this.$route.query.item * 1)
          ? this.course.contents[this.$route.query.item * 1]
          : this.course.contents[0];

        if (!item || !item.isPreview) {
          let firstFreeItemIdx = _.findIndex(this.course.contents, x => x.isPreview);
          this.$router.replace({...this.$route, query: {...this.$route.query, item: firstFreeItemIdx}});
          return
        }
        this.play(item);
      },
      async play(item) {
        this.playingItem = item;
      },
      addToCart() {
        this.$http.post('/api/cart', {
          itemId: this.course.id,
          itemType: productTypes.teacherCourse
        }).then(res => {
          if (res.data.status === 1) {
            this.$swal({
              type: 'success',
              title: 'انجام شد',
              text: res.data.message,
              confirmButtonText: 'باشه !',
            });
            EventBus.$emit('cart-item-added');
          }
        })
      },
      resizeVideContainer() {
        this.videoContainerWidth = this.$refs.videoContainer.clientWidth
      },
      async loadCourse() {
        this.course = (await this.$http.get('/api/courses/' + this.$route.params.id)).data;
      }
    },
    ready: function () {
    },
    beforeDestroy: function () {
      window.removeEventListener('resize', this.resizeVideContainer)
    },
    async mounted() {
      window.addEventListener('resize', this.resizeVideContainer);
      this.resizeVideContainer();
      await this.loadCourse();
      this.showSelectedItemFromQuery();
    },
    watch: {
      '$route'(to, from) {
        this.showSelectedItemFromQuery();
      },
    }
  }
</script>

<style scoped>
  p.text {
    text-align: justify;
    line-height: 2;
    color: #787878;
  }

  span {
    line-height: 3;
  }
</style>
