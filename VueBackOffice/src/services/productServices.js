import axios from 'axios'


export default {

    getProducts(pageNumber,pageSize,orderProperty,asc)

    {

        return axios.get("https://localhost:44355/Product/products/"+pageNumber+"/"+pageSize+"/"+orderProperty+"/"+asc)

                .then(response => (response.data));

    },



    getBrandDetail(id)

    {

        return axios.get("https://localhost:44355/Product/detail/"+id)

                .then(response => (response))

    }

}