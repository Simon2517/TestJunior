import Repository from "./repository";

const resource="/Product"
export default {

    getProducts(pageNumber,pageSize,orderProperty,asc,brandFilter)

    {

        return Repository.get(`https://localhost:44355${resource}/products/${pageNumber}/${pageSize}/${orderProperty}/${asc}/${brandFilter}`)

                .then(response => (response.data));

    },



    getBrandDetail(id)

    {

        return Repository.get(`${resource}/${id}`)

                .then(response => (response))

    }

}