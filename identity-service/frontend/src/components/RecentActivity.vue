<template>
  <v-card class="activity-card">
    <div class="section-head">
      <div>
        <span>Hoạt động</span>
        <h3>Hoạt động gần đây</h3>
      </div>
      <v-chip color="primary" variant="tonal" size="small">Mới nhất</v-chip>
    </div>

    <div class="activity-list">
      <div v-for="activity in items" :key="`${activity.title}-${activity.time}`" class="activity-item">
        <div class="activity-dot" :style="{ color: activity.color, background: activity.bg }">
          <v-icon :icon="activity.icon" size="20" />
        </div>
        <div>
          <strong>{{ activity.title }}</strong>
          <p>{{ activity.text }}</p>
        </div>
        <time>{{ activity.time }}</time>
      </div>
    </div>
  </v-card>
</template>

<script setup>
defineProps({
  items: {
    type: Array,
    default: () => []
  }
})
</script>

<style scoped>
</style>
