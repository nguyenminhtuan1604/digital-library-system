<template>
  <div :style="styles.shell">
    <header :style="styles.topNav">
      <div :style="styles.brand">
        <span :style="styles.brandIcon">
          <v-icon size="26">mdi-book-open-page-variant</v-icon>
        </span>
        <div>
          <div :style="styles.brandTitle">Digital Library</div>
          <div :style="styles.brandSub">Management Console</div>
        </div>
      </div>

      <div :style="styles.topActions">
        <button :style="styles.iconButton" type="button" aria-label="Search">
          <v-icon size="22">mdi-magnify</v-icon>
        </button>
        <button :style="styles.iconButton" type="button" aria-label="Notifications">
          <v-icon size="22">mdi-bell-outline</v-icon>
        </button>
        <div :style="styles.admin">
          <span :style="styles.statusDot" />
          <v-avatar color="primary" size="42">
            <span>{{ userInitials }}</span>
          </v-avatar>
          <div>
            <div :style="styles.adminName">{{ auth.user?.fullName || auth.user?.username || 'Admin' }}</div>
            <div :style="styles.adminRole">{{ auth.role || 'Admin' }}</div>
            <button :style="styles.logoutLink" type="button" @click="handleLogout">Đăng xuất</button>
          </div>
        </div>
      </div>
    </header>

    <div :style="styles.body">
      <aside :style="styles.leftSidebar">
        <div :style="styles.sidebarTitle">Menu</div>
        <nav :style="styles.menuList">
          <router-link
            v-for="item in menuItems"
            :key="item.to"
            :to="item.to"
            :style="menuItemStyle(isActive(item.to))"
          >
            <span :style="menuIconStyle(isActive(item.to))">
              <v-icon size="22">{{ item.icon }}</v-icon>
            </span>
            <span :style="styles.menuText">{{ item.title }}</span>
          </router-link>
        </nav>
      </aside>

      <main :style="styles.main">
        <router-view />
      </main>

      <aside :style="styles.overview">
        <div :style="styles.overviewHead">
          <div :style="styles.kicker">Dashboard Overview</div>
          <h2 :style="styles.overviewTitle">Dashboard Overview</h2>
        </div>

        <div :style="styles.metricStack">
          <article
            v-for="metric in overviewMetrics"
            :key="metric.title"
            :style="styles.metricCard"
          >
            <span :style="metricIconStyle(metric.warning)">
              <v-icon size="24">{{ metric.icon }}</v-icon>
            </span>
            <div>
              <div :style="styles.metricLabel">{{ metric.title }}</div>
              <div :style="styles.metricValue">{{ metric.value }}</div>
            </div>
          </article>
        </div>
      </aside>
    </div>
  </div>
</template>

<script setup>
import { computed } from 'vue'
import { useRoute, useRouter } from 'vue-router'
import { useAuthStore } from '../stores/authStore'

const auth = useAuthStore()
const route = useRoute()
const router = useRouter()

const userInitials = computed(() => {
  const name = auth.user?.fullName || auth.user?.username || 'A'
  return name.split(' ').filter(Boolean).map(part => part[0]).join('').slice(0, 2).toUpperCase()
})

const menuItems = computed(() => {
  const role = auth.role
  const items = [
    { icon: 'mdi-view-dashboard-outline', title: 'Dashboard', to: '/dashboard', roles: ['Admin', 'Librarian', 'Reader'] },
    { icon: 'mdi-bookshelf', title: 'Quản lý sách', to: '/dashboard/books', roles: ['Admin', 'Librarian'] },
    { icon: 'mdi-shape-outline', title: 'Thể loại', to: '/dashboard/categories', roles: ['Admin', 'Librarian'] },
    { icon: 'mdi-book-multiple-outline', title: 'Bản sao sách', to: '/dashboard/copies', roles: ['Admin', 'Librarian'] },
    { icon: 'mdi-account-group-outline', title: 'Quản lý độc giả', to: '/dashboard/readers', roles: ['Admin', 'Librarian'] },
    { icon: 'mdi-card-account-details-outline', title: 'Thẻ thư viện', to: '/dashboard/cards', roles: ['Admin', 'Librarian'] },
    { icon: 'mdi-book-plus-outline', title: 'Mượn sách', to: '/dashboard/borrow', roles: ['Admin', 'Librarian'] },
    { icon: 'mdi-book-arrow-left-outline', title: 'Trả sách', to: '/dashboard/return', roles: ['Admin', 'Librarian'] },
    { icon: 'mdi-clipboard-list-outline', title: 'Phiếu mượn', to: '/dashboard/borrow-records', roles: ['Admin', 'Librarian'] },
    { icon: 'mdi-alert-circle-outline', title: 'Sách quá hạn', to: '/dashboard/overdue', roles: ['Admin', 'Librarian'] },
    { icon: 'mdi-cash-multiple', title: 'Phí phạt / Công nợ', to: '/dashboard/fines', roles: ['Admin', 'Librarian'] },
    { icon: 'mdi-chart-box-outline', title: 'Báo cáo thống kê', to: '/dashboard/reports', roles: ['Admin'] },
  ]
  return items.filter(item => item.roles.includes(role))
})

const overviewMetrics = [
  { title: 'Thư viện', value: '1,234', icon: 'mdi-bookshelf' },
  { title: 'Sách mới hôm nay', value: '15', icon: 'mdi-calendar-plus-outline' },
  { title: 'Mượn sách', value: '85', icon: 'mdi-account-multiple-check-outline' },
  { title: 'Quá hạn', value: '3', icon: 'mdi-clock-alert-outline', warning: true },
]

const styles = {
  shell: {
    minHeight: '100vh',
    color: '#eaf2ff',
    background: 'radial-gradient(circle at 78% 0%, rgba(56,189,248,.10), transparent 32rem), radial-gradient(circle at 8% 26%, rgba(37,99,235,.13), transparent 30rem), #07111f',
    fontFamily: 'Inter, system-ui, -apple-system, Segoe UI, sans-serif',
  },
  topNav: {
    height: '78px',
    display: 'flex',
    alignItems: 'center',
    justifyContent: 'space-between',
    gap: '24px',
    padding: '0 28px',
    borderBottom: '1px solid rgba(96,165,250,.16)',
    background: 'rgba(4,12,24,.94)',
  },
  brand: { display: 'flex', alignItems: 'center', gap: '13px', minWidth: '260px' },
  brandIcon: {
    width: '50px',
    height: '50px',
    flex: '0 0 50px',
    display: 'grid',
    placeItems: 'center',
    color: '#fff',
    borderRadius: '15px',
    background: 'linear-gradient(135deg,#2563eb,#38bdf8)',
  },
  brandTitle: { color: '#fff', fontSize: '18px', fontWeight: '950', lineHeight: '1.1' },
  brandSub: { color: '#93a4bd', fontSize: '13px', marginTop: '3px' },
  topActions: { display: 'flex', alignItems: 'center', gap: '10px', marginLeft: 'auto' },
  iconButton: {
    width: '42px',
    height: '42px',
    display: 'grid',
    placeItems: 'center',
    color: '#dbeafe',
    border: '1px solid rgba(96,165,250,.18)',
    borderRadius: '14px',
    background: 'rgba(15,27,46,.70)',
  },
  admin: {
    display: 'flex',
    alignItems: 'center',
    gap: '12px',
    marginLeft: '8px',
    paddingLeft: '18px',
    borderLeft: '1px solid rgba(148,163,184,.18)',
  },
  statusDot: {
    width: '10px',
    height: '10px',
    flex: '0 0 10px',
    borderRadius: '999px',
    background: '#22c55e',
    boxShadow: '0 0 0 6px rgba(34,197,94,.12)',
  },
  adminName: { color: '#fff', fontSize: '15px', fontWeight: '900', lineHeight: '1.15' },
  adminRole: { color: '#93a4bd', fontSize: '12px', marginTop: '2px' },
  logoutLink: {
    display: 'block',
    marginTop: '2px',
    padding: 0,
    color: '#38bdf8',
    border: 0,
    background: 'transparent',
    fontSize: '12px',
    fontWeight: '850',
    textAlign: 'left',
    cursor: 'pointer',
  },
  body: {
    minHeight: 'calc(100vh - 78px)',
    display: 'grid',
    gridTemplateColumns: '282px minmax(0, 1fr) 330px',
    gap: '22px',
    padding: '24px 28px',
  },
  leftSidebar: {
    minWidth: 0,
    height: 'calc(100vh - 126px)',
    position: 'sticky',
    top: '102px',
    overflowY: 'auto',
    padding: '20px 14px',
    border: '1px solid rgba(96,165,250,.16)',
    borderRadius: '24px',
    background: 'linear-gradient(180deg,rgba(8,19,35,.96),rgba(4,12,24,.96))',
    boxShadow: '0 24px 80px rgba(0,0,0,.28)',
  },
  sidebarTitle: {
    padding: '0 12px 12px',
    color: '#67e8f9',
    fontSize: '12px',
    fontWeight: '950',
    letterSpacing: '.12em',
    textTransform: 'uppercase',
  },
  menuList: { display: 'grid', gridTemplateColumns: '1fr', gap: '8px' },
  menuText: {
    overflow: 'hidden',
    color: 'inherit',
    fontSize: '15px',
    fontWeight: '850',
    textOverflow: 'ellipsis',
    whiteSpace: 'nowrap',
  },
  main: { minWidth: 0, padding: '2px 0 44px' },
  overview: {
    minWidth: 0,
    height: 'calc(100vh - 126px)',
    position: 'sticky',
    top: '102px',
    overflowY: 'auto',
    padding: '22px',
    border: '1px solid rgba(96,165,250,.16)',
    borderRadius: '24px',
    background: 'linear-gradient(180deg,rgba(8,19,35,.96),rgba(4,12,24,.96))',
    boxShadow: '0 24px 80px rgba(0,0,0,.28)',
  },
  overviewHead: { marginBottom: '18px' },
  kicker: { color: '#67e8f9', fontSize: '12px', fontWeight: '950', letterSpacing: '.12em', textTransform: 'uppercase' },
  overviewTitle: { margin: '6px 0 0', color: '#fff', fontSize: '21px', fontWeight: '950', lineHeight: '1.15' },
  metricStack: { display: 'grid', gap: '14px' },
  metricCard: {
    minHeight: '96px',
    display: 'grid',
    gridTemplateColumns: '50px minmax(0,1fr)',
    alignItems: 'center',
    gap: '14px',
    padding: '16px',
    border: '1px solid rgba(96,165,250,.16)',
    borderRadius: '18px',
    background: 'rgba(15,27,46,.72)',
  },
  metricLabel: { color: '#cbd5e1', fontSize: '15px', fontWeight: '800', marginBottom: '6px' },
  metricValue: { color: '#fff', fontSize: '30px', fontWeight: '950', lineHeight: '1' },
}

function menuItemStyle(active) {
  return {
    width: '100%',
    minHeight: '48px',
    display: 'grid',
    gridTemplateColumns: '38px minmax(0,1fr)',
    alignItems: 'center',
    gap: '10px',
    padding: '0 12px',
    color: active ? '#fff' : '#cbd5e1',
    border: active ? '1px solid rgba(96,165,250,.30)' : '1px solid transparent',
    borderRadius: '15px',
    textDecoration: 'none',
    background: active ? 'linear-gradient(135deg,rgba(37,99,235,.38),rgba(56,189,248,.18))' : 'transparent',
  }
}

function menuIconStyle(active) {
  return {
    width: '36px',
    height: '36px',
    display: 'grid',
    placeItems: 'center',
    color: active ? '#fff' : '#9fb3cc',
    borderRadius: '12px',
    background: active ? 'rgba(56,189,248,.18)' : 'rgba(148,163,184,.08)',
  }
}

function metricIconStyle(warning) {
  return {
    width: '50px',
    height: '50px',
    display: 'grid',
    placeItems: 'center',
    color: warning ? '#fb7185' : '#38bdf8',
    borderRadius: '16px',
    background: warning ? 'rgba(251,113,133,.14)' : 'rgba(56,189,248,.12)',
  }
}

function isActive(path) {
  return route.path === path || (path !== '/dashboard' && route.path.startsWith(`${path}/`))
}

function handleLogout() {
  auth.logout()
  router.push({ name: 'Login' })
}
</script>
