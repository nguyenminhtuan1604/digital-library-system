<template>
  <div class="home-page">
    <section class="hero-section">
      <v-container fluid class="hero-inner">
        <div class="hero-copy">
          <p class="eyebrow">Library operating system</p>
          <h1>Digital Library</h1>
          <p class="hero-lead">
            Một giao diện quản lý thư viện tối, gọn và thống nhất cho sách, độc giả,
            mượn trả, phí phạt và báo cáo vận hành.
          </p>
          <div class="hero-actions">
            <v-btn color="primary" size="large" :to="{ name: 'Login' }" prepend-icon="mdi-view-dashboard-outline">
              Vào hệ thống
            </v-btn>
            <v-btn variant="outlined" size="large" :to="{ name: 'Guide' }" prepend-icon="mdi-book-open-outline">
              Xem hướng dẫn
            </v-btn>
          </div>
          <div class="hero-stats">
            <div v-for="item in stats" :key="item.label">
              <strong>{{ item.value }}</strong>
              <span>{{ item.label }}</span>
            </div>
          </div>
        </div>

        <div class="console-preview" aria-label="Library dashboard preview">
          <div class="preview-top">
            <span />
            <span />
            <span />
            <strong>Live Console</strong>
          </div>
          <div class="preview-grid">
            <div class="preview-card main-card">
              <div>
                <small>Thẻ thư viện điện tử</small>
                <h3>Premium Reader</h3>
                <p>LIB-2026-0842</p>
              </div>
              <div class="qr-box">
                <v-icon size="54">mdi-qrcode</v-icon>
              </div>
            </div>
            <div class="preview-card alert-card">
              <v-icon size="28">mdi-alert-outline</v-icon>
              <strong>15</strong>
              <span>Sách quá hạn</span>
            </div>
            <div class="preview-card chart-card">
              <div v-for="bar in bars" :key="bar.day" class="bar-wrap">
                <i :style="{ height: `${bar.value}px` }" :class="{ active: bar.active }" />
                <span>{{ bar.day }}</span>
              </div>
            </div>
            <div class="preview-card list-card">
              <h4>Top sách mượn</h4>
              <p>Clean Code <strong>42 lượt</strong></p>
              <p>Design Systems <strong>38 lượt</strong></p>
              <p>Sapiens <strong>35 lượt</strong></p>
            </div>
          </div>
        </div>
      </v-container>
    </section>

    <section class="feature-section">
      <v-container fluid class="section-inner">
        <div class="section-head">
          <p class="eyebrow">Các luồng chính</p>
          <h2>Đủ chức năng, không rối mắt</h2>
        </div>
        <div class="feature-grid">
          <article v-for="feature in features" :key="feature.title" class="feature-card">
            <span class="feature-icon"><v-icon>{{ feature.icon }}</v-icon></span>
            <h3>{{ feature.title }}</h3>
            <p>{{ feature.desc }}</p>
          </article>
        </div>
      </v-container>
    </section>
  </div>
</template>

<script setup>
const stats = [
  { value: '12K+', label: 'Đầu sách' },
  { value: '45K+', label: 'Bản sao' },
  { value: '5K+', label: 'Độc giả' },
]

const bars = [
  { day: 'T2', value: 86 },
  { day: 'T3', value: 118 },
  { day: 'T4', value: 146 },
  { day: 'T5', value: 176, active: true },
  { day: 'T6', value: 132 },
]

const features = [
  {
    icon: 'mdi-bookshelf',
    title: 'Quản lý kho sách',
    desc: 'Theo dõi đầu sách, bản sao, thể loại, tình trạng và cảnh báo tồn kho trong cùng một nơi.',
  },
  {
    icon: 'mdi-swap-horizontal-bold',
    title: 'Mượn trả nhanh',
    desc: 'Luồng mượn, trả, phiếu mượn và quá hạn rõ ràng để thủ thư xử lý nhanh hơn.',
  },
  {
    icon: 'mdi-chart-box-outline',
    title: 'Báo cáo vận hành',
    desc: 'Dashboard, top sách mượn, phí phạt và các chỉ số chính được trình bày dễ quét.',
  },
]
</script>

<style scoped>
.home-page {
  overflow: hidden;
}

.hero-section {
  position: relative;
  min-height: calc(100vh - 64px);
  display: flex;
  align-items: center;
  border-bottom: 1px solid rgba(96, 165, 250, 0.16);
}

.hero-section::before {
  content: "";
  position: absolute;
  inset: 0;
  background:
    linear-gradient(rgba(96, 165, 250, 0.06) 1px, transparent 1px),
    linear-gradient(90deg, rgba(96, 165, 250, 0.06) 1px, transparent 1px);
  background-size: 72px 72px;
  mask-image: linear-gradient(180deg, black, transparent 88%);
  pointer-events: none;
}

.hero-inner,
.section-inner {
  width: 100%;
  max-width: 1240px;
  margin: 0 auto;
  padding: 88px 24px;
}

.hero-inner {
  display: grid;
  grid-template-columns: minmax(0, 0.88fr) minmax(480px, 1fr);
  gap: 54px;
  align-items: center;
  position: relative;
  z-index: 1;
}

.hero-copy h1 {
  max-width: 720px;
  margin: 12px 0 18px;
  color: white;
  font-size: clamp(3rem, 7vw, 6.8rem);
  font-weight: 950;
  line-height: 0.92;
}

.hero-lead {
  max-width: 620px;
  color: #b7c6dc;
  font-size: clamp(1.05rem, 1.8vw, 1.28rem);
  line-height: 1.68;
}

.hero-actions {
  display: flex;
  flex-wrap: wrap;
  gap: 14px;
  margin: 34px 0;
}

.hero-stats {
  display: grid;
  grid-template-columns: repeat(3, minmax(0, 1fr));
  gap: 12px;
  max-width: 520px;
}

.hero-stats div {
  padding: 16px;
  border: 1px solid rgba(96, 165, 250, 0.18);
  border-radius: 18px;
  background: rgba(15, 27, 46, 0.56);
}

.hero-stats strong,
.hero-stats span {
  display: block;
}

.hero-stats strong {
  color: white;
  font-size: 1.4rem;
  font-weight: 900;
}

.hero-stats span {
  color: var(--muted);
  margin-top: 4px;
}

.console-preview {
  min-height: 610px;
  padding: 16px;
  border: 1px solid rgba(96, 165, 250, 0.26);
  border-radius: 30px;
  background:
    linear-gradient(180deg, rgba(15, 27, 46, 0.80), rgba(2, 6, 23, 0.74));
  box-shadow: 0 40px 120px rgba(0, 0, 0, 0.46);
  backdrop-filter: blur(18px);
}

.preview-top {
  height: 46px;
  display: flex;
  align-items: center;
  gap: 8px;
  padding: 0 12px;
  color: var(--muted);
}

.preview-top span {
  width: 10px;
  height: 10px;
  border-radius: 999px;
  background: rgba(148, 163, 184, 0.42);
}

.preview-top strong {
  margin-left: auto;
  color: var(--blue);
  font-size: 0.8rem;
  text-transform: uppercase;
}

.preview-grid {
  display: grid;
  grid-template-columns: 1.2fr 0.8fr;
  grid-template-rows: 190px 320px;
  gap: 16px;
}

.preview-card {
  border: 1px solid rgba(148, 163, 184, 0.16);
  border-radius: 22px;
  background: rgba(8, 19, 35, 0.86);
  box-shadow: inset 0 1px 0 rgba(255, 255, 255, 0.04);
}

.main-card {
  grid-column: span 2;
  display: flex;
  align-items: center;
  justify-content: space-between;
  padding: 28px;
  background: linear-gradient(135deg, rgba(37, 99, 235, 0.96), rgba(56, 189, 248, 0.72));
}

.main-card small,
.main-card p {
  color: rgba(255, 255, 255, 0.78);
}

.main-card h3 {
  margin: 10px 0 20px;
  color: white;
  font-size: 1.75rem;
  font-weight: 900;
}

.main-card p {
  font-size: 1.2rem;
  font-weight: 800;
  letter-spacing: 0.08em;
}

.qr-box {
  width: 118px;
  height: 118px;
  display: grid;
  place-items: center;
  color: white;
  border-radius: 18px;
  background: rgba(2, 6, 23, 0.46);
}

.alert-card {
  display: grid;
  align-content: center;
  gap: 8px;
  padding: 24px;
  color: #fecdd3;
  border-color: rgba(248, 113, 113, 0.34);
}

.alert-card strong {
  color: #fb7185;
  font-size: 3rem;
  line-height: 1;
}

.chart-card {
  display: grid;
  grid-template-columns: repeat(5, 1fr);
  align-items: end;
  gap: 16px;
  padding: 26px 22px 18px;
}

.bar-wrap {
  display: grid;
  justify-items: center;
  gap: 10px;
  color: var(--muted);
  font-weight: 800;
}

.bar-wrap i {
  width: 100%;
  max-width: 44px;
  border-radius: 10px 10px 2px 2px;
  background: rgba(96, 165, 250, 0.22);
}

.bar-wrap i.active {
  background: linear-gradient(180deg, #38bdf8, #2563eb);
  box-shadow: 0 18px 38px rgba(37, 99, 235, 0.32);
}

.list-card {
  grid-column: span 2;
  padding: 24px;
}

.list-card h4 {
  margin-bottom: 16px;
  color: white;
  font-size: 1.1rem;
}

.list-card p {
  display: flex;
  justify-content: space-between;
  padding: 12px 0;
  color: #cbd5e1;
  border-bottom: 1px solid rgba(148, 163, 184, 0.12);
}

.list-card strong {
  color: var(--blue);
}

.feature-section {
  background: rgba(2, 6, 23, 0.28);
}

.section-head {
  max-width: 720px;
  margin-bottom: 30px;
}

.section-head h2 {
  color: white;
  font-size: clamp(2rem, 4vw, 3.5rem);
  font-weight: 950;
  line-height: 1;
}

.feature-grid {
  display: grid;
  grid-template-columns: repeat(3, minmax(0, 1fr));
  gap: 18px;
}

.feature-card {
  padding: 28px;
  border: 1px solid rgba(96, 165, 250, 0.18);
  border-radius: 22px;
  background: rgba(15, 27, 46, 0.70);
}

.feature-icon {
  width: 54px;
  height: 54px;
  display: grid;
  place-items: center;
  color: var(--blue);
  border-radius: 16px;
  background: rgba(56, 189, 248, 0.12);
}

.feature-card h3 {
  margin: 22px 0 10px;
  color: white;
  font-size: 1.18rem;
  font-weight: 900;
}

.feature-card p {
  color: var(--muted);
  line-height: 1.6;
}

@media (max-width: 1080px) {
  .hero-inner {
    grid-template-columns: 1fr;
  }

  .console-preview {
    min-height: auto;
  }
}

@media (max-width: 760px) {
  .hero-inner,
  .section-inner {
    padding: 62px 18px;
  }

  .hero-stats,
  .feature-grid,
  .preview-grid {
    grid-template-columns: 1fr;
  }

  .main-card,
  .list-card {
    grid-column: span 1;
  }

  .preview-grid {
    grid-template-rows: auto;
  }
}
</style>
