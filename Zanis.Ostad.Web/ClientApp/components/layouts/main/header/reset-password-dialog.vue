<template>
  <b-modal body-class="login" modal-class="no-padding" id="reset-pass-dialog" hide-footer hide-header size="lg">
    <div class="container-fluid">
      <div class="row">
        <div class="col-sm-12 col-md-6 login-form">
          <span @click="close" class="modal-close"><i class="fas fa-times"></i></span>
          <p class="hint">بازیابی رمز عبور</p>
          <p class="text-danger" v-show="errorUserName">چنین کاربری در موجود نیست.</p>
          <p class="text-success" v-show="successToSendResetPasswordToken">ایمیل بازیابی رمز عبور برای شما ارسال شد</p>
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
            <button @click.prevent="onSubmit" :disabled="isSendingData" type="submit">ثبت</button>
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
  export default {
    name: "login-dialog",
    data() {
      return {
        form: {},
        doesFormSubmitted: false,
        error: false,
        errorUserName: false,
        isSendingData: false,
        successToSendResetPasswordToken: false
      }
    },
    methods: {
      close() {
        this.$root.$emit('bv::hide::modal', 'reset-pass-dialog');

      },
      async onSubmit() {
        this.doesFormSubmitted = true;
        if (this.$v.form.$invalid)
          return;
        this.isSendingData = true;
        let res = await this.$http.post('/api/account/requestchangepassword', this.form);
        this.isSendingData = false;
        if (res.data.status === 2) {
          this.errorUserName = true;
          return;
        }
        this.errorUserName = false;
        this.successToSendResetPasswordToken = true;
        storage.setItem('usernameforchangepassword',this.form.userName);
        storage.setItem('retUrl',window.location.pathname)
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
      }
    },
  }
</script>

<style scoped>

</style>
