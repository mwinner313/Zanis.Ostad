import Vue from 'vue'
import VueRouter from 'vue-router'
import {
  routes
} from './routes'

Vue.use(VueRouter)

let router = new VueRouter({
  mode: 'history',
  routes,
  scrollBehavior(to, from, savedPosition) {
    if (to.name === from.name) {
      console.log(to.name)
      console.log(from.name)
      return false
    }
    return {
      x: 0,
      y: 0
    }
  }
});

export default router
