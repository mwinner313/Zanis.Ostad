<template>
  <b-modal modal-class="no-padding" id="login-dialog" hide-footer hide-header size="lg">
    <div class="container-fluid p-0">
      <div class="row no-gutters" style="box-shadow: 0px 3px 0px 4px #777777;">
        <div class="col-md-6">
          <RegisterDialog></RegisterDialog>
        </div>
        <div class="col-md-6">
          <div class="login-user-to-site-wrapper">
            <div class="login-banner d-flex justify-content-center align-items-center">
              <p class>ورود به حساب کاربری</p>
            </div>
            <b-form id="login-form">
              <span
                class="text-danger"
                v-if="errorUserNameOrPassword"
              >رمز عبور یا نام کاربری اشتباه است</span>
              <br>
              <b-form-group>
                <b-form-input
                  type="text"
                  v-model="form.userName"
                  :state="!$v.form.userName.$invalid || !doesFormSubmitted"
                  aria-describedby="input1LiveFeedback"
                  placeholder="نام کاربری (شماره دانشجویی یا کد پرسنلی)"
                />
                <b-form-invalid-feedback>نام کاربری یک فیلد اجباریست</b-form-invalid-feedback>
              </b-form-group>
              <b-form-group>
                <b-form-input
                  type="password"
                  v-model="form.password"
                  :state="!$v.form.password.$invalid || !doesFormSubmitted"
                  aria-describedby="input1LiveFeedback"
                  placeholder="رمز عبور"
                />
                <b-form-invalid-feedback>
                  <p v-show="!$v.form.password.required">رمز عبور یک فیلد اجباریست</p>
                  <p v-show="!$v.form.password.minLength">رمز عبور حد اقل میباست 6 حرف باشد</p>
                </b-form-invalid-feedback>
              </b-form-group>
              <button class="submit" @click.prevent="onSubmit" type="submit">ورود</button>
              <p class="text-center m-3">
                رمز خود را فراموش کرده اید؟
                <span @click="showResetPassDialog" id="reset-pass-btn">کلیک کنید</span>
              </p>
            </b-form>
          </div>
        </div>
      </div>
    </div>
  </b-modal>
</template>

<script>
import { validationMixin } from "vuelidate";
import { required, minLength } from "vuelidate/lib/validators";
import storage from "storage-helper";
import axios from "axios";
import EventBus from "../../../../event-bus";
import RegisterDialog from './register-dialog';
export default {
  name: "login-dialog",
  components : {
    RegisterDialog
  },
  data() {
    return {
      form: {},
      doesFormSubmitted: false,
      error: false,
      errorUserNameOrPassword: false
    };
  },
  methods: {
    showResetPassDialog() {
      this.$root.$emit("bv::hide::modal", "login-dialog");
      this.$root.$emit("bv::show::modal", "reset-pass-dialog");
    },
    showRegisterDialog() {
      this.$root.$emit("bv::hide::modal", "login-dialog");
      this.$root.$emit("bv::show::modal", "register-dialog");
    },
    async onSubmit() {
      this.doesFormSubmitted = true;
      if (this.$v.form.$invalid) return;
      let res = await this.$http.post("/api/account/login", this.form);
      if (res.data.status == 2) {
        this.errorUserNameOrPassword = true;
        return;
      }
      this.errorUserNameOrPassword = false;
      storage.setItem("Authorization", res.data.bearerToken);
      storage.setItem("user", JSON.stringify(res.data.user));
      EventBus.$emit("user-comes-in", res.data.user);
      axios.defaults.headers.common["Authorization"] = res.data.bearerToken;
      this.$root.$emit("bv::hide::modal", "login-dialog");
      this.$toaster.success("خوش آمدید!");
    }
  },
  mounted() {},
  mixins: [validationMixin],
  validations: {
    form: {
      userName: {
        required,
        minLength: minLength(3)
      },
      password: {
        required,
        minLength: minLength(6)
      }
    }
  }
};
</script>

<style lang="scss">
.field-register-user-wrapper {
  background-color: #d3d3d3 !important;
  padding: 20px;
}

#login-dialog,  #reset-pass-dialog, #register-as-teacher-dialog {
  input::placeholder{
    font-size: 14px;

  }
  input{
    border-color: #d2d2d2;
  }
 .modal-body{
   padding:0px;
 }
  .modal-content {
    border-radius: 0px;
  }
  .form-control{
    height:60px;
  }
  .submit{
    width: 100%;
    height: 60px;
    background-image: linear-gradient(to right, #efe778, #edee90, #ecf4a7, #edfabd, #f1ffd3);
  }
  .login-banner{
    position: relative;
    p{
      margin:0 !important;
    }
    margin-top:40px;
    height:90px !important;
    text-align:center;
    width: 80%;
    margin-right: 20%;
    background-color: #d3d3d3;
    &:before{
      position: absolute;
      content:' ';
        right: 0;
        bottom: 0;
        width: 0;
        height: 0;
        border-style: solid;
        border-width: 27px 27px 0 0;
        border-color: #efe778 transparent transparent transparent;
    }
    &:after{
      position: absolute;
      content:' ';
        right: 0;
        bottom: 0;
        width: 0;
        height: 0;
        border-style: solid;
        border-width: 0 0 27px 27px;
        border-color: transparent transparent #ffffff transparent; }
  }
  #login-form {
      margin:10%;
      #reset-pass-btn{
        color:blue;
        cursor: pointer;
      }
  }

}

</style>
