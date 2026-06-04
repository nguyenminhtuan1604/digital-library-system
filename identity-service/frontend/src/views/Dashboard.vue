<template>
  <div class="page-stack dashboard-page">
    <section class="dashboard-hero">
      <div>
        <span class="section-kicker">Tổng quan</span>
        <h1>Xin chào, Quản trị viên!</h1>
        <p>Tổng quan hoạt động thư viện hôm nay.</p>
      </div>
      <div class="dashboard-actions">
        <v-text-field v-model="selectedDate" type="date" hide-details density="compact" prepend-inner-icon="mdi-calendar-blank-outline" class="dashboard-date" />
        <v-btn class="primary-action" prepend-icon="mdi-download-outline" @click="$router.push('/reports')">Xuất báo cáo</v-btn>
      </div>
    </section>

    <v-row class="kpi-grid">
      <v-col v-for="item in kpis" :key="item.label" cols="12" sm="6" lg="3">
        <StatCard v-bind="item" />
      </v-col>
    </v-row>

    <v-row>
      <v-col cols="12" lg="8">
        <ReportChart title="Biểu đồ mượn trả theo tháng" eyebrow="Mượn trả" chip="Dữ liệu thật" :data="borrowChart" />
      </v-col>
      <v-col cols="12" lg="4">
        <ReportChart title="Trạng thái thẻ thư viện" eyebrow="Thẻ thư viện" chip="Trực tiếp" type="donut" :data="cardStatus" />
      </v-col>
    </v-row>

    <v-row>
      <v-col cols="12" lg="6">
        <RecentActivity :items="activities" />
      </v-col>
      <v-col cols="12" lg="6">
        <TopBooks :books="topBooks" />
      </v-col>
    </v-row>
  </div>
</template>

<script setup>
import { computed, onMounted, ref } from 'vue'
import RecentActivity from '../components/RecentActivity.vue'
import ReportChart from '../components/ReportChart.vue'
import StatCard from '../components/StatCard.vue'
import TopBooks from '../components/TopBooks.vue'
import { reportService } from '../services/reportService'
import { useUiStore } from '../stores/ui'

const ui = useUiStore()
const dashboard = ref({})
const selectedDate = ref(new Date().toISOString().slice(0, 10))

const kpis = computed(() => [
  { label: 'Tổng độc giả', value: dashboard.value.totalReaders ?? 0, icon: 'mdi-account-group-outline', hint: 'Dữ liệu từ database', tone: 'blue' },
  { label: 'Thẻ đang hoạt động', value: dashboard.value.activeCards ?? 0, icon: 'mdi-card-account-details-outline', hint: 'Thẻ còn hiệu lực', tone: 'green' },
  { label: 'Sách đang mượn', value: dashboard.value.borrowedBooks ?? 0, icon: 'mdi-book-arrow-right-outline', hint: 'Đang lưu thông', tone: 'cyan' },
  { label: 'Sách quá hạn', value: dashboard.value.overdueBooks ?? 0, icon: 'mdi-alert-circle-outline', hint: 'Cần xử lý', tone: 'amber' }
])

const borrowChart = computed(() => (dashboard.value.monthlyBorrowing || []).map(item => ({
  label: item.month,
  value: item.borrowed
})))

const cardStatus = computed(() => (dashboard.value.cardStatus || []).map(item => ({
  label: translateStatus(item.status),
  value: item.count,
  color: item.status === 'Active' ? '#22C55E' : item.status === 'Locked' ? '#EF4444' : '#F59E0B'
})))

const topBooks = computed(() => (dashboard.value.topBooks || []).map(book => ({
  bookId: book.bookId,
  bookTitle: book.bookTitle,
  totalBorrowed: book.borrowCount
})))

const activities = computed(() => (dashboard.value.recentActivities || []).map(item => ({
  title: item.action,
  text: item.description,
  time: new Date(item.createdAt).toLocaleTimeString('vi-VN', { hour: '2-digit', minute: '2-digit' }),
  icon: iconForActivity(item.type),
  color: colorForActivity(item.type),
  bg: bgForActivity(item.type)
})))

function translateStatus(status) {
  return status === 'Active' ? 'Hoạt động' : status === 'Locked' ? 'Đã khóa' : 'Hết hạn'
}

function iconForActivity(type) {
  return {
    user: 'mdi-account-plus-outline',
    card: 'mdi-card-account-details-outline',
    borrow: 'mdi-book-arrow-right-outline',
    return: 'mdi-book-check-outline',
    warning: 'mdi-alert-circle-outline',
    report: 'mdi-chart-box-outline',
    book: 'mdi-book-open-page-variant-outline'
  }[type] || 'mdi-information-outline'
}

function colorForActivity(type) {
  return {
    user: '#60A5FA',
    card: '#22C55E',
    borrow: '#2563EB',
    return: '#8B5CF6',
    warning: '#F59E0B',
    report: '#0EA5E9',
    book: '#14B8A6'
  }[type] || '#64748B'
}

function bgForActivity(type) {
  return {
    user: '#DBEAFE',
    card: '#DCFCE7',
    borrow: '#DBEAFE',
    return: '#EDE9FE',
    warning: '#FEF3C7',
    report: '#E0F2FE',
    book: '#CCFBF1'
  }[type] || '#F1F5F9'
}

onMounted(async () => {
  try {
    dashboard.value = (await reportService.getDashboard()).data
  } catch (error) {
    dashboard.value = {}
    ui.notify(error.response?.data?.message || 'Không tải được tổng quan', 'error')
  }
})
</script>

<style scoped>
</style>
