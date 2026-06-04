import api from "./api";

export const settingsService = {
  getSettings() {
    return api.get("/api/settings");
  },
  saveSettings(data) {
    return api.put("/api/settings", data);
  }
};
