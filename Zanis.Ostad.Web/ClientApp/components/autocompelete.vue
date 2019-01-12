<template>
  <div class="vuetocompelete-wrapper">
    <span @click="close" v-show="items.length" :class="closeClass">x</span>
    <input v-model="searchText" :placeholder="placeholder" :class="inputClass" autocomplete="off" :name="inputName"
           type="text" @keyup="search">

    <div class="vuetocompelete-list-item-wrapper">
      <div v-if="isLoading" class="content-loader">
        <content-loader ></content-loader>
      </div>
      <ul v-if="!isLoading" v-show="items.length" :class="listClass">
        <li v-for="item in items" @click="$emit('on-select',item)">
          <slot name="item" :item="item"></slot>
        </li>
      </ul>

    </div>

  </div>
</template>

<script>
  import _ from 'lodash';
  import ContentLoader from './home/content-loader'

  export default {
    components: {
      ContentLoader
    },
    name: "",
    props: {
      items: {
        required: false,
        type: Array,
        default: []
      },
      inputClass: {
        required: false,
        type: String,
        default: ''
      }, listClass: {
        required: false,
        type: String,
        default: ''
      },
      inputName: {
        required: false,
        type: String,
        default: 'search-box'
      },
      placeholder: {
        required: false,
        type: String,
        default: ''
      }, closeClass: {
        required: false,
        type: String,
        default: ''
      },
      debounce: {
        type: Number,
        default: 800
      }, minLength: {
        type: Number,
        default: 3
      }, isLoading: {
        type: Boolean,
        default: false
      }
    },
    data() {
      return {
        searchText: ''
      }
    },
    methods: {
      search() {
        if (this.searchText.length >= this.minLength)
          this.$emit('on-change', this.searchText)
      },
      close() {
        this.searchText = "";
        this.$emit('on-close');
      }
    },
    mounted() {
      this.search = _.debounce(this.search, this.debounce);
    }
  }
</script>

<style scoped>
  .vuetocompelete-wrapper input {
    border: 0 solid white;
    background-color: inherit;
    padding: 5px 10px;
  }

  .vuetocompelete-list-item-wrapper {
    position: relative;
  }
  .content-loader{
    width: 100%;
    border-radius: 5px;
    box-shadow: 1px 0px 7px 3px #d5d5d5;
    z-index: 10;
    background-color: white;
    padding: 0 7px;
    list-style: none;
    position: absolute;
    right: 0;
    top: 10px;
  }

  .vuetocompelete-list-item-wrapper ul{
    height: 700px;
    overflow-y: scroll;
    overflow-x: hidden;
    width: 100%;
    border-radius: 5px;
    box-shadow: 1px 0px 7px 3px #d5d5d5;
    z-index: 10;
    background-color: white;
    padding: 0 7px;
    list-style: none;
    position: absolute;
    right: 0;
  }

  .vuetocompelete-list-item-wrapper ul li {
    cursor: pointer;
    background-color: white;
    color: gray;
    padding: 15px;
    border-bottom: 1px solid #e1e1e1;
  }

  .vuetocompelete-list-item-wrapper ul li:hover {
    color: black;
  }

</style>
