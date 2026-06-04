import { createRouter, createWebHistory } from 'vue-router'
import Welcome from '../views/Welcome.vue'
import Register from '../views/Register.vue'
import Dashboard from '../views/Dashboard.vue'
import Users from '../views/Users.vue'
import Cards from '../views/Cards.vue'
import Reports from '../views/Reports.vue'
import Settings from '../views/Settings.vue'
import Login from '../views/Login.vue'

const routes = [
  { path: '/', redirect: '/welcome' },
  { path: '/welcome', name: 'welcome', component: Welcome, meta: { public: true } },
  { path: '/register', name: 'register', component: Register, meta: { public: true } },
  { path: '/login', name: 'login', component: Login, meta: { public: true } },
  { path: '/dashboard', name: 'dashboard', component: Dashboard, meta: { auth: true, title: 'Tổng quan' } },
  { path: '/users', name: 'users', component: Users, meta: { auth: true, title: 'Người dùng' } },
  { path: '/cards', name: 'cards', component: Cards, meta: { auth: true, title: 'Thẻ thư viện' } },
  { path: '/reports', name: 'reports', component: Reports, meta: { auth: true, title: 'Báo cáo' } },
  { path: '/settings', name: 'settings', component: Settings, meta: { auth: true, title: 'Cài đặt' } }
]

const router = createRouter({
  history: createWebHistory(),
  routes,
  scrollBehavior: () => ({ top: 0 })
})

router.beforeEach((to) => {
  if (to.meta.auth && !localStorage.getItem('token')) return '/login'
  if (['login', 'register'].includes(to.name) && localStorage.getItem('token')) return '/dashboard'
})

export default router
