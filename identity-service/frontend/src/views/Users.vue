<template>
  <div class="page-stack">
    <section class="page-title">
      <div>
        <span class="section-kicker">Phân quyền truy cập</span>
        <h1>Người dùng</h1>
        <p>Quản lý tài khoản, vai trò và trạng thái người dùng trong hệ thống.</p>
      </div>
      <div class="toolbar-actions">
        <v-btn color="primary" variant="tonal" prepend-icon="mdi-download-outline" :loading="exporting" @click="exportUsers">
          Xuất người dùng
        </v-btn>
        <v-btn color="primary" prepend-icon="mdi-account-multiple-plus-outline" @click="importDialog = true">
          Nhập người dùng
        </v-btn>
      </div>
    </section>

    <UserTable :users="users" :loading="loading" :saving="saving" @save="saveUser" @delete="deleteUser" />

    <v-dialog v-model="importDialog" max-width="560">
      <v-card class="dialog-card">
        <v-card-title>Nhập người dùng</v-card-title>
        <v-card-text>
          <p class="dialog-help">Chọn file CSV với các cột: FullName, Email, Password, Role, Status.</p>
          <v-file-input v-model="importFile" accept=".csv" label="File CSV" prepend-icon="mdi-file-delimited-outline" />
        </v-card-text>
        <v-card-actions>
          <v-spacer />
          <v-btn variant="text" @click="importDialog = false">Hủy</v-btn>
          <v-btn color="primary" :loading="saving" @click="importUsers">Nhập</v-btn>
        </v-card-actions>
      </v-card>
    </v-dialog>
  </div>
</template>

<script setup>
import { onMounted, ref } from 'vue'
import UserTable from '../components/UserTable.vue'
import { userService } from '../services/userService'
import { useUiStore } from '../stores/ui'

const ui = useUiStore()
const users = ref([])
const loading = ref(true)
const saving = ref(false)
const exporting = ref(false)
const importDialog = ref(false)
const importFile = ref(null)

async function loadUsers() {
  loading.value = true
  try {
    users.value = (await userService.getUsers()).data
  } catch (error) {
    users.value = []
    ui.notify(error.response?.data?.message || 'Không tải được danh sách người dùng', 'error')
  } finally {
    loading.value = false
  }
}

async function saveUser(user, done = () => {}) {
  saving.value = true
  try {
    if (user.id) {
      await userService.updateUser(user.id, user)
      ui.notify('Cập nhật thành công')
    } else {
      await userService.createUser(user)
      ui.notify('Thêm mới thành công')
    }
    await loadUsers()
    done(true)
  } catch (error) {
    ui.notify(error.response?.data?.message || 'Không lưu được người dùng', 'error')
    done(false)
  } finally {
    saving.value = false
  }
}

async function deleteUser(user, done = () => {}) {
  saving.value = true
  try {
    await userService.deleteUser(user.id)
    ui.notify('Xóa thành công', 'warning')
    await loadUsers()
    done(true)
  } catch (error) {
    ui.notify(error.response?.data?.message || 'Không xóa được người dùng', 'error')
    done(false)
  } finally {
    saving.value = false
  }
}

async function importUsers() {
  const file = Array.isArray(importFile.value) ? importFile.value[0] : importFile.value
  if (!file) {
    ui.notify('Vui lòng chọn file CSV', 'warning')
    return
  }

  saving.value = true
  try {
    const { data } = await userService.importUsers(file)
    importDialog.value = false
    importFile.value = null
    await loadUsers()
    ui.notify(`${data.message}: ${data.imported} thêm mới, ${data.skipped} bỏ qua/cập nhật`)
  } catch (error) {
    ui.notify(error.response?.data?.message || 'Không nhập được người dùng', 'error')
  } finally {
    saving.value = false
  }
}

async function exportUsers() {
  exporting.value = true
  try {
    const { data } = await userService.exportUsers()
    downloadBlob(data, 'users-export.csv')
    ui.notify('Đã xuất danh sách người dùng')
  } catch (error) {
    ui.notify(error.response?.data?.message || 'Không xuất được danh sách người dùng', 'error')
  } finally {
    exporting.value = false
  }
}

function downloadBlob(blob, filename) {
  const url = URL.createObjectURL(blob)
  const link = document.createElement('a')
  link.href = url
  link.download = filename
  link.click()
  URL.revokeObjectURL(url)
}

onMounted(loadUsers)
</script>

<style scoped>
</style>
