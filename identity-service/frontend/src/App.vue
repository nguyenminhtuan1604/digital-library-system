<template>
  <v-app>
    <component :is="layout">
      <router-view v-slot="{ Component }">
        <transition name="page-fade" mode="out-in">
          <component :is="Component" />
        </transition>
      </router-view>
    </component>
  </v-app>
</template>

<script setup>
import { computed } from 'vue'
import { useRoute } from 'vue-router'
import AdminLayout from './layouts/AdminLayout.vue'

const route = useRoute()
const layout = computed(() => (route.meta.public ? 'div' : AdminLayout))
</script>

<style scoped>
</style>
