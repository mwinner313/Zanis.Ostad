<template>
  <b-modal body-class="login" modal-class="no-padding" id="reset-pass-dialog" hide-footer hide-header size="sm">
    <div class="container-fluid">
      <div class="row">
        <div class="col-sm-12 login-form">
          <p class="hint mt-2">بازیابی رمز عبور</p>
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
            <button @click.prevent="onSubmit" :disabled="isSendingData" class="submit mb-2" type="submit">ثبت</button>
          </b-form>

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
           this.successToSendResetPasswordToken = true;
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
