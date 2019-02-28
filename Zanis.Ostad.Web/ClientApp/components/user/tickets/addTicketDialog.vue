<template>
  <el-dialog
    @closed="$emit('close')"
    width="550px"
    :visible.sync="isOpen"
    append-to-body>
    <div slot="title">
      تیکت جدید
    </div>
    <el-form ref="form" :model="form">
      <el-form-item prop="categoryId" label="گروه" :rules="[
      { required: true, message: 'گروه درخواست الزامیست'}
    ]">
      <el-select v-model="form.categoryId" placeholder="گروه">
        <el-option
          v-for="item in ticketCategories"
          :key="item.id"
          :label="item.title"
          :value="item.id">
        </el-option>
      </el-select>
      </el-form-item>
      <el-form-item label="عنوان درخواست"
                    prop="title"
                    :rules="[{ required: true, message: 'عنوان درخواست الزامیست'}]">
        <el-input v-model="form.title"></el-input>
      </el-form-item>

      <el-form-item label="توضیحات"
                    prop="description"
                    :rules="[
      { required: true, message: 'توضیحات درخواست الزامیست'}
    ]">
        <el-input type="textarea" rows="3" v-model="form.description"></el-input>
      </el-form-item>
      <el-form-item>
        <el-button type="primary" @click="onSubmit">ارسال</el-button>
      </el-form-item>
    </el-form>
  </el-dialog>
</template>
<script>
  import axios from 'axios';

  export default {
    name: 'app',
    props: {
      isOpen: {
        type: Boolean
      },
    },
    data() {
      return {
        ticketCategories:[],
        form: {
          title: '',
          description: '',
          categoryId:undefined
        }
      };
    },
    methods: {
      onSubmit() {
        this.$refs.form.validate((valid) => {
          if (valid) {
            axios.post('/api/account/tickets', this.form).then(res => {
              this.$emit('close');
            });
          }
        });
      }
    },
    mounted() {
      axios.get('/api/TicketCategory',{params:{type:0}}).then(relatedToSupports=>{
        axios.get('/api/TicketCategory',{params:{type:2}}).then(relatedToRequests=>{
          this.ticketCategories=[...relatedToRequests.data,...relatedToSupports.data];
        })
      })
    },
  }
</script>

<style scoped>

</style>
