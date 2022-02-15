import Repository from "../repository";

const resource = "/InfoRequest";
export default {
  getRequests(pageNumber, pageSize, asc, brandId, productId, productName) {
    return Repository.get(
      `https://localhost:44355${resource}/requests/${pageNumber}/${pageSize}/${asc}/${brandId}/${productId}/${productName}`
    ).then((response) => response.data);
  },

  getRequestDetail(id) {
    return Repository.get(`${resource}/detail/${id}`)
    .then((response) => response.data);
  },
};
