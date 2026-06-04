<template>
  <v-app-bar class="app-header" flat height="72">
    <v-btn icon="mdi-menu" variant="text" class="d-lg-none" @click="ui.toggleDrawer" />

    <div>
      <div class="header-eyebrow">Dịch vụ Tài khoản & Báo cáo</div>
      <h2>{{ title }}</h2>
    </div>

    <v-spacer />

    <v-menu v-model="searchOpen" location="bottom end" offset="8" :close-on-content-click="false">
      <template #activator="{ props }">
        <v-text-field
          v-bind="props"
          v-model="search"
          class="header-search d-none d-md-block"
          hide-details
          prepend-inner-icon="mdi-magnify"
          placeholder="Tìm kiếm..."
          density="compact"
          @focus="searchOpen = true"
        />
      </template>

      <v-card class="search-menu" width="320">
        <v-list v-if="filteredPages.length" density="comfortable">
          <v-list-item
            v-for="page in filteredPages"
            :key="page.to"
            :prepend-icon="page.icon"
            :title="page.title"
            @click="goPage(page.to)"
          />
        </v-list>
        <div v-else class="search-empty">Không tìm thấy kết quả</div>
      </v-card>
    </v-menu>

    <NotificationMenu />
    <UserMenu />
  </v-app-bar>
</template>

<script setup>
import { computed, ref } from 'vue'
import { useRoute, useRouter } from 'vue-router'
import NotificationMenu from './NotificationMenu.vue'
import UserMenu from './UserMenu.vue'
import { useUiStore } from '../stores/ui'

const route = useRoute()
const router = useRouter()
const ui = useUiStore()
const search = ref('')
const searchOpen = ref(false)

const pages = [
  { title: 'Tổng quan', to: '/dashboard', icon: 'mdi-view-dashboard-outline' },
  { title: 'Người dùng', to: '/users', icon: 'mdi-account-group-outline' },
  { title: 'Thẻ thư viện', to: '/cards', icon: 'mdi-card-account-details-outline' },
  { title: 'Báo cáo', to: '/reports', icon: 'mdi-chart-box-outline' },
  { title: 'Cài đặt', to: '/settings', icon: 'mdi-cog-outline' }
]

const title = computed(() => route.meta.title || 'Thư viện số')
const filteredPages = computed(() => {
  const keyword = search.value.trim().toLowerCase()
  if (!keyword) return pages
  return pages.filter(page => page.title.toLowerCase().includes(keyword))
})

function goPage(path) {
  searchOpen.value = false
  search.value = ''
  router.push(path)
}
</script>

<style scoped>
</style>
