import { createRouter, createWebHistory } from 'vue-router'
import { useAuthStore } from '../stores/auth'

// 1. Import các Layouts
import UserLayout from '../layouts/UserLayout.vue'
import AuthLayout from '../layouts/AuthLayout.vue'
import AdminLayout from '../layouts/AdminLayout.vue'

// 2. Import các Views (Trang nội dung)
import LandingPage from '../views/LandingPage.vue'
import LoginView from '../views/LoginView.vue'
import AdminDashboard from '../views/AdminDashboard.vue'

const router = createRouter({
  history: createWebHistory(import.meta.env.BASE_URL),
  routes: [
    // --- LUỒNG ĐỘC GIẢ (Sử dụng UserLayout) ---
    {
      path: '/',
      component: UserLayout, // Khai báo Layout cha
      children: [
        {
          path: '', // Để trống nghĩa là mặc định khi vào '/'
          name: 'home',
          component: LandingPage // Nhúng LandingPage vào giữa UserLayout
        }
      ]
    },
    
    // --- LUỒNG ĐĂNG NHẬP (Sử dụng AuthLayout) ---
    {
      path: '/auth',
      component: AuthLayout,
      children: [
        {
          path: '/login', // Đường dẫn tuyệt đối
          name: 'login',
          component: LoginView
        }
      ]
    },

    // --- LUỒNG ADMIN (Sử dụng AdminLayout) ---
    {
      path: '/admin',
      component: AdminLayout,
      children: [
        {
          path: 'dashboard',
          name: 'admin-dashboard',
          component: AdminDashboard,
          meta: { requiresAuth: true, role: 'admin' }
        }
      ]
    }
  ]
})

// === NGƯỜI GÁC CỔNG (ROUTE GUARD) ===
router.beforeEach((to, from, next) => {
  const authStore = useAuthStore()

  // Chặn nếu trang yêu cầu đăng nhập mà chưa có token
  if (to.meta.requiresAuth && !authStore.isAuthenticated) {
    alert('Phiên đăng nhập đã hết hạn hoặc bạn chưa đăng nhập!')
    return next('/login')
  }

  // Chặn nếu vào trang Admin mà không phải quyền Admin
  if (to.meta.role === 'admin' && !authStore.isAdmin) {
    alert('Truy cập bị từ chối! Bạn không có quyền quản trị hệ thống.')
    return next('/') 
  }

  // Chặn nếu đã đăng nhập mà cố quay lại trang Login
  if (to.path === '/login' && authStore.isAuthenticated) {
    if (authStore.isAdmin) {
      return next('/admin/dashboard')
    } else {
      return next('/')
    }
  }

  next()
})

export default router