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
            <b-nav-item href="#" @click="showLoginRegisterModal">
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
                  <span class="drop-down__name" @click="userMenuToggled=!userMenuToggled">
                    <i class="far fa-user"></i>
                    {{user.fullName}}
                    &nbsp;
                    <i class="fas fa-caret-down"></i>
                    </span>
                </div>
                <div class="drop-down__menu-box" v-show="userMenuToggled">
                  <ul class="drop-down__menu">
                    <li data-name="profile" class="drop-down__item">
                      <router-link to="/user/dashboard"><i class="fas fa-user"></i> &nbsp;حساب کاربری</router-link>
                    </li>
                    <li @click="signOut" data-name="profile" class="drop-down__item">خروج</li>
                  </ul>
                </div>
              </div>
            </li>
            &nbsp;
            <li>
              <router-link to="/start-teaching">
                <button class="start-teaching">شروع تدریس</button>
              </router-link>
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
        userMenuToggled: false
      }
    },
    methods: {
      showLoginRegisterModal() {
        this.$root.$emit('bv::show::modal', 'login-dialog')
      },
      signOut() {
        this.user = undefined;
        storage.removeItem('Authorization');
        storage.removeItem('user');
        this.userMenuToggled=false;
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

<style scoped lang="scss">
  @import '../../../../assets/_variables.scss';

  .nav-link {
    margin: 0 7px;
    padding: 0 !important;;
    color: white !important;
  }

  .nav-item {
    a {
      padding: 0 !important;
    }
    &.active {
      a {
        color: white !important;
      }
    }
  }

  .nav-tabs {
    .nav-link {
      color: gray !important;
      &.active {
        color: black !important;
      }
    }
  }

  #main-nav {
    background-color: #535252;
    .login-btn {
      cursor: pointer;
      width: 100px;
      color: white;
      transition: all 0.3s;
      background-color: #717171;
      border: 1px solid #afafaf;
      &:hover {
        border: 1px solid white;
      }
    }
    .cart-btn {
      line-height: 38px;
      margin: 5px;
      padding: 4px 15px 4px 3px !important;
      cursor: pointer;
      color: #efe778;
      background-color: #717171;
      border: 1px solid #efe778;
      border-radius: 3px;
    }
    .signup-btn {
      cursor: pointer;
      transition: all 0.3s;
      color: #717171;
      background-image: linear-gradient(to right, #efe778, #edee90, #ecf4a7, #edfabd, #f1ffd3);
      &:hover {
        box-shadow: 1px 0 5px #c5c59f;
        background-image: linear-gradient(
            to right,
            #e6f63e,
            #ecf843,
            #f3fa49,
            #f9fd4e,
            #ffff53
        );
      }
    }
    .start-teaching {
      cursor: pointer;
      color: #efe778;
      background-color: #717171;
      border: 1px solid #efe778;
    }

    .drop-down {
      display: inline-block;
      position: relative;
    }

    .drop-down__button {
      width: 145px;
      display: inline-block;
      line-height: 40px;
      padding: 0 18px;
      border-radius: 4px;
      cursor: pointer;
    }

    .drop-down__name {
      white-space: nowrap;
      color: #fff;
    }

    .drop-down__icon {
      width: 18px;
      vertical-align: middle;
      margin-left: 14px;
      height: 18px;
      border-radius: 50%;
      transition: all 0.4s;
      -webkit-transition: all 0.4s;
      -moz-transition: all 0.4s;
      -ms-transition: all 0.4s;
      -o-transition: all 0.4s;

    }
    .drop-down__menu-box {
      position: absolute;
      width: 100%;
      left: 0;
      background-color: #fff;
      border-radius: 4px;
      box-shadow: 0 3px 6px 0 rgba(0, 0, 0, 0.2);
      transition: all 0.3s;
      margin-top: 5px;
      a {
        color: #444444;
      }
    }

    .drop-down__menu {
      margin: 0;
      padding: 0 13px;
      list-style: none;
    }
    .drop-down__menu-box:before {
      content: '';
      background-color: transparent;
      border-right: 8px solid transparent;
      position: absolute;
      border-left: 8px solid transparent;
      border-bottom: 8px solid #fff;
      border-top: 8px solid transparent;
      top: -15px;
      right: 18px;

    }

    .drop-down__menu-box:after {
      content: '';
      background-color: transparent;
    }

    .drop-down__item {
      font-size: 13px;
      padding: 13px 0;
      font-weight: 500;
      color: #909dc2;
      cursor: pointer;
      position: relative;
      border-bottom: 1px solid #e0e2e9;
    }

    .drop-down__item-icon {
      width: 15px;
      height: 15px;
      position: absolute;
      right: 0px;
      fill: #8995b6;

    }

    .drop-down__item:hover .drop-down__item-icon {
      fill: $yellow;
    }

    .drop-down__item:hover {
      color: $yellow;
    }

    .drop-down__item:last-of-type {
      border-bottom: 0;
    }

    .drop-down--active .drop-down__menu-box {
      visibility: visible;
      opacity: 1;
      margin-top: 15px;
    }

    .drop-down__item:before {
      content: '';
      position: absolute;
      width: 3px;
      height: 28px;
      background-color: $yellow;
      left: -13px;
      top: 50%;
      transform: translateY(-50%);
      display: none;
    }

    .drop-down__item:hover:before {
      display: block;
    }
  }
</style>
