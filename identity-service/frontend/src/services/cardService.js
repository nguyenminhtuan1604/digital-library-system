import api from "./api";

export const cardService = {
  getCards() {
    return api.get("/api/cards");
  },
  createCard(data) {
    return api.post("/api/cards", data);
  },
  lockCard(id) {
    return api.put(`/api/cards/${id}/lock`);
  },
  unlockCard(id) {
    return api.put(`/api/cards/${id}/unlock`);
  },
  renewCard(id) {
    return api.put(`/api/cards/${id}/renew`);
  },
  validateCard(readerId) {
    return api.get(`/api/cards/validate/${readerId}`);
  }
};
