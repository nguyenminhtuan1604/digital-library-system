<template>
  <v-card class="data-panel">
    <div class="section-head">
      <div>
        <span>Sách</span>
        <h3>Top sách được mượn nhiều</h3>
      </div>
      <v-chip color="primary" variant="tonal" size="small">Top {{ books.length }}</v-chip>
    </div>

    <div class="top-books-list">
      <div v-for="(book, index) in books" :key="book.bookId || index" class="top-book-item">
        <div class="book-rank">#{{ index + 1 }}</div>
        <div class="book-cover">
          <v-icon icon="mdi-book-open-page-variant-outline" size="22" />
        </div>
        <div class="book-info">
          <strong>{{ book.bookTitle }}</strong>
          <span>{{ book.bookId }}</span>
        </div>
        <div class="borrow-count">
          <strong>{{ book.totalBorrowed }}</strong>
          <span>lượt mượn</span>
        </div>
      </div>
    </div>
  </v-card>
</template>

<script setup>
defineProps({
  books: {
    type: Array,
    default: () => []
  }
})
</script>

<style scoped>
</style>
