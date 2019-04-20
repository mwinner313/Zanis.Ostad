<template>
  <div class="field-register-user-wrapper">
    <p class="mt-4">حساب کاربری خود را ایجاد کنید</p>
    <b-form>
      <b-form-group>
        <b-form-input
          type="text"
          v-model="form.fullName"
          :state="!$v.form.fullName.$invalid || !doesFormSubmitted"
          placeholder="نام و نام خانوادگی"
        />
        <b-form-invalid-feedback>
          <p v-show="!$v.form.fullName.required">نام و نام خانوادگی یک فیلد اجباریست</p>
          <p v-show="!$v.form.fullName.minLength">نام و نام خانوادگی حد اقل میباست 4 حرف باشد</p>
        </b-form-invalid-feedback>
      </b-form-group>
      <!--std no-->

      <b-form-group>
        <b-form-input
          type="text"user-comes-in
          v-model="form.studentNo"
          :state="!$v.form.studentNo.$invalid || !doesFormSubmitted"
          placeholder="شماره دانشجویی "
        />
        <b-form-invalid-feedback>شماره دانشجویی یک فیلد اجباریست</b-form-invalid-feedback>
      </b-form-group>

      <!--password-->
      <b-form-group>
        <b-form-input
          type="password"
          v-model="form.password"
          :state="!$v.form.password.$invalid || !doesFormSubmitted"
          placeholder="رمز عبور"
        />
        <b-form-invalid-feedback>
          <p v-show="!$v.form.password.required">رمز عبور یک فیلد اجباریست</p>
          <p v-show="!$v.form.password.minLength">رمز عبور حد اقل میباست 6 حرف باشد</p>
        </b-form-invalid-feedback>
      </b-form-group>
      <!--password confirm-->
      <b-form-group>
        <b-form-input
          type="password"
          v-model="form.confirmPassword"
          :state="!$v.form.confirmPassword.$invalid || !doesFormSubmitted"
          placeholder="تکرار رمز عبور"
        />
        <b-form-invalid-feedback>
          <p v-show="!$v.form.confirmPassword.required">تکرار رمز عبور یک فیلد اجباریست</p>
          <p v-show="!$v.form.confirmPassword.sameAs">تکرار رمز عبور صحیح نمی باشد</p>
        </b-form-invalid-feedback>
      </b-form-group>
      <!--email-->
      <b-form-group>
        <b-form-input
          type="email"
          v-model="form.emailAddress"
          :state="!$v.form.emailAddress.$invalid || !doesFormSubmitted"
          placeholder="ایمیل"
        />
        <b-form-invalid-feedback>
          <p v-show="!$v.form.emailAddress.required">ایمیل یک فیلد اجباری میباشد</p>
          <p v-show="!$v.form.emailAddress.email">لطفا یک ایمیل صیحیح وارد کنید</p>
        </b-form-invalid-feedback>
      </b-form-group>
      <button @click.prevent="onSubmit" class="submit" type="submit">ثبت نام</button>
    </b-form>
  </div>
</template>

<script>
import { validationMixin } from "vuelidate";
import { required, minLength, sameAs, email } from "vuelidate/lib/validators";
import storage from "storage-helper";
import axios from "axios";
import EventBus from "../../../../event-bus";
export default {
  name: "",
  data() {
    return {
      form: {},
      doesFormSubmitted: false,
      error: false
    };
  },
  methods: {
    async showTeacherRegisterDialog() {
      this.$root.$emit("bv::hide::modal", "register-dialog");
      this.$root.$emit("bv::show::modal", "register-as-teacher-dialog");
    },
    async onSubmit() {
      this.doesFormSubmitted = true;
      if (this.$v.form.$invalid) return;
      let res = await this.$http.post("/api/account/register", this.form);
      if (res.data.status == 2) {
        this.$toaster.error(res.data.message);
        this.error = true;
        return;
      }
      this.error = false;
      storage.setItem("Authorization", res.data.bearerToken);
      storage.setItem("user", JSON.stringify(res.data.user));
      EventBus.$emit("user-comes-in", res.data.user);
      axios.defaults.headers.common["Authorization"] = res.data.bearerToken;
      this.$root.$emit("bv::hide::modal", "register-dialog");
      this.$toaster.success("خوش آمدید!");
    }
  },
  mounted() {},
  mixins: [validationMixin],
  validations: {
    form: {
      studentNo: {
        required,
        minLength: minLength(3)
      },
      password: {
        required,
        minLength: minLength(6)
      },
      fullName: {
        required,
        minLength: minLength(4)
      },
      confirmPassword: {
        required,
        sameAs: sameAs("password")
      },
      emailAddress: {
        required,
        email
      }
    }
  }
};
</script>

<style >
</style>
