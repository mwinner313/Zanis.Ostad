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
            <!--<b-nav-item href="#" @click="showLoginModal">-->
              <!--<button class="login-btn">ورود</button>-->
            <!--</b-nav-item>-->
            <b-nav-item href="#" @click="showRegisterModal">
              <button class="signup-btn"> ورود / ثبت نام</button>
            </b-nav-item>
            <li class="nav-item  cart-container d-none d-sm-block">
              <a class="cart-btn"> <i class="fas fa-shopping-cart"></i> &nbsp;</a>
              <div class="cart">
                <cart></cart>
              </div>
            </li>

            <li v-b-modal.modal1 class="navbar-nav d-block d-sm-none">
              <a href="#" class="nav-link"> <i class="fas fa-shopping-cart "></i> سبد خرید</a>
            </li>
          </b-navbar-nav>

          <ul class="navbar-nav ml-auto">
            <li v-if="user" class="navbar-nav">
              <div class="drop-down">
                <div id="dropDown" class="drop-down__button">
                  <span class="drop-down__name"  @click="userMenuToggled=!userMenuToggled">
                    {{user.fullName}}
                    </span>
                </div>
                <div class="drop-down__menu-box" v-show="userMenuToggled">
                  <ul class="drop-down__menu" >
                    <li data-name="profile" class="drop-down__item"> <router-link to="/user" ><i class="fas fa-user"></i> &nbsp;حساب کاربری </router-link> </li>
                    <li @click="signOut" data-name="profile" class="drop-down__item">خروج</li>
                  </ul>
                </div>
              </div>
            </li>
            &nbsp;
            <li>
              <a href="/start-teaching">
                  <button class="start-teaching">شروع تدریس</button>
              </a>
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
  import EventBus from '../../../../event-bus'
  import storage from 'storage-helper'

  export default {
    name: "",
    components: {
      Cart
    },
    data() {
      return {
        user: null,
        userMenuToggled:false
      }
    },
    methods: {
      showLoginModal() {
        this.$root.$emit('bv::show::modal', 'login-dialog')
      },
      showRegisterModal() {
        this.$root.$emit('bv::show::modal', 'register-dialog')
      },
      signOut(){
        this.user = undefined;
        storage.setItem('Authorization',undefined);
        storage.setItem('user',undefined);
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
</style>
