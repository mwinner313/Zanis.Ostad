<template>
  <section id="search-mobile" class="row d-block d-sm-none">
    <div class="col-sm-12 ">
      <div v-click-outside="close">
        <auto-compelete input-name="search"
                        list-class="list-class"
                        close-class="d-none"
                        input-class="textbox"
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
  .textbox {
    box-shadow: 0 1px 3px rgba(0, 0, 0, 0.12), 0 1px 2px rgba(0, 0, 0, 0.24);
    border-radius: 4px;
    padding: 17px 25px !important;
    background-color: rgba(255, 255, 255, 0.8) !important;
    color: #212121;
    width:100%;
  }
</style>
