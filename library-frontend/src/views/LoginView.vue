<template>
  <div class="login-container">
    <div class="login-card">
      <!-- Cột trái: Hình ảnh/Branding -->
      <div class="login-banner d-none d-md-flex">
        <div class="banner-overlay">
          <div class="d-flex align-center mb-6">
            <v-icon size="36" color="white" class="mr-2">mdi-book-open-page-variant</v-icon>
            <h2 class="text-white font-weight-black m-0" style="font-size: 24px;">Thư viện số</h2>
          </div>
          <h1 class="text-white font-weight-black mb-4" style="font-size: 32px; line-height: 1.2;">
            Tri thức mở ra<br>tương lai
          </h1>
          <p class="text-teal-lighten-4 text-body-2">
            Hệ thống quản lý thư viện số toàn diện, hỗ trợ định danh, phân quyền và tra cứu tài liệu thông minh.
          </p>
        </div>
      </div>

      <!-- Cột phải: Form Đăng nhập -->
      <div class="login-form-wrapper">
        <div class="form-content">
          <h2 class="text-h4 font-weight-black text-grey-darken-4 mb-2">Đăng nhập</h2>
          <p class="text-body-2 text-grey-darken-1 mb-8">Vui lòng nhập thông tin tài khoản của bạn.</p>

          <!-- Báo lỗi nếu có -->
          <div v-if="errorMsg" class="error-alert mb-4">
            <v-icon size="20" color="#dc2626" class="mr-2">mdi-alert-circle</v-icon>
            <span>{{ errorMsg }}</span>
          </div>

          <form @submit.prevent="handleLogin">
            <!-- Email -->
            <div class="input-group mb-5">
              <label>Email đăng nhập</label>
              <div class="input-wrapper">
                <v-icon size="20" color="#94a3b8" class="input-icon">mdi-email-outline</v-icon>
                <input type="email" v-model="email" placeholder="admin@thuvien.com" required />
              </div>
            </div>

            <!-- Mật khẩu -->
            <div class="input-group mb-6">
              <div class="d-flex justify-space-between w-100">
                <label>Mật khẩu</label>
                <a href="#" class="text-teal text-caption font-weight-bold text-decoration-none">Quên mật khẩu?</a>
              </div>
              <div class="input-wrapper">
                <v-icon size="20" color="#94a3b8" class="input-icon">mdi-lock-outline</v-icon>
                <input :type="showPassword ? 'text' : 'password'" v-model="password" placeholder="••••••••" required />
                <v-icon 
                  size="20" 
                  color="#94a3b8" 
                  class="input-icon-right cursor-pointer" 
                  @click="showPassword = !showPassword"
                >
                  {{ showPassword ? 'mdi-eye-off-outline' : 'mdi-eye-outline' }}
                </v-icon>
              </div>
            </div>

            <!-- Nút submit -->
            <button type="submit" class="btn-submit" :disabled="isLoading">
              <v-progress-circular v-if="isLoading" indeterminate size="20" width="3" color="white" class="mr-2"></v-progress-circular>
              <span>{{ isLoading ? 'Đang xử lý...' : 'Đăng nhập vào hệ thống' }}</span>
            </button>
          </form>

          <p class="text-center text-caption text-grey-darken-1 mt-6">
            Chưa có tài khoản? 
            <a href="#" class="text-teal font-weight-bold text-decoration-none">Đăng ký ngay</a>
          </p>
        </div>
      </div>
    </div>
  </div>
</template>

<script setup>
import { ref } from 'vue'
import { useRouter } from 'vue-router'
import { useAuthStore } from '../stores/auth'

const router = useRouter()
const authStore = useAuthStore()

// State
const email = ref('')
const password = ref('')
const showPassword = ref(false)
const isLoading = ref(false)
const errorMsg = ref('')

// === HÀM XỬ LÝ ĐĂNG NHẬP ===
const handleLogin = async () => {
  isLoading.value = true
  errorMsg.value = ''

  try {
    // ---------------------------------------------------------
    // SAU NÀY BẠN GẮN API THỰC TẾ Ở ĐÂY:
    // const response = await axios.post('https://localhost:5001/api/auth/login', {
    //   email: email.value,
    //   password: password.value
    // })
    // ---------------------------------------------------------

    // === MÔ PHỎNG GỌI API (GIẢ LẬP ĐỘ TRỄ 1.5 GIÂY) ===
    await new Promise(resolve => setTimeout(resolve, 1500))

    // DỮ LIỆU MOCK ĐỂ BẠN TEST PHÂN LUỒNG:
    let mockResponse = null;

    if (email.value === 'admin@thuvien.com' && password.value === '123456') {
      mockResponse = {
        token: 'eyJhbGciOiJIUzI1NiIsInR...',
        role: 'admin',
        user: { name: 'Admin Thư Viện', email: 'admin@thuvien.com' }
      }
    } else if (email.value === 'user@thuvien.com' && password.value === '123456') {
      mockResponse = {
        token: 'eyJhbGciOiJIUzI1NiIsInR...',
        role: 'user',
        user: { name: 'Nguyễn Độc Giả', email: 'user@thuvien.com' }
      }
    } else {
      throw new Error('Email hoặc mật khẩu không chính xác!')
    }

    // 1. Lưu thông tin vào Pinia & LocalStorage
    authStore.login(mockResponse.token, mockResponse.role, mockResponse.user)

    // 2. Chuyển hướng theo Role
    if (authStore.isAdmin) {
      router.push('/admin/dashboard') // Admin bay vào Dashboard
    } else {
      router.push('/') // User bay ra Landing Page
    }

  } catch (error) {
    errorMsg.value = error.message || 'Lỗi kết nối đến máy chủ!'
  } finally {
    isLoading.value = false
  }
}
</script>

<style scoped>
* { font-family: 'Inter', sans-serif; box-sizing: border-box; }
.text-teal { color: #0d9488 !important; }

.login-container {
  min-height: 100vh;
  display: flex;
  align-items: center;
  justify-content: center;
  background-color: #f1f5f9;
  padding: 20px;
}

.login-card {
  width: 100%;
  max-width: 1000px;
  background: white;
  border-radius: 24px;
  box-shadow: 0 20px 40px -15px rgba(0,0,0,0.1);
  display: flex;
  overflow: hidden;
  min-height: 600px;
}

/* CỘT TRÁI (BANNER) */
.login-banner {
  flex: 1;
  background: url('https://images.unsplash.com/photo-1507842217343-583bb7270b66?q=80&w=1000&auto=format&fit=crop') center/cover;
  position: relative;
}

.banner-overlay {
  position: absolute;
  inset: 0;
  background: linear-gradient(135deg, rgba(2, 44, 34, 0.9) 0%, rgba(13, 148, 136, 0.8) 100%);
  padding: 60px 40px;
  display: flex;
  flex-direction: column;
  justify-content: center;
}

/* CỘT PHẢI (FORM) */
.login-form-wrapper {
  flex: 1;
  display: flex;
  align-items: center;
  justify-content: center;
  padding: 40px;
}

.form-content {
  width: 100%;
  max-width: 380px;
}

.input-group label {
  display: block;
  font-size: 13px;
  font-weight: 700;
  color: #334155;
  margin-bottom: 6px;
}

.input-wrapper {
  position: relative;
  width: 100%;
  height: 48px;
}

.input-icon {
  position: absolute;
  left: 14px;
  top: 50%;
  transform: translateY(-50%);
}

.input-icon-right {
  position: absolute;
  right: 14px;
  top: 50%;
  transform: translateY(-50%);
}

.input-wrapper input {
  width: 100%;
  height: 100%;
  border: 1px solid #cbd5e1;
  border-radius: 12px;
  padding: 0 40px;
  font-size: 14px;
  color: #1e293b;
  outline: none;
  transition: all 0.2s ease;
}

.input-wrapper input:focus {
  border-color: #0d9488;
  box-shadow: 0 0 0 4px rgba(13, 148, 136, 0.1);
}

.btn-submit {
  width: 100%;
  height: 48px;
  background: #0d9488;
  color: white;
  border: none;
  border-radius: 12px;
  font-size: 15px;
  font-weight: 700;
  cursor: pointer;
  display: flex;
  align-items: center;
  justify-content: center;
  transition: background 0.2s ease;
}

.btn-submit:hover:not(:disabled) {
  background: #0f766e;
}

.btn-submit:disabled {
  background: #94a3b8;
  cursor: not-allowed;
}

.error-alert {
  background: #fee2e2;
  border: 1px solid #fecaca;
  color: #dc2626;
  padding: 12px 16px;
  border-radius: 12px;
  font-size: 13px;
  font-weight: 600;
  display: flex;
  align-items: center;
}

@media (max-width: 768px) {
  .login-card { min-height: auto; }
}
</style>