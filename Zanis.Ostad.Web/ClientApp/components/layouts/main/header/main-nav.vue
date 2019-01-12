<template>
  <div>
    <b-navbar :sticky="true" toggleable="md" id="main-nav">
      <div class="container">
        <b-navbar-toggle target="nav_collapse"></b-navbar-toggle>

        <b-navbar-brand href="#">
          <router-link to="/">
            <img src="../../../../assets/images/nav-logo.png" alt=""/>
          </router-link>
        </b-navbar-brand>

        <b-collapse is-nav id="nav_collapse">

          <b-navbar-nav>
            <b-nav-item href="#" @click="showLoginModal">
              ورود
            </b-nav-item>
            <b-nav-item href="#" @click="showRegisterModal">
              ثبت نام
            </b-nav-item>
          </b-navbar-nav>

          <!-- Right aligned nav items -->
          <ul class="navbar-nav ml-auto">
            <li class="nav-item profile-item-container" v-if="user">
              <a href="#" class="nav-link"> <i class="fas fa-user"></i> &nbsp; {{user.fullName}}</a>
              <div class="profile-items">
                <ul>
                  <li>
                    <router-link to="/my-lessons"> دروس من</router-link>
                  </li>
                </ul>
              </div>
            </li>

            <li  class="nav-item  cart-container d-none d-sm-block">
              <a href="#" class="nav-link"> <i class="fas fa-shopping-cart "></i> &nbsp; سبد خرید <i
                class="fas fa-caret-down "></i> </a>
              <div class="cart">
                <cart></cart>
              </div>
            </li>

            <li v-b-modal.modal1 class="navbar-nav d-block d-sm-none">
              <a href="#" class="nav-link"> <i class="fas fa-shopping-cart "></i> سبد خرید</a>
            </li>
          </ul>


        </b-collapse>
      </div>
    </b-navbar>
    <b-modal modal-class="modal-full" id="modal1" hide-footer title="سبد خرید">
      <div class="modal-cart-container">
        <cart></cart>
      </div>
    </b-modal>
  </div>

</template>

<script>
  import Cart from './cart'
  import storage from 'storage-helper'
  import EventBus from '../../../../event-bus'

  export default {
    name: "",
    components: {
      Cart
    },
    data() {
      return {
        user: null
      }
    },
    methods: {
      showLoginModal() {
        this.$root.$emit('bv::show::modal', 'login-dialog')
      },
      showRegisterModal() {
        this.$root.$emit('bv::show::modal', 'register-dialog')
      }
    },
    mounted() {
      var vm = this;
      EventBus.$on('user-comes-in', function (user) {
        vm.user = user;
      });
      this.user = JSON.parse(storage.getItem('user'));
    }
  }
</script>

<style scoped>
  .navbar-toggler {
    background-color: wheat;
  }

</style>
