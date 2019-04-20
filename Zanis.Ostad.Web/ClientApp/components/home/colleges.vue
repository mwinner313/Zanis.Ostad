<template>
  <div class="container">
    <h3 class="two-side-title">دانشگاه ها</h3>
    <div class="row">
      <div class="col-sm-12 d-flex flex-wrap universities">
        <div class="university-box" :key="item.id" v-for="item in universities">
          <router-link :to="'/college/'+item.id+'-'+item.permaLink">
            <figure>
              <simple-svg
                :filepath="item.iconPath"
                fill="#424f5a"
                stroke="#424f5a"
                :width="'150px'"
                :height="'150px'"
                :id="'play-svg'"
              />
            </figure>
            <span>{{item.name}}</span>
          </router-link>
        </div>
      </div>
    </div>
  </div>
</template>

<script>
export default {
  name: "",
  data() {
    return {
      universities: []
    };
  },
  methods: {
    loadColleges() {
      this.$http.get("/api/colleges").then(res => {
        this.universities = res.data;
      });
    }
  },
  mounted() {
    this.loadColleges();
  }
};
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
  box-shadow: 0 1px 8px rgba(0, 0, 0, 0.12), 0 1px 2px rgba(0, 0, 0, 0.24);
  transition: all 0.3s cubic-bezier(0.25, 0.8, 0.25, 1);
  &:hover {
    svg {
      fill: yellow !important;
      stroke: yellow !important;
    }
    box-shadow: 0 14px 28px rgba(0, 0, 0, 0.25), 0 10px 10px rgba(0, 0, 0, 0.22);
  }
  span {
    font-size: 17px;
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
  &:before,
  &:after {
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
