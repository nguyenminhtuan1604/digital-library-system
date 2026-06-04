<template>
  <div class="admin-shell" :class="{ 'settings-dark-mode': ui.settings.themeMode === 'Tối' }">
    <AppSidebar />
    <AppHeader />

    <v-main class="admin-main">
      <div class="content-wrap">
        <slot />
      </div>
    </v-main>

    <v-snackbar v-model="ui.snackbar.show" :color="ui.snackbar.color" location="top right" timeout="2600">
      {{ ui.snackbar.text }}
    </v-snackbar>
  </div>
</template>

<script setup>
import AppHeader from '../components/AppHeader.vue'
import AppSidebar from '../components/AppSidebar.vue'
import { useUiStore } from '../stores/ui'

const ui = useUiStore()
</script>

<style scoped>
</style>
