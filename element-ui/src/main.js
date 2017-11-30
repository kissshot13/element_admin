// The Vue build version to load with the `import` command
// (runtime-only or standalone) has been set in webpack.base.conf with an alias.
import Vue from 'vue'
import ElementUI from 'element-ui'
import App from './App'
import routes from './router'
import VueRouter from 'vue-router'
import 'element-ui/lib/theme-chalk/index.css'
import 'font-awesome/css/font-awesome.min.css'

Vue.config.productionTip = false

/* eslint-disable no-new */
Vue.use(ElementUI)
Vue.use(VueRouter)

const router = new VueRouter({
  routes
})

// router.beforeEach((to, from, next) => {
//   if (to.path === '/login') {
//     sessionStorage.removeItem('user')
//   }
//   let user = JSON.parse(sessionStorage.getItem('user'))
//   if (!user && to.path !== '/login') {
//     next({ path: '/login' })
//   } else {
//     next()
//   }
// })

new Vue({
  // el: '#app',
  router,
  render: h => h(App)
}).$mount('#app')
