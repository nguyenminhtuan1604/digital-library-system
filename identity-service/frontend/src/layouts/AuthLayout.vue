<template>
  <div class="auth-page">
    <main class="auth-shell">
      <section class="auth-intro-card">
        <div class="auth-brand">
          <div class="auth-brand-icon">
            <v-icon icon="mdi-library" size="30" />
          </div>
          <div>
            <strong>Thư viện số</strong>
            <span>Dịch vụ Tài khoản & Báo cáo</span>
          </div>
        </div>

        <div class="auth-illustration">
          <div class="auth-shelf auth-shelf-one"></div>
          <div class="auth-shelf auth-shelf-two"></div>
          <div class="auth-books">
            <i v-for="n in 16" :key="n"></i>
          </div>
          <div class="auth-report-badge">
            <v-icon icon="mdi-chart-line" size="22" />
            <span>Báo cáo trực tuyến</span>
          </div>
        </div>

        <div class="auth-copy">
          <h1>Quản lý thư viện số</h1>
          <p>
            Quản lý người dùng, thẻ thư viện và báo cáo mượn trả trong một dashboard
            hiện đại, nhanh và dễ theo dõi.
          </p>
        </div>

        <div class="auth-mini-grid">
          <div class="auth-mini-card">
            <v-icon icon="mdi-shield-lock-outline" size="20" />
            <span>Xác thực JWT</span>
          </div>
          <div class="auth-mini-card">
            <v-icon icon="mdi-card-account-details-outline" size="20" />
            <span>Thẻ thư viện</span>
          </div>
          <div class="auth-mini-card">
            <v-icon icon="mdi-chart-box-outline" size="20" />
            <span>Báo cáo thống kê</span>
          </div>
        </div>
      </section>

      <section class="auth-form-panel">
        <slot />
      </section>
    </main>
  </div>
</template>

<script setup>
import { onMounted, onUnmounted } from 'vue'

onMounted(() => {
  document.body.classList.add('auth-lock')
})

onUnmounted(() => {
  document.body.classList.remove('auth-lock')
})
</script>

<style scoped>
</style>
