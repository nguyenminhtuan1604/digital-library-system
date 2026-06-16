import { defineStore } from 'pinia'

export const useAuthStore = defineStore('auth', {
  // 1. STATE: Nơi lưu trữ dữ liệu (Lấy từ LocalStorage lên nếu có)
  state: () => ({
    token: localStorage.getItem('token') || null,
    role: localStorage.getItem('role') || null,
    user: JSON.parse(localStorage.getItem('user')) || null
  }),

  // 2. GETTERS: Nơi lấy ra trạng thái (Kiểu như hỏi: "Đã đăng nhập chưa?")
  getters: {
    isAuthenticated: (state) => !!state.token,
    isAdmin: (state) => state.role === 'admin'
  },

  // 3. ACTIONS: Nơi thực hiện hành động (Đăng nhập, Đăng xuất)
  actions: {
    login(token, role, user) {
      // Lưu vào Pinia
      this.token = token
      this.role = role
      this.user = user
      
      // Lưu xuống trình duyệt để không bị mất khi F5
      localStorage.setItem('token', token)
      localStorage.setItem('role', role)
      localStorage.setItem('user', JSON.stringify(user))
    },
    
    logout() {
      // Xóa khỏi Pinia
      this.token = null
      this.role = null
      this.user = null
      
      // Xóa khỏi trình duyệt
      localStorage.removeItem('token')
      localStorage.removeItem('role')
      localStorage.removeItem('user')
    }
  }
})