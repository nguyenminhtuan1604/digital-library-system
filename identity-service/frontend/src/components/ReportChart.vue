<template>
  <v-card class="chart-card">
    <div class="section-head">
      <div>
        <span>{{ eyebrow }}</span>
        <h3>{{ title }}</h3>
      </div>
      <v-chip color="primary" variant="tonal" size="small">{{ chip }}</v-chip>
    </div>

    <div v-if="type === 'donut'" class="donut-panel">
      <VueApexCharts type="donut" height="230" :options="donutOptions" :series="donutSeries" />
      <div class="status-list">
        <div v-for="item in data" :key="item.label" class="status-row">
          <i :style="{ background: item.color }"></i>
          <span>{{ item.value }} {{ item.label }}</span>
        </div>
      </div>
    </div>

    <VueApexCharts v-else type="line" height="300" :options="lineOptions" :series="lineSeries" />
  </v-card>
</template>

<script setup>
import { computed } from 'vue'
import VueApexCharts from 'vue3-apexcharts'

const props = defineProps({
  title: { type: String, required: true },
  eyebrow: { type: String, default: 'Phân tích' },
  chip: { type: String, default: 'Trực tiếp' },
  type: { type: String, default: 'line' },
  data: { type: Array, default: () => [] }
})

const categories = computed(() => props.data.map(item => item.label))
const values = computed(() => props.data.map(item => Number(item.value) || 0))

const lineSeries = computed(() => [
  {
    name: 'Lượt mượn',
    data: values.value
  }
])

const lineOptions = computed(() => ({
  chart: {
    toolbar: { show: false },
    zoom: { enabled: false },
    fontFamily: 'Inter, Segoe UI, sans-serif'
  },
  colors: ['#60A5FA'],
  dataLabels: { enabled: false },
  stroke: {
    curve: 'smooth',
    width: 4
  },
  grid: {
    borderColor: '#F1F5F9',
    strokeDashArray: 4
  },
  markers: {
    size: 5,
    colors: ['#FFFFFF'],
    strokeColors: '#60A5FA',
    strokeWidth: 3
  },
  xaxis: {
    categories: categories.value,
    labels: { style: { colors: '#64748B', fontWeight: 500 } },
    axisBorder: { show: false },
    axisTicks: { show: false }
  },
  yaxis: {
    labels: { style: { colors: '#64748B', fontWeight: 500 } }
  },
  tooltip: {
    theme: 'light'
  }
}))

const donutSeries = computed(() => values.value)
const donutOptions = computed(() => ({
  chart: {
    toolbar: { show: false },
    fontFamily: 'Inter, Segoe UI, sans-serif'
  },
  labels: categories.value,
  colors: props.data.map(item => item.color),
  dataLabels: { enabled: false },
  legend: { show: false },
  stroke: {
    colors: ['#FFFFFF'],
    width: 4
  },
  plotOptions: {
    pie: {
      donut: {
        size: '72%',
        labels: {
          show: true,
          total: {
            show: true,
            label: 'Cards',
            color: '#64748B',
            fontWeight: 600
          }
        }
      }
    }
  },
  tooltip: {
    theme: 'light'
  }
}))
</script>

<style scoped>
</style>
