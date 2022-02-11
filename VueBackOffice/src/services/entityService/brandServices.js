import Repository from '../repository'

const resource="/Brand"
export default {

    getBrands(pageNumber,pageSize,orderProperty,asc)

    {

        return Repository.get(`https://localhost:44355${resource}/brands/${pageNumber}/${pageSize}/${orderProperty}/${asc}`)

                .then(response => (response.data));

    },

    getBrandsName()

    {

        return Repository.get(`https://localhost:44355${resource}/brandnames`)

                .then(response => (response.data));

    },

    getBrandDetail(id)

    {

        return Repository.get("https://localhost:44355/Brand/detail/"+id)

                .then(response => (response.data))

    },
    addBrand(brand){
        console.log(brand)
        return Repository.post(`https://localhost:44355${resource}/new`,brand)
            .then(response=>(response.data))
    },
    getBrandById(id){
        return Repository.get(`https://localhost:44355${resource}/${id}`)
            .then(response=>(response.data))

    },
    updateBrand(brand){
        return Repository.put(`https://localhost:44355${resource}/update`,brand)
    }

}