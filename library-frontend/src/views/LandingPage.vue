<template>
  <v-app :class="['home-page', { 'dark-mode': isDarkMode }]">
    <!-- SNACKBAR -->
    <v-snackbar v-model="snackbar.show" :color="snackbar.color" timeout="2200" location="top right">
      {{ snackbar.text }}
    </v-snackbar>

    <!-- HEADER -->
    <v-app-bar flat height="84" class="px-md-10 px-4 border-b app-header">
      <div class="d-flex align-center cursor-pointer" @click="scrollToSection('home')">
        <v-icon size="32" color="#0d9488" class="mr-2">mdi-book-open-page-variant</v-icon>
        <div>
          <div class="text-h6 font-weight-black brand-text">Thư viện số</div>
          <div class="text-caption text-grey-darken-1 font-weight-medium" style="line-height: 1;">Digital Library</div>
        </div>
      </div>

      <v-spacer />

      <div class="d-none d-md-flex align-center nav-links">
        <a
          v-for="item in navItems"
          :key="item.id"
          href="#"
          :class="{ active: activeSection === item.id }"
          @click.prevent="scrollToSection(item.id)"
        >
          {{ item.title }}
        </a>
      </div>

      <v-spacer />

      <div class="d-flex align-center ga-3">
        <v-btn
          :icon="isDarkMode ? 'mdi-weather-night' : 'mdi-white-balance-sunny'"
          variant="text"
          color="grey-darken-2"
          size="small"
          @click="toggleTheme"
        />

        <v-btn
          variant="outlined"
          color="#0d9488"
          class="text-none font-weight-bold rounded-lg px-6 d-none d-sm-flex"
          height="42"
          @click="goToLogin"
        >
          Đăng nhập
        </v-btn>

        <v-btn
          color="#0d9488"
          variant="flat"
          class="text-white text-none font-weight-bold rounded-lg px-6 d-none d-sm-flex"
          height="42"
          @click="goToRegister"
        >
          Đăng ký
        </v-btn>

        <v-btn
          icon="mdi-menu"
          variant="text"
          class="d-md-none"
          @click="mobileMenu = true"
        />
      </div>
    </v-app-bar>

    <!-- MOBILE MENU -->
    <v-navigation-drawer v-model="mobileMenu" temporary location="right" width="290">
      <div class="pa-5">
        <div class="d-flex align-center mb-6">
          <v-icon size="30" color="#0d9488" class="mr-2">mdi-book-open-page-variant</v-icon>
          <div>
            <div class="font-weight-black">Thư viện số</div>
            <div class="text-caption text-grey">Digital Library</div>
          </div>
        </div>

        <v-list>
          <v-list-item
            v-for="item in navItems"
            :key="item.id"
            :title="item.title"
            :active="activeSection === item.id"
            rounded="lg"
            @click="scrollMobile(item.id)"
          />
        </v-list>

        <v-btn block color="#0d9488" class="text-white text-none font-weight-bold mt-4" @click="goToRegister">
          Đăng ký ngay
        </v-btn>
      </div>
    </v-navigation-drawer>

    <v-main>
      <!-- HERO -->
      <section id="home" class="hero-section">
        <v-container fluid class="px-md-16 py-12 py-md-16">
          <v-row align="center">
            <v-col cols="12" md="6" class="pr-md-10">
              <v-chip color="#0d9488" variant="tonal" size="small" class="font-weight-bold mb-6 px-3" prepend-icon="mdi-book-outline">
                Chào mừng đến với Thư viện số
              </v-chip>

              <h1 class="hero-title mb-6">
                Tri thức không giới hạn <br />
                trong <span class="text-teal">tầm tay bạn</span>
              </h1>

              <p class="text-body-1 text-grey-darken-1 mb-8 pr-md-10 hero-desc">
                Thư viện số cung cấp hàng ngàn tài liệu, sách điện tử, luận văn, báo cáo và nhiều nguồn học liệu giá trị khác.
              </p>

              <div class="search-box-wrapper mb-6">
                <input
                  v-model="searchKeyword"
                  type="text"
                  placeholder="Tìm kiếm tài liệu, sách, tác giả..."
                  class="hero-search-input"
                  @keyup.enter="handleSearch"
                />
                <button class="hero-search-btn" @click="handleSearch">
                  <v-icon>mdi-magnify</v-icon>
                </button>
              </div>

              <div class="d-flex align-center ga-4 mb-10 flex-wrap">
                <v-btn
                  color="#0d9488"
                  class="text-white text-none font-weight-bold rounded-lg px-6"
                  height="48"
                  elevation="0"
                  prepend-icon="mdi-compass-outline"
                  @click="scrollToSection('documents')"
                >
                  Khám phá ngay
                </v-btn>

                <v-btn
                  variant="outlined"
                  color="#0d9488"
                  class="text-none font-weight-bold rounded-lg px-6 bg-white"
                  height="48"
                  elevation="0"
                  @click="scrollToSection('about')"
                >
                  Tìm hiểu thêm
                  <v-icon class="ml-2">mdi-information-outline</v-icon>
                </v-btn>
              </div>

              <div class="d-flex align-center ga-8 ga-md-12 hero-stats flex-wrap">
                <div
                  v-for="stat in heroStats"
                  :key="stat.label"
                  class="d-flex align-center cursor-pointer"
                  @click="showStatDetail(stat)"
                >
                  <v-icon color="#0d9488" size="28" class="mr-3">{{ stat.icon }}</v-icon>
                  <div>
                    <div class="text-h6 font-weight-black text-grey-darken-4">{{ stat.value }}</div>
                    <div class="text-caption text-grey-darken-1 font-weight-medium">{{ stat.label }}</div>
                  </div>
                </div>
              </div>
            </v-col>

            <v-col cols="12" md="6" class="mt-8 mt-md-0">
              <div class="hero-image-wrapper" @click="openHeroPreview">
                <v-img
                  src="https://images.unsplash.com/photo-1499951360447-b19be8fe80f5?auto=format&fit=crop&q=80&w=1200"
                  cover
                  class="rounded-xl shadow-lg hero-img"
                  height="450"
                >
                  <template #placeholder>
                    <div class="d-flex align-center justify-center fill-height bg-teal-lighten-5">
                      <v-progress-circular indeterminate color="#0d9488" />
                    </div>
                  </template>
                </v-img>

                <div class="image-badge">
                  <v-icon size="18">mdi-cursor-default-click-outline</v-icon>
                  Xem trước hệ thống
                </div>
              </div>
            </v-col>
          </v-row>
        </v-container>
      </section>

      <!-- FEATURES -->
      <section id="about" class="bg-grey-lighten-4">
        <v-container fluid class="px-md-16 py-16">
          <div class="text-center mb-12">
            <v-chip color="#0d9488" variant="tonal" size="small" class="font-weight-bold mb-3">
              TÍNH NĂNG NỔI BẬT
            </v-chip>
            <h2 class="text-h4 font-weight-black text-grey-darken-4 mb-3">
              Hệ thống hiện đại – Trải nghiệm thông minh
            </h2>
            <p class="text-body-2 text-grey-darken-1">
              Được thiết kế để giúp bạn tìm kiếm, quản lý và khai thác tri thức một cách dễ dàng nhất.
            </p>
          </div>

          <v-row>
            <v-col v-for="feature in features" :key="feature.title" cols="12" sm="6" lg="3">
              <v-card
                class="pa-6 rounded-xl h-100 feature-card text-center text-md-left"
                elevation="0"
                @click="openFeature(feature)"
              >
                <v-avatar color="#f0fdfa" size="64" class="mb-5 mx-auto mx-md-0 d-flex">
                  <v-icon :icon="feature.icon" color="#0d9488" size="30" />
                </v-avatar>
                <h3 class="text-subtitle-1 font-weight-bold text-grey-darken-4 mb-2">{{ feature.title }}</h3>
                <p class="text-body-2 text-grey-darken-1 feature-desc">{{ feature.desc }}</p>
                <v-btn variant="text" color="#0d9488" class="text-none px-0 mt-2">
                  Xem chi tiết
                  <v-icon size="16" class="ml-1">mdi-chevron-right</v-icon>
                </v-btn>
              </v-card>
            </v-col>
          </v-row>
        </v-container>
      </section>

      <!-- STATS -->
      <section class="stats-section py-16 px-md-16 text-center text-white">
        <v-chip color="white" variant="outlined" size="small" class="font-weight-bold mb-4 border-opacity-50">
          THỐNG KÊ ẤN TƯỢNG
        </v-chip>
        <h2 class="text-h4 font-weight-black mb-3">Những con số biết nói</h2>
        <p class="text-body-2 text-teal-lighten-4 mb-12">Cùng nhìn lại hành trình phát triển của thư viện số.</p>

        <v-row justify="center" class="stats-row">
          <v-col cols="6" md="3" v-for="stat in systemStats" :key="stat.label">
            <div class="stat-item" @click="showStatDetail(stat)">
              <div class="stat-circle mx-auto mb-4">
                <v-icon :icon="stat.icon" size="32" color="white" />
              </div>
              <div class="text-h3 font-weight-black mb-1">{{ stat.value }}</div>
              <div class="text-caption font-weight-medium text-teal-lighten-4">{{ stat.label }}</div>
            </div>
          </v-col>
        </v-row>
      </section>

      <!-- DOCUMENTS -->
      <section id="documents">
        <v-container fluid class="px-md-16 py-16">
          <div class="d-flex justify-space-between align-end mb-8 flex-wrap ga-3">
            <div>
              <v-chip color="#0d9488" variant="text" size="small" class="font-weight-bold px-0 mb-1">
                TÀI LIỆU NỔI BẬT
              </v-chip>
              <h2 class="text-h4 font-weight-black text-grey-darken-4">Khám phá tài liệu được quan tâm nhất</h2>
            </div>

            <a href="#" class="text-teal-darken-2 font-weight-bold text-decoration-none text-body-2 d-flex align-center" @click.prevent="openAllBooks">
              Xem tất cả
              <v-icon size="16" class="ml-1">mdi-chevron-right</v-icon>
            </a>
          </div>

          <div class="books-slider position-relative">
            <v-btn
              icon="mdi-chevron-left"
              class="slider-btn left d-none d-md-flex"
              size="small"
              color="white"
              elevation="2"
              @click="scrollBooks('left')"
            />

            <div ref="booksScroller" class="d-flex ga-6 overflow-x-auto hide-scrollbar pb-4">
              <v-card
                v-for="book in topBooks"
                :key="book.title"
                width="220"
                min-width="220"
                class="rounded-xl border book-card"
                elevation="0"
                @click="openBook(book)"
              >
                <v-img :src="book.cover" height="280" cover class="align-end">
                  <template #placeholder>
                    <div class="d-flex align-center justify-center fill-height bg-grey-lighten-3">
                      <v-icon size="40" color="grey-lighten-1">mdi-image-outline</v-icon>
                    </div>
                  </template>
                </v-img>

                <div class="pa-4">
                  <h3 class="text-subtitle-2 font-weight-bold text-truncate mb-1">{{ book.title }}</h3>
                  <p class="text-caption text-grey-darken-1 mb-2">{{ book.author }}</p>
                  <div class="d-flex justify-space-between align-center">
                    <div class="d-flex align-center text-caption font-weight-bold text-grey-darken-3">
                      <v-icon color="amber-darken-1" size="16" class="mr-1">mdi-star</v-icon>
                      {{ book.rating }}
                    </div>
                    <div class="text-caption text-grey-darken-1">
                      <v-icon size="14" class="mr-1">mdi-book-open-outline</v-icon>{{ book.borrows }} lượt mượn
                    </div>
                  </div>
                </div>
              </v-card>
            </div>

            <v-btn
              icon="mdi-chevron-right"
              class="slider-btn right d-none d-md-flex"
              size="small"
              color="white"
              elevation="2"
              @click="scrollBooks('right')"
            />
          </div>
        </v-container>
      </section>

      <!-- CATEGORIES -->
      <section id="categories" class="bg-grey-lighten-4">
        <v-container fluid class="px-md-16 py-16">
          <div class="text-center mb-10">
            <v-chip color="#0d9488" variant="tonal" size="small" class="font-weight-bold mb-3">
              THỂ LOẠI TÀI LIỆU
            </v-chip>
            <h2 class="text-h4 font-weight-black text-grey-darken-4">Danh mục học liệu phổ biến</h2>
          </div>

          <v-row>
            <v-col v-for="category in categories" :key="category.name" cols="12" sm="6" md="3">
              <v-card class="pa-5 rounded-xl border category-card" elevation="0" @click="filterByCategory(category)">
                <div class="d-flex align-center">
                  <v-avatar color="#f0fdfa" size="54" class="mr-4">
                    <v-icon color="#0d9488" size="26">{{ category.icon }}</v-icon>
                  </v-avatar>
                  <div>
                    <div class="font-weight-black text-grey-darken-4">{{ category.name }}</div>
                    <div class="text-caption text-grey">{{ category.count }} tài liệu</div>
                  </div>
                </div>
              </v-card>
            </v-col>
          </v-row>
        </v-container>
      </section>

      <!-- REVIEWS / NEWS -->
      <section id="news" class="bg-grey-lighten-5">
        <v-container fluid class="px-md-16 py-10">
          <div class="d-flex justify-space-between align-end mb-8 flex-wrap ga-3">
            <div>
              <v-chip color="#0d9488" variant="text" size="small" class="font-weight-bold px-0 mb-1">
                ĐÁNH GIÁ TỪ ĐỘC GIẢ
              </v-chip>
              <h2 class="text-h4 font-weight-black text-grey-darken-4">Khách hàng nói gì về chúng tôi</h2>
            </div>

            <a href="#" class="text-teal-darken-2 font-weight-bold text-decoration-none text-body-2 d-flex align-center" @click.prevent="showAllReviews">
              Xem tất cả
              <v-icon size="16" class="ml-1">mdi-chevron-right</v-icon>
            </a>
          </div>

          <v-row>
            <v-col cols="12" md="4" v-for="review in visibleReviews" :key="review.name">
              <v-card class="pa-6 rounded-xl border h-100 review-card" elevation="0" @click="openReview(review)">
                <div class="d-flex justify-space-between align-start mb-4">
                  <div class="d-flex align-center">
                    <v-avatar size="48" class="mr-3">
                      <v-img :src="review.avatar" />
                    </v-avatar>
                    <div>
                      <div class="font-weight-bold text-grey-darken-4">{{ review.name }}</div>
                      <div class="text-caption text-grey-darken-1">{{ review.role }}</div>
                    </div>
                  </div>
                  <div class="d-flex text-teal-darken-1">
                    <v-icon size="14" v-for="i in 5" :key="i">mdi-star</v-icon>
                  </div>
                </div>
                <p class="text-body-2 text-grey-darken-3 review-text">"{{ review.text }}"</p>
              </v-card>
            </v-col>
          </v-row>

          <div class="d-flex justify-center mt-6 ga-2">
            <span
              v-for="index in reviewPages"
              :key="index"
              class="carousel-dot"
              :class="{ active: reviewPage === index - 1 }"
              @click="reviewPage = index - 1"
            ></span>
          </div>
        </v-container>
      </section>

      <!-- CTA -->
      <v-container fluid class="px-md-16 py-12">
        <v-card class="cta-banner rounded-xl overflow-hidden" elevation="10">
          <v-row no-gutters align="center">
            <v-col cols="12" md="3" class="d-none d-md-block h-100">
              <v-img src="https://images.unsplash.com/photo-1456953180671-730de08edaa7?auto=format&fit=crop&q=80&w=800" cover height="100%" />
            </v-col>

            <v-col cols="12" md="6" class="pa-8 pa-md-12 text-center text-md-left text-white">
              <h2 class="text-h4 font-weight-black mb-3">Bắt đầu hành trình khám phá tri thức ngay hôm nay!</h2>
              <p class="text-teal-lighten-4 mb-0">Đăng ký tài khoản miễn phí để trải nghiệm tất cả tính năng của thư viện số.</p>
            </v-col>

            <v-col cols="12" md="3" class="pa-8 d-flex justify-center justify-md-end">
              <v-btn
                color="#0d9488"
                class="text-white text-none font-weight-bold rounded-lg px-8"
                height="54"
                prepend-icon="mdi-account-plus-outline"
                @click="goToRegister"
              >
                Đăng ký ngay
              </v-btn>
            </v-col>
          </v-row>
        </v-card>
      </v-container>
    </v-main>

    <!-- FOOTER -->
    <v-footer id="contact" class="footer-section px-md-16 py-12 text-white d-block">
      <v-row>
        <v-col cols="12" md="3" class="pr-md-8">
          <div class="d-flex align-center mb-4">
            <v-icon size="28" color="white" class="mr-2">mdi-book-open-page-variant</v-icon>
            <div>
              <div class="text-h6 font-weight-black" style="line-height: 1.1;">Thư viện số</div>
              <div class="text-caption text-teal-lighten-4" style="line-height: 1;">Digital Library</div>
            </div>
          </div>
          <p class="text-caption text-teal-lighten-5 footer-desc">
            Nền tảng thư viện số hiện đại, cung cấp nguồn tài liệu phong phú và trải nghiệm thông minh cho mọi người.
          </p>
        </v-col>

        <v-col cols="6" md="2">
          <h4 class="font-weight-bold mb-4 text-uppercase text-caption text-teal-lighten-3">Thư viện</h4>
          <div class="footer-links">
            <a href="#" @click.prevent="scrollToSection('documents')">Tài liệu</a>
            <a href="#" @click.prevent="scrollToSection('categories')">Thể loại</a>
            <a href="#" @click.prevent="goToRegister">Thẻ thư viện</a>
            <a href="#" @click.prevent="showGuide">Hướng dẫn mượn trả</a>
          </div>
        </v-col>

        <v-col cols="6" md="3">
          <h4 class="font-weight-bold mb-4 text-uppercase text-caption text-teal-lighten-3">Hỗ trợ</h4>
          <div class="footer-links">
            <a href="#" @click.prevent="openHelp('faq')">Câu hỏi thường gặp</a>
            <a href="#" @click.prevent="openHelp('guide')">Hướng dẫn sử dụng</a>
            <a href="#" @click.prevent="openHelp('privacy')">Chính sách bảo mật</a>
            <a href="#" @click.prevent="openHelp('terms')">Điều khoản dịch vụ</a>
          </div>
        </v-col>

        <v-col cols="12" md="4">
          <h4 class="font-weight-bold mb-4 text-uppercase text-caption text-teal-lighten-3">Liên hệ</h4>
          <div class="d-flex align-start mb-3 text-caption text-teal-lighten-5">
            <v-icon size="18" class="mr-2 mt-n1 text-teal-lighten-3">mdi-map-marker-outline</v-icon>
            123 Đường Tri Thức, Quận 1, TP. Hồ Chí Minh
          </div>
          <div class="d-flex align-center mb-3 text-caption text-teal-lighten-5">
            <v-icon size="18" class="mr-2 text-teal-lighten-3">mdi-phone-outline</v-icon>
            (028) 1234 5678
          </div>
          <div class="d-flex align-center text-caption text-teal-lighten-5">
            <v-icon size="18" class="mr-2 text-teal-lighten-3">mdi-email-outline</v-icon>
            support@thuvienso.edu.vn
          </div>
        </v-col>
      </v-row>

      <v-divider class="my-6 border-opacity-25" color="teal-lighten-3" />
      <div class="text-center text-caption text-teal-lighten-4">
        &copy; 2026 Library System. Bản quyền thuộc về Nhóm Phát Triển.
      </div>
    </v-footer>

    <!-- BOOK DETAIL DIALOG -->
    <v-dialog v-model="bookDialog" max-width="780">
      <v-card class="rounded-xl pa-0 overflow-hidden">
        <v-row no-gutters>
          <v-col cols="12" md="4">
            <v-img :src="selectedBook?.cover" height="100%" min-height="360" cover />
          </v-col>

          <v-col cols="12" md="8">
            <div class="pa-6">
              <div class="d-flex justify-space-between align-center mb-3">
                <v-chip color="#0d9488" variant="tonal" class="font-weight-bold">
                  Tài liệu nổi bật
                </v-chip>
                <v-btn icon="mdi-close" variant="text" @click="bookDialog = false" />
              </div>

              <h2 class="text-h5 font-weight-black mb-2">{{ selectedBook?.title }}</h2>
              <p class="text-grey-darken-1 mb-3">Tác giả: {{ selectedBook?.author }}</p>

              <div class="d-flex align-center ga-4 mb-5">
                <span class="font-weight-bold">
                  <v-icon color="amber" size="18">mdi-star</v-icon>
                  {{ selectedBook?.rating }}
                </span>
                <span class="text-grey">
                  <v-icon size="17">mdi-book-open-outline</v-icon>
                  {{ selectedBook?.borrows }} lượt mượn
                </span>
              </div>

              <p class="text-body-2 text-grey-darken-2 mb-6">
                Tài liệu hỗ trợ học tập, nghiên cứu và tham khảo chuyên sâu. Bạn có thể thêm vào danh sách yêu thích hoặc đăng nhập để mượn trực tuyến.
              </p>

              <div class="d-flex ga-3 flex-wrap">
                <v-btn color="#0d9488" class="text-white text-none font-weight-bold" prepend-icon="mdi-bookmark-plus-outline" @click="borrowSelectedBook">
                  Mượn tài liệu
                </v-btn>

                <v-btn variant="outlined" color="#0d9488" class="text-none font-weight-bold" prepend-icon="mdi-heart-outline" @click="addFavorite(selectedBook)">
                  Yêu thích
                </v-btn>
              </div>
            </div>
          </v-col>
        </v-row>
      </v-card>
    </v-dialog>

    <!-- LIST DIALOG -->
    <v-dialog v-model="listDialog" max-width="950">
      <v-card class="rounded-xl pa-6">
        <div class="d-flex justify-space-between align-center mb-5">
          <h2 class="text-h6 font-weight-black">{{ listDialogTitle }}</h2>
          <v-btn icon="mdi-close" variant="text" @click="listDialog = false" />
        </div>

        <v-row>
          <v-col v-for="book in listBooks" :key="book.title" cols="12" sm="6" md="4">
            <v-card class="rounded-xl border h-100" elevation="0" @click="openBook(book)">
              <v-img :src="book.cover" height="190" cover />
              <div class="pa-4">
                <h3 class="text-subtitle-2 font-weight-bold">{{ book.title }}</h3>
                <p class="text-caption text-grey">{{ book.author }}</p>
              </div>
            </v-card>
          </v-col>
        </v-row>
      </v-card>
    </v-dialog>

    <!-- INFO DIALOG -->
    <v-dialog v-model="infoDialog" max-width="650">
      <v-card class="rounded-xl pa-6">
        <div class="d-flex justify-space-between align-center mb-4">
          <h2 class="text-h6 font-weight-black">{{ infoTitle }}</h2>
          <v-btn icon="mdi-close" variant="text" @click="infoDialog = false" />
        </div>

        <p class="text-body-1 text-grey-darken-2 mb-5" style="line-height: 1.7;">
          {{ infoText }}
        </p>

        <v-btn color="#0d9488" class="text-white text-none font-weight-bold" @click="infoDialog = false">
          Đã hiểu
        </v-btn>
      </v-card>
    </v-dialog>

    <!-- HERO PREVIEW DIALOG -->
    <v-dialog v-model="heroPreviewDialog" max-width="920">
      <v-card class="rounded-xl overflow-hidden">
        <v-img src="https://images.unsplash.com/photo-1499951360447-b19be8fe80f5?auto=format&fit=crop&q=80&w=1200" height="520" cover />
      </v-card>
    </v-dialog>
  </v-app>
</template>

<script setup>
import { computed, ref } from 'vue'
import { useRouter } from 'vue-router'

const router = useRouter()

const isDarkMode = ref(false)
const mobileMenu = ref(false)
const activeSection = ref('home')
const searchKeyword = ref('')
const booksScroller = ref(null)

const snackbar = ref({
  show: false,
  text: '',
  color: '#0d9488'
})

const bookDialog = ref(false)
const selectedBook = ref(null)

const listDialog = ref(false)
const listDialogTitle = ref('Danh sách tài liệu')
const listBooks = ref([])

const infoDialog = ref(false)
const infoTitle = ref('')
const infoText = ref('')

const heroPreviewDialog = ref(false)
const reviewPage = ref(0)

const navItems = [
  { id: 'home', title: 'Trang chủ' },
  { id: 'about', title: 'Giới thiệu' },
  { id: 'documents', title: 'Tài liệu' },
  { id: 'categories', title: 'Thể loại' },
  { id: 'news', title: 'Tin tức' },
  { id: 'contact', title: 'Liên hệ' }
]

const heroStats = [
  { icon: 'mdi-book-multiple-outline', value: '10K+', label: 'Tài liệu', detail: 'Kho tài liệu số gồm sách, báo cáo, luận văn và tài liệu học tập.' },
  { icon: 'mdi-account-group-outline', value: '5K+', label: 'Độc giả', detail: 'Hệ thống đang phục vụ hơn 5.000 độc giả thường xuyên.' },
  { icon: 'mdi-swap-horizontal', value: '20K+', label: 'Lượt mượn', detail: 'Quy trình mượn trả được số hóa, nhanh chóng và dễ theo dõi.' }
]

const features = [
  {
    icon: 'mdi-book-open-blank-variant',
    title: 'Kho tài liệu phong phú',
    desc: 'Hàng ngàn sách, tài liệu đa dạng nhiều lĩnh vực khác nhau, được cập nhật liên tục.',
    detail: 'Người dùng có thể tìm kiếm tài liệu theo tên sách, tác giả, thể loại và xem thông tin chi tiết trước khi mượn.'
  },
  {
    icon: 'mdi-view-grid-outline',
    title: 'Quản lý thông minh',
    desc: 'Đăng ký thẻ thư viện trực tuyến, quản lý thông tin và lịch sử mượn trả dễ dàng.',
    detail: 'Hệ thống hỗ trợ hồ sơ độc giả, trạng thái thẻ, hạn thẻ, lịch sử mượn trả và cảnh báo quá hạn.'
  },
  {
    icon: 'mdi-swap-horizontal',
    title: 'Mượn – Trả linh hoạt',
    desc: 'Quy trình mượn trả đơn giản, nhanh chóng với lịch nhắc và gia hạn trực tuyến.',
    detail: 'Độc giả có thể theo dõi sách đang mượn, hạn trả, tình trạng quá hạn và yêu cầu gia hạn.'
  },
  {
    icon: 'mdi-chart-box-outline',
    title: 'Báo cáo & Thống kê',
    desc: 'Hệ thống báo cáo chi tiết, giúp quản lý dữ liệu hiệu quả và chính xác nhất.',
    detail: 'Dashboard báo cáo tổng hợp lượt mượn, lượt trả, top sách phổ biến và dữ liệu đồng bộ từ event.'
  }
]

const systemStats = [
  { icon: 'mdi-account-outline', value: '8.450+', label: 'Độc giả đăng ký', detail: 'Số lượng độc giả đã tạo tài khoản và sử dụng hệ thống.' },
  { icon: 'mdi-tablet-cellphone', value: '12.600+', label: 'Tài liệu số', detail: 'Tài liệu đang có trong kho học liệu số.' },
  { icon: 'mdi-swap-horizontal', value: '24.300+', label: 'Lượt mượn', detail: 'Tổng số lượt mượn và trả sách trên hệ thống.' },
  { icon: 'mdi-eye-outline', value: '156.000+', label: 'Lượt truy cập', detail: 'Lượt truy cập vào nền tảng thư viện số.' }
]

const topBooks = [
  { cover: 'https://images.unsplash.com/photo-1620712943543-bcc4688e7485?w=400&q=80', title: 'Trí tuệ nhân tạo', author: 'Phạm Minh Tuấn', rating: '4.8', borrows: '12.1K', category: 'Công nghệ' },
  { cover: 'https://images.unsplash.com/photo-1526379095098-d400fd0bf935?w=400&q=80', title: 'Lập trình Python cơ bản', author: 'Nguyễn Văn An', rating: '4.7', borrows: '8.5K', category: 'Công nghệ' },
  { cover: 'https://images.unsplash.com/photo-1542744173-8e7e53415bb0?w=400&q=80', title: 'Marketing hiện đại', author: 'Trần Thị Mai', rating: '4.6', borrows: '6.4K', category: 'Kinh tế' },
  { cover: 'https://images.unsplash.com/photo-1454165804606-c3d57bc86b40?w=400&q=80', title: 'Quản trị dự án', author: 'Lê Hoàng Nam', rating: '4.2', borrows: '5.1K', category: 'Quản trị' },
  { cover: 'https://images.unsplash.com/photo-1544928147-79a2dbc1f389?w=400&q=80', title: 'Tâm lý học ứng dụng', author: 'Đỗ Thu Hương', rating: '4.7', borrows: '7.2K', category: 'Tâm lý' },
  { cover: 'https://images.unsplash.com/photo-1606240724602-5b21f896eae8?w=400&q=80', title: 'Kinh tế học vĩ mô', author: 'Vũ Quốc Bảo', rating: '4.6', borrows: '6.9K', category: 'Kinh tế' }
]

const categories = [
  { name: 'Công nghệ', count: '2.450+', icon: 'mdi-laptop' },
  { name: 'Kinh tế', count: '1.820+', icon: 'mdi-chart-line' },
  { name: 'Quản trị', count: '1.120+', icon: 'mdi-briefcase-outline' },
  { name: 'Tâm lý', count: '930+', icon: 'mdi-brain' },
  { name: 'Văn học', count: '2.310+', icon: 'mdi-book-open-variant' },
  { name: 'Ngôn ngữ', count: '850+', icon: 'mdi-translate' },
  { name: 'Khoa học', count: '1.540+', icon: 'mdi-atom' },
  { name: 'Luật', count: '760+', icon: 'mdi-scale-balance' }
]

const reviews = [
  { avatar: 'https://i.pravatar.cc/150?img=47', name: 'Nguyễn Thị Lan', role: 'Sinh viên', text: 'Thư viện số giúp mình tìm được rất nhiều tài liệu học tập chất lượng. Giao diện dễ dùng, tài liệu đa dạng!' },
  { avatar: 'https://i.pravatar.cc/150?img=11', name: 'Trần Minh Quân', role: 'Giảng viên', text: 'Nguồn tài liệu phong phú, cập nhật thường xuyên. Đây là công cụ hỗ trợ giảng dạy và nghiên cứu tuyệt vời.' },
  { avatar: 'https://i.pravatar.cc/150?img=12', name: 'Phạm Thu Hà', role: 'Nghiên cứu sinh', text: 'Tôi có thể truy cập tài liệu mọi lúc mọi nơi. Thư viện số thực sự là người bạn đồng hành không thể thiếu.' },
  { avatar: 'https://i.pravatar.cc/150?img=32', name: 'Đỗ Minh Đức', role: 'Sinh viên', text: 'Tính năng tìm kiếm rất nhanh, sách được phân loại rõ ràng và dễ sử dụng.' },
  { avatar: 'https://i.pravatar.cc/150?img=5', name: 'Lê Thanh Mai', role: 'Thủ thư', text: 'Hệ thống giúp quản lý mượn trả và báo cáo thống kê thuận tiện hơn rất nhiều.' },
  { avatar: 'https://i.pravatar.cc/150?img=20', name: 'Vũ Quốc Bảo', role: 'Giảng viên', text: 'Tôi đánh giá cao khả năng tra cứu và theo dõi tài liệu của hệ thống.' }
]

const reviewPages = computed(() => Math.ceil(reviews.length / 3))

const visibleReviews = computed(() => {
  const start = reviewPage.value * 3
  return reviews.slice(start, start + 3)
})

const showMessage = (text, color = '#0d9488') => {
  snackbar.value = {
    show: true,
    text,
    color
  }
}

const scrollToSection = (sectionId) => {
  activeSection.value = sectionId
  const element = document.getElementById(sectionId)
  if (element) {
    element.scrollIntoView({ behavior: 'smooth', block: 'start' })
  }
}

const scrollMobile = (sectionId) => {
  mobileMenu.value = false
  scrollToSection(sectionId)
}

const toggleTheme = () => {
  isDarkMode.value = !isDarkMode.value
  showMessage(isDarkMode.value ? 'Đã bật chế độ tối.' : 'Đã bật chế độ sáng.')
}

const goToLogin = () => {
  router.push('/login')
}

const goToRegister = () => {
  router.push('/register')
}

const handleSearch = () => {
  const keyword = searchKeyword.value.trim().toLowerCase()

  if (!keyword) {
    showMessage('Bạn hãy nhập tên tài liệu, sách hoặc tác giả cần tìm.', 'orange')
    return
  }

  const results = topBooks.filter((book) => {
    return (
      book.title.toLowerCase().includes(keyword) ||
      book.author.toLowerCase().includes(keyword) ||
      book.category.toLowerCase().includes(keyword)
    )
  })

  if (results.length === 0) {
    showMessage('Không tìm thấy tài liệu phù hợp.', 'red')
    return
  }

  listDialogTitle.value = `Kết quả tìm kiếm: "${searchKeyword.value}"`
  listBooks.value = results
  listDialog.value = true
}

const openFeature = (feature) => {
  infoTitle.value = feature.title
  infoText.value = feature.detail
  infoDialog.value = true
}

const showStatDetail = (stat) => {
  infoTitle.value = stat.label
  infoText.value = stat.detail
  infoDialog.value = true
}

const scrollBooks = (direction) => {
  if (!booksScroller.value) return

  booksScroller.value.scrollBy({
    left: direction === 'left' ? -500 : 500,
    behavior: 'smooth'
  })
}

const openBook = (book) => {
  selectedBook.value = book
  bookDialog.value = true
}

const openAllBooks = () => {
  listDialogTitle.value = 'Tất cả tài liệu nổi bật'
  listBooks.value = topBooks
  listDialog.value = true
}

const filterByCategory = (category) => {
  const results = topBooks.filter((book) => book.category === category.name)

  listDialogTitle.value = `Tài liệu thuộc thể loại: ${category.name}`
  listBooks.value = results.length ? results : topBooks
  listDialog.value = true
}

const borrowSelectedBook = () => {
  bookDialog.value = false
  showMessage('Bạn cần đăng nhập để mượn tài liệu.')
  router.push('/login')
}

const addFavorite = (book) => {
  if (!book) return
  showMessage(`Đã thêm "${book.title}" vào danh sách yêu thích.`)
}

const showAllReviews = () => {
  reviewPage.value = reviewPage.value === reviewPages.value - 1 ? 0 : reviewPage.value + 1
  showMessage('Đã chuyển sang nhóm đánh giá tiếp theo.')
}

const openReview = (review) => {
  infoTitle.value = `Đánh giá của ${review.name}`
  infoText.value = review.text
  infoDialog.value = true
}

const openHeroPreview = () => {
  heroPreviewDialog.value = true
}

const showGuide = () => {
  infoTitle.value = 'Hướng dẫn mượn trả'
  infoText.value = 'Bạn đăng nhập tài khoản, chọn tài liệu cần mượn, kiểm tra hạn trả và xác nhận mượn. Hệ thống sẽ tự động ghi nhận lịch sử mượn trả.'
  infoDialog.value = true
}

const openHelp = (type) => {
  const content = {
    faq: {
      title: 'Câu hỏi thường gặp',
      text: 'Bạn có thể tìm kiếm tài liệu, đăng ký tài khoản, quản lý thẻ thư viện và theo dõi lịch sử mượn trả ngay trên hệ thống.'
    },
    guide: {
      title: 'Hướng dẫn sử dụng',
      text: 'Sử dụng thanh tìm kiếm để tra cứu tài liệu. Bấm vào sách để xem chi tiết. Đăng nhập để mượn tài liệu hoặc quản lý hồ sơ.'
    },
    privacy: {
      title: 'Chính sách bảo mật',
      text: 'Thông tin tài khoản, lịch sử mượn trả và dữ liệu cá nhân được bảo vệ theo chính sách bảo mật của hệ thống.'
    },
    terms: {
      title: 'Điều khoản dịch vụ',
      text: 'Người dùng cần tuân thủ quy định mượn trả, bảo quản tài liệu và không chia sẻ tài khoản cho người khác.'
    }
  }

  infoTitle.value = content[type].title
  infoText.value = content[type].text
  infoDialog.value = true
}
</script>

<style scoped>
* {
  font-family: 'Inter', sans-serif !important;
}

.home-page {
  background: #ffffff;
  color: #0f172a;
}

.dark-mode {
  background: #0f172a;
}

.app-header {
  background: rgba(255, 255, 255, 0.96) !important;
  backdrop-filter: blur(12px);
}

.dark-mode .app-header {
  background: rgba(15, 23, 42, 0.96) !important;
}

.brand-text {
  line-height: 1.1;
  color: #0f172a;
}

.dark-mode .brand-text,
.dark-mode .hero-title,
.dark-mode .text-grey-darken-4 {
  color: #e5e7eb !important;
}

.dark-mode .bg-grey-lighten-4,
.dark-mode .bg-grey-lighten-5 {
  background: #111827 !important;
}

.text-teal {
  color: #0d9488;
}

.bg-teal {
  background-color: #0d9488;
}

/* Navbar */
.nav-links a {
  text-decoration: none;
  color: #475569;
  font-weight: 700;
  font-size: 14px;
  padding: 8px 16px;
  border-radius: 8px;
  transition: all 0.2s;
}

.nav-links a:hover {
  color: #0d9488;
}

.nav-links a.active {
  color: #0d9488;
  position: relative;
}

.nav-links a.active::after {
  content: '';
  position: absolute;
  bottom: 0;
  left: 16px;
  right: 16px;
  height: 3px;
  background-color: #0d9488;
  border-radius: 3px 3px 0 0;
}

/* Hero */
.hero-section {
  background:
    radial-gradient(circle at 15% 0%, rgba(45, 212, 191, 0.2), transparent 28%),
    linear-gradient(135deg, #f0fdfa 0%, #ffffff 54%, #effcf9 100%);
}

.dark-mode .hero-section {
  background:
    radial-gradient(circle at 15% 0%, rgba(13, 148, 136, 0.2), transparent 28%),
    linear-gradient(135deg, #0f172a 0%, #111827 54%, #0b3b35 100%);
}

.hero-title {
  font-size: 3.5rem;
  font-weight: 900;
  line-height: 1.15;
  color: #0f172a;
  letter-spacing: -1px;
}

.hero-desc {
  line-height: 1.6;
}

.search-box-wrapper {
  display: flex;
  background: white;
  border: 1px solid #cbd5e1;
  border-radius: 12px;
  padding: 6px;
  box-shadow: 0 4px 6px -1px rgba(0,0,0,0.05);
  max-width: 500px;
}

.hero-search-input {
  flex-grow: 1;
  border: none;
  outline: none;
  padding: 0 16px;
  font-size: 15px;
  color: #334155;
}

.hero-search-btn {
  background: #0d9488;
  color: white;
  border: none;
  border-radius: 8px;
  width: 44px;
  height: 44px;
  display: grid;
  place-items: center;
  cursor: pointer;
  transition: 0.2s;
}

.hero-search-btn:hover {
  background: #0f766e;
}

.hero-image-wrapper {
  position: relative;
  cursor: pointer;
}

.hero-img {
  transition: 0.25s ease;
}

.hero-image-wrapper:hover .hero-img {
  transform: translateY(-4px);
}

.image-badge {
  position: absolute;
  left: 20px;
  bottom: 20px;
  background: rgba(13, 148, 136, 0.92);
  color: white;
  border-radius: 999px;
  padding: 9px 14px;
  display: inline-flex;
  align-items: center;
  gap: 7px;
  font-weight: 850;
  box-shadow: 0 12px 24px rgba(13, 148, 136, 0.24);
}

/* Features */
.feature-card,
.book-card,
.review-card,
.category-card {
  border: 1px solid #e2e8f0;
  transition: transform 0.3s, box-shadow 0.3s;
  cursor: pointer;
}

.dark-mode .feature-card,
.dark-mode .book-card,
.dark-mode .review-card,
.dark-mode .category-card {
  background: #1f2937 !important;
  border-color: #334155;
}

.feature-card:hover,
.book-card:hover,
.review-card:hover,
.category-card:hover {
  transform: translateY(-5px);
  box-shadow: 0 20px 25px -5px rgba(0, 0, 0, 0.08) !important;
  border-color: #99f6e4;
}

.feature-desc,
.review-text,
.footer-desc {
  line-height: 1.6;
}

/* Stats */
.stats-section {
  background: linear-gradient(135deg, #022c22 0%, #064e40 100%);
  position: relative;
  overflow: hidden;
}

.stats-section::before {
  content: '';
  position: absolute;
  top: -50%;
  left: -10%;
  width: 50%;
  height: 200%;
  background: radial-gradient(circle, rgba(13, 148, 136, 0.1) 0%, transparent 60%);
}

.stats-section::after {
  content: '';
  position: absolute;
  top: -20%;
  right: -20%;
  width: 60%;
  height: 150%;
  border: 1px solid rgba(255,255,255,0.05);
  border-radius: 50%;
}

.stat-item {
  cursor: pointer;
}

.stat-circle {
  width: 72px;
  height: 72px;
  border-radius: 50%;
  border: 1px solid rgba(255,255,255,0.2);
  display: grid;
  place-items: center;
}

.stats-row > div {
  border-right: 1px solid rgba(255,255,255,0.1);
}

.stats-row > div:last-child {
  border-right: none;
}

/* Books Slider */
.hide-scrollbar::-webkit-scrollbar {
  display: none;
}

.hide-scrollbar {
  -ms-overflow-style: none;
  scrollbar-width: none;
  scroll-behavior: smooth;
}

.slider-btn {
  position: absolute;
  top: 40%;
  transform: translateY(-50%);
  z-index: 2;
  border: 1px solid #e2e8f0;
}

.slider-btn.left {
  left: -20px;
}

.slider-btn.right {
  right: -20px;
}

/* Reviews */
.carousel-dot {
  width: 24px;
  height: 6px;
  background: #cbd5e1;
  border-radius: 3px;
  cursor: pointer;
  transition: 0.3s;
}

.carousel-dot.active {
  width: 32px;
  background: #0d9488;
}

/* CTA */
.cta-banner {
  background: #022c22;
}

/* Footer */
.footer-section {
  background-color: #064e40;
}

.footer-links {
  display: flex;
  flex-direction: column;
  gap: 12px;
}

.footer-links a {
  color: #ccfbf1;
  text-decoration: none;
  font-size: 13px;
  transition: color 0.2s;
}

.footer-links a:hover {
  color: white;
  text-decoration: underline;
}

@media (max-width: 960px) {
  .hero-title {
    font-size: 2.5rem;
  }

  .stats-row > div {
    border-right: none;
    margin-bottom: 20px;
  }
}
</style>
