import Vue from 'vue';
import persianDate from 'persian-date';

export default function register() {
  Vue.filter('persian-date', function (date) {
    return  new persianDate(new Date(date)).format('YYYY/MM/DD, dddd HH:mm')
  })
}
