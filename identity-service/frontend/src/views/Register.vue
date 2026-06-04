<template>
  <AuthLayout>
    <v-card class="auth-card register-card">
      <div class="auth-card-head">
        <div class="login-icon">
          <v-icon icon="mdi-account-plus-outline" size="28" />
        </div>
        <div>
          <span>Tạo tài khoản</span>
          <h2>Đăng ký</h2>
        </div>
      </div>

      <v-form class="auth-form" @submit.prevent="register">
        <v-text-field v-model="form.fullName" :error-messages="errors.fullName" label="Họ và tên" prepend-inner-icon="mdi-account-outline" density="comfortable" />
        <v-text-field v-model="form.email" :error-messages="errors.email" label="Email" prepend-inner-icon="mdi-email-outline" density="comfortable" />
        <v-text-field v-model="form.password" :error-messages="errors.password" label="Mật khẩu" type="password" prepend-inner-icon="mdi-lock-outline" density="comfortable" />
        <v-text-field v-model="form.confirmPassword" :error-messages="errors.confirmPassword" label="Nhập lại mật khẩu" type="password" prepend-inner-icon="mdi-lock-check-outline" density="comfortable" />
        <v-select v-model="form.role" :items="roleOptions" item-title="title" item-value="value" label="Vai trò" prepend-inner-icon="mdi-account-badge-outline" density="comfortable" />

        <v-alert v-if="error" type="error" variant="tonal" density="compact" class="auth-alert">
          {{ error }}
        </v-alert>

        <button class="gradient-button" type="submit" :disabled="loading">
          <v-icon icon="mdi-account-plus-outline" size="20" />
          {{ loading ? 'Đang tạo tài khoản...' : 'Tạo tài khoản' }}
        </button>
      </v-form>

      <p class="auth-switch">
        Đã có tài khoản?
        <RouterLink to="/login">Đăng nhập</RouterLink>
      </p>
    </v-card>

    <v-snackbar v-model="snackbar.show" :color="snackbar.color" location="top right">
      {{ snackbar.text }}
    </v-snackbar>
  </AuthLayout>
</template>

<script setup>
import { reactive, ref } from 'vue'
import { useRouter, RouterLink } from 'vue-router'
import AuthLayout from '../layouts/AuthLayout.vue'
import { authService } from '../services/authService'

const router = useRouter()
const loading = ref(false)
const error = ref('')
const roleOptions = [
  { title: 'Độc giả', value: 'Reader' },
  { title: 'Thủ thư', value: 'Librarian' }
]
const form = reactive({
  fullName: '',
  email: '',
  password: '',
  confirmPassword: '',
  role: 'Reader'
})
const errors = reactive({
  fullName: '',
  email: '',
  password: '',
  confirmPassword: ''
})
const snackbar = reactive({
  show: false,
  color: 'success',
  text: ''
})

function validate() {
  errors.fullName = form.fullName.trim() ? '' : 'Vui lòng nhập họ và tên'
  errors.email = /^[^\s@]+@[^\s@]+\.[^\s@]+$/.test(form.email) ? '' : 'Email không hợp lệ'
  errors.password = form.password.length >= 6 ? '' : 'Mật khẩu tối thiểu 6 ký tự'
  errors.confirmPassword = form.confirmPassword === form.password ? '' : 'Mật khẩu nhập lại không khớp'
  return !errors.fullName && !errors.email && !errors.password && !errors.confirmPassword
}

async function register() {
  if (!validate()) return
  loading.value = true
  error.value = ''
  try {
    await authService.register({
      fullName: form.fullName,
      email: form.email,
      password: form.password,
      role: form.role
    })
    snackbar.show = true
    snackbar.color = 'success'
    snackbar.text = 'Đăng ký thành công'
    setTimeout(() => router.push('/login'), 700)
  } catch (e) {
    error.value = e.response?.data?.message || (!e.response ? 'Không kết nối được máy chủ. Kiểm tra server http://localhost:5003' : 'Đăng ký thất bại')
    snackbar.show = true
    snackbar.color = 'error'
    snackbar.text = error.value
  } finally {
    loading.value = false
  }
}
</script>

<style scoped>
</style>
