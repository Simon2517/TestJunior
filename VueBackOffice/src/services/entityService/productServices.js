import Repository from "../repository";

const resource = "/Product";
export default {
  getProducts(pageNumber, pageSize, orderProperty, asc, brandFilter) {
    return Repository.get(
      `https://localhost:44355${resource}/products/${pageNumber}/${pageSize}/${orderProperty}/${asc}/${brandFilter}`
    )
    .then((response) => response.data);
  },

  getProductDetail(id) {
    return Repository.get(`${resource}/detail/${id}`)
    .then((response) => response.data);
  },

  addProduct(product) {
    return Repository.post(`https://localhost:44355${resource}/new`, product);
  },
  getProductById(id) {
    if (id != null)
      return Repository.get(`https://localhost:44355${resource}/${id}`).then(
        (response) => response.data
      );
    else return null;
  },
  updateProduct(product) {
    return Repository.put(`https://localhost:44355${resource}/update`, product);
  },
  deleteProduct(id) {
    return Repository.delete(`https://localhost:44355${resource}/delete/${id}`);
  },
};
