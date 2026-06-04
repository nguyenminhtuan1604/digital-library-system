<template>
  <v-menu v-model="menuOpen" location="bottom end" offset="12">
    <template #activator="{ props }">
      <v-avatar v-bind="props" class="account-avatar ml-3" size="44">
        <span>AD</span>
      </v-avatar>
    </template>

    <v-card class="account-menu-card" width="320">
      <div class="account-menu-header">
        <v-avatar class="account-menu-avatar" size="56">AD</v-avatar>
        <div class="account-menu-identity">
          <strong>{{ user.fullName }}</strong>
          <span>{{ user.email }}</span>
        </div>
      </div>

      <div class="account-menu-list">
        <button class="account-menu-item" type="button" @click="openProfile">
          <span class="account-menu-icon"><v-icon icon="mdi-account-outline" /></span>
          <span>Hồ sơ cá nhân</span>
        </button>
        <button class="account-menu-item" type="button" @click="openPassword">
          <span class="account-menu-icon"><v-icon icon="mdi-lock-reset" /></span>
          <span>Đổi mật khẩu</span>
        </button>
        <button class="account-menu-item" type="button" @click="goSettings">
          <span class="account-menu-icon"><v-icon icon="mdi-cog-outline" /></span>
          <span>Cài đặt</span>
        </button>
        <button class="account-menu-item danger" type="button" @click="logout">
          <span class="account-menu-icon"><v-icon icon="mdi-logout" /></span>
          <span>Đăng xuất</span>
        </button>
      </div>
    </v-card>
  </v-menu>

  <v-dialog v-model="profileDialog" max-width="520">
    <v-card class="profile-dialog-card">
      <div class="profile-dialog-hero">
        <v-btn
          class="profile-close"
          icon="mdi-close"
          size="small"
          variant="text"
          @click="closeProfile"
        />
        <v-avatar class="profile-dialog-avatar" size="72">AD</v-avatar>
        <div class="profile-dialog-title">
          <h3>{{ user.fullName }}</h3>
          <p>{{ user.email }}</p>
          <v-chip class="profile-role-chip" size="small">Quản trị viên</v-chip>
        </div>
      </div>

      <v-card-text class="profile-dialog-body">
        <template v-if="!editingProfile">
          <section class="profile-section">
            <div class="profile-section-title">
              <v-icon icon="mdi-account-badge-outline" />
              <span>Thông tin tài khoản</span>
            </div>
            <div class="profile-info-list">
              <div class="profile-info-item">
                <span>Họ và tên</span>
                <strong>{{ user.fullName }}</strong>
              </div>
              <div class="profile-info-item">
                <span>Email</span>
                <strong>{{ user.email }}</strong>
              </div>
              <div class="profile-info-item">
                <span>Vai trò</span>
                <strong>Quản trị viên</strong>
              </div>
              <div class="profile-info-item">
                <span>Trạng thái</span>
                <strong class="success-text">Đang hoạt động</strong>
              </div>
              <div class="profile-info-item">
                <span>Ngày tạo</span>
                <strong>04/06/2026</strong>
              </div>
            </div>
          </section>

          <section class="profile-section">
            <div class="profile-section-title">
              <v-icon icon="mdi-shield-check-outline" />
              <span>Bảo mật</span>
            </div>
            <div class="profile-security-grid">
              <div>
                <span>Phương thức đăng nhập</span>
                <strong>JWT</strong>
              </div>
              <div>
                <span>Quyền truy cập</span>
                <strong>Admin</strong>
              </div>
              <div>
                <span>Phiên đăng nhập</span>
                <strong>Đang hoạt động</strong>
              </div>
            </div>
          </section>
        </template>

        <template v-else>
          <section class="profile-section">
            <div class="profile-section-title">
              <v-icon icon="mdi-account-edit-outline" />
              <span>Chỉnh sửa hồ sơ</span>
            </div>
            <v-text-field
              v-model.trim="profileForm.fullName"
              label="Họ và tên"
              prepend-inner-icon="mdi-account-outline"
              variant="outlined"
            />
            <v-text-field
              v-model.trim="profileForm.email"
              label="Email"
              prepend-inner-icon="mdi-email-outline"
              variant="outlined"
            />
            <v-alert v-if="profileError" type="error" variant="tonal" density="compact">
              {{ profileError }}
            </v-alert>
          </section>
        </template>
      </v-card-text>

      <v-card-actions class="profile-dialog-actions">
        <template v-if="!editingProfile">
          <v-spacer />
          <v-btn variant="text" @click="closeProfile">Đóng</v-btn>
          <v-btn class="profile-primary-btn" @click="startEditProfile">Chỉnh sửa hồ sơ</v-btn>
        </template>
        <template v-else>
          <v-spacer />
          <v-btn variant="text" @click="cancelEditProfile">Hủy</v-btn>
          <v-btn class="profile-primary-btn" @click="saveProfile">Lưu thay đổi</v-btn>
        </template>
      </v-card-actions>
    </v-card>
  </v-dialog>

  <v-dialog v-model="passwordDialog" max-width="500">
    <v-card class="password-dialog-card">
      <div class="password-dialog-head">
        <div class="password-icon">
          <v-icon icon="mdi-lock-reset" />
        </div>
        <div>
          <h3>Đổi mật khẩu</h3>
          <p>Cập nhật mật khẩu để bảo vệ tài khoản quản trị.</p>
        </div>
      </div>

      <v-card-text class="password-dialog-body">
        <v-text-field
          v-model="passwordForm.current"
          label="Mật khẩu hiện tại"
          type="password"
          prepend-inner-icon="mdi-lock-outline"
          variant="outlined"
        />
        <v-text-field
          v-model="passwordForm.next"
          label="Mật khẩu mới"
          type="password"
          prepend-inner-icon="mdi-lock-plus-outline"
          variant="outlined"
        />
        <v-text-field
          v-model="passwordForm.confirm"
          label="Nhập lại mật khẩu mới"
          type="password"
          prepend-inner-icon="mdi-lock-check-outline"
          variant="outlined"
        />
        <v-alert v-if="passwordError" type="error" variant="tonal" density="compact">
          {{ passwordError }}
        </v-alert>
      </v-card-text>

      <v-card-actions class="password-dialog-actions">
        <v-spacer />
        <v-btn variant="text" @click="passwordDialog = false">Hủy</v-btn>
        <v-btn class="profile-primary-btn" @click="changePassword">Lưu thay đổi</v-btn>
      </v-card-actions>
    </v-card>
  </v-dialog>
</template>

<script setup>
import { onMounted, reactive, ref } from 'vue'
import { useRouter } from 'vue-router'
import { authService } from '../services/authService'
import { useUiStore } from '../stores/ui'

const USER_KEY = 'user'

const defaultUser = {
  fullName: 'Quản trị viên',
  email: 'admin@library.local',
  role: 'Quản trị viên',
  status: 'Đang hoạt động',
  createdAt: '04/06/2026'
}

const router = useRouter()
const ui = useUiStore()
const menuOpen = ref(false)
const profileDialog = ref(false)
const passwordDialog = ref(false)
const editingProfile = ref(false)
const profileError = ref('')
const passwordError = ref('')

const user = reactive({ ...defaultUser })
const profileForm = reactive({
  fullName: '',
  email: ''
})
const passwordForm = reactive({
  current: '',
  next: '',
  confirm: ''
})

onMounted(loadUser)

function loadUser() {
  try {
    const saved = JSON.parse(localStorage.getItem(USER_KEY) || '{}')
    Object.assign(user, defaultUser, saved)
  } catch {
    Object.assign(user, defaultUser)
  }
}

function persistUser() {
  localStorage.setItem(USER_KEY, JSON.stringify({ ...user }))
}

function openProfile() {
  menuOpen.value = false
  editingProfile.value = false
  profileError.value = ''
  profileDialog.value = true
}

function closeProfile() {
  editingProfile.value = false
  profileDialog.value = false
}

function startEditProfile() {
  profileError.value = ''
  Object.assign(profileForm, {
    fullName: user.fullName,
    email: user.email
  })
  editingProfile.value = true
}

function cancelEditProfile() {
  profileError.value = ''
  editingProfile.value = false
}

function saveProfile() {
  profileError.value = ''
  if (!profileForm.fullName || !profileForm.email) {
    profileError.value = 'Vui lòng nhập đầy đủ thông tin bắt buộc'
    return
  }

  Object.assign(user, {
    fullName: profileForm.fullName,
    email: profileForm.email
  })
  persistUser()
  editingProfile.value = false
  ui.notify('Cập nhật hồ sơ thành công')
}

function openPassword() {
  menuOpen.value = false
  passwordError.value = ''
  Object.assign(passwordForm, { current: '', next: '', confirm: '' })
  passwordDialog.value = true
}

function goSettings() {
  menuOpen.value = false
  router.push('/settings')
}

function changePassword() {
  passwordError.value = ''
  if (!passwordForm.current || !passwordForm.next || !passwordForm.confirm) {
    passwordError.value = 'Vui lòng nhập đầy đủ thông tin bắt buộc'
    return
  }
  if (passwordForm.next.length < 6) {
    passwordError.value = 'Mật khẩu mới tối thiểu 6 ký tự'
    return
  }
  if (passwordForm.next !== passwordForm.confirm) {
    passwordError.value = 'Nhập lại mật khẩu phải trùng với mật khẩu mới'
    return
  }

  passwordDialog.value = false
  ui.notify('Đổi mật khẩu thành công')
}

function logout() {
  authService.logout()
  router.push('/login')
}
</script>

<style scoped>
.account-avatar {
  border: 1px solid #BFDBFE;
  background: linear-gradient(135deg, #DBEAFE, #60A5FA) !important;
  color: #FFFFFF;
  cursor: pointer;
  font-weight: 800;
  box-shadow: 0 8px 20px rgba(96, 165, 250, .22);
  transition: transform .25s ease, box-shadow .25s ease;
}

.account-avatar:hover {
  transform: translateY(-1px);
  box-shadow: 0 12px 26px rgba(96, 165, 250, .30);
}

.account-menu-card {
  padding: 12px;
  border: 1px solid #E2E8F0 !important;
  border-radius: 24px !important;
  background: #FFFFFF !important;
  box-shadow: 0 18px 45px rgba(15, 23, 42, .12) !important;
}

.account-menu-header {
  display: flex;
  align-items: center;
  gap: 14px;
  padding: 10px 10px 14px;
  border-bottom: 1px solid #E2E8F0;
}

.account-menu-avatar,
.profile-dialog-avatar {
  border: 1px solid #BFDBFE;
  background: linear-gradient(135deg, #DBEAFE, #60A5FA) !important;
  color: #FFFFFF;
  font-weight: 800;
  box-shadow: 0 12px 26px rgba(96, 165, 250, .22);
}

.account-menu-identity {
  min-width: 0;
}

.account-menu-identity strong,
.account-menu-identity span {
  display: block;
  overflow: hidden;
  text-overflow: ellipsis;
  white-space: nowrap;
}

.account-menu-identity strong {
  color: #0F172A;
  font-size: 15px;
  font-weight: 800;
}

.account-menu-identity span {
  margin-top: 4px;
  color: #64748B;
  font-size: 13px;
  font-weight: 500;
}

.account-menu-list {
  display: grid;
  gap: 6px;
  padding-top: 12px;
}

.account-menu-item {
  display: flex;
  width: 100%;
  height: 52px;
  align-items: center;
  gap: 12px;
  border: 0;
  border-radius: 16px;
  background: transparent;
  color: #0F172A;
  cursor: pointer;
  font: inherit;
  font-size: 14px;
  font-weight: 700;
  text-align: left;
  transition: background .22s ease, color .22s ease, transform .22s ease;
}

.account-menu-item:hover {
  background: #F1F5F9;
  transform: translateY(-1px);
}

.account-menu-item.danger {
  color: #EF4444;
}

.account-menu-icon {
  display: grid;
  width: 38px;
  height: 38px;
  margin-left: 6px;
  place-items: center;
  border-radius: 999px;
  background: #EFF6FF;
  color: #2563EB;
}

.account-menu-item.danger .account-menu-icon {
  background: #FEF2F2;
  color: #EF4444;
}

.profile-dialog-card,
.password-dialog-card {
  overflow: hidden;
  border: 1px solid #E2E8F0 !important;
  border-radius: 28px !important;
  background: #FFFFFF !important;
  box-shadow: 0 26px 70px rgba(15, 23, 42, .16) !important;
}

.profile-dialog-hero {
  display: flex;
  position: relative;
  align-items: center;
  gap: 16px;
  padding: 28px;
  background: linear-gradient(135deg, #DBEAFE, #EFF6FF);
}

.profile-close {
  position: absolute;
  top: 14px;
  right: 14px;
  color: #2563EB;
}

.profile-dialog-title {
  min-width: 0;
}

.profile-dialog-title h3,
.password-dialog-head h3 {
  margin: 0;
  color: #0F172A;
  font-size: 22px;
  font-weight: 800;
  letter-spacing: 0;
}

.profile-dialog-title p,
.password-dialog-head p {
  margin: 5px 0 10px;
  color: #64748B;
  font-size: 14px;
}

.profile-role-chip {
  background: #FFFFFF !important;
  color: #2563EB !important;
  font-weight: 800;
}

.profile-dialog-body {
  display: grid;
  gap: 16px;
  padding: 22px 24px 10px !important;
}

.profile-section {
  display: grid;
  gap: 14px;
  padding: 16px;
  border: 1px solid #E2E8F0;
  border-radius: 22px;
  background: #FFFFFF;
}

.profile-section-title {
  display: flex;
  align-items: center;
  gap: 10px;
  color: #2563EB;
  font-size: 14px;
  font-weight: 800;
}

.profile-info-list {
  display: grid;
  gap: 10px;
}

.profile-info-item {
  display: flex;
  align-items: center;
  justify-content: space-between;
  gap: 14px;
  padding: 10px 12px;
  border-radius: 14px;
  background: #F8FAFC;
}

.profile-info-item span,
.profile-security-grid span {
  color: #64748B;
  font-size: 13px;
  font-weight: 500;
}

.profile-info-item strong,
.profile-security-grid strong {
  color: #0F172A;
  font-size: 13px;
  font-weight: 800;
  text-align: right;
}

.success-text {
  color: #22C55E !important;
}

.profile-security-grid {
  display: grid;
  grid-template-columns: repeat(3, 1fr);
  gap: 10px;
}

.profile-security-grid div {
  display: grid;
  gap: 6px;
  min-height: 82px;
  align-content: center;
  padding: 12px;
  border-radius: 16px;
  background: #F8FAFC;
}

.profile-security-grid strong {
  text-align: left;
}

.profile-dialog-actions,
.password-dialog-actions {
  padding: 14px 24px 22px !important;
}

.profile-primary-btn {
  border-radius: 14px !important;
  background: #60A5FA !important;
  color: #FFFFFF !important;
  font-weight: 800;
  box-shadow: 0 10px 22px rgba(96, 165, 250, .24);
}

.password-dialog-head {
  display: flex;
  align-items: center;
  gap: 14px;
  padding: 24px 24px 8px;
}

.password-icon {
  display: grid;
  width: 52px;
  height: 52px;
  flex: 0 0 52px;
  place-items: center;
  border-radius: 18px;
  background: #EFF6FF;
  color: #2563EB;
}

.password-dialog-body {
  display: grid;
  gap: 10px;
  padding: 16px 24px 6px !important;
}

@media (max-width: 600px) {
  .profile-dialog-hero {
    align-items: flex-start;
    flex-direction: column;
  }

  .profile-security-grid {
    grid-template-columns: 1fr;
  }

  .profile-info-item {
    align-items: flex-start;
    flex-direction: column;
  }

  .profile-info-item strong {
    text-align: left;
  }
}
</style>
