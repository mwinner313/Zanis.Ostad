import Vue from 'vue'
import axios from 'axios'
import router from './router/index'
import store from './store'
import { sync } from 'vuex-router-sync'
import App from 'components/app-root'
import { FontAwesomeIcon } from './icons'
import BootstrapVue from 'bootstrap-vue'
import VueSweetalert2 from 'vue-sweetalert2';
import storageHelper from 'storage-helper';
import Toaster from 'v-toaster'
import 'v-toaster/dist/v-toaster.css'
import VueCurrencyFilter from 'vue-currency-filter'
import VueVideoPlayer from 'vue-video-player'
import 'video.js/dist/video-js.css'
import 'element-ui/lib/theme-chalk/index.css';
import locale from 'element-ui/lib/locale/lang/fa'
import Element from 'element-ui'
import MomentJalali from 'vue-moment-jalaali'
import registerPersianDateFilter from './persianDateFilter'
import VueAwesomeSwiper from 'vue-awesome-swiper'
import 'swiper/dist/css/swiper.css'
import './assets/fonts/web-fonts-with-css/css/fontawesome-all.css'
Vue.use(VueAwesomeSwiper, /* { default global options } */)
Vue.use(Element,{locale});
Vue.use(MomentJalali);
Vue.use(VueVideoPlayer, /* {
  options: global default options,
  events: global videojs events
} */);
Vue.use(VueCurrencyFilter,
  {
    symbol : '',
    thousandsSeparator: ',',
    fractionCount: 0,
    fractionSeparator: ',',
    symbolPosition: 'front',
    symbolSpacing: true
  });
Vue.use(Toaster, {timeout: 1000});
Vue.use(VueSweetalert2);
Vue.use(BootstrapVue);
Vue.component('icon', FontAwesomeIcon);
Vue.prototype.$http = axios;
sync(store, router);
registerPersianDateFilter();
const app = new Vue({
  store,
  router,
  ...App
});
axios.defaults.headers.common['Authorization'] = storageHelper.getItem('Authorization')
//  axios.defaults.baseURL = 'http://192.168.8.74:2001';
axios.defaults.headers.post['Content-Type']='application/json';
axios
  .interceptors
  .response
  .use(function (response) {
    return response;
  }, function (error) {
    if(error &&error.response && error.response.status==401){
      storageHelper.removeItem('Authorization');
      storageHelper.removeItem('user');
      axios.defaults.headers.common['Authorization']=undefined;
      app.$root.$emit('bv::show::modal', 'login-dialog')
    }
    return Promise.reject(error.response);
  });

export {
  app,
  router,
  store
}


