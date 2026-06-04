<template>
  <v-card class="data-panel">
    <div class="table-toolbar">
      <div>
        <span class="section-kicker">Danh sách người dùng</span>
        <h3>Danh sách người dùng</h3>
      </div>
      <div class="toolbar-actions">
        <v-text-field v-model="search" hide-details prepend-inner-icon="mdi-magnify" placeholder="Tìm theo tên hoặc email" density="compact" class="table-search" />
        <v-select v-model="roleFilter" hide-details :items="roles" item-title="title" item-value="value" density="compact" class="role-filter" />
        <v-btn color="primary" prepend-icon="mdi-plus" @click="openAdd">Thêm người dùng</v-btn>
      </div>
    </div>

    <v-skeleton-loader v-if="loading" type="table" class="mt-4" />

    <v-data-table
      v-else
      :headers="headers"
      :items="filteredUsers"
      :items-per-page="8"
      class="modern-table"
      no-data-text="Chưa có dữ liệu"
      items-per-page-text="Số dòng mỗi trang"
    >
      <template #item.avatar="{ item }">
        <v-avatar :color="item.isActive ? 'primary' : 'grey'" size="38">
          {{ initials(item.fullName) }}
        </v-avatar>
      </template>

      <template #item.role="{ item }">
        <v-chip color="primary" variant="tonal" size="small">{{ roleLabel(item.role) }}</v-chip>
      </template>

      <template #item.isActive="{ item }">
        <v-chip :color="item.isActive ? 'success' : 'error'" variant="tonal" size="small">
          {{ item.isActive ? 'Hoạt động' : 'Đã khóa' }}
        </v-chip>
      </template>

      <template #item.actions="{ item }">
        <v-btn icon="mdi-pencil-outline" variant="text" color="primary" :disabled="saving" @click="openEdit(item)" />
        <v-btn icon="mdi-delete-outline" variant="text" color="error" :disabled="saving" @click="askDelete(item)" />
      </template>
    </v-data-table>
  </v-card>

  <v-dialog v-model="dialog" max-width="560">
    <v-card class="dialog-card">
      <v-card-title>{{ editing ? 'Sửa thông tin người dùng' : 'Thêm người dùng mới' }}</v-card-title>
      <v-card-text>
        <v-text-field v-model="form.fullName" label="Họ và tên" prepend-inner-icon="mdi-account-outline" />
        <v-text-field v-model="form.email" label="Email" prepend-inner-icon="mdi-email-outline" />
        <v-text-field v-model="form.password" :label="editing ? 'Mật khẩu mới (không bắt buộc)' : 'Mật khẩu'" type="password" prepend-inner-icon="mdi-lock-outline" />
        <v-select v-model="form.role" :items="roleItems" item-title="title" item-value="value" label="Vai trò" />
        <v-switch v-model="form.isActive" color="primary" label="Tài khoản hoạt động" />
      </v-card-text>
      <v-card-actions>
        <v-spacer />
        <v-btn variant="text" :disabled="saving" @click="dialog = false">Hủy</v-btn>
        <v-btn color="primary" :loading="saving" @click="save">{{ editing ? 'Cập nhật' : 'Lưu' }}</v-btn>
      </v-card-actions>
    </v-card>
  </v-dialog>

  <v-dialog v-model="confirmDialog" max-width="430">
    <v-card class="dialog-card">
      <v-card-title>Xóa người dùng</v-card-title>
      <v-card-text>
        Bạn có chắc chắn muốn xóa <strong>{{ selected?.fullName }}</strong> không?
      </v-card-text>
      <v-card-actions>
        <v-spacer />
        <v-btn variant="text" :disabled="saving" @click="confirmDialog = false">Hủy</v-btn>
        <v-btn color="error" :loading="saving" @click="remove">Xóa</v-btn>
      </v-card-actions>
    </v-card>
  </v-dialog>
</template>

<script setup>
import { computed, reactive, ref } from 'vue'

const props = defineProps({
  users: { type: Array, default: () => [] },
  loading: { type: Boolean, default: false },
  saving: { type: Boolean, default: false }
})

const emit = defineEmits(['save', 'delete'])
const search = ref('')
const roleFilter = ref('all')
const dialog = ref(false)
const confirmDialog = ref(false)
const editing = ref(false)
const selected = ref(null)

const roleItems = [
  { title: 'Quản trị viên', value: 'Admin' },
  { title: 'Thủ thư', value: 'Librarian' },
  { title: 'Độc giả', value: 'Reader' }
]
const roles = [{ title: 'Tất cả vai trò', value: 'all' }, ...roleItems]

const form = reactive({
  id: null,
  fullName: '',
  email: '',
  password: '',
  role: 'Reader',
  isActive: true
})

const headers = [
  { title: 'Ảnh', key: 'avatar', sortable: false },
  { title: 'Họ và tên', key: 'fullName' },
  { title: 'Email', key: 'email' },
  { title: 'Vai trò', key: 'role' },
  { title: 'Trạng thái', key: 'isActive' },
  { title: 'Thao tác', key: 'actions', sortable: false, align: 'end' }
]

const filteredUsers = computed(() => {
  const keyword = search.value.trim().toLowerCase()
  return props.users
    .map(normalizeUser)
    .filter(user => roleFilter.value === 'all' || user.role === roleFilter.value)
    .filter(user => !keyword || user.fullName.toLowerCase().includes(keyword) || user.email.toLowerCase().includes(keyword))
})

function normalizeUser(user) {
  return {
    id: user.id,
    fullName: user.fullName || user.name || 'Người dùng chưa đặt tên',
    email: user.email || 'unknown@library.local',
    role: user.role || 'Reader',
    isActive: user.isActive ?? true
  }
}

function roleLabel(role) {
  return roleItems.find(item => item.value === role)?.title || role
}

function initials(name) {
  return name.split(' ').map(part => part[0]).join('').slice(0, 2).toUpperCase()
}

function resetForm() {
  Object.assign(form, { id: null, fullName: '', email: '', password: '', role: 'Reader', isActive: true })
}

function openAdd() {
  editing.value = false
  resetForm()
  dialog.value = true
}

function openEdit(user) {
  editing.value = true
  Object.assign(form, { ...normalizeUser(user), password: '' })
  dialog.value = true
}

async function save() {
  const payload = {
    fullName: form.fullName,
    email: form.email,
    role: form.role,
    isActive: form.isActive
  }
  if (!editing.value || form.password) payload.password = form.password
  if (editing.value) payload.id = form.id

  const success = await new Promise(resolve => emit('save', payload, resolve))
  if (success) dialog.value = false
}

function askDelete(user) {
  selected.value = normalizeUser(user)
  confirmDialog.value = true
}

async function remove() {
  const success = await new Promise(resolve => emit('delete', selected.value, resolve))
  if (success) confirmDialog.value = false
}
</script>

<style scoped>
</style>
