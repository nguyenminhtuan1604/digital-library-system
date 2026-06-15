<template>
  <v-app style="background-color: #f3f4f6;">
    <!-- SIDEBAR -->
    <v-navigation-drawer
      permanent
      width="250"
      theme="dark"
      border="0"
      style="background-color: #064e40;"
    >
      <div class="px-5 py-6 d-flex align-center">
        <v-avatar color="#ffffff" size="36" class="mr-3 rounded-lg">
          <v-icon color="#0d9488" size="24">mdi-book-open-page-variant</v-icon>
        </v-avatar>

        <div>
          <div
            class="text-subtitle-2 font-weight-bold text-white tracking-tight"
            style="line-height: 1.2;"
          >
            Library System
          </div>
          <div
            class="text-caption text-teal-lighten-4"
            style="font-size: 0.65rem !important;"
          >
            Hệ thống quản lý thư viện số
          </div>
        </div>
      </div>

      <v-list density="compact" nav class="px-3">
        <v-list-item
          v-for="(item, i) in menuItems"
          :key="i"
          :value="item.value"
          :active="currentMenu === item.value"
          :class="currentMenu === item.value ? 'active-menu-item mb-1' : 'mb-1'"
          rounded="lg"
          @click="currentMenu = item.value"
        >
          <template #prepend>
            <v-icon
              :icon="item.icon"
              :color="currentMenu === item.value ? 'white' : 'teal-lighten-3'"
              size="20"
              class="mr-3"
            />
          </template>

          <v-list-item-title class="font-weight-medium text-body-2 text-teal-lighten-4">
            {{ item.title }}
          </v-list-item-title>
        </v-list-item>
      </v-list>

      <template #append>
        <div class="pa-3">
          <div class="d-flex align-center pa-2 rounded-lg user-profile-box">
            <v-avatar size="32" class="mr-3 border-white border">
              <v-img src="https://images.unsplash.com/photo-1599566150163-29194dcaad36?auto=format&fit=crop&w=100&q=80" />
            </v-avatar>

            <div class="text-caption font-weight-bold text-white flex-grow-1">
              Admin Thư viện
            </div>

            <v-icon color="teal-lighten-3" size="18">mdi-chevron-down</v-icon>
          </div>
        </div>
      </template>
    </v-navigation-drawer>

    <!-- HEADER -->
    <v-app-bar flat color="transparent" height="70" class="px-6">
      <div>
        <h2
          class="text-h5 font-weight-bold text-grey-darken-4 mb-0"
          style="line-height: 1;"
        >
          {{ currentMenuTitle }}
        </h2>

        <div class="text-caption text-grey-darken-1 font-weight-medium mt-1">
          <span class="text-teal-darken-2 font-weight-bold">Trang chủ</span>
          <v-icon size="14" class="mx-1">mdi-chevron-right</v-icon>
          <span>{{ currentMenuTitle }}</span>
        </div>
      </div>

      <v-spacer />

      <div class="d-flex align-center">
        <v-text-field
          v-model="quickSearch"
          placeholder="Tìm kiếm nhanh..."
          variant="outlined"
          density="compact"
          bg-color="white"
          hide-details
          rounded="pill"
          append-inner-icon="mdi-magnify"
          class="mr-4 search-input"
          style="width: 240px;"
        />

        <v-badge content="5" color="red" offset-x="8" offset-y="8" class="mr-4">
          <v-btn
            icon="mdi-bell-outline"
            variant="outlined"
            color="grey-darken-1"
            bg-color="white"
            size="36"
            class="rounded-circle bg-white"
          />
        </v-badge>

        <v-avatar size="36" class="border">
          <v-img src="https://images.unsplash.com/photo-1599566150163-29194dcaad36?auto=format&fit=crop&w=100&q=80" />
        </v-avatar>
      </div>
    </v-app-bar>

    <!-- MAIN -->
    <v-main style="height: calc(100vh - 75px); overflow-y: auto;">
      <v-container fluid class="px-6 py-2" style="max-width: 1600px;">
        <!-- ================= TỔNG QUAN ================= -->
        <div v-if="currentMenu === 'overview'">
          <v-row class="mb-2">
            <v-col
              v-for="(kpi, i) in kpiData"
              :key="i"
              cols="12"
              sm="6"
              md="3"
            >
              <v-card class="pa-4 rounded-xl border d-flex align-center h-100" elevation="0">
                <v-avatar :color="kpi.color" size="56" class="mr-4 rounded-circle flex-shrink-0">
                  <v-icon color="white" size="28">{{ kpi.icon }}</v-icon>
                </v-avatar>

                <div class="flex-grow-1">
                  <div class="text-caption font-weight-bold text-grey-darken-3 mb-0">
                    {{ kpi.title }}
                  </div>

                  <div
                    class="text-h5 font-weight-black mb-0"
                    :style="{ color: kpi.valueColor }"
                  >
                    {{ kpi.value }}
                  </div>

                  <div
                    class="text-caption font-weight-medium text-green-darken-2"
                    style="font-size: 0.7rem !important;"
                  >
                    <v-icon size="12">mdi-arrow-top-right</v-icon>
                    {{ kpi.trend }} so với tháng trước
                  </div>
                </div>
              </v-card>
            </v-col>
          </v-row>

          <v-row class="mb-2">
            <v-col cols="12" md="8">
              <v-card class="pa-5 rounded-xl border h-100" elevation="0">
                <div class="d-flex justify-space-between align-center mb-4">
                  <h3 class="text-subtitle-1 font-weight-bold text-grey-darken-4">
                    Lưu lượng Mượn - Trả trong 7 ngày qua
                  </h3>
                </div>

                <div class="chart-wrapper d-flex">
                  <div
                    class="y-axis d-flex flex-column justify-space-between text-caption text-grey-lighten-1 pr-3 pb-5 font-weight-medium"
                    style="font-size: 0.7rem !important;"
                  >
                    <span>500</span>
                    <span>400</span>
                    <span>300</span>
                    <span>200</span>
                    <span>100</span>
                    <span>0</span>
                  </div>

                  <div class="css-bar-chart-container flex-grow-1 position-relative">
                    <div class="grid-lines">
                      <div v-for="n in 6" :key="n" class="grid-line"></div>
                    </div>

                    <div class="bars-area">
                      <div
                        v-for="(day, index) in barChartData"
                        :key="index"
                        class="bar-group"
                      >
                        <div class="bars">
                          <div
                            class="bar borrow-bar"
                            :style="{ height: (day.borrow / 500 * 100) + '%' }"
                          >
                            <span class="bar-value">{{ day.borrow }}</span>
                          </div>

                          <div
                            class="bar return-bar"
                            :style="{ height: (day.return / 500 * 100) + '%' }"
                          >
                            <span class="bar-value">{{ day.return }}</span>
                          </div>
                        </div>

                        <div class="day-label">{{ day.label }}</div>
                      </div>
                    </div>
                  </div>
                </div>
              </v-card>
            </v-col>

            <v-col cols="12" md="4">
              <v-card class="pa-5 rounded-xl border h-100 d-flex flex-column" elevation="0">
                <h3 class="text-subtitle-1 font-weight-bold text-grey-darken-4 mb-4">
                  Tình trạng Thẻ Độc giả
                </h3>

                <div class="d-flex align-center justify-center my-auto">
                  <div class="css-donut-chart mr-4">
                    <div class="donut-hole d-flex flex-column align-center justify-center">
                      <span class="text-h5 font-weight-black text-grey-darken-4">
                        3,920
                      </span>
                    </div>
                  </div>

                  <div>
                    <div class="text-caption font-weight-bold text-grey-darken-4 mb-1">
                      <v-icon color="#0d9488" size="10" class="mr-2">mdi-circle</v-icon>
                      Hoạt động: 87%
                    </div>

                    <div class="text-caption font-weight-bold text-grey-darken-4">
                      <v-icon color="#ef4444" size="10" class="mr-2">mdi-circle</v-icon>
                      Bị khóa: 13%
                    </div>
                  </div>
                </div>
              </v-card>
            </v-col>
          </v-row>

          <v-row>
            <v-col cols="12" md="5">
              <v-card class="pa-4 rounded-xl border" elevation="0">
                <h3 class="text-subtitle-2 font-weight-bold mb-3">
                  Top 5 tài liệu mượn nhiều nhất
                </h3>

                <v-table density="compact" class="table-custom">
                  <tbody>
                    <tr v-for="(book, i) in topBooks" :key="i">
                      <td>{{ i + 1 }}</td>
                      <td class="font-weight-bold py-2">{{ book.title }}</td>
                      <td class="text-right font-weight-black text-teal-darken-2">
                        {{ book.borrows }}
                      </td>
                    </tr>
                  </tbody>
                </v-table>
              </v-card>
            </v-col>

            <v-col cols="12" md="7">
              <v-card class="pa-4 rounded-xl border" elevation="0">
                <h3 class="text-subtitle-2 font-weight-bold mb-3">
                  Độc giả trễ hạn khẩn cấp
                </h3>

                <v-table density="compact" class="table-custom">
                  <tbody>
                    <tr v-for="(user, i) in overdueUsers" :key="i">
                      <td class="font-weight-bold">{{ user.name }}</td>
                      <td>{{ user.book }}</td>
                      <td>
                        <v-chip
                          color="red-lighten-5"
                          text-color="red-darken-1"
                          size="x-small"
                          class="font-weight-bold"
                        >
                          Trễ {{ user.days }} ngày
                        </v-chip>
                      </td>
                    </tr>
                  </tbody>
                </v-table>
              </v-card>
            </v-col>
          </v-row>
        </div>

        <!-- ================= HỒ SƠ ĐỘC GIẢ ================= -->
        <div v-if="currentMenu === 'readers'">
          <!-- THỐNG KÊ HỒ SƠ ĐỘC GIẢ -->
          <v-row class="mb-3">
            <v-col
              v-for="stat in readerStats"
              :key="stat.title"
              cols="12"
              sm="6"
              md="3"
            >
              <v-card class="reader-stat-card pa-4 rounded-xl border d-flex align-center" elevation="0">
                <v-avatar :color="stat.bg" size="58" class="mr-4 rounded-xl">
                  <v-icon :color="stat.color" size="30">{{ stat.icon }}</v-icon>
                </v-avatar>

                <div>
                  <div class="text-caption font-weight-bold text-grey-darken-1">
                    {{ stat.title }}
                  </div>

                  <div class="text-h5 font-weight-black text-grey-darken-4">
                    {{ stat.value }}
                  </div>

                  <div class="text-caption text-grey-darken-1">
                    {{ stat.note }}
                  </div>
                </div>
              </v-card>
            </v-col>
          </v-row>

          <!-- BỘ LỌC -->
          <v-card class="pa-4 rounded-xl border mb-3" elevation="0">
            <v-row align="end">
              <v-col cols="12" md="4">
                <v-text-field
                  v-model="readerSearch"
                  label="Tìm kiếm"
                  placeholder="Nhập tên, email hoặc mã độc giả..."
                  prepend-inner-icon="mdi-magnify"
                  variant="outlined"
                  density="comfortable"
                  color="#0d9488"
                  hide-details
                  @update:model-value="readerPage = 1"
                />
              </v-col>

              <v-col cols="12" md="3">
                <v-select
                  v-model="readerStatusFilter"
                  :items="readerStatusOptions"
                  item-title="title"
                  item-value="value"
                  label="Trạng thái"
                  variant="outlined"
                  density="comfortable"
                  color="#0d9488"
                  hide-details
                  @update:model-value="readerPage = 1"
                />
              </v-col>

              <v-col cols="12" md="3">
                <v-select
                  v-model="readerDepartmentFilter"
                  :items="readerDepartmentOptions"
                  item-title="title"
                  item-value="value"
                  label="Khoa / Lớp"
                  variant="outlined"
                  density="comfortable"
                  color="#0d9488"
                  hide-details
                  @update:model-value="readerPage = 1"
                />
              </v-col>

              <v-col cols="12" md="2" class="d-flex ga-2">
                <v-btn
                  block
                  variant="outlined"
                  color="grey-darken-2"
                  class="text-none font-weight-bold rounded-lg"
                  height="48"
                  @click="resetReaderFilter"
                >
                  <v-icon start size="18">mdi-filter-remove-outline</v-icon>
                  Bỏ lọc
                </v-btn>
              </v-col>
            </v-row>
          </v-card>

          <v-row>
            <!-- BẢNG DANH SÁCH -->
            <v-col cols="12" lg="8">
              <v-card class="pa-4 rounded-xl border" elevation="0">
                <div class="d-flex justify-space-between align-center mb-4 flex-wrap ga-3">
                  <div>
                    <h3 class="text-subtitle-1 font-weight-bold mb-1">
                      Danh sách hồ sơ độc giả
                    </h3>

                    <div class="text-caption text-grey-darken-1">
                      Hiển thị {{ filteredReaders.length }} hồ sơ
                    </div>
                  </div>

                  <div class="d-flex ga-2 flex-wrap">
                    <v-btn
                      color="#0d9488"
                      class="text-white text-none font-weight-bold rounded-lg"
                      prepend-icon="mdi-plus"
                      @click="openAddReader"
                    >
                      Thêm độc giả
                    </v-btn>

                    <v-btn
                      variant="outlined"
                      color="#0d9488"
                      class="text-none font-weight-bold rounded-lg"
                      prepend-icon="mdi-download"
                      @click="exportReadersCsv"
                    >
                      Xuất Excel
                    </v-btn>
                  </div>
                </div>

                <div class="reader-table-wrapper">
                  <v-table density="comfortable" class="reader-table">
                    <thead>
                      <tr>
                        <th>
                          <input
                            type="checkbox"
                            :checked="isAllReadersSelected"
                            @change="toggleSelectReaderPage"
                          />
                        </th>
                        <th>Mã độc giả</th>
                        <th>Thông tin độc giả</th>
                        <th>SĐT</th>
                        <th>Khoa/Lớp</th>
                        <th>Mượn</th>
                        <th>Vi phạm</th>
                        <th>Trạng thái</th>
                        <th class="text-center">Thao tác</th>
                      </tr>
                    </thead>

                    <tbody>
                      <tr
                        v-for="reader in paginatedReaders"
                        :key="reader.id"
                        :class="{ 'selected-reader-row': selectedReader?.id === reader.id }"
                        @click="selectReader(reader)"
                      >
                        <td @click.stop>
                          <input
                            v-model="selectedReaderIds"
                            type="checkbox"
                            :value="reader.id"
                          />
                        </td>

                        <td class="font-weight-black text-teal-darken-2">
                          {{ reader.id }}
                        </td>

                        <td>
                          <div class="d-flex align-center">
                            <v-avatar size="42" class="mr-3">
                              <v-img :src="reader.avatar" />
                            </v-avatar>

                            <div>
                              <div class="font-weight-bold text-grey-darken-4">
                                {{ reader.name }}
                              </div>
                              <div class="text-caption text-grey-darken-1">
                                {{ reader.email }}
                              </div>
                            </div>
                          </div>
                        </td>

                        <td>{{ reader.phone }}</td>
                        <td>{{ reader.department }}</td>
                        <td class="font-weight-bold">{{ reader.borrowing }}</td>
                        <td class="font-weight-bold">{{ reader.violations }}</td>

                        <td>
                          <v-chip
                            size="small"
                            class="font-weight-bold"
                            :color="readerStatusColor(reader.status).bg"
                            :text-color="readerStatusColor(reader.status).text"
                          >
                            {{ readerStatusText(reader.status) }}
                          </v-chip>
                        </td>

                        <td class="text-center" @click.stop>
                          <v-btn
                            icon="mdi-eye-outline"
                            size="small"
                            variant="text"
                            color="grey-darken-2"
                            @click="selectReader(reader)"
                          />

                          <v-btn
                            icon="mdi-pencil-outline"
                            size="small"
                            variant="text"
                            color="#0d9488"
                            @click="openEditReader(reader)"
                          />

                          <v-btn
                            :icon="reader.status === 'locked' ? 'mdi-lock-open-outline' : 'mdi-lock-outline'"
                            size="small"
                            variant="text"
                            color="red"
                            @click="toggleReaderLock(reader)"
                          />
                        </td>
                      </tr>

                      <tr v-if="paginatedReaders.length === 0">
                        <td colspan="9" class="text-center text-grey py-8">
                          Không tìm thấy hồ sơ độc giả phù hợp.
                        </td>
                      </tr>
                    </tbody>
                  </v-table>
                </div>

                <!-- PHÂN TRANG -->
                <div class="d-flex justify-space-between align-center mt-4 flex-wrap ga-3">
                  <div class="text-caption text-grey-darken-1">
                    Trang {{ readerPage }} / {{ readerTotalPages }}
                  </div>

                  <div class="d-flex align-center ga-2 flex-wrap">
                    <v-select
                      v-model="readerPerPage"
                      :items="[5, 7, 10, 20]"
                      density="compact"
                      variant="outlined"
                      hide-details
                      style="width: 90px"
                    />

                    <v-btn
                      variant="outlined"
                      size="small"
                      :disabled="readerPage === 1"
                      @click="readerPage--"
                    >
                      Trước
                    </v-btn>

                    <v-btn
                      v-for="page in readerTotalPages"
                      :key="page"
                      size="small"
                      :color="page === readerPage ? '#0d9488' : 'grey'"
                      :variant="page === readerPage ? 'flat' : 'outlined'"
                      @click="readerPage = page"
                    >
                      {{ page }}
                    </v-btn>

                    <v-btn
                      variant="outlined"
                      size="small"
                      :disabled="readerPage === readerTotalPages"
                      @click="readerPage++"
                    >
                      Tiếp
                    </v-btn>
                  </div>
                </div>

                <!-- THANH THAO TÁC NHIỀU DÒNG -->
                <div v-if="selectedReaderIds.length > 0" class="bulk-reader-bar mt-4">
                  <span>Đã chọn {{ selectedReaderIds.length }} hồ sơ</span>

                  <div class="d-flex ga-2 flex-wrap">
                    <v-btn
                      size="small"
                      variant="outlined"
                      color="#0d9488"
                      class="text-none font-weight-bold"
                      @click="bulkActivateReaders"
                    >
                      Kích hoạt
                    </v-btn>

                    <v-btn
                      size="small"
                      variant="outlined"
                      color="orange"
                      class="text-none font-weight-bold"
                      @click="bulkLockReaders"
                    >
                      Khóa tài khoản
                    </v-btn>

                    <v-btn
                      size="small"
                      variant="outlined"
                      color="red"
                      class="text-none font-weight-bold"
                      @click="bulkDeleteReaders"
                    >
                      Xóa
                    </v-btn>
                  </div>
                </div>
              </v-card>
            </v-col>

            <!-- CHI TIẾT HỒ SƠ -->
            <v-col cols="12" lg="4">
              <v-card
                v-if="selectedReader"
                class="pa-5 rounded-xl border detail-profile-card"
                elevation="0"
              >
                <div class="d-flex justify-space-between align-center mb-4">
                  <h3 class="text-subtitle-1 font-weight-bold mb-0">
                    Chi tiết hồ sơ
                  </h3>

                  <v-chip
                    size="small"
                    class="font-weight-bold"
                    :color="readerStatusColor(selectedReader.status).bg"
                    :text-color="readerStatusColor(selectedReader.status).text"
                  >
                    {{ readerStatusText(selectedReader.status) }}
                  </v-chip>
                </div>

                <div class="text-center mb-5">
                  <v-avatar size="92">
                    <v-img :src="selectedReader.avatar" />
                  </v-avatar>

                  <h2 class="text-h6 font-weight-black mt-3 mb-1">
                    {{ selectedReader.name }}
                  </h2>

                  <div class="text-caption text-grey-darken-1 font-weight-bold">
                    {{ selectedReader.id }}
                  </div>
                </div>

                <div class="profile-detail-list">
                  <div>
                    <span>Họ tên</span>
                    <strong>{{ selectedReader.name }}</strong>
                  </div>

                  <div>
                    <span>Mã độc giả</span>
                    <strong>{{ selectedReader.id }}</strong>
                  </div>

                  <div>
                    <span>Email</span>
                    <strong>{{ selectedReader.email }}</strong>
                  </div>

                  <div>
                    <span>Số điện thoại</span>
                    <strong>{{ selectedReader.phone }}</strong>
                  </div>

                  <div>
                    <span>Khoa/Lớp</span>
                    <strong>{{ selectedReader.department }}</strong>
                  </div>

                  <div>
                    <span>Địa chỉ</span>
                    <strong>{{ selectedReader.address }}</strong>
                  </div>

                  <div>
                    <span>Ngày đăng ký</span>
                    <strong>{{ selectedReader.registeredAt }}</strong>
                  </div>

                  <div>
                    <span>Sách đang mượn</span>
                    <strong>{{ selectedReader.borrowing }}</strong>
                  </div>

                  <div>
                    <span>Số lần vi phạm</span>
                    <strong>{{ selectedReader.violations }}</strong>
                  </div>
                </div>

                <v-btn
                  block
                  variant="outlined"
                  color="#0d9488"
                  class="text-none font-weight-bold rounded-lg mt-5"
                  prepend-icon="mdi-history"
                  @click="openReaderHistory(selectedReader)"
                >
                  Xem lịch sử mượn trả
                </v-btn>

                <v-btn
                  block
                  variant="outlined"
                  color="red"
                  class="text-none font-weight-bold rounded-lg mt-3"
                  :prepend-icon="selectedReader.status === 'locked' ? 'mdi-lock-open-outline' : 'mdi-lock-outline'"
                  @click="toggleReaderLock(selectedReader)"
                >
                  {{ selectedReader.status === 'locked' ? 'Mở khóa tài khoản' : 'Khóa tài khoản' }}
                </v-btn>
              </v-card>
            </v-col>
          </v-row>

          <!-- FORM THÊM / SỬA ĐỘC GIẢ -->
          <v-dialog v-model="readerFormDialog" max-width="760">
            <v-card class="pa-6 rounded-xl">
              <div class="d-flex justify-space-between align-center mb-5">
                <h2 class="text-h6 font-weight-black">
                  {{ editingReader ? 'Chỉnh sửa hồ sơ độc giả' : 'Thêm độc giả mới' }}
                </h2>

                <v-btn icon="mdi-close" variant="text" @click="readerFormDialog = false" />
              </div>

              <v-row>
                <v-col cols="12" md="6">
                  <v-text-field
                    v-model="readerForm.name"
                    label="Họ và tên"
                    variant="outlined"
                    color="#0d9488"
                  />
                </v-col>

                <v-col cols="12" md="6">
                  <v-text-field
                    v-model="readerForm.email"
                    label="Email"
                    variant="outlined"
                    color="#0d9488"
                  />
                </v-col>

                <v-col cols="12" md="6">
                  <v-text-field
                    v-model="readerForm.phone"
                    label="Số điện thoại"
                    variant="outlined"
                    color="#0d9488"
                  />
                </v-col>

                <v-col cols="12" md="6">
                  <v-text-field
                    v-model="readerForm.department"
                    label="Khoa/Lớp"
                    variant="outlined"
                    color="#0d9488"
                  />
                </v-col>

                <v-col cols="12">
                  <v-text-field
                    v-model="readerForm.address"
                    label="Địa chỉ"
                    variant="outlined"
                    color="#0d9488"
                  />
                </v-col>

                <v-col cols="12" md="4">
                  <v-text-field
                    v-model.number="readerForm.borrowing"
                    type="number"
                    label="Sách đang mượn"
                    variant="outlined"
                    color="#0d9488"
                  />
                </v-col>

                <v-col cols="12" md="4">
                  <v-text-field
                    v-model.number="readerForm.violations"
                    type="number"
                    label="Số lần vi phạm"
                    variant="outlined"
                    color="#0d9488"
                  />
                </v-col>

                <v-col cols="12" md="4">
                  <v-select
                    v-model="readerForm.status"
                    :items="readerStatusOptions.filter(x => x.value !== 'all')"
                    item-title="title"
                    item-value="value"
                    label="Trạng thái"
                    variant="outlined"
                    color="#0d9488"
                  />
                </v-col>
              </v-row>

              <div class="d-flex justify-end ga-3 mt-3">
                <v-btn
                  variant="outlined"
                  color="grey"
                  class="text-none font-weight-bold"
                  @click="readerFormDialog = false"
                >
                  Hủy
                </v-btn>

                <v-btn
                  color="#0d9488"
                  class="text-white text-none font-weight-bold"
                  prepend-icon="mdi-content-save-outline"
                  @click="saveReader"
                >
                  Lưu hồ sơ
                </v-btn>
              </div>
            </v-card>
          </v-dialog>

          <!-- LỊCH SỬ MƯỢN TRẢ -->
          <v-dialog v-model="readerHistoryDialog" max-width="760">
            <v-card class="pa-6 rounded-xl">
              <div class="d-flex justify-space-between align-center mb-5">
                <h2 class="text-h6 font-weight-black">
                  Lịch sử mượn trả - {{ historyReader?.name }}
                </h2>

                <v-btn
                  icon="mdi-close"
                  variant="text"
                  @click="readerHistoryDialog = false"
                />
              </div>

              <v-table density="comfortable" class="reader-table">
                <thead>
                  <tr>
                    <th>Tên sách</th>
                    <th>Ngày mượn</th>
                    <th>Ngày trả</th>
                    <th>Trạng thái</th>
                  </tr>
                </thead>

                <tbody>
                  <tr v-for="item in readerBorrowHistory" :key="item.book">
                    <td class="font-weight-bold">{{ item.book }}</td>
                    <td>{{ item.borrowedAt }}</td>
                    <td>{{ item.returnedAt }}</td>
                    <td>
                      <v-chip
                        size="small"
                        class="font-weight-bold"
                        :color="item.status === 'late' ? 'red-lighten-5' : 'green-lighten-5'"
                        :text-color="item.status === 'late' ? 'red-darken-2' : 'green-darken-2'"
                      >
                        {{ item.statusText }}
                      </v-chip>
                    </td>
                  </tr>
                </tbody>
              </v-table>
            </v-card>
          </v-dialog>
        </div>

        <!-- ================= THẺ THƯ VIỆN - BẢN SẠCH, RÕ CHỮ ================= -->
        <div v-if="currentMenu === 'cards'" class="library-cards-page">
          <v-row class="mb-3" dense>
            <!-- PREVIEW CARD -->
            <v-col cols="12" lg="5">
              <v-card class="card-preview-panel pa-4 rounded-xl border" elevation="0">
                <div class="section-title-row mb-3">
                  <h3>Xem trước thẻ thư viện</h3>
                  <span v-if="selectedLibraryCard" class="status-pill" :class="`status-${selectedLibraryCard.status}`">
                    {{ cardStatusText(selectedLibraryCard.status) }}
                  </span>
                </div>

                <div v-if="selectedLibraryCard" class="library-id-card">
                  <div class="card-wave wave-1"></div>
                  <div class="card-wave wave-2"></div>

                  <div class="id-card-head">
                    <div class="id-logo">
                      <v-icon color="#0d9488" size="28">mdi-book-open-page-variant</v-icon>
                    </div>

                    <div>
                      <strong>LIBRARY SYSTEM</strong>
                      <p>Thẻ thư viện</p>
                    </div>
                  </div>

                  <div class="id-card-body">
                    <img :src="selectedLibraryCard.avatar" :alt="selectedLibraryCard.owner" class="card-avatar" />

                    <div class="card-owner-info">
                      <h2>{{ selectedLibraryCard.id }}</h2>
                      <h4>{{ selectedLibraryCard.owner }}</h4>

                      <div class="card-meta">
                        <span>
                          <v-icon size="15">mdi-card-account-details-outline</v-icon>
                          {{ selectedLibraryCard.type }}
                        </span>
                        <span>
                          <v-icon size="15">mdi-domain</v-icon>
                          {{ selectedLibraryCard.department }}
                        </span>
                      </div>

                      <div class="date-row">
                        <div>
                          <small>Ngày cấp</small>
                          <b>{{ selectedLibraryCard.issueDate }}</b>
                        </div>

                        <div>
                          <small>Hết hạn</small>
                          <b>{{ selectedLibraryCard.expiryDate }}</b>
                        </div>
                      </div>
                    </div>

                    <div class="qr-code">
                      <span v-for="n in 49" :key="n" :class="{ filled: qrFill(n, selectedLibraryCard.id) }"></span>
                    </div>
                  </div>

                  <div class="id-card-foot">
                    <span class="status-pill light" :class="`status-${selectedLibraryCard.status}`">
                      {{ cardStatusText(selectedLibraryCard.status) }}
                    </span>

                    <div>
                      <div class="barcode">
                        <i v-for="n in 32" :key="n" :style="{ height: `${18 + (n % 5) * 4}px` }"></i>
                      </div>
                      <small>{{ selectedLibraryCard.id }}</small>
                    </div>
                  </div>
                </div>
              </v-card>
            </v-col>

            <!-- QUICK ACTIONS + STATS -->
            <v-col cols="12" lg="7">
              <v-card class="pa-4 rounded-xl border h-100" elevation="0">
                <div class="section-title-row mb-3">
                  <h3>Thao tác nhanh</h3>
                </div>

                <div class="quick-card-actions">
                  <button
                    v-for="action in cardQuickActions"
                    :key="action.title"
                    class="quick-card-btn"
                    type="button"
                    :style="{ color: action.color }"
                    @click="action.handler"
                  >
                    <v-icon size="31">{{ action.icon }}</v-icon>
                    <span>{{ action.title }}</span>
                  </button>
                </div>

                <v-divider class="my-4" />

                <div class="section-title-row mb-3">
                  <h3>Thống kê thẻ</h3>
                </div>

                <div class="library-card-stats">
                  <div v-for="stat in libraryCardStats" :key="stat.title" class="card-stat-box">
                    <div class="stat-mini-icon" :style="{ color: stat.color }">
                      <v-icon size="26">{{ stat.icon }}</v-icon>
                    </div>
                    <p>{{ stat.title }}</p>
                    <h2>{{ stat.value }}</h2>
                    <span>{{ stat.note }}</span>
                  </div>
                </div>
              </v-card>
            </v-col>
          </v-row>

          <!-- FILTER -->
          <v-card class="pa-4 rounded-xl border mb-3" elevation="0">
            <v-row dense align="end">
              <v-col cols="12" md="4">
                <v-text-field
                  v-model="cardSearch"
                  placeholder="Nhập mã thẻ, tên độc giả hoặc email..."
                  prepend-inner-icon="mdi-magnify"
                  variant="outlined"
                  density="comfortable"
                  color="#0d9488"
                  hide-details
                />
              </v-col>

              <v-col cols="12" md="2">
                <v-select
                  v-model="cardStatusFilter"
                  :items="cardStatusOptions"
                  item-title="title"
                  item-value="value"
                  label="Trạng thái"
                  variant="outlined"
                  density="comfortable"
                  color="#0d9488"
                  hide-details
                />
              </v-col>

              <v-col cols="12" md="2">
                <v-select
                  v-model="cardTypeFilter"
                  :items="cardTypeOptions"
                  item-title="title"
                  item-value="value"
                  label="Loại thẻ"
                  variant="outlined"
                  density="comfortable"
                  color="#0d9488"
                  hide-details
                />
              </v-col>

              <v-col cols="12" md="2">
                <v-select
                  v-model="cardDepartmentFilter"
                  :items="cardDepartmentOptions"
                  item-title="title"
                  item-value="value"
                  label="Khoa / Đơn vị"
                  variant="outlined"
                  density="comfortable"
                  color="#0d9488"
                  hide-details
                />
              </v-col>

              <v-col cols="12" md="2">
                <v-btn
                  block
                  variant="outlined"
                  height="48"
                  class="text-none font-weight-bold rounded-lg"
                  color="grey-darken-3"
                  prepend-icon="mdi-filter-remove-outline"
                  @click="resetCardFilter"
                >
                  Bỏ lọc
                </v-btn>
              </v-col>
            </v-row>
          </v-card>

          <!-- TABLE -->
          <v-card class="pa-4 rounded-xl border" elevation="0">
            <div class="section-title-row mb-3">
              <div>
                <h3>Danh sách thẻ thư viện</h3>
                <p>Hiển thị {{ filteredLibraryCards.length }} thẻ</p>
              </div>

              <div class="top-actions">
                <v-btn
                  color="#0d9488"
                  class="text-white text-none font-weight-bold rounded-lg"
                  prepend-icon="mdi-plus"
                  @click="openAddCard"
                >
                  Cấp thẻ mới
                </v-btn>

                <v-btn
                  variant="outlined"
                  color="#0d9488"
                  class="text-none font-weight-bold rounded-lg"
                  prepend-icon="mdi-download"
                  @click="exportCardsCsv"
                >
                  Xuất Excel
                </v-btn>
              </div>
            </div>

            <div class="library-table-wrap">
              <table class="library-card-table">
                <thead>
                  <tr>
                    <th>
                      <input type="checkbox" :checked="isAllCardsSelected" @change="toggleSelectCardPage" />
                    </th>
                    <th>Mã thẻ</th>
                    <th>Chủ thẻ</th>
                    <th>Loại thẻ</th>
                    <th>Ngày cấp</th>
                    <th>Ngày hết hạn</th>
                    <th>QR / Barcode</th>
                    <th>Trạng thái</th>
                    <th class="text-center">Thao tác</th>
                  </tr>
                </thead>

                <tbody>
                  <tr
                    v-for="card in paginatedLibraryCards"
                    :key="card.id"
                    :class="{ selected: selectedLibraryCard?.id === card.id }"
                    @click="selectLibraryCard(card)"
                  >
                    <td @click.stop>
                      <input type="checkbox" :value="card.id" v-model="selectedCardIds" />
                    </td>

                    <td class="card-id-cell">{{ card.id }}</td>

                    <td>
                      <div class="owner-cell">
                        <v-avatar size="42">
                          <v-img :src="card.avatar" />
                        </v-avatar>

                        <div>
                          <strong>{{ card.owner }}</strong>
                          <span>{{ card.email }}</span>
                        </div>
                      </div>
                    </td>

                    <td>
                      <span class="type-pill" :class="`type-${card.type.replaceAll(' ', '-').toLowerCase()}`">
                        {{ card.type }}
                      </span>
                    </td>

                    <td>{{ card.issueDate }}</td>

                    <td>
                      <span :class="{ 'date-danger': card.status === 'expired', 'date-warning': card.status === 'expiring' }">
                        {{ card.expiryDate }}
                        <v-icon v-if="card.status === 'expired' || card.status === 'expiring'" size="15" class="ml-1">
                          mdi-alert-circle
                        </v-icon>
                      </span>
                    </td>

                    <td>
                      <button class="mini-qr" type="button" @click.stop="selectLibraryCard(card)">
                        <span v-for="n in 25" :key="n" :class="{ filled: qrFill(n, card.id) }"></span>
                      </button>
                    </td>

                    <td>
                      <span class="status-pill" :class="`status-${card.status}`">
                        {{ cardStatusText(card.status) }}
                      </span>
                    </td>

                    <td class="text-center" @click.stop>
                      <div class="table-card-actions">
                        <button title="Gia hạn" @click="openRenewCard(card)">
                          <v-icon size="18">mdi-refresh</v-icon>
                        </button>

                        <button title="In thẻ" @click="printCard(card)">
                          <v-icon size="18">mdi-printer-outline</v-icon>
                        </button>

                        <button title="Khóa / mở khóa" class="orange" @click="toggleCardLock(card)">
                          <v-icon size="18">
                            {{ card.status === 'locked' ? 'mdi-lock-open-outline' : 'mdi-lock-outline' }}
                          </v-icon>
                        </button>

                        <button title="Sửa" @click="openEditCard(card)">
                          <v-icon size="18">mdi-pencil-outline</v-icon>
                        </button>
                      </div>
                    </td>
                  </tr>

                  <tr v-if="paginatedLibraryCards.length === 0">
                    <td colspan="9" class="empty-table-text">Không tìm thấy thẻ phù hợp.</td>
                  </tr>
                </tbody>
              </table>
            </div>

            <div class="card-pagination">
              <div class="d-flex align-center ga-3">
                <v-select
                  v-model="cardPerPage"
                  :items="[5, 7, 10, 20]"
                  density="compact"
                  variant="outlined"
                  hide-details
                  style="width: 90px"
                />
                <span>thẻ mỗi trang</span>
              </div>

              <div class="d-flex align-center ga-2">
                <v-btn size="small" variant="outlined" :disabled="cardPage === 1" @click="cardPage--">
                  Trước
                </v-btn>

                <v-btn
                  v-for="page in cardTotalPages"
                  :key="page"
                  size="small"
                  :color="page === cardPage ? '#0d9488' : 'grey'"
                  :variant="page === cardPage ? 'flat' : 'outlined'"
                  @click="cardPage = page"
                >
                  {{ page }}
                </v-btn>

                <v-btn size="small" variant="outlined" :disabled="cardPage === cardTotalPages" @click="cardPage++">
                  Tiếp
                </v-btn>
              </div>
            </div>

            <div v-if="selectedCardIds.length > 0" class="card-bulk-bar">
              <span>Đã chọn {{ selectedCardIds.length }} thẻ</span>

              <div class="d-flex ga-2">
                <v-btn size="small" variant="outlined" color="#0d9488" @click="bulkActivateCards">Kích hoạt</v-btn>
                <v-btn size="small" variant="outlined" color="orange" @click="bulkLockCards">Khóa thẻ</v-btn>
                <v-btn size="small" variant="outlined" color="purple" @click="bulkPrintCards">In thẻ</v-btn>
                <v-btn size="small" variant="outlined" color="red" @click="bulkDeleteCards">Xóa</v-btn>
              </div>
            </div>
          </v-card>

          <!-- ADD / EDIT DIALOG -->
          <v-dialog v-model="cardFormDialog" max-width="760">
            <v-card class="pa-6 rounded-xl">
              <div class="section-title-row mb-5">
                <h3>{{ editingCard ? 'Chỉnh sửa thẻ thư viện' : 'Cấp thẻ thư viện mới' }}</h3>
                <v-btn icon="mdi-close" variant="text" @click="cardFormDialog = false" />
              </div>

              <v-row>
                <v-col cols="12" md="6">
                  <v-text-field v-model="cardForm.owner" label="Chủ thẻ" variant="outlined" color="#0d9488" />
                </v-col>

                <v-col cols="12" md="6">
                  <v-text-field v-model="cardForm.email" label="Email" variant="outlined" color="#0d9488" />
                </v-col>

                <v-col cols="12" md="6">
                  <v-text-field v-model="cardForm.department" label="Khoa/Lớp" variant="outlined" color="#0d9488" />
                </v-col>

                <v-col cols="12" md="6">
                  <v-select
                    v-model="cardForm.type"
                    :items="cardTypeOptions.filter(item => item.value !== 'all')"
                    item-title="title"
                    item-value="value"
                    label="Loại thẻ"
                    variant="outlined"
                    color="#0d9488"
                  />
                </v-col>

                <v-col cols="12" md="4">
                  <v-text-field v-model="cardForm.issueDate" label="Ngày cấp" variant="outlined" color="#0d9488" />
                </v-col>

                <v-col cols="12" md="4">
                  <v-text-field v-model="cardForm.expiryDate" label="Ngày hết hạn" variant="outlined" color="#0d9488" />
                </v-col>

                <v-col cols="12" md="4">
                  <v-select
                    v-model="cardForm.status"
                    :items="cardStatusOptions.filter(item => item.value !== 'all')"
                    item-title="title"
                    item-value="value"
                    label="Trạng thái"
                    variant="outlined"
                    color="#0d9488"
                  />
                </v-col>
              </v-row>

              <div class="d-flex justify-end ga-3 mt-2">
                <v-btn variant="outlined" color="grey" class="text-none font-weight-bold" @click="cardFormDialog = false">
                  Hủy
                </v-btn>

                <v-btn color="#0d9488" class="text-white text-none font-weight-bold" prepend-icon="mdi-content-save-outline" @click="saveLibraryCard">
                  Lưu thẻ
                </v-btn>
              </div>
            </v-card>
          </v-dialog>

          <!-- RENEW DIALOG -->
          <v-dialog v-model="renewCardDialog" max-width="480">
            <v-card class="pa-6 rounded-xl">
              <div class="section-title-row mb-4">
                <h3>Gia hạn thẻ</h3>
                <v-btn icon="mdi-close" variant="text" @click="renewCardDialog = false" />
              </div>

              <p class="text-grey-darken-1 mb-4">
                Thẻ: <b>{{ selectedLibraryCard?.id }}</b> - {{ selectedLibraryCard?.owner }}
              </p>

              <v-select
                v-model="renewMonths"
                :items="[3, 6, 12, 24]"
                label="Gia hạn thêm"
                suffix="tháng"
                variant="outlined"
                color="#0d9488"
              />

              <div class="d-flex justify-end ga-3 mt-2">
                <v-btn variant="outlined" color="grey" class="text-none font-weight-bold" @click="renewCardDialog = false">
                  Hủy
                </v-btn>

                <v-btn color="#0d9488" class="text-white text-none font-weight-bold" @click="renewSelectedCard">
                  Gia hạn
                </v-btn>
              </div>
            </v-card>
          </v-dialog>
        </div>


        <!-- ================= HỒ SƠ MƯỢN TRẢ ================= -->
        <div v-if="currentMenu === 'history'">
          <v-card class="pa-5 rounded-xl border" elevation="0">
            <h3 class="text-subtitle-1 font-weight-bold mb-4">
              Hồ sơ lịch sử mượn trả tài liệu số
            </h3>

            <v-table density="compact" class="table-custom">
              <thead>
                <tr class="bg-grey-lighten-4">
                  <th class="font-weight-bold">Độc giả</th>
                  <th class="font-weight-bold">Tên tài liệu</th>
                  <th class="font-weight-bold">Ngày mượn</th>
                  <th class="font-weight-bold">Ngày trả thực tế</th>
                  <th class="font-weight-bold text-right">Phạt phát sinh</th>
                </tr>
              </thead>

              <tbody>
                <tr v-for="(record, idx) in returnHistory" :key="idx">
                  <td class="font-weight-bold">{{ record.user }}</td>
                  <td class="text-grey-darken-3 font-weight-medium">{{ record.book }}</td>
                  <td>{{ record.borrowDate }}</td>
                  <td>
                    <v-chip
                      color="emerald"
                      class="bg-green-lighten-5 text-green-darken-2 font-weight-bold"
                      size="x-small"
                      prepend-icon="mdi-calendar-check"
                    >
                      {{ record.returnDate }}
                    </v-chip>
                  </td>
                  <td
                    class="text-right font-weight-bold"
                    :class="record.fine !== '0' ? 'text-red' : 'text-grey-darken-2'"
                  >
                    {{ record.fine }}đ
                  </td>
                </tr>
              </tbody>
            </v-table>
          </v-card>
        </div>

        <!-- ================= BÁO CÁO ================= -->
        <section v-if="currentMenu === 'reports'" class="report-section">
          
          <v-card class="report-panel" elevation="0">
            <v-row dense align="center">
              <v-col cols="12" md="2">
                <div class="filter-label">Loại báo cáo</div>
                <v-select v-model="reportType" :items="['Tổng hợp', 'Chi tiết mượn trả']" variant="outlined" density="compact" hide-details color="#0d9488" />
              </v-col>
              <v-col cols="12" md="2">
                <div class="filter-label">Khoảng thời gian</div>
                <v-select v-model="reportPeriod" :items="['Tháng này', 'Tuần này', 'Quý này']" variant="outlined" density="compact" hide-details color="#0d9488" />
              </v-col>
              <v-col cols="12" md="2">
                <div class="filter-label">Từ ngày</div>
                <v-text-field v-model="reportFrom" type="date" variant="outlined" density="compact" hide-details color="#0d9488" />
              </v-col>
              <v-col cols="12" md="2">
                <div class="filter-label">Đến ngày</div>
                <v-text-field v-model="reportTo" type="date" variant="outlined" density="compact" hide-details color="#0d9488" />
              </v-col>
              <v-col cols="12" md="2" class="d-flex align-end ml-auto">
                <v-btn block color="#0d9488" class="text-white text-none font-weight-bold mt-4 mt-md-0" height="42" elevation="0" prepend-icon="mdi-chart-bar" @click="generateReport">
                  Tạo báo cáo
                </v-btn>
              </v-col>
            </v-row>
          </v-card>

          <v-row class="mt-2">
            <v-col v-for="kpi in reportKpis" :key="kpi.title" cols="12" sm="4" lg="2">
              <v-card class="report-kpi-card" elevation="0">
                <div class="d-flex flex-column align-center text-center">
                  <v-avatar :color="kpi.bg" size="48" class="mb-2 rounded-lg">
                    <v-icon :color="kpi.color" size="24">{{ kpi.icon }}</v-icon>
                  </v-avatar>
                  <div class="kpi-title">{{ kpi.title }}</div>
                  <div class="kpi-value">{{ kpi.value }}</div>
                  <div class="kpi-trend" :class="{ 'text-red': kpi.danger }">{{ kpi.trend }}</div>
                </div>
              </v-card>
            </v-col>
          </v-row>

          <v-row class="mt-2">
            
            <v-col cols="12" lg="6">
              <v-card class="report-panel h-100" elevation="0">
                <div class="d-flex justify-space-between align-center mb-6">
                  <h3 class="panel-title">Biểu đồ lượt mượn - trả theo ngày</h3>
                  <v-select :items="['7 ngày qua', 'Tháng này']" value="7 ngày qua" variant="outlined" density="compact" hide-details style="max-width: 140px;" class="custom-select" />
                </div><div class="svg-chart-container position-relative">
                  <div class="y-axis-labels">
                    <span>300</span><span>250</span><span>200</span><span>150</span><span>100</span><span>50</span><span>0</span>
                  </div>
                  
                  <div class="svg-wrapper">
                    <svg viewBox="0 0 700 240" preserveAspectRatio="none" class="w-100 h-100 overflow-visible">
                      <line x1="0" y1="0" x2="700" y2="0" stroke="#f1f5f9" stroke-width="1.5" />
                      <line x1="0" y1="40" x2="700" y2="40" stroke="#f1f5f9" stroke-width="1.5" />
                      <line x1="0" y1="80" x2="700" y2="80" stroke="#f1f5f9" stroke-width="1.5" />
                      <line x1="0" y1="120" x2="700" y2="120" stroke="#f1f5f9" stroke-width="1.5" />
                      <line x1="0" y1="160" x2="700" y2="160" stroke="#f1f5f9" stroke-width="1.5" />
                      <line x1="0" y1="200" x2="700" y2="200" stroke="#f1f5f9" stroke-width="1.5" />
                      <line x1="0" y1="240" x2="700" y2="240" stroke="#e2e8f0" stroke-width="2" />

                      <path d="M 0 240 L 0 160 L 100 88 L 200 128 L 300 40 L 400 88 L 500 56 L 600 104 L 700 120 L 700 240 Z" fill="rgba(16, 185, 129, 0.15)" />
                      <polyline points="0,160 100,88 200,128 300,40 400,88 500,56 600,104 700,120" fill="none" stroke="#10b981" stroke-width="3" stroke-linecap="round" stroke-linejoin="round" />
                      <circle cx="0" cy="160" r="4" fill="white" stroke="#10b981" stroke-width="2" />
                      <circle cx="100" cy="88" r="4" fill="white" stroke="#10b981" stroke-width="2" />
                      <circle cx="200" cy="128" r="4" fill="white" stroke="#10b981" stroke-width="2" />
                      <circle cx="300" cy="40" r="4" fill="white" stroke="#10b981" stroke-width="2" />
                      <circle cx="400" cy="88" r="4" fill="white" stroke="#10b981" stroke-width="2" />
                      <circle cx="500" cy="56" r="4" fill="white" stroke="#10b981" stroke-width="2" />
                      <circle cx="600" cy="104" r="4" fill="white" stroke="#10b981" stroke-width="2" />
                      <circle cx="700" cy="120" r="4" fill="white" stroke="#10b981" stroke-width="2" />

                      <path d="M 0 240 L 0 216 L 100 176 L 200 192 L 300 124 L 400 160 L 500 136 L 600 184 L 700 188 L 700 240 Z" fill="rgba(59, 130, 246, 0.15)" />
                      <polyline points="0,216 100,176 200,192 300,124 400,160 500,136 600,184 700,188" fill="none" stroke="#3b82f6" stroke-width="3" stroke-linecap="round" stroke-linejoin="round" />
                      <circle cx="0" cy="216" r="4" fill="white" stroke="#3b82f6" stroke-width="2" />
                      <circle cx="100" cy="176" r="4" fill="white" stroke="#3b82f6" stroke-width="2" /><circle cx="200" cy="192" r="4" fill="white" stroke="#3b82f6" stroke-width="2" />
                      <circle cx="300" cy="124" r="4" fill="white" stroke="#3b82f6" stroke-width="2" />
                      <circle cx="400" cy="160" r="4" fill="white" stroke="#3b82f6" stroke-width="2" />
                      <circle cx="500" cy="136" r="4" fill="white" stroke="#3b82f6" stroke-width="2" />
                      <circle cx="600" cy="184" r="4" fill="white" stroke="#3b82f6" stroke-width="2" />
                      <circle cx="700" cy="188" r="4" fill="white" stroke="#3b82f6" stroke-width="2" />
                    </svg>

                    <div class="x-axis-labels">
                      <span>01/06</span><span>03/06</span><span>05/06</span><span>07/06</span><span>09/06</span><span>11/06</span><span>13/06</span><span>15/06</span>
                    </div>
                  </div>
                </div>

                <div class="d-flex justify-center ga-6 mt-4 legend-area">
                  <span><i class="bg-green-accent-4"></i>Lượt mượn</span>
                  <span><i class="bg-blue-accent-2"></i>Lượt trả</span>
                </div>
              </v-card>
            </v-col>

            <v-col cols="12" lg="3">
              <v-card class="report-panel h-100" elevation="0">
                <h3 class="panel-title mb-6">Lượt mượn theo thể loại</h3>
                <div class="d-flex align-center flex-column">
                  <div class="new-donut">
                    <div class="new-donut-hole">
                      <strong>100%</strong>
                      <span>Tổng</span>
                    </div>
                  </div>
                  <div class="donut-legend mt-6 w-100">
                    <div v-for="cat in categoryStats" :key="cat.name" class="d-flex justify-space-between align-center mb-2">
                      <div class="d-flex align-center font-weight-bold text-caption text-grey-darken-3">
                        <span class="legend-dot" :style="{ background: cat.color }"></span> {{ cat.name }}
                      </div>
                      <b class="text-caption">{{ cat.percent }}%</b>
                    </div>
                  </div>
                </div>
              </v-card>
            </v-col>

            <v-col cols="12" lg="3">
              <v-card class="report-panel h-100" elevation="0">
                <div class="d-flex justify-space-between align-center mb-6">
                  <h3 class="panel-title">Top 5 sách được mượn nhiều</h3>
                  <v-select :items="['Top 5', 'Top 10']" value="Top 5" variant="outlined" density="compact" hide-details style="max-width: 90px;" class="custom-select" />
                </div>
                
                <div class="progress-list">
                  <div v-for="book in reportTopBooks" :key="book.title" class="mb-4">
                    <div class="d-flex justify-space-between mb-1"><span class="text-caption font-weight-bold text-grey-darken-3">{{ book.title }}</span>
                      <b class="text-caption">{{ book.count }}</b>
                    </div>
                    <div class="progress-track">
                      <div class="progress-fill" :style="{ width: book.percent + '%' }"></div>
                    </div>
                  </div>
                </div>
              </v-card>
            </v-col>
          </v-row>

          <v-row class="mt-2">
            <v-col cols="12" lg="4">
              <v-card class="report-panel h-100" elevation="0">
                <div class="d-flex justify-space-between align-center mb-4">
                  <h3 class="panel-title">Top 5 độc giả mượn nhiều nhất</h3>
                  <v-select :items="['Tháng này']" value="Tháng này" variant="outlined" density="compact" hide-details style="max-width: 110px;" class="custom-select" />
                </div>
                <v-table density="compact" class="report-table">
                  <thead>
                    <tr><th>#</th><th>Độc giả</th><th>Mã số</th><th class="text-right">Lượt mượn</th></tr>
                  </thead>
                  <tbody>
                    <tr v-for="user in topReaders" :key="user.id">
                      <td>{{ user.rank }}</td>
                      <td class="font-weight-bold text-grey-darken-3">{{ user.name }}</td>
                      <td class="text-grey-darken-1">{{ user.id }}</td>
                      <td class="text-right font-weight-black">{{ user.count }}</td>
                    </tr>
                  </tbody>
                </v-table>
                <v-btn block variant="outlined" color="#0d9488" class="mt-4 text-none font-weight-bold rounded-lg">Xem tất cả</v-btn>
              </v-card>
            </v-col>

            <v-col cols="12" lg="4">
              <v-card class="report-panel h-100" elevation="0">
                <div class="d-flex justify-space-between align-center mb-4">
                  <h3 class="panel-title">Top 5 đầu sách được mượn nhiều nhất</h3>
                  <v-select :items="['Tháng này']" value="Tháng này" variant="outlined" density="compact" hide-details style="max-width: 110px;" class="custom-select" />
                </div>
                <v-table density="compact" class="report-table">
                  <thead>
                    <tr><th>#</th><th>Sách</th><th class="text-right">Lượt mượn</th></tr>
                  </thead>
                  <tbody>
                    <tr v-for="book in topBooksDetail" :key="book.title">
                      <td>{{ book.rank }}</td>
                      <td class="font-weight-bold text-grey-darken-3">{{ book.title }}</td>
                      <td class="text-right font-weight-black">{{ book.count }}</td>
                    </tr>
                  </tbody>
                </v-table><v-btn block variant="outlined" color="#0d9488" class="mt-4 text-none font-weight-bold rounded-lg">Xem tất cả</v-btn>
              </v-card>
            </v-col>

            <v-col cols="12" lg="4">
              <v-card class="report-panel h-100" elevation="0">
                <div class="d-flex justify-space-between align-center mb-4">
                  <h3 class="panel-title">Thống kê theo khoa / đơn vị</h3>
                  <v-select :items="['Tháng này']" value="Tháng này" variant="outlined" density="compact" hide-details style="max-width: 110px;" class="custom-select" />
                </div>
                
                <div class="progress-list mt-6">
                  <div v-for="dept in deptStats" :key="dept.name" class="mb-4">
                    <div class="d-flex justify-space-between mb-1">
                      <span class="text-caption font-weight-bold text-grey-darken-4">{{ dept.name }}</span>
                      <b class="text-caption">{{ dept.percent }}%</b>
                    </div>
                    <div class="progress-track bg-grey-lighten-3">
                      <div class="progress-fill" :style="{ width: dept.percent + '%' }"></div>
                    </div>
                    <div class="text-caption text-grey-darken-1 mt-1">{{ dept.count }} lượt mượn</div>
                  </div>
                </div>
                <v-btn block variant="outlined" color="#0d9488" class="mt-4 text-none font-weight-bold rounded-lg">Xem tất cả</v-btn>
              </v-card>
            </v-col>
          </v-row>

          <v-card class="report-panel mt-4" elevation="0">
            <div class="d-flex justify-space-between align-center flex-wrap ga-4">
              <div>
                <strong class="text-teal-darken-2">Ghi chú:</strong>
                <p class="text-caption text-grey-darken-1 mb-0 mt-1">Báo cáo được cập nhật đến {{ lastUpdated }}</p>
              </div>
              <div class="d-flex ga-3">
                <v-btn variant="outlined" color="#0d9488" class="text-none font-weight-bold rounded-lg" prepend-icon="mdi-file-pdf-box">Xuất PDF</v-btn>
                <v-btn variant="outlined" color="#0d9488" class="text-none font-weight-bold rounded-lg" prepend-icon="mdi-file-excel" @click="exportReportCsv">Xuất Excel</v-btn>
                <v-btn color="#0d9488" class="text-white text-none font-weight-bold rounded-lg" prepend-icon="mdi-printer" @click="printReport">In báo cáo</v-btn>
              </div>
            </div>
          </v-card>

          <v-card class="report-panel mt-4 mb-4" elevation="0">
            <h3 class="panel-title mb-3">Sự kiện đồng bộ báo cáo - RabbitMQ Consume Log</h3>
            <div class="terminal-log">
              <div v-for="(log, i) in rabbitMqLogs" :key="i">{{ log }}</div>
            </div>
          </v-card>
        </section>

        <section v-if="currentMenu === 'notifications'"><v-card class="pa-10 text-center rounded-xl border mt-4" elevation="0">
            <v-icon size="64" color="teal-lighten-4" class="mb-4">mdi-bell-ring</v-icon>
            <h3 class="text-h5 font-weight-bold text-grey-darken-3">Trung tâm Thông báo</h3>
          </v-card>
        </section>

        <section v-if="currentMenu === 'settings'">
          <v-card class="pa-10 text-center rounded-xl border mt-4" elevation="0">
            <v-icon size="64" color="teal-lighten-4" class="mb-4">mdi-cog</v-icon>
            <h3 class="text-h5 font-weight-bold text-grey-darken-3">Cấu hình Hệ thống</h3>
          </v-card>
        </section>

        <!-- ================= THÔNG BÁO ================= -->
        <div v-if="currentMenu === 'notifications'">
          <v-card class="pa-5 rounded-xl border" elevation="0">
            <h3 class="text-subtitle-1 font-weight-bold mb-4">
              Thông báo hệ thống định danh trung tâm
            </h3>

            <v-list density="comfortable">
              <v-list-item
                v-for="n in 3"
                :key="n"
                :title="'Cảnh báo vi phạm trễ hạn hệ thống #' + n"
                subtitle="Độc giả mang mã thẻ LIB-2026-041 giữ sách quá hạn quy định vượt mức 5 ngày."
                class="border-b mb-1 rounded-lg"
              >
                <template #prepend>
                  <v-icon color="red">mdi-alert-circle-outline</v-icon>
                </template>
              </v-list-item>
            </v-list>
          </v-card>
        </div>

        <!-- ================= CÀI ĐẶT ================= -->
        <div v-if="currentMenu === 'settings'" class="settings-page">
  <!-- SETTINGS TABS -->
  <v-card class="settings-tabs-card" elevation="0">
    <button
      v-for="tab in settingTabs"
      :key="tab.value"
      class="setting-tab"
      :class="{ active: currentSettingTab === tab.value }"
      @click="currentSettingTab = tab.value"
    >
      <v-icon size="22">{{ tab.icon }}</v-icon>
      <span>{{ tab.title }}</span>
    </button>
  </v-card>

  <!-- TAB 1: CẤU HÌNH HỆ THỐNG -->
  <div v-if="currentSettingTab === 'system'" class="settings-grid">
    <!-- THÔNG TIN THƯ VIỆN -->
    <v-card class="setting-card" elevation="0">
      <div class="setting-card-title">
        <v-icon color="#64748b" size="26">mdi-bank-outline</v-icon>
        <h3>Thông tin thư viện</h3>
      </div>

      <div class="form-group">
        <label>Tên thư viện</label>
        <input v-model="librarySettings.name" placeholder="Nhập tên thư viện" />
      </div>

      <div class="form-group">
        <label>Địa chỉ</label>
        <input v-model="librarySettings.address" placeholder="Nhập địa chỉ" />
      </div>

      <div class="form-row">
        <div class="form-group">
          <label>Email</label>
          <input v-model="librarySettings.email" placeholder="Email thư viện" />
        </div>

        <div class="form-group">
          <label>Số điện thoại</label>
          <input v-model="librarySettings.phone" placeholder="Số điện thoại" />
        </div>
      </div>

      <div class="form-group">
        <label>Website</label>
        <input v-model="librarySettings.website" placeholder="Website thư viện" />
      </div>

      <div class="form-group">
        <label>Mô tả</label>
        <textarea v-model="librarySettings.description" rows="3"></textarea>
      </div>

      <button class="save-setting-btn" @click="saveLibrarySettings">
        <v-icon size="18">mdi-content-save-outline</v-icon>
        Lưu thay đổi
      </button>
    </v-card>

    <!-- CÀI ĐẶT CHUNG -->
    <v-card class="setting-card" elevation="0">
      <div class="setting-card-title">
        <v-icon color="#64748b" size="26">mdi-cog-outline</v-icon>
        <h3>Cài đặt chung</h3>
      </div>

      <div class="switch-list">
        <div class="switch-row">
          <span>Cho phép đăng ký tài khoản độc giả</span>
          <v-switch v-model="generalSettings.allowRegister" color="#0d9488" hide-details inset />
        </div>

        <div class="switch-row">
          <span>Yêu cầu duyệt khi đăng ký tài khoản</span>
          <v-switch v-model="generalSettings.needApprove" color="#0d9488" hide-details inset />
        </div>

        <div class="switch-row">
          <span>Gia hạn mượn tự động</span>
          <v-switch v-model="generalSettings.autoRenew" color="#0d9488" hide-details inset />
        </div>

        <div class="form-group mt-3">
          <label>Số ngày gia hạn tối đa</label>
          <div class="input-with-unit">
            <input v-model.number="generalSettings.maxRenewDays" type="number" />
            <span>ngày</span>
          </div>
        </div>

        <div class="switch-row">
          <span>Gửi email nhắc hạn mượn</span>
          <v-switch v-model="generalSettings.sendReminderEmail" color="#0d9488" hide-details inset />
        </div>

        <div class="switch-row">
          <span>Hiển thị sách mới trên trang chủ</span>
          <v-switch v-model="generalSettings.showNewBooks" color="#0d9488" hide-details inset />
        </div>
      </div>

      <button class="save-setting-btn" @click="saveGeneralSettings">
        <v-icon size="18">mdi-content-save-outline</v-icon>
        Lưu thay đổi
      </button>
    </v-card>

    <!-- CÀI ĐẶT MƯỢN TRẢ -->
    <v-card class="setting-card" elevation="0">
      <div class="setting-card-title">
        <v-icon color="#64748b" size="26">mdi-swap-horizontal</v-icon>
        <h3>Cài đặt mượn trả</h3>
      </div>

      <div class="form-group">
        <label>Số ngày mượn tối đa</label>
        <div class="input-with-unit">
          <input v-model.number="borrowSettings.maxBorrowDays" type="number" />
          <span>ngày</span>
        </div>
      </div>

      <div class="form-group">
        <label>Số sách mượn tối đa</label>
        <div class="input-with-unit">
          <input v-model.number="borrowSettings.maxBooks" type="number" />
          <span>quyển</span>
        </div>
      </div>

      <div class="form-group">
        <label>Phạt quá hạn</label>
        <div class="input-with-unit">
          <input v-model.number="borrowSettings.finePerDay" type="number" />
          <span>VNĐ/ngày</span>
        </div>
      </div>

      <div class="form-group">
        <label>Giới hạn quá hạn trước khi khóa tài khoản</label>
        <div class="input-with-unit">
          <input v-model.number="borrowSettings.lockAfterDays" type="number" />
          <span>ngày</span>
        </div>
      </div>

      <button class="save-setting-btn" @click="saveBorrowSettings">
        <v-icon size="18">mdi-content-save-outline</v-icon>
        Lưu thay đổi
      </button>
    </v-card>

    <!-- CÀI ĐẶT THÔNG BÁO -->
    <v-card class="setting-card" elevation="0">
      <div class="setting-card-title">
        <v-icon color="#64748b" size="26">mdi-bell-outline</v-icon>
        <h3>Cài đặt thông báo</h3>
      </div>

      <div class="checkbox-list">
        <label>
          <input v-model="notificationSettings.borrowSuccess" type="checkbox" />
          Thông báo mượn sách thành công
        </label>

        <label>
          <input v-model="notificationSettings.returnSuccess" type="checkbox" />
          Thông báo trả sách thành công
        </label>

        <label>
          <input v-model="notificationSettings.nearDue" type="checkbox" />
          Thông báo sắp hết hạn
        </label>

        <div class="inline-setting">
          <span>Trước</span>
          <input v-model.number="notificationSettings.nearDueDays" type="number" />
          <span>ngày</span>
        </div>

        <label>
          <input v-model="notificationSettings.overdue" type="checkbox" />
          Thông báo quá hạn
        </label>

        <label>
          <input v-model="notificationSettings.newBook" type="checkbox" />
          Thông báo sách mới
        </label>
      </div>

      <button class="save-setting-btn" @click="saveNotificationSettings">
        <v-icon size="18">mdi-content-save-outline</v-icon>
        Lưu thay đổi
      </button>
    </v-card>

    <!-- CÀI ĐẶT GIAO DIỆN -->
    <v-card class="setting-card" elevation="0">
      <div class="setting-card-title">
        <v-icon color="#64748b" size="26">mdi-palette-outline</v-icon>
        <h3>Cài đặt giao diện</h3>
      </div>

      <div class="form-group">
        <label>Chủ đề màu chính</label>
        <div class="color-input">
          <input v-model="uiSettings.primaryColor" type="color" />
          <input v-model="uiSettings.primaryColor" />
        </div>
      </div>

      <div class="form-group">
        <label>Ngôn ngữ hiển thị</label>
        <select v-model="uiSettings.language">
          <option>Tiếng Việt</option>
          <option>English</option>
        </select>
      </div>

      <div class="form-group">
        <label>Số lượng bản ghi trên trang</label>
        <select v-model.number="uiSettings.pageSize">
          <option :value="5">5 bản ghi</option>
          <option :value="10">10 bản ghi</option>
          <option :value="20">20 bản ghi</option>
          <option :value="50">50 bản ghi</option>
        </select>
      </div>

      <button class="save-setting-btn" @click="saveUiSettings">
        <v-icon size="18">mdi-content-save-outline</v-icon>
        Lưu thay đổi
      </button>
    </v-card>

    <!-- BẢO MẬT -->
    <v-card class="setting-card" elevation="0">
      <div class="setting-card-title">
        <v-icon color="#64748b" size="26">mdi-lock-outline</v-icon>
        <h3>Bảo mật</h3>
      </div>

      <div class="form-group">
        <label>Phiên đăng nhập hết hạn sau</label>
        <div class="input-with-unit">
          <input v-model.number="securitySettings.sessionTimeout" type="number" />
          <span>phút</span>
        </div>
      </div>

      <div class="form-group">
        <label>Đổi mật khẩu định kỳ</label>
        <div class="input-with-unit">
          <input v-model.number="securitySettings.passwordExpireDays" type="number" />
          <span>ngày</span>
        </div>
      </div>

      <div class="switch-row">
        <span>Xác thực 2 lớp (2FA)</span>
        <v-switch v-model="securitySettings.twoFactor" color="#0d9488" hide-details inset />
      </div>

      <div class="switch-row">
        <span>Khóa tài khoản khi đăng nhập sai nhiều lần</span>
        <v-switch v-model="securitySettings.lockOnFailedLogin" color="#0d9488" hide-details inset />
      </div>

      <button class="save-setting-btn" @click="saveSecuritySettings">
        <v-icon size="18">mdi-content-save-outline</v-icon>
        Lưu thay đổi
      </button>
    </v-card>
  </div>

  <!-- TAB 2: QUẢN LÝ TÀI KHOẢN -->
  <div v-if="currentSettingTab === 'accounts'">
    <v-card class="setting-card full-card" elevation="0">
      <div class="setting-card-title row-between">
        <div class="d-flex align-center">
          <v-icon color="#64748b" size="26" class="mr-2">mdi-account-group-outline</v-icon>
          <h3>Quản lý tài khoản quản trị</h3>
        </div>

        <button class="save-setting-btn small" @click="openAddAccount">
          <v-icon size="18">mdi-plus</v-icon>
          Thêm tài khoản
        </button>
      </div>

      <table class="setting-table">
        <thead>
          <tr>
            <th>Người dùng</th>
            <th>Email</th>
            <th>Vai trò</th>
            <th>Trạng thái</th>
            <th>Lần đăng nhập cuối</th>
            <th>Thao tác</th>
          </tr>
        </thead>

        <tbody>
          <tr v-for="account in adminAccounts" :key="account.id">
            <td>
              <div class="account-cell">
                <v-avatar size="38">
                  <v-img :src="account.avatar" />
                </v-avatar>

                <div>
                  <strong>{{ account.name }}</strong>
                  <span>{{ account.username }}</span>
                </div>
              </div>
            </td>

            <td>{{ account.email }}</td>
            <td>
              <span class="role-chip">{{ account.role }}</span>
            </td>
            <td>
              <span class="account-status" :class="{ locked: !account.active }">
                {{ account.active ? 'Đang hoạt động' : 'Đã khóa' }}
              </span>
            </td>
            <td>{{ account.lastLogin }}</td>
            <td>
              <div class="setting-actions">
                <button @click="editAccount(account)">
                  <v-icon size="18">mdi-pencil-outline</v-icon>
                </button>

                <button @click="toggleAccount(account)">
                  <v-icon size="18">
                    {{ account.active ? 'mdi-lock-outline' : 'mdi-lock-open-outline' }}
                  </v-icon>
                </button>

                <button class="danger" @click="deleteAccount(account)">
                  <v-icon size="18">mdi-delete-outline</v-icon>
                </button>
              </div>
            </td>
          </tr>
        </tbody>
      </table>
    </v-card>
  </div>

  <!-- TAB 3: PHÂN QUYỀN -->
  <div v-if="currentSettingTab === 'roles'">
    <v-card class="setting-card full-card" elevation="0">
      <div class="setting-card-title">
        <v-icon color="#64748b" size="26">mdi-shield-account-outline</v-icon>
        <h3>Phân quyền chức năng</h3>
      </div>

      <div class="role-grid">
        <div v-for="role in roles" :key="role.name" class="role-card">
          <h4>{{ role.name }}</h4>
          <p>{{ role.description }}</p>

          <label v-for="permission in rolePermissions" :key="permission.value">
            <input
              v-model="role.permissions"
              type="checkbox"
              :value="permission.value"
            />
            {{ permission.title }}
          </label>

          <button class="save-setting-btn small mt-3" @click="saveRole(role)">
            Lưu phân quyền
          </button>
        </div>
      </div>
    </v-card>
  </div>

  <!-- TAB 4: SAO LƯU DỮ LIỆU -->
  <div v-if="currentSettingTab === 'backup'">
    <v-row>
      <v-col cols="12" md="6">
        <v-card class="setting-card h-100" elevation="0">
          <div class="setting-card-title">
            <v-icon color="#64748b" size="26">mdi-database-export-outline</v-icon>
            <h3>Sao lưu dữ liệu</h3>
          </div>

          <p class="setting-note">
            Sao lưu toàn bộ dữ liệu độc giả, thẻ thư viện, mượn trả và báo cáo.
          </p>

          <button class="save-setting-btn" @click="createBackup">
            <v-icon size="18">mdi-database-export-outline</v-icon>
            Tạo bản sao lưu
          </button>

          <button class="outline-setting-btn mt-3" @click="downloadBackup">
            <v-icon size="18">mdi-download</v-icon>
            Tải bản sao lưu gần nhất
          </button>
        </v-card>
      </v-col>

      <v-col cols="12" md="6">
        <v-card class="setting-card h-100" elevation="0">
          <div class="setting-card-title">
            <v-icon color="#64748b" size="26">mdi-database-import-outline</v-icon>
            <h3>Khôi phục dữ liệu</h3>
          </div>

          <p class="setting-note">
            Chọn file sao lưu để khôi phục dữ liệu hệ thống.
          </p>

          <input type="file" class="file-input" @change="selectBackupFile" />

          <button class="save-setting-btn mt-3" @click="restoreBackup">
            <v-icon size="18">mdi-database-import-outline</v-icon>
            Khôi phục dữ liệu
          </button>
        </v-card>
      </v-col>
    </v-row>

    <v-card class="setting-card full-card mt-4" elevation="0">
      <div class="setting-card-title">
        <v-icon color="#64748b" size="26">mdi-history</v-icon>
        <h3>Lịch sử sao lưu</h3>
      </div>

      <table class="setting-table">
        <thead>
          <tr>
            <th>Tên file</th>
            <th>Thời gian</th>
            <th>Dung lượng</th>
            <th>Người tạo</th>
            <th>Thao tác</th>
          </tr>
        </thead>

        <tbody>
          <tr v-for="backup in backupHistory" :key="backup.file">
            <td class="font-weight-bold">{{ backup.file }}</td>
            <td>{{ backup.time }}</td>
            <td>{{ backup.size }}</td>
            <td>{{ backup.createdBy }}</td>
            <td>
              <button class="outline-mini-btn" @click="downloadBackupItem(backup)">
                Tải xuống
              </button>
            </td>
          </tr>
        </tbody>
      </table>
    </v-card>
  </div>

  <!-- TAB 5: NHẬT KÝ HỆ THỐNG -->
  <div v-if="currentSettingTab === 'logs'">
    <v-card class="setting-card full-card" elevation="0">
      <div class="setting-card-title row-between">
        <div class="d-flex align-center">
          <v-icon color="#64748b" size="26" class="mr-2">mdi-file-document-outline</v-icon>
          <h3>Nhật ký hệ thống</h3>
        </div>

        <button class="outline-setting-btn small" @click="clearLogs">
          <v-icon size="18">mdi-delete-outline</v-icon>
          Xóa log
        </button>
      </div>

      <div class="log-filter">
        <select v-model="logLevelFilter">
          <option value="all">Tất cả mức độ</option>
          <option value="INFO">INFO</option>
          <option value="WARN">WARN</option>
          <option value="ERROR">ERROR</option>
        </select>

        <input v-model="logSearch" placeholder="Tìm kiếm log..." />
      </div>

      <div class="system-log-box">
        <div
          v-for="log in filteredSystemLogs"
          :key="log.id"
          class="log-row"
          :class="log.level.toLowerCase()"
        >
          <span>[{{ log.level }}]</span>
          <b>{{ log.time }}</b>
          <p>{{ log.message }}</p>
        </div>
      </div>
    </v-card>
  </div>

  <!-- ACCOUNT DIALOG -->
  <v-dialog v-model="accountDialog" max-width="620">
    <v-card class="pa-6 rounded-xl">
      <div class="dialog-head">
        <h3>{{ editingAccount ? 'Sửa tài khoản' : 'Thêm tài khoản' }}</h3>
        <v-btn icon="mdi-close" variant="text" @click="accountDialog = false" />
      </div>

      <v-row>
        <v-col cols="12" md="6">
          <v-text-field v-model="accountForm.name" label="Họ tên" variant="outlined" color="#0d9488" />
        </v-col>

        <v-col cols="12" md="6">
          <v-text-field v-model="accountForm.username" label="Tên đăng nhập" variant="outlined" color="#0d9488" />
        </v-col>

        <v-col cols="12">
          <v-text-field v-model="accountForm.email" label="Email" variant="outlined" color="#0d9488" />
        </v-col>

        <v-col cols="12" md="6">
          <v-select
            v-model="accountForm.role"
            :items="['Admin', 'Thủ thư', 'Nhân viên báo cáo']"
            label="Vai trò"
            variant="outlined"
            color="#0d9488"
          />
        </v-col>

        <v-col cols="12" md="6">
          <v-select
            v-model="accountForm.active"
            :items="[
              { title: 'Đang hoạt động', value: true },
              { title: 'Đã khóa', value: false }
            ]"
            item-title="title"
            item-value="value"
            label="Trạng thái"
            variant="outlined"
            color="#0d9488"
          />
        </v-col>
      </v-row>

      <div class="d-flex justify-end ga-3 mt-2">
        <v-btn variant="outlined" color="grey" @click="accountDialog = false">
          Hủy
        </v-btn>

        <v-btn color="#0d9488" class="text-white" @click="saveAccount">
          Lưu tài khoản
        </v-btn>
      </div>
    </v-card>
  </v-dialog>
</div>
      </v-container>
    </v-main>
  </v-app>
</template>

<script setup>
import { computed, ref, watch } from 'vue'

const currentMenu = ref('overview')
const quickSearch = ref('')

const menuItems = [
  { title: 'Tổng quan', icon: 'mdi-home', value: 'overview' },
  { title: 'Hồ sơ độc giả', icon: 'mdi-account-circle-outline', value: 'readers' },
  { title: 'Thẻ thư viện', icon: 'mdi-card-account-details-outline', value: 'cards' },
  { title: 'Hồ sơ mượn trả', icon: 'mdi-history', value: 'history' },
  { title: 'Báo cáo thống kê', icon: 'mdi-chart-bar', value: 'reports' },
  { title: 'Thông báo', icon: 'mdi-bell-outline', value: 'notifications' },
  { title: 'Cài đặt', icon: 'mdi-cog-outline', value: 'settings' }
]

const currentMenuTitle = computed(() => {
  return menuItems.find((m) => m.value === currentMenu.value)?.title || 'Dashboard'
})

const sampleCards = ref([
  { id: 'LIB-2026-001', expiry: '31/12/2026', active: true },
  { id: 'LIB-2026-002', expiry: '15/08/2026', active: true },
  { id: 'LIB-2026-043', expiry: '01/05/2026', active: false },
  { id: 'LIB-2026-119', expiry: '14/06/2026', active: true }
])

const returnHistory = ref([
  {
    user: 'Nguyễn Văn Mạnh',
    book: 'Cấu trúc dữ liệu và giải thuật',
    borrowDate: '01/06/2026',
    returnDate: '10/06/2026',
    fine: '0'
  },
  {
    user: 'Lê Thị Cẩm Tú',
    book: 'Kinh tế học vĩ mô',
    borrowDate: '25/05/2026',
    returnDate: '12/06/2026',
    fine: '15.000'
  },
  {
    user: 'Trần Văn Bình',
    book: 'Lập trình hướng đối tượng',
    borrowDate: '05/06/2026',
    returnDate: '14/06/2026',
    fine: '0'
  }
])

const kpiData = [
  {
    title: 'Sách đang cho mượn',
    value: '1,284',
    icon: 'mdi-book-open-variant',
    color: '#3b82f6',
    valueColor: '#3b82f6',
    trend: '8.6%'
  },
  {
    title: 'Đã trả trong tháng',
    value: '946',
    icon: 'mdi-check-circle',
    color: '#10b981',
    valueColor: '#10b981',
    trend: '6.3%'
  },
  {
    title: 'Độc giả vi phạm',
    value: '58',
    icon: 'mdi-alert',
    color: '#ef4444',
    valueColor: '#ef4444',
    trend: '12.1%'
  },
  {
    title: 'Thẻ đang hoạt động',
    value: '3,412',
    icon: 'mdi-card-account-details',
    color: '#f97316',
    valueColor: '#f97316',
    trend: '4.7%'
  }
]

const barChartData = [
  { label: 'T2', borrow: 210, return: 160 },
  { label: 'T3', borrow: 280, return: 190 },
  { label: 'T4', borrow: 320, return: 240 },
  { label: 'T5', borrow: 410, return: 280 },
  { label: 'T6', borrow: 365, return: 260 },
  { label: 'T7', borrow: 290, return: 200 },
  { label: 'CN', borrow: 305, return: 210 }
]

const topBooks = [
  { title: 'Trí tuệ nhân tạo', borrows: 128 },
  { title: 'Lập trình Python', borrows: 114 },
  { title: 'Marketing hiện đại', borrows: 98 }
]

const overdueUsers = [
  { name: 'Nguyễn Thu Hà', book: 'Trí tuệ nhân tạo', days: 5 },
  { name: 'Lê Hoàng Nam', book: 'Marketing hiện đại', days: 12 }
]

// ===================== HỒ SƠ ĐỘC GIẢ =====================

const readerSearch = ref('')
const readerStatusFilter = ref('all')
const readerDepartmentFilter = ref('all')
const readerPage = ref(1)
const readerPerPage = ref(7)

const selectedReaderIds = ref([])
const selectedReader = ref(null)
const readerFormDialog = ref(false)
const readerHistoryDialog = ref(false)
const editingReader = ref(null)
const historyReader = ref(null)

const readerStatusOptions = [
  { title: 'Tất cả trạng thái', value: 'all' },
  { title: 'Đang hoạt động', value: 'active' },
  { title: 'Cảnh báo', value: 'warning' },
  { title: 'Đã khóa', value: 'locked' }
]

const readerDepartmentOptions = [
  { title: 'Tất cả khoa/đơn vị', value: 'all' },
  { title: 'CNTT', value: 'CNTT' },
  { title: 'QTKD', value: 'QTKD' },
  { title: 'Ngôn ngữ Anh', value: 'Ngôn ngữ Anh' },
  { title: 'Luật', value: 'Luật' },
  { title: 'Kế toán', value: 'Kế toán' },
  { title: 'Điện tử', value: 'Điện tử' },
  { title: 'Cơ khí', value: 'Cơ khí' }
]

const readerForm = ref({
  name: '',
  email: '',
  phone: '',
  department: '',
  address: '',
  borrowing: 0,
  violations: 0,
  status: 'active'
})

const readerProfiles = ref([
  {
    id: 'DG000123',
    name: 'Nguyễn Thị Minh Anh',
    email: 'minhanh@gmail.com',
    phone: '0901 234 567',
    department: 'CNTT - K20',
    address: '123 Đường Cầu Giấy, Quận Cầu Giấy, Hà Nội',
    registeredAt: '15/03/2023',
    borrowing: 3,
    violations: 0,
    status: 'active',
    avatar: 'https://i.pravatar.cc/100?img=47'
  },
  {
    id: 'DG000456',
    name: 'Trần Văn Bảo',
    email: 'tranbao98@gmail.com',
    phone: '0902 345 678',
    department: 'QTKD - K21',
    address: '25 Nguyễn Trãi, Thanh Xuân, Hà Nội',
    registeredAt: '20/04/2023',
    borrowing: 1,
    violations: 1,
    status: 'warning',
    avatar: 'https://i.pravatar.cc/100?img=12'
  },
  {
    id: 'DG000789',
    name: 'Lê Hoàng Yến',
    email: 'lehoangyen@gmail.com',
    phone: '0903 456 789',
    department: 'Ngôn ngữ Anh - K22',
    address: '88 Trần Duy Hưng, Hà Nội',
    registeredAt: '01/05/2023',
    borrowing: 0,
    violations: 0,
    status: 'active',
    avatar: 'https://i.pravatar.cc/100?img=32'
  },
  {
    id: 'DG001012',
    name: 'Phạm Minh Đức',
    email: 'phamduc@gmail.com',
    phone: '0904 567 890',
    department: 'CNTT - K20',
    address: '12 Láng Hạ, Hà Nội',
    registeredAt: '12/06/2023',
    borrowing: 4,
    violations: 2,
    status: 'warning',
    avatar: 'https://i.pravatar.cc/100?img=15'
  },
  {
    id: 'DG001345',
    name: 'Vũ Thị Phương Linh',
    email: 'linhvu@gmail.com',
    phone: '0905 678 901',
    department: 'Luật - K21',
    address: '45 Kim Mã, Ba Đình, Hà Nội',
    registeredAt: '18/07/2023',
    borrowing: 0,
    violations: 0,
    status: 'active',
    avatar: 'https://i.pravatar.cc/100?img=49'
  },
  {
    id: 'DG001678',
    name: 'Đỗ Quang Huy',
    email: 'quanghuy@gmail.com',
    phone: '0906 789 012',
    department: 'Cơ khí - K20',
    address: '96 Nguyễn Chí Thanh, Hà Nội',
    registeredAt: '09/08/2023',
    borrowing: 2,
    violations: 3,
    status: 'locked',
    avatar: 'https://i.pravatar.cc/100?img=14'
  },
  {
    id: 'DG002001',
    name: 'Nguyễn Hà My',
    email: 'hamy@gmail.com',
    phone: '0907 890 123',
    department: 'Kế toán - K22',
    address: '77 Hoàng Quốc Việt, Hà Nội',
    registeredAt: '21/09/2023',
    borrowing: 1,
    violations: 0,
    status: 'active',
    avatar: 'https://i.pravatar.cc/100?img=44'
  },
  {
    id: 'DG002145',
    name: 'Hoàng Minh Quân',
    email: 'minhquan@gmail.com',
    phone: '0908 111 222',
    department: 'Điện tử - K21',
    address: '18 Hồ Tùng Mậu, Hà Nội',
    registeredAt: '02/10/2023',
    borrowing: 2,
    violations: 0,
    status: 'active',
    avatar: 'https://i.pravatar.cc/100?img=22'
  }
])

selectedReader.value = readerProfiles.value[0]

const readerStats = computed(() => {
  const total = readerProfiles.value.length
  const active = readerProfiles.value.filter((reader) => reader.status === 'active').length
  const borrowing = readerProfiles.value.filter((reader) => reader.borrowing > 0).length
  const warning = readerProfiles.value.filter(
    (reader) => reader.violations > 0 || reader.status === 'warning' || reader.status === 'locked'
  ).length

  return [
    {
      title: 'Tổng độc giả',
      value: total,
      note: '100%',
      icon: 'mdi-account-group',
      color: '#0d9488',
      bg: 'teal-lighten-5'
    },
    {
      title: 'Đang hoạt động',
      value: active,
      note: total ? `${((active / total) * 100).toFixed(1)}%` : '0%',
      icon: 'mdi-account-check-outline',
      color: '#16a34a',
      bg: 'green-lighten-5'
    },
    {
      title: 'Đang mượn sách',
      value: borrowing,
      note: total ? `${((borrowing / total) * 100).toFixed(1)}%` : '0%',
      icon: 'mdi-book-open-page-variant',
      color: '#2563eb',
      bg: 'blue-lighten-5'
    },
    {
      title: 'Độc giả vi phạm',
      value: warning,
      note: total ? `${((warning / total) * 100).toFixed(1)}%` : '0%',
      icon: 'mdi-alert-outline',
      color: '#ef4444',
      bg: 'red-lighten-5'
    }
  ]
})

const filteredReaders = computed(() => {
  const keyword = `${readerSearch.value} ${quickSearch.value}`.toLowerCase().trim()

  return readerProfiles.value.filter((reader) => {
    const matchKeyword =
      !keyword ||
      reader.id.toLowerCase().includes(keyword) ||
      reader.name.toLowerCase().includes(keyword) ||
      reader.email.toLowerCase().includes(keyword) ||
      reader.phone.toLowerCase().includes(keyword)

    const matchStatus =
      readerStatusFilter.value === 'all' || reader.status === readerStatusFilter.value

    const matchDepartment =
      readerDepartmentFilter.value === 'all' ||
      reader.department.includes(readerDepartmentFilter.value)

    return matchKeyword && matchStatus && matchDepartment
  })
})

const readerTotalPages = computed(() => {
  return Math.max(1, Math.ceil(filteredReaders.value.length / readerPerPage.value))
})

const paginatedReaders = computed(() => {
  const start = (readerPage.value - 1) * readerPerPage.value
  return filteredReaders.value.slice(start, start + readerPerPage.value)
})

const isAllReadersSelected = computed(() => {
  return (
    paginatedReaders.value.length > 0 &&
    paginatedReaders.value.every((reader) => selectedReaderIds.value.includes(reader.id))
  )
})

watch([readerSearch, quickSearch, readerStatusFilter, readerDepartmentFilter, readerPerPage], () => {
  readerPage.value = 1
})

watch(readerTotalPages, () => {
  if (readerPage.value > readerTotalPages.value) {
    readerPage.value = readerTotalPages.value
  }
})

const readerStatusText = (status) => {
  if (status === 'active') return 'Đang hoạt động'
  if (status === 'warning') return 'Cảnh báo'
  if (status === 'locked') return 'Đã khóa'
  return 'Không rõ'
}

const readerStatusColor = (status) => {
  if (status === 'active') {
    return { bg: 'green-lighten-5', text: 'green-darken-2' }
  }

  if (status === 'warning') {
    return { bg: 'orange-lighten-5', text: 'orange-darken-2' }
  }

  if (status === 'locked') {
    return { bg: 'red-lighten-5', text: 'red-darken-2' }
  }

  return { bg: 'grey-lighten-3', text: 'grey-darken-2' }
}

const selectReader = (reader) => {
  selectedReader.value = reader
}

const resetReaderFilter = () => {
  readerSearch.value = ''
  quickSearch.value = ''
  readerStatusFilter.value = 'all'
  readerDepartmentFilter.value = 'all'
  selectedReaderIds.value = []
  readerPage.value = 1
}

const toggleSelectReaderPage = () => {
  if (isAllReadersSelected.value) {
    selectedReaderIds.value = selectedReaderIds.value.filter(
      (id) => !paginatedReaders.value.some((reader) => reader.id === id)
    )
  } else {
    const ids = paginatedReaders.value.map((reader) => reader.id)
    selectedReaderIds.value = Array.from(new Set([...selectedReaderIds.value, ...ids]))
  }
}

const openAddReader = () => {
  editingReader.value = null
  readerForm.value = {
    name: '',
    email: '',
    phone: '',
    department: '',
    address: '',
    borrowing: 0,
    violations: 0,
    status: 'active'
  }
  readerFormDialog.value = true
}

const openEditReader = (reader) => {
  editingReader.value = reader
  readerForm.value = {
    name: reader.name,
    email: reader.email,
    phone: reader.phone,
    department: reader.department,
    address: reader.address,
    borrowing: reader.borrowing,
    violations: reader.violations,
    status: reader.status
  }
  readerFormDialog.value = true
}

const saveReader = () => {
  if (!readerForm.value.name || !readerForm.value.email || !readerForm.value.phone) {
    alert('Vui lòng nhập đầy đủ họ tên, email và số điện thoại.')
    return
  }

  if (editingReader.value) {
    Object.assign(editingReader.value, readerForm.value)
    selectedReader.value = editingReader.value
    alert('Đã cập nhật hồ sơ độc giả.')
  } else {
    const newId = `DG${String(Date.now()).slice(-6)}`
    const newReader = {
      id: newId,
      registeredAt: new Date().toLocaleDateString('vi-VN'),
      avatar: `https://i.pravatar.cc/100?u=${newId}`,
      ...readerForm.value
    }

    readerProfiles.value.unshift(newReader)
    selectedReader.value = newReader
    alert('Đã thêm độc giả mới.')
  }

  readerFormDialog.value = false
}

const toggleReaderLock = (reader) => {
  if (reader.status === 'locked') {
    reader.status = 'active'
    alert(`Đã mở khóa tài khoản ${reader.name}.`)
  } else {
    reader.status = 'locked'
    alert(`Đã khóa tài khoản ${reader.name}.`)
  }

  selectedReader.value = reader
}

const bulkActivateReaders = () => {
  readerProfiles.value.forEach((reader) => {
    if (selectedReaderIds.value.includes(reader.id)) {
      reader.status = 'active'
    }
  })

  selectedReaderIds.value = []
  alert('Đã kích hoạt các hồ sơ đã chọn.')
}

const bulkLockReaders = () => {
  readerProfiles.value.forEach((reader) => {
    if (selectedReaderIds.value.includes(reader.id)) {
      reader.status = 'locked'
    }
  })

  selectedReaderIds.value = []
  alert('Đã khóa các hồ sơ đã chọn.')
}

const bulkDeleteReaders = () => {
  if (selectedReaderIds.value.length === 0) return

  const ok = confirm(`Bạn có chắc muốn xóa ${selectedReaderIds.value.length} hồ sơ độc giả?`)
  if (!ok) return

  readerProfiles.value = readerProfiles.value.filter(
    (reader) => !selectedReaderIds.value.includes(reader.id)
  )
  selectedReaderIds.value = []
  selectedReader.value = readerProfiles.value[0] || null
}

const exportReadersCsv = () => {
  const rows = [
    ['Mã độc giả', 'Họ tên', 'Email', 'Số điện thoại', 'Khoa/Lớp', 'Địa chỉ', 'Ngày đăng ký', 'Sách đang mượn', 'Vi phạm', 'Trạng thái'],
    ...filteredReaders.value.map((reader) => [
      reader.id,
      reader.name,
      reader.email,
      reader.phone,
      reader.department,
      reader.address,
      reader.registeredAt,
      reader.borrowing,
      reader.violations,
      readerStatusText(reader.status)
    ])
  ]

  const csv = rows.map((row) => row.join(',')).join('\n')
  const blob = new Blob(['\ufeff' + csv], { type: 'text/csv;charset=utf-8;' })
  const url = URL.createObjectURL(blob)

  const link = document.createElement('a')
  link.href = url
  link.download = 'danh-sach-ho-so-doc-gia.csv'
  link.click()

  URL.revokeObjectURL(url)
}

const readerBorrowHistory = ref([
  {
    book: 'Trí tuệ nhân tạo',
    borrowedAt: '01/06/2026',
    returnedAt: '10/06/2026',
    status: 'returned',
    statusText: 'Đã trả'
  },
  {
    book: 'Lập trình Python',
    borrowedAt: '05/06/2026',
    returnedAt: 'Chưa trả',
    status: 'late',
    statusText: 'Trễ hạn'
  },
  {
    book: 'Marketing hiện đại',
    borrowedAt: '12/06/2026',
    returnedAt: 'Chưa trả',
    status: 'borrowing',
    statusText: 'Đang mượn'
  }
])

const openReaderHistory = (reader) => {
  historyReader.value = reader
  readerHistoryDialog.value = true
}
// ===================== THẺ THƯ VIỆN =====================

const cardSearch = ref('')
const cardStatusFilter = ref('all')
const cardTypeFilter = ref('all')
const cardDepartmentFilter = ref('all')
const cardPage = ref(1)
const cardPerPage = ref(5)

const selectedCardIds = ref([])
const selectedLibraryCard = ref(null)
const cardFormDialog = ref(false)
const renewCardDialog = ref(false)
const editingCard = ref(null)
const renewMonths = ref(12)

const cardStatusOptions = [
  { title: 'Tất cả', value: 'all' },
  { title: 'Đang hoạt động', value: 'active' },
  { title: 'Sắp hết hạn', value: 'expiring' },
  { title: 'Hết hạn', value: 'expired' },
  { title: 'Đã khóa', value: 'locked' }
]

const cardTypeOptions = [
  { title: 'Tất cả', value: 'all' },
  { title: 'Sinh viên', value: 'Sinh viên' },
  { title: 'Giảng viên', value: 'Giảng viên' },
  { title: 'Cộng tác viên', value: 'Cộng tác viên' },
  { title: 'Thẻ thường', value: 'Thẻ thường' }
]

const cardDepartmentOptions = [
  { title: 'Tất cả', value: 'all' },
  { title: 'CNTT', value: 'CNTT' },
  { title: 'QTKD', value: 'QTKD' },
  { title: 'Ngôn ngữ Anh', value: 'Ngôn ngữ Anh' },
  { title: 'Luật', value: 'Luật' },
  { title: 'Kế toán', value: 'Kế toán' }
]

const cardForm = ref({
  owner: '',
  email: '',
  department: '',
  type: 'Sinh viên',
  issueDate: '',
  expiryDate: '',
  status: 'active'
})

const libraryCards = ref([
  {
    id: 'LIB-2026-001',
    owner: 'Nguyễn Thu Hà',
    email: 'thuha@gmail.com',
    department: 'CNTT - K20',
    type: 'Sinh viên',
    issueDate: '12/01/2026',
    expiryDate: '12/01/2027',
    status: 'active',
    avatar: 'https://i.pravatar.cc/100?img=47'
  },
  {
    id: 'LIB-2026-002',
    owner: 'Lê Hoàng Nam',
    email: 'hoangnam@gmail.com',
    department: 'QTKD - K21',
    type: 'Sinh viên',
    issueDate: '18/01/2026',
    expiryDate: '18/01/2027',
    status: 'active',
    avatar: 'https://i.pravatar.cc/100?img=12'
  },
  {
    id: 'LIB-2026-003',
    owner: 'Phạm Minh Anh',
    email: 'minhanh@gmail.com',
    department: 'Ngôn ngữ Anh - K22',
    type: 'Giảng viên',
    issueDate: '22/01/2026',
    expiryDate: '22/01/2027',
    status: 'expiring',
    avatar: 'https://i.pravatar.cc/100?img=32'
  },
  {
    id: 'LIB-2026-004',
    owner: 'Trần Văn Bình',
    email: 'vanbinh@gmail.com',
    department: 'CNTT - K20',
    type: 'Giảng viên',
    issueDate: '02/02/2026',
    expiryDate: '02/02/2026',
    status: 'expired',
    avatar: 'https://i.pravatar.cc/100?img=14'
  },
  {
    id: 'LIB-2026-005',
    owner: 'Đỗ Thị Lan',
    email: 'dothilan@gmail.com',
    department: 'Luật - K21',
    type: 'Cộng tác viên',
    issueDate: '15/02/2026',
    expiryDate: '15/02/2027',
    status: 'active',
    avatar: 'https://i.pravatar.cc/100?img=49'
  },
  {
    id: 'LIB-2026-006',
    owner: 'Nguyễn Văn Khánh',
    email: 'vankhanh@gmail.com',
    department: 'Kế toán - K22',
    type: 'Thẻ thường',
    issueDate: '20/02/2026',
    expiryDate: '20/02/2027',
    status: 'locked',
    avatar: 'https://i.pravatar.cc/100?img=22'
  },
  {
    id: 'LIB-2026-007',
    owner: 'Vũ Thị Phương Linh',
    email: 'linhvu@gmail.com',
    department: 'CNTT - K21',
    type: 'Sinh viên',
    issueDate: '01/03/2026',
    expiryDate: '01/03/2027',
    status: 'active',
    avatar: 'https://i.pravatar.cc/100?img=44'
  }
])

selectedLibraryCard.value = libraryCards.value[0]

const libraryCardStats = computed(() => {
  const total = libraryCards.value.length
  const active = libraryCards.value.filter((card) => card.status === 'active').length
  const expiring = libraryCards.value.filter((card) => card.status === 'expiring').length
  const bad = libraryCards.value.filter((card) => card.status === 'expired' || card.status === 'locked').length

  const ratio = (value) => (total ? `${((value / total) * 100).toFixed(1)}%` : '0%')

  return [
    { title: 'Tổng thẻ', value: total, note: '100%', icon: 'mdi-card-account-details-outline', color: '#0d9488', bg: 'teal-lighten-5' },
    { title: 'Đang hoạt động', value: active, note: ratio(active), icon: 'mdi-check-decagram-outline', color: '#16a34a', bg: 'green-lighten-5' },
    { title: 'Sắp hết hạn', value: expiring, note: ratio(expiring), icon: 'mdi-calendar-alert-outline', color: '#f97316', bg: 'orange-lighten-5' },
    { title: 'Hết hạn / Khóa', value: bad, note: ratio(bad), icon: 'mdi-lock-alert-outline', color: '#ef4444', bg: 'red-lighten-5' }
  ]
})

const filteredLibraryCards = computed(() => {
  const keyword = `${cardSearch.value} ${currentMenu.value === 'cards' ? quickSearch.value : ''}`.toLowerCase().trim()

  return libraryCards.value.filter((card) => {
    const matchKeyword =
      !keyword ||
      card.id.toLowerCase().includes(keyword) ||
      card.owner.toLowerCase().includes(keyword) ||
      card.email.toLowerCase().includes(keyword)

    const matchStatus = cardStatusFilter.value === 'all' || card.status === cardStatusFilter.value
    const matchType = cardTypeFilter.value === 'all' || card.type === cardTypeFilter.value
    const matchDepartment = cardDepartmentFilter.value === 'all' || card.department.includes(cardDepartmentFilter.value)

    return matchKeyword && matchStatus && matchType && matchDepartment
  })
})

const cardTotalPages = computed(() => Math.max(1, Math.ceil(filteredLibraryCards.value.length / cardPerPage.value)))

const paginatedLibraryCards = computed(() => {
  const start = (cardPage.value - 1) * cardPerPage.value
  return filteredLibraryCards.value.slice(start, start + cardPerPage.value)
})

const isAllCardsSelected = computed(() => {
  return (
    paginatedLibraryCards.value.length > 0 &&
    paginatedLibraryCards.value.every((card) => selectedCardIds.value.includes(card.id))
  )
})

watch([cardSearch, quickSearch, cardStatusFilter, cardTypeFilter, cardDepartmentFilter, cardPerPage], () => {
  cardPage.value = 1
})

watch(cardTotalPages, () => {
  if (cardPage.value > cardTotalPages.value) cardPage.value = cardTotalPages.value
})

const cardQuickActions = computed(() => [
  { title: 'Cấp thẻ mới', icon: 'mdi-card-plus-outline', color: '#0d9488', handler: openAddCard },
  { title: 'Gia hạn thẻ', icon: 'mdi-clock-plus-outline', color: '#2563eb', handler: () => openRenewCard(selectedLibraryCard.value) },
  { title: selectedLibraryCard.value?.status === 'locked' ? 'Mở khóa' : 'Khóa thẻ', icon: 'mdi-lock-outline', color: '#f97316', handler: () => toggleCardLock(selectedLibraryCard.value) },
  { title: 'In thẻ', icon: 'mdi-printer-outline', color: '#7c3aed', handler: () => printCard(selectedLibraryCard.value) },
  { title: 'Xuất Excel', icon: 'mdi-download-outline', color: '#0d9488', handler: exportCardsCsv }
])

const cardStatusText = (status) => {
  if (status === 'active') return 'Đang hoạt động'
  if (status === 'expiring') return 'Sắp hết hạn'
  if (status === 'expired') return 'Hết hạn'
  if (status === 'locked') return 'Đã khóa'
  return 'Không rõ'
}

const cardStatusColor = (status) => {
  if (status === 'active') return { bg: 'green-lighten-5', text: 'green-darken-2' }
  if (status === 'expiring') return { bg: 'orange-lighten-5', text: 'orange-darken-2' }
  if (status === 'expired') return { bg: 'red-lighten-5', text: 'red-darken-2' }
  if (status === 'locked') return { bg: 'grey-lighten-3', text: 'grey-darken-2' }
  return { bg: 'grey-lighten-3', text: 'grey-darken-2' }
}

const cardTypeColor = (type) => {
  if (type === 'Sinh viên') return { bg: 'teal-lighten-5', text: 'teal-darken-3' }
  if (type === 'Giảng viên') return { bg: 'blue-lighten-5', text: 'blue-darken-2' }
  if (type === 'Cộng tác viên') return { bg: 'deep-purple-lighten-5', text: 'deep-purple-darken-2' }
  return { bg: 'grey-lighten-3', text: 'grey-darken-2' }
}

const qrFill = (n, seed = '') => {
  const code = seed.split('').reduce((sum, char) => sum + char.charCodeAt(0), 0)
  return (n * 7 + code) % 5 !== 0
}

const selectLibraryCard = (card) => {
  selectedLibraryCard.value = card
}

const resetCardFilter = () => {
  cardSearch.value = ''
  quickSearch.value = ''
  cardStatusFilter.value = 'all'
  cardTypeFilter.value = 'all'
  cardDepartmentFilter.value = 'all'
  selectedCardIds.value = []
  cardPage.value = 1
}

const toggleSelectCardPage = () => {
  if (isAllCardsSelected.value) {
    selectedCardIds.value = selectedCardIds.value.filter(
      (id) => !paginatedLibraryCards.value.some((card) => card.id === id)
    )
  } else {
    const ids = paginatedLibraryCards.value.map((card) => card.id)
    selectedCardIds.value = Array.from(new Set([...selectedCardIds.value, ...ids]))
  }
}

const openAddCard = () => {
  editingCard.value = null
  cardForm.value = {
    owner: '',
    email: '',
    department: '',
    type: 'Sinh viên',
    issueDate: new Date().toLocaleDateString('vi-VN'),
    expiryDate: addMonthsToVNDate(new Date().toLocaleDateString('vi-VN'), 12),
    status: 'active'
  }
  cardFormDialog.value = true
}

const openEditCard = (card) => {
  editingCard.value = card
  cardForm.value = {
    owner: card.owner,
    email: card.email,
    department: card.department,
    type: card.type,
    issueDate: card.issueDate,
    expiryDate: card.expiryDate,
    status: card.status
  }
  selectedLibraryCard.value = card
  cardFormDialog.value = true
}

const saveLibraryCard = () => {
  if (!cardForm.value.owner || !cardForm.value.email || !cardForm.value.expiryDate) {
    alert('Vui lòng nhập đầy đủ chủ thẻ, email và ngày hết hạn.')
    return
  }

  if (editingCard.value) {
    Object.assign(editingCard.value, cardForm.value)
    selectedLibraryCard.value = editingCard.value
    alert('Đã cập nhật thẻ thư viện.')
  } else {
    const newId = `LIB-2026-${String(libraryCards.value.length + 1).padStart(3, '0')}`
    const newCard = {
      id: newId,
      avatar: `https://i.pravatar.cc/100?u=${newId}`,
      ...cardForm.value
    }
    libraryCards.value.unshift(newCard)
    selectedLibraryCard.value = newCard
    alert('Đã cấp thẻ thư viện mới.')
  }

  cardFormDialog.value = false
}

const openRenewCard = (card) => {
  if (!card) return
  selectedLibraryCard.value = card
  renewMonths.value = 12
  renewCardDialog.value = true
}

const renewSelectedCard = () => {
  if (!selectedLibraryCard.value) return
  selectedLibraryCard.value.expiryDate = addMonthsToVNDate(selectedLibraryCard.value.expiryDate, renewMonths.value)
  selectedLibraryCard.value.status = 'active'
  renewCardDialog.value = false
  alert(`Đã gia hạn thẻ ${selectedLibraryCard.value.id} thêm ${renewMonths.value} tháng.`)
}

const addMonthsToVNDate = (dateText, months) => {
  const parts = dateText.split('/').map(Number)
  const day = parts[0] || 1
  const month = (parts[1] || 1) - 1
  const year = parts[2] || new Date().getFullYear()
  const date = new Date(year, month, day)
  date.setMonth(date.getMonth() + Number(months || 0))
  return date.toLocaleDateString('vi-VN')
}

const toggleCardLock = (card) => {
  if (!card) return
  if (card.status === 'locked') {
    card.status = 'active'
    alert(`Đã mở khóa thẻ ${card.id}.`)
  } else {
    card.status = 'locked'
    alert(`Đã khóa thẻ ${card.id}.`)
  }
  selectedLibraryCard.value = card
}

const printCard = (card) => {
  if (!card) return
  selectedLibraryCard.value = card
  alert(`Đang gửi thẻ ${card.id} của ${card.owner} tới máy in.`)
}

const bulkActivateCards = () => {
  libraryCards.value.forEach((card) => {
    if (selectedCardIds.value.includes(card.id)) card.status = 'active'
  })
  selectedCardIds.value = []
  alert('Đã kích hoạt các thẻ đã chọn.')
}

const bulkLockCards = () => {
  libraryCards.value.forEach((card) => {
    if (selectedCardIds.value.includes(card.id)) card.status = 'locked'
  })
  selectedCardIds.value = []
  alert('Đã khóa các thẻ đã chọn.')
}

const bulkPrintCards = () => {
  alert(`Đã gửi ${selectedCardIds.value.length} thẻ tới máy in.`)
  selectedCardIds.value = []
}

const bulkDeleteCards = () => {
  if (selectedCardIds.value.length === 0) return
  const ok = confirm(`Bạn có chắc muốn xóa ${selectedCardIds.value.length} thẻ thư viện?`)
  if (!ok) return
  libraryCards.value = libraryCards.value.filter((card) => !selectedCardIds.value.includes(card.id))
  selectedCardIds.value = []
  selectedLibraryCard.value = libraryCards.value[0] || null
}

const exportCardsCsv = () => {
  const rows = [
    ['Mã thẻ', 'Chủ thẻ', 'Email', 'Khoa/Lớp', 'Loại thẻ', 'Ngày cấp', 'Ngày hết hạn', 'Trạng thái'],
    ...filteredLibraryCards.value.map((card) => [
      card.id,
      card.owner,
      card.email,
      card.department,
      card.type,
      card.issueDate,
      card.expiryDate,
      cardStatusText(card.status)
    ])
  ]

  const csv = rows.map((row) => row.join(',')).join('\n')
  const blob = new Blob(['\ufeff' + csv], { type: 'text/csv;charset=utf-8;' })
  const url = URL.createObjectURL(blob)
  const link = document.createElement('a')
  link.href = url
  link.download = 'danh-sach-the-thu-vien.csv'
  link.click()
  URL.revokeObjectURL(url)
}

// ===================== BÁO CÁO THỐNG KÊ =====================

// ================= DỮ LIỆU BÁO CÁO THỐNG KÊ (REPORTS) =================
const reportType = ref('Tổng hợp')
const reportPeriod = ref('Tháng này')
const reportFrom = ref('2026-06-01')
const reportTo = ref('2026-06-15')
const lastUpdated = ref('15/06/2026 - 03:54 AM')

const reportKpis = ref([
  { title: 'Tổng lượt mượn', value: '1,248', trend: '↑ 12.5% so với kỳ trước', icon: 'mdi-book-open-page-variant', color: '#0d9488', bg: '#ccfbf1' },
  { title: 'Đã trả đúng hạn', value: '1,086', trend: '87.0% tổng lượt mượn', icon: 'mdi-keyboard-return', color: '#2563eb', bg: '#dbeafe' },
  { title: 'Đang mượn', value: '86', trend: '↑ 8.3% so với kỳ trước', icon: 'mdi-clock-outline', color: '#f97316', bg: '#ffedd5' },
  { title: 'Quá hạn', value: '76', trend: '↓ 5.0% so với kỳ trước', icon: 'mdi-alert-circle-outline', color: '#ef4444', bg: '#fee2e2', danger: true },
  { title: 'Độc giả mới', value: '42', trend: '↑ 16.7% so với kỳ trước', icon: 'mdi-account-group-outline', color: '#a855f7', bg: '#f3e8ff' },
  { title: 'Sách mới nhập', value: '28', trend: '↑ 27.3% so với kỳ trước', icon: 'mdi-book-plus-outline', color: '#14b8a6', bg: '#ccfbf1' }
])

const categoryStats = ref([
  { name: 'Khoa học', percent: 28, color: '#0d9488' },
  { name: 'Kỹ năng', percent: 22, color: '#2563eb' },
  { name: 'Văn học', percent: 18, color: '#f97316' },
  { name: 'Kinh tế', percent: 15, color: '#a855f7' },
  { name: 'Lịch sử', percent: 10, color: '#ef4444' },
  { name: 'Khác', percent: 7, color: '#9ca3af' }
])

const reportTopBooks = ref([
  { title: 'Lập trình Python cơ bản', count: 125, percent: 100 },
  { title: 'Nhà giả kim', count: 98, percent: 78 },
  { title: 'Clean Code', count: 86, percent: 68 },
  { title: 'Đắc nhân tâm', count: 75, percent: 60 },
  { title: 'Marketing căn bản', count: 62, percent: 49 }
])

const topReaders = ref([
  { rank: 1, name: 'Nguyễn Thu Hà', id: 'DG000123', count: 18 },
  { rank: 2, name: 'Lê Hoàng Nam', id: 'DG000456', count: 15 },
  { rank: 3, name: 'Phạm Minh Anh', id: 'DG000789', count: 12 },
  { rank: 4, name: 'Trần Văn Bình', id: 'DG001012', count: 10 },
  { rank: 5, name: 'Đỗ Thị Lan', id: 'DG001345', count: 9 }
])

const topBooksDetail = ref([
  { rank: 1, title: 'Lập trình Python cơ bản', count: 125 },
  { rank: 2, title: 'Nhà giả kim', count: 98 },
  { rank: 3, title: 'Clean Code', count: 86 },
  { rank: 4, title: 'Đắc nhân tâm', count: 75 },
  { rank: 5, title: 'Marketing căn bản', count: 62 }
])

const deptStats = ref([
  { name: 'CNTT - K20', count: 456, percent: 36.5 },
  { name: 'QTKD - K21', count: 298, percent: 23.9 },
  { name: 'Ngôn ngữ Anh - K22', count: 245, percent: 19.6 },
  { name: 'Luật - K21', count: 142, percent: 11.4 },
  { name: 'Kế toán - K20', count: 85, percent: 6.8 }
])

const rabbitMqLogs = ref(['[INFO] 03:01:22 - Consumed BookBorrowedEvent from Exchange...',
  '[INFO] 03:04:10 - Consumed BookReturnedEvent from Exchange...',
  '[INFO] 03:07:55 - Consumed ReaderStatusChangedEvent from Identity Service...',
  '[INFO] 03:10:18 - Successfully compiled report dataset grid.'
])

const generateReport = () => {
  lastUpdated.value = new Date().toLocaleString('vi-VN')
  rabbitMqLogs.value.push(`[INFO] ${new Date().toLocaleTimeString('vi-VN')} - Cập nhật dữ liệu từ hệ thống thành công.`)
  alert('Đã cập nhật số liệu báo cáo.')
}

const printReport = () => {
  window.print()
}

const exportReportCsv = () => {
  const rows = [
    ['Chỉ số', 'Giá trị', 'Trạng thái'],
    ...reportKpis.value.map(k => [k.title, k.value, k.trend])
  ]
  const csv = rows.map((row) => row.join(',')).join('\n')
  const blob = new Blob(['\ufeff' + csv], { type: 'text/csv;charset=utf-8;' })
  const url = URL.createObjectURL(blob)
  const link = document.createElement('a')
  link.href = url
  link.download = 'thong-ke.csv'
  link.click()
  URL.revokeObjectURL(url)
}

// ===================== CÀI ĐẶT HỆ THỐNG =====================

const currentSettingTab = ref('system')

const settingTabs = [
  { title: 'Cấu hình hệ thống', value: 'system', icon: 'mdi-cog-outline' },
  { title: 'Quản lý tài khoản', value: 'accounts', icon: 'mdi-account-outline' },
  { title: 'Phân quyền', value: 'roles', icon: 'mdi-shield-account-outline' },
  { title: 'Sao lưu dữ liệu', value: 'backup', icon: 'mdi-database-outline' },
  { title: 'Nhật ký hệ thống', value: 'logs', icon: 'mdi-file-document-outline' }
]

const librarySettings = ref({
  name: 'Thư viện Trường Đại học Công nghệ',
  address: '144 Xuân Thủy, Cầu Giấy, Hà Nội',
  email: 'thuvien@unet.edu.vn',
  phone: '024 1234 5678',
  website: 'https://library.unet.edu.vn',
  description: 'Hệ thống quản lý thư viện số - Cung cấp tài nguyên học tập và nghiên cứu cho sinh viên và giảng viên.'
})

const generalSettings = ref({
  allowRegister: true,
  needApprove: true,
  autoRenew: true,
  maxRenewDays: 3,
  sendReminderEmail: false,
  showNewBooks: true
})

const borrowSettings = ref({
  maxBorrowDays: 14,
  maxBooks: 5,
  finePerDay: 1000,
  lockAfterDays: 30
})

const notificationSettings = ref({
  borrowSuccess: true,
  returnSuccess: true,
  nearDue: true,
  nearDueDays: 2,
  overdue: true,
  newBook: false
})

const uiSettings = ref({
  primaryColor: '#0d9488',
  language: 'Tiếng Việt',
  pageSize: 10
})

const securitySettings = ref({
  sessionTimeout: 30,
  passwordExpireDays: 90,
  twoFactor: false,
  lockOnFailedLogin: true
})

const adminAccounts = ref([
  {
    id: 1,
    name: 'Admin Thư viện',
    username: 'admin',
    email: 'admin@library.com',
    role: 'Admin',
    active: true,
    lastLogin: '15/06/2026 04:20',
    avatar: 'https://i.pravatar.cc/100?img=12'
  },
  {
    id: 2,
    name: 'Nguyễn Thu Hà',
    username: 'thuthu01',
    email: 'thuthu01@library.com',
    role: 'Thủ thư',
    active: true,
    lastLogin: '14/06/2026 21:10',
    avatar: 'https://i.pravatar.cc/100?img=47'
  },
  {
    id: 3,
    name: 'Lê Hoàng Nam',
    username: 'report01',
    email: 'report01@library.com',
    role: 'Nhân viên báo cáo',
    active: false,
    lastLogin: '10/06/2026 09:35',
    avatar: 'https://i.pravatar.cc/100?img=15'
  }
])

const accountDialog = ref(false)
const editingAccount = ref(null)

const accountForm = ref({
  name: '',
  username: '',
  email: '',
  role: 'Thủ thư',
  active: true
})

const rolePermissions = [
  { title: 'Xem dashboard', value: 'view_dashboard' },
  { title: 'Quản lý độc giả', value: 'manage_readers' },
  { title: 'Quản lý thẻ thư viện', value: 'manage_cards' },
  { title: 'Quản lý mượn trả', value: 'manage_borrow' },
  { title: 'Xem báo cáo', value: 'view_reports' },
  { title: 'Cấu hình hệ thống', value: 'manage_settings' }
]

const roles = ref([
  {
    name: 'Admin',
    description: 'Toàn quyền quản trị hệ thống',
    permissions: ['view_dashboard', 'manage_readers', 'manage_cards', 'manage_borrow', 'view_reports', 'manage_settings']
  },
  {
    name: 'Thủ thư',
    description: 'Quản lý độc giả, thẻ và mượn trả',
    permissions: ['view_dashboard', 'manage_readers', 'manage_cards', 'manage_borrow']
  },
  {
    name: 'Nhân viên báo cáo',
    description: 'Chỉ xem và xuất báo cáo thống kê',
    permissions: ['view_dashboard', 'view_reports']
  }
])

const backupFile = ref(null)

const backupHistory = ref([
  {
    file: 'backup-library-2026-06-15.zip',
    time: '15/06/2026 03:00',
    size: '25.4 MB',
    createdBy: 'Admin Thư viện'
  },
  {
    file: 'backup-library-2026-06-14.zip',
    time: '14/06/2026 03:00',
    size: '24.9 MB',
    createdBy: 'Hệ thống'
  },
  {
    file: 'backup-library-2026-06-13.zip',
    time: '13/06/2026 03:00',
    size: '24.1 MB',
    createdBy: 'Hệ thống'
  }
])

const logLevelFilter = ref('all')
const logSearch = ref('')

const systemLogs = ref([
  {
    id: 1,
    level: 'INFO',
    time: '15/06/2026 04:20:11',
    message: 'Admin Thư viện đăng nhập hệ thống.'
  },
  {
    id: 2,
    level: 'INFO',
    time: '15/06/2026 04:21:08',
    message: 'Cập nhật cấu hình mượn trả thành công.'
  },
  {
    id: 3,
    level: 'WARN',
    time: '15/06/2026 04:25:32',
    message: 'Phát hiện 5 độc giả quá hạn cần xử lý.'
  },
  {
    id: 4,
    level: 'ERROR',
    time: '15/06/2026 04:28:40',
    message: 'Không thể gửi email nhắc hạn do SMTP chưa cấu hình.'
  }
])

const filteredSystemLogs = computed(() => {
  const keyword = logSearch.value.toLowerCase().trim()

  return systemLogs.value.filter((log) => {
    const matchLevel = logLevelFilter.value === 'all' || log.level === logLevelFilter.value
    const matchKeyword =
      !keyword ||
      log.message.toLowerCase().includes(keyword) ||
      log.time.toLowerCase().includes(keyword)

    return matchLevel && matchKeyword
  })
})

const saveLibrarySettings = () => {
  alert('Đã lưu thông tin thư viện.')
}

const saveGeneralSettings = () => {
  alert('Đã lưu cài đặt chung.')
}

const saveBorrowSettings = () => {
  alert('Đã lưu cài đặt mượn trả.')
}

const saveNotificationSettings = () => {
  alert('Đã lưu cài đặt thông báo.')
}

const saveUiSettings = () => {
  document.documentElement.style.setProperty('--main-color', uiSettings.value.primaryColor)
  alert('Đã lưu cài đặt giao diện.')
}

const saveSecuritySettings = () => {
  alert('Đã lưu cài đặt bảo mật.')
}

const openAddAccount = () => {
  editingAccount.value = null
  accountForm.value = {
    name: '',
    username: '',
    email: '',
    role: 'Thủ thư',
    active: true
  }
  accountDialog.value = true
}

const editAccount = (account) => {
  editingAccount.value = account
  accountForm.value = {
    name: account.name,
    username: account.username,
    email: account.email,
    role: account.role,
    active: account.active
  }
  accountDialog.value = true
}

const saveAccount = () => {
  if (!accountForm.value.name || !accountForm.value.username || !accountForm.value.email) {
    alert('Vui lòng nhập đầy đủ thông tin tài khoản.')
    return
  }

  if (editingAccount.value) {
    Object.assign(editingAccount.value, accountForm.value)
    alert('Đã cập nhật tài khoản.')
  } else {
    adminAccounts.value.push({
      id: Date.now(),
      lastLogin: 'Chưa đăng nhập',
      avatar: `https://i.pravatar.cc/100?u=${Date.now()}`,
      ...accountForm.value
    })
    alert('Đã thêm tài khoản mới.')
  }

  accountDialog.value = false
}

const toggleAccount = (account) => {
  account.active = !account.active
  alert(account.active ? 'Đã mở khóa tài khoản.' : 'Đã khóa tài khoản.')
}

const deleteAccount = (account) => {
  const ok = confirm(`Bạn có chắc muốn xóa tài khoản ${account.name}?`)
  if (!ok) return

  adminAccounts.value = adminAccounts.value.filter((item) => item.id !== account.id)
}

const saveRole = (role) => {
  alert(`Đã lưu phân quyền cho vai trò ${role.name}.`)
}

const createBackup = () => {
  const fileName = `backup-library-${new Date().toISOString().slice(0, 10)}.zip`
  backupHistory.value.unshift({
    file: fileName,
    time: new Date().toLocaleString('vi-VN'),
    size: '26.1 MB',
    createdBy: 'Admin Thư viện'
  })
  alert('Đã tạo bản sao lưu dữ liệu.')
}

const downloadBackup = () => {
  alert('Đang tải bản sao lưu gần nhất.')
}

const downloadBackupItem = (backup) => {
  alert(`Đang tải ${backup.file}.`)
}

const selectBackupFile = (event) => {
  backupFile.value = event.target.files[0]
}

const restoreBackup = () => {
  if (!backupFile.value) {
    alert('Vui lòng chọn file sao lưu trước khi khôi phục.')
    return
  }

  alert(`Đã khôi phục dữ liệu từ file ${backupFile.value.name}.`)
}

const clearLogs = () => {
  const ok = confirm('Bạn có chắc muốn xóa toàn bộ nhật ký hệ thống?')
  if (!ok) return

  systemLogs.value = []
}

</script>

<style scoped>
* {
  font-family: 'Inter', sans-serif !important;
}

.tracking-tight {
  letter-spacing: -0.5px;
}

.active-menu-item {
  background-color: #0d9488 !important;
}

.user-profile-box {
  background-color: rgba(255, 255, 255, 0.05);
  cursor: pointer;
}

.user-profile-box:hover {
  background-color: rgba(255, 255, 255, 0.1);
}

.search-input :deep(.v-field--variant-outlined) {
  border-color: #cbd5e1;
}

/* CSS BAR CHART */
.legend-square {
  width: 10px;
  height: 10px;
  border-radius: 2px;
  display: inline-block;
}

.chart-wrapper {
  height: 180px;
}

.css-bar-chart-container {
  height: 100%;
  border-bottom: 1px solid #e2e8f0;
}

.grid-lines {
  position: absolute;
  inset: 0;
  display: flex;
  flex-direction: column;
  justify-content: space-between;
}

.grid-line {
  width: 100%;
  border-top: 1px dashed #e2e8f0;
}

.bars-area {
  position: absolute;
  inset: 0;
  display: flex;
  justify-content: space-around;
}

.bar-group {
  display: flex;
  flex-direction: column;
  align-items: center;
  justify-content: flex-end;
  height: 100%;
  width: 50px;
}

.bars {
  display: flex;
  align-items: flex-end;
  justify-content: center;
  height: calc(100% - 24px);
  width: 100%;
  gap: 6px;
}

.bar {
  width: 14px;
  border-radius: 2px 2px 0 0;
  position: relative;
}

.borrow-bar {
  background-color: #0d9488;
}

.return-bar {
  background-color: #f97316;
}

.bar-value {
  position: absolute;
  top: -18px;
  left: 50%;
  transform: translateX(-50%);
  font-size: 10px;
  font-weight: 700;
  color: #0d9488;
}

.return-bar .bar-value {
  color: #f97316;
}

.day-label {
  height: 24px;
  display: flex;
  align-items: flex-end;
  padding-bottom: 2px;
  font-size: 11px;
  font-weight: 600;
  color: #64748b;
}

/* CSS DONUT CHART */
.css-donut-chart {
  width: 150px;
  height: 150px;
  border-radius: 50%;
  background: conic-gradient(#0d9488 0% 87%, #ef4444 87% 100%);
  display: flex;
  align-items: center;
  justify-content: center;
}

.donut-hole {
  width: 110px;
  height: 110px;
  background-color: white;
  border-radius: 50%;
}

/* CUSTOM TABLES */
.table-custom :deep(th),
.reader-table :deep(th) {
  border-bottom: 1px solid #e2e8f0 !important;
  background-color: #f8fafc !important;
  height: 38px !important;
}

.table-custom :deep(td),
.reader-table :deep(td) {
  border-bottom: 1px solid #f1f5f9 !important;
  padding-top: 8px !important;
  padding-bottom: 8px !important;
}

.reader-table-wrapper {
  overflow-x: auto;
}

.reader-table {
  min-width: 960px;
}

.selected-reader-row {
  background-color: #ecfdf5 !important;
}

.reader-stat-card {
  min-height: 112px;
}

.bulk-reader-bar {
  padding: 14px 16px;
  border-radius: 14px;
  background: #f8fafc;
  border: 1px solid #e2e8f0;
  display: flex;
  align-items: center;
  justify-content: space-between;
  gap: 12px;
  flex-wrap: wrap;
}

.bulk-reader-bar span {
  font-weight: 800;
  color: #334155;
}

.detail-profile-card {
  position: sticky;
  top: 10px;
}

.profile-detail-list {
  display: grid;
  gap: 0;
}

.profile-detail-list div {
  display: grid;
  grid-template-columns: 110px 1fr;
  gap: 10px;
  padding: 11px 0;
  border-bottom: 1px solid #edf2f7;
}

.profile-detail-list span {
  color: #64748b;
  font-size: 12px;
  font-weight: 700;
}

.profile-detail-list strong {
  color: #111827;
  font-size: 12px;
  font-weight: 850;
}
/* ================= THẺ THƯ VIỆN ================= */
.library-card-preview {
  position: relative;
  min-height: 300px;
  border-radius: 22px;
  padding: 24px;
  overflow: hidden;
  color: white;
  background:
    radial-gradient(circle at right top, rgba(45, 212, 191, 0.35), transparent 34%),
    linear-gradient(135deg, #064e40 0%, #075948 48%, #04362f 100%);
  box-shadow: 0 20px 42px rgba(6, 78, 64, 0.22);
}

.preview-wave {
  position: absolute;
  right: -80px;
  top: 25px;
  width: 330px;
  height: 160px;
  border-radius: 50%;
  border: 1px solid rgba(204, 251, 241, 0.28);
  transform: rotate(-18deg);
}

.preview-wave::after {
  content: "";
  position: absolute;
  inset: 28px;
  border-radius: 50%;
  border: 1px solid rgba(204, 251, 241, 0.18);
}

.preview-head,
.preview-body,
.preview-footer {
  position: relative;
  z-index: 2;
}

.preview-head {
  display: flex;
  align-items: center;
  gap: 14px;
  margin-bottom: 18px;
}

.preview-logo {
  width: 46px;
  height: 46px;
  border-radius: 12px;
  background: white;
  display: grid;
  place-items: center;
}

.preview-head h3 {
  margin: 0;
  font-size: 18px;
  font-weight: 950;
  letter-spacing: 0.4px;
}

.preview-head p {
  margin: 2px 0 0;
  color: #ccfbf1;
}

.preview-body {
  display: grid;
  grid-template-columns: 112px 1fr 104px;
  align-items: center;
  gap: 18px;
  border-radius: 18px;
  background: rgba(255, 255, 255, 0.92);
  color: #064e40;
  padding: 18px;
}

.preview-avatar {
  border-radius: 16px !important;
  border: 4px solid rgba(13, 148, 136, 0.15);
}

.preview-info h2 {
  margin: 0 0 6px;
  color: #064e40;
  font-size: 24px;
  font-weight: 950;
  letter-spacing: -0.5px;
}

.preview-info h4 {
  margin: 0 0 10px;
  color: #0f172a;
  font-size: 17px;
  font-weight: 950;
}

.preview-info p {
  margin: 0 0 12px;
  display: flex;
  align-items: center;
  gap: 6px;
  color: #334155;
  font-weight: 700;
}

.preview-dates {
  display: flex;
  gap: 22px;
  color: #475569;
  font-size: 12px;
}

.preview-dates strong {
  color: #0f172a;
}

.preview-footer {
  margin-top: 16px;
  display: flex;
  justify-content: space-between;
  align-items: center;
}

.preview-footer span {
  display: inline-flex;
  align-items: center;
  gap: 6px;
  padding: 7px 13px;
  background: #dcfce7;
  color: #166534;
  border-radius: 999px;
  font-size: 12px;
  font-weight: 950;
}

.barcode {
  display: flex;
  align-items: end;
  gap: 3px;
  height: 36px;
}

.barcode i {
  width: 3px;
  background: white;
  display: block;
}

.fake-qr {
  display: grid;
  gap: 3px;
  background: white;
  border-radius: 10px;
  padding: 8px;
  border: 1px solid #e2e8f0;
}

.fake-qr.large {
  width: 96px;
  height: 96px;
  grid-template-columns: repeat(6, 1fr);
}

.fake-qr.small {
  width: 36px;
  height: 36px;
  grid-template-columns: repeat(4, 1fr);
  padding: 4px;
  gap: 2px;
}

.fake-qr span {
  border-radius: 2px;
  background: #e5e7eb;
}

.fake-qr span.fill {
  background: #111827;
}

.quick-card-action {
  width: 100%;
  height: 96px;
  border: 1px solid #e2e8f0;
  border-radius: 16px;
  background: linear-gradient(180deg, #ffffff, #f8fafc);
  display: grid;
  place-items: center;
  gap: 6px;
  cursor: pointer;
  transition: 0.22s;
}

.quick-card-action span {
  font-size: 12px;
  font-weight: 900;
}

.quick-card-action:hover {
  transform: translateY(-3px);
  box-shadow: 0 16px 30px rgba(15, 23, 42, 0.08);
}

.mini-stat-card {
  min-height: 138px;
}

.library-card-table :deep(tbody tr) {
  cursor: pointer;
}

.library-card-table :deep(tbody tr:hover) {
  background: #f0fdfa;
}

@media print {
  body * {
    visibility: hidden;
  }

  .library-card-preview,
  .library-card-preview * {
    visibility: visible;
  }

  .library-card-preview {
    position: fixed;
    left: 20px;
    top: 20px;
    width: 680px;
  }
}



/* ================= FIX THẺ THƯ VIỆN - RÕ CHỮ, GỌN, ĐẸP ================= */
.library-cards-page {
  color: #0f172a;
}

.library-cards-page .section-title-row {
  display: flex;
  align-items: center;
  justify-content: space-between;
  gap: 12px;
}

.library-cards-page .section-title-row h3 {
  margin: 0;
  font-size: 17px;
  font-weight: 900;
  color: #0f172a;
}

.library-cards-page .section-title-row p {
  margin: 4px 0 0;
  color: #64748b;
  font-size: 13px;
  font-weight: 600;
}

.card-preview-panel {
  min-height: 340px;
}

.library-id-card {
  position: relative;
  overflow: hidden;
  min-height: 280px;
  border-radius: 22px;
  padding: 22px;
  color: white;
  background: linear-gradient(135deg, #065f46 0%, #047857 52%, #0f766e 100%);
  box-shadow: 0 20px 42px rgba(6, 95, 70, 0.22);
}

.card-wave {
  position: absolute;
  right: -90px;
  top: -70px;
  width: 320px;
  height: 190px;
  border-radius: 50%;
  border: 1px solid rgba(255, 255, 255, 0.16);
}

.card-wave.wave-2 {
  top: -20px;
  right: -130px;
  width: 360px;
  height: 220px;
}

.id-card-head,
.id-card-body,
.id-card-foot {
  position: relative;
  z-index: 2;
}

.id-card-head {
  display: flex;
  align-items: center;
  gap: 12px;
  margin-bottom: 18px;
}

.id-logo {
  width: 46px;
  height: 46px;
  border-radius: 12px;
  background: white;
  display: grid;
  place-items: center;
}

.id-card-head strong {
  display: block;
  font-size: 18px;
  font-weight: 950;
  letter-spacing: 0.5px;
}

.id-card-head p {
  margin: 2px 0 0;
  color: #d1fae5;
  font-size: 13px;
  font-weight: 700;
}

.id-card-body {
  display: grid;
  grid-template-columns: 108px 1fr 96px;
  gap: 18px;
  align-items: center;
  background: rgba(255, 255, 255, 0.92);
  color: #0f172a;
  border-radius: 18px;
  padding: 16px;
}

.card-avatar {
  width: 96px;
  height: 104px;
  border-radius: 14px;
  object-fit: cover;
}

.card-owner-info h2 {
  margin: 0 0 4px;
  font-size: 25px;
  font-weight: 950;
  color: #065f46;
}

.card-owner-info h4 {
  margin: 0 0 8px;
  font-size: 18px;
  font-weight: 900;
}

.card-meta {
  display: flex;
  flex-wrap: wrap;
  gap: 10px;
  color: #334155;
  font-size: 13px;
  font-weight: 800;
}

.date-row {
  display: flex;
  gap: 24px;
  margin-top: 12px;
}

.date-row small {
  display: block;
  color: #64748b;
  font-size: 11px;
  font-weight: 800;
}

.date-row b {
  display: block;
  color: #0f172a;
  font-size: 12px;
}

.qr-code,
.mini-qr {
  display: grid;
  background: white;
  border: 1px solid #e2e8f0;
}

.qr-code {
  width: 86px;
  height: 86px;
  grid-template-columns: repeat(7, 1fr);
  gap: 4px;
  padding: 8px;
  border-radius: 10px;
}

.qr-code span,
.mini-qr span {
  border-radius: 2px;
  background: #e5e7eb;
}

.qr-code span.filled,
.mini-qr span.filled {
  background: #0f172a;
}

.id-card-foot {
  display: flex;
  justify-content: space-between;
  align-items: end;
  margin-top: 16px;
}

.barcode {
  display: flex;
  align-items: end;
  gap: 3px;
}

.barcode i {
  width: 3px;
  background: white;
  display: block;
}

.id-card-foot small {
  display: block;
  text-align: right;
  margin-top: 4px;
  color: #d1fae5;
  font-weight: 800;
}

.quick-card-actions {
  display: grid;
  grid-template-columns: repeat(5, 1fr);
  gap: 14px;
}

.quick-card-btn {
  height: 92px;
  border: 1px solid #e2e8f0;
  border-radius: 16px;
  background: #ffffff;
  cursor: pointer;
  display: grid;
  place-items: center;
  gap: 6px;
  font-weight: 900;
  transition: 0.2s;
}

.quick-card-btn:hover {
  transform: translateY(-2px);
  box-shadow: 0 12px 30px rgba(15, 23, 42, 0.08);
}

.quick-card-btn span {
  font-size: 13px;
}

.library-card-stats {
  display: grid;
  grid-template-columns: repeat(4, 1fr);
  gap: 14px;
}

.card-stat-box {
  min-height: 118px;
  border: 1px solid #e2e8f0;
  border-radius: 16px;
  padding: 14px;
  background: #ffffff;
}

.stat-mini-icon {
  width: 44px;
  height: 44px;
  border-radius: 12px;
  display: grid;
  place-items: center;
  background: #f0fdfa;
  margin-bottom: 10px;
}

.card-stat-box p {
  margin: 0;
  color: #64748b;
  font-weight: 800;
  font-size: 13px;
}

.card-stat-box h2 {
  margin: 4px 0;
  color: #0f172a;
  font-size: 24px;
  font-weight: 950;
}

.card-stat-box span {
  color: #64748b;
  font-weight: 700;
  font-size: 12px;
}

.library-table-wrap {
  width: 100%;
  overflow-x: auto;
}

.library-card-table {
  width: 100%;
  border-collapse: collapse;
  min-width: 1080px;
}

.library-card-table thead {
  background: #f8fafc;
}

.library-card-table th {
  padding: 13px 12px;
  color: #0f172a;
  font-size: 13px;
  font-weight: 950;
  border-bottom: 1px solid #e2e8f0;
  white-space: nowrap;
}

.library-card-table td {
  padding: 13px 12px;
  border-bottom: 1px solid #edf2f7;
  color: #0f172a;
  font-size: 14px;
  vertical-align: middle;
}

.library-card-table tbody tr {
  cursor: pointer;
}

.library-card-table tbody tr:hover,
.library-card-table tbody tr.selected {
  background: #ecfdf5;
}

.card-id-cell {
  color: #047857 !important;
  font-weight: 950;
}

.owner-cell {
  display: flex;
  align-items: center;
  gap: 12px;
}

.owner-cell strong {
  display: block;
  color: #0f172a;
  font-weight: 950;
}

.owner-cell span {
  display: block;
  color: #64748b;
  font-size: 12px;
  margin-top: 2px;
}

.status-pill,
.type-pill {
  display: inline-flex;
  align-items: center;
  justify-content: center;
  padding: 7px 12px;
  border-radius: 999px;
  font-size: 12px;
  font-weight: 950;
  white-space: nowrap;
}

.status-pill.light {
  background: #d1fae5 !important;
  color: #047857 !important;
}

.status-active {
  background: #dcfce7;
  color: #15803d;
}

.status-expiring {
  background: #ffedd5;
  color: #ea580c;
}

.status-expired {
  background: #fee2e2;
  color: #dc2626;
}

.status-locked {
  background: #e2e8f0;
  color: #475569;
}

.type-sinh-viên,
.type-thẻ-thường {
  background: #ccfbf1;
  color: #047857;
}

.type-giảng-viên {
  background: #dbeafe;
  color: #2563eb;
}

.type-cộng-tác-viên {
  background: #ede9fe;
  color: #7c3aed;
}

.date-danger {
  color: #ef4444;
  font-weight: 950;
}

.date-warning {
  color: #f97316;
  font-weight: 950;
}

.mini-qr {
  width: 36px;
  height: 36px;
  grid-template-columns: repeat(5, 1fr);
  gap: 2px;
  padding: 5px;
  border-radius: 8px;
  cursor: pointer;
}

.table-card-actions {
  display: flex;
  justify-content: center;
  gap: 8px;
}

.table-card-actions button {
  width: 34px;
  height: 34px;
  border: 1px solid #e2e8f0;
  border-radius: 9px;
  background: white;
  color: #0d9488;
  cursor: pointer;
}

.table-card-actions button.orange {
  color: #f97316;
}

.table-card-actions button:hover {
  background: #f0fdfa;
}

.empty-table-text {
  text-align: center;
  color: #64748b !important;
  padding: 32px !important;
}

.card-pagination,
.card-bulk-bar {
  display: flex;
  align-items: center;
  justify-content: space-between;
  gap: 12px;
  margin-top: 16px;
  flex-wrap: wrap;
}

.card-bulk-bar {
  padding: 14px;
  border-radius: 14px;
  background: #f8fafc;
}

.card-bulk-bar span {
  font-weight: 900;
  color: #0f172a;
}

.top-actions {
  display: flex;
  gap: 10px;
}

@media (max-width: 1200px) {
  .quick-card-actions,
  .library-card-stats {
    grid-template-columns: repeat(2, 1fr);
  }

  .id-card-body {
    grid-template-columns: 90px 1fr;
  }

  .qr-code {
    display: none;
  }
}

@media (max-width: 700px) {
  .quick-card-actions,
  .library-card-stats {
    grid-template-columns: 1fr;
  }

  .id-card-body {
    grid-template-columns: 1fr;
  }

  .top-actions {
    width: 100%;
    flex-direction: column;
  }
}

/* ================= CSS CỦA TRANG BÁO CÁO ================= */
.report-panel, .report-kpi-card { border: 1px solid #e2e8f0; border-radius: 18px !important; background: white; box-shadow: 0 4px 15px rgba(0, 0, 0, 0.02); padding: 20px; }
.filter-label { font-size: 11px; font-weight: 800; color: #64748b; margin-bottom: 4px; text-transform: uppercase; letter-spacing: 0.5px; }

.report-kpi-card { min-height: 140px; display: flex; justify-content: center; align-items: center; transition: 0.2s; }
.report-kpi-card:hover { transform: translateY(-3px); box-shadow: 0 10px 25px rgba(13, 148, 136, 0.1); }
.kpi-title { font-size: 13px; font-weight: 850; color: #334155; margin-bottom: 4px; }
.kpi-value { font-size: 26px; font-weight: 950; color: #0f172a; margin-bottom: 4px; }
.kpi-trend { font-size: 12px; font-weight: 800; color: #0d9488; }

.panel-title { margin: 0; font-size: 17px; font-weight: 950; color: #0f172a; }
.custom-select :deep(.v-field__input) { min-height: 34px !important; padding-top: 0; padding-bottom: 0; font-size: 13px; font-weight: 700; color: #475569; }

/* SVG AREA CHART */
.svg-chart-container { height: 260px; display: flex; align-items: flex-end; padding-top: 10px; }
.y-axis-labels { display: flex; flex-direction: column; justify-content: space-between; height: 240px; padding-right: 12px; font-size: 11px; font-weight: 700; color: #94a3b8; }
.svg-wrapper { flex-grow: 1; height: 260px; position: relative; }
.x-axis-labels { display: flex; justify-content: space-between; padding-top: 10px; font-size: 11px; font-weight: 700; color: #94a3b8; margin-left: -10px; margin-right: -10px; }

.legend-area span { display: inline-flex; align-items: center; font-size: 12px; font-weight: 800; color: #475569; }
.legend-area i { width: 14px; height: 10px; border-radius: 3px; margin-right: 6px; }

/* DONUT CHART (CSS PURE) */
.new-donut { width: 180px; height: 180px; border-radius: 50%; background: conic-gradient( #0d9488 0 28%, #2563eb 28% 50%, #f97316 50% 68%, #a855f7 68% 83%, #ef4444 83% 93%, #9ca3af 93% 100% ); display: grid; place-items: center; }
.new-donut-hole { width: 100px; height: 100px; border-radius: 50%; background: white; display: flex; flex-direction: column; align-items: center; justify-content: center; }
.new-donut-hole strong { font-size: 24px; font-weight: 950; line-height: 1; color: #0f172a; }
.new-donut-hole span { font-size: 12px; font-weight: 700; color: #64748b; margin-top: 4px; }
.donut-legend { display: grid; grid-template-columns: 1fr 1fr; gap: 8px 16px; }
.legend-dot { width: 10px; height: 10px; border-radius: 50%; display: inline-block; margin-right: 6px; }

/* PROGRESS BARS */
.progress-list { width: 100%; }
.progress-track { height: 10px; border-radius: 999px; background: #e2e8f0; overflow: hidden; width: 100%; }
.progress-fill { height: 100%; background: #0d9488; border-radius: 999px; }

/* TABLE BÁO CÁO */
.report-table :deep(th) { border-bottom: 1px solid #e2e8f0 !important; background-color: transparent !important; font-weight: 800 !important; color: #64748b !important; }
.report-table :deep(td) { border-bottom: 1px solid #f1f5f9 !important; padding: 10px 0 !important; }

/* TERMINAL LOG */
.terminal-log { background: #0f172a; color: #10b981; border-radius: 12px; padding: 16px; font-family: monospace !important; font-size: 13px; line-height: 1.6; height: 140px; overflow-y: auto; }

/* IN ẨN SIDEBAR */
@media print {
  .sidebar, .v-app-bar, .terminal-log, button { display: none !important; }
  .main-scroll { height: auto !important; overflow: visible !important; }
  .report-section { background: white !important; }
}

/* ===================== SETTINGS PAGE ===================== */

.settings-page {
  padding-bottom: 28px;
}

.settings-tabs-card {
  display: flex;
  align-items: center;
  gap: 8px;
  padding: 0 16px;
  min-height: 70px;
  border: 1px solid #e5e7eb !important;
  border-radius: 18px !important;
  background: #ffffff;
  margin-bottom: 18px;
  overflow-x: auto;
}

.setting-tab {
  height: 70px;
  min-width: 180px;
  border: none;
  background: transparent;
  color: #64748b;
  font-weight: 850;
  cursor: pointer;
  display: flex;
  align-items: center;
  justify-content: center;
  gap: 10px;
  border-bottom: 3px solid transparent;
}

.setting-tab.active {
  color: #0d9488;
  border-bottom-color: #0d9488;
}

.settings-grid {
  display: grid;
  grid-template-columns: 1.25fr 1fr 1fr;
  gap: 18px;
}

.setting-card {
  background: white;
  border: 1px solid #e5e7eb !important;
  border-radius: 18px !important;
  padding: 22px;
  box-shadow: 0 10px 26px rgba(15, 23, 42, 0.04);
}

.setting-card.full-card {
  width: 100%;
}

.setting-card-title {
  display: flex;
  align-items: center;
  gap: 10px;
  margin-bottom: 22px;
}

.setting-card-title h3 {
  margin: 0;
  font-size: 18px;
  font-weight: 950;
  color: #111827;
}

.row-between {
  justify-content: space-between;
}

.form-row {
  display: grid;
  grid-template-columns: 1fr 1fr;
  gap: 14px;
}

.form-group {
  display: grid;
  gap: 7px;
  margin-bottom: 16px;
}

.form-group label {
  font-size: 13px;
  font-weight: 850;
  color: #334155;
}

.form-group input,
.form-group select,
.form-group textarea,
.input-with-unit input,
.color-input input,
.log-filter input,
.log-filter select {
  width: 100%;
  border: 1px solid #d1d5db;
  border-radius: 10px;
  min-height: 44px;
  padding: 0 12px;
  outline: none;
  background: white;
  color: #111827;
}

.form-group textarea {
  padding: 12px;
  resize: vertical;
}

.input-with-unit {
  display: grid;
  grid-template-columns: 1fr 82px;
}

.input-with-unit input {
  border-radius: 10px 0 0 10px;
}

.input-with-unit span {
  min-height: 44px;
  display: grid;
  place-items: center;
  border: 1px solid #d1d5db;
  border-left: none;
  border-radius: 0 10px 10px 0;
  color: #334155;
  background: #f8fafc;
  font-weight: 700;
}

.switch-list {
  display: grid;
  gap: 12px;
}

.switch-row {
  min-height: 44px;
  display: flex;
  align-items: center;
  justify-content: space-between;
  gap: 16px;
}

.switch-row span {
  color: #111827;
  font-weight: 700;
}

.checkbox-list {
  display: grid;
  gap: 16px;
}

.checkbox-list label {
  display: flex;
  align-items: center;
  gap: 12px;
  color: #111827;
  font-weight: 700;
}

.checkbox-list input[type='checkbox'] {
  width: 18px;
  height: 18px;
  accent-color: #0d9488;
}

.inline-setting {
  display: grid;
  grid-template-columns: 1fr 70px 50px;
  gap: 8px;
  align-items: center;
  margin-left: 30px;
}

.inline-setting input {
  height: 38px;
  border: 1px solid #d1d5db;
  border-radius: 8px;
  text-align: center;
}

.color-input {
  display: grid;
  grid-template-columns: 60px 1fr;
  gap: 10px;
}

.color-input input[type='color'] {
  padding: 4px;
}

.save-setting-btn,
.outline-setting-btn {
  min-height: 42px;
  border-radius: 10px;
  padding: 0 18px;
  cursor: pointer;
  font-weight: 900;
  display: inline-flex;
  align-items: center;
  justify-content: center;
  gap: 7px;
}

.save-setting-btn {
  border: none;
  background: linear-gradient(135deg, #0d9488, #14b8a6);
  color: white;
  box-shadow: 0 8px 18px rgba(13, 148, 136, 0.22);
}

.outline-setting-btn {
  border: 1px solid #0d9488;
  background: white;
  color: #0d9488;
}

.save-setting-btn.small,
.outline-setting-btn.small {
  min-height: 38px;
  font-size: 13px;
}

.setting-note {
  color: #64748b;
  line-height: 1.6;
  margin-bottom: 22px;
}

.setting-table {
  width: 100%;
  border-collapse: collapse;
}

.setting-table th {
  text-align: left;
  padding: 14px 12px;
  background: #f8fafc;
  font-weight: 950;
  color: #0f172a;
  border-bottom: 1px solid #e5e7eb;
}

.setting-table td {
  padding: 14px 12px;
  border-bottom: 1px solid #f1f5f9;
}

.account-cell {
  display: flex;
  align-items: center;
  gap: 12px;
}

.account-cell strong {
  display: block;
  font-weight: 950;
}

.account-cell span {
  display: block;
  color: #64748b;
  font-size: 12px;
}

.role-chip,
.account-status {
  display: inline-flex;
  padding: 7px 12px;
  border-radius: 999px;
  font-size: 12px;
  font-weight: 950;
}

.role-chip {
  background: #e0f2fe;
  color: #0369a1;
}

.account-status {
  background: #dcfce7;
  color: #16a34a;
}

.account-status.locked {
  background: #fee2e2;
  color: #ef4444;
}

.setting-actions {
  display: flex;
  gap: 8px;
}

.setting-actions button {
  width: 34px;
  height: 34px;
  border: none;
  border-radius: 9px;
  background: #f8fafc;
  color: #334155;
  cursor: pointer;
}

.setting-actions button.danger {
  color: #ef4444;
}

.role-grid {
  display: grid;
  grid-template-columns: repeat(3, 1fr);
  gap: 18px;
}

.role-card {
  border: 1px solid #e5e7eb;
  border-radius: 16px;
  padding: 18px;
  background: #f8fafc;
}

.role-card h4 {
  margin: 0 0 6px;
  font-weight: 950;
}

.role-card p {
  margin: 0 0 14px;
  color: #64748b;
}

.role-card label {
  display: flex;
  align-items: center;
  gap: 10px;
  margin-bottom: 10px;
  font-weight: 700;
}

.role-card input {
  accent-color: #0d9488;
}

.file-input {
  width: 100%;
  border: 1px dashed #0d9488;
  border-radius: 12px;
  padding: 18px;
  background: #f0fdfa;
}

.outline-mini-btn {
  height: 32px;
  border: 1px solid #0d9488;
  border-radius: 8px;
  background: white;
  color: #0d9488;
  cursor: pointer;
  font-weight: 850;
  padding: 0 12px;
}

.log-filter {
  display: grid;
  grid-template-columns: 200px 1fr;
  gap: 14px;
  margin-bottom: 16px;
}

.system-log-box {
  background: #111827;
  color: #d1fae5;
  border-radius: 12px;
  padding: 16px;
  max-height: 420px;
  overflow-y: auto;
  font-family: Consolas, monospace !important;
}

.log-row {
  display: flex;
  gap: 10px;
  align-items: flex-start;
  padding: 8px 0;
  border-bottom: 1px solid rgba(255,255,255,0.08);
}

.log-row span {
  font-weight: 950;
}

.log-row.info span {
  color: #86efac;
}

.log-row.warn span {
  color: #fbbf24;
}

.log-row.error span {
  color: #f87171;
}

.log-row b {
  color: #93c5fd;
  min-width: 145px;
}

.log-row p {
  margin: 0;
}

.dialog-head {
  display: flex;
  align-items: center;
  justify-content: space-between;
  margin-bottom: 18px;
}

.dialog-head h3 {
  margin: 0;
  font-weight: 950;
}

@media (max-width: 1200px) {
  .settings-grid,
  .role-grid {
    grid-template-columns: 1fr 1fr;
  }
}

@media (max-width: 800px) {
  .settings-grid,
  .role-grid,
  .form-row,
  .log-filter {
    grid-template-columns: 1fr;
  }

  .setting-tab {
    min-width: 150px;
  }

  .setting-table {
    min-width: 850px;
  }

  .setting-card.full-card {
    overflow-x: auto;
  }
}

</style>
