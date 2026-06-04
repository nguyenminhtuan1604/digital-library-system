<template>
  <v-card class="library-card-item">
    <div class="library-card-top">
      <div class="card-chip-icon">
        <v-icon icon="mdi-card-account-details-outline" size="28" />
      </div>
      <v-chip :color="statusColor" variant="flat" size="small">{{ statusLabel }}</v-chip>
    </div>

    <h3>{{ card.cardNumber }}</h3>
    <p>{{ card.readerName }}</p>

    <div class="card-meta">
      <div>
        <span>Ngày cấp</span>
        <strong>{{ formatDate(card.issuedAt) }}</strong>
      </div>
      <div>
        <span>Ngày hết hạn</span>
        <strong>{{ formatDate(card.expiredAt) }}</strong>
      </div>
    </div>

    <div class="card-actions">
      <v-btn color="primary" variant="tonal" size="small" :loading="loading" @click="$emit('renew', card.id)">
        Gia hạn thẻ
      </v-btn>
      <v-btn v-if="normalizedStatus === 'Active'" color="error" variant="outlined" size="small" :disabled="loading" @click="$emit('lock', card.id)">
        Khóa thẻ
      </v-btn>
      <v-btn v-else color="success" variant="outlined" size="small" :disabled="loading" @click="$emit('unlock', card.id)">
        Mở khóa thẻ
      </v-btn>
    </div>
  </v-card>
</template>

<script setup>
import { computed } from 'vue'

const props = defineProps({
  card: { type: Object, required: true },
  loading: { type: Boolean, default: false }
})

defineEmits(['renew', 'lock', 'unlock'])

const normalizedStatus = computed(() => props.card.status || 'Active')
const statusLabel = computed(() => {
  if (normalizedStatus.value === 'Active') return 'Hoạt động'
  if (normalizedStatus.value === 'Locked') return 'Đã khóa'
  if (normalizedStatus.value === 'Expired') return 'Hết hạn'
  return normalizedStatus.value
})
const statusColor = computed(() => {
  if (normalizedStatus.value === 'Active') return 'success'
  if (normalizedStatus.value === 'Locked') return 'error'
  if (normalizedStatus.value === 'Expired') return 'warning'
  return 'primary'
})

function formatDate(value) {
  if (!value) return 'Không có'
  return new Date(value).toLocaleDateString('vi-VN')
}
</script>

<style scoped>
</style>
