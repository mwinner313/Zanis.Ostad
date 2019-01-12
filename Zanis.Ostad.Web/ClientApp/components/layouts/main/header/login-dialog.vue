<template>
  <b-modal body-class="login" modal-class="no-padding" id="login-dialog" hide-footer hide-header size="lg">
    <div class="container-fluid">
      <div class="row">
        <div class="col-sm-12 col-md-6 login-form">
          <span @click="$root.$emit('bv::hide::modal', 'login-dialog')" class="modal-close"><i class="fas fa-times"></i></span>
          <p class="hint">به بزرگی و کوچکی حروف توجه فرمایید .</p>
          <p class="text-danger" v-show="errorUserNameOrPassword">نام کاربری یا رمز عبور اشتباه است .</p>
          <b-form>
            <!--User Name-->
            <b-form-group>
              <b-form-input
                type="text"
                v-model="form.userName"
                :state="!$v.form.userName.$invalid || !doesFormSubmitted"
                aria-describedby="input1LiveFeedback"
                placeholder="نام کاربری"/>
              <b-form-invalid-feedback>
                نام کاربری یک فیلد اجباریست
              </b-form-invalid-feedback>
            </b-form-group>
            <!--User Name-->

            <!--Password-->
            <b-form-group>
              <b-form-input
                type="text"
                v-model="form.password"
                :state="!$v.form.password.$invalid || !doesFormSubmitted"
                aria-describedby="input1LiveFeedback"
                placeholder="رمز عبور"/>
              <b-form-invalid-feedback>
                <p v-show="!$v.form.password.required"> رمز عبور یک فیلد اجباریست</p>
                <p v-show="!$v.form.password.minLength"> رمز عبور حد اقل میباست 6 حرف باشد</p>
              </b-form-invalid-feedback>
            </b-form-group>
            <!--Password-->
            <button @click.prevent="onSubmit" type="submit">ورود</button>
            <button @click.prevent="showRegisterDialog">ایجاد حساب کاربری</button>
          </b-form>

        </div>
        <div class="col-md-6 d-none d-md-block login-intro">
          <img src="../../../../assets/images/logo-zanis.png" class="login-logo"/>
          <p>
            شرکت رایان پژوهان زانیس اقدام به تولید بسته های آموزشی دروس
            دانشگاهی تحت عنوان " استاد زانیس" نموده است که در نمایندگی
            های فروش کتب دانشگاه پیام نور سراسر کشور و همچنین در فروشگاه
            اینترنتی استاد زانیس عرضه می شوند کلیه محصولات استاد زانیس
            در فروشگاه سایت با هولوگرام وزارت فرهنگ و ارشاد اسلامی به
            فروش می رسند
          </p>
          <div class="divider"></div>
          <p>
            در صورتی که اطلاعات مربوط به ثبت نام در آزمون خود را فراموش
            کرده اید از این طریق پیگیری کنید >
          </p>
          <span>شماره پشتیبانی &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                   321012-025</span>
        </div>
      </div>
    </div>
  </b-modal>
</template>

<script>
  import {validationMixin} from "vuelidate"
  import {required, minLength} from "vuelidate/lib/validators"
  import storage from 'storage-helper'
  import axios from 'axios'
  import EventBus from '../../../../event-bus'

  export default {
    name: "login-dialog",
    data() {
      return {
        form: {},
        doesFormSubmitted: false,
        error:false,
        errorUserNameOrPassword:false
      }
    },
    methods: {
      showRegisterDialog() {
        this.$root.$emit('bv::hide::modal', 'login-dialog');
        this.$root.$emit('bv::show::modal', 'register-dialog');
      },
      async onSubmit() {
        this.doesFormSubmitted = true;
        if (this.$v.form.$invalid)
          return;
        let res = await this.$http.post('/api/account/login', this.form);
        if(res.data.status==2){
          this.errorUserNameOrPassword=true;
          return;
        }
        this.errorUserNameOrPassword=false;
        storage.setItem('Authorization',res.data.bearerToken);
        storage.setItem('user',JSON.stringify(res.data.user));
        EventBus.$emit('user-comes-in',res.data.user);
        axios.defaults.headers.common['Authorization']=res.data.bearerToken;
        this.$root.$emit('bv::hide::modal', 'login-dialog')
      }
    },
    mounted() {
    },
    mixins: [
      validationMixin
    ],
    validations: {
      form: {
        userName: {
          required,
          minLength: minLength(3)
        },
        password: {
          required,
          minLength: minLength(6)
        },
      }
    },
  }
</script>

<style scoped>

</style>
