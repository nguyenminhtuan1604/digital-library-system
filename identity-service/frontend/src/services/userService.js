import api from "./api";

export const userService = {
  getUsers() {
    return api.get("/api/users");
  },
  createUser(data) {
    return api.post("/api/users", data);
  },
  updateUser(id, data) {
    return api.put(`/api/users/${id}`, data);
  },
  deleteUser(id) {
    return api.delete(`/api/users/${id}`);
  },
  importUsers(file) {
    const formData = new FormData();
    formData.append("file", file);
    return api.post("/api/users/import", formData, {
      headers: { "Content-Type": "multipart/form-data" }
    });
  },
  exportUsers() {
    return api.get("/api/users/export", { responseType: "blob" });
  },
  getProfile(id) {
    return api.get(`/api/users/${id}/profile`);
  }
};
