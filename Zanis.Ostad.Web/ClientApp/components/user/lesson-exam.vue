<template>
  <div class=" col-sm-12">
    <div class="gray-shadow">
      <div class="box-mp">
        <div v-if="!isLoading" class="exam-smaples">
          <h6 class="blue">
            نمونه سوالات {{lesson.lessonName}}
          </h6>
          <h6>کد درس : <span class="blue">{{lesson.lessonCode}}</span></h6>
          <div class="list">
            <ul>
              <li v-for="item in lesson.exams">
                <button  @click.prevent="showFileInViewer(item)">
                  {{item.title}}
                </button>
              </li>
            </ul>
          </div>
        </div>
        <br/>
        <div id="pdf" v-show="file" style="height: 700px"></div>
      </div>
    </div>
  </div>
</template>

<script>
  export default {
    props: {
      lesson: {
        type: Object,
        required: true
      }
    },
    name: "",
    components: {},

    data() {
      return {
        file: null,
        isLoading: false,
      }
    },
    methods: {
      async showFileInViewer(file) {
        this.file=file;
        let blobAsUrl = await this.$http.get(file.filePath, {
          responseType: 'blob',
          transformResponse: function (data) {
            // don't forget to inject $window
            return (window.URL || window.webkitURL).createObjectURL(data);
          }
        });
        PDFObject.embed(blobAsUrl.data, "#pdf")
      },
    }
  }
</script>

<style scoped>

</style>
