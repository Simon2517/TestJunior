import Vue from "vue";
import Router from "vue-router";

import Brands from "../components/Brand/Brand.vue";
import Products from "../components/Product/Product.vue";
import InfoRequest from "../components/InfoRequests/Inforequest.vue";

import AddBrand from "../components/Brand/AddBrand.vue";
import EditBrand from "../components/Brand/EditBrand.vue";
import AddOrEditProduct from "../components/Product/AddOrEditProduct.vue";

import BrandDetail from "../components/Brand/BrandDetail.vue";
import ProductDetail from "../components/Product/ProductDetail.vue";
import RequestDetail from "../components/InfoRequests/RequestDetail.vue";
Vue.use(Router);

export default new Router({
  routes: [
    {
      path: "/brands",
      name: "brandList",
      component: Brands,
    },
    {
      path: "/brand/detail/:id",
      name: "brandDetail",
      component: BrandDetail,
    },
    {
      path: "/brand/new",
      name: "addBrand",
      component: AddBrand,
    },
    {
      path: "/brand/:id/edit",
      name: "editBrand",
      component: EditBrand,
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
      path: "/product/:id/edit",
      name: "editProduct",
      component: AddOrEditProduct,
    },
    {
      path: "/product/detail/:id",
      name: "productDetail",
      component: ProductDetail,
    },
    {
      path: "/inforequest",
      name: "leads",
      component: InfoRequest,
    },
    {
      path: "/leed/detail/:id",
      name: "leedDetail",
      component: RequestDetail,
    },
  ],
});
