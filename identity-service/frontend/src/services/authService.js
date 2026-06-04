import api from "./api";

export const authService = {
  login(data) {
    return api.post("/api/auth/login", data);
  },
  register(data) {
    return api.post("/api/auth/register", data);
  },
  logout() {
    localStorage.clear();
  }
};
