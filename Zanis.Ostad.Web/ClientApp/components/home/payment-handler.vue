<template>
  <div>
    <section class="container">
      <div class="row" style="    margin: 8%;">
        <ul ontouchstart>
          <li v-if="res.status===statuses.success">
            <div class="flash success">
              <div class="inside">
                <figure class="icon">
                  <svg viewBox="0 0 84 84">
                    <g fill="none">
                      <circle cx="42" cy="42" fill="#BDE3CA" r="42"></circle>
                      <path
                        d="M30,62 C33.0710678,65.0710678 37.3137085,66.9705627 42,66.9705627 C46.6862915,66.9705627 50.9289322,65.0710678 54,62"
                        stroke-width="5"
                        stroke="#5FB67D"
                      ></path>
                      <circle cx="30" cy="42" fill="#5FB67D" r="3"></circle>
                      <circle cx="54" cy="42" fill="#5FB67D" r="3"></circle>
                    </g>
                  </svg>
                </figure>
                <div>
                  <h2>پرداخت موفق!</h2>
                  <p class="text-center">تا چند لحطه دیگر به صفحه چیزایی که خریدید میرید</p>
                </div>
              </div>
            </div>
          </li>
          <li v-if="res.status===statuses.fail">
            <div class="flash error">
              <div class="inside">
                <figure class="icon">
                  <svg viewBox="0 0 84 84">
                    <g fill="none">
                      <circle cx="42" cy="42" fill="#F4B6BA" r="42"></circle>
                      <path
                        d="M30,65 C33.0710678,61.9107443 37.3137085,60 42,60 C46.6862915,60 50.9289322,61.9107443 54,65"
                        stroke-width="5"
                        stroke="#C46465"
                      ></path>
                      <circle cx="30" cy="45" fill="#C46465" r="3"></circle>
                      <circle cx="54" cy="45" fill="#C46465" r="3"></circle>
                    </g>
                  </svg>
                </figure>
                <div class="text-center">
                  <h2>اوپس!</h2>
                  <p>پرداخت شما ناموفق بود.</p>
                  <a @click.prevent="goToHome" class="btn btn-success">بازگشت به صفحه اصلی</a>
                </div>
              </div>
            </div>
          </li>
          <li v-show="isLoading">
            <div class="flash working">
              <div class="bg">
                <div class="bg-icon"></div>
                <div class="bg-btns"></div>
              </div>
              <div class="inside">
                <figure class="icon">
                  <svg viewBox="0 0 84 84">
                    <g fill="none">
                      <circle cx="42" cy="42" fill="#EEE5C6" r="42"></circle>
                      <path
                        d="M30,62 C33.0710678,65.0710678 37.3137085,66.9705627 42,66.9705627 C46.6862915,66.9705627 50.9289322,65.0710678 54,62"
                        stroke-width="5"
                        stroke="#E1C55E"
                      ></path>
                      <circle cx="30" cy="42" fill="#E1C55E" r="3"></circle>
                      <circle cx="54" cy="42" fill="#E1C55E" r="3"></circle>
                    </g>
                  </svg>
                </figure>
                <div>
                  <h2>در حال پردازش !</h2>
                  <p class="text-center">چند لحظه صبر کنید</p>
                </div>
                <div class="btns">
                  <button>
                    <svg viewBox="0 0 56 56">
                      <g fill="#E1C55E">
                        <circle cx="16" cy="28" r="3"></circle>
                        <circle cx="28" cy="28" r="3"></circle>
                        <circle cx="40" cy="28" r="3"></circle>
                      </g>
                    </svg>
                  </button>
                </div>
              </div>
            </div>
          </li>
        </ul>
      </div>
    </section>
  </div>
</template>

<script>
import SmallHeader from "../layouts/main/header/index-small-image";
import statuses from "../../enums/response-status";

export default {
  name: "",
  components: {
    SmallHeader
  },
  data() {
    return {
      statuses,
      message: "در حال پردازش",
      isLoading: true,
      res: {}
    };
  },
  methods: {
    goToHome() {
      this.$router.push({ name: "home" });
    },
    async confirm() {
      let bankRes = Object.assign({}, this.$route.query);
      this.$router.replace({});
      let res = (await this.$http.post("/api/order/confirm", bankRes)).data;
      setTimeout(() => {
        this.res = res;
        this.isLoading = false;
        this.message =
          this.res.status === statuses.success
            ? "پرداخت موفق"
            : "پرداخت ناموفق";
        if (res.status === statuses.success) {
          setTimeout(() => this.$router.push({ name: "my-lessons" }), 2000);
        }
      }, 1000);
    }
  },
  mounted() {
    this.confirm();
  }
};
</script>

<style scoped>
*,
*:before,
*:after {
  box-sizing: border-box;
}

* {
  -webkit-tap-highlight-color: rgba(0, 0, 0, 0);
}

*:focus {
  outline: none !important;
}

::-moz-selection {
  background: none;
}

::selection {
  background: none;
}

a {
  cursor: pointer;
}

body,
html {
  height: 100%;
}

body {
  display: flex;
  align-content: center;
  justify-content: center;
  background: #1e3647;
  color: white;
  font-family: "brandon-grotesque", "Brandon Grotesque", sans-serif;
}

ul {
  display: flex;
  align-content: center;
  justify-content: center;
  width: 100%;
  list-style: none;
}

.flash {
  position: relative;
  width: 100%;
  max-width: 288px;
}

.flash svg {
  stroke-linecap: round;
  stroke-linejoin: round;
}

.flash .bg > div {
  position: absolute;
  z-index: -1;
}

.flash .bg .bg-icon {
  top: -52px;
  left: calc(50% - 52px);
  width: 104px;
  height: 104px;
  background: #4b5e6b;
  border-radius: 100%;
}

.flash .bg .bg-btns {
  display: flex;
  align-items: center;
  align-content: center;
  justify-content: space-between;
  position: absolute;
  bottom: -38px;
  left: calc(50% - 88px);
  width: 176px;
}

.flash .bg .bg-btns:before,
.flash .bg .bg-btns:after {
  content: "";
  display: block;
  width: 76px;
  height: 76px;
  background: #4b5e6b;
  border-radius: 100%;
}

.flash .inside {
  border: 4px solid #4b5e6b;
  border-radius: 6px;
  height: 230px;
  padding: 48px 25px 0;
}

.flash h2 {
  font-size: 36px;
  font-weight: 800;
  line-height: 52px;
  text-align: center;
}

.flash p {
  font-size: 20px;
  font-weight: 500;
  line-height: 29px;
}

.flash .btns {
  display: flex;
  align-items: center;
  align-content: center;
  justify-content: space-between;
  position: absolute;
  bottom: -34px;
  left: calc(50% - 84px);
  width: 168px;
}

.flash button {
  position: relative;
  cursor: pointer;
  display: flex;
  align-items: center;
  justify-content: center;
  -webkit-appearance: none;
  -moz-appearance: none;
  appearance: none;
  width: 68px;
  height: 68px;
  border: 0;
  border-radius: 100%;
  padding: 0;
  font-family: "brandon-grotesque", "Brandon Grotesque", sans-serif;
}

.flash button svg {
  will-change: transform;
  transition: all 300ms ease-out;
  -webkit-transform-origin: center center;
  transform-origin: center center;
  width: 56px;
  height: 56px;
  border-radius: 100%;
  background: white;
}

.flash button span {
  display: block;
  position: absolute;
  bottom: -26px;
  color: rgba(255, 255, 255, 0.5);
  font-size: 12px;
  text-transform: uppercase;
}

.flash button:hover svg,
.flash button:focus svg {
  transition: all 400ms cubic-bezier(0.6, 0, 0.1, 2);
  -webkit-transform: scale(1.2);
  transform: scale(1.2);
}

.flash button:active svg {
  transition: all 150ms ease-in;
  -webkit-transform: scale(0.9);
  transform: scale(0.9);
}

.flash .icon {
  position: absolute;
  top: -48px;
  left: calc(50% - 48px);
  width: 96px;
  height: 96px;
  border-radius: 100%;
  padding: 6px;
}

.flash .icon svg {
  width: 84px;
  height: 84px;
}

.flash.success .inside,
.flash.success .icon,
.flash.success button {
  background-color: #5fb67d;
}

.flash.working .inside,
.flash.working .icon,
.flash.working button {
  background-color: #92dbe6;
}

.flash.working .bg .bg-btns,
.flash.working .btns {
  justify-content: center;
}

.flash.working .bg .bg-btns:after,
.flash.working .btns:after {
  display: none;
}

.flash.working button {
  pointer-events: none;
}

.flash.working button svg {
  background: rgba(255, 255, 255, 0.5);
}

.flash.error .inside,
.flash.error .icon,
.flash.error button {
  background-color: #c46465;
}

.flash.error .bg .bg-btns,
.flash.error .btns {
  justify-content: center;
}

.flash.error .bg .bg-btns:after,
.flash.error .btns:after {
  display: none;
}

.flash .icon circle:not(:first-child) {
  -webkit-transform: scale(0);
  transform: scale(0);
  -webkit-animation: tsfm 100ms ease-out both;
  animation: tsfm 100ms ease-out both;
}

.flash .icon circle:nth-child(3) {
  -webkit-transform-origin: 30px 42px;
  transform-origin: 30px 42px;
}

.flash .icon circle:nth-child(4) {
  -webkit-transform-origin: 54px 42px;
  transform-origin: 54px 42px;
}

.flash.success,
.flash.working {
  opacity: 0;
  visibility: hidden;
  -webkit-transform-origin: left top;
  transform-origin: left top;
  -webkit-transform: rotateX(30deg) rotateY(30deg) translateY(300px)
    translateZ(0);
  transform: rotateX(30deg) rotateY(30deg) translateY(300px) translateZ(0);
  will-change: opacity, transform, visibility;
  -webkit-animation: tsfm-op 750ms cubic-bezier(0, 0.6, 0.3, 1) both;
  animation: tsfm-op 750ms cubic-bezier(0, 0.6, 0.3, 1) both;
}

.flash.success {
  -webkit-animation-delay: 250ms;
  animation-delay: 250ms;
}

.flash.success .icon path {
  stroke-dasharray: 26.661;
  stroke-dashoffset: 26.661;
  will-change: stroke-dashoffset;
  -webkit-animation: sdo 250ms ease-out 700ms both;
  animation: sdo 250ms ease-out 700ms both;
}

.flash.success .icon circle:nth-child(3) {
  -webkit-animation-delay: 500ms;
  animation-delay: 500ms;
}

.flash.success .icon circle:nth-child(4) {
  -webkit-animation-delay: 600ms;
  animation-delay: 600ms;
}

.flash.success button span {
  opacity: 0;
  visibility: hidden;
  -webkit-transform: translateY(100%);
  transform: translateY(100%);
  -webkit-animation: tsfm-op 500ms cubic-bezier(0, 0.75, 0.25, 1) both;
  animation: tsfm-op 500ms cubic-bezier(0, 0.75, 0.25, 1) both;
}

.flash.success button:nth-child(1) polyline {
  stroke-dasharray: 36.1;
  stroke-dashoffset: 36.1;
  will-change: stroke-dashoffset;
  -webkit-animation: sdo 350ms ease-out 900ms both;
  animation: sdo 350ms ease-out 900ms both;
}

.flash.success button:nth-child(1) span {
  -webkit-animation-delay: 1400ms;
  animation-delay: 1400ms;
}

.flash.success button:nth-child(2) polyline {
  stroke-dasharray: 24.1;
  stroke-dashoffset: 24.1;
  will-change: stroke-dashoffset;
  -webkit-animation: sdo 250ms ease-out 1000ms both;
  animation: sdo 250ms ease-out 1000ms both;
}

.flash.success button:nth-child(2) line {
  stroke-dasharray: 24;
  stroke-dashoffset: 24;
  will-change: stroke-dashoffset;
  -webkit-animation: sdo 250ms ease-out 1100ms both;
  animation: sdo 250ms ease-out 1100ms both;
}

.flash.success button:nth-child(2) span {
  -webkit-animation-delay: 1500ms;
  animation-delay: 1500ms;
}

.flash.working {
  -webkit-animation-delay: 500ms;
  animation-delay: 500ms;
}

.flash.working .icon circle:nth-child(2) {
  stroke-dasharray: 43.982;
  stroke-dashoffset: 43.982;
  will-change: stroke-dashoffset;
  -webkit-animation: sdo 400ms ease-out 950ms both;
  animation: sdo 400ms ease-out 950ms both;
  -webkit-transform: scale(1);
  transform: scale(1);
}

.flash.working .icon circle:nth-child(3) {
  -webkit-animation-delay: 750ms;
  animation-delay: 750ms;
}

.flash.working .icon circle:nth-child(4) {
  -webkit-animation-delay: 850ms;
  animation-delay: 850ms;
}

.flash.working button circle {
  opacity: 0;
  will-change: opacity;
}

.flash.working button circle:nth-child(1) {
  -webkit-animation: ellipsis-1 900ms ease-out infinite forwards;
  animation: ellipsis-1 900ms ease-out infinite forwards;
}

.flash.working button circle:nth-child(2) {
  -webkit-animation: ellipsis-2 900ms ease-out infinite forwards;
  animation: ellipsis-2 900ms ease-out infinite forwards;
}

.flash.working button circle:nth-child(3) {
  -webkit-animation: ellipsis-3 900ms ease-out infinite forwards;
  animation: ellipsis-3 900ms ease-out infinite forwards;
}

.flash.error {
  opacity: 0;
  -webkit-animation: jitter 225ms ease-in-out 2 both 750ms;
  animation: jitter 225ms ease-in-out 2 both 750ms;
}

.flash.error .icon path {
  stroke-dasharray: 26.661;
  stroke-dashoffset: 26.661;
  will-change: stroke-dashoffset;
  -webkit-animation: sdo 250ms ease-out 1200ms both;
  animation: sdo 250ms ease-out 1200ms both;
}

.flash.error .icon circle:nth-child(3) {
  -webkit-animation-delay: 1000ms;
  animation-delay: 1000ms;
}

.flash.error .icon circle:nth-child(4) {
  -webkit-animation-delay: 1100ms;
  animation-delay: 1100ms;
}

.flash.error button {
  -webkit-transform-origin: center center;
  transform-origin: center center;
  -webkit-transform: rotate(90deg);
  transform: rotate(90deg);
  will-change: transform;
  -webkit-animation: tsfm 570ms ease-out 1000ms both;
  animation: tsfm 570ms ease-out 1000ms both;
}

.flash.error button polyline {
  stroke-dasharray: 18.1;
  stroke-dashoffset: 18.1;
  will-change: stroke-dashoffset;
  -webkit-animation: sdo 200ms ease-out 1300ms both;
  animation: sdo 200ms ease-out 1300ms both;
}

.flash.error button path {
  stroke-dasharray: 66.376;
  stroke-dashoffset: 66.376;
  will-change: stroke-dashoffset;
  -webkit-animation: sdo 650ms ease-out 1400ms both;
  animation: sdo 650ms ease-out 1400ms both;
}

.flash.error button span {
  opacity: 0;
  visibility: hidden;
  -webkit-transform: translateY(100%);
  transform: translateY(100%);
  -webkit-animation: tsfm-op 500ms cubic-bezier(0, 0.75, 0.25, 1) 1700ms both;
  animation: tsfm-op 500ms cubic-bezier(0, 0.75, 0.25, 1) 1700ms both;
}

body {
  align-items: flex-start;
}

ul {
  flex-flow: row wrap;
  padding: 12px 12px 78px;
}

ul li {
  padding: 98px 12px 12px;
}

ul li:nth-child(1) {
  padding-top: 74px;
}

@media only screen and (min-width: 648px) {
  ul li:nth-child(2) {
    padding-top: 74px;
  }
}

@media only screen and (min-width: 960px) {
  body {
    align-items: center;
  }

  ul {
    align-items: center;
    padding: 12px;
  }

  ul li {
    padding: 12px;
  }

  ul li:nth-child(1),
  ul li:nth-child(2) {
    padding-top: 12px;
  }
}

@-webkit-keyframes tsfm {
  to {
    -webkit-transform: none;
    transform: none;
  }
}

@keyframes tsfm {
  to {
    -webkit-transform: none;
    transform: none;
  }
}

@-webkit-keyframes op {
  to {
    opacity: 1;
  }
}

@keyframes op {
  to {
    opacity: 1;
  }
}

@-webkit-keyframes sdo {
  to {
    stroke-dashoffset: 0;
  }
}

@keyframes sdo {
  to {
    stroke-dashoffset: 0;
  }
}

@-webkit-keyframes ellipsis-1 {
  0% {
    opacity: 0;
  }
  20%,
  95% {
    opacity: 1;
  }
  100% {
    opacity: 0;
  }
}

@keyframes ellipsis-1 {
  0% {
    opacity: 0;
  }
  20%,
  95% {
    opacity: 1;
  }
  100% {
    opacity: 0;
  }
}

@-webkit-keyframes ellipsis-2 {
  0%,
  20% {
    opacity: 0;
  }
  40%,
  95% {
    opacity: 1;
  }
  100% {
    opacity: 0;
  }
}

@keyframes ellipsis-2 {
  0%,
  20% {
    opacity: 0;
  }
  40%,
  95% {
    opacity: 1;
  }
  100% {
    opacity: 0;
  }
}

@-webkit-keyframes ellipsis-3 {
  0%,
  40% {
    opacity: 0;
  }
  60%,
  95% {
    opacity: 1;
  }
  100% {
    opacity: 0;
  }
}

@keyframes ellipsis-3 {
  0%,
  40% {
    opacity: 0;
  }
  60%,
  95% {
    opacity: 1;
  }
  100% {
    opacity: 0;
  }
}

@-webkit-keyframes tsfm-op {
  10% {
    opacity: 1;
    visibility: visible;
  }
  100% {
    opacity: 1;
    visibility: visible;
    -webkit-transform: none;
    transform: none;
  }
}

@keyframes tsfm-op {
  10% {
    opacity: 1;
    visibility: visible;
  }
  100% {
    opacity: 1;
    visibility: visible;
    -webkit-transform: none;
    transform: none;
  }
}

@-webkit-keyframes jitter {
  0% {
    -webkit-transform: translateX(0px) translateY(0px);
    transform: translateX(0px) translateY(0px);
    opacity: 0;
  }
  10% {
    -webkit-transform: translateX(4px) translateY(4px);
    transform: translateX(4px) translateY(4px);
    opacity: 0.8;
  }
  20% {
    -webkit-transform: translateX(0px) translateY(0px);
    transform: translateX(0px) translateY(0px);
    opacity: 0.47;
  }
  30% {
    -webkit-transform: translateX(0px) translateY(4px);
    transform: translateX(0px) translateY(4px);
    opacity: 0.3;
  }
  70% {
    -webkit-transform: translateX(-2px) translateY(2px);
    transform: translateX(-2px) translateY(2px);
    opacity: 0.96;
  }
  80% {
    -webkit-transform: translateX(0px) translateY(-4px);
    transform: translateX(0px) translateY(-4px);
    opacity: 0.9;
  }
  90% {
    -webkit-transform: translateX(2px) translateY(-4px);
    transform: translateX(2px) translateY(-4px);
    opacity: 0.81;
  }
  100% {
    -webkit-transform: translateX(0) translateY(0);
    transform: translateX(0) translateY(0);
    opacity: 1;
  }
}

@keyframes jitter {
  0% {
    -webkit-transform: translateX(0px) translateY(0px);
    transform: translateX(0px) translateY(0px);
    opacity: 0;
  }
  10% {
    -webkit-transform: translateX(4px) translateY(4px);
    transform: translateX(4px) translateY(4px);
    opacity: 0.8;
  }
  20% {
    -webkit-transform: translateX(0px) translateY(0px);
    transform: translateX(0px) translateY(0px);
    opacity: 0.47;
  }
  30% {
    -webkit-transform: translateX(0px) translateY(4px);
    transform: translateX(0px) translateY(4px);
    opacity: 0.3;
  }
  70% {
    -webkit-transform: translateX(-2px) translateY(2px);
    transform: translateX(-2px) translateY(2px);
    opacity: 0.96;
  }
  80% {
    -webkit-transform: translateX(0px) translateY(-4px);
    transform: translateX(0px) translateY(-4px);
    opacity: 0.9;
  }
  90% {
    -webkit-transform: translateX(2px) translateY(-4px);
    transform: translateX(2px) translateY(-4px);
    opacity: 0.81;
  }
  100% {
    -webkit-transform: translateX(0) translateY(0);
    transform: translateX(0) translateY(0);
    opacity: 1;
  }
}
</style>
