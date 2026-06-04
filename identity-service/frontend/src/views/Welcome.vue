<template>
  <div class="welcome-page">
    <section class="welcome-copy">
      <div class="brand-row">
        <div class="brand-badge">
          <v-icon icon="mdi-library" size="34" />
        </div>
        <div>
          <strong>Thư viện số</strong>
          <span>Dịch vụ Tài khoản & Báo cáo</span>
        </div>
      </div>

      <h1>Quản lý thư viện số</h1>
      <p>
        Hệ thống quản lý thư viện số thông minh cho độc giả, thủ thư và quản trị viên.
      </p>

      <div class="welcome-badges">
        <v-chip color="primary" variant="tonal" prepend-icon="mdi-shield-account-outline">Tài khoản</v-chip>
        <v-chip color="info" variant="tonal" prepend-icon="mdi-card-account-details-outline">Thẻ thư viện</v-chip>
        <v-chip color="secondary" variant="tonal" prepend-icon="mdi-chart-box-outline">Báo cáo</v-chip>
      </div>

      <div class="welcome-actions">
        <v-btn color="primary" size="large" append-icon="mdi-arrow-right" to="/login">Đăng nhập</v-btn>
        <v-btn color="primary" size="large" variant="outlined" to="/register">Đăng ký</v-btn>
      </div>
    </section>

    <section class="welcome-dashboard-card">
      <div class="mock-window">
        <div class="mock-topbar">
          <i></i><i></i><i></i>
        </div>
        <div class="mock-grid">
          <div class="mock-stat">
            <v-icon icon="mdi-book-open-page-variant-outline" />
            <strong>12.4K</strong>
            <span>Lượt mượn</span>
          </div>
          <div class="mock-stat">
            <v-icon icon="mdi-account-group-outline" />
            <strong>3.2K</strong>
            <span>Độc giả</span>
          </div>
          <div class="mock-stat">
            <v-icon icon="mdi-chart-line" />
            <strong>98%</strong>
            <span>Hoạt động</span>
          </div>
        </div>
        <div class="mock-chart">
          <b v-for="height in [46, 72, 58, 92, 68, 110, 82]" :key="height" :style="{ height: `${height}px` }"></b>
        </div>
      </div>
    </section>
  </div>
</template>

<script setup>
</script>

<style scoped>
</style>
