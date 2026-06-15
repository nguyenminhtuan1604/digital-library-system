import { createApp } from 'vue'
import { createPinia } from 'pinia'
import { createVuetify } from 'vuetify'
import * as components from 'vuetify/components'
import * as directives from 'vuetify/directives'
import '@mdi/font/css/materialdesignicons.css'
import 'vuetify/styles'
import App from './App.vue'
import router from './router'
import './assets/main.css'

const vuetify = createVuetify({
  components,
  directives,
  theme: {
    defaultTheme: 'darkLibrary',
    themes: {
      darkLibrary: {
        dark: true,
        colors: {
          primary: '#38BDF8',
          secondary: '#172554',
          accent: '#2563EB',
          success: '#22C55E',
          warning: '#F59E0B',
          error: '#F87171',
          info: '#60A5FA',
          background: '#07111F',
          surface: '#0F1B2E',
        },
      },
    },
  },
  defaults: {
    VCard: { rounded: 'lg', elevation: 0 },
    VBtn: { rounded: 'lg', class: 'text-none' },
    VTextField: { variant: 'outlined', density: 'compact' },
    VSelect: { variant: 'outlined', density: 'compact' },
    VDataTable: { density: 'comfortable' },
  },
})

const app = createApp(App)
app.use(createPinia())
app.use(router)
app.use(vuetify)
app.mount('#app')
