import Login from '../pages/Login.vue'
import Table from '../pages/nav1/Table.vue'
import Home from '../pages/Home.vue'
import NotFound from '../pages/404.vue'
import Form from '../pages/nav1/Form.vue'
import Page4 from '../pages/nav2/Page4.vue'
import Page5 from '../pages/nav2/Page5.vue'
import Page6 from '../pages/nav3/Page6.vue'

const routes = [
  {
    path: '/login',
    name: 'login',
    component: Login,
    hidden: true
  },
  {
    path: '/404',
    name: '',
    component: NotFound,
    hidden: true
  }, {
    path: '/',
    component: Home,
    name: '导航一',
    iconCls: 'fa fa-envelope-o',
    children: [
      { path: '/table', component: Table, name: 'Table' },
      { path: '/form', component: Form, name: 'Form' }
    ]
  }, {
    path: '/',
    component: Home,
    name: '导航二',
    iconCls: 'fa fa-id-card-o',
    children: [
      { path: '/page4', component: Page4, name: '页面4' },
      { path: '/page5', component: Page5, name: '页面5' }
    ]
  }, {
    path: '/',
    component: Home,
    name: '导航三',
    iconCls: 'fa fa-address-card',
    leaf: true,
    children: [
      { path: '/page6', component: Page6, name: '页面6' }
    ]
  }, {
    path: '*',
    hidden: true,
    redirect: { path: '/404' }
  }
]

export default routes
