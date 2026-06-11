<template>
  <section :style="styles.page">
    <header :style="styles.hero">
      <div :style="styles.kicker">Digital Library Management</div>
      <h1 :style="styles.title">Library Management Dashboard</h1>
      <p :style="styles.subtitle">
        Theo dõi hoạt động thư viện, tìm kiếm tài liệu, quản lý phiếu mượn và truy cập nhanh
        các tác vụ quan trọng trong một không gian rõ ràng.
      </p>
    </header>

    <section :style="styles.toolbar">
      <v-text-field
        v-model="search"
        density="comfortable"
        variant="solo-filled"
        flat
        hide-details
        prepend-inner-icon="mdi-magnify"
        placeholder="Tìm sách, tác giả, phiếu mượn..."
      />

      <div :style="styles.actions">
        <v-btn color="primary" prepend-icon="mdi-plus" to="/dashboard/books/new" height="46">
          Thêm sách mới
        </v-btn>
      </div>
    </section>

    <section :style="styles.placeholder">
      <div :style="styles.placeholderIcon">
        <v-icon size="42">mdi-view-dashboard-outline</v-icon>
      </div>
      <h2 :style="styles.placeholderTitle">Quick links and summary will appear here.</h2>
      <p :style="styles.placeholderText">No recent library activity to show.</p>
    </section>
  </section>
</template>

<script setup>
import { ref } from 'vue'

const search = ref('')

const styles = {
  page: {
    display: 'grid',
    gap: '22px',
  },
  hero: {
    padding: '30px',
    border: '1px solid rgba(96,165,250,.16)',
    borderRadius: '26px',
    background: 'linear-gradient(135deg,rgba(37,99,235,.20),rgba(56,189,248,.07)), rgba(15,27,46,.78)',
    boxShadow: '0 24px 80px rgba(0,0,0,.35)',
  },
  kicker: {
    color: '#67e8f9',
    fontSize: '12px',
    fontWeight: '950',
    letterSpacing: '.12em',
    textTransform: 'uppercase',
  },
  title: {
    maxWidth: '780px',
    margin: '8px 0 12px',
    color: '#fff',
    fontSize: 'clamp(2rem,4vw,3.7rem)',
    fontWeight: '950',
    lineHeight: '1',
  },
  subtitle: {
    maxWidth: '790px',
    color: '#aebed3',
    fontSize: '16px',
    lineHeight: '1.72',
  },
  toolbar: {
    display: 'grid',
    gap: '16px',
    padding: '18px',
    border: '1px solid rgba(96,165,250,.16)',
    borderRadius: '22px',
    background: 'rgba(8,19,35,.78)',
  },
  actions: {
    display: 'flex',
    justifyContent: 'flex-start',
  },
  placeholder: {
    minHeight: '430px',
    display: 'grid',
    placeItems: 'center',
    alignContent: 'center',
    gap: '12px',
    padding: '34px',
    color: '#aebed3',
    textAlign: 'center',
    border: '1px dashed rgba(96,165,250,.26)',
    borderRadius: '26px',
    background: 'linear-gradient(rgba(96,165,250,.04) 1px, transparent 1px), linear-gradient(90deg, rgba(96,165,250,.04) 1px, transparent 1px), rgba(8,19,35,.62)',
    backgroundSize: '42px 42px',
  },
  placeholderIcon: {
    width: '76px',
    height: '76px',
    display: 'grid',
    placeItems: 'center',
    color: '#38bdf8',
    border: '1px solid rgba(96,165,250,.24)',
    borderRadius: '24px',
    background: 'rgba(56,189,248,.12)',
  },
  placeholderTitle: {
    color: '#fff',
    fontSize: '20px',
    fontWeight: '950',
  },
  placeholderText: {
    color: '#93a4bd',
  },
}
</script>
