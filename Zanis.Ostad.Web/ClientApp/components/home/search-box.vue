<template>
  <section id="search" class="row ">
    <div class="col-sm-3 p2 d-none d-sm-block">
      <div class="purple-wrapper " id="search-title">
        <p class=" no-margin">یکی از موارد زیر را انتخاب کنید</p>
      </div>
    </div>
    <div class="col-sm-9 p2 d-none d-sm-block">
      <div  v-click-outside="close" class="purple-wrapper">
        <img
          src="../../assets/images/search.png"
          class="search-btn float-right"
          alt=""
        />
        <auto-compelete input-name="search"
                        list-class="list-class"
                        close-class="float-right closebtn"
                        input-class="purple-wrapper"
                        @on-change="search"
                        :min-length="3"
                        :items="items"
                        :is-loading="isLoading"
                        @on-close="close"
                        @on-select="goTo"
                        placeholder=" نام واحددرسی ، رشته ...">
          <template slot="item" slot-scope="{item}" >

              <p>  {{item.type==1?'درس ':'  رشته '}}{{item.title}}</p>
              <small class="red">
                {{item.grade}}
              </small>
              <small class="green">
                <template v-if="item.type===1" v-for="field in item.fields">
                  ,&nbsp; {{field}} &nbsp;&nbsp;
                </template>
              </small>
          </template>
        </auto-compelete>
      </div>
    </div>
  </section>
</template>

<script>
  import ClickOutside from 'vue-click-outside'
  import AutoCompelete from '../autocompelete'

  export default {
    name: "",
    components: {
      AutoCompelete
    },
    data() {
      return {
        items: [],
        isLoading:false
      }
    },
    methods: {
      goTo(item) {
       if(item.type===1)
         this.$router.push({name:'lesson',params:{lessonId:item.id}})
        else
          alert('you need to impelement')
      },
      close() {
        this.items = [];
        this.isLoading=false;
      },
      async search(term) {
        this.isLoading=true;
        this.items = (await this.$http.get('/api/General/Search', {
          params: {term}
        })).data;
        this.isLoading=false;
      }
    } ,directives: {
      ClickOutside
    }
  }
</script>

<style>
  .list-class {
    top: 10px;
  }



  .closebtn {
    padding-top: 16px;
    padding-left: 16px;
    font-size: 30px;
    color: white;
    cursor: pointer;
  }
  .no-margin {
    margin: 0px !important;
  }

</style>
