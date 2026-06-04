<template>
  <div class="page-stack">
    <section class="page-title">
      <div>
        <span class="section-kicker">Hệ thống</span>
        <h1>Cài đặt</h1>
        <p>Cấu hình giao diện, thông báo, bảo mật và sao lưu dữ liệu.</p>
      </div>
      <v-btn
        color="primary"
        prepend-icon="mdi-content-save-outline"
        :loading="saving"
        :disabled="loading || saving"
        @click="saveSettings"
      >
        Lưu cấu hình
      </v-btn>
    </section>

    <v-skeleton-loader v-if="loading" type="card, card, card" />

    <v-row v-else>
      <v-col cols="12" lg="6">
        <v-card class="settings-card">
          <h3>Cài đặt chung</h3>
          <v-text-field v-model="settings.systemName" label="Tên hệ thống" prepend-inner-icon="mdi-library" />
          <v-select v-model="settings.language" label="Ngôn ngữ" :items="['Tiếng Việt']" prepend-inner-icon="mdi-translate" />
          <v-select v-model="settings.timeZone" label="Múi giờ" :items="timezones" prepend-inner-icon="mdi-clock-outline" />
          <v-switch v-model="settings.enableRealtimeNotification" color="primary" label="Bật thông báo thời gian thực" />
        </v-card>
      </v-col>

      <v-col cols="12" lg="6">
        <v-card class="settings-card">
          <h3>Giao diện</h3>
          <v-select
            v-model="settings.themeMode"
            label="Chế độ sáng/tối"
            :items="themeModes"
            prepend-inner-icon="mdi-theme-light-dark"
          />
          <v-select
            v-model="settings.primaryColor"
            label="Màu chủ đạo"
            :items="colorOptions"
            item-title="title"
            item-value="title"
            prepend-inner-icon="mdi-palette-outline"
          >
            <template #selection="{ item }">
              <div class="color-selection">
                <i :style="{ background: item.raw.color }"></i>
                <span>{{ item.raw.title }}</span>
              </div>
            </template>
            <template #item="{ props, item }">
              <v-list-item v-bind="props">
                <template #prepend>
                  <i class="color-dot" :style="{ background: item.raw.color }"></i>
                </template>
              </v-list-item>
            </template>
          </v-select>
          <v-slider v-model="settings.borderRadius" label="Bo góc giao diện" min="8" max="28" step="2" thumb-label />
          <v-switch v-model="settings.collapseSidebar" color="primary" label="Thu gọn sidebar" />
        </v-card>
      </v-col>

      <v-col cols="12" lg="6">
        <v-card class="settings-card">
          <h3>Bảo mật</h3>
          <v-switch v-model="settings.requireAdminVerification" color="primary" label="Yêu cầu xác thực quản trị" />
          <v-switch v-model="settings.enableActivityLog" color="primary" label="Ghi log thao tác người dùng" />
          <v-text-field
            v-model.number="settings.autoLogoutMinutes"
            type="number"
            label="Tự động đăng xuất sau số phút không hoạt động"
            min="5"
            max="240"
            prepend-inner-icon="mdi-timer-outline"
          />
        </v-card>
      </v-col>

      <v-col cols="12" lg="6">
        <v-card class="settings-card">
          <h3>Thông báo</h3>
          <v-switch v-model="settings.notifyExpiringCards" color="primary" label="Thông báo thẻ sắp hết hạn" />
          <v-switch v-model="settings.notifyOverdueBooks" color="primary" label="Thông báo sách quá hạn" />
          <v-switch v-model="settings.notifyNewUsers" color="primary" label="Thông báo người dùng mới đăng ký" />
          <v-switch v-model="settings.notifyByEmail" color="primary" label="Nhận thông báo qua email" />
        </v-card>
      </v-col>

      <v-col cols="12">
        <v-card class="settings-card backup-card">
          <div>
            <h3>Sao lưu dữ liệu</h3>
            <p>Lần sao lưu gần nhất: <strong>{{ lastBackupLabel }}</strong></p>
          </div>
          <div class="backup-actions">
            <v-btn color="primary" prepend-icon="mdi-download-outline" @click="backupData">Sao lưu dữ liệu</v-btn>
            <v-btn color="primary" variant="tonal" prepend-icon="mdi-upload-outline" @click="triggerRestore">Khôi phục dữ liệu</v-btn>
            <input ref="restoreInput" type="file" accept=".json,application/json" hidden @change="restoreData" />
          </div>
        </v-card>
      </v-col>
    </v-row>
  </div>
</template>

<script setup>
import { computed, onMounted, reactive, ref, watch } from 'vue'
import { settingsService } from '../services/settingsService'
import { useUiStore } from '../stores/ui'

const STORAGE_KEY = 'systemSettings'
const ui = useUiStore()
const restoreInput = ref(null)
const loading = ref(true)
const saving = ref(false)
const initialized = ref(false)
const lastBackup = ref('')

const timezones = ['Asia/Ho_Chi_Minh', 'UTC']
const themeModes = ['Sáng', 'Tối']
const colorOptions = [
  { title: 'Xanh pastel', color: '#60A5FA' },
  { title: 'Xanh dương', color: '#3B82F6' },
  { title: 'Tím', color: '#A78BFA' },
  { title: 'Xanh lá', color: '#22C55E' }
]

const defaults = {
  systemName: 'Thư viện số',
  language: 'Tiếng Việt',
  timeZone: 'Asia/Ho_Chi_Minh',
  enableRealtimeNotification: true,
  themeMode: 'Sáng',
  primaryColor: 'Xanh pastel',
  borderRadius: 14,
  collapseSidebar: false,
  requireAdminVerification: true,
  enableActivityLog: true,
  autoLogoutMinutes: 30,
  notifyExpiringCards: true,
  notifyOverdueBooks: true,
  notifyNewUsers: true,
  notifyByEmail: false
}

const settings = reactive({ ...defaults })
const lastBackupLabel = computed(() => lastBackup.value ? new Date(lastBackup.value).toLocaleString('vi-VN') : 'Chưa có')

watch(
  settings,
  () => {
    ui.applySettings(settings)
  },
  { deep: true }
)

watch(
  () => settings.enableRealtimeNotification,
  (value, oldValue) => {
    if (!initialized.value || value === oldValue) return
    ui.notify(value ? 'Đã bật thông báo thời gian thực' : 'Đã tắt thông báo thời gian thực')
  }
)

async function loadSettings() {
  loading.value = true
  try {
    const { data } = await settingsService.getSettings()
    mergeSettings(data)
    localStorage.setItem(STORAGE_KEY, JSON.stringify(settings))
    ui.applySettings(settings)
  } catch (error) {
    console.error(error.response?.data || error.message)
    loadLocalSettings()
    ui.notify('Không thể tải cấu hình', 'error')
  } finally {
    loading.value = false
    initialized.value = true
  }
}

async function saveSettings() {
  saving.value = true
  try {
    await settingsService.saveSettings(settings)
    const { data } = await settingsService.getSettings()
    mergeSettings(data)
    localStorage.setItem(STORAGE_KEY, JSON.stringify(settings))
    ui.applySettings(settings)
    ui.notify('Lưu cấu hình thành công')
  } catch (error) {
    console.error(error.response?.data || error.message)
    ui.notify('Không thể lưu cấu hình', 'error')
  } finally {
    saving.value = false
  }
}

function loadLocalSettings() {
  const raw = localStorage.getItem(STORAGE_KEY)
  if (!raw) return
  try {
    mergeSettings(JSON.parse(raw))
    ui.applySettings(settings)
  } catch (error) {
    console.error(error)
  }
}

function mergeSettings(saved = {}) {
  Object.assign(settings, defaults, {
    ...saved,
    timeZone: saved.timeZone || saved.timezone || defaults.timeZone,
    themeMode: normalizeTheme(saved.themeMode),
    primaryColor: normalizeColor(saved.primaryColor)
  })
}

function normalizeTheme(value) {
  if (value === 'dark' || value === 'Tối') return 'Tối'
  return 'Sáng'
}

function normalizeColor(value) {
  const map = {
    '#60A5FA': 'Xanh pastel',
    '#3B82F6': 'Xanh dương',
    '#A78BFA': 'Tím',
    '#22C55E': 'Xanh lá'
  }
  return colorOptions.some(item => item.title === value) ? value : map[value] || 'Xanh pastel'
}

function backupData() {
  const backupTime = new Date().toISOString()
  const user = JSON.parse(localStorage.getItem('user') || '{"fullName":"Quản trị viên","email":"admin@library.local","role":"Admin"}')
  const payload = {
    settings: JSON.parse(JSON.stringify(settings)),
    backupTime,
    user
  }
  const blob = new Blob([JSON.stringify(payload, null, 2)], { type: 'application/json;charset=utf-8' })
  const url = URL.createObjectURL(blob)
  const link = document.createElement('a')
  link.href = url
  link.download = 'digital-library-backup.json'
  link.click()
  URL.revokeObjectURL(url)
  lastBackup.value = backupTime
  ui.notify('Đã tạo file sao lưu dữ liệu')
}

function triggerRestore() {
  restoreInput.value?.click()
}

async function restoreData(event) {
  const file = event.target.files?.[0]
  event.target.value = ''
  if (!file) return

  try {
    const payload = JSON.parse(await file.text())
    if (!payload.settings) throw new Error('Invalid backup file')
    mergeSettings(payload.settings)
    await saveSettings()
    lastBackup.value = payload.backupTime || new Date().toISOString()
    ui.notify('Khôi phục cấu hình thành công')
  } catch (error) {
    console.error(error)
    ui.notify('File khôi phục không hợp lệ', 'error')
  }
}

onMounted(loadSettings)
</script>

<style scoped>
</style>
