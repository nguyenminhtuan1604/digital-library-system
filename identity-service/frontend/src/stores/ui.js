import { defineStore } from 'pinia'

export const useUiStore = defineStore('ui', {
  state: () => ({
    drawer: true,
    settings: {
      themeMode: 'Sáng',
      primaryColor: 'Xanh pastel',
      borderRadius: 14,
      collapseSidebar: false,
      enableRealtimeNotification: true
    },
    snackbar: {
      show: false,
      color: 'success',
      text: ''
    }
  }),
  actions: {
    toggleDrawer() {
      this.drawer = !this.drawer
    },
    notify(text, color = 'success') {
      this.snackbar = { show: true, color, text }
    },
    applySettings(settings = {}) {
      this.settings = {
        ...this.settings,
        ...settings
      }

      const colorMap = {
        'Xanh pastel': '#60A5FA',
        'Xanh dương': '#3B82F6',
        'Tím': '#A78BFA',
        'Xanh lá': '#22C55E'
      }
      const primary = colorMap[this.settings.primaryColor] || '#60A5FA'
      const radius = Number(this.settings.borderRadius || 14)

      document.documentElement.style.setProperty('--primary', primary)
      document.documentElement.style.setProperty('--primary-hover', primary)
      document.documentElement.style.setProperty('--indigo', primary)
      document.documentElement.style.setProperty('--blue', primary)
      document.documentElement.style.setProperty('--sky', primary)
      document.documentElement.style.setProperty('--app-radius', `${radius}px`)
      document.body.classList.toggle('dark-mode', this.settings.themeMode === 'Tối')
    }
  }
})
