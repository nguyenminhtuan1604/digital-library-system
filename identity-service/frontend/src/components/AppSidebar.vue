<template>
  <v-navigation-drawer
    v-model="ui.drawer"
    class="app-sidebar"
    :rail="ui.settings.collapseSidebar && !mobile"
    :width="260"
    :permanent="!mobile"
    :temporary="mobile"
  >
    <div class="sidebar-brand">
      <div class="sidebar-logo">
        <v-icon icon="mdi-library" size="28" />
      </div>
      <div v-if="!ui.settings.collapseSidebar || mobile">
        <div class="sidebar-title">Thư viện số</div>
        <div class="sidebar-subtitle">Dịch vụ Tài khoản & Báo cáo</div>
      </div>
    </div>

    <v-list nav class="sidebar-menu">
      <v-list-item
        v-for="item in menu"
        :key="item.title"
        :to="item.to"
        :prepend-icon="item.icon"
        :title="ui.settings.collapseSidebar && !mobile ? undefined : item.title"
      />
      <v-divider class="my-3 opacity-20" />
      <v-list-item prepend-icon="mdi-logout" :title="ui.settings.collapseSidebar && !mobile ? undefined : 'Đăng xuất'" @click="logout" />
    </v-list>

    <template #append>
      <div v-if="!ui.settings.collapseSidebar || mobile" class="sidebar-profile">
        <div class="profile-card">
          <v-avatar color="primary" size="42">AD</v-avatar>
          <div>
            <strong>Quản trị viên</strong>
            <span>admin@library.local</span>
          </div>
        </div>
        <v-btn block class="sidebar-logout" variant="tonal" prepend-icon="mdi-logout" @click="logout">
          Đăng xuất
        </v-btn>
      </div>
    </template>
  </v-navigation-drawer>
</template>

<script setup>
import { computed } from 'vue'
import { useDisplay } from 'vuetify'
import { useRouter } from 'vue-router'
import { useUiStore } from '../stores/ui'
import { authService } from '../services/authService'

const router = useRouter()
const ui = useUiStore()
const { mdAndDown } = useDisplay()
const mobile = computed(() => mdAndDown.value)

const menu = [
  { title: 'Tổng quan', icon: 'mdi-view-dashboard-outline', to: '/dashboard' },
  { title: 'Người dùng', icon: 'mdi-account-group-outline', to: '/users' },
  { title: 'Thẻ thư viện', icon: 'mdi-card-account-details-outline', to: '/cards' },
  { title: 'Báo cáo', icon: 'mdi-chart-box-outline', to: '/reports' },
  { title: 'Cài đặt', icon: 'mdi-cog-outline', to: '/settings' }
]

function logout() {
  authService.logout()
  router.push('/login')
}
</script>

<style scoped>
</style>
