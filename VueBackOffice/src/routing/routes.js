import Vue from 'vue'
import Router from 'vue-router'
import Brands from '../components/Brand.vue';
import Products from '../components/Product.vue';
import InfoRequest from '../components/Inforequest.vue';
Vue.use(Router)

export default new Router({
    routes: [
        {
            path: '/brands',
            name: 'brandList',
            component:Brands
        },
        {
            path: '/products',
            name: 'productList',
            component:Products
        },        
        {
            path: '/inforequest',
            name: 'leads',
            component:InfoRequest
        },
    ],


})