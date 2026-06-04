<template>
  <div class="page-stack">
    <section class="page-title">
      <div>
        <span class="section-kicker">Thành viên</span>
        <h1>Thẻ thư viện</h1>
        <p>Theo dõi mã thẻ, tên độc giả, ngày hết hạn và trạng thái thẻ.</p>
      </div>
      <div class="toolbar-actions">
        <v-btn color="primary" variant="tonal" prepend-icon="mdi-card-search-outline" @click="openValidateDialog">
          Test Validate Card
        </v-btn>
        <v-btn color="primary" prepend-icon="mdi-card-plus-outline" @click="openIssueDialog">Tạo thẻ</v-btn>
      </div>
    </section>

    <v-skeleton-loader v-if="loading" type="card, card, card" />

    <v-row v-else>
      <v-col v-for="card in normalizedCards" :key="card.id" cols="12" md="6" xl="4">
        <LibraryCardItem :card="card" :loading="saving" @renew="renewCard" @lock="lockCard" @unlock="unlockCard" />
      </v-col>
    </v-row>

    <v-empty-state v-if="!loading && !normalizedCards.length" icon="mdi-card-account-details-outline" title="Chưa có thẻ thư viện" text="Danh sách thẻ sẽ hiển thị khi API trả dữ liệu." />

    <v-dialog v-model="issueDialog" max-width="520">
      <v-card class="dialog-card">
        <v-card-title>Tạo thẻ thư viện</v-card-title>
        <v-card-text>
          <v-select v-model="selectedReaderId" :items="readerOptions" item-title="label" item-value="id" label="Độc giả" prepend-inner-icon="mdi-account-outline" />
        </v-card-text>
        <v-card-actions>
          <v-spacer />
          <v-btn variant="text" :disabled="saving" @click="issueDialog = false">Hủy</v-btn>
          <v-btn color="primary" :loading="saving" @click="createCard">Tạo thẻ</v-btn>
        </v-card-actions>
      </v-card>
    </v-dialog>

    <v-dialog v-model="validateDialog" max-width="520">
      <v-card class="dialog-card">
        <v-card-title>Kiểm tra thẻ thư viện</v-card-title>
        <v-card-text>
          <v-select v-model="validateReaderId" :items="readerOptions" item-title="label" item-value="id" label="Độc giả" prepend-inner-icon="mdi-account-search-outline" />
          <v-alert v-if="validateResult" class="mt-3" :type="validateResult.valid ? 'success' : 'warning'" variant="tonal">
            <strong>{{ validateStatusText }}</strong>
            <div>{{ validateResult.message }}</div>
          </v-alert>
        </v-card-text>
        <v-card-actions>
          <v-spacer />
          <v-btn variant="text" :disabled="saving" @click="validateDialog = false">Đóng</v-btn>
          <v-btn color="primary" :loading="saving" @click="validateCard">Kiểm tra</v-btn>
        </v-card-actions>
      </v-card>
    </v-dialog>
  </div>
</template>

<script setup>
import { computed, onMounted, ref } from 'vue'
import LibraryCardItem from '../components/LibraryCardItem.vue'
import { cardService } from '../services/cardService'
import { userService } from '../services/userService'
import { useUiStore } from '../stores/ui'

const ui = useUiStore()
const cards = ref([])
const users = ref([])
const loading = ref(true)
const saving = ref(false)
const issueDialog = ref(false)
const validateDialog = ref(false)
const selectedReaderId = ref(null)
const validateReaderId = ref(null)
const validateResult = ref(null)

const normalizedCards = computed(() => cards.value.map(card => ({
  id: card.id,
  userId: card.userId,
  cardNumber: card.cardNumber || card.code || 'Không có',
  readerName: card.readerName || card.fullName || 'Độc giả không xác định',
  issuedAt: card.issuedAt || card.createdAt,
  expiredAt: card.expiredAt || card.expiredDate,
  status: card.status || 'Active'
})))

const readerOptions = computed(() => users.value
  .filter(user => user.role === 'Reader')
  .map(user => ({ id: user.id, label: `${user.fullName} (${user.email})` })))

const validateStatusText = computed(() => {
  if (!validateResult.value) return ''
  if (validateResult.value.valid) return 'Thẻ hợp lệ'
  if (validateResult.value.cardStatus === 'Locked') return 'Thẻ bị khóa'
  if (validateResult.value.cardStatus === 'Expired') return 'Thẻ hết hạn'
  return 'Thẻ không hợp lệ'
})

async function load() {
  loading.value = true
  try {
    const [cardRes, userRes] = await Promise.all([cardService.getCards(), userService.getUsers()])
    cards.value = cardRes.data
    users.value = userRes.data
  } catch (error) {
    cards.value = []
    ui.notify(error.response?.data?.message || 'Không tải được danh sách thẻ', 'error')
  } finally {
    loading.value = false
  }
}

function openIssueDialog() {
  selectedReaderId.value = readerOptions.value[0]?.id ?? null
  issueDialog.value = true
}

function openValidateDialog() {
  validateReaderId.value = readerOptions.value[0]?.id ?? null
  validateResult.value = null
  validateDialog.value = true
}

async function createCard() {
  if (!selectedReaderId.value) {
    ui.notify('Vui lòng chọn độc giả', 'warning')
    return
  }
  saving.value = true
  try {
    await cardService.createCard({ userId: selectedReaderId.value })
    ui.notify('Thêm mới thành công')
    issueDialog.value = false
    await load()
  } catch (error) {
    ui.notify(error.response?.data?.message || 'Không tạo được thẻ', 'error')
  } finally {
    saving.value = false
  }
}

async function validateCard() {
  if (!validateReaderId.value) {
    ui.notify('Vui lòng chọn độc giả', 'warning')
    return
  }
  saving.value = true
  try {
    validateResult.value = (await cardService.validateCard(validateReaderId.value)).data
  } catch (error) {
    ui.notify(error.response?.data?.message || 'Không kiểm tra được thẻ', 'error')
  } finally {
    saving.value = false
  }
}

async function renewCard(id) {
  saving.value = true
  try {
    await cardService.renewCard(id)
    ui.notify('Cập nhật thành công')
    await load()
  } catch (error) {
    ui.notify(error.response?.data?.message || 'Không gia hạn được thẻ', 'error')
  } finally {
    saving.value = false
  }
}

async function lockCard(id) {
  saving.value = true
  try {
    await cardService.lockCard(id)
    ui.notify('Cập nhật thành công')
    await load()
  } catch (error) {
    ui.notify(error.response?.data?.message || 'Không khóa được thẻ', 'error')
  } finally {
    saving.value = false
  }
}

async function unlockCard(id) {
  saving.value = true
  try {
    await cardService.unlockCard(id)
    ui.notify('Cập nhật thành công')
    await load()
  } catch (error) {
    ui.notify(error.response?.data?.message || 'Không mở khóa được thẻ', 'error')
  } finally {
    saving.value = false
  }
}

onMounted(load)
</script>

<style scoped>
</style>
