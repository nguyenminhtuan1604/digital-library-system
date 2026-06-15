<template>
  <div class="auth-layout d-flex align-center justify-center">
    <div class="bg-shape shape-1"></div>
    <div class="bg-shape shape-2"></div>

    <v-card class="auth-card rounded-xl pa-6 pa-sm-10" width="100%" max-width="500" elevation="0">
      
      <div class="text-center mb-6 cursor-pointer" @click="$router.push('/')">
        <div class="d-flex align-center justify-center mb-1">
          <v-icon color="#0d9488" size="36" class="mr-2">mdi-book-open-page-variant</v-icon>
          <h2 class="text-h5 font-weight-black text-grey-darken-4">Library System</h2>
        </div>
        <p class="text-caption text-grey-darken-1">Hệ thống quản lý thư viện số</p>
      </div>

      <div class="custom-toggle-bg d-flex rounded-lg pa-1 mb-8">
        <v-btn 
          class="flex-grow-1 rounded-md text-none font-weight-bold transition-all" 
          :color="isLogin ? '#0d9488' : 'transparent'" 
          :class="isLogin ? 'text-white elevation-2' : 'text-grey-darken-1 elevation-0'" 
          @click="isLogin = true"
        >
          Đăng nhập
        </v-btn>
        <v-btn 
          class="flex-grow-1 rounded-md text-none font-weight-bold transition-all" 
          :color="!isLogin ? '#0d9488' : 'transparent'" 
          :class="!isLogin ? 'text-white elevation-2' : 'text-grey-darken-1 elevation-0'" 
          @click="isLogin = false"
        >
          Đăng ký
        </v-btn>
      </div>

      <v-window v-model="isLogin">
        
        <v-window-item :value="true">
          <div class="text-center mb-6">
            <h3 class="text-h5 font-weight-bold text-grey-darken-4 mb-2">Đăng nhập</h3>
            <p class="text-body-2 text-grey-darken-1">Chào mừng bạn trở lại, vui lòng đăng nhập để tiếp tục.</p>
          </div>

          <v-form @submit.prevent="handleSubmit">
            <div class="input-label">Email hoặc Tên đăng nhập</div>
            <v-text-field
              v-model="loginForm.username"
              placeholder="admin@library.com"
              type="text"
              variant="outlined"
              density="comfortable"
              prepend-inner-icon="mdi-email-outline"
              color="#0d9488"
              class="mb-4 custom-input"
              hide-details
            ></v-text-field>

            <div class="input-label">Mật khẩu</div>
            <v-text-field
              v-model="loginForm.password"
              placeholder="••••••••"
              :type="showPassword ? 'text' : 'password'"
              variant="outlined"
              density="comfortable"
              prepend-inner-icon="mdi-lock-outline"
              :append-inner-icon="showPassword ? 'mdi-eye-outline' : 'mdi-eye-off-outline'"
              @click:append-inner="showPassword = !showPassword"
              color="#0d9488"
              class="mb-2 custom-input"
              hide-details
            ></v-text-field>

            <div class="d-flex align-center justify-space-between mb-6 mt-1">
              <v-checkbox 
                v-model="loginForm.rememberMe"
                label="Ghi nhớ đăng nhập" 
                color="#0d9488" 
                hide-details 
                density="compact"
                class="custom-checkbox"
              ></v-checkbox>
              <a href="#" class="text-decoration-none font-weight-bold text-caption teal-link">Quên mật khẩu?</a>
            </div>

            <v-btn type="submit" color="#0d9488" block size="large" class="text-white font-weight-bold text-none rounded-lg" elevation="0">
              <v-icon start size="20">mdi-login-variant</v-icon> Đăng nhập
            </v-btn>
          </v-form>
        </v-window-item>

        <v-window-item :value="false">
          <div class="text-center mb-6">
            <h3 class="text-h5 font-weight-bold text-grey-darken-4 mb-2">Tạo tài khoản mới</h3>
            <p class="text-body-2 text-grey-darken-1">Điền thông tin bên dưới để tạo tài khoản độc giả.</p>
          </div>

          <v-form @submit.prevent="handleSubmit">
            <div class="input-label">Họ và tên</div>
            <v-text-field v-model="registerForm.fullName" placeholder="Nguyễn Văn A" variant="outlined" density="comfortable" prepend-inner-icon="mdi-account-outline" color="#0d9488" class="mb-4 custom-input" hide-details></v-text-field>

            <div class="input-label">Email</div>
            <v-text-field v-model="registerForm.email" placeholder="example@email.com" type="email" variant="outlined" density="comfortable" prepend-inner-icon="mdi-email-outline" color="#0d9488" class="mb-4 custom-input" hide-details></v-text-field>

            <div class="input-label">Số điện thoại</div>
            <v-text-field v-model="registerForm.phone" placeholder="0123 456 789" type="tel" variant="outlined" density="comfortable" prepend-inner-icon="mdi-phone-outline" color="#0d9488" class="mb-4 custom-input" hide-details></v-text-field>

            <div class="input-label">Mật khẩu</div>
            <v-text-field v-model="registerForm.password" placeholder="••••••••" :type="showRegPassword ? 'text' : 'password'" variant="outlined" density="comfortable" prepend-inner-icon="mdi-lock-outline" :append-inner-icon="showRegPassword ? 'mdi-eye-outline' : 'mdi-eye-off-outline'" @click:append-inner="showRegPassword = !showRegPassword" color="#0d9488" class="mb-4 custom-input" hide-details></v-text-field>

            <div class="input-label">Xác nhận mật khẩu</div>
            <v-text-field v-model="registerForm.confirmPassword" placeholder="••••••••" :type="showConfirmPassword ? 'text' : 'password'" variant="outlined" density="comfortable" prepend-inner-icon="mdi-lock-check-outline" :append-inner-icon="showConfirmPassword ? 'mdi-eye-outline' : 'mdi-eye-off-outline'" @click:append-inner="showConfirmPassword = !showConfirmPassword" color="#0d9488" class="mb-2 custom-input" hide-details></v-text-field>

            <div class="mb-6 mt-2">
              <v-checkbox v-model="registerForm.agreeTerms" color="#0d9488" hide-details density="compact" class="custom-checkbox align-start">
                <template v-slot:label>
                  <span class="text-caption text-grey-darken-2 mt-1">
                    Tôi đồng ý với <a href="#" class="teal-link">Điều khoản sử dụng</a> và <a href="#" class="teal-link">Chính sách bảo mật</a>
                  </span>
                </template>
              </v-checkbox>
            </div>

            <v-btn type="submit" color="#0d9488" block size="large" class="text-white font-weight-bold text-none rounded-lg" elevation="0">
              <v-icon start size="20">mdi-account-plus-outline</v-icon> Đăng ký
            </v-btn>
          </v-form>
        </v-window-item>
      </v-window>

      <div class="d-flex align-center my-6">
        <v-divider class="border-opacity-50" color="#cbd5e1"></v-divider>
        <span class="mx-4 text-caption text-grey-darken-1 font-weight-medium">hoặc</span>
        <v-divider class="border-opacity-50" color="#cbd5e1"></v-divider>
      </div>

      <v-btn variant="outlined" color="#e2e8f0" block size="large" class="text-none font-weight-bold rounded-lg bg-white mb-6 google-btn" elevation="0" @click="loginWithGoogle">
        <v-img src="https://www.svgrepo.com/show/475656/google-color.svg" width="20" height="20" class="mr-3 flex-grow-0"></v-img>
        <span class="text-grey-darken-3">{{ isLogin ? 'Đăng nhập với Google' : 'Đăng ký với Google' }}</span>
      </v-btn>

      <div class="text-center text-body-2 mb-2">
        <span class="text-grey-darken-1">{{ isLogin ? 'Chưa có tài khoản?' : 'Đã có tài khoản?' }}</span>
        <a href="#" class="font-weight-bold teal-link ml-1" @click.prevent="isLogin = !isLogin">
          {{ isLogin ? 'Đăng ký ngay' : 'Đăng nhập ngay' }}
        </a>
      </div>

    </v-card>
  </div>
</template>

<script setup>
import { ref, reactive } from 'vue'
import { useRouter } from 'vue-router'

const router = useRouter()

// Trạng thái kiểm soát màn hình hiện tại (true: Đăng nhập, false: Đăng ký)
const isLogin = ref(true)

// Kiểm soát hiển thị mật khẩu trơn
const showPassword = ref(false)
const showRegPassword = ref(false)
const showConfirmPassword = ref(false)

// Các Object lưu trữ dữ liệu Form đầu vào
const loginForm = reactive({
  username: '',
  password: '',
  rememberMe: false
})

const registerForm = reactive({
  fullName: '',
  email: '',
  phone: '',
  password: '',
  confirmPassword: '',
  agreeTerms: false
})

// Hàm xử lý nút Submit (Đăng nhập / Đăng ký)
const handleSubmit = () => {
  if (isLogin.value) {
    // Luồng xử lý Đăng nhập
    router.push('/dashboard')
  } else {
    // Luồng xử lý Đăng ký
    if (!registerForm.agreeTerms) {
      alert("Bạn phải đồng ý với Điều khoản sử dụng hệ thống!")
      return
    }
    if (registerForm.password !== registerForm.confirmPassword) {
      alert("Mật khẩu xác nhận không khớp nhau!")
      return
    }
    alert("🎉 Đăng ký tài khoản thành công! Hệ thống tự động chuyển về form Đăng nhập.")
    isLogin.value = true
  }
}

// Hàm xử lý nút Google
const loginWithGoogle = () => {
  alert("Chức năng đang chờ tích hợp Google OAuth2 từ Backend .NET!");
  // Sau này thay alert bằng lệnh gọi API hoặc thư viện vue3-google-login
}
</script>

<style scoped>
/* THIẾT LẬP NỀN KHÔNG GIAN */
.auth-layout {
  min-height: 100vh;
  background: linear-gradient(135deg, #f8fafc 0%, #e6fcf5 100%);
  position: relative;
  font-family: 'Inter', sans-serif !important;
  padding: 20px;
}

/* ĐỊNH HÌNH CÁC KHỐI BLUR NỀN */
.bg-shape {
  position: absolute;
  border-radius: 50%;
  filter: blur(80px);
  z-index: 0;
}
.shape-1 { width: 500px; height: 500px; background-color: rgba(20, 184, 166, 0.12); top: -100px; left: -100px; }
.shape-2 { width: 600px; height: 600px; background-color: rgba(13, 148, 136, 0.08); bottom: -150px; right: -150px; }

/* KIỂU DÁNG THẺ THÀNH PHẦN CHÍNH */
.auth-card {
  z-index: 1;
  box-shadow: 0 25px 50px rgba(13, 148, 136, 0.06) !important;
  border: 1px solid rgba(255, 255, 255, 0.8);
  background: #ffffff;
}

/* THANH CÔNG TẮC ĐĂNG NHẬP / ĐĂNG KÝ */
.custom-toggle-bg {
  background-color: #f1f5f9;
  border: 1px solid #e2e8f0;
}
.transition-all { transition: all 0.25s ease-in-out; }

/* KIỂU DÁNG CỦA NHÃN TEXT (LABELS) */
.input-label {
  font-size: 0.85rem;
  font-weight: 700;
  color: #1e293b;
  margin-bottom: 6px;
  margin-left: 2px;
}

/* THIẾT LẬP ĐƯỜNG VIỀN BO TRÒN INPUT */
.custom-input :deep(.v-field--variant-outlined) {
  border-radius: 8px;
  background-color: #ffffff;
}
.custom-input :deep(.v-field__outline__start),
.custom-input :deep(.v-field__outline__end),
.custom-input :deep(.v-field__outline__notch::before),
.custom-input :deep(.v-field__outline__notch::after) {
  border-color: #cbd5e1 !important;
}
.custom-input :deep(.v-field--focused .v-field__outline__start),
.custom-input :deep(.v-field--focused .v-field__outline__end) {
  border-color: #0d9488 !important;
}

/* TRIỆT TIÊU KHUYẾT ĐIỂM AUTOFILL ĐỔI NỀN XANH CỦA CHROME */
:deep(input:-webkit-autofill) {
  -webkit-box-shadow: 0 0 0 30px white inset !important;
  -webkit-text-fill-color: #334155 !important;
}

/* CHỈNH SỬA CHECKBOX TEXT */
:deep(.v-checkbox .v-label) {
  font-size: 0.85rem !important;
  color: #475569;
}

/* ĐƯỜNG LINK XANH NGỌC */
.teal-link {
  color: #0d9488;
  text-decoration: none;
  transition: color 0.2s;
}
.teal-link:hover { color: #0f766e; text-decoration: underline !important; }

/* GOOGLE SELECTION BUTTON */
.google-btn {
  border-color: #cbd5e1 !important;
}
.google-btn:hover {
  background-color: #f8fafc !important;
}
</style>