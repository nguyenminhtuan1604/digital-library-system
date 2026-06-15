<template>
  <main class="auth-page">
    <section class="auth-panel">
      <div class="auth-form-wrap">
        <div class="auth-form">
          <router-link to="/" class="auth-brand compact">
            <span><v-icon size="26">mdi-book-open-page-variant</v-icon></span>
            <strong>Digital Library</strong>
          </router-link>

          <p class="eyebrow">Create reader account</p>
          <h1>Đăng ký thành viên</h1>
          <p class="form-subtitle">Tạo tài khoản độc giả để sử dụng hệ thống thư viện số.</p>

          <v-alert
            v-if="error"
            type="error"
            variant="tonal"
            density="comfortable"
            class="mb-5"
            closable
            @click:close="error = ''"
          >
            {{ error }}
          </v-alert>

          <v-alert v-if="success" type="success" variant="tonal" density="comfortable" class="mb-5">
            {{ success }}
          </v-alert>

          <v-form @submit.prevent="handleRegister">
            <label>Họ và tên</label>
            <v-text-field
              v-model="form.fullName"
              prepend-inner-icon="mdi-account-outline"
              placeholder="Nguyễn Văn A"
              class="mb-4"
              hide-details
              required
            />

            <label>Email đăng ký</label>
            <v-text-field
              v-model="form.email"
              prepend-inner-icon="mdi-email-outline"
              placeholder="yourname@gmail.com"
              type="email"
              class="mb-4"
              hide-details
              required
            />

            <label>Mật khẩu</label>
            <v-text-field
              v-model="form.password"
              prepend-inner-icon="mdi-lock-outline"
              placeholder="Nhập mật khẩu"
              type="password"
              class="mb-4"
              hide-details
              required
            />

            <label>Xác nhận mật khẩu</label>
            <v-text-field
              v-model="form.confirmPassword"
              prepend-inner-icon="mdi-lock-check-outline"
              placeholder="Nhập lại mật khẩu"
              type="password"
              class="mb-6"
              hide-details
              required
            />

            <v-btn
              type="submit"
              color="primary"
              size="large"
              block
              height="50"
              :loading="loading"
              prepend-icon="mdi-account-plus-outline"
            >
              Đăng ký tài khoản
            </v-btn>
          </v-form>

          <p class="auth-switch">
            Đã có tài khoản?
            <router-link to="/login">Đăng nhập ngay</router-link>
          </p>
        </div>
      </div>

      <div class="auth-visual">
        <div class="visual-copy">
          <p class="eyebrow">Connected library</p>
          <h2>Một tài khoản, nhiều dịch vụ thư viện.</h2>
          <p>
            Đăng ký để tra cứu, mượn trả và theo dõi lịch sử sử dụng thư viện trong
            cùng một giao diện thống nhất.
          </p>
        </div>
        <div class="reader-card">
          <div>
            <small>Reader profile</small>
            <strong>LIB-NEW-2026</strong>
          </div>
          <v-icon size="62">mdi-card-account-details-outline</v-icon>
        </div>
      </div>
    </section>
  </main>
</template>

<script setup>
import { reactive, ref } from 'vue'
import { authApi } from '../../services/authApi'

const form = reactive({ fullName: '', email: '', password: '', confirmPassword: '' })
const loading = ref(false)
const error = ref('')
const success = ref('')

async function handleRegister() {
  error.value = ''
  success.value = ''
  if (form.password !== form.confirmPassword) {
    error.value = 'Mật khẩu xác nhận không khớp.'
    return
  }
  loading.value = true
  try {
    await authApi.register({ fullName: form.fullName, email: form.email, password: form.password, role: 'Reader' })
    success.value = 'Đăng ký thành công. Bạn có thể chuyển sang trang đăng nhập.'
    Object.assign(form, { fullName: '', email: '', password: '', confirmPassword: '' })
  } catch (e) {
    error.value = e.response?.data?.message || 'Đăng ký thất bại. Vui lòng kiểm tra lại.'
  } finally {
    loading.value = false
  }
}
</script>

<style scoped>
.auth-page {
  min-height: 100vh;
  display: grid;
  place-items: center;
  padding: 28px;
  background:
    radial-gradient(circle at 22% 10%, rgba(56, 189, 248, 0.18), transparent 30rem),
    radial-gradient(circle at 82% 24%, rgba(37, 99, 235, 0.26), transparent 28rem),
    var(--bg);
}

.auth-panel {
  width: min(1180px, 100%);
  min-height: 740px;
  display: grid;
  grid-template-columns: 0.9fr 1fr;
  overflow: hidden;
  border: 1px solid rgba(96, 165, 250, 0.20);
  border-radius: 30px;
  background: rgba(5, 12, 24, 0.82);
  box-shadow: 0 42px 130px rgba(0, 0, 0, 0.46);
}

.auth-form-wrap {
  display: grid;
  place-items: center;
  padding: 38px;
}

.auth-form {
  width: min(100%, 460px);
}

.auth-brand {
  display: inline-flex;
  align-items: center;
  gap: 12px;
  color: white;
  text-decoration: none;
  font-weight: 900;
}

.auth-brand.compact {
  margin-bottom: 30px;
}

.auth-brand span {
  width: 46px;
  height: 46px;
  display: grid;
  place-items: center;
  border-radius: 15px;
  background: linear-gradient(135deg, #2563eb, #38bdf8);
}

.auth-form h1 {
  margin: 8px 0 8px;
  color: white;
  font-size: 2.35rem;
  font-weight: 950;
  line-height: 1.05;
}

.form-subtitle {
  color: var(--muted);
  margin-bottom: 30px;
}

label {
  display: block;
  color: #dbeafe;
  font-size: 0.9rem;
  font-weight: 800;
  margin: 0 0 9px;
}

.auth-switch {
  margin-top: 26px;
  color: var(--muted);
  text-align: center;
}

.auth-switch a {
  color: var(--blue);
  font-weight: 800;
  text-decoration: none;
}

.auth-visual {
  position: relative;
  display: flex;
  flex-direction: column;
  justify-content: flex-end;
  gap: 24px;
  padding: 42px;
  background:
    linear-gradient(135deg, rgba(2, 6, 23, 0.36), rgba(37, 99, 235, 0.55)),
    url('https://images.unsplash.com/photo-1507842217343-583bb7270b66?q=80&w=1400&auto=format&fit=crop');
  background-size: cover;
  background-position: center;
}

.auth-visual::after {
  content: "";
  position: absolute;
  inset: 0;
  background: linear-gradient(180deg, rgba(2, 6, 23, 0.22), rgba(2, 6, 23, 0.88));
}

.visual-copy,
.reader-card {
  position: relative;
  z-index: 1;
}

.visual-copy h2 {
  max-width: 560px;
  margin: 10px 0 18px;
  color: white;
  font-size: clamp(2.2rem, 4vw, 4.1rem);
  font-weight: 950;
  line-height: 0.98;
}

.visual-copy p:not(.eyebrow) {
  max-width: 560px;
  color: #cbd5e1;
  line-height: 1.7;
}

.reader-card {
  display: flex;
  align-items: center;
  justify-content: space-between;
  padding: 24px;
  border: 1px solid rgba(255, 255, 255, 0.18);
  border-radius: 24px;
  color: white;
  background: rgba(2, 6, 23, 0.54);
  backdrop-filter: blur(16px);
}

.reader-card small,
.reader-card strong {
  display: block;
}

.reader-card small {
  color: #9fb3cc;
  margin-bottom: 8px;
}

.reader-card strong {
  font-size: 1.45rem;
}

@media (max-width: 920px) {
  .auth-panel {
    grid-template-columns: 1fr;
  }

  .auth-visual {
    min-height: 360px;
  }
}

@media (max-width: 620px) {
  .auth-page {
    padding: 0;
  }

  .auth-panel {
    min-height: 100vh;
    border-radius: 0;
  }
}
</style>
