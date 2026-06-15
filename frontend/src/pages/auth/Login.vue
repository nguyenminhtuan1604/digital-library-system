<template>
  <main class="auth-page">
    <section class="auth-panel">
      <div class="auth-visual">
        <router-link to="/" class="auth-brand">
          <span><v-icon size="28">mdi-book-open-page-variant</v-icon></span>
          <strong>Digital Library</strong>
        </router-link>

        <div class="visual-copy">
          <p class="eyebrow">Secure access</p>
          <h1>Quản lý thư viện trong một màn hình gọn và rõ.</h1>
          <p>
            Dashboard tối, dữ liệu nổi bật, luồng mượn trả và báo cáo được gom về
            một hệ thống dùng chung cho các nhóm service.
          </p>
        </div>

        <div class="mini-console">
          <div>
            <small>Hôm nay</small>
            <strong>+45 bản sao</strong>
          </div>
          <div>
            <small>Đang mượn</small>
            <strong>450</strong>
          </div>
          <div>
            <small>Cần xử lý</small>
            <strong class="danger">15 quá hạn</strong>
          </div>
        </div>
      </div>

      <div class="auth-form-wrap">
        <div class="auth-form">
          <p class="eyebrow">Welcome back</p>
          <h2>Đăng nhập</h2>
          <p class="form-subtitle">Chọn tài khoản demo hoặc nhập thông tin của bạn.</p>

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

          <v-form @submit.prevent="handleLogin">
            <label>Vai trò demo</label>
            <v-select
              v-model="selectedRoleVal"
              :items="roles"
              item-title="title"
              item-value="value"
              class="mb-4"
              hide-details
              @update:model-value="onRoleChanged"
            />

            <label>Email hoặc tên đăng nhập</label>
            <v-text-field
              v-model="email"
              type="text"
              prepend-inner-icon="mdi-account-outline"
              placeholder="admin@smartlib.local"
              class="mb-4"
              hide-details
              required
            />

            <div class="field-row">
              <label>Mật khẩu</label>
              <a href="#">Quên mật khẩu?</a>
            </div>
            <v-text-field
              v-model="password"
              :type="showPass ? 'text' : 'password'"
              prepend-inner-icon="mdi-lock-outline"
              :append-inner-icon="showPass ? 'mdi-eye-off-outline' : 'mdi-eye-outline'"
              placeholder="Nhập mật khẩu"
              class="mb-3"
              hide-details
              required
              @click:append-inner="showPass = !showPass"
            />

            <v-checkbox
              v-model="remember"
              color="primary"
              density="compact"
              hide-details
              label="Ghi nhớ đăng nhập"
              class="mb-5"
            />

            <v-btn
              type="submit"
              color="primary"
              size="large"
              block
              height="50"
              :loading="loading"
              prepend-icon="mdi-login"
            >
              Đăng nhập
            </v-btn>
          </v-form>

          <p class="auth-switch">
            Chưa có tài khoản?
            <router-link to="/register">Đăng ký tài khoản mới</router-link>
          </p>
        </div>
      </div>
    </section>
  </main>
</template>

<script setup>
import { onMounted, ref } from 'vue'
import { useRouter } from 'vue-router'
import { useAuthStore } from '../../stores/authStore'
import { authApi } from '../../services/authApi'

const router = useRouter()
const auth = useAuthStore()

const email = ref('')
const password = ref('')
const remember = ref(false)
const showPass = ref(false)
const loading = ref(false)
const error = ref('')

const roles = [
  { title: 'Quản trị viên (Admin)', value: 'admin', email: 'admin@smartlib.local', password: 'Admin@123' },
  { title: 'Thủ thư (Librarian)', value: 'librarian', email: 'librarian@smartlib.local', password: 'Librarian@123' },
  { title: 'Độc giả (Reader)', value: 'reader', email: 'reader@smartlib.local', password: 'Reader@123' },
]

const selectedRoleVal = ref('admin')

function onRoleChanged(val) {
  const role = roles.find(item => item.value === val)
  if (!role) return
  email.value = role.email
  password.value = role.password
}

onMounted(() => onRoleChanged('admin'))

async function handleLogin() {
  loading.value = true
  error.value = ''
  try {
    const { data } = await authApi.login(email.value, password.value)
    auth.setAuth(data.accessToken || data.token, data.user)
    router.push({ name: 'Dashboard' })
  } catch (e) {
    error.value = e.response?.data?.message || 'Đăng nhập thất bại. Vui lòng kiểm tra lại.'
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
    radial-gradient(circle at 16% 8%, rgba(37, 99, 235, 0.26), transparent 30rem),
    radial-gradient(circle at 88% 18%, rgba(56, 189, 248, 0.18), transparent 28rem),
    var(--bg);
}

.auth-panel {
  width: min(1180px, 100%);
  min-height: 720px;
  display: grid;
  grid-template-columns: 1fr 0.88fr;
  overflow: hidden;
  border: 1px solid rgba(96, 165, 250, 0.20);
  border-radius: 30px;
  background: rgba(5, 12, 24, 0.82);
  box-shadow: 0 42px 130px rgba(0, 0, 0, 0.46);
}

.auth-visual {
  position: relative;
  display: flex;
  flex-direction: column;
  justify-content: space-between;
  padding: 34px;
  background:
    linear-gradient(135deg, rgba(37, 99, 235, 0.28), rgba(2, 6, 23, 0.62)),
    url('https://images.unsplash.com/photo-1521587760476-6c12a4b040da?q=80&w=1400&auto=format&fit=crop');
  background-size: cover;
  background-position: center;
}

.auth-visual::after {
  content: "";
  position: absolute;
  inset: 0;
  background: linear-gradient(180deg, rgba(2, 6, 23, 0.22), rgba(2, 6, 23, 0.86));
}

.auth-brand,
.visual-copy,
.mini-console {
  position: relative;
  z-index: 1;
}

.auth-brand {
  display: inline-flex;
  align-items: center;
  gap: 12px;
  color: white;
  text-decoration: none;
  font-weight: 900;
}

.auth-brand span {
  width: 48px;
  height: 48px;
  display: grid;
  place-items: center;
  border-radius: 15px;
  background: linear-gradient(135deg, #2563eb, #38bdf8);
}

.visual-copy {
  max-width: 560px;
}

.visual-copy h1 {
  margin: 10px 0 18px;
  color: white;
  font-size: clamp(2.2rem, 4vw, 4.4rem);
  font-weight: 950;
  line-height: 0.98;
}

.visual-copy p:not(.eyebrow) {
  color: #cbd5e1;
  line-height: 1.7;
}

.mini-console {
  display: grid;
  grid-template-columns: repeat(3, 1fr);
  gap: 12px;
}

.mini-console div {
  padding: 16px;
  border: 1px solid rgba(255, 255, 255, 0.16);
  border-radius: 18px;
  background: rgba(2, 6, 23, 0.52);
  backdrop-filter: blur(14px);
}

.mini-console small,
.mini-console strong {
  display: block;
}

.mini-console small {
  color: #9fb3cc;
  margin-bottom: 8px;
}

.mini-console strong {
  color: white;
  font-size: 1.05rem;
}

.mini-console .danger {
  color: #fb7185;
}

.auth-form-wrap {
  display: grid;
  place-items: center;
  padding: 38px;
}

.auth-form {
  width: min(100%, 460px);
}

.auth-form h2 {
  margin: 8px 0 8px;
  color: white;
  font-size: 2.3rem;
  font-weight: 950;
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

.field-row {
  display: flex;
  align-items: center;
  justify-content: space-between;
}

.field-row a,
.auth-switch a {
  color: var(--blue);
  font-weight: 800;
  text-decoration: none;
}

.auth-switch {
  margin-top: 26px;
  color: var(--muted);
  text-align: center;
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

  .mini-console {
    grid-template-columns: 1fr;
  }
}
</style>
