<template>
  <div class="home-page">
    <!-- HEADER -->
    <v-app-bar flat color="white" height="72" class="navbar px-md-10">
      <div class="brand d-flex align-center cursor-pointer" @click="$router.push('/')">
        <div class="brand-icon">
          <v-icon color="white" size="22">mdi-book-open-page-variant</v-icon>
        </div>

        <div>
          <div class="brand-title">Thư viện số</div>
          <div class="brand-subtitle">Digital Library</div>
        </div>
      </div>

      <v-spacer />

      <div class="d-none d-md-flex align-center nav-links">
        <a href="#" class="nav-link active">Trang chủ</a>
        <a href="#" class="nav-link">Giới thiệu</a>
        <a href="#" class="nav-link">Tài liệu</a>
        <a href="#" class="nav-link">Thể loại</a>
        <a href="#" class="nav-link">Tin tức</a>
        <a href="#" class="nav-link">Liên hệ</a>
      </div>

      <v-spacer />

      <div class="d-flex align-center">
        <v-btn icon="mdi-clock-outline" variant="text" size="small" class="mr-2 text-grey-darken-1" />

        <v-btn
          variant="text"
          class="text-none font-weight-bold mr-2 login-text"
          size="small"
          to="/login"
        >
          <v-icon start size="16">mdi-login</v-icon>
          Đăng nhập
        </v-btn>

        <v-btn
          color="#4f46e5"
          class="text-none font-weight-bold rounded-lg px-5 text-white"
          size="small"
          elevation="0"
          to="/login"
        >
          Đăng ký
        </v-btn>
      </div>
    </v-app-bar>

    <!-- HERO -->
    <section class="hero-section">
      <v-container class="main-container">
        <v-row align="center">
          <v-col cols="12" md="6">
            <div class="welcome-badge">
              <v-icon size="14" class="mr-2">mdi-book-open-variant</v-icon>
              Chào mừng đến với Thư viện số
            </div>

            <h1 class="hero-title">
              Tri thức không giới hạn
              <br />
              trong <span>tầm tay bạn</span>
            </h1>

            <p class="hero-desc">
              Thư viện số cung cấp hàng ngàn tài liệu, sách điện tử, luận văn,
              báo cáo và nhiều nguồn học liệu giá trị khác.
            </p>

            <div class="search-box">
              <input placeholder="Tìm kiếm tài liệu, sách, tác giả..." />
              <button>
                <v-icon size="22">mdi-magnify</v-icon>
              </button>
            </div>

            <div class="hero-actions">
              <v-btn
                color="#4f46e5"
                class="text-none text-white rounded-lg font-weight-bold px-6"
                elevation="0"
              >
                <v-icon start size="18">mdi-compass-outline</v-icon>
                Khám phá ngay
              </v-btn>

              <v-btn
                variant="flat"
                color="white"
                class="text-none rounded-lg font-weight-bold px-6 ml-3 learn-more"
                elevation="0"
              >
                Tìm hiểu thêm
                <v-icon end size="17">mdi-information-outline</v-icon>
              </v-btn>
            </div>

            <div class="hero-stats">
              <div>
                <strong>10K+</strong>
                <span>
                  <v-icon size="15">mdi-book-open-outline</v-icon>
                  Tài liệu
                </span>
              </div>

              <div>
                <strong>5K+</strong>
                <span>
                  <v-icon size="15">mdi-account-group-outline</v-icon>
                  Độc giả
                </span>
              </div>

              <div>
                <strong>20K+</strong>
                <span>
                  <v-icon size="15">mdi-swap-horizontal</v-icon>
                  Lượt mượn
                </span>
              </div>
            </div>
          </v-col>

          <v-col cols="12" md="6" class="text-md-right mt-10 mt-md-0">
            <div class="hero-image-card">
              <img
                src="https://images.unsplash.com/photo-1516321318423-f06f85e504b3?auto=format&fit=crop&w=900&q=80"
                alt="Digital library"
              />
            </div>
          </v-col>
        </v-row>
      </v-container>
    </section>

    <!-- FEATURES -->
    <section class="features-section">
      <v-container class="main-container text-center">
        <div class="section-badge">TÍNH NĂNG NỔI BẬT</div>

        <h2 class="section-title">
          Hệ thống hiện đại - Trải nghiệm thông minh
        </h2>

        <p class="section-desc">
          Được thiết kế để giúp bạn tìm kiếm, quản lý và khai thác tri thức một cách dễ dàng nhất.
        </p>

        <v-row class="mt-10">
          <v-col cols="12" sm="6" md="3" v-for="(feature, index) in features" :key="index">
            <v-card class="feature-card" elevation="0">
              <div class="feature-icon" :style="{ backgroundColor: feature.bg }">
                <v-icon :color="feature.color" size="26">{{ feature.icon }}</v-icon>
              </div>

              <h3>{{ feature.title }}</h3>
              <p>{{ feature.desc }}</p>
            </v-card>
          </v-col>
        </v-row>
      </v-container>
    </section>

    <!-- STATS SECTION -->
    <section class="stats-section">
      <v-container class="main-container text-center">
        <div class="stats-badge">THỐNG KÊ ẤN TƯỢNG</div>

        <h2>Những con số biết nói</h2>

        <p>Cùng nhìn lại hành trình phát triển của thư viện số.</p>

        <v-row class="mt-12">
          <v-col cols="12" sm="6" md="3" v-for="(stat, index) in stats" :key="index">
            <div class="stat-box">
              <div class="stat-icon">
                <v-icon size="30">{{ stat.icon }}</v-icon>
              </div>

              <strong>{{ stat.value }}</strong>
              <span>{{ stat.label }}</span>
            </div>
          </v-col>
        </v-row>
      </v-container>
    </section>

    <!-- BOOKS -->
    <section class="books-section">
      <v-container class="main-container">
        <div class="section-header">
          <div>
            <div class="small-label">TÀI LIỆU NỔI BẬT</div>
            <h2>Khám phá tài liệu được quan tâm nhất</h2>
          </div>

          <a href="#">Xem tất cả</a>
        </div>

        <div class="book-list">
          <div class="book-card" v-for="(book, index) in books" :key="index">
            <img :src="book.image" :alt="book.title" />

            <h3>{{ book.title }}</h3>
            <p>{{ book.author }}</p>

            <div class="book-meta">
              <span>
                <v-icon size="14" color="#f59e0b">mdi-star</v-icon>
                {{ book.rating }}
              </span>

              <span>
                <v-icon size="14">mdi-eye-outline</v-icon>
                {{ book.views }}
              </span>
            </div>
          </div>
        </div>
      </v-container>
    </section>

    <!-- REVIEWS -->
    <section class="reviews-section">
      <v-container class="main-container">
        <div class="section-header">
          <div>
            <div class="small-label">ĐÁNH GIÁ TỪ ĐỘC GIẢ</div>
            <h2>Khách hàng nói gì về chúng tôi</h2>
          </div>

          <a href="#">Xem tất cả</a>
        </div>

        <v-row>
          <v-col cols="12" md="4" v-for="(review, index) in reviews" :key="index">
            <v-card class="review-card" elevation="0">
              <div class="review-header">
                <v-avatar size="42">
                  <img :src="review.avatar" :alt="review.name" />
                </v-avatar>

                <div>
                  <h4>{{ review.name }}</h4>
                  <p>{{ review.role }}</p>
                </div>

                <div class="stars">
                  <v-icon v-for="n in 5" :key="n" size="13" color="#f59e0b">mdi-star</v-icon>
                </div>
              </div>

              <p class="review-text">
                {{ review.text }}
              </p>
            </v-card>
          </v-col>
        </v-row>

        <div class="slider-dots">
          <span class="active"></span>
          <span></span>
          <span></span>
        </div>
      </v-container>
    </section>

    <!-- CTA CÓ ẢNH CUỐN SÁCH -->
    <section class="cta-section">
      <v-container class="main-container">
        <v-card class="cta-card" elevation="0">
          <div class="cta-left">
            <img
              class="cta-book-image"
              src="https://images.unsplash.com/photo-1519682337058-a94d519337bc?auto=format&fit=crop&w=500&q=80"
              alt="Magic book"
            />

            <div>
              <h2>Bắt đầu hành trình khám phá tri thức ngay hôm nay!</h2>
              <p>Đăng ký tài khoản miễn phí để trải nghiệm tất cả tính năng của thư viện số.</p>
            </div>
          </div>

          <v-btn
            color="#4f46e5"
            class="text-none text-white rounded-lg px-7 font-weight-bold"
            elevation="0"
            to="/login"
          >
            <v-icon start size="17">mdi-account-plus-outline</v-icon>
            Đăng ký ngay
          </v-btn>
        </v-card>
      </v-container>
    </section>

    <!-- FOOTER -->
    <footer class="footer">
      <v-container class="main-container">
        <v-row>
          <v-col cols="12" md="3">
            <div class="footer-brand">
              <div class="brand-icon">
                <v-icon color="white" size="20">mdi-book-open-page-variant</v-icon>
              </div>

              <div>
                <h3>Thư viện số</h3>
                <p>Digital Library</p>
              </div>
            </div>

            <p class="footer-desc">
              Nền tảng thư viện số hiện đại, cung cấp nguồn tài liệu phong phú
              và trải nghiệm thông minh cho mọi người.
            </p>

            <div class="socials">
              <span><v-icon size="16">mdi-facebook</v-icon></span>
              <span><v-icon size="16">mdi-twitter</v-icon></span>
              <span><v-icon size="16">mdi-youtube</v-icon></span>
              <span><v-icon size="16">mdi-instagram</v-icon></span>
            </div>
          </v-col>

          <v-col cols="6" md="3">
            <h4>Thư viện</h4>
            <a href="#">Tài liệu</a>
            <a href="#">Thể loại</a>
            <a href="#">Thẻ thư viện</a>
            <a href="#">Hướng dẫn mượn trả</a>
          </v-col>

          <v-col cols="6" md="3">
            <h4>Hỗ trợ</h4>
            <a href="#">Câu hỏi thường gặp</a>
            <a href="#">Hướng dẫn sử dụng</a>
            <a href="#">Chính sách bảo mật</a>
            <a href="#">Điều khoản sử dụng</a>
          </v-col>

          <v-col cols="12" md="3">
            <h4>Liên hệ</h4>

            <div class="contact-item">
              <v-icon size="17">mdi-map-marker-outline</v-icon>
              <span>123 Đường Tri Thức, Quận 1, TP. Hồ Chí Minh</span>
            </div>

            <div class="contact-item">
              <v-icon size="17">mdi-phone-outline</v-icon>
              <span>(028) 1234 5678</span>
            </div>

            <div class="contact-item">
              <v-icon size="17">mdi-email-outline</v-icon>
              <span>support@thuvien.so</span>
            </div>
          </v-col>
        </v-row>

        <div class="footer-bottom">
          © 2026 Thư viện số. All rights reserved.
        </div>
      </v-container>
    </footer>
  </div>
</template>

<script setup>
const features = [
  {
    icon: 'mdi-book-open-page-variant-outline',
    color: '#4f46e5',
    bg: '#eef2ff',
    title: 'Kho tài liệu phong phú',
    desc: 'Hàng ngàn sách, tài liệu thuộc nhiều lĩnh vực khác nhau, được cập nhật liên tục.'
  },
  {
    icon: 'mdi-view-grid-outline',
    color: '#22c55e',
    bg: '#ecfdf5',
    title: 'Quản lý thẻ thư viện',
    desc: 'Đăng ký thẻ thư viện trực tuyến, quản lý thông tin và lịch sử mượn trả dễ dàng.'
  },
  {
    icon: 'mdi-swap-horizontal',
    color: '#4f46e5',
    bg: '#eef2ff',
    title: 'Mượn - Trả linh hoạt',
    desc: 'Quy trình mượn trả đơn giản, nhanh chóng với lịch nhắc và gia hạn trực tuyến.'
  },
  {
    icon: 'mdi-chart-bar',
    color: '#eab308',
    bg: '#fefce8',
    title: 'Báo cáo & Thống kê',
    desc: 'Hệ thống báo cáo chi tiết, giúp quản lý dữ liệu hiệu quả và chính xác.'
  }
]

const stats = [
  { icon: 'mdi-account-group-outline', value: '8.450+', label: 'Độc giả đăng ký' },
  { icon: 'mdi-book-outline', value: '12.600+', label: 'Tài liệu số' },
  { icon: 'mdi-swap-horizontal', value: '24.300+', label: 'Lượt mượn' },
  { icon: 'mdi-eye-outline', value: '156.000+', label: 'Lượt truy cập' }
]

const books = [
  {
    title: 'Trí tuệ nhân tạo',
    author: 'Phạm Minh Tuấn',
    rating: '4.8',
    views: '1.2K lượt mượn',
    image: 'https://images.unsplash.com/photo-1532012197267-da84d127e765?auto=format&fit=crop&w=400&q=80'
  },
  {
    title: 'Lập trình Python cơ bản',
    author: 'Nguyễn Văn An',
    rating: '4.7',
    views: '856 lượt mượn',
    image: 'https://images.unsplash.com/photo-1515879218367-8466d910aaa4?auto=format&fit=crop&w=400&q=80'
  },
  {
    title: 'Marketing hiện đại',
    author: 'Trần Thị Mai',
    rating: '4.6',
    views: '648 lượt mượn',
    image: 'https://images.unsplash.com/photo-1542744173-8e7e53415bb0?auto=format&fit=crop&w=400&q=80'
  },
  {
    title: 'Quản trị dự án',
    author: 'Lê Hoàng Nam',
    rating: '4.2',
    views: '512 lượt mượn',
    image: 'https://images.unsplash.com/photo-1553877522-43269d4ea984?auto=format&fit=crop&w=400&q=80'
  },
  {
    title: 'Tâm lý học ứng dụng',
    author: 'Đỗ Thu Hương',
    rating: '4.7',
    views: '723 lượt mượn',
    image: 'https://images.unsplash.com/photo-1512820790803-83ca734da794?auto=format&fit=crop&w=400&q=80'
  },
  {
    title: 'Kinh tế học vĩ mô',
    author: 'Vũ Quốc Bảo',
    rating: '4.6',
    views: '883 lượt mượn',
    image: 'https://images.unsplash.com/photo-1554224155-6726b3ff858f?auto=format&fit=crop&w=400&q=80'
  }
]

const reviews = [
  {
    name: 'Nguyễn Thị Lan',
    role: 'Sinh viên',
    text: 'Thư viện số giúp mình tìm được rất nhiều tài liệu học tập chất lượng. Giao diện dễ dùng, tài liệu đa dạng!',
    avatar: 'https://images.unsplash.com/photo-1494790108377-be9c29b29330?auto=format&fit=crop&w=100&q=80'
  },
  {
    name: 'Trần Minh Quân',
    role: 'Giảng viên',
    text: 'Nguồn tài liệu phong phú, cập nhật thường xuyên. Đây là công cụ hỗ trợ giảng dạy và nghiên cứu tuyệt vời.',
    avatar: 'https://images.unsplash.com/photo-1500648767791-00dcc994a43e?auto=format&fit=crop&w=100&q=80'
  },
  {
    name: 'Phạm Thu Hà',
    role: 'Nghiên cứu sinh',
    text: 'Tôi có thể truy cập tài liệu mọi lúc mọi nơi. Thư viện số thực sự là người bạn đồng hành không thể thiếu.',
    avatar: 'https://images.unsplash.com/photo-1544005313-94ddf0286df2?auto=format&fit=crop&w=100&q=80'
  }
]
</script>

<style scoped>
.home-page {
  min-height: 100vh;
  background: #ffffff;
  color: #1e1b4b;
  font-family: 'Inter', 'Segoe UI', sans-serif;
}

.main-container {
  max-width: 1280px;
}

.navbar {
  border-bottom: 1px solid #e5e7eb;
}

.brand {
  gap: 10px;
}

.brand-icon {
  width: 34px;
  height: 34px;
  border-radius: 8px;
  display: grid;
  place-items: center;
  background: #4f46e5;
}

.brand-title {
  font-weight: 900;
  color: #1f2937;
  line-height: 1;
}

.brand-subtitle {
  color: #6b7280;
  font-size: 11px;
  margin-top: 3px;
}

.nav-links {
  gap: 32px;
}

.nav-link {
  text-decoration: none;
  color: #111827;
  font-weight: 700;
  font-size: 13px;
  padding: 28px 0 24px;
  position: relative;
}

.nav-link.active {
  color: #4f46e5;
}

.nav-link.active::after {
  content: "";
  position: absolute;
  left: 0;
  bottom: 0;
  height: 3px;
  width: 100%;
  background: #4f46e5;
  border-radius: 999px;
}

.login-text {
  color: #111827 !important;
}

.hero-section {
  background: #eef0ff;
  padding: 56px 0 76px;
}

.welcome-badge {
  display: inline-flex;
  align-items: center;
  color: #4f46e5;
  font-size: 13px;
  font-weight: 800;
  margin-bottom: 26px;
}

.hero-title {
  font-size: 54px;
  line-height: 1.12;
  font-weight: 950;
  letter-spacing: -1.5px;
  color: #17172f;
  margin-bottom: 28px;
}

.hero-title span {
  color: #4f46e5;
}

.hero-desc {
  max-width: 620px;
  color: #7a7f91;
  line-height: 1.75;
  font-size: 15px;
  margin-bottom: 24px;
}

.search-box {
  width: 100%;
  max-width: 370px;
  height: 44px;
  background: white;
  border-radius: 8px;
  display: flex;
  align-items: center;
  overflow: hidden;
  box-shadow: 0 8px 24px rgba(79, 70, 229, 0.08);
  margin-bottom: 22px;
}

.search-box input {
  flex: 1;
  border: none;
  outline: none;
  padding: 0 16px;
  font-size: 13px;
  color: #111827;
}

.search-box button {
  width: 52px;
  height: 44px;
  border: none;
  background: #4f46e5;
  color: white;
  display: grid;
  place-items: center;
  cursor: pointer;
}

.hero-actions {
  margin-bottom: 24px;
}

.learn-more {
  color: #111827 !important;
}

.hero-stats {
  display: flex;
  gap: 36px;
  margin-top: 18px;
}

.hero-stats strong {
  display: block;
  font-size: 22px;
  font-weight: 950;
  color: #17172f;
}

.hero-stats span {
  display: flex;
  align-items: center;
  gap: 4px;
  color: #6b7280;
  font-size: 12px;
  margin-top: 4px;
}

.hero-image-card {
  width: 100%;
  max-width: 350px;
  margin-left: auto;
  border-radius: 20px;
  overflow: hidden;
  background: #ded4ff;
  box-shadow: 0 24px 60px rgba(79, 70, 229, 0.16);
}

.hero-image-card img {
  width: 100%;
  height: 255px;
  object-fit: cover;
  display: block;
}

.features-section {
  padding: 78px 0 58px;
  background: white;
}

.section-badge,
.small-label {
  color: #4f46e5;
  font-size: 12px;
  font-weight: 900;
  letter-spacing: 0.6px;
  margin-bottom: 12px;
}

.section-badge {
  display: inline-block;
  background: #eef2ff;
  padding: 7px 14px;
  border-radius: 999px;
}

.section-title {
  font-size: 32px;
  font-weight: 950;
  color: #17172f;
  margin-bottom: 14px;
}

.section-desc {
  color: #8a8f9f;
  font-size: 15px;
  max-width: 720px;
  margin: 0 auto;
}

.feature-card {
  border: 1px solid #e5e7eb;
  border-radius: 12px !important;
  padding: 28px 24px;
  text-align: left;
  min-height: 155px;
  transition: 0.25s;
}

.feature-card:hover {
  transform: translateY(-6px);
  box-shadow: 0 18px 36px rgba(79, 70, 229, 0.08) !important;
}

.feature-icon {
  width: 40px;
  height: 40px;
  border-radius: 8px;
  display: grid;
  place-items: center;
  margin-bottom: 22px;
}

.feature-card h3 {
  color: #17172f;
  font-size: 15px;
  font-weight: 900;
  margin-bottom: 10px;
}

.feature-card p {
  color: #8a8f9f;
  font-size: 13px;
  line-height: 1.7;
}

.stats-section {
  background: #24236f;
  color: white;
  padding: 62px 0 72px;
}

.stats-badge {
  display: inline-block;
  padding: 7px 16px;
  border-radius: 999px;
  border: 1px solid rgba(255, 255, 255, 0.45);
  font-size: 12px;
  font-weight: 800;
  margin-bottom: 18px;
}

.stats-section h2 {
  font-size: 31px;
  font-weight: 950;
  margin-bottom: 10px;
}

.stats-section p {
  color: #c7c7f7;
}

.stat-box {
  text-align: center;
}

.stat-icon {
  width: 50px;
  height: 50px;
  border: 1px solid rgba(255, 255, 255, 0.55);
  border-radius: 50%;
  display: grid;
  place-items: center;
  margin: 0 auto 18px;
}

.stat-box strong {
  display: block;
  font-size: 34px;
  font-weight: 950;
}

.stat-box span {
  display: block;
  color: #d6d7ff;
  font-size: 13px;
  margin-top: 10px;
}

.books-section,
.reviews-section {
  padding: 66px 0 40px;
  background: white;
}

.section-header {
  display: flex;
  align-items: flex-end;
  justify-content: space-between;
  margin-bottom: 28px;
}

.section-header h2 {
  color: #17172f;
  font-size: 27px;
  font-weight: 950;
}

.section-header a {
  color: #4f46e5;
  text-decoration: none;
  font-weight: 700;
  font-size: 13px;
}

.book-list {
  display: grid;
  grid-template-columns: repeat(6, 1fr);
  gap: 24px;
}

.book-card img {
  width: 100%;
  height: 245px;
  border-radius: 10px;
  object-fit: cover;
  box-shadow: 0 14px 28px rgba(17, 24, 39, 0.14);
  transition: 0.25s;
}

.book-card:hover img {
  transform: translateY(-6px);
}

.book-card h3 {
  font-size: 14px;
  font-weight: 900;
  color: #17172f;
  margin-top: 14px;
  margin-bottom: 3px;
}

.book-card p {
  color: #6b7280;
  font-size: 12px;
  margin-bottom: 7px;
}

.book-meta {
  display: flex;
  flex-direction: column;
  gap: 3px;
  color: #4b5563;
  font-size: 12px;
}

.book-meta span {
  display: flex;
  align-items: center;
  gap: 4px;
}

.review-card {
  border: 1px solid #e5e7eb;
  border-radius: 12px !important;
  padding: 24px;
  min-height: 142px;
}

.review-header {
  display: flex;
  align-items: center;
  margin-bottom: 16px;
}

.review-header h4 {
  font-size: 13px;
  font-weight: 900;
  color: #17172f;
  margin-left: 12px;
}

.review-header p {
  font-size: 11px;
  color: #8a8f9f;
  margin-left: 12px;
}

.stars {
  margin-left: auto;
}

.review-text {
  color: #7a7f91;
  font-size: 13px;
  line-height: 1.7;
}

.slider-dots {
  display: flex;
  justify-content: center;
  gap: 8px;
  margin-top: 32px;
}

.slider-dots span {
  width: 8px;
  height: 8px;
  background: #e5e7eb;
  border-radius: 999px;
}

.slider-dots span.active {
  width: 28px;
  background: #4f46e5;
}

.cta-section {
  padding: 40px 0 70px;
  background: white;
}

.cta-card {
  background: #f0f1ff !important;
  border-radius: 18px !important;
  padding: 42px 58px;
  display: flex;
  align-items: center;
  justify-content: space-between;
}

.cta-left {
  display: flex;
  align-items: center;
  gap: 34px;
}

.cta-book-image {
  width: 126px;
  height: 126px;
  border-radius: 12px;
  object-fit: cover;
  box-shadow: 0 16px 35px rgba(79, 70, 229, 0.18);
}

.cta-card h2 {
  color: #17172f;
  font-size: 24px;
  font-weight: 950;
  margin-bottom: 12px;
}

.cta-card p {
  color: #7a7f91;
  font-size: 14px;
}

.footer {
  background: #17172a;
  color: #aab0c5;
  padding: 56px 0 34px;
}

.footer-brand {
  display: flex;
  align-items: center;
  gap: 10px;
  margin-bottom: 18px;
}

.footer-brand h3 {
  color: white;
  font-size: 15px;
  font-weight: 900;
}

.footer-brand p {
  color: #aab0c5;
  font-size: 11px;
}

.footer-desc {
  color: #aab0c5;
  font-size: 13px;
  line-height: 1.8;
  max-width: 260px;
  margin-bottom: 26px;
}

.socials {
  display: flex;
  gap: 10px;
}

.socials span {
  width: 30px;
  height: 30px;
  border: 1px solid #55566e;
  border-radius: 50%;
  display: grid;
  place-items: center;
}

.footer h4 {
  color: white;
  font-size: 14px;
  font-weight: 900;
  margin-bottom: 18px;
}

.footer a {
  display: block;
  color: #aab0c5;
  text-decoration: none;
  font-size: 13px;
  margin-bottom: 12px;
}

.footer a:hover {
  color: white;
}

.contact-item {
  display: flex;
  align-items: flex-start;
  gap: 10px;
  color: #aab0c5;
  font-size: 13px;
  margin-bottom: 14px;
  line-height: 1.6;
}

.footer-bottom {
  border-top: 1px solid #34354a;
  margin-top: 38px;
  padding-top: 24px;
  text-align: center;
  color: #8b91a8;
  font-size: 12px;
}

@media (max-width: 960px) {
  .hero-title {
    font-size: 38px;
  }

  .book-list {
    grid-template-columns: repeat(2, 1fr);
  }

  .cta-card,
  .cta-left {
    flex-direction: column;
    text-align: center;
  }

  .hero-image-card {
    margin: 0 auto;
  }
}

@media (max-width: 600px) {
  .hero-section {
    padding: 36px 0;
  }

  .hero-title {
    font-size: 32px;
  }

  .hero-stats {
    gap: 18px;
  }

  .book-list {
    grid-template-columns: 1fr;
  }

  .book-card img {
    height: 320px;
  }

  .section-header {
    flex-direction: column;
    align-items: flex-start;
    gap: 10px;
  }

  .cta-card {
    padding: 28px;
  }
}
</style>