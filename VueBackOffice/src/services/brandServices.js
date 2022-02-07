import axios from 'axios'


export default {

    getBrands(pageNumber,pageSize,orderProperty,asc)

    {

        return axios.get("https://localhost:44355/Brand/brands/"+pageNumber+"/"+pageSize+"/"+orderProperty+"/"+asc)

                .then(response => (response.data));

    },

    getBrandsName()

    {

        return axios.get("https://localhost:44355/Brand/brandnames")

                .then(response => (response.data));

    },

    getBrandDetail(id)

    {

        return axios.get("https://localhost:44355/Brand/detail/"+id)

                .then(response => (response))

    }

}