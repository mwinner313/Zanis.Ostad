<template>
  <b-modal body-class="login" modal-class="no-padding" id="register-as-teacher-dialog" hide-footer hide-header size="lg">
    <div class="container-fluid">
      <div class="row">
        <div class="col-sm-12 col-md-6 login-form">
          <span @click="$root.$emit('bv::hide::modal', 'register-as-teacher-dialog')" class="modal-close"><i class="fas fa-times"></i></span>
          <p class="hint">به بزرگی و کوچکی حروف توجه فرمایید .</p>
          <h4>ثبت نام به عنوان استاد</h4>
          <b-form>
            <b-form-group>
              <b-form-input
                type="text"
                v-model="form.fullName"
                :state="!$v.form.fullName.$invalid || !doesFormSubmitted"
                placeholder="نام و نام خانوادگی"/>
              <b-form-invalid-feedback>
                <p v-show="!$v.form.fullName.required"> نام و نام خانوادگی یک فیلد اجباریست</p>
                <p v-show="!$v.form.fullName.minLength"> نام و نام خانوادگی حد اقل میباست 4 حرف باشد</p>
              </b-form-invalid-feedback>
            </b-form-group>
            <!--std no-->

            <b-form-group>
              <b-form-input
                type="text"
                v-model="form.teacherOrStudentNo"
                :state="!$v.form.teacherOrStudentNo.$invalid || !doesFormSubmitted"
                placeholder="شماره دانشجویی یا کد پرسنلی "/>
              <b-form-invalid-feedback>
                شماره دانشجویی یک فیلد اجباریست
              </b-form-invalid-feedback>
            </b-form-group>

            <!--password-->
            <b-form-group>
              <b-form-input
                type="password"
                v-model="form.password"
                :state="!$v.form.password.$invalid || !doesFormSubmitted"
                placeholder="رمز عبور"/>
              <b-form-invalid-feedback>
                <p v-show="!$v.form.password.required"> رمز عبور یک فیلد اجباریست</p>
                <p v-show="!$v.form.password.minLength"> رمز عبور حد اقل میباست 6 حرف باشد</p>
              </b-form-invalid-feedback>
            </b-form-group>
            <!--password confirm-->
            <b-form-group>
              <b-form-input
                type="password"
                v-model="form.confirmPassword"
                :state="!$v.form.confirmPassword.$invalid || !doesFormSubmitted"
                placeholder="تکرار رمز عبور"/>
              <b-form-invalid-feedback>
                <p v-show="!$v.form.confirmPassword.required"> تکرار رمز عبور یک فیلد اجباریست</p>
                <p v-show="!$v.form.confirmPassword.sameAs"> تکرار رمز عبور صحیح نمی باشد</p>
              </b-form-invalid-feedback>
            </b-form-group>
            <!--email-->
            <b-form-group>
              <b-form-input
                type="email"
                v-model="form.emailAddress"
                :state="!$v.form.emailAddress.$invalid || !doesFormSubmitted"
                placeholder="ایمیل"/>
              <b-form-invalid-feedback>
                <p v-show="!$v.form.emailAddress.required"> ایمیل یک فیلد اجباری میباشد</p>
                <p v-show="!$v.form.emailAddress.email">لطفا یک ایمیل صیحیح وارد کنید</p>
              </b-form-invalid-feedback>
            </b-form-group>
            <button @click.prevent="onSubmit" type="submit">ثبت</button>
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
  import {required, minLength,sameAs,email} from "vuelidate/lib/validators"
  import storage from 'storage-helper'
  import axios from 'axios'
  import EventBus from '../../../../event-bus'
  export default {
    name: "",
    data() {
      return {
        form: {},
        doesFormSubmitted: false,
        error: false
      }
    },
    methods: {
      async onSubmit() {
        this.doesFormSubmitted = true;
        if (this.$v.form.$invalid)
          return;
        let res = await this.$http.post('/api/account/RegisterAsTeacher', this.form);
        if ( res.data.status == 2 ) {
          this.$toaster.error(res.data.message,{timeout:2000});
          this.error = true;
          return;
        }
        this.error=false;
        storage.setItem('Authorization' , res.data.bearerToken);
        storage.setItem('user',JSON.stringify(res.data.user));
        EventBus.$emit('user-comes-in',res.data.user);
        axios.defaults.headers.common['Authorization'] = res.data.bearerToken;
        this.$root.$emit('bv::hide::modal', 'register-as-teacher-dialog');
        this.$toaster.success('خوش آمدید!');
      }
    },
    mounted() {
    },
    mixins: [
      validationMixin
    ],
    validations: {
      form: {
        teacherOrStudentNo: {
          required,
          minLength: minLength(3)
        },
        password: {
          required,
          minLength: minLength(6)
        },
        fullName:{
          required,
          minLength: minLength(4)
        },
        confirmPassword:{
          required,
          sameAs:sameAs('password')
        },
        emailAddress:{
          required,
          email
        }
      }
    },
  }
</script>

<style >

</style>
