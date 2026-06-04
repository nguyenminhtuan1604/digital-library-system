<template>
  <v-menu v-model="menuOpen" location="bottom end" offset="12" :close-on-content-click="false">
    <template #activator="{ props }">
      <v-btn v-bind="props" icon variant="tonal" color="primary" class="ml-3 topbar-icon">
        <v-badge v-if="unreadCount" :content="unreadCount" color="error" offset-x="-2" offset-y="-2">
          <v-icon icon="mdi-bell-outline" />
        </v-badge>
        <v-icon v-else icon="mdi-bell-outline" />
      </v-btn>
    </template>

    <v-card class="notification-menu" width="390">
      <div class="menu-head">
        <div>
          <span>Thông báo</span>
          <h3>Hoạt động mới</h3>
        </div>
        <v-chip color="primary" variant="tonal" size="small">{{ unreadCount }} chưa đọc</v-chip>
      </div>

      <div class="notification-list">
        <div v-for="item in notifications" :key="item.title" class="notification-item" :class="{ unread: !item.read }">
          <div class="notification-icon" :style="{ color: item.color, background: item.bg }">
            <v-icon :icon="item.icon" size="20" />
          </div>
          <div>
            <strong>{{ item.title }}</strong>
            <p>{{ item.text }}</p>
          </div>
          <time>{{ item.time }}</time>
        </div>
      </div>

      <div class="menu-actions">
        <v-btn variant="text" color="primary" @click="markAllRead">Đánh dấu tất cả đã đọc</v-btn>
        <v-btn variant="tonal" color="primary" @click="viewAll">Xem tất cả thông báo</v-btn>
      </div>
    </v-card>
  </v-menu>
</template>

<script setup>
import { computed, ref } from 'vue'
import { useRouter } from 'vue-router'
import { useUiStore } from '../stores/ui'

const router = useRouter()
const ui = useUiStore()
const menuOpen = ref(false)
const notifications = ref([
  { title: 'Thẻ sắp hết hạn', text: 'Có 3 thẻ thư viện sắp hết hạn.', time: '09:10', icon: 'mdi-card-account-details-outline', color: '#2563EB', bg: '#DBEAFE', read: false },
  { title: 'Sách quá hạn', text: 'Có 2 sách đang quá hạn cần xử lý.', time: '10:25', icon: 'mdi-alert-circle-outline', color: '#D97706', bg: '#FEF3C7', read: false },
  { title: 'Báo cáo cập nhật', text: 'Báo cáo hôm nay đã được cập nhật.', time: '11:40', icon: 'mdi-chart-box-outline', color: '#16A34A', bg: '#DCFCE7', read: false },
  { title: 'Người dùng mới', text: 'Có người dùng mới đăng ký.', time: '13:05', icon: 'mdi-account-plus-outline', color: '#7C3AED', bg: '#EDE9FE', read: false }
])

const unreadCount = computed(() => notifications.value.filter(item => !item.read).length)

function markAllRead() {
  notifications.value = notifications.value.map(item => ({ ...item, read: true }))
  ui.notify('Đã đánh dấu tất cả thông báo là đã đọc')
}

function viewAll() {
  menuOpen.value = false
  router.push('/reports')
}
</script>

<style scoped>
</style>
