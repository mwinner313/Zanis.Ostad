<template>
     <div>
       <div class="container">
         <div class="row" v-if="cartItems.length">
           <div v-for="item in cartItems" class="col-sm-12 cart-item">
             <span @click="remove(item)" class="delete-item"><i class="fas fa-trash"></i></span>
             <span>{{item.productTitle}}</span>
             <span class="text-danger float-right">{{item.price}}</span>
           </div>
           <div class="col-sm-12 cart-item">
             <span>مجموع خرید :</span>
             <span class="text-success float-right">{{sumBy(cartItems,x=>x.price)}} تومان</span>
           </div>
         </div>
         <div v-if="!cartItems.length">
           <span class="text-center"> سبد خرید شما خالیست </span>
         </div>
       </div>
       <button v-show="cartItems.length" @click="order" class="btn btn-block btn-primary">نهایی کردن خرید</button>
       <form method="post"  :action="orderRes.paymentRedirectUrl" ref="bankform">
         <input type="hidden"   name="MID" :value="orderRes.mid">
         <input type="hidden"  name="Amount" :value="orderRes.amount">
         <input type="hidden"  name="ResNum" :value="orderRes.resNumOrOrderId">
         <input type="hidden"  name="RedirectURL" :value="orderRes.returnRedirectUrl">
       </form>
     </div>

</template>

<script>
  import storage from 'storage-helper'
  import _ from 'lodash';
  import EventBus from '../../../../event-bus'

  export default {
    name: "",
    data() {
      return {
        cartItems: [],
        orderRes: {}
      }
    },
    methods: {
      async loadCartItems() {
        let authorization=storage.getItem('Authorization');
        if ([undefined,'undefined',null].indexOf(authorization)  ==  -1)
          this.cartItems = (await this.$http.get('/api/cart')).data;
      },
      remove(item) {
        this.$swal({
          title: 'از حذف این مورد اطمینان دارید ؟',
          text: item.productTitle,
          type: 'warning',
          showCancelButton: true,
          confirmButtonColor: '#3085d6',
          cancelButtonColor: '#d33',
          confirmButtonText: 'بلی',
          cancelButtonText: 'خیر'
        }).then(async (result) => {
          if (result.value) {
            let res = await this.$http.delete('/api/cart/' + item.id);
            if (res.data.status === 1) {
              this.$toaster.success('انجام شد');
              this.cartItems = this.cartItems.filter(x => x.id !== item.id);
            }
          }
        })
      },
      sumBy: _.sumBy,
      async order() {
        let res = await this.$http.post('/api/Order/Create');
        this.orderRes = res.data;
         setTimeout(()=>this.$refs.bankform.submit(),10);
      }
    },
    mounted() {
      var vm = this;
      EventBus.$on('cart-item-added', function () {
        vm.loadCartItems()
      });
      this.loadCartItems()
    }
  }
</script>

<style scoped lang="scss">
  form {
    display: none;
  }
  
.cart-item {
  border-bottom: 1px solid #e8e8e8;
  padding: 10px 10px;
  .delete-item {
    cursor: pointer;
    padding-left: 7px;
    i {
      font-size: 12px;
    }
  }
}
</style>
