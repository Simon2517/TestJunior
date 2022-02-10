import Vue from "vue";
import Router from "vue-router";
import Brands from "../components/Brand/Brand.vue";
import Products from "../components/Product/Product.vue";
import InfoRequest from "../components/Inforequest.vue";
import AddBrand from "../components/Brand/AddBrand.vue";
import AddOrEditProduct from "../components/Product/AddOrEditProduct.vue";
Vue.use(Router);

export default new Router({
  routes: [
    {
      path: "/brands",
      name: "brandList",
      component: Brands,
    },
    {
      path: "/brand/new",
      name: "addBrand",
      component: AddBrand,
    },
    {
      path: "/products",
      name: "productList",
      component: Products,
    },
    {
      path: "/product/new",
      name: "addProduct",
      component: AddOrEditProduct,
    },
    {
      path: "/inforequest",
      name: "leads",
      component: InfoRequest,
    },
  ],
});
