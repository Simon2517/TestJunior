import axios from "axios";

const resource="/Product"
export default {

    getProducts(pageNumber,pageSize,orderProperty,asc,brandFilter)

    {

        return axios.get(`https://localhost:44355${resource}/products/${pageNumber}/${pageSize}/${orderProperty}/${asc}/${brandFilter}`)

                .then(response => (response.data));

    },



    getBrandDetail(id)

    {

        return axios.get(`${resource}/${id}`)

                .then(response => (response))

    }

}