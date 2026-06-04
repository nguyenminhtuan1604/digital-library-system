import { createApp } from 'vue'
import { createPinia } from 'pinia'
import { createVuetify } from 'vuetify'
import * as components from 'vuetify/components'
import * as directives from 'vuetify/directives'
import { vi } from 'vuetify/locale'
import 'vuetify/styles'
import '@mdi/font/css/materialdesignicons.css'
import App from './App.vue'
import router from './router'
import './style.css'

const vuetify = createVuetify({
  components,
  directives,
  theme: {
    defaultTheme: 'digitalLight',
    themes: {
      digitalLight: {
        dark: false,
        colors: {
          primary: '#60A5FA',
          secondary: '#60A5FA',
          accent: '#DBEAFE',
          background: '#F8FAFC',
          surface: '#ffffff',
          error: '#ef4444',
          success: '#22C55E',
          warning: '#f59e0b',
          info: '#60A5FA'
        }
      }
    }
  },
  locale: {
    locale: 'vi',
    fallback: 'vi',
    messages: { vi }
  },
  defaults: {
    VBtn: { rounded: 'lg', elevation: 0 },
    VCard: { rounded: 'xl', elevation: 0 },
    VTextField: { variant: 'outlined', density: 'comfortable', color: 'primary' },
    VSelect: { variant: 'outlined', density: 'comfortable', color: 'primary' },
    VDataTable: { density: 'comfortable' }
  }
})

createApp(App).use(createPinia()).use(router).use(vuetify).mount('#app')
