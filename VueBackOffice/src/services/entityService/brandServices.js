import Repository from "../repository";

const resource = "/Brand";
export default {
  getBrands(pageNumber, pageSize, brandName) {
    return Repository.get(
      `https://localhost:44355${resource}/brands/${pageNumber}/${pageSize}/${brandName}`
    )
    .then((response) => response.data);
  },

  getBrandsName() {
    return Repository.get(`https://localhost:44355${resource}/brandnames`)
    .then((response) => response.data);
  },

  getBrandDetail(id) {
    return Repository.get("https://localhost:44355/Brand/detail/" + id)
    .then((response) => response.data);
  },
  addBrand(brand) {
    return Repository.post(
      `https://localhost:44355${resource}/new`,
      brand)
    // ).then((response) => response.data,response=>{
    //   if(response.status==400){
    //     console.log('hello');
    //   }
    // });
  },
  getBrandById(id) {
    return Repository.get(`https://localhost:44355${resource}/${id}`).then(
      (response) => response.data
    );
  },
  updateBrand(brand) {
    return Repository.put(`https://localhost:44355${resource}/update`, brand);
  },
  deleteBrand(id) {
    return Repository.delete(`https://localhost:44355${resource}/delete/${id}`);
  },
};
