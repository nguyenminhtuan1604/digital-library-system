import api from "./api";

export const reportService = {
  getOverview(params = {}) {
    return api.get("/api/reports/overview", { params });
  },
  getDashboard() {
    return api.get("/api/dashboard");
  },
  getBooks(params = {}) {
    return api.get("/api/reports/books", { params });
  },
  getBorrowing(params = {}) {
    return api.get("/api/reports/borrowing", { params });
  },
  getFines(params = {}) {
    return api.get("/api/reports/fines", { params });
  },
  exportReport(params = {}) {
    return api.get("/api/reports/export", { params, responseType: "blob" });
  }
};
