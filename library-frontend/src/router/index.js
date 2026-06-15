import { createRouter, createWebHistory } from 'vue-router'
import LandingPage from '../views/LandingPage.vue'
import LoginView from '../views/LoginView.vue'
import AdminDashboard from '../views/AdminDashboard.vue'

const router = createRouter({
  history: createWebHistory(import.meta.env.BASE_URL),
  routes: [
    {
      path: '/',
      name: 'home',
      component: LandingPage // Trang chủ (như ảnh thiết kế)
    },
    {
      path: '/login',
      name: 'login',
      component: LoginView // Trang đăng nhập
    },
    {
      path: '/dashboard',
      name: 'dashboard',
      component: AdminDashboard // Trang thống kê (vừa làm xong)
    }
  ]
})

export default router