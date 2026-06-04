<template>
  <AuthLayout>
    <v-card class="auth-card login-card">
      <div class="auth-card-head">
        <div class="login-icon">
          <v-icon icon="mdi-shield-account-outline" size="28" />
        </div>
        <div>
          <span>Chào mừng trở lại</span>
          <h2>Đăng nhập</h2>
        </div>
      </div>

      <v-form class="auth-form" @submit.prevent="login">
        <v-text-field
          v-model="form.email"
          label="Email"
          prepend-inner-icon="mdi-email-outline"
          autocomplete="email"
          density="comfortable"
        />
        <v-text-field
          v-model="form.password"
          label="Mật khẩu"
          type="password"
          prepend-inner-icon="mdi-lock-outline"
          autocomplete="current-password"
          density="comfortable"
        />

        <div class="login-options">
          <v-checkbox v-model="remember" color="primary" hide-details density="compact" label="Ghi nhớ đăng nhập" />
          <RouterLink to="/register">Chưa có tài khoản? Đăng ký</RouterLink>
        </div>

        <v-alert v-if="error" type="error" variant="tonal" density="compact" class="auth-alert">
          {{ error }}
        </v-alert>

        <button class="gradient-button" type="submit" :disabled="loading">
          <v-icon icon="mdi-login" size="20" />
          {{ loading ? 'Đang đăng nhập...' : 'Đăng nhập' }}
        </button>
      </v-form>

      <div class="demo-account">
        <span>Tài khoản demo</span>
        <strong>admin@library.local</strong>
        <strong>Admin@123</strong>
      </div>
    </v-card>
  </AuthLayout>
</template>

<script setup>
import { reactive, ref } from 'vue'
import { RouterLink, useRouter } from 'vue-router'
import AuthLayout from '../layouts/AuthLayout.vue'
import { authService } from '../services/authService'

const router = useRouter()
const remember = ref(true)
const loading = ref(false)
const error = ref('')
const form = reactive({
  email: 'admin@library.local',
  password: 'Admin@123'
})

async function login() {
  error.value = ''
  loading.value = true
  try {
    const response = await authService.login({
      email: form.email,
      password: form.password
    })
    const token = response.data.token
    const user = response.data.user
    if (!token || !user) throw new Error('Phản hồi đăng nhập không hợp lệ')
    localStorage.setItem('token', token)
    localStorage.setItem('user', JSON.stringify(user))
    router.push('/dashboard')
  } catch (e) {
    if (!e.response) {
      error.value = 'Không kết nối được máy chủ. Kiểm tra server http://localhost:5003'
    } else if (e.response.status === 401) {
      error.value = e.response.data?.message || 'Email hoặc mật khẩu không đúng'
    } else {
      error.value = e.response.data?.message || 'Đăng nhập thất bại'
    }
  } finally {
    loading.value = false
  }
}
</script>

<style scoped>
</style>
