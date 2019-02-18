<template>
  <div>
    <small-header title="بازیابی رمز عبور"></small-header>
    <div class="container">
      <div class="row">
        <div class="col-md-6 offset-md-3 ">
          <div class="card-d">
            <b-form name="form">
              <b-form-group>
                <b-form-input
                  type="password"
                  v-model="form.newPassword"
                  :state="!$v.form.newPassword.$invalid || !doesFormSubmitted"
                  placeholder="رمز عبور جدید"/>
                <b-form-invalid-feedback>
                  <p v-show="!$v.form.newPassword.required"> رمز عبور یک فیلد اجباریست</p>
                  <p v-show="!$v.form.newPassword.minLength"> رمز عبور حد اقل میباست 6 حرف باشد</p>
                </b-form-invalid-feedback>
              </b-form-group>
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

              <button type="submit" @click.prevent="submit" class="btn btn-primary submit mb-2">ثبت</button>
            </b-form>
          </div>
        </div>
      </div>
    </div>
  </div>
</template>

<script>
  import SmallHeader from '../layouts/main/header/index-small-image'
  import axios from 'axios';
  import storage from 'storage-helper'
  import {validationMixin} from "vuelidate"
  import {required, minLength, sameAs} from "vuelidate/lib/validators"
  import EventBus from '../../event-bus'

  export default {
    components: {
      SmallHeader
    },
    data() {
      return {
        doesFormSubmitted:false,
        error: false,
        form: {
          token: this.$route.query.token,
          newPassword: '',
          confirmPassword: '',
          userName:storage.getItem('usernameforchangepassword')
        }
      }
    },
    mounted(){
      if(this.$route.query.token===undefined)
        this.$router.push({name:'home'})
    },
    methods: {
      async submit() {
        this.doesFormSubmitted = true;
        if (this.$v.form.$invalid)
          return;
        let res = await axios.post('/api/account/ConfirmChangePassword', this.form);
        if (res.data.status === 2) {
          this.$toaster.warning('لینک باز شده فعلی برای تغییر رمز عبور منسوخ شده', {timeout: 3000});
          this.error = true;
          return;
        }
        this.error = false;
        storage.setItem('Authorization', res.data.bearerToken);
        storage.setItem('user', JSON.stringify(res.data.user));
        EventBus.$emit('user-comes-in', res.data.user);
        axios.defaults.headers.common['Authorization'] = res.data.bearerToken;
        this.$router.push({path:storage.getItem('retUrl')});
        this.$toaster.success('خوش آمدید!');
      }
    },
    mixins: [
      validationMixin
    ],
    validations: {
      form: {
        newPassword: {
          required,
          minLength: minLength(6)
        },
        confirmPassword: {
          required,
          sameAs: sameAs('newPassword')
        }
      }
    }
  }
</script>

<style scoped>
  .submit {
    width: 100%;
  }
</style>
