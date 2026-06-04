<template>
  <v-card class="stat-card" :class="tone">
    <div class="stat-icon">
      <v-icon :icon="icon" size="24" />
    </div>
    <div class="stat-copy">
      <p>{{ label }}</p>
      <h3>{{ value }}</h3>
      <span v-if="hint">{{ hint }}</span>
    </div>
  </v-card>
</template>

<script setup>
defineProps({
  label: { type: String, required: true },
  value: { type: [String, Number], required: true },
  icon: { type: String, required: true },
  hint: { type: String, default: '' },
  tone: { type: String, default: 'blue' }
})
</script>

<style scoped>
</style>
