<template>
  <div>
    <header class="container">
      <div class="row box">
        <div class="col-md-3 header-side-image">
          <img src="../../assets/images/message_center_prescribing_218621 (1).png" alt>
        </div>
        <div class="col-md-9">
          <h1 class="title">نمونه سوالات {{lesson.lessonName}}</h1>
        </div>
      </div>
    </header>
    <div class="container mb-5">
      <div class="row">
        <div class="col-md-3 p-0">
          <div class="box p-3">
            <span class="two-side-title">اطلاعات</span>
            <ul class="line-separated">
              <li>
                <i class="fas fa-clipboard-list"></i>&nbsp; تعداد نمونه سوالات :
                <p class="float-right">{{examCount}}</p>
              </li>
              <li>
                <i class="far fa-clock"></i> &nbsp;تعداد پاسخ های تشریحی یا کلیدی :
                <p class="float-right">{{answerCount}}</p>
              </li>
              <li>
                <p class="price-title">
                  <i class="fas fa-dollar-sign"></i> &nbsp; قیمت :
                </p>
                <p class="float-right price">{{lesson.examSamplesPrice}} تومان</p>
              </li>
            </ul>
            <button class="fill" @click="addToCart">
              <i class="fas fa-cart-plus"></i> افزودن به سبد خرید
            </button>
          </div>
        </div>
        <div class="col-sm-9 p-0 pl-3">
          <div class="box p-3">
            <ul class="file-items">
              <li v-for="exam in lesson.exams" :key="exam.id">{{exam.title}}</li>
            </ul>
            <h3
              v-if="!lesson.exams.length"
              class="text-center m-5 no-content-text"
            >برای این درس نمونه سوالی وجود ندارد</h3>
          </div>
        </div>
      </div>
    </div>
  </div>
</template>

<script>
import productTypes from "../../enums/prouctTypes";
import EventBus from "../../event-bus";
export default {
  name: "",
  data() {
    return {
      isAddingToCart: false,
      lesson: { exams: [] }
    };
  },
  async mounted() {
    await this.loadLessonAndExams();
    window.document.title =
      "نمونه سوالات " + this.lesson.lessonName + " | استاد زانیس";
  },
  computed: {
    examCount() {
      return this.lesson.exams.filter(r => r.type == 0).length;
    },
    answerCount() {
      return this.lesson.exams.filter(r => r.type == 2 || r.type == 1).length;
    }
  },
  methods: {
    async loadLessonAndExams() {
      let { data: lesson } = await this.$http.get(
        "/api/GradeFieldLessons/" + this.$route.params.lessonId
      );
      this.lesson = lesson;
    },
    async addToCart() {
      this.isAddingToCart = true;
      try {
        let res = await this.$http.post("/api/cart", {
          itemId: this.lesson.lessonId,
          itemType: productTypes.lessonExam
        });
        this.isAddingToCart = false;
        this.$swal({
          type: "success",
          title: "انجام شد",
          text: res.message,
          confirmButtonText: "باشه !"
        });
        EventBus.$emit("cart-item-added");
      } catch (e) {
        this.isAddingToCart = false;
      }
    }
  }
};
</script>

<style scoped lang="scss">
@import "../../assets/_variables.scss";
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
  padding-top: 31px;
  padding-right: 17px;
  font-size: 27px;
  font-weight: normal;
}
header {
  margin-top: 50px;
  margin-bottom: 20px;
}
.box {
  box-shadow: 0 1px 8px rgba(0, 0, 0, 0.12), 0 1px 2px rgba(0, 0, 0, 0.24);
}
.no-content-text {
  color: #797979;
}
ul.file-items {
  list-style: none;
  li {
    color: #797979;
  }
  li::before {
    content: "\2022";

    color: #efe778;
    font-weight: bold;
    display: inline-block;
    width: 1em;
    margin-right: -1em;
  }
}
.two-side-title {
  color: #535252;
  display: block;
  text-align: center;
  position: relative;
  &:before,
  &:after {
    top: 10px;
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
    font-size: 13px;
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
</style>
