import Vue from 'vue'

import Cookies from 'js-cookie'

import 'normalize.css/normalize.css' // a modern alternative to CSS resets

import Element from 'element-ui'
import './styles/element-variables.scss'

import '@/styles/index.scss' // global css

import App from './App'
import store from './store'
import router from './router'

import './icons' // icon
import './permission' // permission control
import './utils/error-log' // error log
// import i18n from './lang' // internationalization
import global from '@/utils/global'
import * as filters from './utils/filter' // global filters
// import Chat from 'jwchat'
// import GoEasy from 'goeasy'
/**
 * If you don't want to use mock-server
 * you want to use MockJs for mock api
 * you can execute: mockXHR()
 *
 * Currently MockJs will be used in the production environment,
 * please remove it before going online! ! !
 */
import TablePage from '@/components/TablePage'
import Translate from '@/components/Translate'
import JsonViewer from 'vue-json-viewer'
import FormInfo from '@/components/FormInfo'
import Select from '@/components/Select'
import DateGroup from '@/components/DateGroup'

Vue.component('table-page', TablePage)
Vue.component('translate', Translate)
Vue.component('json-viewer', JsonViewer)
Vue.component('form-info', FormInfo)
Vue.component('select-data', Select)
Vue.component('date-group', DateGroup)
Vue.use(global)
// Vue.use(Chat)

// Vue.use(Element, {
//   size: Cookies.get('size') || 'medium', // set element-ui default size
//   i18n: (key, value) => i18n.t(key, value)
// })
// Vue.prototype.goeasy = GoEasy.getInstance({
//   host: 'hangzhou.goeasy.io',
//   appkey: 'BS-2a0c7e391a984445a095356a1e028c81'
// })
Vue.use(Element, {
  size: Cookies.get('size') || 'medium' // set element-ui default size
})

// register global utility filters
Object.keys(filters).forEach(key => {
  Vue.filter(key, filters[key])
})

Vue.config.productionTip = false

new Vue({
  el: '#app',
  router,
  store,
  render: h => h(App)
})