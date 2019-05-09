<template>
  <section>
    <header class="container">
      <div class="row box">
        <div class="col-md-3 header-side-image">
          <img src="../../../assets/images/Icons8_flat_graduation_cap.svg.png" alt>
        </div>
        <div class="col-md-9">
          <h1 class="title">{{course.title}}</h1>
        </div>
      </div>
    </header>

    <div class="container">
      <div class="row">
        <div class="col-md-3 p-0">
          <div class="box p-3">
            <span class="two-side-title">اطلاعات دوره</span>
            <ul class="line-separated">
              <li>
                <i class="fas fa-clipboard-list"></i>&nbsp; تعداد سرفصل ها :
                <p class="float-right">{{course.contents.length}}</p>
              </li>
              <li v-if="playingItem">
                <i class="far fa-clock"></i> &nbsp;مدت زمان دوره :
                <p class="float-right">{{course.duration}}</p>
              </li>
              <li>
                <p class="price-title">
                  <i class="fas fa-dollar-sign"></i> &nbsp; قیمت :
                </p>
                <p class="float-right price">{{course.priceAsTomans}} تومان</p>
              </li>
            </ul>
            <button class="fill" @click="addToCart">
              <i class="fas fa-cart-plus"></i> افزودن به سبد خرید
            </button>
          </div>
          <div class="box p-3 mt-2">
            <img class="author" src="../../../assets/images/5920.jpg" alt>
            <h6 class="text-center mt-2">{{course.teacher}}</h6>
            <span class="two-side-title">درباره مدرس</span>
            <p class="text">{{course.teacherDescription}}</p>
          </div>
        </div>
        <div class="col-md-9 p-0 pl-3" ref="videoContainer">
          <video-player
            v-if="playingItem"
            class="video-player-box mb-3"
            ref="player"
            :options="playerOptions"
            :playsinline="true"
          ></video-player>

          <div class="box mb-3 p-3">
            <span class="two-side-title-large">توضیحات دوره</span>
            <p class="text">{{course.description}}</p>
            <span class="two-side-title-large">رشته های مرتبط</span>
            <p class="text">
              <span
                style="color:red;"
              >{{course.relatedLessonFields.length && course.relatedLessonFields[0].gradeName}}</span> /
              <span
                v-for="field in course.relatedLessonFields"
                :key="field.id"
              >{{field.fieldName}} ,</span>
            </p>
            <span class="two-side-title-large">سر فصل ها</span>
            <div class="course-items">
              <div class="line"></div>
              <ul>
                <li @click="play(item)" v-for="(item,idx) in course.contents" :key="item.id">
                  <span v-if="item.isPreview" class="row-number">{{ idx + 1 }}</span>
                  <span v-if="!item.isPreview" class="row-number">
                    <i class="fas fa-lock"></i>
                  </span>
                  <span v-if="item.contentType==0"
                    :class="['item-title',item.isPreview?'can-be-played-online':'',playingItem.id==item.id?'playing':'']"
                  >{{item.title}}</span>
                  <span v-if="item.contentType==1" class="item-title">
                    <template v-if="!item.isPreview">{{item.title}}</template>  
                    <a style="color:#787878" v-if="item.isPreview" :href="item.filePath" target="_blank" >{{item.title}}</a>
                  </span>
                  <span v-if="item.contentType == 0" class="duration">{{item.videoDuration}}</span>
                  <span class="line-vertical"></span>
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
import SmallHeader from "../../layouts/main/header/index-small-image";
import productTypes from "../../../enums/prouctTypes";
import { videoPlayer } from "vue-video-player";
import "video.js/dist/video-js.css";
import EventBus from "../../../event-bus";
import _ from "lodash";

export default {
  name: "",
  components: {
    SmallHeader,
    videoPlayer
  },
  data() {
    return {
      course: { relatedLessonFields: [], contents: [] },
      playingItem: {},
      videoContainerWidth: 100
    };
  },
  computed: {
    playerOptions() {
      return {
        muted: false,
        language: "fa",
        playbackRates: [0.7, 1.0, 1.5, 2.0],
        width: this.videoContainerWidth,
        height: 313,
        sources: [
          {
            type: "video/mp4",
            src: this.playingItem.filePath
          }
        ]
      };
    }
  },
  methods: {
    show(item) {
      if (!item.isPreview) {
        let firstFreeItemIdx = _.findIndex(
          this.course.contents,
          x => x.isPreview
        );
        this.$router.replace({
          ...this.$route,
          query: { ...this.$route.query, item: firstFreeItemIdx }
        });
        return;
      }
      this.$router.replace({
        ...this.$route,
        query: {
          ...this.$route.query,
          item: this.course.contents.indexOf(item)
        }
      });
    },
    showSelectedItemFromQuery() {
      let firstFreeItemIdx = _.findIndex(
        this.course.contents,
        x => x.isPreview &&x.contentType==0
      );
      this.play(this.course.contents[firstFreeItemIdx]);
    },
    async play(item) {
      if(item.contentType === 1)
      return;
      this.playingItem = item;
      window.scrollTo({
        top: this.$refs.videoContainer.offsetTop - 100,
        left: 0,
        behavior: "smooth"
      });
      setTimeout(() => this.$refs.player.player.play(), 1000);
    },
    addToCart() {
      this.$http
        .post("/api/cart", {
          itemId: this.course.id,
          itemType: productTypes.teacherCourse
        })
        .then(res => {
          if (res.data.status === 1) {
            this.$swal({
              type: "success",
              title: "انجام شد",
              text: res.data.message,
              confirmButtonText: "باشه !"
            });
            EventBus.$emit("cart-item-added");
          }
        });
    },
    resizeVideContainer() {
      this.videoContainerWidth = this.$refs.videoContainer.clientWidth - 16;
    },
    async loadCourse() {
      this.course = (await this.$http.get(
        "/api/courses/" + this.$route.params.id
      )).data;
    }
  },
  beforeDestroy: function() {
    window.removeEventListener("resize", this.resizeVideContainer);
  },
  async mounted() {
    window.addEventListener("resize", this.resizeVideContainer);
    this.resizeVideContainer();
    await this.loadCourse();
    this.showSelectedItemFromQuery();
      window.document.title = this.course.title + " | استاد زانیس"
  }
};
</script>

<style scoped lang="scss">
@import "../../../assets/_variables.scss";

.box {
  box-shadow: 0 1px 8px rgba(0, 0, 0, 0.12), 0 1px 2px rgba(0, 0, 0, 0.24);
}

p.text {
  text-align: justify;
  line-height: 2;
  color: #787878;
}

span {
  line-height: 3;
}
.header-side-image {
  display: flex;
  background-color: $yellow;
  img {
    height: 76px;
    margin: 10px auto;
  }
}
.title {
  color: #656565;
  padding-top: 21px;
  padding-right: 17px;
  font-size: 27px;
}
header {
  margin-top: 50px;
  margin-bottom: 20px;
}
.text {
  line-height: 29px;
  color: #7b7b7b;
  text-align: justify;
}
.two-side-title-large {
  color: #535252;
  display: block;
  text-align: center;
  position: relative;
  &:before,
  &:after {
    top: 23px;
    content: " ";
    background-color: #efe775;
    width: 42%;
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
.two-side-title {
  color: #535252;
  display: block;
  text-align: center;
  position: relative;
  &:before,
  &:after {
    top: 23px;
    content: " ";
    background-color: #efe775;
    width: 30%;
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
ul.line-separated {
  color: #656565;
  list-style: none;
  padding: 0px;
  li {
    &:first-child {
      border-top: 1px solid #ffffff;
    }
    p {
      margin-left: 10px;
    }
    padding: 10px 0px;
    border-top: 1px solid #cecece;
  }
}
.price {
  border-radius: 3px;
  background-color: #80e051;
  padding: 7px;
  box-shadow: 0px 0px 4px #8ba97c;
}
.price-title {
  display: inline-block;
  padding-top: 6px;
}
.fill {
  border-radius: 8px;
  overflow: hidden;
  width: 100%;
  height: 60px;
  background-color: #ffc107;
  color: black;
  position: relative;
  i {
    right: 16px;
    position: absolute;
    top: 21px;
    color: #464646;
  }
}

.fill::after {
  width: 90px;
  height: 90px;
  position: absolute;
  right: -41px;
  top: -17px;
  background: hsla(0, 0%, 100%, 0.21);
  content: "";
  border-radius: 50%;
  transition: all 0.7s;
}

.fill:hover::after {
  width: 900px;
  height: 900px;
  right: -450px;
  top: -450px;
  -webkit-transform: unset;
  transform: unset;
}
.author {
  width: 100px;
  margin: auto;
  display: block;
  border: 5px solid #efe778;
  border-radius: 50%;
}
.course-items {
  color: #6f6f6f;
  ul {
    list-style: none;
    padding: 0px;
    li {
      position: relative;
      margin-bottom: 29px;
      .line-vertical {
        position: absolute;
        border-bottom: 1px solid #9a9a9a;
        top: 57px;
        width: 93%;
        right: 48px;
      }
    }
  }
  position: relative;
  margin: 10%;
  .line {
    content: " ";
    height: 98%;
    position: absolute;
    border-left: 4px solid #efe778;
    top: 0;
    right: 20px;
    z-index: -1;
  }
  .row-number {
    background-color: #e0d20b;
    border-radius: 50%;
    display: inline-block;
    width: 45px;
    height: 45px;
    text-align: center;
  }
  .duration {
    float: left;
    padding-left: 15px;
  }
  .item-title {
    padding-right: 15px;
  }
}
.can-be-played-online {
  cursor: pointer;
}
.playing {
  color: black;
}
</style>
