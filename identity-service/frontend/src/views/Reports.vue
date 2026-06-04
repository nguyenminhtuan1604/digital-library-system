<template>
  <div class="page-stack">
    <section class="page-title">
      <div>
        <span class="section-kicker">Báo cáo tổng quan</span>
        <h1>Báo cáo</h1>
        <p>Báo cáo tổng hợp mượn trả, tiền phạt và sách phổ biến.</p>
      </div>
      <div class="dashboard-actions">
        <v-text-field v-model="filters.fromDate" type="date" hide-details density="compact" label="Từ ngày" class="dashboard-date" />
        <v-text-field v-model="filters.toDate" type="date" hide-details density="compact" label="Đến ngày" class="dashboard-date" />
        <v-btn color="primary" variant="tonal" prepend-icon="mdi-filter-outline" :loading="loading" @click="loadReports">Lọc</v-btn>
        <v-btn color="primary" prepend-icon="mdi-download-outline" :loading="exporting" @click="exportReport">
          Xuất báo cáo
        </v-btn>
      </div>
    </section>

    <v-row>
      <v-col v-for="item in stats" :key="item.label" cols="12" md="4">
        <StatCard v-bind="item" />
      </v-col>
    </v-row>

    <v-row>
      <v-col cols="12" lg="7">
        <ReportChart title="Thống kê mượn sách" eyebrow="Theo tháng" chip="Mượn sách" :data="borrowStats" />
      </v-col>
      <v-col cols="12" lg="5">
        <ReportChart title="Thống kê tiền phạt" eyebrow="Tài chính" chip="Tiền phạt" :data="fineStats" />
      </v-col>
      <v-col cols="12">
        <ReportChart title="Thống kê độc giả" eyebrow="Người dùng" chip="Tổng quan" :data="readerStats" />
      </v-col>
    </v-row>

    <v-card class="data-panel">
      <div class="section-head">
        <div>
          <span>Sách</span>
          <h3>Sách được mượn nhiều</h3>
        </div>
      </div>
      <v-skeleton-loader v-if="loading" type="table" class="mt-4" />
      <v-table v-else class="modern-simple-table">
        <thead>
          <tr>
            <th>Mã sách</th>
            <th>Tên sách</th>
            <th>Số lượt mượn</th>
          </tr>
        </thead>
        <tbody>
          <tr v-for="book in books" :key="book.bookId">
            <td>{{ book.bookId }}</td>
            <td>{{ book.bookTitle }}</td>
            <td>{{ book.borrowCount }}</td>
          </tr>
        </tbody>
      </v-table>
    </v-card>
  </div>
</template>

<script setup>
import { computed, onMounted, reactive, ref } from 'vue'
import ReportChart from '../components/ReportChart.vue'
import StatCard from '../components/StatCard.vue'
import { reportService } from '../services/reportService'
import { useUiStore } from '../stores/ui'

const ui = useUiStore()
const loading = ref(true)
const exporting = ref(false)
const overview = ref({})
const booksReport = ref({})
const borrowingReport = ref({})
const finesReport = ref({})
const filters = reactive({
  fromDate: '',
  toDate: ''
})

const queryParams = computed(() => ({
  ...(filters.fromDate ? { fromDate: filters.fromDate } : {}),
  ...(filters.toDate ? { toDate: filters.toDate } : {})
}))

const books = computed(() => booksReport.value.topBooks || [])

const stats = computed(() => [
  { label: 'Tổng lượt mượn', value: overview.value.totalBorrowed ?? 0, icon: 'mdi-book-arrow-right-outline', hint: 'Lượt mượn sách', tone: 'blue' },
  { label: 'Tổng lượt trả', value: overview.value.totalReturned ?? 0, icon: 'mdi-book-check-outline', hint: 'Lượt trả sách', tone: 'green' },
  { label: 'Tổng tiền phạt', value: `${overview.value.totalFines ?? 0} đ`, icon: 'mdi-cash-multiple', hint: 'Tiền phạt đã ghi nhận', tone: 'cyan' }
])

const borrowStats = computed(() => (borrowingReport.value.monthly || []).map(item => ({
  label: item.month,
  value: item.borrowed
})))

const fineStats = computed(() => (finesReport.value.monthly || []).map(item => ({
  label: item.month,
  value: item.amount
})))

const readerStats = computed(() => [
  { label: 'Tổng độc giả', value: overview.value.totalReaders ?? 0 },
  { label: 'Tổng thẻ', value: overview.value.totalCards ?? 0 },
  { label: 'Quá hạn', value: overview.value.overdueBooks ?? 0 },
  { label: 'Đã trả', value: overview.value.totalReturned ?? 0 }
])

async function loadReports() {
  loading.value = true
  try {
    const params = queryParams.value
    const [overviewRes, booksRes, borrowingRes, finesRes] = await Promise.all([
      reportService.getOverview(params),
      reportService.getBooks(params),
      reportService.getBorrowing(params),
      reportService.getFines(params)
    ])
    overview.value = overviewRes.data
    booksReport.value = booksRes.data
    borrowingReport.value = borrowingRes.data
    finesReport.value = finesRes.data
  } catch (error) {
    ui.notify(error.response?.data?.message || 'Không tải được dữ liệu báo cáo', 'error')
  } finally {
    loading.value = false
  }
}

async function exportReport() {
  exporting.value = true
  try {
    const { data } = await reportService.exportReport({ ...queryParams.value, format: 'csv' })
    downloadBlob(data, 'report-summary.csv')
    ui.notify('Đã xuất báo cáo CSV')
  } catch (error) {
    ui.notify(error.response?.data?.message || 'Không xuất được báo cáo', 'error')
  } finally {
    exporting.value = false
  }
}

function downloadBlob(blob, filename) {
  const url = URL.createObjectURL(blob)
  const link = document.createElement('a')
  link.href = url
  link.download = filename
  link.click()
  URL.revokeObjectURL(url)
}

onMounted(loadReports)
</script>

<style scoped>
</style>
