<template>
  <div class="public-shell">
    <v-app-bar class="public-topbar" elevation="0" density="comfortable">
      <v-container fluid class="public-nav">
        <router-link to="/" class="brand-link">
          <span class="brand-mark">
            <v-icon size="24">mdi-book-open-page-variant</v-icon>
          </span>
          <span>
            <strong>Digital Library</strong>
            <small>Library Operating System</small>
          </span>
        </router-link>

        <v-spacer />

        <nav class="nav-links d-none d-md-flex">
          <v-btn variant="text" :to="{ name: 'Home' }">Trang chủ</v-btn>
          <v-btn variant="text" :to="{ name: 'About' }">Giới thiệu</v-btn>
          <v-btn variant="text" :to="{ name: 'Guide' }">Hướng dẫn</v-btn>
          <v-btn variant="text" :to="{ name: 'Contact' }">Liên hệ</v-btn>
        </nav>

        <div class="nav-actions d-none d-md-flex">
          <v-btn variant="outlined" :to="{ name: 'Login' }" prepend-icon="mdi-login">Đăng nhập</v-btn>
          <v-btn color="primary" :to="{ name: 'Register' }" prepend-icon="mdi-account-plus">Đăng ký</v-btn>
        </div>

        <v-app-bar-nav-icon class="d-md-none" @click="drawer = true" />
      </v-container>
    </v-app-bar>

    <v-navigation-drawer v-model="drawer" temporary location="right" class="public-mobile-drawer">
      <div class="pa-5">
        <router-link to="/" class="brand-link mb-6" @click="drawer = false">
          <span class="brand-mark"><v-icon size="24">mdi-book-open-page-variant</v-icon></span>
          <span><strong>Digital Library</strong><small>Library OS</small></span>
        </router-link>
        <v-list nav>
          <v-list-item :to="{ name: 'Home' }" prepend-icon="mdi-home-outline" title="Trang chủ" @click="drawer = false" />
          <v-list-item :to="{ name: 'About' }" prepend-icon="mdi-information-outline" title="Giới thiệu" @click="drawer = false" />
          <v-list-item :to="{ name: 'Guide' }" prepend-icon="mdi-book-open-outline" title="Hướng dẫn" @click="drawer = false" />
          <v-list-item :to="{ name: 'Contact' }" prepend-icon="mdi-email-outline" title="Liên hệ" @click="drawer = false" />
        </v-list>
        <div class="mobile-actions">
          <v-btn block variant="outlined" :to="{ name: 'Login' }" prepend-icon="mdi-login" @click="drawer = false">Đăng nhập</v-btn>
          <v-btn block color="primary" :to="{ name: 'Register' }" prepend-icon="mdi-account-plus" @click="drawer = false">Đăng ký</v-btn>
        </div>
      </div>
    </v-navigation-drawer>

    <v-main class="public-main">
      <router-view />
    </v-main>

    <footer class="public-footer">
      <v-container fluid class="footer-inner">
        <div>
          <div class="brand-link footer-brand">
            <span class="brand-mark"><v-icon size="24">mdi-book-open-page-variant</v-icon></span>
            <span><strong>Digital Library</strong><small>Unified library platform</small></span>
          </div>
          <p>Quản lý sách, độc giả, mượn trả và báo cáo vận hành trong một giao diện tối gọn, rõ ràng.</p>
        </div>
        <div class="footer-links">
          <router-link to="/">Trang chủ</router-link>
          <router-link to="/guide">Hướng dẫn</router-link>
          <router-link to="/contact">Liên hệ</router-link>
        </div>
        <div class="footer-meta">
          <span>© 2026 Digital Library</span>
          <span>Built for multi-service library teams</span>
        </div>
      </v-container>
    </footer>
  </div>
</template>

<script setup>
import { ref } from 'vue'

const drawer = ref(false)
</script>

<style scoped>
.public-shell {
  min-height: 100vh;
  color: var(--text);
  background:
    linear-gradient(180deg, rgba(7, 17, 31, 0.20), rgba(7, 17, 31, 0.92)),
    var(--bg);
}

.public-topbar {
  background: rgba(4, 12, 24, 0.78) !important;
  border-bottom: 1px solid rgba(96, 165, 250, 0.16) !important;
  backdrop-filter: blur(18px);
}

.public-nav {
  max-width: 1240px;
  padding-inline: 24px;
}

.brand-link {
  display: inline-flex;
  align-items: center;
  gap: 12px;
  color: var(--text);
  text-decoration: none;
}

.brand-link strong,
.brand-link small {
  display: block;
}

.brand-link strong {
  font-size: 1.05rem;
  font-weight: 900;
  line-height: 1.1;
}

.brand-link small {
  color: var(--muted);
  font-size: 0.75rem;
  margin-top: 2px;
}

.brand-mark {
  width: 44px;
  height: 44px;
  display: grid;
  place-items: center;
  color: white;
  border-radius: 14px;
  background: linear-gradient(135deg, #2563eb, #38bdf8);
  box-shadow: 0 16px 38px rgba(37, 99, 235, 0.34);
}

.nav-links {
  gap: 4px;
  margin-right: 18px;
}

.nav-links :deep(.v-btn) {
  color: #cbd5e1 !important;
}

.nav-links :deep(.v-btn--active) {
  color: var(--blue) !important;
}

.nav-actions,
.mobile-actions {
  display: flex;
  gap: 10px;
}

.mobile-actions {
  flex-direction: column;
  margin-top: 20px;
}

.public-mobile-drawer {
  background: #06101f !important;
  border-left: 1px solid rgba(96, 165, 250, 0.20) !important;
}

.public-main {
  min-height: 72vh;
}

.public-footer {
  border-top: 1px solid rgba(96, 165, 250, 0.16);
  background: rgba(4, 12, 24, 0.86);
}

.footer-inner {
  max-width: 1240px;
  display: grid;
  grid-template-columns: minmax(0, 1.4fr) auto auto;
  gap: 32px;
  align-items: center;
  padding: 30px 24px;
}

.footer-brand {
  margin-bottom: 12px;
}

.footer-inner p,
.footer-meta {
  color: var(--muted);
  font-size: 0.92rem;
}

.footer-links,
.footer-meta {
  display: flex;
  flex-direction: column;
  gap: 8px;
}

.footer-links a {
  color: #cbd5e1;
  text-decoration: none;
}

@media (max-width: 860px) {
  .footer-inner {
    grid-template-columns: 1fr;
  }
}
</style>
